char *title = "Erosion and Dilation filter";
char *description = "Erosion and Dilation filter";
/*
Фильтр «минимум» – также известный как фильтр эрозии, заменяет значение минимальным в окрестности.
Фильтр «максимум» – также известный как фильтр расширения, заменяет значение максимальным в окрестности.
*/

#include <iostream>
#include <cstdio>
#include <cstdlib>
#include <assert.h>
#include <time.h>
#include <cuda.h>
#include <cuda_runtime.h>

#ifndef max
#define max( a, b ) ( ((a) > (b)) ? (a) : (b) )
#endif

#ifndef min
#define min( a, b ) ( ((a) < (b)) ? (a) : (b) )
#endif

#ifndef uint8_t
typedef unsigned char uint8_t;
#endif

#ifndef uint16_t
typedef unsigned short uint16_t;
#endif

#ifndef uint32_t
typedef unsigned int uint32_t;
#endif

// http://en.wikipedia.org/wiki/BMP_file_format
#define BMPMAGIC	0x00
#define BMPFILESIZE	0x02
#define BMPOFFSET	0x0A
#define BMPDIBSISE	0x0E
#define BMPWIDTH	0x12
#define BMPHEIGHT	0x16
#define BMPBITSPERPIXEL	0x1C

typedef struct {
	uint8_t magic[2];   /* the magic number used to identify the BMP file:
						0x42 0x4D (Hex code points for B and M).
						The following entries are possible:
						BM - Windows 3.1x, 95, NT, ... etc
						BA - OS/2 Bitmap Array
						CI - OS/2 Color Icon
						CP - OS/2 Color Pointer
						IC - OS/2 Icon
						PT - OS/2 Pointer. */
	uint32_t filesz;    /* the size of the BMP file in bytes */
	uint16_t creator1;  /* reserved. */
	uint16_t creator2;  /* reserved. */
	uint32_t offset;    /* the offset, i.e. starting address,
						of the byte where the bitmap data can be found. */
} bmp_header_t;

typedef struct {
	uint32_t header_sz;     /* the size of this header (40 bytes) */
	uint32_t width;         /* the bitmap width in pixels */
	uint32_t height;        /* the bitmap height in pixels */
	uint16_t nplanes;       /* the number of color planes being used.
							Must be set to 1. */
	uint16_t depth;         /* the number of bits per pixel,
							which is the color depth of the image.
							Typical values are 1, 4, 8, 16, 24 and 32. */
	uint32_t compress_type; /* the compression method being used.
							See also bmp_compression_method_t. */
	uint32_t bmp_bytesz;    /* the image size. This is the size of the raw bitmap
							data (see below), and should not be confused
							with the file size. */
	uint32_t hres;          /* the horizontal resolution of the image.
							(pixel per meter) */
	uint32_t vres;          /* the vertical resolution of the image.
							(pixel per meter) */
	uint32_t ncolors;       /* the number of colors in the color palette,
							or 0 to default to 2<sup><i>n</i></sup>. */
	uint32_t nimpcolors;    /* the number of important colors used,
							or 0 when every color is important;
							generally ignored. */
} bmp_dib_v3_header_t;


__global__ void global_set2grayscale(
	float *grayscale,
	unsigned int *input,
	float weight,
	int width, 
	int height)
{
	for (int id = blockDim.x*blockIdx.x + threadIdx.x;
		id < width*height;
		id += blockDim.x*gridDim.x) {
			grayscale[id] += weight*(float)(input[id]&0xFF);
	}
}

__global__ void global_add2grayscale(
	float *grayscale,
	unsigned int *input,
	float weight,
	int width, 
	int height)
{
	for (int id = blockDim.x*blockIdx.x + threadIdx.x;
		id < width*height;
		id += blockDim.x*gridDim.x) {
			grayscale[id] += weight*(float)(input[id]&0xFF);
	}
}

// Поиск индекса элемента с наилучшим значением интенсивности
__global__ void global_find2grayscale(
	float *grayscale,
	unsigned int *index,
	int width, 
	int height, 
	float radius2, 
	int mode)
{
	int radius = 1;
	while((float)radius*radius<radius2) radius++; // поскольку числа маленькие, то быстрее перебором
	if(mode>0){ // если расширение
		for (int id = blockDim.x*blockIdx.x + threadIdx.x;
			id < width*height;
			id += blockDim.x*gridDim.x) {
				int x=id%width;
				int y=id/width;
				int currentId = id;
				// перебираем пиксели в окружности
				for(int i=max(0,x-radius);i<=min(width,x+radius);i++){
					for(int j=max(0,y-radius);j<=min(height,y+radius);j++){
						if((float)((i-x)*(i-x)+(j-y)*(j-y))<=radius2) {
							int itemId = j*width+i;
							if(grayscale[itemId]>grayscale[currentId]) currentId=itemId;
						}
					}
				}
				index[id] = currentId;
		}
	} else if(mode<0){ // если эрозия
		for (int id = blockDim.x*blockIdx.x + threadIdx.x;
			id < width*height;
			id += blockDim.x*gridDim.x) {
				int x=id%width;
				int y=id/width;
				int currentId = id;
				// перебираем пиксели в окружности
				for(int i=max(0,x-radius);i<=min(width,x+radius);i++){
					for(int j=max(0,y-radius);j<=min(height,y+radius);j++){
						if((float)((i-x)*(i-x)+(j-y)*(j-y))<=radius2) {
							int itemId = j*width+i;
							if(grayscale[itemId]<grayscale[currentId]) currentId=itemId;
						}
					}
				}
				index[id] = currentId;
		}
	}else { 
		for (int id = blockDim.x*blockIdx.x + threadIdx.x;
			id < width*height;
			id += blockDim.x*gridDim.x) {
				index[id] = id;
		}
	}
}

// Получение цвета из таблицы по индексу
__global__ void global_indexcolor(
	unsigned int *input,
	unsigned int *output,
	unsigned int *index,
	int width, 
	int height)
{
	for (int id = blockDim.x*blockIdx.x + threadIdx.x;
		id < width*height;
		id += blockDim.x*gridDim.x) {
			output[id] = input[index[id]];
	}
}


__host__ void host_erosiondilationfilter(
	int gridSize, 
	int blockSize, 
	unsigned int *r, 
	unsigned int *g, 
	unsigned int *b, 
	int width, 
	int height, 
	float radius2, 
	int mode)
{
	// формула для конвертации RGB в интенсивность
	// http://en.wikipedia.org/wiki/Grayscale
	// Y = 0.2126 * R + 0.7152 * G + 0.0722 * B 
	unsigned int *channel[3] = { r,g,b };
	float weight[3] = {0.2126f, 0.7152f, 0.0722f};
	// channel - массив данных
	// width - ширина
	// height - высота

	cudaError_t err;
	float *device_grayscale;
	unsigned int *device_input;
	unsigned int *device_output;
	unsigned int *device_index;

	err = cudaMalloc((void**)&device_input, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_grayscale, width*height*sizeof(float));

	int blocks = (gridSize > 0)? gridSize : min(15, (int)sqrt(width*height));
	int threads = (blockSize > 0)? blockSize : min(15, (int)sqrt(width*height));

	// Шаг 1. Рассчитываем монохромное изображение
	cudaMemcpy(device_input, channel[0], width*height*sizeof(unsigned int), cudaMemcpyHostToDevice);
	global_set2grayscale <<< blocks, threads >>>(device_grayscale, device_input, weight[0], width, height);
	for(int j=1;j<3;j++){
		cudaMemcpy(device_input, channel[j], width*height*sizeof(unsigned int), cudaMemcpyHostToDevice);
		global_add2grayscale <<< blocks, threads >>>(device_grayscale, device_input, weight[j], width, height);
	}

	err = cudaMalloc((void**)&device_index, width*height*sizeof(unsigned int));

	// Шаг 2. Находим индексы (то есть находим требуемую замену пикселей)
	global_find2grayscale <<< blocks, threads >>>(device_grayscale, device_index, width, height, radius2, mode);

	cudaFree((void*)device_grayscale);
	err = cudaMalloc((void**)&device_output, width*height*sizeof(unsigned int));

	// Шаг 3. Заменяем пиксели согласно ранее полученной подстановке
	for(int j=3;j-->0;){
		// Перевый раз копировать в видео память не надо, поскольку уже копировали при подсчёте монохромного изображения
		if(j<2) cudaMemcpy(device_input, channel[j], width*height*sizeof(unsigned int), cudaMemcpyHostToDevice);
		global_indexcolor <<< blocks, threads >>>(device_input, device_output, device_index, width, height);
		cudaMemcpy(channel[j], device_output, width*height*sizeof(unsigned int), cudaMemcpyDeviceToHost);
	}
	cudaFree((void*)device_index);
	cudaFree((void*)device_input);
	cudaFree((void*)device_output);

	err = err;
}


int main(int argc, char* argv[])
{
	int i, j;

	std::cout << title << std::endl;

	// Find/set the device.
	int device_count = 0;
	cudaGetDeviceCount(&device_count);
	for (i = 0; i < device_count; ++i)
	{
		cudaDeviceProp properties;
		cudaGetDeviceProperties(&properties, i);
		std::cout << "Running on GPU " << i << " (" << properties.name << ")" << std::endl;
	}

	if (argc < 5){
		printf("Usage :\t%s filter radius2 inputfilename.bmp ouputfilename.bmp\n", argv[0]);
		exit(-1);
	}

	// Получаем параметры - имена файлов

	char *filter = argv[1];
	float radius2 = atof(argv[2]);
	char *inputFileName = argv[3];
	char *outputFileName = argv[4];
	int gridSize = (argc>5)?atoi(argv[5]):0;
	int blockSize = (argc>6)?atoi(argv[6]):0;
	int mode = (strcmp(filter,"dilation")==0)?1:-1;


	printf("Title :\t%s\n", title);
	printf("Description :\t%s\n", description);
	printf("Filter :\t%s\n", filter);
	printf("Radius2 :\t%d\n", radius2);
	printf("Input file name :\t%s\n", inputFileName);
	printf("Output file name :\t%s\n", outputFileName);


	FILE *file = fopen(inputFileName, "rb");
	if (!file) { 
		fprintf(stderr, "Open file error (%s)\n", inputFileName); 
		fflush(stderr); 
		exit(-1); 
	}
	fseek(file, 0L, SEEK_END);
	long size = ftell(file);
	unsigned char *buffer = (unsigned char *)malloc((size_t)size);
	fseek(file, 0L, SEEK_SET);
	fread((void*)buffer, (size_t)1, (size_t)size, file);
	fclose(file);

	uint32_t width = *(uint32_t *)&buffer[BMPWIDTH];
	uint32_t height = *(uint32_t *)&buffer[BMPHEIGHT];
	uint32_t file_size = *(uint32_t *)&buffer[BMPFILESIZE];
	uint32_t offset = *(uint32_t *)&buffer[BMPOFFSET];
	uint16_t bits_per_pixel = *(uint16_t *)&buffer[BMPBITSPERPIXEL];
	uint16_t bytes_per_pixel = ((int)((bits_per_pixel+7)/8));
	uint32_t bytes_per_line = ((int)((bits_per_pixel * width+31)/32))*4; // http://en.wikipedia.org/wiki/BMP_file_format

	printf("BMP image size :\t%ld x %ld\n", width, height);
	printf("BMP file size :\t%ld\n", file_size);
	printf("BMP pixels offset :\t%ld\n", offset);
	printf("BMP bits per pixel :\t%d\n", bits_per_pixel);
	printf("BMP bytes per pixel :\t%d\n", bytes_per_pixel);
	printf("BMP bytes per line :\t%d\n", bytes_per_line);

	uint8_t *pixels =(uint8_t *)&buffer[offset];
	uint8_t *pixel;
	uint32_t x, y;

	/* выделение памяти под байтовые цветовые каналы */
	int count = width*height;
	unsigned int* channel[3];
	for (j = 0; j < 3; j++)
		channel[j] = (unsigned int*)malloc(count*sizeof(unsigned int));

	int pos = 0;
	for (y = 0; y < height; y++) {
		for (x = 0; x < width; x++) {
			pixel = &pixels[y*bytes_per_line+x*bytes_per_pixel];
			channel[0][pos] = (unsigned int)pixel[0];
			channel[1][pos] = (unsigned int)pixel[1];
			channel[2][pos] = (unsigned int)pixel[2];
			pos++;
		}
	}

	clock_t t1, t2;
	t1 = clock();

	host_erosiondilationfilter(gridSize, blockSize, 
		channel[0], channel[1], channel[2], 
		(int)width, (int)height,
		radius2, mode);

	t2 = clock();
	double seconds = ((double)(t2-t1))/CLOCKS_PER_SEC;
	printf("Execution time :\t%le\n", seconds);

	pos = 0;
	for (y = 0; y < height; y++) {
		for (x = 0; x < width; x++) {
			pixel = &pixels[y*bytes_per_line+x*bytes_per_pixel];
			pixel[0] = (uint8_t)channel[0][pos];
			pixel[1] = (uint8_t)channel[1][pos];
			pixel[2] = (uint8_t)channel[2][pos];
			pos++;
		}
	}
	/* высвобождаем массивы */
	for (j = 0; j < 3; j++)
		free(channel[j]);

	/* выводим результаты */
	file = fopen(outputFileName, "wb");
	if (!file) {
		fprintf(stderr, "Open file error (%s)\n", outputFileName); 
		fflush(stderr);
		exit(-1);
	}
	fwrite((void*)buffer, (size_t)1, (size_t)size, file);
	printf("Output file size :\t%ld\n", size);
	free(buffer);
	fclose(file);

	cudaDeviceReset();

	exit(0);
}