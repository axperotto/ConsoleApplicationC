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
        COMMAND_FIRSTNIBBLE,
        COMMAND_LASTNIBBLE,
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
        private void composeFrame(byte newChar)
        {
            switch (messageIndexes_T)
            {
                case MessageIndexes_t.FRAMEVERSION:
                    if(newChar == 0x01)
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
                    actualFrame.MessageType = newChar;
                    messageIndexes_T++;
                    break;
                /*
                 * 00000000 01111010
                 * 11111100 00000100
                 * 11111100 01111110
                 * */
                case MessageIndexes_t.COMMAND_FIRSTNIBBLE:
                    actualFrame.Command = 0;
                    actualFrame.Command = newChar;
                    messageIndexes_T++;
                    break;
                case MessageIndexes_t.COMMAND_LASTNIBBLE:
                    actualFrame.Command = (UInt16)(actualFrame.Command | newChar << 8);
                    messageIndexes_T++;
                    break;
                case MessageIndexes_t.DATALENGTH:
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
                    actualFrame.OptionalData[subBufferIndex++] = newChar;
                    if(subBufferIndex == actualFrame.DataLength)
                    {
                        messageIndexes_T++;
                    }
                    break;
                case MessageIndexes_t.CRC:
                    messageIndexes_T++;
                    break;
            }
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
