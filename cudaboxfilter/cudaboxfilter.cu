char *title = "box filtering";
char *description = "box filtering";
/*
Box-фильтр представляет собой так называемую четырехточечную свертку. 
В этом фильтре новое значение пикселя вычисляется, как среднее значение 
его четырех соседних пикселей: верхнего, нижнего, левого и правого.
Вернее рассматривается матрица 3х3 и вычисляется взвешенная сумма соседних элементов.
*/

#include <iostream>
#include <cstdio>
#include <cstdlib>
#include <time.h>
#include <cuda.h>
#include <cuda_runtime.h>

#ifndef max
#define max( a, b ) ( ((a) > (b)) ? (a) : (b) )
#endif

#ifndef min
#define min( a, b ) ( ((a) < (b)) ? (a) : (b) )
#endif

#define assert( bool ) 

#define BUFFER_SIZE 4096


__global__ void global_boxfilter(
	unsigned int *input,
	unsigned int *output,
	int *matrix,
	int width, 
	int height)
{
	for (int id = blockDim.x*blockIdx.x + threadIdx.x;
		id < width*height;
		id += blockDim.x*gridDim.x) {
		int x = id % width;
		int y = id / width;
	
		int s1 = 0;
		int s2 = 0;
		for (int dx = -1; dx <= 1; dx++) {
			for (int dy = -1; dy <= 1; dy++) {
				if (x + dx < 0) continue;
				if (x + dx >= width) continue;
				if (y + dy < 0) continue;
				if (y + dy >= height) continue;
				int value = (int)(unsigned int)input[id + dx + width*dy];
				s1 += value*matrix[(1 + dx) + 3 * (1 + dy)];
				s2 += matrix[(1 + dx) + 3 * (1 + dy)];
			}
		}
		output[id] = (unsigned int)(s1 / s2);
	}
}


__host__ void host_boxfilter(int gridSize, int blockSize, unsigned int *r, unsigned int *g, unsigned int *b, int *matrix, int width, int height)
{
	unsigned int *channel[3];
	channel[0] = r;
	channel[1] = g;
	channel[2] = b;

	// channel - массив данных
	// matrix - коэффициенты
	// width - ширина
	// height - высота

	cudaError_t err;
	unsigned int *device_input;
	unsigned int *device_output;
	int *device_matrix;

	err = cudaMalloc((void**)&device_input, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_output, width*height*sizeof(unsigned int));
	err = cudaMalloc((void**)&device_matrix, 9 * sizeof(int));
	cudaMemcpy(device_matrix, matrix, 9*sizeof(int), cudaMemcpyHostToDevice);

	int blocks = (gridSize > 0)? gridSize : min(15, (int)sqrt(width*height));
	int threads = (blockSize > 0)? blockSize : min(15, (int)sqrt(width*height));

	for(int j=0; j<3; j++) {
		cudaMemcpy(device_input, channel[j], width*height*sizeof(unsigned int), cudaMemcpyHostToDevice);

		global_boxfilter <<< blocks, threads >>>(device_input, device_output, device_matrix, width, height);

		cudaMemcpy(channel[j], device_output, width*height*sizeof(unsigned int), cudaMemcpyDeviceToHost);
	}

	// Освобождаем память на устройстве
	cudaFree((void*)device_input);
	cudaFree((void*)device_output);
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

	if (argc < 17){
		printf("Usage :\t%s filter matrix[0][0] ... matrix[2][2] width height inputfilename.bin ouputfilename.bin\n", argv[0]);
		exit(-1);
	}

	int matrix[9];
	unsigned int* channel[3];
	// Получаем параметры - имена файлов

	char *filter = argv[1];
	matrix[0] = atoi(argv[2 + 3 * 0 + 0]);
	matrix[1] = atoi(argv[2 + 3 * 0 + 1]);
	matrix[2] = atoi(argv[2 + 3 * 0 + 2]);
	matrix[3] = atoi(argv[2 + 3 * 1 + 0]);
	matrix[4] = atoi(argv[2 + 3 * 1 + 1]);
	matrix[5] = atoi(argv[2 + 3 * 1 + 2]);
	matrix[6] = atoi(argv[2 + 3 * 2 + 0]);
	matrix[7] = atoi(argv[2 + 3 * 2 + 1]);
	matrix[8] = atoi(argv[2 + 3 * 2 + 2]);
	int width = atoi(argv[11]);
	int height = atoi(argv[12]);
	char *inputFileName = argv[13];
	char *outputFileName = argv[14];
	int gridSize = atoi(argv[15]);
	int blockSize = atoi(argv[16]);

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

	host_boxfilter(gridSize, blockSize, channel[0], channel[1], channel[2], matrix, width, height);

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