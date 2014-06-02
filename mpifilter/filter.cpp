#define _CRT_SECURE_NO_WARNINGS

char *title = "Median Filter";
char *description = "Медианный фильтр";

#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include <stdlib.h>
#include <limits.h>
#include <mpi.h> 

#ifndef max
#define max( a, b ) ( ((a) > (b)) ? (a) : (b) )
#endif

#ifndef min
#define min( a, b ) ( ((a) < (b)) ? (a) : (b) )
#endif

int compareBytes(const void * a, const void * b)
{
	if (*(unsigned char*)a < *(unsigned char*)b) return -1;
	if (*(unsigned char*)a > *(unsigned char*)b) return 1;
	return 0;
}

#define DATA_TAG 1

#define BUFFER_SIZE 4096

int main(int argc, char *argv[])
{
	int nrank;     /* Общее количество процессов */
	int myrank;    /* Номер текущего процесса */
	unsigned char* channel[2][3];
	int i, j, k;
	char buffer[BUFFER_SIZE];

	MPI_Status status;
	MPI_File inFile;
	MPI_File outFile;
	MPI_Offset pos;

	/* Иницилизация MPI */
	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &nrank);
	MPI_Comm_rank(MPI_COMM_WORLD, &myrank);

	if (myrank == 0 && argc < 6) {
		printf("Число процессов :\t%d\n", nrank);
		printf("Инструкция по применению :\t%s filter step width height inputfilename.bin ouputfilename.bin\n", argv[0]);
	}

	if (argc < 6) {
		MPI_Finalize();
		exit(-1);
	}

	char *filter = argv[1];
	int step = atoi(argv[2]);
	int width = atoi(argv[3]);
	int height = atoi(argv[4]);
	char *inputFileName = argv[5];
	char *outputFileName = argv[6];

	if (myrank == 0) {

		printf("Название :\t%s\n", title);
		printf("Описание :\t%s\n", description);
		printf("Фильтр :\t%s\n", filter);
		printf("Шаг :\t%d\n", step);
		printf("Размеры :\t%d %d\n", width, height);
		printf("Имя входного файла :\t%s\n", inputFileName);
		printf("Имя выходного файла :\t%s\n", outputFileName);
	}
	
	/* выделение памяти под байтовые цветовые каналы */
	for (i = 0; i < 2; i++)
		for (j = 0; j < 3; j++)
			channel[i][j] = (unsigned char*)malloc(width*height*sizeof(char));

	/* Расчёт размеров полей в зависимости от переданного параметра */
	int N = step % 2 == 0 ? step += 1 : step;
	int Nh = N / 2;

	/*
	 * Для исходного изображения мы просто разделяем обрабатываемые точки равномерно между процессами,
	 * Каждый процесс считывает из файла только достаточные для его работы данные, 
	 * обрабатывает точки, а потом сливаем их в выходной файл
	 * всего мы должны получить 	long total = (width - 2 * Nh)*(height - 2 * Nh);
	 */

	int x1, x2, x3, y1, y2, y3, n = N * N / 2;
	unsigned char* rgb[3];
	for (j = 0; j < 3; j++) rgb[j] = (unsigned char*)malloc(N * N*sizeof(char));

	long total = (width - 2 * Nh)*(height - 2 * Nh);

	long startId = myrank*total / nrank; /* id начальной обрабатываемой точки в массиве (width - 2 * Nh)*(height - 2 * Nh) */
	long endId = (myrank + 1)*total / nrank; /* id конечной обрабатываемой точки в массиве (width - 2 * Nh)*(height - 2 * Nh) */

	/*
	 * Определяем начальную и конечную строки исходного изображения, 
	 * и соответственно смещение и количество записей
	 * с-по которые надо считать из файла, чтобы иметь достаточные данные для обработки точек
	 * (с учётом необрабатываемых полей)
	 */
	int Y1 = (startId / (width - 2 * Nh));
	int X1 = 0;
	int Y2 = ((endId + width - 2 * Nh - 1) / (width - 2 * Nh)) + 2 * Nh;
	int X2 = 0;
	MPI_Offset offset = Y1*width + X1;
	MPI_Offset count = Y2*width + X2 - offset;

	/*
	 * Каждый процесс считывает только нужные ему данные из файла
	 */
	int rc = MPI_File_open(MPI_COMM_WORLD, inputFileName, MPI_MODE_RDONLY, MPI_INFO_NULL, &inFile);
	if (rc) {
		fprintf(stderr, "Ошибка открытия файла (%s)\n", inputFileName); fflush(stderr);
		MPI_Finalize();
		exit(-1);
	}

	MPI_File_seek(inFile, 3*offset, MPI_SEEK_SET);

	for (pos = offset; pos < offset + count; pos += BUFFER_SIZE/3){
		long size = min(BUFFER_SIZE / 3, offset + count - (int)pos);
		MPI_File_read(inFile, buffer, 3 * size, MPI_BYTE, &status);
		for (j = 0; j < 3; j++)
			for (k = 0; k < size; k++)
				channel[0][j][pos + k] = buffer[3 * k + j];
	}

	MPI_File_close(&inFile);

	/*
	 * Здесь собственно и содержится сам алгоритм медийной фильтрации
	 * Для каждой точки берутся N*N соседних, они сортируеются в одномерном массиве,
	 * затем берётся значение из середины массива
	 */
	int id;  for (id = startId; id<endId; id++)
	{
		y1 = (id / (width - 2 * Nh)) + Nh;
		x1 = (id % (width - 2 * Nh)) + Nh;
		i = 0;
		for (y2 = -Nh; y2 <= Nh; y2++)
		{
			y3 = y1 + y2;
			for (x2 = -Nh; x2 <= Nh; x2++)
			{
				x3 = x1 + x2;
				for (j = 0; j < 3; j++) rgb[j][i] = channel[0][j][y3*width + x3];
				i++;
			}
		}
		for (j = 0; j < 3; j++) qsort(rgb[j], N*N, sizeof(char), compareBytes);
		for (j = 0; j < 3; j++) channel[1][j][y1*width + x1] = rgb[j][n];
	}

	/*
	 * Сохраняем результаты
	 * Для этого рассчитываем с какой позиции в файле должен лежать обработанный блок и количество записей
	 * (с учётом не обрабатываемых полей)
	 */
	Y1 = (startId / (width - 2 * Nh)) + Nh;
	X1 = (startId % (width - 2 * Nh)) + Nh;
	Y2 = (endId / (width - 2 * Nh)) + Nh;
	X2 = (endId % (width - 2 * Nh)) + Nh;
	offset = Y1*width + X1;
	count = Y2*width + X2 - offset;

	/*
	 * Каждый процесс записыват только свои данные в выходной файл
	 */
	rc = MPI_File_open(MPI_COMM_WORLD, outputFileName, MPI_MODE_WRONLY | MPI_MODE_CREATE, MPI_INFO_NULL, &outFile);
	if (rc) {
		fprintf(stderr, "Ошибка открытия файла (%s)\n", outputFileName); fflush(stderr);
		MPI_Finalize();
		exit(-1);
	}

	MPI_File_set_size(outFile, 3 * width*height);
	MPI_File_seek(outFile, 3*offset, MPI_SEEK_SET);

	for (pos = offset; pos < offset + count; pos += BUFFER_SIZE/3){
		long size = min(BUFFER_SIZE / 3, offset + count - pos);
		for (j = 0; j < 3; j++)
			for (k = 0; k < size; k++)
				buffer[3 * k + j] = channel[1][j][pos + k];
		MPI_File_write(outFile, buffer, 3 * size, MPI_BYTE, &status);
	}

	MPI_File_close(&outFile);

	MPI_Finalize();
	exit(0);
}
