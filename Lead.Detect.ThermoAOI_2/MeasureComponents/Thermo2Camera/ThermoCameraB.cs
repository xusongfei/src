using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.frameworkManage;

namespace Lead.Detect.MeasureComponents.Thermo2Camera
{
    /// <summary>
    /// Machine B Camera, implement user define protocols
    /// </summary>
    public class ThermoCameraB : ThermoCameraBase
    {
        public ThermoCameraB()
        {
            IP = "127.0.0.1";
            Port = 50000;
        }


        /// <summary>
        /// Trigger返回值起始终止符判断
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Trigger(string msg)
        {
         
            LastError = string.Empty;
            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

            var ret = base.Trigger(msg);
            if (ret)
            {
                var triggerResult = TriggerResult;
                if (!string.IsNullOrEmpty(triggerResult))
                {
                    return true;
                }

                //trigger return msg error
                LastError = "TriggerRecvFormatError";
                return false;
            }
            else
            {
                //trigger fail
                LastError = "TriggerRecvFail";
                return false;
            }
        }


        private int _product = 0;

        public Dictionary<int, string> SwitchProductMsg = new Dictionary<int, string>()
        {
            {1, "P1"},
            {2, "P2"},
            {3, "P3"},
            {4, "P4"},
        };

        public Dictionary<int, string> TriggerProductMsg = new Dictionary<int, string>()
        {
            {1, "S1"},
            {2, "S2"},
            {3, "S3"},
            {4, "S4"},
            {5, "S5"},
            {6, "S6"},
            {7, "S7"},
            {8, "S8"},
        };

        /// <summary>
        /// 切换产品编号
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool SwitchProduct(int product)
        {
            if (!SwitchProductMsg.ContainsKey(product))
            {
                LastError = $"SwitchProduct {product} Error";
                return false;
            }

            _product = product;
            var ret = Trigger(SwitchProductMsg[product]);
            if (!ret)
            {

            }

            return ret;
        }


        /// <summary>
        /// 触发产品测试,解析数据
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public bool TriggerProduct(int step)
        {
            //defence check
            if (_product <= 0)
            {
                LastError = "NOT SELECT PRODUCT";
                return false;
            }
            if (!SwitchProductMsg.ContainsKey(_product) || !TriggerProductMsg.ContainsKey(step))
            {
                LastError = $"Trigger Step {step} Error";
                return false;
            }


            //trigger run
            if (FrameworkExtenion.IsSimulate)
            {
                TriggerResult = $"{SwitchProductMsg[_product] + TriggerProductMsg[step]}:0.1,0.2,0.3,0.4,0.5,0.6,0.7,0.8";
                return true;
            }

            var ret = Trigger(TriggerProductMsg[step]);
            if (ret)
            {
                //parse return data
                var triggerResult = TriggerResult;
                if (triggerResult.StartsWith(SwitchProductMsg[_product] + TriggerProductMsg[step]))
                {
                    return true;
                }
                else
                {
                    LastError = "RecvTriggerProductError";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="resultInfo"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public override string GetResult(string resultInfo, int timeout = 3000)
        {
            if (!string.IsNullOrEmpty(resultInfo))
            {
                var resultMsg = ReadMsg(timeout);
                return resultMsg;
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
            sb.AppendLine($"Recv: {TriggerResult} Error:{LastError}");

            for (var i = 0; i < 6; i++)
            {
                TriggerProduct(i + 1);
                sb.AppendLine($"Step: {i + 1} Recv: {TriggerResult} Error: {LastError}");
            }

            MessageBox.Show(sb.ToString());
        }
    }
}