using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lead.Detect.MeasureComponents.Thermo2Camera
{

    /// <summary>
    /// Machine A Camera
    /// </summary>
    public class ThermoCameraA : ThermoCameraBase
    {

        public ThermoCameraA()
        {
            IP = "127.0.0.1";
            Port = 6000;
        }


        public override bool Trigger(string msg)
        {
            //起始终止符判断
            LastError = string.Empty;
            var ret = base.Trigger(msg);
            if (ret)
            {
                if (!string.IsNullOrEmpty(TriggerResult))
                {
                    return true;
                }
                //trigger return msg error
                LastError = "RecvTriggerMsgFormatError";
                return false;
            }
            else
            {
                //trigger fail
                LastError = "RecvTriggerError";
                return false;
            }
        }


        public static readonly string FAIL_MSG = "3.0";


        public Dictionary<int, string> ProductMsg = new Dictionary<int, string>()
        {
            {1, "P1" },
            {2, "P2" },
            {3, "P3" },
            {4, "P4" },
            {5, "P5" },
            {6, "P6" },
        };


        private int _product = 0;
        public Dictionary<string, int> ProductIndexDict = new Dictionary<string, int>()
        {
            {"A117VC", 1 },
            {"A117MDL", 2 },
            {"A147VC", 3 },
            {"A147MDL", 4 },
        };


        public Dictionary<int, int> ProductTriggerStep = new Dictionary<int, int>()
        {
            {1, 100 }, //a117 vc
            {2, 100 }, //a147 vc
            {3, 100 }, //a117 module
            {4, 100 }, //a147 module
            {5, 100 },
            {6, 100 },
            {7, 100 },
            {8, 100 },
        };



        public bool SwitchProduct(int product)
        {
            if (!ProductMsg.ContainsKey(product))
            {
                LastError = $"SwitchProduct {product} Error";
                return false;
            }

            _product = product;
            return true;
        }


        public bool TriggerBarcode()
        {
            var ret = Trigger($"0,{_product}");
            if (ret)
            {
                //parse trigger result to getresult
                var triggerResult = TriggerResult;
                if (triggerResult.StartsWith(FAIL_MSG))
                {
                    LastError = $"FAILMSG {triggerResult}";
                    return false;
                }

                var data = triggerResult.Split(',');
                if (data.Length < 2)
                {
                    TriggerResult = string.Empty;
                    LastError = $"RecvBarcodeError {triggerResult}";
                    return false;
                }

                if (triggerResult.StartsWith("0,0"))
                {
                    TriggerResult = string.Empty;
                    LastError = $"RecvBarcodeError {triggerResult}";
                    return false;
                }
                else if (triggerResult.StartsWith("0,1"))
                {
                    if (data.Length > 2)
                    {
                        TriggerResult = data[2];
                        return true;
                    }
                    else
                    {
                        LastError = $"RecvBarcodeEmpty {triggerResult}";
                        return false;
                    }
                }
                else
                {
                    TriggerResult = string.Empty;
                    LastError = $"RecvFormatError {triggerResult}";
                    return false;
                }
            }
            return false;
        }

        public bool TriggerProduct(int step)
        {
            if (!ProductMsg.ContainsKey(_product)
                || !ProductTriggerStep.ContainsKey(_product)
                || !(step > 0 && step < ProductTriggerStep[_product]))
            {
                LastError = $"Trigger Step {step} Error";
                return false;
            }

            var sendMsg = $"1,{_product},{step}";

            var ret = Trigger(sendMsg);
            if (ret)
            {
                //parse trigger result to getresult
                var triggerResult = TriggerResult;
                if (triggerResult.StartsWith(FAIL_MSG))
                {
                    LastError = $"TriggerProductFail {step} {TriggerResult}";
                    return false;
                }
                else if (triggerResult.StartsWith($"{sendMsg},1"))
                {
                    TriggerResult = $"Capture {step} OK {TriggerResult}";
                    return true;

                }
                else if (triggerResult.StartsWith($"{sendMsg},0"))
                {
                    TriggerResult = $"Capture {step} NG {TriggerResult}";
                    return false;
                }
                else
                {
                    TriggerResult = $"RecvCaptureError {step} {triggerResult}";
                    return false;
                }
            }

            return false;
        }


        public bool TriggerCalib(int step)
        {

            if (!ProductMsg.ContainsKey(_product)
                || !ProductTriggerStep.ContainsKey(_product)
                || !(step > 0 && step < ProductTriggerStep[_product]))
            {
                LastError = $"TRIGGER CALIB {step} Error";
                return false;
            }

            var sendMsg = $"2,{_product},{step}";

            var ret = Trigger(sendMsg);
            if (ret)
            {
                //parse trigger result to getresult
                var triggerResult = TriggerResult;
                if (triggerResult.StartsWith(FAIL_MSG))
                {
                    LastError = $"TRIGGER CALIB FAIL {step}";
                    return false;
                }
                else if (triggerResult.StartsWith($"{sendMsg},1"))
                {
                    TriggerResult = $"CALIB {step} OK";
                    return true;

                }
                else if (triggerResult.StartsWith($"{sendMsg},0"))
                {
                    TriggerResult = $"CALIB {step} NG";
                    return false;
                }
                else
                {
                    TriggerResult = $"RECV CALIB FORMAT ERROR {step} {triggerResult}";
                    return false;
                }
            }

            return false;
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="resultInfo"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public override string GetResult(string resultInfo,int timeout = 3000)
        {
            if (!string.IsNullOrEmpty(resultInfo))
            {
                var resultMsg = ReadMsg(timeout);
                if (resultMsg.StartsWith("2,1") || resultMsg.StartsWith("2,0"))
                {
                    var data = resultMsg.Split(',');

                    var fais = data.Skip(2);
                    var sb = new StringBuilder();
                    foreach (var fai in fais)
                    {
                        if (string.IsNullOrEmpty(fai))
                        {
                            continue;
                        }
                        sb.Append(fai.Split(':')[1]);
                        sb.Append(",");
                    }

                    if (resultMsg.StartsWith("2,1"))
                    {
                        return "OK," + sb.ToString();
                    }
                    else if (resultMsg.StartsWith("2,0"))
                    {
                        return "NG," + sb.ToString();
                    }
                    else
                    {
                        return "ERROR," + sb.ToString();
                    }
                }
                else
                {
                    return $"NG,{resultMsg}";
                }
            }
            else
            {
                return TriggerResult;
            }
        }

        public override void Test()
        {
            var sb = new StringBuilder();


            SwitchProduct(1);

            TriggerBarcode();
            sb.AppendLine($"Recv:{TriggerResult} Error:{LastError}");

            for (var i = 0; i < 30; i++)
            {
                TriggerProduct(i + 1);
                sb.AppendLine($"Recv:{TriggerResult} Error:{LastError}");
            }

            MessageBox.Show(sb.ToString());
        }
    }
}
