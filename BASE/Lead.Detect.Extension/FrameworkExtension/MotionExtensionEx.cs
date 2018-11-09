using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension
{
    /// <summary>
    /// IVioEx Extension
    /// </summary>
    public static partial class MotionExtensionEx
    {
        public static bool GetVioSts(this IVioEx vioex, bool status = true)
        {
            int sts;
            vioex.DriverCard.GetDo(vioex.Port, out sts);
            return (sts == 1) == status;
        }

        public static bool SetVio(this IVioEx vioex, StationTask task, bool status = true)
        {
            task?.AbortIfCancel(nameof(SetVio));
            task?.JoinIfPause();

            vioex.DriverCard.SetDo(vioex.Port, status ? 1 : 0);
            task?.Log($"{vioex.Name} SetVio {vioex.Port} {status} success", LogLevel.Debug);
            return true;
        }

        public static bool WaitVioAndClear(this IVioEx vioex, StationTask task, bool status = true,
            bool clearStatus = false, int timeout = -1)
        {
            if (!WaitVio(vioex, task, status, timeout))
            {
                return false;
            }

            SetVio(vioex, task, clearStatus);
            return true;
        }

        public static bool WaitVio(this IVioEx vioex, StationTask task, bool status = true, int timeout = -1)
        {
            task?.AbortIfCancel(nameof(WaitVio));
            task?.JoinIfPause();

            timeout = timeout < 0 ? int.MaxValue : timeout;

            var err = $"{vioex.Name} WaitVio {vioex.Port} {status}";
            var t = 0;
            while (t++ <= timeout)
            {
                var sts = 0;
                vioex.DriverCard.GetDo(vioex.Port, out sts);
                if (sts == 1 == status)
                {
                    task?.Log($"{err} success", LogLevel.Debug);
                    return true;
                }

                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel(nameof(WaitVio));
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            task?.Log($"{err} timeout", LogLevel.Error);
            return false;
        }
    }

    /// <summary>
    /// ICylinderEx Extension
    /// </summary>
    public static partial class MotionExtensionEx
    {
        public static void SetDoAsync(this ICylinderEx[] cyex, StationTask task, bool status = true)
        {
            for (int i = 0; i < cyex.Length; i++)
            {
                if (cyex[i].DoOrg >= 0)
                    cyex[i].DriverCard2.SetDo(cyex[i].DoOrg, status ? 0 : 1);
                if (cyex[i].DoWork >= 0)
                    cyex[i].DriverCard2.SetDo(cyex[i].DoWork, status ? 1 : 0);
            }
        }

        public static bool SetDo(this ICylinderEx cyex, StationTask task, bool status = true, int timeout = 3000, bool? ignoreOrWaringOrError = false)
        {
            return SetDo(new[] { cyex }, task, new[] { status }, timeout, ignoreOrWaringOrError);
        }

        public static bool SetDo(this ICylinderEx[] cyex, StationTask task, bool[] status, int timeout = 3000, bool? ignoreOrWaringOrError = false)
        {
            for (int i = 0; i < cyex.Length; i++)
            {
                if (cyex[i].DoOrg >= 0)
                    cyex[i].DriverCard2.SetDo(cyex[i].DoOrg, status[i] ? 0 : 1);
                if (cyex[i].DoWork >= 0)
                    cyex[i].DriverCard2.SetDo(cyex[i].DoWork, status[i] ? 1 : 0);
            }

            if (FrameworkExtenion.IsSimulate)
            {
                var err = $"{string.Join(",", cyex.Select(c => c.Name))} SetDo {string.Join(",", status)} {timeout}";
                task?.Log($"{err} success", LogLevel.Debug);
                return true;
            }


            return WaitDi(cyex, task, status, timeout, ignoreOrWaringOrError);
        }

        public static bool WaitDi(this ICylinderEx cyex, StationTask task, bool status = true, int timeout = -1)
        {
            return WaitDi(new[] { cyex.DiOrg, cyex.DiWork }, task, new[] { cyex.DriverCard, cyex.DriverCard }, new[] { !status, status }, timeout);
        }

        public static bool WaitDi(this ICylinderEx[] cyex, StationTask task, bool[] status, int timeout = -1, bool? ignoreOrWaringOrError = false)
        {
            task?.AbortIfCancel(nameof(WaitDi));
            task?.JoinIfPause();

            if (FrameworkExtenion.IsSimulate)
            {
                var msg = $"{string.Join(",", cyex.Select(c => c.Name))} WaitDi {string.Join(",", status)} {timeout}";
                task?.Log($"{msg} success", LogLevel.Debug);
                return true;
            }


            var motion1 = cyex.Select(c => c.DriverCard);
            var motion2 = cyex.Select(c => c.DriverCard);
            var motion = motion1.Concat(motion2).ToArray();

            var diOrg = cyex.Select(c => c.DiOrg);
            var diWork = cyex.Select(c => c.DiWork);

            var diOrgSts = status.Select(s => !s);
            var diWorkSts = status.Select(s => s);

            var waitdi = diOrg.Concat(diWork).ToArray();
            var waitSts = diOrgSts.Concat(diWorkSts).ToArray();

            var err = $"{string.Join(",", cyex.Select(c => c.Name))} SetDo {string.Join(",", status)} WaitDi {timeout}";
            var ret = WaitDi(waitdi, task, motion, waitSts, timeout);
            if (!ret)
            {
                task?.Log($"{err} error", ignoreOrWaringOrError == null ? LogLevel.Debug : ((ignoreOrWaringOrError == false) ? LogLevel.Warning : LogLevel.Error));
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Debug);
            }

            return ret;
        }
    }

    /// <summary>
    /// IDoEx IDiEx Extension
    /// </summary>
    public static partial class MotionExtensionEx
    {
        public static bool GetDoSts(this IDoEx doex, bool status = true)
        {
            int sts;
            doex.DriverCard.GetDo(doex.Port, out sts);
            if (sts == 1 == status) return true;
            return false;
        }

        public static bool SetDo(this IDoEx doex, bool status = true)
        {
            doex.DriverCard.SetDo(doex.Port, status ? 1 : 0);
            return true;
        }

        public static bool SetDoAsync(this IDoEx[] doex, bool[] status)
        {
            for (int i = 0; i < doex.Length; i++)
            {
                doex[i].DriverCard.SetDo(doex[i].Port, status[i] ? 1 : 0);
            }

            return true;
        }

        public static bool Toggle(this IDoEx doex)
        {
            SetDo(doex, !GetDoSts(doex));
            return true;
        }

        public static bool WaitDo(this IDoEx[] doex, StationTask task, bool[] status, int timeout = -1)
        {
            return WaitDo(doex.Select(d => d.Port).ToArray(), task, doex.Select(d => d.DriverCard).ToArray(), status, timeout);
        }

        public static bool WaitDo(this int[] diex, StationTask task, MotionCardWrapper[] motion, bool[] status, int timeout = -1)
        {
            task?.AbortIfCancel(nameof(WaitDi));
            task?.JoinIfPause();

            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

            int[] sts = new int[diex.Length];

            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ <= timeout)
            {
                //check di status
                for (int i = 0; i < diex.Length; i++)
                {
                    motion[i].GetDo(diex[i], out sts[i]);
                }

                bool ret = true;
                for (int i = 0; i < diex.Length; i++)
                {
                    ret = ((sts[i] == 1) == status[i]);
                    if (!ret)
                    {
                        break;
                    }
                }

                if (ret)
                {
                    return true;
                }

                //check task status
                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel(nameof(WaitDi));
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            return false;
        }

        private static object _getdi = new object();


        /// <summary>
        /// get di sts based on normal open/closes
        /// </summary>
        /// <param name="diex"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool GetDiSts(this IDiEx diex, bool status = true)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                if (diex.Type == EleDiType.Open)
                {
                    return diex.Enable;
                }
                else if (diex.Type == EleDiType.Close)
                {
                    return diex.Enable;
                }
                else
                {
                    return false;
                }
            }

            lock (_getdi)
            {
                int sts;
                diex.DriverCard.GetDi(diex.Port, out sts);

                if (diex.Type == EleDiType.Open)
                {
                    var ret = (sts == 1) == status;
                    return diex.Enable && ret;
                }
                else if (diex.Type == EleDiType.Close)
                {
                    var ret = (sts == 0) == status;
                    return diex.Enable && ret;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool GetDiStsRaw(this IDiEx diex, bool status = true)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                if (diex.Type == EleDiType.Open)
                {
                    return diex.Enable;
                }
                else if (diex.Type == EleDiType.Close)
                {
                    return diex.Enable;
                }
                else
                {
                    return false;
                }
            }

            lock (_getdi)
            {
                int sts;
                diex.DriverCard.GetDi(diex.Port, out sts);
                var ret = (sts == 1) == status;
                return ret;
            }
        }


        public static bool WaitDi(this IDiEx[] diex, StationTask task, bool status = true, int timeout = -1)
        {
            return WaitDi(diex.Select(d => d.Port).ToArray(), task, diex.Select(d => d.DriverCard).ToArray(), diex.Select(d => status).ToArray(), timeout);
        }


        public static bool WaitDi(this IDiEx diex, StationTask task, bool status = true, int timeout = -1)
        {
            return WaitDi(diex.Port, task, diex.DriverCard, status, timeout);
        }

        public static bool WaitDi(this int diex, StationTask task, MotionCardWrapper motion, bool status = true, int timeout = -1)
        {
            return WaitDi(new[] { diex }, task, new[] { motion }, new[] { status }, timeout);
        }


        /// <summary>
        /// must be normal open di
        /// </summary>
        /// <param name="diex"></param>
        /// <param name="task"></param>
        /// <param name="motion"></param>
        /// <param name="status"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool WaitDi(this int[] diex, StationTask task, MotionCardWrapper[] motion, bool[] status, int timeout = -1)
        {
            task?.AbortIfCancel(nameof(WaitDi));
            task?.JoinIfPause();

            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

            int[] sts = new int[diex.Length];

            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ <= timeout)
            {
                //check di status
                for (int i = 0; i < diex.Length; i++)
                {
                    motion[i].GetDi(diex[i], out sts[i]);
                }

                bool ret = true;
                for (int i = 0; i < diex.Length; i++)
                {
                    ret = ((sts[i] == 1) == status[i]);
                    if (!ret)
                    {
                        break;
                    }
                }

                if (ret)
                {
                    return true;
                }

                //check task status
                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel(nameof(WaitDi));
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }
            return false;
        }
    }


    /// <summary>
    /// IAxisEx Extension
    /// </summary>
    public static partial class MotionExtensionEx
    {
        public static bool Stop(this IAxisEx[] axis)
        {
            foreach (var a in axis)
            {
                a?.DriverCard.MoveStop(a.AxisChannel);
            }

            return true;
        }

        public static bool GetAlarm(this IAxisEx a)
        {
            return a.DriverCard.GetAxisAlarm(a.AxisChannel) || a.DriverCard.GetAxisEmg(a.AxisChannel);
        }

        public static bool GetServo(this IAxisEx a)
        {
            return a.DriverCard.GetAxisEnable(a.AxisChannel);
        }

        public static bool GetMdn(this IAxisEx a)
        {
            return a.DriverCard.GetAxisDone(a.AxisChannel);
        }

        public static bool GetAstp(this IAxisEx a)
        {
            return a.DriverCard.GetAxisAstp(a.AxisChannel);
        }

        public static bool GetInp(this IAxisEx a)
        {
            return a.DriverCard.GetAxisInp(a.AxisChannel);
        }

        public static bool GetPel(this IAxisEx a)
        {
            return a.DriverCard.LimitPel(a.AxisChannel);
        }

        public static bool GetOrg(this IAxisEx a)
        {
            return a.DriverCard.LimitOrg(a.AxisChannel);
        }

        public static bool GetMel(this IAxisEx a)
        {
            return a.DriverCard.LimitMel(a.AxisChannel);
        }

        public static int[] GetPulse(this IAxisEx[] axis)
        {
            return axis.Select(GetPulse).ToArray();
        }

        public static int GetPulse(this IAxisEx axis)
        {
            int pos = 0;
            axis?.DriverCard.GetEncPos(axis.AxisChannel, ref pos);
            return pos;
        }

        public static double[] GetPos(this IAxisEx[] axis)
        {
            return axis.Select(GetPos).ToArray();
        }

        public static double GetPos(this IAxisEx axis)
        {
            int pos = 0;
            if (axis != null)
            {
                axis.DriverCard.GetEncPos(axis.AxisChannel, ref pos);
                return (int)(1000 * axis.ToMm(pos)) / 1000d;
            }
            return pos;
        }


        public static bool ServoEnable(this IAxisEx axis, StationTask task, bool status)
        {
            return ServoEnable(new[] { axis }, task, status);
        }

        public static bool ServoEnable(this IAxisEx[] axis, StationTask task, bool status)
        {
            task?.AbortIfCancel(nameof(ServoEnable));
            task?.JoinIfPause();

            //start servo
            foreach (var a in axis)
            {
                a?.DriverCard.ServoEnable(a.AxisChannel, status);
            }

            //wait servo
            Thread.Sleep(100);
            var err = $"{string.Join(",", axis.Select(a => a == null ? string.Empty : a.Name))} ServoEnable {status}";
            var ret = axis.All(a => a == null || (a.GetServo() && !a.GetAlarm()));
            if (!ret)
            {
                task?.Log($"{err} error", LogLevel.Error);
                task?.ThrowException($"{err} error");
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Debug);
            }

            return ret;
        }


        public static bool Home(this IAxisEx axis, StationTask task, double vel, int timeout = -1)
        {
            return Home(new[] { axis }, task, new[] { vel }, timeout);
        }

        public static bool Home(this IAxisEx[] axis, StationTask task, double[] vel, int timeout = -1)
        {
            if (axis.Any(a => a != null && (!a.GetServo() || a.GetAlarm())))
            {
                task?.ThrowException("Home Servo Error");
                return false;
            }

            task?.AbortIfCancel(nameof(Home));
            task?.JoinIfPause();

            //start home
            foreach (var a in axis)
            {
                a?.DriverCard.Home(a.AxisChannel, a.ToPls(a.HomeVm));
            }

            var axisInps = axis.Select(a => a == null || a.GetInp()).ToArray();

            //wait home done
            var err = $"{string.Join(",", axis.Select(a => a == null ? string.Empty : a.Name))} Home";
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ <= timeout)
            {
                if (axis.All(a =>
                    a == null
                    || (axisInps[Array.IndexOf(axis, a)] && a.GetInp() && a.DriverCard.CheckHomeDone(a.AxisChannel))
                    || (!axisInps[Array.IndexOf(axis, a)] && !a.GetInp() && a.DriverCard.CheckHomeDone(a.AxisChannel)))
                )
                {
                    var ret = axis.All(a => a.DriverCard.SetEncPos(a.AxisChannel, 0) == 0);
                    task?.Log($"{err} success {ret}", LogLevel.Debug);
                    return true;
                }

                if (axis.Any(a => a != null && (a.GetAlarm() || a.GetAstp())) || axis.All(a => a == null || (a.GetMdn() && !a.DriverCard.CheckHomeDone(a.AxisChannel))))
                {
                    axis.Stop();
                    task?.Log($"{err} alarm error", LogLevel.Error);
                    task?.ThrowException($"{err} alarm error");
                    return false;
                }

                //check task status
                if (task != null)
                {
                    if (task.RunningState != RunningState.Running && task.RunningState != RunningState.Resetting)
                    {
                        axis.Stop();
                        task.AbortIfCancel(nameof(Home));
                    }

                    if (task.RunningState == RunningState.Pause)
                    {
                        axis.Stop();
                        task.JoinIfPause();
                        task.AbortIfCancel(nameof(Home));
                        foreach (var a in axis)
                        {
                            a?.DriverCard.Home(a.AxisChannel, a.ToPls(a.HomeVm));
                        }
                    }
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            //home timeout
            task?.Log($"{err} timeout", LogLevel.Error);
            task?.ThrowException($"{err} timeout");
            return false;
        }


        public static bool MoveAbs(this IAxisEx axis, double pos, double vel, int timeout = -1)
        {
            return MoveAbs(axis, null, pos, vel, timeout);
        }

        public static bool MoveAbs(this IAxisEx[] axis, double[] pos, double[] vel, int timeout = -1)
        {
            return MoveAbs(axis, null, pos, vel, timeout);
        }


        public static bool MoveAbs(this IAxisEx axis, StationTask task, double pos, double vel, int timeout = -1, bool isCheckLimit = true)
        {
            return MoveAbs(new[] { axis }, task, new[] { pos }, new[] { vel }, timeout, isCheckLimit);
        }

        public static bool MoveAbsAsync(this IAxisEx[] axis, StationTask task, double[] pos, double[] vel)
        {
            if (axis.Any(a => a != null && (!a.GetServo() || a.GetAlarm())))
            {
                task?.ThrowException("MoveAbs Servo error");
                return false;
            }

            task?.AbortIfCancel(nameof(MoveAbsAsync));
            task?.JoinIfPause();

            //start move
            for (int i = 0; i < axis.Length; i++)
            {
                if (axis[i] != null)
                    axis[i].Error = string.Empty;
                axis[i]?.DriverCard.MoveAbs(axis[i].AxisChannel, axis[i].ToPls(pos[i]), axis[i].ToPls(vel[i]));
            }

            var err =
                $"{string.Join(",", axis.Select(a => a == null ? string.Empty : a.Name))} MoveAbs {string.Join(",", pos.Select(p => p.ToString("F2")))} {string.Join(",", vel.Select(p => p.ToString("F2")))}";
            task?.Log($"{err} success", LogLevel.Debug);

            return true;
        }

        public static bool MoveAbs(this IAxisEx[] axis, StationTask task, double[] pos, double[] vel, int timeout = -1, bool isCheckLimit = true)
        {
            if (axis.Any(a => a != null && (!a.GetServo() || a.GetAlarm())))
            {
                task?.ThrowException("MoveAbs Servo error");
                return false;
            }

            task?.AbortIfCancel(nameof(MoveAbs));
            task?.JoinIfPause();

            //start move
            for (int i = 0; i < axis.Length; i++)
            {
                if (axis[i] != null)
                    axis[i].Error = string.Empty;
                axis[i]?.DriverCard.MoveAbs(axis[i].AxisChannel, axis[i].ToPls(pos[i]), axis[i].ToPls(vel[i]));
            }

            var axisInps = axis.Select(a => a == null || a.GetInp()).ToArray();

            //wait done
            var err = $"{string.Join(",", axis.Select(a => a == null ? string.Empty : a.Name))} MoveAbs {string.Join(",", pos.Select(p => p.ToString("F2")))} {string.Join(",", vel.Select(p => p.ToString("F2")))}";
            var t = 0;
            timeout = timeout < 0 ? int.MaxValue : timeout;
            while (t++ <= timeout)
            {
                //check alarm
                if (axis.Any(a => a != null && (a.GetAlarm() || a.GetAstp() || isCheckLimit && (a.GetMel() || a.GetPel()))))
                {
                    axis.Stop();
                    var msg = $"{err} LIMIT: MEL {axis.Any(a => a != null && a.GetMel())} PEL {axis.Any(a => a != null && a.GetPel())} or ALARM {axis.Any(a => a != null && a.GetAlarm())} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }

                //check done
                if (axis.All(a => a == null
                                  || (axisInps[Array.IndexOf(axis, a)] && a.GetInp() && a.GetMdn())
                                  || (!axisInps[Array.IndexOf(axis, a)] && !a.GetInp() && a.GetMdn()))
                )
                {
                    var fail = axis.Any(IsMoveErrorHappen);
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {string.Join(",", axis.Select(a => a == null ? string.Empty : a.Error))}", LogLevel.Error);
                        task?.ThrowException(err);
                    }
                    else
                    {
                        task?.Log($"{err} success", LogLevel.Debug);
                    }

                    return !fail;
                }

                //check task status
                if (task != null)
                {
                    if (task.RunningState != RunningState.Running && task.RunningState != RunningState.Resetting)
                    {
                        axis.Stop();
                        task.AbortIfCancel(nameof(MoveAbs));
                    }

                    if (task.RunningState == RunningState.Pause)
                    {
                        axis.Stop();
                        task.JoinIfPause();
                        task.AbortIfCancel(nameof(MoveAbs));
                        for (int i = 0; i < axis.Length; i++)
                        {
                            axis[i]?.DriverCard.MoveAbs(axis[i].AxisChannel, axis[i].ToPls(pos[i]), axis[i].ToPls(vel[i]));
                        }
                    }
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            //MoveAbs timeout
            task?.Log($"{err} timeout", LogLevel.Error);
            task?.ThrowException($"{err} timeout");
            return false;
        }

        public static bool MoveRel(this IAxisEx[] axis, double[] pos, double[] vel, int timeout = -1)
        {
            return MoveRel(axis, null, pos, vel, timeout);
        }

        public static bool MoveRel(this IAxisEx axis, StationTask task, double step, double vel, int timeout = -1,
            bool isCheckLimit = true)
        {
            return MoveRel(new[] { axis }, task, new[] { step }, new[] { vel }, timeout, isCheckLimit);
        }

        public static bool MoveRel(this IAxisEx[] axis, StationTask task, double[] step, double[] vel, int timeout = -1, bool isCheckLimit = true)
        {
            if (axis.Any(a => a != null && (!a.GetServo() || a.GetAlarm())))
            {
                task?.ThrowException("MoveRel Servo error");
                return false;
            }

            task?.AbortIfCancel(nameof(MoveRel));
            task?.JoinIfPause();

            //start move
            int[] startpls = new int[axis.Length];
            int[] pausepls = new int[axis.Length];
            for (int i = 0; i < axis.Length; i++)
            {
                if (axis[i] != null)
                    axis[i].Error = string.Empty;
                axis[i]?.DriverCard.GetEncPos(axis[i].AxisChannel, ref startpls[i]);
                axis[i]?.DriverCard.MoveRel(axis[i].AxisChannel, axis[i].ToPls(step[i]), axis[i].ToPls(vel[i]));
            }

            var axisInps = axis.Select(a => a == null || a.GetInp()).ToArray();

            //wait done
            var err = $"{string.Join(",", axis.Select(a => a == null ? string.Empty : a.Name))} MoveRel {string.Join(",", step.Select(p => p.ToString("F2")))} {string.Join(",", vel.Select(p => p.ToString("F2")))}";
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ <= timeout)
            {
                //check alarm
                if (axis.Any(a => a != null && (a.GetAlarm() || a.GetAstp() || isCheckLimit && (a.GetMel() || a.GetPel()))))
                {
                    axis.Stop();
                    var msg = $"{err} LIMIT: MEL {axis.Any(a => a != null && a.GetMel())} PEL {axis.Any(a => a != null && a.GetPel())} or ALARM {axis.Any(a => a != null && a.GetAlarm())} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }

                if (axis.All(a => a == null
                                  || (axisInps[Array.IndexOf(axis, a)] && a.GetInp() && a.GetMdn())
                                  || (!axisInps[Array.IndexOf(axis, a)] && !a.GetInp() && a.GetMdn()))
                )
                {
                    var fail = axis.Any(IsMoveErrorHappen);
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {string.Join(",", axis.Select(a => a == null ? string.Empty : a.Error))}", LogLevel.Error);
                        task?.ThrowException(err);
                    }
                    else
                    {
                        task?.Log($"{err} success", LogLevel.Debug);
                    }

                    return !fail;
                }

                //check axis status
                if (task != null)
                {
                    if (task.RunningState != RunningState.Running && task.RunningState != RunningState.Resetting)
                    {
                        axis.Stop();
                        task.AbortIfCancel(nameof(MoveRel));
                    }

                    if (task.RunningState == RunningState.Pause)
                    {
                        axis.Stop();
                        for (int i = 0; i < axis.Length; i++)
                        {
                            axis[i]?.DriverCard.GetEncPos(axis[i].AxisChannel, ref pausepls[i]);
                        }

                        task.JoinIfPause();
                        task.AbortIfCancel(nameof(MoveRel));
                        for (int i = 0; i < axis.Length; i++)
                        {
                            axis[i]?.DriverCard.MoveRel(axis[i].AxisChannel, axis[i].ToPls(step[i]) - (pausepls[i] - startpls[i]), axis[i].ToPls(vel[i]));
                        }
                    }
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            //MoveRel timeout
            task?.Log($"{err} timeout", LogLevel.Error);
            task?.ThrowException($"{err} timeout");
            return false;
        }

        public static bool Jump(this IAxisEx[] axis, double[] pos, double[] vel, double jump = -50, int timeout = -1, bool isCheckLimit = true, double zMinLimit = 0)
        {
            return Jump(axis, null, pos, vel, jump, timeout, isCheckLimit, zMinLimit);
        }

        public static bool Jump(this IAxisEx[] axis, StationTask task, double[] pos, double[] vel, double jump = -50, int timeout = -1, bool isCheckLimit = true, double zMinLimit = 0)
        {
            if (pos == null)
            {
                task?.ThrowException("Jump Params Error");
                return false;
            }

            //Z Range Check
            if (axis[2] != null)
            {
                axis[2].Error = string.Empty;
                var curpos = axis[2].GetPos();
                if (curpos + jump < zMinLimit)
                {
                    var err = $"Jump Z:{jump} Over Range {curpos + jump} < {zMinLimit} error";
                    axis[2].Error = err;
                    task?.ThrowException(err);
                    task?.Log(err, LogLevel.Error);
                    return false;
                }

                var ret = MoveRel(axis[2], task, jump, vel[2], timeout, isCheckLimit);
                if (!ret)
                {
                    return false;
                }
            }


            if (axis.Length == 3)
            {
                //xyz platform
                var ret = MoveAbs(new[] { axis[0], axis[1] }, task, new[] { pos[0], pos[1] }, new[] { vel[0], vel[1] }, timeout, isCheckLimit);
                if (!ret)
                {
                    return false;
                }
            }
            else if (axis.Length == 4)
            {
                //xyzu platform
                var ret = MoveAbs(new[] { axis[0], axis[1], axis[3] }, task, new[] { pos[0], pos[1], pos[3] }, new[] { vel[0], vel[1], vel[3] }, timeout, isCheckLimit);
                if (!ret)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            if (axis[2] != null)
            {
                var ret = MoveAbs(axis[2], task, pos[2], vel[2], timeout, isCheckLimit);
                if (!ret)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsMoveErrorHappen(IAxisEx axis)
        {
            if (axis == null)
            {
                return false;
            }

            Thread.Sleep(1);
            var p1 = 0;
            axis.DriverCard.GetCommandPos(axis.AxisChannel, ref p1);
            var p2 = 0;
            axis.DriverCard.GetEncPos(axis.AxisChannel, ref p2);

            int limit = 200; //0.02mm
            if (p2 - p1 > limit || p1 - p2 > limit)
            {
                axis.Error = $"PLS ERROR {Math.Abs(p2 - p1)} > {limit}";
                return true;
            }

            return false;
        }
    }
}