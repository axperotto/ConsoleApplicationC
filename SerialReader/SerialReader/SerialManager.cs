using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSerial
{
    public class SerialManager
    {
        SerialPort serialPort = new SerialPort();
        UartBuffer m_buffer;
        public void OpenSerial(UartBuffer buffer, String portName, int baudrate)
        {
            m_buffer = buffer;

            serialPort.PortName = portName;
            serialPort.BaudRate = baudrate;

            serialPort.DataReceived += SerialPort_DataReceived;

            serialPort.Open();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while(serialPort.BytesToRead != 0)
            {
                byte newByte = (byte)serialPort.ReadByte();
                m_buffer.addChar(newByte);
            }
        }


        public void CloseSerial()
        {

        }

        public void SendCommand(byte[] bytesToSend)
        {
            if(serialPort.IsOpen == true)
            {
                serialPort.Write(bytesToSend, 0, bytesToSend.Length);
            }
        }
    }
}
