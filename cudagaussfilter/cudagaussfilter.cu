char *title = "gauss filtering";
char *description = "gauss filtering";

#include <iostream>
#include <cstdio>
#include <cstdlib>
#include <assert.h>
#include <time.h>
#include <cuda.h>
#include <cuda_runtime.h>
#include <math.h>       /* exp */

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

__global__ void global_defaultgaussfilter(
	unsigned int *input,
	unsigned int *output,
	double *matrix,	int Nh,
	int width,int height,
	int ddxx, int ddyy)
{
	assert(ddxx*ddyy==0);

	if(ddxx>0) for (int id = blockDim.x*blockIdx.x + threadIdx.x;
	id < width*height;
	id += blockDim.x*gridDim.x) {
		int x = id % width;
		int y = id / width;

		double s1 = 0;
		double s2 = 0;
		for (int dx = -Nh; dx <= Nh; dx++) {
			if (x + dx < 0) continue;
			if (x + dx >= width) continue;
			int value = (double)(unsigned int)input[id + dx];
			s1 += value*matrix[Nh + dx];
			s2 += matrix[Nh + dx];
		}
		output[id] = (unsigned int)(s1 / s2);
	}
	if(ddyy>0) for (int id = blockDim.x*blockIdx.x + threadIdx.x;
	id < width*height;
	id += blockDim.x*gridDim.x) {
		int x = id % width;
		int y = id / width;

		double s1 = 0;
		double s2 = 0;
		for (int dy = -Nh; dy <= Nh; dy++) {
			if (y + dy < 0) continue;
			if (y + dy >= height) continue;
			int value = (double)(unsigned int)input[id + width*dy];
			s1 += value*matrix[Nh + dy];
			s2 += matrix[Nh + dy];
		}
		output[id] = (unsigned int)(s1 / s2);
	}
}


__host__ void host_defaultgaussfilter(
	int gridSize, int blockSize, 
	unsigned int *r, unsigned int *g, unsigned int *b, 
	double *matrix, int Nh, 
	int width, int height)
{
	unsigned int *channel[3] = { r,g,b };

	// channel - массив данных
	// sigma - коэффициент размытия
	// width - ширина
	// height - высота

	cudaError_t err;
	unsigned int *device_a;
	unsigned int *device_b;
	double *device_matrix;

	err = cudaMalloc((void**)&device_a, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_b, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_matrix, (2 * Nh + 1) * sizeof(double));
	cudaMemcpy(device_matrix, matrix, (2 * Nh + 1)*sizeof(double), cudaMemcpyHostToDevice);

	int blocks = (gridSize > 0)? gridSize : min(15, (int)sqrt(width*height));
	int threads = (blockSize > 0)? blockSize : min(15, (int)sqrt(width*height));

	for(int j=0; j<3; j++) {
		cudaMemcpy(device_a, channel[j], width*height*sizeof(unsigned int), cudaMemcpyHostToDevice);

		global_defaultgaussfilter <<< blocks, threads >>>(device_a, device_b, device_matrix, Nh, width, height, 0, 1);
		global_defaultgaussfilter <<< blocks, threads >>>(device_b, device_a, device_matrix, Nh, width, height, 1, 0);

		cudaMemcpy(channel[j], device_a, width*height*sizeof(unsigned int), cudaMemcpyDeviceToHost);
	}

	// Освобождаем память на устройстве
	cudaFree((void*)device_a);
	cudaFree((void*)device_b);
	cudaFree((void*)device_matrix);

	err = err;
}

__global__ void global_recursivegaussfilter(
	unsigned int *input,
	unsigned int *output,
	double *bb,	int p, int q,
	int width,int height,
	int ddxx, int ddyy)
{
	assert(p==1);
	assert(q==3);
	assert(ddxx*ddyy==0);

	// Копируем в локальные переменные для ускорения доступа к значениям
	double b1 = bb[1]/bb[0];
	double b2 = bb[2]/bb[0];
	double b3 = bb[3]/bb[0];
	double B = 1.0 - (b1+b2+b3);

	if(ddxx>0) for (int y = blockDim.x*blockIdx.x + threadIdx.x;
	y < height;
	y += blockDim.x*gridDim.x) {
		int id = y*width;
		if(width>1) output[id]=B*input[id];id++;
		if(width>2) output[id]=B*input[id]+b1*output[id-1];id++;
		if(width>3) output[id]=B*input[id]+b1*output[id-1]+b2*output[id-2];id++;
		for(; id<width; id++) 
			output[id]=B*input[id]+b1*output[id-1]+b2*output[id-2]+b3*output[id-3];
	}
	if(ddxx<0) for (int y = blockDim.x*blockIdx.x + threadIdx.x;
	y < height;
	y += blockDim.x*gridDim.x) {
		int id = y*width+width;
		id--;if(width>1) output[id]=B*input[id];
		id--;if(width>2) output[id]=B*input[id]+b1*output[id+1];
		id--;if(width>3) output[id]=B*input[id]+b1*output[id+1]+b2*output[id+2];
		for(id--; id>=0; id--) 
			output[id]=B*input[id]+b1*output[id+1]+b2*output[id+2]+b3*output[id+3];
	}
	if(ddyy>0) for (int x = blockDim.x*blockIdx.x + threadIdx.x;
	x < width;
	x += blockDim.x*gridDim.x) {
		int id = x;
		if(height>1) output[id]=B*input[id];id+=width;
		if(height>2) output[id]=B*input[id]+b1*output[id-width];id+=width;
		if(height>3) output[id]=B*input[id]+b1*output[id-width]+b2*output[id-2*width];id+=width;
		for(int y=3; y<height; y++, id+=width) 
			output[id]=B*input[id]+b1*output[id-width]+b2*output[id-2*width]+b3*output[id-3*width];
	}
	if(ddyy<0) for (int x = blockDim.x*blockIdx.x + threadIdx.x;
	x < width;
	x += blockDim.x*gridDim.x){
		int id = height*width+x;
		id-=width;if(height>1) output[id]=B*input[id];
		id-=width;if(height>2) output[id]=B*input[id]+b1*output[id+width];
		id-=width;if(height>3) output[id]=B*input[id]+b1*output[id+width]+b2*output[id+2*width];
		for(id-=width; id>=0; id-=width) 
			output[id]=B*input[id]+b1*output[id+width]+b2*output[id+2*width]+b3*output[id+3*width];
	}
}

__host__ void host_recursivegaussfilter(
	int gridSize, int blockSize, 
	unsigned int *r, unsigned int *g, unsigned int *b, 
	double *bb, int p, int q, 
	int width, int height)
{

	unsigned int *channel[3] = { r,g,b };

	// channel - массив данных
	// sigma - коэффициенты
	// width - ширина
	// height - высота

	cudaError_t err;
	unsigned int *device_a;
	unsigned int *device_b;
	double *device_bb;

	err = cudaMalloc((void**)&device_a, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_b, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_bb, (p+q) * sizeof(double));
	cudaMemcpy(device_bb, bb, (p+q)*sizeof(double), cudaMemcpyHostToDevice);

	int blocks = (gridSize > 0)? gridSize : min(15, (int)sqrt(max(width,height)));
	int threads = (blockSize > 0)? blockSize : min(15, (int)sqrt(max(width,height)));

	for(int j=0; j<3; j++) {
		cudaMemcpy(device_a, channel[j], width*height*sizeof(unsigned int), cudaMemcpyHostToDevice);

		global_recursivegaussfilter <<< blocks, threads >>>(device_a, device_b, device_bb, p, q, width, height, 0, 1);
		global_recursivegaussfilter <<< blocks, threads >>>(device_b, device_a, device_bb, p, q, width, height, 0, -1);
		global_recursivegaussfilter <<< blocks, threads >>>(device_a, device_b, device_bb, p, q, width, height, 1, 0);
		global_recursivegaussfilter <<< blocks, threads >>>(device_b, device_a, device_bb, p, q, width, height, -1, 0);

		cudaMemcpy(channel[j], device_a, width*height*sizeof(unsigned int), cudaMemcpyDeviceToHost);
	}

	// Освобождаем память на устройстве
	cudaFree((void*)device_a);
	cudaFree((void*)device_b);
	cudaFree((void*)device_bb);

	err = err;
}

#define RECURSIVE 1
#define DEFAULT 0

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
		printf("Usage :\t%s filter sigma inputfilename.bmp ouputfilename.bmp\n", argv[0]);
		printf("filter :\tgauss recursivegauss\n");
		exit(-1);
	}

	// Получаем параметры - имена файлов

	char *filter = argv[1];
	double sigma = atof(argv[2]);
	char *inputFileName = argv[3];
	char *outputFileName = argv[4];
	int gridSize = (argc>5)?atoi(argv[5]):0;
	int blockSize = (argc>6)?atoi(argv[6]):0;
	int mode =(strcmp(filter,"gauss")==0)?DEFAULT:RECURSIVE;

	printf("Title :\t%s\n", title);
	printf("Description :\t%s\n", description);
	printf("Filter :\t%s\n", filter);
	printf("Sigma :\t%le\n", sigma);
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

	switch(mode){
	case RECURSIVE:{
		int p = 1, q = 3;
		double * bb = (double*)malloc((p+q)*sizeof(double));

		//// http://mognovse.ru/qgi-chekan-rostislav-vladimirovich.html
		//// cтандартные рекомендации по выбору константы q представляют собой:
		//double qq;
		//if(sigma>2.5) qq=0.98711*sigma - 0.96330;
		//else if(sigma>0.5) qq=3.97156 - 4.14554*sqrt(1.0 - 0.26891*sigma);
		//else qq=3.97156 - 4.14554*sqrt(1.0 - 0.26891*sigma);

		//double qq2 = qq*qq;
		//double qq3 = qq2*qq;

		//bb[0] = 1.5725 + 2.44413*qq + 1.4281*qq2;
		//bb[1] = 2.44413*qq + 2.85619*qq2 + 1.26661*qq3;
		//bb[2] = -1.4281*qq2 - 1.26661*qq3;
		//bb[3] = 0.422205*qq3;

		// http://habrahabr.ru/post/151157/
		double sigma_inv_4;

		sigma_inv_4 = sigma*sigma; sigma_inv_4 = 1.0/(sigma_inv_4*sigma_inv_4);

		double coef_A = sigma_inv_4*(sigma*(sigma*(sigma*1.1442707+0.0130625)-0.7500910)+0.2546730);
		double coef_W = sigma_inv_4*(sigma*(sigma*(sigma*1.3642870+0.0088755)-0.3255340)+0.3016210);
		double coef_B = sigma_inv_4*(sigma*(sigma*(sigma*1.2397166-0.0001644)-0.6363580)-0.0536068);

		double z0_abs   = exp(coef_A);

		double z0_real  = z0_abs*cos(coef_W);
		double z0_im    = z0_abs*sin(coef_W);
		double z2       = exp(coef_B);

		double z0_abs_2 = z0_abs*z0_abs;

		bb[3] = 1.0/(z2*z0_abs_2);
		bb[2] = -(2*z0_real+z2)*bb[3];
		bb[1] = (z0_abs_2+2*z0_real*z2)*bb[3];
		bb[0] = 1.0;

		clock_t t1, t2;
		t1 = clock();

		host_recursivegaussfilter(gridSize, blockSize, 
			channel[0], channel[1], channel[2], 
			bb, p, q,
			width, height);

		t2 = clock();
		double seconds = ((double)(t2-t1))/CLOCKS_PER_SEC;
		printf("Execution time :\t%le\n", seconds);

		free(bb);
				   }
				   break;
	default:{
		// Nh - полуразмер
		int Nh = (int)(3 * sigma);

		double * matrix = (double*)malloc((2 * Nh + 1)*sizeof(double));
		for (int i = -Nh; i <= Nh; i++) matrix[Nh + i] = exp(-(double)i*i / (2.0*sigma*sigma));

		clock_t t1, t2;
		t1 = clock();

		host_defaultgaussfilter(gridSize, blockSize, 
			channel[0], channel[1], channel[2], 
			matrix, Nh, 
			width, height);

		t2 = clock();
		double seconds = ((double)(t2-t1))/CLOCKS_PER_SEC;
		printf("Execution time :\t%le\n", seconds);

		free(matrix);
			}
			break;
	}

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