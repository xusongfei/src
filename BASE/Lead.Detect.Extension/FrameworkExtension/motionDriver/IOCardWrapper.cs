using System;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.FrameworkExtension.motionDriver
{
    public class IOCardWrapper : IGioWrapper
    {
        private readonly IMotionCard _motion;


        public int Id;

        public int Index;

        public string Name;

        public IOCardWrapper(IMotionCard motion)
        {
            Index = _motion.DevIndex;
            _motion = motion;
        }

        public void Init(string file)
        {
            {
                var prim = _motion as IPrim;
                if (prim == null) throw new Exception();
                var ret = prim.IPrimInit();
                if (ret != 0) throw new Exception();
                var msg = "";
                ret = prim.IPrimConnect(ref msg);
                if (ret != 0) throw new Exception();
                ret = prim.IPrimStart();
                if (ret != 0) throw new Exception();
            }
            _motion.LoadConfigFile(file);
        }

        public void Uninit()
        {
            var prim = _motion as IPrim;
            if (prim == null) throw new Exception();
            var ret = prim.IPrimStop();
            if (ret != 0) throw new Exception();
            var msg = "";
            ret = prim.IPrimDisConnect(ref msg);
            if (ret != 0) throw new Exception();
            ret = prim.IPrimDispose();
            if (ret != 0) throw new Exception();
        }

        public void GetDi(int port, out int status)
        {
            _motion.ReadSingleDInput(Index, 0, port, out status);
        }

        public void SetDi(int port, int status)
        {
            throw new NotImplementedException();
        }


        public void SetDo(int port, int status)
        {
            _motion.WriteSingleDOutput(Index, 0, port, status);
        }

        public void GetDo(int port, out int status)
        {
            _motion.ReadSingleDOutput(Index, 0, port, out status);
        }
    }
}