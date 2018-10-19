using System;
using System.Runtime.InteropServices;

namespace Lead.Detect.Interfaces.Dev
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PTStatus
    {
        public UInt16 BitSts; //b0: Is PTB work? [1:working, 0:Stopped]

        //b1: Is point buffer full? [1:full, 0:not full]
        //b2: Is point buffer empty? [1:empty, 0:not empty]
        //b3, b4, b5: Reserved for future
        //b6~: Be always 0
        public UInt16 PntBufFreeSpace;
        public UInt16 PntBufUsageSpace;
        public UInt32 RunningCnt;
    }
}