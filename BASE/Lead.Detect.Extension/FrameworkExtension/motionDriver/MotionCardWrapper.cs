using System;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.FrameworkExtension.motionDriver
{
    public class MotionCardWrapper : IElement
    {
        public int Index { get; protected set; }
        public string Name { get; protected set; }

        public IP2PMotionCard Motion { get; protected set; }
        public ITriggerMotionCard Trigger { get; protected set; }
        public IInterpMotionCard Interp { get; protected set; }

        public MotionCardWrapper()
        {

        }

        public MotionCardWrapper(IMotionCard motion)
        {
            if (motion == null)
            {
                return;
            }

            Name = (motion as IPrim)?.Name;
            Index = motion.DevIndex;
            Motion = motion;
            Trigger = motion;
            Interp = motion;
        }

        public MotionCardWrapper(IP2PMotionCard motion)
        {
            if (motion == null)
            {
                return;
            }

            Name = (motion as IPrim)?.Name;
            Index = motion.DevIndex;
            Motion = motion;
        }



        public void Init(string file)
        {
            {
                var prim = Motion as IPrim;
                if (prim == null) throw new Exception();
                var ret = prim.IPrimInit();
                if (ret != 0) throw new Exception();
                var msg = "";
                ret = prim.IPrimConnect(ref msg);
                if (ret != 0) throw new Exception();
                ret = prim.IPrimStart();
                if (ret != 0) throw new Exception();
            }
            //Motion.LoadConfigFile(file);
        }

        public void Uninit()
        {
            var prim = Motion as IPrim;
            if (prim == null) throw new Exception();
            var ret = prim.IPrimStop();
            if (ret != 0) throw new Exception();
            var msg = "";
            ret = prim.IPrimDisConnect(ref msg);
            if (ret != 0) throw new Exception();
            ret = prim.IPrimDispose();
            if (ret != 0) throw new Exception();
        }


        public void SetDo(int port, int status)
        {
            Motion.WriteSingleDOutput(Index, 0, port, status);
        }

        public void GetDo(int port, out int status)
        {
            Motion.ReadSingleDOutput(Index, 0, port, out status);
        }

        public void GetDi(int port, out int status)
        {
            Motion.ReadSingleDInput(Index, 0, port, out status);
        }


        public int GetEncPos(int axis, ref int pos)
        {
            double p = 0;
            var ret = Motion.GetAxisPositionF(axis, ref p);
            pos = (int)p;
            return ret;
        }

        public int SetEncPos(int axis, int pos)
        {
            return Motion.SetAxisPositionOrFeedbackPules(axis, pos);
        }

        public int GetCommandPos(int axis, ref int pos)
        {
            double p = 0;
            var ret = Motion.GetAxisCommandF(axis, ref p);
            pos = (int)p;
            return ret;
        }

        public int SetCommandPos(int axis, int pos)
        {
            Motion.SetAxisPositionOrFeedbackPules(axis, pos);
            return 0;
        }


        public void ServoEnable(int axis, bool enable)
        {
            Motion.AxisSetEnable(Index, axis, enable);
        }

        public void MoveAbs(int axis, int pos, int vel)
        {
            Motion.AxisAbsMove(Index, axis, pos, vel);
        }

        public void MoveRel(int axis, int step, int vel)
        {
            Motion.AxisRelMove(Index, axis, step, vel);
        }

        public bool CheckMoveDone(int axis)
        {
            return Motion.AxisIsStop(Index, axis);
        }

        public void MoveStop(int axis)
        {
            Motion.AxisStopMove(Index, axis);
        }


        public void Home(int axis, int vel)
        {
            Motion.AxisSetHomeVel(axis, vel);
            Motion.AxisHomeMove(Index, axis);
        }

        public bool CheckHomeDone(int axis)
        {
            return !Motion.AxisHMV(Index, axis) && Motion.AxisIsStop(Index, axis);
        }

        public bool GetAxisEnable(int axis)
        {
            return Motion.AxisIsEnble(Index, axis);
        }

        public bool GetAxisAlarm(int axis)
        {
            return Motion.AxisIsAlarm(Index, axis);
        }

        public bool GetAxisEmg(int axis)
        {
            return Motion.AxisSingalEMG(Index, axis);
        }

        public bool GetAxisDone(int axis)
        {
            return Motion.AxisIsStop(Index, axis);
        }

        public bool GetAxisAstp(int axis)
        {
            return Motion.AxisAstp(Index, axis);
        }

        public bool GetAxisInp(int axis)
        {
            return Motion.AxisInp(Index, axis);
        }
        public bool CheckLimit(int axis)
        {
            return Motion.LimitMel(Index, axis) || Motion.LimitPel(Index, axis);
        }

        public bool LimitMel(int axis)
        {
            return Motion.LimitMel(Index, axis);

        }
        public bool LimitPel(int axis)
        {
            return Motion.LimitPel(Index, axis);
        }

        public bool LimitOrg(int axis)
        {
            return Motion.LimitOrg(Index, axis);
        }

        public override string ToString()
        {
            return $"{Name} {Index} {Motion.GetType().Name}";
        }

        public string Export()
        {
            return $"{Name} {Index} {Motion.GetType().Name}";
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');
            int i = 0;
            var id = int.Parse(data[i++]);

            Name = data[i++];
            Index = int.Parse(data[i++]);

            var typeName = data[i++];

            //load motioncardwrapper from prims
            var motionPrim = DevPrimsManager.Instance.GetPrimByName(Name);
            if (motionPrim == null)
            {
                throw new Exception($"LOAD MOTION ERROR: {Name} not found in DevPrimsManager");
            }
            if (motionPrim is IMotionCard)
            {
                var m = motionPrim as IMotionCard;
                Motion = m;
                Trigger = m;
                Interp = m;
            }
            else if (motionPrim is IP2PMotionCard)
            {
                Motion = motionPrim as IP2PMotionCard;
            }
            else
            {
                throw new Exception($"LOAD MOTION ERROR: {line}");
            }

            if (machine.MotionExs.ContainsKey(id))
            {
                return;
            }
            machine.MotionExs.Add(id, this);
        }

    }
}