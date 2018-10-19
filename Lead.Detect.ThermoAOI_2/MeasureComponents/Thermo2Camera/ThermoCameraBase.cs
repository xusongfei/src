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

        #region  Image Manage

        public string ImageFolder { get; set; }

        public float ImageFolderStorageLimit_MB { get; set; } = 4096;

        private DateTime _triggerTime;

        public void ClearImageFolderStorage()
        {

        }
        public Image FindLastImageAfterTrigger()
        {
            //todo

            return null;
        }

        #endregion



        #region ICameraEx interface

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
                if (_client == null || !_client.Connected)
                {
                    LastError = "Client Connected Error";
                    return false;
                }

                _ns = _ns ?? _client.GetStream();
                {
                    var msgBytes = Encoding.ASCII.GetBytes(msg);
                    _ns.Write(msgBytes, 0, msgBytes.Length);

                    _ns.ReadTimeout = 3000;
                    var buffer = new byte[1024];
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

        public string ReadMsg()
        {
            if (_ns == null)
            {
                return string.Empty;
            }

            _ns.ReadTimeout = 3000;
            var buffer = new byte[1024];
            var count = _ns.Read(buffer, 0, buffer.Length);
            if (count <= 0)
            {
                return string.Empty;
            }

            return Encoding.ASCII.GetString(buffer, 0, count);
        }


        /// <summary>
        /// 解析TriggerResult结果
        /// </summary>
        /// <param name="resultInfo"></param>
        /// <returns></returns>
        public virtual string GetResult(string resultInfo)
        {

            return TriggerResult;
        }

        public virtual void Test()
        {
            
        }

        #endregion
    }
}
