using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppSerial
{
    public class ProcessBytes
    {
        UART_MsgComposer uART_MsgComposer;
        UartBuffer uartBuffer;
        public UART_MsgComposer UART_MsgComposer { get => uART_MsgComposer; set => uART_MsgComposer = value; }
        public UartBuffer UartBuffer { get => uartBuffer; set => uartBuffer = value; }

        public void StartThread()
        {
            Thread thread1 = new Thread(processBytes);
            thread1.Start();
        }

        void processBytes()
        {
            /* Va messo dentro un thread */
            while (true)
            {
                byte byteApp = 0;
                bufferState_t uartState = bufferState_t.BUFFER_EMPTY;

                do
                {
                    uartState = UartBuffer.popChar(out byteApp);
                    UART_MsgComposer.AddChar(byteApp);
                } while (uartState == bufferState_t.BUFFER_OK);

                /* Sleep 10ms */
                Thread.Sleep(10);
            }
        }
    }
}
