char *title = "Median Filter";
char *description = "Median Filter";

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


__global__ void global_medianfilter(
	unsigned int *input,
	unsigned int *output,
	unsigned int *buffer,
	int Nh,
	int width, 
	int height)
{
	int bufferId = (2 * Nh + 1) *(2 * Nh + 1)*(blockDim.x*blockIdx.x + threadIdx.x);
	for (int id = blockDim.x*blockIdx.x + threadIdx.x;
		id < width*height;
		id += blockDim.x*gridDim.x) {
			int x = id % width;
			int y = id / width;

			int k = 0;
			for (int dx = -Nh; dx <= Nh; dx++) {
				for (int dy = -Nh; dy <= Nh; dy++) {
					if (x + dx < 0) continue;
					if (x + dx >= width) continue;
					if (y + dy < 0) continue;
					if (y + dy >= height) continue;
					buffer[bufferId + k++] = input[id + dx + width*dy];
				}
			}
			for (int i = bufferId; i <bufferId + k - 1; i++) {
				for (int j = i + 1; j < bufferId + k; j++) {
					if (buffer[i] > buffer[j]) {
						unsigned int tmp = buffer[i];
						buffer[i] = buffer[j];
						buffer[j] = tmp;
					}
				}
			}
			output[id] = buffer[bufferId + (k >> 1)];
	}
}

__host__ void host_medianfilter(int gridSize, int blockSize, unsigned int *r, unsigned int *g, unsigned int *b, int Nh, int width, int height)
{
	unsigned int *channel[3] = { r,g,b };

	// channel - массив данных
	// Nh - полуразмер
	// width - ширина
	// height - высота

	cudaError_t err;
	unsigned int *device_input;
	unsigned int *device_output;
	unsigned int *device_buffer;

	int blocks = (gridSize > 0)? gridSize : min(15, (int)sqrt(width*height));
	int threads = (blockSize > 0)? blockSize : min(15, (int)sqrt(width*height));

	err = cudaMalloc((void**)&device_input, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_output, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_buffer, blocks*threads*(2 * Nh + 1) *(2 * Nh + 1) * sizeof(int));

	for(int j=0; j<3; j++) {
		cudaMemcpy(device_input, channel[j], width*height*sizeof(unsigned int), cudaMemcpyHostToDevice);

		global_medianfilter <<< blocks, threads >>>(device_input, device_output, device_buffer, Nh, width, height);

		cudaMemcpy(channel[j], device_output, width*height*sizeof(unsigned int), cudaMemcpyDeviceToHost);
	}

	// Освобождаем память на устройстве
	cudaFree((void*)device_input);
	cudaFree((void*)device_output);
	cudaFree((void*)device_buffer);

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
		printf("Usage :\t%s filter Nh inputfilename.bmp ouputfilename.bmp\n", argv[0]);
		exit(-1);
	}

	// Получаем параметры - имена файлов

	char *filter = argv[1];
	int step = atoi(argv[2]);
	char *inputFileName = argv[3];
	char *outputFileName = argv[4];
	int gridSize = (argc>5)?atoi(argv[5]):0;
	int blockSize = (argc>6)?atoi(argv[6]):0;

	printf("Title :\t%s\n", title);
	printf("Description :\t%s\n", description);
	printf("Filter :\t%s\n", filter);
	printf("Step :\t%d\n", step);
	printf("Input file name :\t%s\n", inputFileName);
	printf("Output file name :\t%s\n", outputFileName);

	/* Расчёт размеров полей в зависимости от переданного параметра */
	int N = step % 2 == 0 ? step += 1 : step;
	int Nh = N >> 1;

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

	host_medianfilter(gridSize, blockSize, channel[0], channel[1], channel[2], Nh, width, height);

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