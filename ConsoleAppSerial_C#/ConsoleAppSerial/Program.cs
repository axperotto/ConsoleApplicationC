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
        static ManualResetEvent resetEvent = new ManualResetEvent(false);
        static FrameFormat frameRx = null;
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

            uART_MsgComposer.OnFrameReceived += UART_MsgComposer_OnFrameReceived;


            /* Handshake */
            FrameFormat frameRequestFwVersion = new FrameFormat();
            frameRequestFwVersion.Command = 0x01; /* Read Fw Version */
            byte[] bytesToSend = uART_MsgComposer.GetBytesStream(frameRequestFwVersion);
            serialManager.SendCommand(bytesToSend);

            /* Wait For Rx */
            if(resetEvent.WaitOne(1000))
            {
                resetEvent.Reset();
                /* Analizzo frameRx */

            }
            else
            {
                /* Segnalo il timeout */
            }
        }

        private static void UART_MsgComposer_OnFrameReceived(object sender, FrameFormat e)
        {
            frameRx = e;
            resetEvent.Set();
        }
    }
}
