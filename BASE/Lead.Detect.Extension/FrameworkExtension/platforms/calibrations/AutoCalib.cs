using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.platforms.calibrations
{
    public class AutoCalib : StationTask
    {
        public event Action<int> CalibProgressEvent;

        public event Action<bool> CalibFinishEvent;

        private bool _isNormalFinish;
        public string CalibInfo { get; set; }

        public List<string> DataList { get; set; }

        public AutoCalib() : base(0, "AutoCalib", null)
        {
            if (!Directory.Exists($@".\Calib"))
            {
                Directory.CreateDirectory(@".\Calib");
            }

            CalibInfo = GetType().Name;
            DataList = new List<string>();
        }

        protected override int ResetLoop()
        {
            UninitCalib();
            return 0;
        }

        protected override int RunLoop()
        {
            if (Station.RunningState != RunningState.WaitRun)
            {
                MessageBox.Show($"工站{Station.Name}未复位", CalibInfo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OnCalibFinishEvent(_isNormalFinish);
                return -1;
            }

            try
            {
                _isNormalFinish = true;

                CheckCalibParams();
                Log($"CALIBRATION RUN START...");
                ClearData();
                OnCalibProgress(1);

                InitCalib();
                OnCalibProgress(5);

                if (MessageBox.Show($"标定初始化完成. 开始{CalibInfo}标定?", CalibInfo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    ThrowException("Calib Cancelled");
                }

                DoCalib();

                OnCalibProgress(95);
                SaveData();
            }
            catch (Exception e)
            {
                _isNormalFinish = false;
                Log($"CALIBRATION RUN FAIL:{e.Message}");
                MessageBox.Show($"{CalibInfo}标定运行异常:{e.Message}");
            }
            finally
            {
                try
                {
                    UninitCalib();

                    Log($"CALIBRATION RUN SUCCESS");
                    DisplayOutput();
                }
                catch (Exception e)
                {
                    _isNormalFinish = false;
                    MessageBox.Show($"{CalibInfo}标定复位异常:{e.Message}");
                }

                OnCalibProgress(100);
                OnCalibFinishEvent(_isNormalFinish);
            }


            return -1;
        }

        private void CheckCalibParams()
        {
            var prop = this.GetType().GetProperties().ToList();

            var nullProp = prop.FindAll(p => p.GetValue(this) == null && !p.Name.StartsWith("Output"));
            if (nullProp.Count > 0)
            {
                throw new NullReferenceException(string.Join(",", nullProp.Select(p => p.Name)));
            }
        }

        public virtual void ClearData()
        {
            DataList.Clear();
        }

        public virtual void InitCalib()
        {
        }

        public virtual void DoCalib()
        {
        }

        public virtual void UninitCalib()
        {
        }

        public virtual void SaveData(string fileAppendix = null)
        {
            string file;
            if (fileAppendix != null)
            {
                file = $@".\Calib\{CalibInfo}-{fileAppendix}-{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}.dat";
            }
            else
            {
                file = $@".\Calib\{CalibInfo}-{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}.dat";
            }

            using (var fs = new FileStream(file, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    foreach (var data in DataList)
                    {
                        sw.WriteLine(data);
                    }
                }
            }
        }

        public virtual string DisplayOutput()
        {
            var sb = new StringBuilder();
            var props = this.GetType().GetProperties().ToList().FindAll(p => p.Name.StartsWith("Output") || p.GetCustomAttribute<CategoryAttribute>()?.Category == "OUTPUT");

            foreach (var p in props)
            {
                sb.AppendLine($"{p.Name,35}: {p.GetValue(this)}");
            }
            sb.AppendLine("\n-----------------Data--------------");
            foreach (var d in DataList)
            {
                sb.AppendLine(d);
            }
            sb.AppendLine("\n-----------------Data--------------");

            Log($"----------------------------------------------\n标定结果:\n{sb.ToString()}\n");

            return sb.ToString();

        }

        protected virtual void OnCalibProgress(int obj)
        {
            CalibProgressEvent?.Invoke(obj);
        }

        protected virtual void OnCalibFinishEvent(bool obj)
        {
            CalibFinishEvent?.Invoke(obj);
        }
    }
}