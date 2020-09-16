#include "circularBuffer.h"

void addChar(circularBuffer_t* circularBufferPtr, uint8_t newChar)
{
	/* Scrivo nell'indice attuale */
	uint16_t writeIndex = circularBufferPtr->writeIndex;
	circularBufferPtr->byteArray[writeIndex] = newChar;

	/* Incrementare l'indice in maniera circolare */
	circularBufferPtr->writeIndex++;

	if (circularBufferPtr->writeIndex >= circularBufferPtr->bufferSize)
	{
		circularBufferPtr->writeIndex = 0;
		circularBufferPtr->overflow++;
	}
}

bufferState_t  popChar(circularBuffer_t* circularBufferPtr, uint8_t* outChar)
{
	bufferState_t retVal = BUFFER_EMPTY;

	if (circularBufferPtr->overflow == 0)
	{
		if (circularBufferPtr->readIndex != circularBufferPtr->writeIndex)
		{
			retVal = BUFFER_OK;

			/* Dereferenziamo il puntatore di outChar */
			uint16_t readIndex = circularBufferPtr->readIndex++;
			*outChar = circularBufferPtr->byteArray[readIndex];

			if (circularBufferPtr->readIndex >= circularBufferPtr->bufferSize)
			{
				circularBufferPtr->readIndex = 0;
			}
		}
	}
	else
	{
		retVal = BUFFER_OK;

		/* Dereferenziamo il puntatore di outChar */
		uint16_t readIndex = circularBufferPtr->readIndex++;
		*outChar = circularBufferPtr->byteArray[readIndex];

		if (circularBufferPtr->readIndex >= circularBufferPtr->bufferSize)
		{
			circularBufferPtr->readIndex = 0;
			circularBufferPtr->overflow = 0;
		}
			
	}

	return retVal;
}