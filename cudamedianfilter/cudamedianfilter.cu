char *title = "Median Filter";
char *description = "Median Filter";

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

__global__ void global_medianfilter(
	unsigned int *input,
	unsigned int *output,
	unsigned int *buffer,
	int Nh,
	int width, 
	int height)
{
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
				buffer[(2 * Nh + 1) *(2 * Nh + 1)*(blockDim.x*blockIdx.x + threadIdx.x) + k++] = input[id + dx + width*dy];
			}
		}
		for (int i = (2 * Nh + 1) *(2 * Nh + 1)*(blockDim.x*blockIdx.x + threadIdx.x); i < (2 * Nh + 1) *(2 * Nh + 1)*(blockDim.x*blockIdx.x + threadIdx.x) + k - 1; i++) {
			for (int j = i + 1; j < (2 * Nh + 1) *(2 * Nh + 1)*(blockDim.x*blockIdx.x + threadIdx.x) + k; j++) {
				if (buffer[i] > buffer[j]) {
					unsigned int tmp = buffer[i];
					buffer[i] = buffer[j];
					buffer[j] = tmp;
				}
			}
		}
		output[id] = buffer[(2 * Nh + 1) *(2 * Nh + 1)*(blockDim.x*blockIdx.x + threadIdx.x) + (k >> 1)];
	}
}

__host__ void host_medianfilter(int gridSize, int blockSize, unsigned int *r, unsigned int *g, unsigned int *b, int Nh, int width, int height)
{
	unsigned int *channel[3];
	channel[0] = r;
	channel[1] = g;
	channel[2] = b;

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
		printf("Usage :\t%s filter Nh width height inputfilename.bin ouputfilename.bin\n", argv[0]);
		exit(-1);
	}

	unsigned int* channel[3];
	// Получаем параметры - имена файлов

	char *filter = argv[1];
	int step = atoi(argv[2]);
	int width = atoi(argv[3]);
	int height = atoi(argv[4]);
	char *inputFileName = argv[5];
	char *outputFileName = argv[6];
	int gridSize = atoi(argv[7]);
	int blockSize = atoi(argv[8]);

	printf("Title :\t%s\n", title);
	printf("Description :\t%s\n", description);
	printf("Filter :\t%s\n", filter);
	printf("Step :\t%d\n", step);
	printf("Size :\t%d %d\n", width, height);
	printf("Input file name :\t%s\n", inputFileName);
	printf("Output file name :\t%s\n", outputFileName);

	int count = width*height;

	/* Расчёт размеров полей в зависимости от переданного параметра */
	int N = step % 2 == 0 ? step += 1 : step;
	int Nh = N >> 1;

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

	host_medianfilter(gridSize, blockSize, channel[0], channel[1], channel[2], Nh, width, height);

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