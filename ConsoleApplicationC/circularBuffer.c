#include "circularBuffer.h"

void addChar(circularBuffer_t* circularBufferPtr, uint8_t newChar)
{
	/* Scrivo nell'indice attuale */
	uint16_t writeIndex = circularBufferPtr->writeIndex;
	circularBufferPtr->byteArray[writeIndex] = newChar;

	/* Incrementare l'indice in maniera circolare */
	circularBufferPtr->writeIndex =
		(writeIndex + 1);

	/* Vede il trabocco del bicchiere */
	circularBufferPtr->writeIndex = circularBufferPtr->writeIndex % circularBufferPtr->bufferSize;

	/* Alternativa con l'if */
	if (circularBufferPtr->writeIndex >= circularBufferPtr->bufferSize)
	{
		circularBufferPtr->writeIndex = 0;
	}
}

bufferState_t  popChar(circularBuffer_t* circularBufferPtr, uint8_t* outChar)
{

}