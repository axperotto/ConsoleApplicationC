using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSerial
{
    public enum UART_MsgComposer_State_t
    {
        WAITING_FOR_BC = 0,
        WAITING_FOR_MSG_COMPLETE = 1
    }

    public enum MessageIndexes_t
    {
        FRAMEVERSION = 0,
        MESSAGETYPE,
        COMMAND_MSB_NIBBLE,
        COMMAND_LSB_NIBBLE,
        DATALENGTH,
        OPTIONALDATA,
        CRC
    }


    public class UART_MsgComposer
    {
        FrameFormat actualFrame = new FrameFormat();
        UART_MsgComposer_State_t uART_MsgComposer_State = UART_MsgComposer_State_t.WAITING_FOR_BC;
        int subBufferIndex = 0;
        MessageIndexes_t messageIndexes_T = MessageIndexes_t.FRAMEVERSION;
        byte actualCRC = 0xBC;

        public event EventHandler<FrameFormat> OnFrameReceived;

        private void composeFrame(byte newChar)
        {
            switch (messageIndexes_T)
            {
                case MessageIndexes_t.FRAMEVERSION:
                    actualCRC = (byte)(actualCRC ^ newChar);

                    if (newChar == 0x01)
                    {
                        actualFrame.FrameVersion = 0x01;
                        messageIndexes_T++;
                    }
                    else
                    {
                        /* caso di errore resetto la macchina a stati */
                        uART_MsgComposer_State = UART_MsgComposer_State_t.WAITING_FOR_BC;
                        messageIndexes_T = MessageIndexes_t.FRAMEVERSION;
                    }
                    break;
                case MessageIndexes_t.MESSAGETYPE:
                    actualCRC = (byte)(actualCRC ^ newChar);
                    actualFrame.MessageType = newChar;
                    messageIndexes_T++;
                    break;
                /*
                 * 00000000 01111010
                 * 11111100 00000100
                 * 11111100 01111110
                 * */
                case MessageIndexes_t.COMMAND_MSB_NIBBLE:
                    actualCRC = (byte)(actualCRC ^ newChar);
                    actualFrame.Command = 0;
                    actualFrame.Command = (UInt16)(actualFrame.Command | newChar << 8);
                    messageIndexes_T++;
                    break;
                case MessageIndexes_t.COMMAND_LSB_NIBBLE:
                    actualCRC = (byte)(actualCRC ^ newChar);
                    actualFrame.Command = (UInt16)(actualFrame.Command | newChar);
                    messageIndexes_T++;
                    break;
                case MessageIndexes_t.DATALENGTH:
                    actualCRC = (byte)(actualCRC ^ newChar);
                    actualFrame.DataLength = newChar;
                    subBufferIndex = 0;
                    if(actualFrame.DataLength == 0)
                    {
                        messageIndexes_T = MessageIndexes_t.CRC;
                    }
                    else
                    {
                        messageIndexes_T++;
                    }
                    break;
                case MessageIndexes_t.OPTIONALDATA:
                    actualCRC = (byte)(actualCRC ^ newChar);
                    actualFrame.OptionalData[subBufferIndex++] = newChar;
                    if(subBufferIndex == actualFrame.DataLength)
                    {
                        messageIndexes_T++;
                    }
                    break;
                case MessageIndexes_t.CRC:
                    /*
                     * 101110 ^
                     * 001010 =
                     * 100100
                     */
                    actualCRC = (byte)~actualCRC;
                    actualFrame.IsCRC_ok = ((actualCRC ^ newChar) == 0);

                    if (OnFrameReceived != null)
                    {
                        OnFrameReceived(this, actualFrame);
                    }

                    /* Resetto le macchina a stati perchè ho finito */
                    uART_MsgComposer_State = UART_MsgComposer_State_t.WAITING_FOR_BC;
                    messageIndexes_T = MessageIndexes_t.FRAMEVERSION;
                    break;
            }
        }

        public byte[] GetBytesStream(FrameFormat frameRequestFwVersion)
        {
            byte[] retVal = new byte[7 + frameRequestFwVersion.DataLength];

            byte crc = 0;
            retVal[0] = frameRequestFwVersion.Header;
            crc = retVal[0];
            retVal[1] = frameRequestFwVersion.FrameVersion;
            crc = (byte)(crc ^ retVal[1]);
            retVal[2] = frameRequestFwVersion.MessageType;
            retVal[3] = (byte)((frameRequestFwVersion.Command & 0xFF00) >> 8);
            retVal[4] = (byte)(frameRequestFwVersion.Command & 0x00FF);
            retVal[5] = frameRequestFwVersion.DataLength;

            for (int n = 0; n < frameRequestFwVersion.DataLength; n++)
            {
                retVal[6 + n] = frameRequestFwVersion.OptionalData[n];
            }

            for(int n = 0;n< 6 + frameRequestFwVersion.DataLength;n++)
            {
                crc = (byte)(crc ^ frameRequestFwVersion.OptionalData[n]);
            }

            retVal[7 + frameRequestFwVersion.DataLength - 1] = crc;

            return retVal;
        }

        public void AddChar(byte newChar)
        {
            switch (uART_MsgComposer_State)
            {
                case UART_MsgComposer_State_t.WAITING_FOR_BC:
                    if(newChar == 0xBC)
                    {
                        actualFrame = new FrameFormat();
                        actualFrame.Header = 0xBC;
                        uART_MsgComposer_State = UART_MsgComposer_State_t.WAITING_FOR_MSG_COMPLETE;
                    }
                    break;
                case UART_MsgComposer_State_t.WAITING_FOR_MSG_COMPLETE:
                    composeFrame(newChar);
                    break;
            }
        }
    }
}
