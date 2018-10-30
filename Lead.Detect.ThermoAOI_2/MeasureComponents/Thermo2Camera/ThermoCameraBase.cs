using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.CameraControl;

namespace Lead.Detect.MeasureComponents.Thermo2Camera
{
    public class ThermoCameraBase : ICameraEx
    {
        private TcpClient _client;

        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string LastError { get; set; }

        public bool Connect()
        {
            if (_client != null)
            {
                Disconnect();
            }

            try
            {
                _client = new TcpClient();
                _client.Connect(IP, Port);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Disconnect()
        {
            if (_client != null)
            {
                _ns = null;
                _client.Close();
                _client.Dispose();
                _client = null;
            }
            return true;
        }

        public Image GrabOne()
        {
            return null;
        }


        protected string TriggerResult;

        private NetworkStream _ns;

        /// <summary>
        /// 触发相机
        /// </summary>
        /// <param name="msg"></param>
        public virtual bool Trigger(string msg)
        {
            try
            {
                TriggerResult = string.Empty;

                if (_client == null || !_client.Connected)
                {
                    LastError = "Client Connected Error";
                    return false;
                }

                _ns = _ns ?? _client.GetStream();
                {
                    var msgBytes = Encoding.ASCII.GetBytes(msg);
                    _ns.Write(msgBytes, 0, msgBytes.Length);

                    int timeoutCount = 0;
                    while (!_ns.DataAvailable && timeoutCount++ < 6000)
                    {
                        System.Threading.Thread.Sleep(1);
                    }

                    if (!_ns.DataAvailable)
                    {
                        LastError = "Client No Recv Data";
                        return false;
                    }

                    _ns.ReadTimeout = 6000;
                    var buffer = new byte[2048];
                    var count = _ns.Read(buffer, 0, buffer.Length);
                    if (count <= 0)
                    {
                        return false;
                    }
                    TriggerResult = Encoding.ASCII.GetString(buffer, 0, count);
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pos"></param>
        public virtual bool Trigger(string msg, PosXYZ pos)
        {
            return false;
        }

        public string ReadMsg(int timeout = 6000)
        {
            try
            {
                int timeoutCount = 0;
                while (!_ns.DataAvailable && timeoutCount++ < timeout)
                {
                    System.Threading.Thread.Sleep(1);
                }

                if (_ns == null || !_ns.DataAvailable)
                {
                    return string.Empty;
                }

                _ns.ReadTimeout = timeout;
                var buffer = new byte[4096];
                var count = _ns.Read(buffer, 0, buffer.Length);
                if (count <= 0)
                {
                    return string.Empty;
                }
                return Encoding.ASCII.GetString(buffer, 0, count);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 解析TriggerResult结果
        /// </summary>
        /// <param name="resultInfo"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public virtual string GetResult(string resultInfo, int timeout = 0)
        {
            return TriggerResult;
        }

        public virtual void Test()
        {

        }

        public override string ToString()
        {
            return GetType().Name.ToString();
        }
    }
}
