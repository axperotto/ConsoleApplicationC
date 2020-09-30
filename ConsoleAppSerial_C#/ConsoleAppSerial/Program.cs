using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppSerial
{


    class Program
    {
        static void Main(string[] args)
        {
            UART_MsgComposer uART_MsgComposer = new UART_MsgComposer();
            UartBuffer uartBuffer = new UartBuffer(100000);

            /* Passarlo al driver seriale che non abbiamo ancora */
            SerialManager serialManager = new SerialManager();
            serialManager.OpenSerial(uartBuffer, "COM4", 115200);

            ProcessBytes processBytes = new ProcessBytes();
            processBytes.UartBuffer = uartBuffer;
            processBytes.UART_MsgComposer = uART_MsgComposer;
            processBytes.StartThread();

        }
    }
}
