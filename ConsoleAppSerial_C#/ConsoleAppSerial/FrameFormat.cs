using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSerial
{
    public class FrameFormat
    {
        public bool IsCRC_ok = true;
        public byte Header = 0xBC;
        public byte FrameVersion = 1;
        public byte MessageType = 0;
        public UInt16 Command = 0;
        public byte DataLength = 0;
        public byte[] OptionalData = new byte[256];
    }
}
