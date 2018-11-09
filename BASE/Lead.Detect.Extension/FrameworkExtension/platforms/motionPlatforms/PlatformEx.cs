using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.safeCheckObjects;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.Helper;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    public class PlatformEx : IElement
    {
        public IAxisEx AX => Axis[0];
        public IAxisEx AY => Axis[1];
        public IAxisEx AZ => Axis[2];
        public IAxisEx AU => Axis[3];
        public IAxisEx AV => Axis[4];
        public IAxisEx AW => Axis[5];

        public PlatformEx()
        {
        }

        /// <summary>
        /// Manual Platform Ctor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="axis"></param>
        /// <param name="positions"></param>
        public PlatformEx(string name, IAxisEx[] axis, List<IPlatformPos> positions)
        {
            _isAutoMode = false;
            Name = name;
            Task = null;

            Axis = axis;

            Positions = positions;
        }


        /// <summary>
        /// StationTask Platform Ctor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="axis"></param>
        /// <param name="task"></param>
        /// <param name="positions"></param>
        public PlatformEx(string name, IAxisEx[] axis, StationTask task, List<IPlatformPos> positions)
        {
            _isAutoMode = true;
            Name = name;
            Task = task;

            Axis = axis;

            Positions = positions;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        private bool _isAutoMode;
        private StationTask _task;

        public StationTask Task
        {
            get { return _isAutoMode ? _task : null; }
            protected set { _task = value; }
        }

        public IAxisEx[] Axis { get; protected set; }

        public double[] Vel
        {
            get { return Axis != null ? Axis.Select(a => a == null ? 0 : a.AxisSpeed).ToArray() : new[] { 0d }; }
        }

        public double[] Acc
        {
            get { return Axis != null ? Axis.Select(a => a == null ? 0 : a.AxisAcc).ToArray() : new[] { 0d }; }
        }

        public List<IPlatformPos> Positions { get; protected set; }


        /// <summary>
        /// indexer of axis
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IAxisEx this[int index]
        {
            get { return Axis[index]; }
        }

        /// <summary>
        /// indexer of positions
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public IPlatformPos this[string pos]
        {
            get { return Positions.FirstOrDefault(p => p.Name == pos); }
        }

        public double[] CurPos
        {
            get { return Axis.GetPos(); }
        }

        public int[] CurPulse
        {
            get { return Axis.GetPulse(); }
        }

        public virtual Type PosType
        {
            get { return typeof(IPlatformPos); }
        }


        public List<SafeCheckObject> SafeChecks { get; } = new List<SafeCheckObject>();
        public string LastSafeCheckError { get; set; }


        /// <summary>
        /// 加载点位文件
        /// </summary>
        /// <param name="file"></param>
        public void Load(string file = null)
        {
            file = file ?? $@".\Config\{Name}.dat";
            if (!File.Exists(file))
            {
                Positions = new List<IPlatformPos>();
            }
            else
            {
                //Positions = XmlSerializerHelper.ReadXML(file, Positions.GetType()) as List<IPlatformPos>;
                LoadPts(file);
            }
        }


        /// <summary>
        /// 保存点位文件
        /// </summary>
        /// <param name="file"></param>
        public void Save(string file = null)
        {
            file = file ?? $@".\Config\{Name}.dat";
            //XmlSerializerHelper.WriteXML(Positions, file);
            SavePts(file);
        }

        public void LoadPts(string file)
        {
            using (var fs = new FileStream(file, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    //find platform pos start line
                    var line = sr.ReadLine();
                    if (line != null && line.StartsWith(Name))
                    {
                        var propCount = PosType.GetProperties().Length;

                        //start read pos lines
                        line = sr.ReadLine();
                        while (!string.IsNullOrEmpty(line))
                        {
                            var data = line.Split(',');
                            if (data.Length == propCount)
                            {
                                if (PosType == typeof(PosXYZ))
                                {
                                    Positions.Add(PosXYZ.Create(line));
                                }
                                else if (PosType == typeof(PosXYZU))
                                {
                                    Positions.Add(PosXYZU.Create(line));
                                }
                                else if (PosType == typeof(PosXYZUVW))
                                {
                                    Positions.Add(PosXYZUVW.Create(line));
                                }
                                else
                                {
                                    //pos type error
                                }
                            }
                            else
                            {
                                //pos prop length error
                                MessageBox.Show($"{line} Props Count Error");
                            }

                            line = sr.ReadLine();
                        }
                    }

                }
            }
        }


        public void SavePts(string file)
        {
            using (var fs = new FileStream(file, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"{Name}");
                    foreach (var p in Positions)
                    {
                        sw.WriteLine(p.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 平台运动前安全检查
        /// </summary>
        /// <param name="i"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool RunSafeCheck(SafeCheckType target = SafeCheckType.Always, int i = -1)
        {
            LastSafeCheckError = string.Empty;

            if (SafeChecks.Count > 0)
            {
                var sc = SafeChecks.FindAll(s => s.Enable && s.Target == SafeCheckType.Always || s.Target == target);
                if (sc.Count > 0)
                {
                    var ret = sc.All(s => s.Check(this, i));
                    LastSafeCheckError = string.Join(",", sc.Select(s => s.Error));

                    //throw exception on task thread
                    if (_isAutoMode && !ret)
                    {
                        Task?.ThrowException($"{Name} {LastSafeCheckError}");
                    }
                    return ret;
                }
            }

            if (Task != null)
            {
                AssertAutoMode(Task);
            }
            return true;
        }

        /// <summary>
        /// must call in stationtask
        /// </summary>
        /// <returns></returns>
        public PlatformEx EnterAuto(StationTask task = null)
        {
            if (task != null)
            {
                Task = task;
            }

            foreach (var sc in SafeChecks)
            {
                sc.Enable = true;
            }

            _isAutoMode = true;
            return this;
        }

        /// <summary>
        /// must call int manual operation
        /// </summary>
        /// <returns></returns>
        public PlatformEx ExitAuto()
        {
            _isAutoMode = false;
            return this;
        }

        public bool AssertAutoMode(StationTask task)
        {
            if (!_isAutoMode)
            {
                task.Log($"{Name} not in Auto Mode", LogLevel.Error);
                return false;
            }
            return true;
        }

        public bool AssertPosTeached(string pos, StationTask task)
        {
            if (this[pos] == null)
            {
                task?.Log($"{Name} {pos} not teached", LogLevel.Error);
                return false;
            }
            return true;
        }

        #region  motion

        public bool Servo(int i = -1, bool status = true)
        {
            if (i >= 0)
            {
                return Axis[i] != null && Axis[i].ServoEnable(Task, status);
            }
            else
            {
                return Axis.ServoEnable(Task, status);
            }
        }

        /// <summary>
        /// home by axis index array
        /// </summary>
        /// <param name="order"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool Home(int[] order, int timeout)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.ManualHome : SafeCheckType.AutoHome))
            {
                return false;
            }

            var orderMin = order.Min();
            var orderMax = order.Max();
            for (int ord = orderMin; ord <= orderMax; ord++)
            {
                var homeAxis = new List<IAxisEx>();
                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i] == ord && Axis[i] != null)
                    {
                        homeAxis.Add(Axis[i]);
                    }
                }

                if (homeAxis.Count > 0)
                {
                    var homeVm = homeAxis.Select(a => a.HomeVm).ToArray();
                    var ret = homeAxis.ToArray().Home(Task, homeVm, timeout);
                    if (!ret)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// home by axis home order
        /// </summary>
        /// <param name="i"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool Home(int i = -1, int timeout = -1)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.ManualHome : SafeCheckType.AutoHome, i))
            {
                return false;
            }

            if (i >= 0)
            {
                return Axis[i] != null && Axis[i].Home(Task, Axis[i].HomeVm, timeout);
            }
            else
            {
                //get axis home order range
                var axis = Axis.ToList().FindAll(a => a != null);
                if (axis.Count == 0)
                {
                    return true;
                }

                var order = axis.Select(a => a.HomeOrder).ToArray();
                var orderMin = order.Min();
                var orderMax = order.Max();

                //home axis from min to max
                for (int ord = orderMin; ord <= orderMax; ord++)
                {
                    var homeAxis = axis.FindAll(a => a.HomeOrder == ord);
                    if (homeAxis.Count > 0)
                    {
                        var homeVm = homeAxis.Select(a => a.HomeVm).ToArray();
                        var ret = homeAxis.ToArray().Home(Task, homeVm, timeout);
                        if (!ret)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public void Stop()
        {
            Axis.Stop();
        }


        /// <summary>
        /// calculate move timeout ms on axis speed
        /// </summary>
        /// <param name="step"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public double GetMoveTimeout(double[] step, int i = -1)
        {
            var vel = Vel;
            var axis = Axis;
            var timeout = -1d;

            if (i < 0)
            {
                for (var s = 0; s < step.Length && s < Vel.Length; s++)
                {
                    if (axis[s] != null)
                    {
                        var t = Math.Abs(step[s]) / vel[s];
                        if (t > timeout)
                        {
                            timeout = t;
                        }
                    }
                }
            }
            else
            {
                if (axis[i] != null)
                {
                    timeout = step[i] / Vel[i];
                }
                else
                {
                    timeout = -1;
                }
            }
            return timeout;
        }


        public bool MoveRel(int i, double step, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto, i))
            {
                return false;
            }

            return Axis[i] != null && Axis[i].MoveRel(Task, step, Axis[i].AxisSpeed, timeout, checkLimit);
        }

        public bool MoveAbs(int i, double pos, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto, i))
            {
                return false;
            }

            return Axis[i] != null && Axis[i].MoveAbs(Task, pos, Axis[i].AxisSpeed, timeout, checkLimit);
        }

        public bool MoveAbs(int i, IPlatformPos pos, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto, i))
            {
                return false;
            }

            return Axis[i] != null && Axis[i].MoveAbs(Task, pos.Data()[i], Axis[i].AxisSpeed, timeout, checkLimit);
        }

        public bool MoveAbs(int i, string position, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto, i))
            {
                return false;
            }

            var pos = Positions.FirstOrDefault(p => p.Name == position);
            if (pos == null)
            {
                throw new Exception($"MoveAbs Position {position} not Exist");
            }


            return Axis[i] != null && Axis[i].MoveAbs(Task, pos.Data()[i], Axis[i].AxisSpeed, timeout, checkLimit);
        }

        public bool MoveAbs(string position, double[] vel = null, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            var pos = Positions.FirstOrDefault(p => p.Name == position);
            if (pos == null)
            {
                throw new Exception($"MoveAbs Position {position} not Exist");
            }

            return Axis.MoveAbs(Task, pos.Data(), vel ?? Vel, timeout, checkLimit);
        }

        public bool MoveAbs(double[] pos, double[] vel = null, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            return Axis.MoveAbs(Task, pos, vel ?? Vel, timeout, checkLimit);
        }

        public bool MoveAbs(IPlatformPos pos, double[] vel = null, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            return Axis.MoveAbs(Task, pos.Data(), vel ?? Vel, timeout, checkLimit);
        }

        public bool MoveAbsAsync(IPlatformPos pos, double[] vel = null)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            return Axis.MoveAbsAsync(Task, pos.Data(), vel ?? Vel);
        }

        public bool MoveAbs(string position, int[] order, double[] vel = null, int timeout = -1, bool checkLimit = true)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            var pos = Positions.FirstOrDefault(p => p.Name == position);
            if (pos == null)
            {
                throw new Exception($"MoveAbs Position {position} not Exist");
            }

            if (order.Length != Axis.Length || order.Min() != 0 || order.Max() != Axis.Length - 1)
            {
                throw new Exception($"MoveAbs Position {string.Join(",", order)} order error");
            }

            for (int i = 0; i < order.Length; i++)
            {
                var index = Array.IndexOf(order, i);
                var ret = MoveAbs(index, pos.Data()[index], timeout, checkLimit);
                if (!ret)
                {
                    return false;
                }
            }

            return true;
        }


        public bool Jump(string position, double jumpHeight = 0, double[] vel = null, int timeout = -1, bool checkLimit = true, double zLimit = 0d)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            var pos = Positions.FirstOrDefault(p => p.Name == position);
            if (pos == null)
            {
                throw new Exception($"Jump Position {position} not Exist");
            }

            return Axis.Jump(Task, pos.Data(), vel ?? Vel, jumpHeight, timeout, checkLimit, zLimit);
        }

        public bool Jump(double[] pos, double jumpHeight = 0, double[] vel = null, int timeout = -1, bool checkLimit = true, double zLimit = 0d)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            return Axis.Jump(Task, pos, vel ?? Vel, jumpHeight, timeout, checkLimit, zLimit);
        }

        public bool Jump(IPlatformPos pos, double jumpHeight = 0, double[] vel = null, int timeout = -1, bool checkLimit = true, double zLimit = 0d)
        {
            if (!RunSafeCheck(Task == null ? SafeCheckType.Manual : SafeCheckType.Auto))
            {
                return false;
            }

            return Axis.Jump(Task, pos.Data(), vel ?? Vel, jumpHeight, timeout, checkLimit, zLimit);
        }

        #endregion

        #region  position

        public Dictionary<string, Func<double[], double[]>> PosConvertFuncs { get; set; } = new Dictionary<string, Func<double[], double[]>>();

        /// <summary>
        /// get or convert specific type pos
        /// </summary>
        /// <param name="posConvertType"></param>
        /// <param name="pos"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        public virtual double[] GetPos(string posConvertType = null, double[] pos = null)
        {
            if (pos == null)
            {
                //get pos from platform move pos
                if (string.IsNullOrEmpty(posConvertType))
                {
                    return CurPos;
                }
                else if (PosConvertFuncs.ContainsKey(posConvertType))
                {
                    return PosConvertFuncs[posConvertType].Invoke(CurPos);
                }
                else
                {
                    throw new NotSupportedException("GetPos");
                }
            }
            else
            {
                //conver pos to platform move pos
                if (string.IsNullOrEmpty(posConvertType))
                {
                    //no conversion
                    return pos;
                }
                else if (PosConvertFuncs.ContainsKey(posConvertType))
                {
                    return PosConvertFuncs[posConvertType].Invoke(pos);
                }
                else
                {
                    throw new NotSupportedException("GetPos");
                }
            }
        }

        /// <summary>
        /// 示教点函数
        /// </summary>
        /// <param name="name"></param>
        public virtual void TeachPos(string name)
        {
            throw new NotImplementedException("TeachPos");
        }

        /// <summary>
        /// 检查该点存在，且数量是1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool ExistsPos(string name)
        {
            if (Positions.Exists(p => p.Name == name))
            {
                return Positions.Count(p => p.Name == name) == 1;
            }

            return true;
        }


        /// <summary>
        /// 检查平台是否在点位位置
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="i">axis id</param>
        /// <returns></returns>
        public virtual bool LocateInPos(string pos, int i = -1)
        {
            if (i < 0)
            {
                if (Axis.Any(a => a != null && (!a.GetServo() || a.GetAlarm() || a.GetAstp() || !a.GetMdn())))
                {
                    return false;
                }

                var p = this[pos];
                if (p == null)
                {
                    return false;
                }

                if (PosType == typeof(PosXYZ))
                {
                    if (new PosXYZ(CurPos).DistanceTo(p) > 1)
                    {
                        return false;
                    }
                }
                else if (PosType == typeof(PosXYZU))
                {
                    if (new PosXYZU(CurPos).DistanceTo(p) > 1)
                    {
                        return false;
                    }
                }
                else if (PosType == typeof(PosXYZUVW))
                {
                    if (new PosXYZUVW(CurPos).DistanceTo(p) > 1)
                    {
                        return false;
                    }
                }
                else
                {
                    throw new NotSupportedException($"{PosType} not support for LocateInPos");
                    //return false;
                }

                return true;
            }
            else
            {
                if (Axis[i] == null)
                {
                    return false;
                }

                if (Axis[i] != null && (!Axis[i].GetServo() || Axis[i].GetAlarm() || Axis[i].GetAstp() || !Axis[i].GetMdn()))
                {
                    if (!Axis[i].GetMdn())
                    {
                        MessageBox.Show("GetMdn");
                    }
                    return false;
                }

                var p = this[pos];
                if (p == null)
                {
                    return false;
                }

                if (Math.Abs(p.Data()[i] - CurPos[i]) > 1)
                {
                    return false;
                }

                return true;
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{Name} {GetType().Name} {Description}";
        }


        public string ShowStatus()
        {
            return
                $"\n{Name} {GetType().Name} {Description}\n" +
                $"使  能 {Axis.All(a => a == null || a.GetServo())}\n" +
                $"报  警 {Axis.Any(a => a != null && a.GetAlarm())}\n" +
                $"正限位 {Axis.Any(a => a != null && a.GetPel())}\n" +
                $"负限位 {Axis.Any(a => a != null && a.GetMel())}\n" +
                $"安  全 {LastSafeCheckError}";
        }

        public string Export()
        {
            return $"{Name} {GetType().Name} {Description} BEGIN\r\n\t\t{string.Join("\r\n\t\t", Axis.Select(a => a == null ? "NULL" : $"{a.AxisChannel} {a.Export()}"))}\r\n\t{Name} END";
        }

        void IElement.Import(string line, StateMachine machine)
        {
            Import(line, machine);
        }

        public static void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            int i = 0;

            var id = int.Parse(data[i++]);
            var name = data[i++];
            var typeName = data[i++];
            var desc = data[i++];

            PlatformEx platform = null;
            switch (typeName)
            {
                case "PlatformXyz":
                    platform = new PlatformXyz()
                    {
                        Positions = new List<IPlatformPos>()
                    };
                    break;
                case "PlatformXyzu":
                    platform = new PlatformXyzu()
                    {
                        Positions = new List<IPlatformPos>()
                    };
                    break;

                case "PlatformXyzuvw":
                    platform = new PlatformXyzuvw()
                    {
                        Positions = new List<IPlatformPos>()
                    };
                    break;
                default:
                    throw new FormatException();
            }

            platform.Name = name;
            platform.Description = desc;

            if (machine.Platforms.ContainsKey(id))
            {
                return;
            }
            machine.Platforms.Add(id, platform);
        }
    }
}