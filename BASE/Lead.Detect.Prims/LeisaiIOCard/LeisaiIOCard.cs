using System;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Base.PrimView;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimLeisaiIOCard
{
    public class LeisaiIOCard : IPrim, IP2PMotionCard
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

        private LeisaiIOConfig _ioConfig;


        public LeisaiIOCard()
        {
            _ioConfig = new LeisaiIOConfig();
            PrimDebugUI = new PrimDefaultConfigControl(this);
            PrimConfigUI = new PrimDefaultConfigControl(this);
            PrimOutputUI = new PrimDefaultConfigControl(this);
        }

        public LeisaiIOCard(XmlNode node)
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
                _ioConfig = XMLHelper.XMLToObject(xmlNode, typeof(LeisaiIOConfig)) as LeisaiIOConfig;

                if (_ioConfig != null)
                {
                    Name = _ioConfig.Name;
                    DevIndex = _ioConfig.DevIndex;
                    Node = _ioConfig.Node;
                    ConfigFilePath = _ioConfig.ConfigFilePath;


                    PrimTypeName = _ioConfig.PrimTypeName;
                    PrimId = _ioConfig.PrimId;
                }
            }
            else
                return -1;

            return iRet;
        }

        public XmlNode ExportConfig()
        {
            _ioConfig = _ioConfig ?? new LeisaiIOConfig();

            _ioConfig.Name = Name;
            _ioConfig.DevIndex = DevIndex;
            _ioConfig.Node = Node;
            _ioConfig.ConfigFilePath = ConfigFilePath;

            _ioConfig.PrimTypeName = PrimTypeName;
            _ioConfig.PrimId = PrimId;

            var node = XMLHelper.ObjectToXML(_ioConfig);

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
            return 0;
        }

        public int IPrimDispose()
        {
            return 0;
        }


        public int IPrimConnect(ref string result)
        {
            var ret = csLTDMC.LTDMC.nmc_set_connect_state((ushort)DevIndex, (ushort)Node, 1, (ushort)0);
            if(ret!=0)
            {
                throw new Exception($"leisai can connect fail {DevIndex} {Node} {ret}");
            }
            PrimConnStat = PrimConnState.Connected;
            return 0;
        }

        public int IPrimDisConnect(ref string result)
        {
            csLTDMC.LTDMC.nmc_set_connect_state((ushort)DevIndex, (ushort)Node, 0, (ushort)0);
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
            OnPrimDataRefresh?.Invoke(devname, context);
            return 0;
        }

        protected virtual int OnOnPrimOpLog(string devname, object log)
        {
            OnPrimOpLog?.Invoke(devname, log);
            return 0;
        }

        protected virtual int OnOnPrimStateChanged(string devname, object context)
        {
            OnPrimStateChanged?.Invoke(devname, context);
            return 0;
        }


        #region interface IP2PMotionCard

        public int DevIndex { get; set; }

        public int Node { get; set; }
        public string ConfigFilePath { get; set; }

        public void LoadConfigFile(string file)
        {
            //csLTDMC.LTDMC.dmc_download_configfile((ushort)DevIndex, file);
        }

        public int WriteSingleDOutput(int index, int i, int port, int status)
        {

            status = status == 1 ? 0 : 1;
            return csLTDMC.LTDMC.nmc_write_outbit((ushort)DevIndex, (ushort)Node, (ushort)port, (ushort)status);
        }

        public int ReadSingleDOutput(int index, int i, int port, out int status)
        {
            ushort val = 0;
            csLTDMC.LTDMC.nmc_read_outbit((ushort)DevIndex, (ushort)Node, (ushort)port, ref val);
            status = val;
            status = status == 0 ? 1 : 0;
            return 0;
        }

        public int ReadSingleDInput(int index, int i, int port, out int status)
        {
            ushort val = 0;
            csLTDMC.LTDMC.nmc_read_inbit((ushort)DevIndex, (ushort)Node, (ushort)port, ref val);
            status = val;
            status = status == 0 ? 1 : 0;
            return 0;
        }

        #endregion

        #region axis

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
            return csLTDMC.LTDMC.dmc_set_sevon_enable((ushort)DevIndex, (ushort)axis, (ushort)(enable ? 1 : 0));
        }

        public int AxisAbsMove(int index, int axis, int pos, int vel)
        {
            return csLTDMC.LTDMC.dmc_pmove((ushort)DevIndex, (ushort)axis, pos, 1);
        }

        public int AxisRelMove(int index, int axis, int step, int vel)
        {
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
            return state == 1;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion
    }
}