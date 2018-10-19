using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Lead.Detect.PrimPlcOmron.OmronUdp
{
    public class OmronUDP
    {
        private static string receiveData = "";
        private bool _boolTaskStart;
        private StructIP _struIP;
        private Task _taskCheckConnect;
        private UdpState _udpState;
        private CmdData dataSend;
        private IPEndPoint ipInfo;
        public bool isInited;
        private readonly object lockObj = new object();
        private UdpClient udpClient;
        public bool IsConnected { get; set; }

        private void CheckHeartThrob()
        {
            while (_boolTaskStart)
            {
                lock (lockObj)
                {
                    try
                    {
                        var ip = _struIP.ipPLC;
                        var port = _struIP.port;
                        var localIP = _struIP.ipLocal;
                        receiveData = "";
                        var ping = new Ping();
                        var iep = new IPEndPoint(IPAddress.Parse(ip), port);

                        var pr = ping.Send(ip, 1000);
                        if (pr.Status != IPStatus.Success)
                        {
                            IsConnected = false;
                            continue;
                        }

                        var ipArrary = localIP.Split('.');
                        var arraryLenth = ipArrary.Length;
                        var ipEnd = Convert.ToByte(ipArrary[arraryLenth - 1]);
                        var heartByte = new byte[18];
                        heartByte[0] = 0x80;
                        heartByte[1] = 0x00;
                        heartByte[2] = 0x02;
                        heartByte[3] = 0x00; //dna 目标网络号
                        heartByte[4] = 0x00; //PLC IP末尾的数字
                        heartByte[5] = 0x00;
                        heartByte[6] = 0x00; //sna 本地网络号
                        heartByte[7] = ipEnd; //本地 IP末尾的数字
                        heartByte[8] = 0x00;
                        heartByte[9] = 0x01; //SID 发什么回什么，用来一致化发送和接收

                        heartByte[10] = 0x01; //IO数据存储区
                        heartByte[11] = 0x01; //默认是写操作
                        heartByte[12] = 0x00; //默认DM区域
                        heartByte[13] = 0x00; //字节开始位置
                        heartByte[14] = 0x00; //200 开始
                        heartByte[15] = 0x00; //该字节在欧姆龙PLC中无作用
                        heartByte[16] = 0x00; //字数量
                        heartByte[17] = 0x00; //字数量
                        udpClient.Send(heartByte, heartByte.Length);
                        var strReceive = "";
                        var rt = ReadResult(ref strReceive);
                    }
                    catch (Exception )
                    {
                        IsConnected = false;
                    }
                }

                Thread.Sleep(3000);
            }
        }

        //关闭UDP通讯
        public void Close()
        {
            _boolTaskStart = false;
            //udpClient.Close();
        }

        private void EndReceive(IAsyncResult ar)
        {
            var s = ar.AsyncState as UdpState;
            try
            {
                if (s != null)
                {
                    var udpClient = s.UdpClient;
                    var ip = s.IP;
                    receiveData = "";
                    var receiveBytes = udpClient.EndReceive(ar, ref ip);

                    for (var i = 0; i < receiveBytes.Length; i++) receiveData += receiveBytes[i].ToString("X2");

                    udpClient.BeginReceive(EndReceive, s); //在这里重新开始一个异步接收，用于处理下一个网络请求
                }
            }
            catch
            {
                //处理异常
            }
        }

        //通讯连接
        public bool Init(string ip, int port, string localIp)
        {
            try
            {
                //ping PLC，看硬件是否能连上
                var ping = new Ping();
                var iep = new IPEndPoint(IPAddress.Parse(ip), port);

                var pr = ping.Send(ip, 1000);
                if (pr.Status != IPStatus.Success)
                {
                    IsConnected = false;
                    return false;
                }

                if (udpClient == null)
                {
                    udpClient = new UdpClient();
                    _struIP = new StructIP();
                    dataSend = new CmdData();
                }

                ipInfo = new IPEndPoint(IPAddress.Parse(ip), port);
                _udpState = new UdpState(udpClient, ipInfo);
                _struIP.ipPLC = ip;
                _struIP.port = port;
                _struIP.ipLocal = localIp;
                udpClient.Connect(ipInfo);
                var length = ip.Length;
                var index = 0;
                for (var i = 0; i < length; i++)
                    if ("." == ip.Substring(i, 1))
                    {
                        index++;
                        if (3 == index)
                        {
                            var ipLast = ip.Substring(i + 1, length - i - 1);
                            dataSend.DA1 = Convert.ToByte(ipLast);
                            break;
                        }
                    }

                length = localIp.Length;
                index = 0;
                for (var i = 0; i < length; i++)
                    if ("." == localIp.Substring(i, 1))
                    {
                        index++;
                        if (3 == index)
                        {
                            var ipLast = localIp.Substring(i + 1, length - i - 1);
                            dataSend.SA1 = Convert.ToByte(ipLast);
                            break;
                        }
                    }

                if (isInited == false)
                {
                    udpClient.BeginReceive(EndReceive, _udpState);
                    Thread.Sleep(500);
                    _boolTaskStart = true;
                    _taskCheckConnect = new Task(CheckHeartThrob);
                    _taskCheckConnect.Start();
                }

                isInited = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //读单个位
        public bool ReadBit(string posStr, int index, out int dataRead)
        {
            dataRead = -1;
            if (IsConnected == false) return false;
            var startPostion = "";
            lock (lockObj)
            {
                try
                {
                    Thread.Sleep(5);
                    receiveData = "";
                    dataSend.SetOpt(Operate.read);
                    var boolGetStart = dataSend.GetStartPos(posStr, true, out startPostion);
                    if (boolGetStart == false) return false;

                    dataSend.SetStart(startPostion);
                    dataSend.SetDataCount(16);

                    var data = dataSend.UDPDataSend();
                    udpClient.Send(data, data.Length);
                    var strReceive = "";
                    if (ReadResult(ref strReceive))
                    {
                        var rate = dataSend.DataLengthRate;
                        var receive = strReceive.Substring(28, strReceive.Length - 28);
                        if (receive.Length % 2 != 0) return false;
                        var lenthBytes = receive.Length / 2;
                        var dataArrary = new int[lenthBytes];
                        for (var i = 0; i < lenthBytes; i++)
                        {
                            var subStr = receive.Substring(i * 2, 2);

                            dataArrary[i] = int.Parse(subStr);
                        }

                        dataRead = dataArrary[index];
                        return true;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool ReadBitByName(Dictionary<string, string> dataDictionary, string name, out int dataRead) //根据名称 读单个位
        {
            dataRead = -1;
            if (IsConnected == false) return false;
            var index = 0; //写读字时用不到
            var ret = false;
            var posStr = "";
            var keyValue = "";
            dataDictionary.TryGetValue(name, out keyValue);
            dataSend.SplitKeyValue(keyValue, out posStr, out index);
            ret = ReadBit(posStr, index, out dataRead);
            return ret;
        }

        //读单个字的16位
        public bool ReadBitChannel(string posStr, out int[] dataRead)
        {
            var startPostion = "";
            dataRead = null;
            if (IsConnected == false) return false;
            receiveData = "";
            dataSend.SetOpt(Operate.read);
            var boolGetStart = dataSend.GetStartPos(posStr, true, out startPostion);
            if (boolGetStart == false) return false;

            dataSend.SetStart(startPostion);
            dataSend.SetDataCount(16);

            var data = dataSend.UDPDataSend();
            udpClient.Send(data, data.Length);
            var strReceive = "";
            if (ReadResult(ref strReceive))
            {
                var rate = dataSend.DataLengthRate;
                var receive = strReceive.Substring(28, strReceive.Length - 28);
                if (receive.Length % 2 != 0) return false;
                var lenthBytes = receive.Length / 2;
                dataRead = new int[lenthBytes];
                for (var i = 0; i < lenthBytes; i++)
                {
                    var subStr = receive.Substring(i * 2, 2);

                    dataRead[i] = int.Parse(subStr);
                }

                return true;
            }

            return false;
        }

        private bool ReadResult(ref string strReceive)
        {
            var start = DateTime.Now;
            while (true)
            {
                if (receiveData != "" && receiveData.Length >= 28)
                {
                    IsConnected = true;
                    Thread.Sleep(5);
                    break;
                }

                var span = DateTime.Now - start;
                if (span.Seconds > 2)
                {
                    IsConnected = false;
                    udpClient.Connect(ipInfo);
                    return false;
                }
            }

            strReceive = receiveData;
            return true;
        }

        //读单个字或者多个字
        public bool ReadWord(string posStr, int readNum, out int[] dataRead)
        {
            var startPostion = "";
            dataRead = null;
            if (IsConnected == false) return false;
            lock (lockObj)
            {
                try
                {
                    receiveData = "";
                    dataSend.SetOpt(Operate.read);
                    var boolGetStart = dataSend.GetStartPos(posStr, false, out startPostion);
                    if (boolGetStart == false) return false;

                    dataSend.SetStart(startPostion);
                    dataSend.SetDataCount(readNum);

                    var data = dataSend.UDPDataSend();
                    udpClient.Send(data, data.Length);
                    var strReceive = "";
                    if (ReadResult(ref strReceive))
                    {
                        var rate = dataSend.DataLengthRate;
                        var receive = strReceive.Substring(28, strReceive.Length - 28);
                        if (receive.Length % rate != 0) return false;
                        dataRead = new int[receive.Length / rate];
                        for (var i = 0; i < receive.Length / rate; i++) dataRead[i] = Convert.ToInt32(receive.Substring(rate * i, rate), 16);
                        return true;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool ReadWordByName(Dictionary<string, string> dataDictionary, string name, out int dataRead) //根据名称 读单个字
        {
            dataRead = -1;
            if (IsConnected == false) return false;
            int[] dataReadArrary = null;
            var index = 0; //写读字时用不到
            var ret = false;
            var posStr = "";
            var keyValue = "";
            dataDictionary.TryGetValue(name, out keyValue);
            dataSend.SplitKeyValue(keyValue, out posStr, out index);
            ret = ReadWord(posStr, 1, out dataReadArrary);
            dataRead = dataReadArrary[0];
            return ret;
        }

        //写单个位
        public bool WriteBit(string posStr, int index, int dataSet)
        {
            if (IsConnected == false) return false;
            var startPostion = "";
            receiveData = "";
            int[] dataGet = null;
            lock (lockObj)
            {
                try
                {
                    ReadBitChannel(posStr, out dataGet);
                    dataGet[index] = dataSet;
                    dataSend.SetOpt(Operate.write);
                    var boolGetStart = dataSend.GetStartPos(posStr, true, out startPostion);
                    if (boolGetStart == false) return false;
                    dataSend.SetStart(startPostion);

                    dataSend.SetData(dataGet);
                    dataSend.SetDataCount(16);

                    var data = dataSend.UDPDataSend();
                    udpClient.Send(data, data.Length);
                    var strReceive = "";
                    return ReadResult(ref strReceive);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool WriteBitByName(Dictionary<string, string> dataDictionary, string name, int dataSet) //根据名称 写单个位
        {
            if (IsConnected == false) return false;
            var index = 0; //写读字时用不到
            var ret = false;
            var posStr = "";
            var keyValue = "";
            dataDictionary.TryGetValue(name, out keyValue);
            dataSend.SplitKeyValue(keyValue, out posStr, out index);
            ret = WriteBit(posStr, index, dataSet);
            return ret;
        }


        //发送UDP指令  dataStr十六进制
        //写单个字
        public bool WriteWord(string posStr, int dataSet)
        {
            if (IsConnected == false) return false;
            var startPostion = "";
            lock (lockObj)
            {
                try
                {
                    receiveData = "";
                    dataSend.SetOpt(Operate.write);
                    var boolGetStart = dataSend.GetStartPos(posStr, false, out startPostion);
                    if (boolGetStart == false) return false;
                    dataSend.SetStart(startPostion);
                    dataSend.SetData(dataSet);
                    dataSend.SetDataCount(1);
                    var data = dataSend.UDPDataSend();
                    udpClient.Send(data, data.Length);
                    var strReceive = "";
                    var rt = ReadResult(ref strReceive);
                    return rt;
                }
                catch
                {
                    return false;
                }
            }
        }


        public bool WriteWordByName(Dictionary<string, string> dataDictionary, string name, int dataSet) //根据名称 写单个字
        {
            if (IsConnected == false) return false;
            var index = 0; //写读字时用不到
            var ret = false;
            var posStr = "";
            var keyValue = "";
            dataDictionary.TryGetValue(name, out keyValue);
            dataSend.SplitKeyValue(keyValue, out posStr, out index);
            ret = WriteWord(posStr, dataSet);
            return ret;
        }

        #region "备注程序，暂不用"

        //发送UDP指令
        //***************************以下方法暂不用，暂时先作备注*********************************
        //public void Write(CmdData head, ref string strSend)
        //{
        //    byte[] data = head.UDPDataSend();
        //    udpClient.Send(data, data.Length);

        //    strSend = "";
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        strSend += data[i].ToString("X2");
        //    }
        //}

        #endregion
    }

    public enum Operate
    {
        write,
        read
    }

    public enum Field
    {
        CIO,
        WR,
        DM,
        EM,
        T,
        C,
        IR,
        DR,
        HR,
        AR
    }

    public class UdpState
    {
        public UdpState(UdpClient udpclient, IPEndPoint ip)
        {
            UdpClient = udpclient;
            IP = ip;
        }

        public UdpClient UdpClient { get; }

        public IPEndPoint IP { get; }
    }

    internal struct StructIP
    {
        public string ipPLC;
        public int port;
        public string ipLocal;
    }
}