using System;
using System.Text.RegularExpressions;

namespace Lead.Detect.PrimPlcOmron.OmronUdp
{
    public class CmdData
    {
        private byte count1; //字数量
        private byte count2 = 0x01;
        private readonly byte da2 = 0x00;

        //private string startPosHostLink = "0200";   //默认起始位置为200
        //private string countHostLink = "0001";       //默认读取一个字
        //private string dataSendHostLink = "";

        private byte[] dataSend; //发送的数据,字节数量必须为 字数量Count的2倍
        private readonly byte dna = 0x00; //dna 目标网络号

        ////Host-Link 协议内容
        //private string readOrWrite = "W";   //默认写操作
        private string _field = "D"; //默认DM存储区 "D"

        private readonly byte gct = 0x02;

        //FINS参数

        private readonly byte mrc = 0x01; //IO数据存储区
        private readonly byte rsv = 0x00;
        private readonly byte sa2 = 0x00;
        private readonly byte sna = 0x00; //sna 本地网络号
        private byte src = 0x82; //默认DM区域

        private byte startH; //字节开始位置
        private byte startL = 0xc8; //200 开始
        private readonly byte startM = 0x00; //该字节在欧姆龙PLC中无作用

        public int DataLengthRate { get; } = 4;


        public byte ICF { get; set; } = 0x80;

        public byte DA1 { get; set; } = 0x08;

        public byte SA1 { get; set; } = 0x02;

        public byte SID { get; set; } = 0x01;

        public byte OPT { get; private set; } = 0x02;

        #region "FINS读写"

        public byte[] UDPDataSend()
        {
            byte[] result;
            if (0x01 == OPT)
            {
                result = new byte[18];
            }
            else
            {
                result = new byte[18 + dataSend.Length];
                Array.Copy(dataSend, 0, result, 18, dataSend.Length);
            }

            result[0] = ICF;
            result[1] = rsv;
            result[2] = gct;
            result[3] = dna;
            result[4] = DA1;
            result[5] = da2;
            result[6] = sna;
            result[7] = SA1;
            result[8] = sa2;
            result[9] = SID;
            result[10] = mrc;
            result[11] = OPT;
            result[12] = src;
            result[13] = startH;
            result[14] = startL;
            result[15] = startM;
            result[16] = count1;
            result[17] = count2;

            return result;
        }

        //设置是读操作还是写操作
        public void SetOpt(Operate operate)
        {
            switch (operate)
            {
                case Operate.read:
                    OPT = 0x01;
                    //readOrWrite = "R";
                    break;
                case Operate.write:
                    OPT = 0x02;
                    //readOrWrite = "W";
                    break;
                default:
                    break;
            }
        }

        //设置PLC对应的分区
        public int SetSrc(Field fild, bool isBit)
        {
            if (isBit == false)
                switch (fild)
                {
                    case Field.CIO:
                        src = 0xB0;
                        _field = "CIO";
                        break;
                    case Field.WR:
                        src = 0xB1;
                        _field = "WR";
                        break;
                    case Field.HR:
                        src = 0xB2;
                        _field = "HR";
                        break;
                    case Field.AR:
                        src = 0xB3;
                        _field = "AR";
                        break;
                    case Field.C:
                        src = 0x89;
                        _field = "C";
                        break;
                    case Field.T:
                        src = 0x89;
                        _field = "T";
                        break;
                    case Field.DM:
                        src = 0x82;
                        _field = "D";
                        break;
                    //case Field.DR:
                    //    src = 0xBC;
                    //    break;
                    //case Field.IR:
                    //    src = 0xDC;
                    //    break;
                    //case Field.EM:
                    //    src = 0xA0;
                    //    break;
                    default:
                        break;
                }
            else
                switch (fild)
                {
                    case Field.CIO:
                        src = 0x30;
                        _field = "CIO";
                        break;
                    case Field.WR:
                        src = 0x31;
                        _field = "WR";
                        break;
                    case Field.HR:
                        src = 0x32;
                        _field = "HR";
                        break;
                    case Field.AR:
                        src = 0x33;
                        _field = "AR";
                        break;
                    case Field.C:
                        src = 0x09;
                        _field = "C";
                        break;
                    case Field.T:
                        src = 0x09;
                        _field = "T";
                        break;
                    case Field.DM:
                        src = 0x02;
                        _field = "D";
                        break;
                    default:
                        break;
                }

            if (_field == "NULL")
            {
                return -1;
            }
            return 0;
        }

        //设置起始位
        public bool SetStart(string strStart)
        {
            var startPos = WordConvertToByte(Convert.ToInt16(strStart));
            if (2 != startPos.Length) return false;
            startH = startPos[0];
            startL = startPos[1];
            return true;
        }

        //设置需要读取的数据个数 -- 读操作
        public bool SetDataCount(int count)
        {
            var countByte = WordConvertToByte((short) count);
            count1 = countByte[0];
            count2 = countByte[1];
            return true;
        }

        //写单个字用
        private byte[] WordConvertToByte(short word)
        {
            var result = new byte[2];
            result[0] = Convert.ToByte((word >> 8) & 0x00FF);
            result[1] = Convert.ToByte(word & 0x00FF);
            return result;
        }

        //写多个位用
        private byte[] WordConvertToByte(int[] word)
        {
            var dataLenth = word.Length;

            var result = new byte[dataLenth];
            for (var i = 0; i < dataLenth; i++) result[i] = Convert.ToByte(word[i] & 0x00FF);
            return result;
        }

        //设置写入的数据和数据长度 --- 写操作
        public void SetData(int data)
        {
            dataSend = WordConvertToByte((short) data);
        }

        public void SetData(int[] data)
        {
            dataSend = WordConvertToByte(data);
        }

        //获取读写的首地址
        public bool GetStartPos(string posStr, bool isBit, out string startPos)
        {
            startPos = "";
            var ret = true;
            var charCount = 0;
            var rex = new Regex(@"^\d+$");
            for (var i = 0; i < posStr.Length; i++)
            {
                var strSub = posStr.Substring(i, 1);
                if (rex.IsMatch(strSub))
                {
                    charCount = i;
                    break;
                }
            }

            var fied = posStr.Substring(0, charCount);
            switch (fied)
            {
                case "CIO":
                    SetSrc(Field.CIO, isBit);
                    break;
                case "WR":
                    SetSrc(Field.WR, isBit);
                    break;
                case "HR":
                    SetSrc(Field.HR, isBit);
                    break;
                case "AR":
                    SetSrc(Field.AR, isBit);
                    break;
                case "C":
                    SetSrc(Field.C, isBit);
                    break;
                case "T":
                    SetSrc(Field.T, isBit);
                    break;
                case "D":
                    SetSrc(Field.DM, isBit);
                    break;
                default:
                    return false;
            }

            startPos = posStr.Substring(charCount, posStr.Length - charCount);
            if (!rex.IsMatch(startPos)) return false;
            return ret;
        }

        //分离键值，用逗号分开，第1个元素：D20  第2个元素：读位时表示bit的index，读字时无作用
        public void SplitKeyValue(string valueStr, out string posStr, out int index)
        {
            index = -1;
            posStr = "";
            var strSpited = valueStr.Split(',');
            if (strSpited != null)
            {
                posStr = strSpited[0];
                index = Convert.ToInt32(strSpited[1]);
            }
        }

        //private byte[] WordConvertToByteBit(short word)
        //{
        //    byte[] result = new byte[16];
        //    result[0] = Convert.ToByte((word >> 8) & 0x00FF);
        //    result[1] = Convert.ToByte(word & 0x00FF);
        //    return result;
        //}

        #endregion


        #region "暂时不用的程序"

        //****************************************以下为HostLink内容，暂时不用************************************

        ////返回TCP协议的握手指令
        //public byte[] TCPDataHandclasp()
        //{
        //    byte[] result = new byte[20];
        //    result[0] = 0x46;   //ASCII Code : FINS
        //    result[1] = 0x49;
        //    result[2] = 0x4e;
        //    result[3] = 0x53;
        //    result[4] = 0x00;   //Length of data from command onwards :12 bytes
        //    result[5] = 0x00;
        //    result[6] = 0x00;
        //    result[7] = 0x0c;
        //    result[8] = 0x00;   //Command
        //    result[9] = 0x00;
        //    result[10] = 0x00;
        //    result[11] = 0x00;
        //    result[12] = 0x00;  //Error Code : not used
        //    result[13] = 0x00;
        //    result[14] = 0x00;
        //    result[15] = 0x00;
        //    result[16] = 0x00;  //Client node address,0~254   客户端IP
        //    result[17] = 0x00;
        //    result[18] = 0x00;
        //    result[19] = sa1;
        //    return result;
        //}

        ////发现TCP协议的指令
        //public byte[] TCPDataSend()
        //{
        //    byte[] finsCmd = UDPDataSend();

        //    byte[] result = new byte[16 + finsCmd.Length];

        //    result[0] = 0x46;   //ASCII Code : FINS
        //    result[1] = 0x49;
        //    result[2] = 0x4e;
        //    result[3] = 0x53;
        //    result[4] = 0x00;   //Length of data after command  :20~2020 bytes   0014到 07e4
        //    result[5] = 0x00;
        //    result[6] = 0x00;
        //    result[7] = Convert.ToByte(8 + finsCmd.Length);
        //    result[8] = 0x00;   //Command
        //    result[9] = 0x00;
        //    result[10] = 0x00;
        //    result[11] = 0x02;
        //    result[12] = 0x00;  //Error Code : not used
        //    result[13] = 0x00;
        //    result[14] = 0x00;
        //    result[15] = 0x00;

        //    Array.Copy(finsCmd, 0, result, 16, finsCmd.Length);

        //    return result;
        //}

        //public string HostLinkDataSend()
        //{
        //    string result = "@00";
        //    result += readOrWrite;
        //    result += field;
        //    result += startPosHostLink;
        //    if ("W" == readOrWrite)
        //    {
        //        result += dataSendHostLink;
        //    }
        //    else
        //    {
        //        result += countHostLink;
        //    }
        //    result += FCS(result);
        //    result += "*";
        //    result += "\r";
        //    return result;
        //}


        ////设置串口的起始
        //public void SetStartHostLink(string startPos)
        //{
        //    startPosHostLink = startPos.PadLeft(4, '0');
        //}


        //public bool SetDataHostLink(string count, string data)
        //{
        //    countHostLink = count.PadLeft(4, '0');
        //    dataSendHostLink = data;
        //    return true;
        //}

        ////校验位异或运算
        //public string FCS(string InputStr)
        //{
        //    string Tempfes = string.Empty;

        //    byte Xorresult = 0;
        //    for (int index = 0; index < InputStr.Length; index++)
        //    {
        //        Xorresult ^= (byte)InputStr[index]; //'`按位异或
        //    }

        //    Tempfes = Xorresult.ToString("X");// Hex$(Xorresult) //' `转化为16进制
        //    if (Tempfes.Length == 1)
        //    {
        //        Tempfes = "0" + Tempfes;
        //    }
        //    return Tempfes;
        //}


        //public string ExchangeBy2Str(string strMes)
        //{
        //    string result = "";
        //    if (0 != strMes.Length % 2)
        //    {
        //        return "";
        //    }
        //    int num = strMes.Length / 2;
        //    for (int i = num-1; i >= 0; i--)
        //    {
        //        result += strMes.Substring(strMes.Length - 2 - 2 * i, 2);
        //    }
        //    return result;
        //}

        #endregion
    }
}