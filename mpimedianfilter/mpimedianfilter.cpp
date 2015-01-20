#define _CRT_SECURE_NO_WARNINGS

char *title = "Median Filter";
char *description = "Median Filter";

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

int compareColors(const void * a, const void * b)
{
	if (*(unsigned int*)a < *(unsigned int*)b) return -1;
	if (*(unsigned int*)a > *(unsigned int*)b) return 1;
	return 0;
}

#define DATA_TAG 1

#define BUFFER_SIZE 4096

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

#define  IDX(x,y,width) ((y)*(width)+(x))
#define  OFFSET(x,y,bytes_per_pixel,bytes_per_line) ((x)*(bytes_per_pixel)+(y)*(bytes_per_line))

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

int main(int argc, char *argv[])
{
	int nrank;     /* Общее количество процессов */
	int myrank;    /* Номер текущего процесса */
	unsigned int* channel[2][4];
	unsigned char* buffer;
	int i, j, k;

	MPI_Request request;
	MPI_Status status;
	MPI_File inFile;
	MPI_File outFile;
	MPI_Offset pos;


	/* Иницилизация MPI */
	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &nrank);
	MPI_Comm_rank(MPI_COMM_WORLD, &myrank);

	if (myrank == 0 && argc < 5) {
		printf("Usage :\t%s filter step inputfilename.bmp ouputfilename.bmp\n", argv[0]);
	}

	if (argc < 5) {
		MPI_Finalize();
		exit(-1);
	}

	char *filter = argv[1];
	int step = atoi(argv[2]);
	char *inputFileName = argv[3];
	char *outputFileName = argv[4];

	if (myrank == 0) {
		fprintf(stdout,"Title :\t%s\n", title);
		fprintf(stdout,"Description :\t%s\n", description);
		fprintf(stdout,"Number of process :\t%d\n", nrank);
		fprintf(stdout,"Filter :\t%s\n", filter);
		fprintf(stdout,"Step :\t%d\n", step);
		fprintf(stdout,"Input file name :\t%s\n", inputFileName);
		fprintf(stdout,"Output file name :\t%s\n", outputFileName);
		fflush(stdout);
	}

	buffer = (unsigned char*)malloc(BUFFER_SIZE);

	/*
	* Каждый процесс считывает только нужные ему данные из файла
	*/

	//fprintf(stdout,"process %d open read %s\n", myrank, inputFileName);
	//fflush(stdout);
	
	int rc = MPI_File_open(MPI_COMM_WORLD, inputFileName, MPI_MODE_RDONLY, MPI_INFO_NULL, &inFile);
	if (rc) {
		fprintf(stderr, "Open file error (%s)\n", inputFileName); fflush(stderr);
		MPI_Finalize();
		exit(-1);
	}

	MPI_File_read(inFile, buffer, sizeof(bmp_header_t)+sizeof(bmp_dib_v3_header_t), MPI_BYTE, &status);

	int width = *(uint32_t *)&buffer[BMPWIDTH];
	int height = *(uint32_t *)&buffer[BMPHEIGHT];
	int file_size = *(uint32_t *)&buffer[BMPFILESIZE];
	int offset = *(uint32_t *)&buffer[BMPOFFSET];
	int bits_per_pixel = *(uint16_t *)&buffer[BMPBITSPERPIXEL];
	int bytes_per_pixel = ((int)((bits_per_pixel+7)/8));
	int bytes_per_line = ((int)((bits_per_pixel * width+31)/32))*4; // http://en.wikipedia.org/wiki/BMP_file_format

	if (myrank == 0) {
		fprintf(stdout,"BMP image size :\t%ld x %ld\n", width, height);
		fprintf(stdout,"BMP file size :\t%ld\n", file_size);
		fprintf(stdout,"BMP pixels offset :\t%ld\n", offset);
		fprintf(stdout,"BMP bits per pixel :\t%ld\n", bits_per_pixel);
		fprintf(stdout,"BMP bytes per pixel :\t%ld\n", bytes_per_pixel);
		fprintf(stdout,"BMP bytes per line :\t%ld\n", bytes_per_line);
		fflush(stdout);
	}

	/* Расчёт размеров полей в зависимости от переданного параметра */
	int N = step % 2 == 0 ? step += 1 : step;
	int Nh = N >> 1;

	/*
	* Для исходного изображения мы просто разделяем обрабатываемые точки равномерно между процессами,
	* Каждый процесс считывает из файла только достаточные для его работы данные, 
	* обрабатывает точки, а потом сливаем их в выходной файл
	* всего мы должны получить 	int total = (width - 2 * Nh)*(height - 2 * Nh);
	*/

	int x, y;
	int x1, y1;
	unsigned int* rgb[3];
	for (j = 0; j < min(3,bytes_per_pixel); j++) rgb[j] = (unsigned int*)malloc(N * N*sizeof(int));

	int total = width*height;
	int startId = myrank*total / nrank; /* id начальной обрабатываемой точки в массиве */
	int endId = (myrank + 1)*total / nrank; /* id конечной обрабатываемой точки в массиве */

	//fprintf(stdout,"process %d [%ld-%ld)\n", myrank, startId, endId);
	//fflush(stdout);

	/*
	* Определяем начальную и конечную строки исходного изображения, 
	* и соответственно смещение и количество записей
	* с-по которые надо считать из файла, чтобы иметь достаточные данные для обработки точек
	* (с учётом необрабатываемых полей)
	*/
	int Y1 = startId / width;
	int Y2 = endId / width;
	int X1 = startId % width;
	int X2 = endId % width;
	if(X2==0) { Y2--; X2=width; };
	Y1 = max(0, Y1-Nh);
	X1 = max(0, X1-Nh);
	Y2 = min(height-1, Y2+Nh);
	X2 = min(width, X2+Nh);

	int lowId = IDX(X1,Y1,width);
	int highId = IDX(X2,Y2,width);

	/* выделение памяти под байтовые цветовые каналы */
	for (i = 0; i < 2; i++)
		for (j = 0; j < bytes_per_pixel; j++)
			channel[i][j] = (unsigned int*)malloc((highId-lowId)*sizeof(unsigned int)+1);

	//fprintf(stdout,"process %d read (%ld:%ld)-(%ld:%ld)\n", myrank, X1, Y1, X2, Y2);
	//fflush(stdout);

	for (y = Y1; y <= Y2; y++){
		MPI_File_seek(inFile, offset + OFFSET(((y==Y1)?X1:0),y,bytes_per_pixel,bytes_per_line), MPI_SEEK_SET);
		for(x=((y==Y1)?X1:0); x<((y==Y2)?X2:width); x+=(int)(BUFFER_SIZE / bytes_per_pixel)) {
			int size = min(BUFFER_SIZE / bytes_per_pixel, ((y==Y2)?X2:width) - x);
			MPI_File_read(inFile, buffer, bytes_per_pixel * size, MPI_BYTE, &status);
			i = 0;
			for (k = 0; k < size; k++) {
				for (j = 0; j < bytes_per_pixel; j++){
					channel[0][j][IDX(x+k,y,width)-lowId] = (unsigned int)buffer[i++];
				}
			}
		}
	}

	/*
	* Здесь собственно и содержится сам алгоритм медианной фильтрации
	* Для каждой точки берутся N*N соседних, они сортируеются в одномерном массиве,
	* затем берётся значение из середины массива
	*/

	//fprintf(stdout,"process %d run [%ld-%ld)\n", myrank, startId, endId);
	//fflush(stdout);

	int id;  for (id = startId; id<endId; id++)
	{
		y = id / width;
		x = id % width;
		k = 0;
		for (y1 = y-Nh; y1 <= y+Nh; y1++)
		{
			if(y1<0) continue;
			if(y1>=height) continue;
			for (x1 = x-Nh; x1 <= x+Nh; x1++)
			{
				if(x1<0) continue;
				if(x1>=width) continue;
				for (j = 0; j < min(3,bytes_per_pixel); j++) rgb[j][k] = channel[0][j][IDX(x1,y1,width)-lowId];
				k++;
			}
		}
		for (j = 0; j < min(3,bytes_per_pixel); j++) qsort(rgb[j], k, sizeof(int), compareColors);
		for (j = 0; j < min(3,bytes_per_pixel); j++) channel[1][j][IDX(x,y,width)-lowId] = rgb[j][k>>1];
	}

	for (j = min(3,bytes_per_pixel); j<bytes_per_pixel; j++) {
		unsigned int *tmp = channel[0][j];
		channel[0][j] = channel[1][j];
		channel[1][j] = tmp;
	}

	/*
	* Каждый процесс записыват только свои данные в выходной файл
	*/

	//fprintf(stdout,"process %d open write %s\n", myrank, outputFileName);
	//fflush(stdout);
	
	rc = MPI_File_open(MPI_COMM_WORLD, outputFileName, MPI_MODE_WRONLY | MPI_MODE_CREATE, MPI_INFO_NULL, &outFile);
	if (rc) {
		fprintf(stderr, "Open file error (%s)\n", outputFileName); fflush(stderr);
		MPI_Finalize();
		exit(-1);
	}
	MPI_File_set_size(outFile, file_size);

	/*
	* Копируем исходный файл
	* Исходный файл надо копировать поскольку он содержит дополнительную информацию не обрабатываему данной программой
	* Копируем всё до и после пиксельных данных
	*/

	//fprintf(stdout,"process %d copy %s to %s\n", myrank, inputFileName, outputFileName);
	//fflush(stdout);

	int writeCount = 0;
	int startPos = myrank*offset / nrank;
	int endPos = (myrank + 1)*offset / nrank;

	MPI_File_seek(inFile, startPos, MPI_SEEK_SET);
	MPI_File_seek(outFile, startPos, MPI_SEEK_SET);

	for (pos = startPos; pos < endPos; pos += BUFFER_SIZE){
		int size = min(BUFFER_SIZE, endPos - pos);
		MPI_File_read(inFile, buffer, size, MPI_BYTE, &status);
		if(writeCount>0) { MPI_Wait(&request, &status); writeCount--; }
		MPI_File_iwrite(outFile, buffer, size, MPI_BYTE, &request); writeCount++;
	}

	startPos = (offset+bytes_per_line*height)+myrank*(file_size-offset-bytes_per_line*height) / nrank;
	endPos = (offset+bytes_per_line*height)+(myrank + 1)*(file_size-offset-bytes_per_line*height) / nrank;

	MPI_File_seek(inFile, startPos, MPI_SEEK_SET);
	MPI_File_seek(outFile, startPos, MPI_SEEK_SET);

	for (pos = startPos; pos < endPos; pos += BUFFER_SIZE){
		int size = min(BUFFER_SIZE, endPos - pos);
		MPI_File_read(inFile, buffer, size, MPI_BYTE, &status);
		if(writeCount>0) { MPI_Wait(&request, &status); writeCount--; }
		MPI_File_iwrite(outFile, buffer, size, MPI_BYTE, &request); writeCount++;
	}
	if(writeCount>0) { MPI_Wait(&request, &status); writeCount--; }

	if(nrank>1) MPI_Barrier(MPI_COMM_WORLD);
	MPI_File_close(&inFile);

	/*
	* Сохраняем результаты
	* Для этого рассчитываем с какой позиции в файле должен лежать обработанный блок и количество записей
	* (с учётом не обрабатываемых полей)
	*/

	Y1 = startId / width;
	Y2 = endId / width;
	X1 = startId % width;
	X2 = endId % width;
	if(X2==0) { Y2--; X2=width; };

	//fprintf(stdout,"process %d write (%ld:%ld)-(%ld:%ld)\n", myrank, X1, Y1, X2, Y2);
	//fflush(stdout);

	for (y = Y1; y <= Y2; y++){
		MPI_File_seek(outFile, offset + OFFSET(((y==Y1)?X1:0),y,bytes_per_pixel,bytes_per_line), MPI_SEEK_SET);
		for(x=((y==Y1)?X1:0); x<((y==Y2)?X2:width); x+=(int)(BUFFER_SIZE / bytes_per_pixel)) {
			int size = min(BUFFER_SIZE / bytes_per_pixel, ((y==Y2)?X2:width) - x);
			i = 0;
			for (k = 0; k < size; k++) {
				for (j = 0; j < bytes_per_pixel; j++){
					buffer[i++] = (unsigned char)channel[1][j][IDX(x+k,y,width)-lowId];
				}
			}
			if(writeCount>0) { MPI_Wait(&request, &status); writeCount--; }
			MPI_File_iwrite(outFile, buffer, bytes_per_pixel * size, MPI_BYTE, &request); writeCount++;
		}
	}
	if(writeCount>0) { MPI_Wait(&request, &status); writeCount--; }

	MPI_File_close(&outFile);

	/* освобождение памяти под байтовые цветовые каналы */
	for (i = 0; i < 2; i++)
		for (j = 0; j < bytes_per_pixel; j++)
			free(channel[i][j]);
	for (j = 0; j < min(3,bytes_per_pixel); j++)
		free(rgb[j]);
	free(buffer);

	MPI_Finalize();
	exit(0);
}
