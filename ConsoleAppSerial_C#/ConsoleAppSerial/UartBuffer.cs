using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSerial
{
    public enum bufferState_t
    {
        BUFFER_EMPTY = 0,
        BUFFER_OK
    }

    public class UartBuffer
    {
        byte[] byteArray;
        int readIndex;
        int writeIndex;
        bool overflow;

        public UartBuffer(int bufferSize)
        {
            byteArray = new byte[bufferSize];
            readIndex = 0;
            writeIndex = 0;
            overflow = false;
        }

        public void addChar(byte newChar)
        {
            /* Scrivo nell'indice attuale */
            byteArray[writeIndex] = newChar;

            /* Incrementare l'indice in maniera circolare */
            writeIndex++;

            if (writeIndex >= byteArray.Length)
            {
                writeIndex = 0;
                overflow = true;
            }
        }

        public bufferState_t popChar(out byte outChar)
        {
            bufferState_t retVal = bufferState_t.BUFFER_EMPTY;
            outChar = 0;

            if (overflow == false)
            {
                if (readIndex != writeIndex)
                {
                    retVal = bufferState_t.BUFFER_OK;

                    /* Dereferenziamo il puntatore di outChar */
                    outChar = byteArray[readIndex];
                    readIndex++;
                    if (readIndex >= byteArray.Length)
                    {
                        readIndex = 0;
                    }
                }
            }
            else
            {
                retVal = bufferState_t.BUFFER_OK;

                /* Dereferenziamo il puntatore di outChar */
                outChar = byteArray[readIndex];
                readIndex++;

                if (readIndex >= byteArray.Length)
                {
                    readIndex = 0;
                    overflow = false;
                }

            }

            return retVal;
        }
    }
}
