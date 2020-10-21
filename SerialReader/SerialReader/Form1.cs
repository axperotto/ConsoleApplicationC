using ConsoleAppSerial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace SerialReader
{
    public partial class Form1 : Form
    {
        SerialManager serialManager = new SerialManager();
        UartBuffer uartBuffer = new UartBuffer(100000);
        UART_MsgComposer uART_MsgComposer = new UART_MsgComposer();

        AutoResetEvent resetEvent = new AutoResetEvent(false);
        FrameFormat actualFrameFormat = null;
        public Form1()
        {
            InitializeComponent();
            comboBoxSerialPorts.Items.AddRange(SerialPort.GetPortNames());

            uART_MsgComposer.OnFrameReceived += UART_MsgComposer_OnFrameReceived;

            ProcessBytes processBytes = new ProcessBytes();
            processBytes.UartBuffer = uartBuffer;
            processBytes.UART_MsgComposer = uART_MsgComposer;
            processBytes.StartThread();

        }

        private void UART_MsgComposer_OnFrameReceived(object sender, FrameFormat e)
        {
            resetEvent.Set();
            actualFrameFormat = e;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            /* Passarlo al driver seriale che non abbiamo ancora */
            serialManager.OpenSerial(uartBuffer, comboBoxSerialPorts.Text, 115200);
        }

        private void buttonRefreshSerialPorts_Click(object sender, EventArgs e)
        {
            comboBoxSerialPorts.Items.Clear();
            comboBoxSerialPorts.Items.AddRange(SerialPort.GetPortNames());
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        const int timeoutOperation = 2000;
        private void buttonSendDNFUCode_Click(object sender, EventArgs e)
        {
            buttonSendDNFUCode.Enabled = false;
            FrameFormat frameFormat = new FrameFormat();
            frameFormat.Command = (ushort)FrameCmd.FRAME_WRITE_MAKER_CODE;
            frameFormat.MessageType = (byte)MessageType_e.REQUEST;
            frameFormat.DataLength = (byte)sizeof(UInt32);

            UInt32 DNFUCode = Convert.ToUInt32("0x" + maskedTextBoxDNFUCode.Text, 16);
            byte[] bytesDNFUCode = BitConverter.GetBytes(DNFUCode);

            if(BitConverter.IsLittleEndian == true)
            {
                bytesDNFUCode = (byte[])bytesDNFUCode.Reverse().ToArray();
            }

            for(int i = 0;i< frameFormat.DataLength; i++)
            {
                frameFormat.OptionalData[i] = bytesDNFUCode[i];
            }
            serialManager.SendCommand(uART_MsgComposer.GetBytesStream(frameFormat));
            
            Thread thread1 = new Thread(WaitDNFUSet);
            thread1.Start();
        }

        private void InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }

        private void WaitDNFUSet()
        {
            if (resetEvent.WaitOne(timeoutOperation) == true)
            {
                /* Led Verde */
                bool isOK = (actualFrameFormat.DataLength == 1) &&
                    (actualFrameFormat.OptionalData[0] == 1) &&
                    (actualFrameFormat.IsCRC_ok == true);
            }
            else
            {
                /* Led Rosso */
            }

            InvokeUI(() =>
            {
                buttonSendDNFUCode.Enabled = true;
            });
        }
    }
}
