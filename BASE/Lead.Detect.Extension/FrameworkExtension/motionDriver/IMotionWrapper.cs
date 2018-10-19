using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;

namespace Lead.Detect.FrameworkExtension.motionDriver
{

    public interface IMotionWrapper : IElement
    {
        string Name { get; }
        void Init(string file);

        void Uninit();


        void GetDi(int port, out int status);
        void SetDo(int port, int status);
        void GetDo(int port, out int status);

        int GetCommandPos(int axis, ref int pos);
        int GetEncPos(int axis, ref int pos);

        int SetCommandPos(int axis, int pos);
        int SetEncPos(int axis, int pos);


        void ServoEnable(int axis, bool enable);
        void Home(int axis, int vel);
        void MoveAbs(int axis, int pos, int vel);
        void MoveRel(int axis, int step, int vel);
        void MoveStop(int axis);


        bool CheckHomeDone(int axis);
        bool CheckMoveDone(int axis);

        bool GetAxisEnable(int axis);
        bool GetAxisAlarm(int axis);
        bool GetAxisEmg(int axis);
        bool GetAxisDone(int axis);

        bool GetAxisAstp(int axis);
        bool CheckLimit(int axis);
        bool LimitMel(int axis);
        bool LimitPel(int axis);
        bool LimitOrg(int axis);
    }
}