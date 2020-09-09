#ifndef CIRCULARBUFFER_C
#define CIRCULARBUFFER_C

#include "inttypes.h"

typedef enum _bufferState {
    BUFFER_EMPTY = 0,
    BUFFER_OK
} bufferState_t;

typedef struct _circularBuffer {
    const uint16_t bufferSize;
    uint8_t* byteArray;
    uint16_t readIndex;
    uint16_t writeIndex;
} circularBuffer_t;

/* Mette il carattere nella posizione i-esima e incrementa l'indixe in maniera circolare */
void addChar(circularBuffer_t* circularBufferPtr, uint8_t newChar);
bufferState_t  popChar(circularBuffer_t* circularBufferPtr, uint8_t* outChar);

#endif /* CIRCULARBUFFER_C */