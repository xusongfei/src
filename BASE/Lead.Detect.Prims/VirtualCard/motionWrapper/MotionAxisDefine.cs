namespace Lead.Detect.PrimVirtualCard.motionWrapper
{
    public class MotionAxisDefine
    {
        public const int MOTION_IO_ALM = 0x01 << 0;
        public const int MOTION_IO_MEL = 0x01 << 1;
        public const int MOTION_IO_PEL = 0x01 << 2;
        public const int MOTION_IO_ORG = 0x01 << 3;
        public const int MOTION_IO_SVON = 0x01 << 4;
        public const int MOTION_IO_EMG = 0x01 << 5;

        public const int MOTION_STS_HMV = 0x01 << 7;
        public const int MOTION_STS_MDN = 0x01 << 8;
        public const int MOTION_STS_ASTP = 0x01 << 9;
        public const int MOTION_STS_HOME = 0x01 << 10;
        public const int MOTION_STS_INP = 0x01 << 11; // In position.
        public const int MOTION_STS_SERVO = 0x01 << 12; // Servo on signal.
        public const int MOTION_STS_RDY = 0x01 << 13; // Ready.


        //public const int MOTION_STS_RDY   = 0x01 << 8; // Ready.
        //public const int MOTION_STS_SERVO = 0x01 << 7;   // Servo on signal.
        //public const int MOTION_STS_INP   = 0x01 << 6;   // In position.

        //public const int MOTION_STS_HOME  = 0x01 << 3;   
        //public const int MOTION_STS_ASTP  = 0x01 << 2;   
        //public const int MOTION_STS_MDN   = 0x01 << 1;
        //public const int MOTION_STS_HMV   = 0x01 << 0;
    }
}