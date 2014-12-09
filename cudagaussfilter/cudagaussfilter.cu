char *title = "gauss filtering";
char *description = "gauss filtering";

#include <iostream>
#include <cstdio>
#include <cstdlib>
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

#define assert( bool ) 

#define BUFFER_SIZE 4096


__global__ void global_gaussfilter_by_row(
	unsigned int *input,
	unsigned int *output,
	double *matrix,
	int Nh,
	int width,
	int height)
{
	for (int id = blockDim.x*blockIdx.x + threadIdx.x;
		id < width*height;
		id += blockDim.x*gridDim.x) {
		int x = id % width;

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
}
__global__ void global_gaussfilter_by_col(
	unsigned int *input,
	unsigned int *output,
	double *matrix,
	int Nh,
	int width,
	int height)
{
	for (int id = blockDim.x*blockIdx.x + threadIdx.x;
		id < width*height;
		id += blockDim.x*gridDim.x) {
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


__host__ void host_gaussfilter(int gridSize, int blockSize, unsigned int *r, unsigned int *g, unsigned int *b, double *matrix, int Nh, int width, int height)
{
	unsigned int *channel[3];
	channel[0] = r;
	channel[1] = g;
	channel[2] = b;

	// channel - массив данных
	// sigma - коэффициенты
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

		global_gaussfilter_by_row <<< blocks, threads >>>(device_a, device_b, device_matrix, Nh, width, height);
		global_gaussfilter_by_col <<< blocks, threads >>>(device_b, device_a, device_matrix, Nh, width, height);

		cudaMemcpy(channel[j], device_a, width*height*sizeof(unsigned int), cudaMemcpyDeviceToHost);
	}

	// Освобождаем память на устройстве
	cudaFree((void*)device_a);
	cudaFree((void*)device_b);
	cudaFree((void*)device_matrix);
	
	err = err;
}


int main(int argc, char* argv[])
{
	int j, k;
	unsigned char buffer[BUFFER_SIZE];

	std::cout << title << std::endl;

	// Find/set the device.
	int device_count = 0;
	cudaGetDeviceCount(&device_count);
	for (int i = 0; i < device_count; ++i)
	{
		cudaDeviceProp properties;
		cudaGetDeviceProperties(&properties, i);
		std::cout << "Running on GPU " << i << " (" << properties.name << ")" << std::endl;
	}

	if (argc < 9){
		printf("Usage :\t%s filter sigma width height inputfilename.bin ouputfilename.bin\n", argv[0]);
		exit(-1);
	}

	unsigned int* channel[3];

	// Получаем параметры - имена файлов

	char *filter = argv[1];
	double sigma = atof(argv[2]);
	int width = atoi(argv[3]);
	int height = atoi(argv[4]);
	char *inputFileName = argv[5];
	char *outputFileName = argv[6];
	int gridSize = atoi(argv[7]);
	int blockSize = atoi(argv[8]);

	// Nh - полуразмер
	int Nh = (int)(3 * sigma);
	
	double * matrix = (double*)malloc((2 * Nh + 1)*sizeof(double));
	for (int i = -Nh; i <= Nh; i++) matrix[Nh + i] = exp(-(double)i*i / (2.0*sigma*sigma));

	int count = width*height;

	printf("Title :\t%s\n", title);
	printf("Description :\t%s\n", description);
	printf("Filter :\t%s\n", filter);
	printf("Size :\t%d %d\n", width, height);
	printf("Input file name :\t%s\n", inputFileName);
	printf("Output file name :\t%s\n", outputFileName);

	/* выделение памяти под байтовые цветовые каналы */
	for (j = 0; j < 3; j++)
		channel[j] = (unsigned int*)malloc(width*height*sizeof(unsigned int));

	FILE *file = fopen(inputFileName, "rb");
	if (!file) {
		fprintf(stderr, "Open file error (%s)\n", inputFileName); fflush(stderr);
		exit(-1);
	}

	for (int pos = 0; pos < count; pos += (int)(BUFFER_SIZE / 3)) {
		int size = min((int)(BUFFER_SIZE / 3), (count - pos));
		fread(buffer, (size_t)3, (size_t)size, file);
		for (j = 0; j < 3; j++)
			for (k = 0; k < size; k++)
				channel[j][pos + k] = (unsigned int)buffer[3 * k + j];
	}

	fclose(file);

	host_gaussfilter(gridSize, blockSize, channel[0], channel[1], channel[2], matrix, Nh, width, height);

	/* выводим результаты */
	file = fopen(outputFileName, "wb");
	if (!file) {
		fprintf(stderr, "Open file error (%s)\n", outputFileName); fflush(stderr);
		exit(-1);
	}

	for (int pos = 0; pos < count; pos += (int)(BUFFER_SIZE / 3)) {
		int size = min((int)(BUFFER_SIZE / 3), (count - pos));
		for (j = 0; j < 3; j++)
			for (k = 0; k < size; k++)
				buffer[3 * k + j] = (unsigned char)channel[j][pos + k];
		fwrite(buffer, (size_t)3, (size_t)size, file);
	}

	fclose(file);

	// Высвобождаем массив
	for (j = 0; j < 3; j++)
		free(channel[j]);

	cudaDeviceReset();

	exit(0);
}