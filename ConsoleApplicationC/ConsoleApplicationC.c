// ConsoleApplicationC.cpp : Questo file contiene la funzione 'main', in cui inizia e termina l'esecuzione del programma.
//

#include "inttypes.h"
#include "init.h"
#include "string.h"

#include "circularBuffer.h"

typedef void (*dataReadyFptr_t)(int, int);

void callback(int a, int b)
{

}


#define BUFFER_SIZE (uint16_t)10
int main()
{
	uint8_t buffer[BUFFER_SIZE];
	memset(buffer, 0, BUFFER_SIZE);

	circularBuffer_t circularBuffer =
	{
		/*uint16_t bufferSize*/BUFFER_SIZE,
		/*uint8_t* byteArray, &buffer[0] ==== buffer */buffer,
		/*uint16_t readIndex,*/0,
		/*uint16_t writeIndex,*/0,
		/*uint8_t overflow,*/0
	};

	int i = 0;
	/* Ne metto 7 */
	for (i = 0; i < 7; i++)
	{
		addChar(&circularBuffer, (uint8_t)(i+1));
	}

	uint8_t val = 0;
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val);
	popChar(&circularBuffer, &val); /* Qui ritorna empty perchè l'ho svuotato tutto */

	dataReadyFptr_t gfd = callback;

	uint8_t byteVar = 0; /* 0,255 */
	init();

	init();
	/* Per gli unsigned int vado da 0 a (2^N -1) */
	uint16_t ushortVar = 0; /* 0, 65536 */
	uint32_t wordVar = 0; /* 0, 4.294.967.296 */
	uint64_t longVar; /* 0, numero 2^64 */

	byteVar = 0xFF;
	ushortVar = 0x0000;
	wordVar = 0xFFFFFFFF;

	/* Per gli unsigned int vado da -(2^(N-1) -1) a (2^(N-1) -1) */
	int8_t signedByteVar = 0; /* -127,+127 */
	int16_t signedshortVar = 0; /* 0, 65536 */
	int32_t signedWordVar = 0; /* 0, 4.294.967.296 */
	int64_t signedlongVar = 0; /* 0, numero 2^64 */

	float floaVar = 0.0; /* 4 byte */
	double doubleVar = 0.0; /* 8 byte */

	char m = '0';


	/* Array */
	uint8_t arrayUINT8[] = { 0, 1, 2, 3 };
	uint8_t arrayUINT8_2[100];

	arrayUINT8[2] = 0;
	arrayUINT8_2[3] = 1;
	//memset(arrayUINT8_2, 0, sizeof(arrayUINT8_2));
	//memcpy(arrayUINT8_2, arrayUINT8, sizeof(arrayUINT8));

	/* Struct */
	packsInts_t packsInts;
	packsInts.s = 9;

	/* Puntatori */
	uint8_t variable = 0;
	variable = variable + 1;
	variable = variable + 2;

	uint8_t* variablePtr = &variable;
	*variablePtr = 9; /* Deferenziare */

	variablePtr = variablePtr + 1; /* Incremento del puntatore */

	*arrayUINT8 = 10; /* arrayUINT8[0] = 10 */
	*(arrayUINT8 + 1) = 2; /* arrayUINT8[1] = 2 */

	variablePtr = arrayUINT8; /* Ora variablePtr punta a arrayUINT8[0] */

	packsInts_t* packsIntsPtr = &packsInts;
	packsIntsPtr->s = 0;

	gfd(9, 9);
}

