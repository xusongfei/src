using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Base.PrimView;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using System.IO;
using System.Threading;

namespace Lead.Detect.PrimLeisai
{
    public class LeiSaiCard : IPrim, IP2PMotionCard
    {
        #region IPrim

        public string Name { get; set; }
        public string PrimTypeName { get; set; }
        public Guid PrimId { get; set; }
        public Type ChildType { get; set; }
        public PrimType HSType { get; set; }
        public PrimManufacture Manu { get; set; }
        public PrimVer Ver { get; set; }
        public PrimConnType ConnType { get; set; }
        public string ConnInfo { get; set; }
        public PrimConnState PrimConnStat { get; set; }
        public PrimRunState PrimRunStat { get; set; }
        public bool PrimSimulator { get; set; }
        public bool PrimDebug { get; set; }
        public bool PrimEnable { get; set; }
        public IDataArea DataArea { get; set; }
        public Control PrimDebugUI { get; }
        public Control PrimConfigUI { get; }
        public Control PrimOutputUI { get; }

        private LeisaiConfig _config;


        public LeiSaiCard()
        {
            _config = new LeisaiConfig();
            PrimDebugUI = new PrimDefaultConfigControl(this);
            PrimConfigUI = new PrimDefaultConfigControl(this);
            PrimOutputUI = new PrimDefaultConfigControl(this);
        }

        public LeiSaiCard(XmlNode node)
        {
            ImportConfig(node);


            PrimDebugUI = new PrimDefaultConfigControl(this);
            PrimConfigUI = new PrimDefaultConfigControl(this);
            PrimOutputUI = new PrimDefaultConfigControl(this);
        }

        public int ImportConfig(XmlNode xmlNode)
        {
            var iRet = 0;
            if (xmlNode != null)
            {
                _config = XMLHelper.XMLToObject(xmlNode, typeof(LeisaiConfig)) as LeisaiConfig;

                if (_config != null)
                {
                    Name = _config.Name;
                    DevIndex = _config.DevIndex;
                    ConfigFilePath = _config.ConfigFilePath;


                    PrimTypeName = _config.PrimTypeName;
                    PrimId = _config.PrimId;
                }
            }
            else
                return -1;
            return iRet;
        }

        public XmlNode ExportConfig()
        {
            _config = _config ?? new LeisaiConfig();

            _config.Name = Name;
            _config.DevIndex = DevIndex;
            _config.ConfigFilePath = ConfigFilePath;

            _config.PrimTypeName = PrimTypeName;
            _config.PrimId = PrimId;

            var node = XMLHelper.ObjectToXML(_config);

            return node;
        }

        public int ImportDataConfig(XmlNode xmlNode)
        {
            return 0;
        }

        public XmlNode ExportDataConfig()
        {
            return null;
        }

        public int IPrimInit()
        {
            var ret = csLTDMC.LTDMC.dmc_board_init();
            if (ret == 0)
            {
                throw new Exception("not found leisai card");
            }
            //csLTDMC.LTDMC.dmc_soft_reset((ushort)DevIndex);

            return 0;
        }

        public int IPrimDispose()
        {
            csLTDMC.LTDMC.dmc_board_close();
            return 0;
        }


        public int IPrimConnect(ref string result)
        {
            if (_config != null)
            {
                LoadConfigFile(Path.GetFullPath(_config.ConfigFilePath));
            }
            PrimConnStat = PrimConnState.Connected;
            return 0;
        }

        public int IPrimDisConnect(ref string result)
        {
            PrimConnStat = PrimConnState.UnConnected;
            return 0;
        }

        public int IPrimResume()
        {
            PrimRunStat = PrimRunState.Running;
            return 0;
        }

        public object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info)
        {
            return 0;
        }

        public int IPrimStart()
        {
            PrimRunStat = PrimRunState.Running;
            return 0;
        }

        public int IPrimStop()
        {
            PrimRunStat = PrimRunState.Stop;
            return 0;
        }

        public int IPrimSuspend()
        {
            PrimRunStat = PrimRunState.Suspend;
            return 0;
        }

        public event PrimDataRefresh OnPrimDataRefresh;
        public event PrimOpLog OnPrimOpLog;
        public event PrimStateChanged OnPrimStateChanged;

        #endregion


        protected virtual int OnOnPrimDataRefresh(string devname, object context)
        {
            OnPrimDataRefresh?.Invoke(devname, context); return 0;
        }

        protected virtual int OnOnPrimOpLog(string devname, object log)
        {
            OnPrimOpLog?.Invoke(devname, log); return 0;
        }

        protected virtual int OnOnPrimStateChanged(string devname, object context)
        {
            OnPrimStateChanged?.Invoke(devname, context);
            return 0;
        }


        #region interface IP2PMotionCard


        public int DevIndex { get; set; }
        public string ConfigFilePath { get; set; }

        public void LoadConfigFile(string file)
        {
            var ret = csLTDMC.LTDMC.dmc_download_configfile((ushort)DevIndex, file);

            for (int i = 0; i < 6; i++)
            {
                csLTDMC.LTDMC.dmc_set_home_el_return((ushort)DevIndex, (ushort)i, 1);
            }

            if (ret != 0)
            {
                throw new Exception("load config file fail");
            }
        }

        public int WriteSingleDOutput(int index, int i, int port, int status)
        {
            status = status == 1 ? 0 : 1;

            var ret = csLTDMC.LTDMC.dmc_write_outbit((ushort)DevIndex, (ushort)port, (ushort)status);
            if (ret != 0)
            {
                throw new Exception("WriteSingleDOutput fail");
            }
            return 0;
        }

        public int ReadSingleDOutput(int index, int i, int port, out int status)
        {
            status = csLTDMC.LTDMC.dmc_read_outbit((ushort)DevIndex, (ushort)port);
            status = status == 0 ? 1 : 0;
            return 0;
        }

        public int ReadSingleDInput(int index, int i, int port, out int status)
        {
            status = csLTDMC.LTDMC.dmc_read_inbit((ushort)DevIndex, (ushort)port);
            status = status == 0 ? 1 : 0;
            return 0;
        }

        public int GetAxisPositionF(int axis, ref double d)
        {
            d = csLTDMC.LTDMC.dmc_get_position((ushort)DevIndex, (ushort)axis);
            return 0;
        }

        public int SetAxisPositionOrFeedbackPules(int axis, int pos)
        {
            return csLTDMC.LTDMC.dmc_set_position((ushort)DevIndex, (ushort)axis, pos);
        }

        public int GetAxisCommandF(int axis, ref double d)
        {
            d = csLTDMC.LTDMC.dmc_get_target_position((ushort)DevIndex, (ushort)axis);
            return 0;
        }

        public int AxisSetEnable(int index, int axis, bool enable)
        {
            csLTDMC.LTDMC.dmc_write_erc_pin((ushort)DevIndex, (ushort)axis, (ushort)0);
            Thread.Sleep(50);
            csLTDMC.LTDMC.dmc_write_erc_pin((ushort)DevIndex, (ushort)axis, (ushort)1);
            Thread.Sleep(50);
            csLTDMC.LTDMC.dmc_write_erc_pin((ushort)DevIndex, (ushort)axis, (ushort)0);
            Thread.Sleep(50);
            return csLTDMC.LTDMC.dmc_set_sevon_enable((ushort)DevIndex, (ushort)axis, (ushort)(enable ? 1 : 0));
        }

        public int AxisAbsMove(int index, int axis, int pos, int vel)
        {
            var minVel = 0d;
            var maxVel = 0d;
            var tAcc = 0d;
            var tDec = 0d;
            var stopVel = 0d;
            csLTDMC.LTDMC.dmc_get_profile((ushort)DevIndex, (ushort)axis, ref minVel, ref maxVel, ref tAcc, ref tDec, ref stopVel);
            csLTDMC.LTDMC.dmc_set_profile((ushort)DevIndex, (ushort)axis, minVel, vel, tAcc, tDec, stopVel);
            return csLTDMC.LTDMC.dmc_pmove((ushort)DevIndex, (ushort)axis, pos, 1);
        }

        public int AxisRelMove(int index, int axis, int step, int vel)
        {
            var minVel = 0d;
            var maxVel = 0d;
            var tAcc = 0d;
            var tDec = 0d;
            var stopVel = 0d;
            csLTDMC.LTDMC.dmc_get_profile((ushort)DevIndex, (ushort)axis, ref minVel, ref maxVel, ref tAcc, ref tDec, ref stopVel);
            csLTDMC.LTDMC.dmc_set_profile((ushort)DevIndex, (ushort)axis, minVel, vel, tAcc, tDec, stopVel);
            return csLTDMC.LTDMC.dmc_pmove((ushort)DevIndex, (ushort)axis, step, 0);
        }

        public bool AxisIsStop(int index, int axis)
        {
            return csLTDMC.LTDMC.dmc_check_done((ushort)DevIndex, (ushort)axis) == 1;
        }

        public int AxisStopMove(int index, int axis)
        {

            return csLTDMC.LTDMC.dmc_stop((ushort)DevIndex, (ushort)axis, 0);
        }

        public int AxisHomeMove(int index, int axis)
        {
            return csLTDMC.LTDMC.dmc_home_move((ushort)DevIndex, (ushort)axis);
        }

        public bool AxisHMV(int motionDevIndex, int axis)
        {
            ushort state = 0;
            var ret = csLTDMC.LTDMC.dmc_get_home_result((ushort)DevIndex, (ushort)axis, ref state);
            return state != 1;
        }

        public bool AxisIsEnble(int index, int axis)
        {
            return csLTDMC.LTDMC.dmc_get_sevon_enable((ushort)DevIndex, (ushort)axis) == 1;
        }

        public bool AxisIsAlarm(int index, int axis)
        {
            var sts = csLTDMC.LTDMC.dmc_axis_io_status((ushort)DevIndex, (ushort)axis);
            return (sts & (1 << 0)) > 0;
        }

        public bool AxisSingalEMG(int index, int axis)
        {
            var sts = csLTDMC.LTDMC.dmc_axis_io_status((ushort)DevIndex, (ushort)axis);
            return (sts & (1 << 3)) > 0;
        }

        public bool LimitMel(int index, int axis)
        {
            var sts = csLTDMC.LTDMC.dmc_axis_io_status((ushort)DevIndex, (ushort)axis);
            return (sts & (1 << 2)) > 0;
        }

        public bool LimitPel(int motionDevIndex, int axis)
        {
            var sts = csLTDMC.LTDMC.dmc_axis_io_status((ushort)DevIndex, (ushort)axis);
            return (sts & (1 << 1)) > 0;
        }

        public bool LimitOrg(int motionDevIndex, int axis)
        {
            var sts = csLTDMC.LTDMC.dmc_axis_io_status((ushort)DevIndex, (ushort)axis);
            return (sts & (1 << 4)) > 0;
        }

        public bool AxisAstp(int index, int axis)
        {
            var sts = csLTDMC.LTDMC.dmc_axis_io_status((ushort)DevIndex, (ushort)axis);
            return (sts & (1 << 3)) > 0;
        }

        public int AxisSetAcc(int boardId, int axis, double acc)
        {
            return csLTDMC.LTDMC.dmc_set_profile((ushort)DevIndex, (ushort)axis, acc / 4, acc / 2, acc / 50 * 0.01, acc / 50 * 0.01, acc);
        }

        public int AxisSetDec(int boardId, int axis, double dec)
        {
            return csLTDMC.LTDMC.dmc_set_profile((ushort)DevIndex, (ushort)axis, dec / 4, dec / 2, dec / 50 * 0.01, dec / 50 * 0.01, dec);
        }

        public int AxisSetHomeVel(int axis, int vel)
        {
            var minVel = 0d;
            var maxVel = 0d;
            var tAcc = 0d;
            var tDec = 0d;
            var stopVel = 0d;
            csLTDMC.LTDMC.dmc_get_profile((ushort)DevIndex, (ushort)axis, ref minVel, ref maxVel, ref tAcc, ref tDec, ref stopVel);
            csLTDMC.LTDMC.dmc_set_profile((ushort)DevIndex, (ushort)axis, vel / 3d, vel, tAcc, tDec, stopVel);
            return 0;
        }

        public bool AxisInp(int index, int axisChannel)
        {
            return true;
        }

        #endregion
    }
}