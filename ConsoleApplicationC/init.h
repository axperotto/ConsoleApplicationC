#ifndef INIT_H
#define INIT_H
#include "inttypes.h"

typedef struct _packInts {
	uint8_t m;
	int16_t s;
	uint8_t j[1024];
/* byte 0 */
	uint8_t x : 4;
	uint8_t y : 4;

/* byte 1 */
	uint8_t g : 3;
	uint8_t h : 3;
	uint8_t p : 2;
} packsInts_t;

void init();

#endif /* INIT_H */