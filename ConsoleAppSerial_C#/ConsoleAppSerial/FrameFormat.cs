using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSerial
{
    public class FrameFormat
    {
        public byte Header = 0;
        public byte FrameVersion = 0;
        public byte MessageType = 0;
        public UInt16 Command = 0;
        public byte DataLength = 0;
        public byte[] OptionalData = new byte[256];
    }
}
