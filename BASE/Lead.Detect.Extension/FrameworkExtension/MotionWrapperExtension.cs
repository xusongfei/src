using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.Helper;
using Lead.Detect.FrameworkExtension;

namespace Lead.Detect.FrameworkExtension
{
    public static class MotionWrapperExtension
    {

        #region  vio

        public static bool GetVioSts(this EleVio vio, IMotionWrapper vioCard, bool status = true)
        {
            int sts;
            vioCard.GetDo(vio.Port, out sts);
            return (sts == 1) == status;
        }

        public static bool SetVio(this EleVio vio, StationTask task, IMotionWrapper vioCard, bool status = true)
        {
            vioCard.SetDo(vio.Port, status ? 1 : 0);
            task?.Log($"{vio.Name} SetVio {vio.Port} {status} success", LogLevel.Debug);
            return true;
        }

        public static bool WaitVio(this EleVio vio, StationTask task, IMotionWrapper vioCard, bool status = true, int timeout = -1)
        {
            timeout = timeout < 0 ? int.MaxValue : timeout;

            var err = $"{vio.Name} WaitVio {vio.Port} {status}";
            var t = 0;
            while (t++ < timeout)
            {
                var sts = 0;
                vioCard.GetDo(vio.Port, out sts);
                if (sts == 1 == status)
                {
                    task?.Log($"{err} success", LogLevel.Debug);
                    return true;
                }
                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel("WaitVio");
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

        public static bool WaitVioAndClear(this EleVio vio, StationTask task, IMotionWrapper vioCard, bool status = true, bool clear = false, int timeout = -1)
        {
            WaitVio(vio, task, vioCard, status, timeout);
            SetVio(vio, task, vioCard, clear);
            return true;
        }

        #endregion

        #region  cy

        public static void SetDoAsync(this EleCylinder[] cy, StationTask task, IMotionWrapper motion, bool status = true)
        {
            for (int i = 0; i < cy.Length; i++)
            {
                motion.SetDo(cy[i].DoOrg, status ? 0 : 1);
                motion.SetDo(cy[i].DoWork, status ? 1 : 0);
            }
        }

        public static bool SetDo(this EleCylinder[] cy, StationTask task, IMotionWrapper motion, bool status = true, int timeout = 3000, bool isErrorOnSignalError = true)
        {
            for (int i = 0; i < cy.Length; i++)
            {
                motion.SetDo(cy[i].DoOrg, status ? 0 : 1);
                motion.SetDo(cy[i].DoWork, status ? 1 : 0);
            }

            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

            var diOrg = cy.Select(c => c.DiOrg);
            var diWork = cy.Select(c => c.DiWork);
            var diOrgSts = cy.Select(c => !status);
            var diWorkSts = cy.Select(c => status);

            var waitdi = diOrg.Concat(diWork).ToArray();
            var waitSts = diOrgSts.Concat(diWorkSts).ToArray();

            var err = $"{string.Join(",", cy.Select(c => c.Name))} SetDo {status} {timeout}";
            var ret = WaitDi(waitdi, task, motion, waitSts, timeout);
            if (!ret)
            {
                task?.Log($"{err} error", isErrorOnSignalError ? LogLevel.Warning : LogLevel.Debug);
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Debug);
            }
            return ret;
        }

        public static bool SetDo(this EleCylinder cy, StationTask task, IMotionWrapper motion, bool status = true, int timeout = 3000, bool isErrorOnSignalError = true)
        {
            motion.SetDo(cy.DoOrg, status ? 0 : 1);
            motion.SetDo(cy.DoWork, status ? 1 : 0);

            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

            var err = $"{cy.Name} SetDo {status} {timeout}";
            var ret = WaitDi(new[] { cy.DiOrg, cy.DiWork }, task, motion, new[] { !status, status }, timeout);
            if (!ret)
            {
                task?.Log($"{err} error", isErrorOnSignalError ? LogLevel.Error : LogLevel.Debug);
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Debug);
            }
            return ret;
        }

        public static bool WaitDi(this EleCylinder cy, StationTask task, IMotionWrapper motion, bool status = true, int timeout = -1)
        {
            return WaitDi(new[] { cy.DiOrg, cy.DiWork }, task, motion, new[] { !status, status }, timeout);
        }

        #endregion

        #region  dio

        public static bool SetDo(this EleDo eledo, bool status = true)
        {
            if (eledo == null)
            {
                return false;
            }

            var motion = DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == eledo.Driver) as IMotionCard;
            motion?.WriteSingleDOutput(motion.DevIndex, 0, eledo.Port, status ? 1 : 0);
            return true;
        }

        public static bool Toggle(this EleDo eledo)
        {
            var motion = DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == eledo.Driver) as IMotionCard;
            int sts = 0;
            motion?.ReadSingleDOutput(motion.DevIndex, 0, eledo.Port, out sts);
            motion?.WriteSingleDOutput(motion.DevIndex, 0, eledo.Port, sts == 0 ? 1 : 0);
            return true;
        }



        public static void SetDo(this EleDo eledo, IMotionWrapper motion, bool status = true)
        {
            motion.SetDo(eledo.Port, status ? 1 : 0);
        }

        public static bool GetDiSts(this EleDi di, bool status = true)
        {
            var motion = DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == di.Driver) as IMotionCard;
            int sts = 0;
            motion?.ReadSingleDInput(motion.DevIndex, 0, di.Port, out sts);
            return (sts == 1) == status;
        }


        private static object _getdi = new object();
        public static bool GetDiSts(this EleDi eledi, IMotionWrapper motion, bool status = true)
        {
            lock (_getdi)
            {
                if (FrameworkExtenion.IsSimulate)
                {
                    return true;
                }
                int sts;
                motion.GetDi(eledi.Port, out sts);
                if (sts == 1 == status) return true;
                return false;
            }
        }

        public static bool WaitDi(this EleDi eledi, StationTask task, IMotionWrapper motion, bool status = true, int timeout = -1)
        {
            return WaitDi(eledi.Port, task, motion, status, timeout);
        }

        public static bool WaitDi(this int eledi, StationTask task, IMotionWrapper motion, bool status = true, int timeout = -1)
        {
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                var sts = 0;
                motion.GetDi(eledi, out sts);
                if (sts == 1 == status)
                {
                    return true;
                }
                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel("WaitDi");
                }
                else
                {
                    Application.DoEvents();
                }
                Thread.Sleep(1);
            }
            return false;

        }

        public static bool WaitDi(this int[] eledi, StationTask task, IMotionWrapper motion, bool[] status, int timeout = -1)
        {
            int[] sts = new int[eledi.Length];

            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                //check di status
                for (int i = 0; i < eledi.Length; i++)
                {
                    motion.GetDi(eledi[i], out sts[i]);
                }
                bool ret = true;
                for (int i = 0; i < eledi.Length; i++)
                {
                    ret = ret && ((sts[i] == 1) == status[i]);
                }
                if (ret)
                {
                    return true;
                }
                //check task status
                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel("WaitDi");
                }
                else
                {
                    Application.DoEvents();
                }
                Thread.Sleep(1);
            }
            return false;
        }


        #endregion

        #region  axis

        public static double GetPos(this EleAxis eleAxis)
        {
            var motion = DevPrimsManager.Instance.GetPrimByName(eleAxis.Driver) as IMotionCard;
            return ((int)(100 * GetPos(eleAxis, new MotionCardWrapper(motion)))) / 100d;
        }

        public static double[] GetPos(this EleAxis[] eleAxis)
        {
            return eleAxis.Select(GetPos).ToArray();
        }

        public static double GetPos(this EleAxis eleAxis, IMotionWrapper motion)
        {
            int pos = 0;
            motion.GetEncPos(eleAxis.AxisChannel, ref pos);
            return (int)(100 * eleAxis.ToMm(pos)) / 100d;
        }

        public static bool ServoEnable(this EleAxis eleAxis, StationTask task, IMotionWrapper motion, bool status)
        {
            if (motion.GetAxisEnable(eleAxis.AxisChannel))
            {
                motion.ServoEnable(eleAxis.AxisChannel, false);
                Thread.Sleep(300);
            }

            motion.ServoEnable(eleAxis.AxisChannel, status);

            var err = $"{eleAxis.Name} ServoEnable {status}";
            Thread.Sleep(1000);
            var ret = motion.GetAxisEnable(eleAxis.AxisChannel);
            if (!ret)
            {
                task?.Log($"{err} error", LogLevel.Error);
                task?.ThrowException($"{err} error");
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Info);
            }
            return ret;
        }

        public static bool ServoEnable(this EleAxis[] eleAxis, StationTask task, IMotionWrapper motion, bool status)
        {
            //start servo
            foreach (var ele in eleAxis)
            {
                if (motion.GetAxisEnable(ele.AxisChannel))
                {
                    motion.ServoEnable(ele.AxisChannel, false);
                    Thread.Sleep(300);
                }
                motion.ServoEnable(ele.AxisChannel, status);
            }

            //wait servo
            var err = $"{string.Join(",", eleAxis.Select(e => e.Name))} ServoEnable {status}";
            Thread.Sleep(500);
            var ret = eleAxis.All(e => motion.GetAxisEnable(e.AxisChannel));
            if (!ret)
            {
                task?.Log($"{err} error", LogLevel.Error);
                task?.ThrowException($"{err} error");
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Info);
            }
            return ret;
        }

        public static bool Home(this EleAxis eleAxis, StationTask task, IMotionWrapper motion, double vel, int timeout = -1)
        {
            //start home
            if (!motion.GetAxisEnable(eleAxis.AxisChannel))
            {
                task?.Log($"{eleAxis.Name} Home servo error", LogLevel.Error);
                task?.ThrowException("home servo error");
                return false;
            }
            motion.Home(eleAxis.AxisChannel, eleAxis.ToPls(eleAxis.HomeVm));

            //wait done
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                if (motion.CheckHomeDone(eleAxis.AxisChannel))
                {
                    task?.Log($"{eleAxis.Name} Home success", LogLevel.Debug);
                    return true;
                }
                if (motion.GetAxisAlarm(eleAxis.AxisChannel))
                {
                    task?.Log($"{eleAxis.Name} Home alarm", LogLevel.Error);
                    task?.ThrowException($"{eleAxis.Name} Home alarm");
                    return false;
                }
                //check task status
                if (task != null)
                {
                    if (!task.IsRunning)
                    {
                        motion.MoveStop(eleAxis.AxisChannel);
                    }
                    if (task.IsPause)
                    {
                        motion.MoveStop(eleAxis.AxisChannel);
                        task.JoinIfPause();
                        motion.Home(eleAxis.AxisChannel, eleAxis.ToPls(eleAxis.HomeVm));
                    }
                    task.AbortIfCancel("Home");
                }
                else
                {
                    Application.DoEvents();
                }
                Thread.Sleep(1);
            }
            //home timeout
            task?.Log($"{eleAxis.Name} Home timeout", LogLevel.Error);
            task?.ThrowException($"{eleAxis.Name} Home timeout");
            return false;
        }

        public static bool Home(this EleAxis[] eleAxis, StationTask task, IMotionWrapper motion, double[] vel, int timeout = -1)
        {
            if (eleAxis.Any(e => !motion.GetAxisEnable(e.AxisChannel)))
            {
                task?.ThrowException("home servo error");
                return false;
            }

            //start home
            foreach (var a in eleAxis)
            {
                motion.Home(a.AxisChannel, a.ToPls(a.HomeVm));
            }

            //wait home done
            var err = $"{string.Join(",", eleAxis.Select(e => e.Name))} Home";
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                if (eleAxis.All(a => motion.CheckHomeDone(a.AxisChannel)))
                {
                    task?.Log($"{err} success", LogLevel.Debug);
                    return true;
                }
                if (eleAxis.Any(e => motion.GetAxisAlarm(e.AxisChannel)))
                {
                    task?.Log($"{err} alarm error", LogLevel.Error);
                    task?.ThrowException($"{err} alarm error");
                    return false;
                }
                //check task status
                if (task != null)
                {
                    if (!task.IsRunning)
                    {
                        foreach (var a in eleAxis)
                        {
                            motion.MoveStop(a.AxisChannel);
                        }
                    }
                    if (task.IsPause)
                    {
                        foreach (var a in eleAxis)
                        {
                            motion.MoveStop(a.AxisChannel);
                        }
                        task.JoinIfPause();
                        foreach (var a in eleAxis)
                        {
                            motion.Home(a.AxisChannel, a.ToPls(a.HomeVm));
                        }
                    }
                    task.AbortIfCancel("Home");
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


        public static bool MoveAbs(this EleAxis eleAxis, double pos, double vel, int timeout = -1)
        {
            var motion = DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == eleAxis.Driver);
            return MoveAbs(eleAxis, null, new MotionCardWrapper(motion as IMotionCard), pos, vel, timeout);
        }

        public static bool MoveAbs(this EleAxis[] eleAxis, double[] pos, double[] vel, int timeout = -1)
        {
            var motion = eleAxis.Select(e => DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == e.Driver)).ToArray();
            return MoveAbs(eleAxis, null, new MotionCardWrapper(motion[0] as IMotionCard), pos, vel, timeout);
        }


        public static bool MoveAbs(this EleAxis eleAxis, StationTask task, IMotionWrapper motion, double pos, double vel, int timeout = -1, bool isCheckLimit = true)
        {
            if (!motion.GetAxisEnable(eleAxis.AxisChannel))
            {
                task?.ThrowException("MoveAbs servo error");
                return false;
            }
            //start move
            motion.MoveAbs(eleAxis.AxisChannel, eleAxis.ToPls(pos), eleAxis.ToPls(vel));

            //wait done
            var err = $"{eleAxis.Name} MoveAbs {pos:F2} {vel:F2}";
            var t = 0;
            timeout = timeout < 0 ? int.MaxValue : timeout;
            while (t++ < timeout)
            {
                if (motion.GetAxisAlarm(eleAxis.AxisChannel) || isCheckLimit && motion.CheckLimit(eleAxis.AxisChannel))
                {
                    var msg = $"{err} limit MEL {motion.LimitMel(eleAxis.AxisChannel)} PEL {motion.LimitPel(eleAxis.AxisChannel)} or alarm {motion.GetAxisAlarm(eleAxis.AxisChannel)} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }
                if (motion.CheckMoveDone(eleAxis.AxisChannel))
                {
                    var fail = IsMoveErrorHappen(eleAxis, motion);
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {eleAxis.Error}", LogLevel.Error);
                        task?.ThrowException($"{err} move pulse error {eleAxis.Error}");
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
                    if (!task.IsRunning)
                    {
                        motion.MoveStop(eleAxis.AxisChannel);
                    }
                    if (task.IsPause)
                    {
                        motion.MoveStop(eleAxis.AxisChannel);
                        task.JoinIfPause();
                        motion.MoveAbs(eleAxis.AxisChannel, eleAxis.ToPls(pos), eleAxis.ToPls(vel));
                    }
                    task.AbortIfCancel("cancel moveabs");
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

        public static bool MoveAbs(this EleAxis[] eleAxis, StationTask task, IMotionWrapper motion, double[] pos, double[] vel, int timeout = -1, bool isCheckLimit = true)
        {
            if (eleAxis.Any(e => !motion.GetAxisEnable(e.AxisChannel)))
            {
                task?.ThrowException("MoveAbs GetAxisEnable error");
                return false;
            }

            //start move
            for (int i = 0; i < eleAxis.Length; i++)
            {
                motion.MoveAbs(eleAxis[i].AxisChannel, eleAxis[i].ToPls(pos[i]), eleAxis[i].ToPls(vel[i]));
            }

            //wait done
            var err = $"{string.Join(",", eleAxis.Select(e => e.Name))} MoveAbs {string.Join(",", pos.Select(p => p.ToString("F2")))} {string.Join(",", vel.Select(p => p.ToString("F2")))}";
            var t = 0;
            timeout = timeout < 0 ? int.MaxValue : timeout;
            while (t++ < timeout)
            {
                //check alarm
                if (eleAxis.Any(a => motion.GetAxisAlarm(a.AxisChannel) || isCheckLimit && motion.CheckLimit(a.AxisChannel)))
                {
                    var msg = $"{err} limit {eleAxis.Any(a => motion.CheckLimit(a.AxisChannel))} or alarm {eleAxis.Any(a => motion.GetAxisAlarm(a.AxisChannel))} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }
                //check done
                if (eleAxis.All(a => motion.CheckMoveDone(a.AxisChannel)))
                {
                    var fail = eleAxis.Any(a => IsMoveErrorHappen(a, motion));
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {string.Join(",", eleAxis.Select(e => e.Error))}", LogLevel.Error);
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
                    if (!task.IsRunning)
                    {
                        foreach (var a in eleAxis)
                        {
                            motion.MoveStop(a.AxisChannel);
                        }
                    }
                    if (task.IsPause)
                    {
                        foreach (var a in eleAxis)
                        {
                            motion.MoveStop(a.AxisChannel);
                        }
                        task.JoinIfPause();
                        for (int i = 0; i < eleAxis.Length; i++)
                        {
                            motion.MoveAbs(eleAxis[i].AxisChannel, eleAxis[i].ToPls(pos[i]), eleAxis[i].ToPls(vel[i]));
                        }
                    }
                    task.AbortIfCancel("MoveAbs");
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

        public static bool MoveRel(this EleAxis eleAxis, StationTask task, IMotionWrapper motion, double step, double vel, int timeout = -1, bool isCheckLimit = true)
        {
            if (!motion.GetAxisEnable(eleAxis.AxisChannel))
            {
                task?.ThrowException("MoveRel servo error");
                return false;
            }
            //start move
            int startpls = 0;
            int pausepls = 0;
            motion.GetEncPos(eleAxis.AxisChannel, ref startpls);
            motion.MoveRel(eleAxis.AxisChannel, eleAxis.ToPls(step), eleAxis.ToPls(vel));

            //wait done
            var err = $"{eleAxis.Name} MoveRel {step:F2} {vel:F2}";
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                if (motion.GetAxisAlarm(eleAxis.AxisChannel) || isCheckLimit && motion.CheckLimit(eleAxis.AxisChannel))
                {
                    var msg = $"{err} limit MEL {motion.LimitMel(eleAxis.AxisChannel)} PEL {motion.LimitPel(eleAxis.AxisChannel)} or alarm {motion.GetAxisAlarm(eleAxis.AxisChannel)} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }
                if (motion.CheckMoveDone(eleAxis.AxisChannel))
                {
                    var fail = IsMoveErrorHappen(eleAxis, motion);
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {eleAxis.Error}", LogLevel.Error);
                        task?.ThrowException($"{err} move pulse error {eleAxis.Error}");
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
                    if (!task.IsRunning)
                    {
                        motion.MoveStop(eleAxis.AxisChannel);
                    }
                    if (task.IsPause)
                    {
                        motion.MoveStop(eleAxis.AxisChannel);
                        motion.GetEncPos(eleAxis.AxisChannel, ref pausepls);
                        task.JoinIfPause();
                        motion.MoveRel(eleAxis.AxisChannel, eleAxis.ToPls(step) - (pausepls - startpls), eleAxis.ToPls(vel));
                    }
                    task.AbortIfCancel("MoveRel");
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

        /// <summary>
        /// motion for tuning usage
        /// </summary>
        /// <param name="eleAxis"></param>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool MoveRel(this EleAxis[] eleAxis, double[] pos, double[] vel, int timeout = -1)
        {
            var motion = eleAxis.Select(e => DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == e.Driver)).ToArray();
            return MoveRel(eleAxis, null, new MotionCardWrapper(motion[0] as IMotionCard), pos, vel, timeout);
        }

        public static bool MoveRel(this EleAxis[] eleAxis, StationTask task, IMotionWrapper motion, double[] step, double[] vel, int timeout = -1)
        {
            if (eleAxis.Any(e => !motion.GetAxisEnable(e.AxisChannel)))
            {
                task?.ThrowException("MoveRel servo error");
                return false;
            }
            //start move
            int[] startpls = new int[eleAxis.Length];
            int[] pausepls = new int[eleAxis.Length];
            for (int i = 0; i < eleAxis.Length; i++)
            {
                motion.GetEncPos(eleAxis[i].AxisChannel, ref startpls[i]);
                motion.MoveRel(eleAxis[i].AxisChannel, eleAxis[i].ToPls(step[i]), eleAxis[i].ToPls(vel[i]));
            }

            //wait done
            var err = $"{string.Join(",", eleAxis.Select(e => e.Name))} MoveRel {string.Join(",", step.Select(p => p.ToString("F2")))} {string.Join(",", vel.Select(p => p.ToString("F2")))}";
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                //check alarm
                if (eleAxis.Any(a => motion.GetAxisAlarm(a.AxisChannel) || motion.CheckLimit(a.AxisChannel)))
                {
                    var msg = $"{err} limit {eleAxis.Any(a => motion.CheckLimit(a.AxisChannel))} or alarm {eleAxis.Any(a => motion.GetAxisAlarm(a.AxisChannel))} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }
                if (eleAxis.All(a => motion.CheckMoveDone(a.AxisChannel)))
                {
                    var fail = eleAxis.Any(a => IsMoveErrorHappen(a, motion));
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {string.Join(",", eleAxis.Select(e => e.Error))}", LogLevel.Error);
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
                    if (!task.IsRunning)
                    {
                        foreach (var a in eleAxis)
                        {
                            motion.MoveStop(a.AxisChannel);
                        }
                    }
                    if (task.IsPause)
                    {
                        for (int i = 0; i < eleAxis.Length; i++)
                        {
                            motion.MoveStop(eleAxis[i].AxisChannel);
                            motion.GetEncPos(eleAxis[i].AxisChannel, ref pausepls[i]);
                        }
                        task.JoinIfPause();
                        for (int i = 0; i < eleAxis.Length; i++)
                        {
                            motion.MoveRel(eleAxis[i].AxisChannel, eleAxis[i].ToPls(step[i]) - (pausepls[i] - startpls[i]), eleAxis[i].ToPls(vel[i]));
                        }
                    }
                    task.AbortIfCancel("cancel moverel");
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

        public static bool Jump(this EleAxis[] axis, double[] pos, double[] vel, double jump = -50, int timeout = -1, bool isCheckLimit = true, double zMinLimit = 0)
        {
            var motion = new MotionCardWrapper(DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == axis[0].Driver) as IMotionCard);
            return Jump(axis, null, motion, pos, vel, jump, timeout, isCheckLimit, zMinLimit);
        }

        public static bool Jump(this EleAxis[] axis, StationTask task, IMotionWrapper motion, double[] pos, double[] vel, double jump = -50, int timeout = -1, bool isCheckLimit = true, double zMinLimit = 0)
        {
            if (pos == null)
            {
                task?.ThrowException("Jump Params Error");
                return false;
            }

            axis[2].Error = string.Empty;
            var curpos = axis[2].GetPos(motion);
            if (curpos + jump < zMinLimit)
            {
                var err = $"Jump Z Over Range {curpos + jump} < {zMinLimit} error";
                axis[2].Error = err;
                task?.ThrowException(err);
                task?.Log(err, LogLevel.Error);
                return false;
            }
            else
            {
                axis[2].MoveRel(task, motion, jump, vel[2], timeout, isCheckLimit);
            }

            new[] { axis[0], axis[1] }.MoveAbs(task, motion, new[] { pos[0], pos[1] }, new[] { vel[0], vel[1] }, timeout, isCheckLimit);
            axis[2].MoveAbs(task, motion, pos[2], vel[2], timeout, isCheckLimit);
            return true;
        }

        private static bool IsMoveErrorHappen(EleAxis eleAxis, IMotionWrapper motion)
        {
            Thread.Sleep(50);
            var p1 = 0;
            motion.GetCommandPos(eleAxis.AxisChannel, ref p1);
            var p2 = 0;
            motion.GetEncPos(eleAxis.AxisChannel, ref p2);

            int limit = 1000;
            if (p2 - p1 > limit || p1 - p2 > limit)
            {
                eleAxis.Error = $"PLS ERROR {Math.Abs(p2 - p1)} > {limit}";
                return true;
            }
            return false;
        }

        #endregion
    }
}