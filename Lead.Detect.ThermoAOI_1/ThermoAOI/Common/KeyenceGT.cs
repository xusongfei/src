using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.ThermoAOI.Machine1.Common
{
    public class KeyenceGT : TcpClient
    {

        public new void Connect(string ip, int port)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }
            if (Connected)
            {
                return;
            }
            base.Connect(ip, port);
        }


        private object _readLock = new object();

        public double[] ReadData(int delay = 20)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return new[] { 0.1, 0.2, 0.3 };
            }

            var startcount = _sw.ElapsedMilliseconds;
            while (_sw.ElapsedMilliseconds - startcount < delay)
            {
                Thread.SpinWait(100);
            }

            return QueryGtValue();
        }

        private Stopwatch _sw = new Stopwatch();
        private StationTask _task;
        private double[] GtValue = new[] { 0d, 0d, 0d };
        public void RunGtService(StationTask t)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }

            if (_task != null)
            {
                return;
            }

            _task = t;

            if (!Connected)
            {
                _task.Log($"RunGtService error", LogLevel.Error);
                return;
            }


            Task.Run(() =>
            {
                try
                {
                    _task.Log($"RunGtService Start.....", LogLevel.Info);

                    var ns = GetStream();
                    byte[] readBuffer = new byte[4096];
                    byte[] writeBuffer = Encoding.ASCII.GetBytes("M0\r\n");

                    _sw.Start();

                    while (_task.RunningState == RunningState.WaitRun || _task.RunningState == RunningState.Running || _task.RunningState == RunningState.Pause)
                    {
                        lock (this)
                        {
                            ns.Write(writeBuffer, 0, writeBuffer.Length);
                            Thread.Sleep(10);
                            var count = ns.Read(readBuffer, 0, readBuffer.Length);

                            var data = Encoding.ASCII.GetString(readBuffer, 0, count).Replace("\r\n", string.Empty);
                            GtValue = data.Split(',').Skip(1).Select(v => double.Parse(v) / 10000).ToArray();

                            if (GtValue.Length < 3)
                            {
                                _task.Log($"DATA LEGTH {GtValue.Length} ERROR", LogLevel.Error);
                                return;
                            }
                            else if (GtValue.Any(v => v > 6))
                            {
                                _task.Log($"GT READ MAX OVERRANGE {string.Join(",", GtValue.Select(v => v.ToString("F3")))} ERROR", LogLevel.Error);
                                return;
                            }
                        }
                    }

                    _sw.Stop();


                }
                catch (Exception ex)
                {
                    _task.Log($"RunGtServiceError:{ex.Message}", LogLevel.Error);
                }
                finally
                {
                    if (_task.RunningState == RunningState.Running || _task.RunningState == RunningState.Pause)
                    {
                        _task.Log("RunGTService Error Finish!!!", LogLevel.Error);
                    }
                    else
                    {
                        _task.Log("RunGTService Finish!!!", LogLevel.Debug);
                    }
                }



            });
        }


        public double[] QueryGtValue()
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return new[] { 0.1, 0.2, 0.3 };
            }


            lock (this)
            {
                var result = new double[3];
                Array.Copy(GtValue, result, result.Length);
                return result;
            }
        }











    }
}
