using System;

namespace Lead.Detect.PrimVirtualCard.motionWrapper
{
    public struct CardAxisMotion
    {
        public bool Astp;
        public bool Hmv;
        public bool Mdn;
        public bool Alarm;
        public bool Mel;
        public bool Pel;
        public bool Org;
        public bool Servo;


        public bool IsMove;
        public int CurPos;
        public int CommandPos;


        public int Vel;
        public int Acc;
        public int Dec;

        /// <summary>
        ///     update at each 0.1s
        /// </summary>
        public void Update()
        {
            //is moving
            if (IsMove && !Mdn)
            {
                if (Math.Abs(Vel) < 10)
                {
                    Vel = 10 * Math.Sign(Vel);
                }

                CurPos = CurPos + (int)(Vel * 0.1);

                //check move finish
                if (Math.Abs(CurPos - CommandPos) <= Math.Abs(Vel * 0.1))
                {
                    CurPos = CommandPos;
                    Mdn = true;
                    IsMove = false;
                }
            }
        }

        public override string ToString()
        {
            return $"CurPos {CurPos:F2} CommandPos {CommandPos:F2} Vel {Vel:F2}";
        }
    }
}