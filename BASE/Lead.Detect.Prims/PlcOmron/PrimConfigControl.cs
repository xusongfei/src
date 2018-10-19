using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimPlcOmron.OmronData;
using OMRON.Compolet.CIPCompolet64;

namespace Lead.Detect.PrimPlcOmron
{
    public delegate void UpdatePrimConnState(PrimConnState state);

    public delegate void UpdatePrimRunState(PrimRunState state);

    public delegate void MonitorVariableChanged(string valName, string valAddr, string valType, object valLast, object valCur);

    public partial class PrimConfigControl : UserControl
    {
        public event MonitorVariableChanged OnMonitorVariableChanged;

        private ActionState _actionState;
        //private BindingSource _bindingSource;
        private bool _boolTaskStart;
        private readonly OmronConfig _config;

        private bool _cycleReadFlag;
        private List<PLCParamRow> _dgvTable;
        private bool _getCommInfoFlag;
        private bool _getVarListFlag;
        private MyConfig _myConfig;
        private Dictionary<string, string> _nameDictionary;
        private readonly string _path = "d:\\my_config.xml";

        private readonly PrimOmron _primOmron;
        private int _selectedColumnIndex = -1;
        private int _selectedRowIndex = -1;

        private Thread _taskCycleRead;
        //private Task _taskUI;
        public Dictionary<string, bool> dicUpdateFlag = new Dictionary<string, bool>();

        public Dictionary<string, object> dicValues = new Dictionary<string, object>();

        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public PrimConfigControl(PrimOmron prim)
        {
            InitializeComponent();
            _primOmron = prim;
            _config = _primOmron._config;
        }

        public string PrimOmronName
        {
            set { tTaskBoxName.Text = value; }
        }

        private void AddRow(ref List<PLCParamRow> list, ref DataGridView dgv)
        {
            var row = new PLCParamRow();
            list.Add(row);
            var bindingSource = new BindingSource();
            bindingSource.DataSource = list;
            //dgv.DataSource = bindingSource;
        }


        private void btnAddRow_Click(object sender, EventArgs e)
        {
            dgvDataConfig.Rows.Add();
            //_primOmron.AddDataConfig();
        }

        private void btnConfigApply_Click(object sender, EventArgs e)
        {
            var rowCount = dgvDataConfig.Rows.Count;

            if (rowCount < 1) return;

            _primOmron._dataConfigs.ListDataConfig.Clear();

            for (var i = 0; i < rowCount; i++)
            {
                var dtConfig = new DataConfig();

                if (dgvDataConfig.Rows[i].Cells[0].Value == null)
                    dtConfig.DtName = "";
                else
                    dtConfig.DtName = (string) dgvDataConfig.Rows[i].Cells[0].Value;

                if (dgvDataConfig.Rows[i].Cells[1].Value == null)
                    dtConfig.DtType = "";
                else
                    dtConfig.DtType = (string) dgvDataConfig.Rows[i].Cells[1].Value;

                if (dgvDataConfig.Rows[i].Cells[2].Value == null)
                    dtConfig.DtSection = OmronDataSection.Other;
                else
                    dtConfig.DtSection = StrTrun2OmronDataSection((string) dgvDataConfig.Rows[i].Cells[2].Value);

                if (dgvDataConfig.Rows[i].Cells[3].Value == null)
                    dtConfig.DtAddress = "";
                else
                    dtConfig.DtAddress = (string) dgvDataConfig.Rows[i].Cells[3].Value;

                //dtConfig.DtModifyValue = (string)dgvDataConfig.Rows[i].Cells[4].Value;

                if (dgvDataConfig.Rows[i].Cells[6].Value == null)
                    dtConfig.DtRdMode = DataReadMode.Other;
                else
                    dtConfig.DtRdMode = StrTrun2OmronDataReadMode((string) dgvDataConfig.Rows[i].Cells[6].Value);

                //dtConfig.DtCurrentValue = "";
                if (dgvDataConfig.Rows[i].Cells[7].Value == null)
                    dtConfig.DtIsNotify = false;
                else
                    dtConfig.DtIsNotify = (bool) dgvDataConfig.Rows[i].Cells[7].Value;

                _primOmron._dataConfigs.ListDataConfig.Add(dtConfig);
            }
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            var _curSelectedIdx = dgvDataConfig.CurrentCell.RowIndex;
            dgvDataConfig.Rows.RemoveAt(_curSelectedIdx);
            //_primOmron.RemoveDataConfig(_idx);
        }

        private void btnGetClock_Click(object sender, EventArgs e)
        {
            try
            {
                txtClock.Text = njCompolet1.Clock.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetCommunication_Click(object sender, EventArgs e)
        {
            if (radioPeerAddress.Checked)
            {
                listViewOfEachValue.Items[2].SubItems[1].Text = njCompolet1.LocalPort.ToString();
                listViewOfEachValue.Items[3].SubItems[1].Text = njCompolet1.PeerAddress;
                listViewOfEachValue.Items[4].SubItems[1].Text = string.Empty;
                listViewOfEachValue.Items[5].SubItems[1].Text = string.Empty;
            }
            else if (radioRoutePath.Checked)
            {
                listViewOfEachValue.Items[5].SubItems[1].Text = njCompolet1.RoutePath;
                listViewOfEachValue.Items[2].SubItems[1].Text = string.Empty;
                listViewOfEachValue.Items[3].SubItems[1].Text = string.Empty;
            }

            listViewOfEachValue.Items[0].SubItems[1].Text = njCompolet1.Active.ToString();
            listViewOfEachValue.Items[1].SubItems[1].Text = njCompolet1.ConnectionType.ToString();
            listViewOfEachValue.Items[4].SubItems[1].Text = njCompolet1.UseRoutePath.ToString();
            listViewOfEachValue.Items[6].SubItems[1].Text = njCompolet1.IsConnected.ToString();
            listViewOfEachValue.Items[7].SubItems[1].Text = njCompolet1.TypeName;
            listViewOfEachValue.Items[8].SubItems[1].Text = njCompolet1.DontFragment.ToString();

            _primOmron._completInfo.Active = njCompolet1.Active;
            _primOmron._completInfo.ConnectionType = njCompolet1.ConnectionType.ToString();
            _primOmron._completInfo.UseRoutePath = njCompolet1.UseRoutePath;
            _primOmron._completInfo.IsConnected = njCompolet1.IsConnected;
            _primOmron._completInfo.TypeName = njCompolet1.TypeName;
            _primOmron._completInfo.DontFragment = njCompolet1.DontFragment;
        }

        private void btnGetMessageTimeLimit_Click(object sender, EventArgs e)
        {
            try
            {
                txtReceiveTimeLimit.Text = njCompolet1.ReceiveTimeLimit.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetPlcEncoding_Click(object sender, EventArgs e)
        {
            try
            {
                cmbPlcEncodingName.Text = njCompolet1.PlcEncoding.WebName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                switch (njCompolet1.RunMode)
                {
                    case NJCompolet.RunModeTypes.Program:
                        cmbRunMode.SelectedIndex = 0;
                        break;
                    case NJCompolet.RunModeTypes.Run:
                        cmbRunMode.SelectedIndex = 1;
                        break;
                }

                switch (njCompolet1.RunStatus)
                {
                    case 0x00:
                        cmbRunStatus.SelectedIndex = 0;
                        cmbCPUStatus.Text = "Normal";
                        break;
                    case 0x01:
                        cmbRunStatus.SelectedIndex = 1;
                        cmbCPUStatus.Text = "Normal";
                        break;
                    case 0x80:
                        cmbRunStatus.SelectedIndex = 0;
                        cmbCPUStatus.Text = "Standby";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetUnitName_Click(object sender, EventArgs e)
        {
            try
            {
                txtUnitName.Text = njCompolet1.UnitName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetVaiable_Click(object sender, EventArgs e)
        {
            try
            {
                listViewOfVariableNames.Items.Clear();
                string[] vars = null;
                if (chkSystemVariable.Checked)
                    vars = njCompolet1.SystemVariableNames;
                else
                    vars = njCompolet1.VariableNames;

                foreach (var var in vars)
                {
                    var accessablePath = GetAccessablePath(var);
                    DisplayVariableInfomation(accessablePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _primOmron.IPrimInit();
        }

        private void btnReadRaw_Click(object sender, EventArgs e)
        {
            try
            {
                var varname = txtBinaryVariableName.Text;
                object obj = njCompolet1.ReadRawData(varname);
                var info = njCompolet1.GetVariableInfo(varname);
                var val = ByteArrayToString(obj as byte[]);

                if (listViewOfVariableNames.SelectedItems.Count > 0)
                    if (listViewOfVariableNames.SelectedItems[0].SubItems[0].Text == varname)
                    {
                        listViewOfVariableNames.SelectedItems[0].SubItems.Add(string.Empty);
                        listViewOfVariableNames.SelectedItems[0].SubItems[2].Text = val;
                    }

                txtBinaryValue.Text = val;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.StackTrace, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReadRawDataMultiple_Click(object sender, EventArgs e)
        {
            try
            {
                var varlist = txtBinaryVariableName.Text.Replace(" ", string.Empty).Split(',');

                var retVals = njCompolet1.ReadRawDataMultiple(varlist);
                var multival = string.Empty;
                for (var index = 0; index < varlist.Length; index++)
                {
                    var varName = varlist[index];
                    var val = ByteArrayToString(retVals[varName] as byte[]);

                    if (listViewOfVariableNames.SelectedItems.Count > index)
                        if (listViewOfVariableNames.SelectedItems[index].SubItems[0].Text == varlist[index])
                        {
                            listViewOfVariableNames.SelectedItems[index].SubItems.Add(string.Empty);
                            listViewOfVariableNames.SelectedItems[index].SubItems[2].Text = val;
                        }

                    multival += val + ",";
                }

                multival = multival.TrimEnd(',');
                txtBinaryValue.Text = multival;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReadVariable_Click(object sender, EventArgs e)
        {
            try
            {
                var varname = txtVariableName.Text;
                var obj = njCompolet1.ReadVariable(varname);
                if (obj == null) throw new NotSupportedException();

                var info = njCompolet1.GetVariableInfo(varname);
                var str = GetValueOfVariables(obj);
                if (listViewOfVariableNames.SelectedItems.Count > 0)
                    if (listViewOfVariableNames.SelectedItems[0].SubItems[0].Text == varname)
                    {
                        listViewOfVariableNames.SelectedItems[0].SubItems.Add(string.Empty);
                        listViewOfVariableNames.SelectedItems[0].SubItems[2].Text = str;
                    }

                txtValue.Text = str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnReadVariableMultiple_Click(object sender, EventArgs e)
        {
            try
            {
                var varlist = txtVariableName.Text.Replace(" ", string.Empty).Split(',');

                var retVals = njCompolet1.ReadVariableMultiple(varlist);
                if (retVals == null) throw new NotSupportedException();

                var multival = string.Empty;
                for (var index = 0; index < varlist.Length; index++)
                {
                    var varName = varlist[index];
                    var val = retVals[varName];
                    var valStr = GetValueOfVariables(val);
                    if (listViewOfVariableNames.SelectedItems.Count > index)
                        if (listViewOfVariableNames.SelectedItems[index].SubItems[0].Text == varlist[index])
                        {
                            listViewOfVariableNames.SelectedItems[index].SubItems.Add(string.Empty);
                            listViewOfVariableNames.SelectedItems[index].SubItems[2].Text = valStr;
                        }

                    multival += valStr + ",";
                }

                multival = multival.TrimEnd(',');
                txtValue.Text = multival;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
        }

        private void btnSetClock_Click(object sender, EventArgs e)
        {
            try
            {
                njCompolet1.Clock = DateTime.Parse(txtClock.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetMessageTimeLimit_Click(object sender, EventArgs e)
        {
            try
            {
                njCompolet1.ReceiveTimeLimit = long.Parse(txtReceiveTimeLimit.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetPlcEncoding_Click(object sender, EventArgs e)
        {
            try
            {
                njCompolet1.PlcEncoding = Encoding.GetEncoding(cmbPlcEncodingName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cmbRunMode.Text)
                {
                    case "Program":
                        njCompolet1.RunMode = NJCompolet.RunModeTypes.Program;
                        break;
                    case "Run":
                        njCompolet1.RunMode = NJCompolet.RunModeTypes.Run;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetUnitName_Click(object sender, EventArgs e)
        {
            try
            {
                njCompolet1.UnitName = txtUnitName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
        }

        private void btnTableBrowse_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tBoxTableDataPath.Text = openFileDialog.FileName;

                if (string.IsNullOrEmpty(tBoxTableDataPath.Text)) return;

                if (!File.Exists(tBoxTableDataPath.Text))
                {
                    MessageBox.Show("选择文件不存在");
                    return;
                }

                _config.DataTablePath = tBoxTableDataPath.Text;
            }
        }

        private void btnTableLoad_Click(object sender, EventArgs e)
        {
            if (_primOmron != null)
            {
                var iRet = _primOmron.ImportDataTable(_config.DataTablePath);
                if (iRet != 0) return;

                DataTableUpdate();
            }
        }

        private void btnTableSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "*.xml|*.XML";
                saveFileDialog.FileName = "DataConfigs";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var savePath = saveFileDialog.FileName;
                    _primOmron.ExportDataTable(savePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWriteRaw_Click(object sender, EventArgs e)
        {
            try
            {
                // write
                if (txtBinaryVariableName.Text.StartsWith("_"))
                {
                    MessageBox.Show("The SystemVariable can not write!");
                    return;
                }

                var val = StringToByteArray(txtBinaryValue.Text.Replace("-", string.Empty));
                njCompolet1.WriteRawData(txtBinaryVariableName.Text, val);

                // read
                btnReadRaw_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnWriteVariable_Click(object sender, EventArgs e)
        {
            try
            {
                // write
                //--------------------------------------------------------------------------------
                var valWrite = txtVariableName.Text;
                if (valWrite.StartsWith("_"))
                {
                    MessageBox.Show("The SystemVariable can not write!");
                    return;
                }

                var val = RemoveBrackets(txtValue.Text);
                if (njCompolet1.GetVariableInfo(txtVariableName.Text).Type == VariableType.STRUCT) val = ObjectToByteArray(val);
                njCompolet1.WriteVariable(txtVariableName.Text, val);

                // read
                btnReadVariable_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string ByteArrayToString(byte[] ba)
        {
            if (ba.Length == 0)
                return string.Empty;
            return BitConverter.ToString(ba);
        }

        private void cBoxMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxMonitor.Checked)
            {
                CycleReadTaskStart();
                tmrUpdate.Start();
            }
            else
            {
                CycleReadTaskSuspend();
                tmrUpdate.Stop();
            }
        }

        private bool CheckDataAddress(string address, string dataType)
        {
            var ret = false;
            Regex rex;
            switch (dataType)
            {
                case StruDataType.WORD:
                    rex = new Regex(@"^\d+$");
                    ret = rex.IsMatch(address);
                    break;
                case StruDataType.BOOL:
                    rex = new Regex(@"^([0-9]{1,}[.][0-9]{1,2})$");
                    ret = rex.IsMatch(address);
                    break;
            }

            return ret;
        }

        private bool CheckDataModify(string address, string dataType)
        {
            var ret = false;
            Regex rex;
            switch (dataType)
            {
                case StruDataType.WORD:
                    rex = new Regex(@"^\d+$");
                    ret = rex.IsMatch(address);
                    break;
                case StruDataType.BOOL:
                    rex = new Regex(@"^([0-1]{1,1})$");
                    ret = rex.IsMatch(address);
                    break;
            }

            return ret;
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                njCompolet1.Active = chkActive.Checked;
                if (chkActive.Checked)
                    if (!njCompolet1.IsConnected)
                    {
                        if (radioPeerAddress.Checked)
                            MessageBox.Show("Connection failed !" + Environment.NewLine + "Please check PeerAddress.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                            MessageBox.Show("Connection failed !" + Environment.NewLine + "Please check RoutePath.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        njCompolet1.Active = false;
                        chkActive.Checked = false;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                njCompolet1.Active = false;
                chkActive.Checked = false;
            }

            _primOmron._completInfo.Active = njCompolet1.Active;
        }

        private void chkDoNotFragment_CheckedChanged(object sender, EventArgs e)
        {
            njCompolet1.DontFragment = chkDoNotFragment.Checked;
        }

        private void cmbConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConnectionType.SelectedIndex == 0)
                njCompolet1.ConnectionType = ConnectionType.UCMM;
            else
                njCompolet1.ConnectionType = ConnectionType.Class3;
        }

        private void CycleRead()
        {
            // 阻塞等触发
            while (_cycleReadFlag)
            {
                if (_getVarListFlag)
                    foreach (var config in _primOmron._dataConfigs.ListDataConfig)
                        ReadSingleVariable(config);
                Application.DoEvents();
                Thread.Sleep(100);
                Thread.Sleep(_config.CycleTime);
            }
        }

        public void CycleReadTaskStart()
        {
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (!_primOmron._completInfo.Active) SetActiveFlag(true);

                if (!_getCommInfoFlag)
                {
                    GetCommunicationInfo();
                    _getCommInfoFlag = true;
                }

                //GetSysVariableList();
                if (!_getVarListFlag)
                {
                    GetVariableList();
                    _getVarListFlag = true;
                }
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {
            }

            if (_taskCycleRead == null)
            {
                _taskCycleRead = new Thread(() => CycleRead());
                _cycleReadFlag = true;
                _taskCycleRead.Start();
            }

            if (_taskCycleRead.ThreadState == ThreadState.Running)
                return;
            //_taskCycleRead.Resume();
        }

        public void CycleReadTaskSuspend()
        {
            //_taskCycleRead.Suspend();
        }

        private void DatagrideviewToTable(DataGridView dgv, ref List<PLCParamRow> list, ref Dictionary<string, string> dic, out int errorRowInex)
        {
            errorRowInex = -1; //-1表示无错误
            var rowCount = dgv.Rows.Count;
            dic.Clear();
            var list1 = new List<PLCParamRow>();
            for (var i = 0; i < rowCount; i++)
            {
                var row = new PLCParamRow();
                row.DataName = (string) dgv.Rows[i].Cells[0].Value;
                row.DataType = (string) dgv.Rows[i].Cells[1].Value;
                row.DataSection = (string) dgv.Rows[i].Cells[2].Value;
                row.DataAddress = (string) dgv.Rows[i].Cells[3].Value;
                row.DataModifyValue = (string) dgv.Rows[i].Cells[4].Value;
                row.DataCurrentValue = "";
                if (row.DataName.Trim() == "")
                {
                    errorRowInex = i + 1;
                    return;
                }

                if (row.DataType == "")
                {
                    errorRowInex = i + 1;
                    return;
                }

                if (row.DataSection == "")
                {
                    errorRowInex = i + 1;
                    return;
                }

                if (CheckDataAddress(row.DataAddress, row.DataType) == false)
                {
                    errorRowInex = i + 1;
                    return;
                }

                list1.Add(row);
                string address = null;
                string index = null;
                switch (row.DataType)
                {
                    case StruDataType.BOOL:
                        SpitAddress(row.DataAddress, out address, out index);
                        break;
                    case StruDataType.WORD:
                        address = row.DataAddress;
                        index = "999";
                        break;
                    default:
                        break;
                }

                var value = string.Format("{0}{1},{2}", row.DataSection, address, index);
                dic.Add(row.DataName, value);
                row = null;
            }

            list = list1;
            list1 = null;
        }

        public void DataTableUpdate()
        {
            if (_primOmron == null) return;

            if (_primOmron._dataConfigs == null) return;

            if (_primOmron._dataConfigs.ListDataConfig == null) return;

            dgvDataConfig.Rows.Clear();

            if (_primOmron._dataConfigs.ListDataConfig.Count < 1) return;

            foreach (var config in _primOmron._dataConfigs.ListDataConfig)
            {
                var index = dgvDataConfig.Rows.Add();
                dgvDataConfig.Rows[index].Cells[0].Value = config.DtName;
                dgvDataConfig.Rows[index].Cells[1].Value = config.DtType;
                dgvDataConfig.Rows[index].Cells[2].Value = config.DtSection.ToString();
                dgvDataConfig.Rows[index].Cells[3].Value = config.DtAddress;
                dgvDataConfig.Rows[index].Cells[6].Value = config.DtRdMode.ToString();
                dgvDataConfig.Rows[index].Cells[7].Value = config.DtIsNotify;
            }

            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = _primOmron._dataConfigs.ListDataConfig;
            //dgvDataConfig.DataSource = bindingSource;
        }

        private void DictionaryToList(Dictionary<string, string> dic, out List<PLCParamDictionary> list)
        {
            list = null;
            var name = new List<string>();
            var value = new List<string>();
            foreach (var item in dic)
            {
                name.Add(item.Key);
                value.Add(item.Value);
            }

            var listTemp = new List<PLCParamDictionary>();
            var count = name.Count;
            var nameArrary = name.ToArray();
            var valueArrary = value.ToArray();
            for (var i = 0; i < count; i++)
            {
                var dr = new PLCParamDictionary();
                dr.Key = nameArrary[i];
                dr.Value = valueArrary[i];
                listTemp.Add(dr);
                dr = null;
            }

            list = listTemp;
            listTemp = null;
            name = null;
            value = null;
        }

        private void DisplayVariableInfomation(string str)
        {
            var item = listViewOfVariableNames.Items.Add(str);
            var info = njCompolet1.GetVariableInfo(str);
            if (info.IsArray)
            {
                var text = info.Type.ToString();
                foreach (var num in info.NumberOfElements) text += "[" + num + "]";
                item.SubItems.Add(text);
            }
            else
            {
                item.SubItems.Add(info.Type.ToString());
            }

            item.SubItems.Add(string.Empty);
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            var cycTime = -1;
            int.TryParse(doUpDown.Text, out cycTime);
            _config.CycleTime = cycTime;
        }

        private string GetAccessablePath(string path)
        {
            var newPath = string.Empty;
            newPath += path;
            var info = njCompolet1.GetVariableInfo(path);
            if (IsMustElementAccess(info))
            {
                // get only first element
                for (var i = 0; i < info.Dimension; i++) newPath += "[" + info.StartArrayElements[i] + "]";
                return GetAccessablePath(newPath);
            }

            if (IsMustMemberAccess(info))
            {
                // get only first member
                newPath += "." + info.StructMembers[0].Name;
                return GetAccessablePath(newPath);
            }

            return newPath;
        }

        public void GetCommunicationInfo()
        {
            if (radioPeerAddress.Checked)
            {
                listViewOfEachValue.Items[2].SubItems[1].Text = njCompolet1.LocalPort.ToString();
                listViewOfEachValue.Items[3].SubItems[1].Text = njCompolet1.PeerAddress;
                listViewOfEachValue.Items[4].SubItems[1].Text = string.Empty;
                listViewOfEachValue.Items[5].SubItems[1].Text = string.Empty;

                _primOmron._completInfo.LocalPort = njCompolet1.LocalPort;
                _primOmron._completInfo.PeerAddress = njCompolet1.PeerAddress;
            }
            else if (radioRoutePath.Checked)
            {
                listViewOfEachValue.Items[5].SubItems[1].Text = njCompolet1.RoutePath;
                listViewOfEachValue.Items[2].SubItems[1].Text = string.Empty;
                listViewOfEachValue.Items[3].SubItems[1].Text = string.Empty;

                _primOmron._completInfo.RoutePath = njCompolet1.RoutePath;
            }

            listViewOfEachValue.Items[0].SubItems[1].Text = njCompolet1.Active.ToString();
            listViewOfEachValue.Items[1].SubItems[1].Text = njCompolet1.ConnectionType.ToString();
            listViewOfEachValue.Items[4].SubItems[1].Text = njCompolet1.UseRoutePath.ToString();
            listViewOfEachValue.Items[6].SubItems[1].Text = njCompolet1.IsConnected.ToString();
            listViewOfEachValue.Items[7].SubItems[1].Text = njCompolet1.TypeName;
            listViewOfEachValue.Items[8].SubItems[1].Text = njCompolet1.DontFragment.ToString();

            _primOmron._completInfo.Active = njCompolet1.Active;
            _primOmron._completInfo.ConnectionType = njCompolet1.ConnectionType.ToString();
            _primOmron._completInfo.UseRoutePath = njCompolet1.UseRoutePath;
            _primOmron._completInfo.IsConnected = njCompolet1.IsConnected;
            _primOmron._completInfo.TypeName = njCompolet1.TypeName;
            _primOmron._completInfo.DontFragment = njCompolet1.DontFragment;
        }

        private void GetLoacalIP(out string ip)
        {
            ip = "";
            var name = Dns.GetHostName();
            var ipadrlist = Dns.GetHostAddresses(name);
            foreach (var ipa in ipadrlist)
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    ip = ipa.ToString();
        }

        private void GetParam(string path, out MyConfig myConfig)
        {
            myConfig = null;
            myConfig = (MyConfig) LoadFromXml(typeof(MyConfig), path);
        }

        public void GetSysVariableList()
        {
            try
            {
                if (_primOmron._completInfo.SysVariableList == null)
                    _primOmron._completInfo.SysVariableList = new List<string>();
                else
                    _primOmron._completInfo.SysVariableList.Clear();

                listViewOfVariableNames.Items.Clear();
                string[] vars = null;

                vars = njCompolet1.SystemVariableNames;

                foreach (var var in vars)
                {
                    _primOmron._completInfo.SysVariableList.Add(var);

                    var accessablePath = GetAccessablePath(var);
                    DisplayVariableInfomation(accessablePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetValueOfVariables(object val)
        {
            var valStr = string.Empty;
            if (val.GetType().IsArray)
            {
                var valArray = val as Array;
                if (valArray.Rank == 1)
                {
                    valStr += "[";
                    foreach (var a in valArray) valStr += GetValueString(a) + ",";
                    valStr = valStr.TrimEnd(',');
                    valStr += "]";
                }
                else if (valArray.Rank == 2)
                {
                    for (var i = 0; i <= valArray.GetUpperBound(0); i++)
                    {
                        valStr += "[";
                        for (var j = 0; j <= valArray.GetUpperBound(1); j++) valStr += GetValueString(valArray.GetValue(i, j)) + ",";
                        valStr = valStr.TrimEnd(',');
                        valStr += "]";
                    }
                }
                else if (valArray.Rank == 3)
                {
                    for (var i = 0; i <= valArray.GetUpperBound(0); i++)
                    for (var j = 0; j <= valArray.GetUpperBound(1); j++)
                    {
                        valStr += "[";
                        for (var z = 0; z <= valArray.GetUpperBound(2); z++) valStr += GetValueString(valArray.GetValue(i, j, z)) + ",";
                        valStr = valStr.TrimEnd(',');
                        valStr += "]";
                    }
                }
            }
            else
            {
                valStr = GetValueString(val);
            }

            return valStr;
        }

        private string GetValueString(object val)
        {
            if (val is float || val is double)
                return string.Format("{0:R}", val);
            return val.ToString();
        }

        public void GetVariableList()
        {
            try
            {
                if (_primOmron._completInfo.VariableList == null)
                    _primOmron._completInfo.VariableList = new List<string>();
                else
                    _primOmron._completInfo.VariableList.Clear();

                listViewOfVariableNames.Items.Clear();
                //    String[] vars = null;


                foreach (var config in _primOmron._dataConfigs.ListDataConfig)
                    if (config.DtAddress.Contains("."))
                    {
                        var strs = config.DtAddress.Split('.');

                        if (!string.IsNullOrEmpty(strs[0]))
                            if (!_primOmron._completInfo.VariableList.Contains(strs[0]))
                            {
                                _primOmron._completInfo.VariableList.Add(strs[0]);
                                var accessablePath = GetAccessablePath(strs[0]);
                                DisplayVariableInfomation(accessablePath);
                            }
                    }
                //vars = this.njCompolet1.VariableNames;

                //foreach (string var in vars)
                //{
                //    _primOmron._completInfo.VariableList.Add(var);

                //    String accessablePath = this.GetAccessablePath(var);
                //    this.DisplayVariableInfomation(accessablePath);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void groupBoxClock_Enter(object sender, EventArgs e)
        {
        }

        private bool IsMustElementAccess(VariableInfo info)
        {
            var toReturn = false;
            if (info.IsArray)
                if (info.Type == VariableType.STRING || info.Type == VariableType.UNION)
                    toReturn = true;

            return toReturn;
        }

        private bool IsMustMemberAccess(VariableInfo info)
        {
            var toReturn = false;

            if (info.Type == VariableType.UNION) toReturn = true;

            return toReturn;
        }

        private void lbTitle_Click(object sender, EventArgs e)
        {
        }

        private void ListToDictionary(List<PLCParamDictionary> list, out Dictionary<string, string> dic)
        {
            dic = null;
            dic = list.ToDictionary(item => item.Key, item => item.Value);
        }

        private void listViewOfVariableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewOfVariableNames.SelectedItems.Count == 0) return;
            if (listViewOfVariableNames.SelectedItems.Count == 1)
            {
                txtVariableName.Text = listViewOfVariableNames.SelectedItems[0].Text;
                txtBinaryVariableName.Text = listViewOfVariableNames.SelectedItems[0].Text;
            }
            else
            {
                var varlist = new string[listViewOfVariableNames.SelectedItems.Count];
                var variables = string.Empty;
                for (var i = 0; i < listViewOfVariableNames.SelectedItems.Count; i++)
                {
                    varlist[i] = listViewOfVariableNames.SelectedItems[i].Text;
                    variables += varlist[i] + ",";
                }

                variables = variables.TrimEnd(',');
                txtVariableName.Text = variables;
                txtBinaryVariableName.Text = variables;
            }

            txtValue.Text = string.Empty;
            txtBinaryValue.Text = string.Empty;
        }

        public object LoadFromXml(Type type, string filename)
        {
            using (XmlReader xRead = new XmlTextReader(filename))
            {
                var sl = new XmlSerializer(type);
                var obj = sl.Deserialize(xRead);
                xRead.Close();
                return obj;
            }
        }

        private void numPortNo_ValueChanged(object sender, EventArgs e)
        {
            njCompolet1.LocalPort = (int) numPortNo.Value;
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj is Array)
            {
                var arr = obj as Array;
                var bin = new byte[arr.Length];
                for (var i = 0; i < bin.Length; i++) bin[i] = Convert.ToByte(arr.GetValue(i));
                return bin;
            }

            return new byte[1] {Convert.ToByte(obj)};
        }

        private void PrimConfigControl_Load(object sender, EventArgs e)
        {
            PrimOmronName = _primOmron.Name;

            switch (_config.CommMode)
            {
                case OmronCommMode.Fins:
                    comBoxCommMode.Text = "Fins";
                    break;
                case OmronCommMode.Complet:
                    comBoxCommMode.Text = "Complet";
                    break;
            }

            tBoxTableDataPath.Text = _config.DataTablePath;

            doUpDown.Text = _config.CycleTime.ToString();

            DataTableUpdate();

            CheckForIllegalCrossThreadCalls = false;
            if (File.Exists(_path)) GetParam(_path, out _myConfig);
            if (_myConfig == null)
            {
                _myConfig = new MyConfig();
                _myConfig.DgvTable = new List<PLCParamRow>();
                _myConfig.QueryList = new List<PLCParamDictionary>();
            }

            tBoxDstIp.Text = _myConfig.DstIp;
            tBoxDstPort.Text = _myConfig.DstPort;
            tBoxLocalIp.Text = _myConfig.LocalIp;


            _dgvTable = _myConfig.DgvTable;
            ListToDictionary(_myConfig.QueryList, out _nameDictionary);
            TableToDatagridview(_dgvTable, _nameDictionary, false, ref dgvDataConfig);

            //_dgvTable = new List<PLCParamRow> { };
            //_nameDictionary = new Dictionary<string, string> { };
            dgvDataConfig.AutoGenerateColumns = false;
            _actionState = ActionState.write;
            SetDoubleBufferedForDataGridView(ref dgvDataConfig, true);
            _boolTaskStart = true;
            //_taskUI = new Task(UpdateUI);
            //_taskUI.Start();

            //_bindingSource = new BindingSource();
            //_bindingSource.DataSource = _primOmron._listDataConfig;
            //dgvDataConfig.DataSource = new BindingList<DataConfig>(_primOmron._listDataConfig); ;
        }

        private void radioPeerAddress_CheckedChanged(object sender, EventArgs e)
        {
            labelPortNo.Enabled = radioPeerAddress.Checked;
            numPortNo.Enabled = radioPeerAddress.Checked;
            labelIPAddress.Enabled = radioPeerAddress.Checked;
            txtIPAddress.Enabled = radioPeerAddress.Checked;
            labelRoutePath.Enabled = !radioPeerAddress.Checked;
            txtRoutePath.Enabled = !radioPeerAddress.Checked;

            njCompolet1.UseRoutePath = !radioPeerAddress.Checked;
        }

        private void ReadSingleVariable(DataConfig config)
        {
            if (_config.CommMode == OmronCommMode.Complet)
                ReadSingleVariableFromComplet(config);
            else if (_config.CommMode == OmronCommMode.Fins) ReadSingleVariableFromFins(config);
        }

        private void ReadSingleVariableFromComplet(DataConfig config)
        {
            if (string.IsNullOrEmpty(config.DtName)) return;

            if (config.DtRdMode == DataReadMode.NonCycle) return;

            var addr = config.DtAddress;

            if (config.DtAddress.Contains("."))
            {
                var strs = config.DtAddress.Split('.');
                if (strs.Length < 0) return;

                if (string.IsNullOrEmpty(strs[0])) return;

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) return;

            var obj = njCompolet1.ReadVariable(config.DtAddress);

            if (!dicValues.ContainsKey(config.DtName))
            {
                //Call Event 调用事件
                if (config.DtIsNotify && OnMonitorVariableChanged != null) OnMonitorVariableChanged(config.DtName, config.DtAddress, config.DtType, null, obj);

                dicValues.Add(config.DtName, obj);
                dicUpdateFlag.Add(config.DtName, true);
            }
            else
            {
                var lastVal = dicValues[config.DtName];
                var lastValStr = lastVal.ToString();
                var curValStr = obj.ToString();
                if (!lastValStr.Equals(curValStr))
                {
                    //Call Event 调用事件
                    if (config.DtIsNotify && OnMonitorVariableChanged != null) OnMonitorVariableChanged(config.DtName, config.DtAddress, config.DtType, lastVal, obj);

                    dicValues[config.DtName] = obj;
                    dicUpdateFlag[config.DtName] = true;
                }
            }
        }

        private void ReadSingleVariableFromFins(DataConfig config)
        {
        }

        public object ReadVariableFromAddr(string valAddr)
        {
            object val = null;
            if (string.IsNullOrEmpty(valAddr)) return null;

            var addr = valAddr;

            if (valAddr.Contains("."))
            {
                var strs = valAddr.Split('.');
                if (strs.Length < 0) return null;

                if (string.IsNullOrEmpty(strs[0])) return null;

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) return null;

            val = njCompolet1.ReadVariable(valAddr);

            return val;
        }

        public object ReadVariableFromName(string valName)
        {
            object val = null;
            if (string.IsNullOrEmpty(valName)) return null;

            var valAddr = "";

            foreach (var config in _primOmron._dataConfigs.ListDataConfig)
                if (config.DtName == valName)
                {
                    valAddr = config.DtAddress;
                    break;
                }

            if (string.IsNullOrEmpty(valAddr)) return null;

            var addr = valAddr;

            if (valAddr.Contains("."))
            {
                var strs = valAddr.Split('.');
                if (strs.Length < 0) return null;

                if (string.IsNullOrEmpty(strs[0])) return null;

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) return null;

            val = njCompolet1.ReadVariable(valAddr);

            return val;
        }

        private object RemoveBrackets(string val)
        {
            object obj = string.Empty;
            if (val.IndexOf("[") >= 0)
            {
                var str = val.Trim('[', ']');
                str = str.Replace("][", ",");
                obj = str.Split(',');
            }
            else
            {
                obj = val;
            }

            return obj;
        }

        private void RemoveRow(ref List<PLCParamRow> list, ref DataGridView dgv)
        {
            //_selectedRowIndex在cellClick中获取，表示当前选中行
            if (_selectedRowIndex > -1)
            {
                list.RemoveAt(_selectedRowIndex);
                var bindingSource = new BindingSource();
                bindingSource.DataSource = list;
                //dgv.DataSource = bindingSource;
            }

            if (_selectedRowIndex == 0) _selectedRowIndex = -1;
        }

        private void SaveParam(string path, MyConfig myConfig)
        {
            SaveToXml(path, typeof(MyConfig), myConfig);
        }

        public void SaveToXml(string filename, Type type, object target)
        {
            using (var xWrite = new XmlTextWriter(filename, null))
            {
                var sl = new XmlSerializer(type);
                sl.Serialize(xWrite, target);
                xWrite.Close();
            }
        }

        public void SetActiveFlag(bool active)
        {
            try
            {
                chkActive.Checked = active;
                njCompolet1.Active = chkActive.Checked;
                if (chkActive.Checked)
                    if (!njCompolet1.IsConnected)
                    {
                        if (radioPeerAddress.Checked)
                            MessageBox.Show("Connection failed !" + Environment.NewLine + "Please check PeerAddress.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                            MessageBox.Show("Connection failed !" + Environment.NewLine + "Please check RoutePath.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        njCompolet1.Active = false;
                        chkActive.Checked = false;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                njCompolet1.Active = false;
                chkActive.Checked = false;
            }

            _primOmron._completInfo.Active = njCompolet1.Active;
        }

        //设置datagridview为双缓存，解决datagridview更新时闪烁的问题
        public void SetDoubleBufferedForDataGridView(ref DataGridView control, bool isDoubleBuffered)
        {
            //获取控件的Type
            var dgvType = control.GetType();
            //通过Type获取控件的指定属性
            //BindingFlags.Instance                    指定实例成员将包括在搜索中
            //BindingFlags.NonPublic                 指定非公共成员将包括在搜索中
            var properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            //为控件的属性设置值
            properInfo.SetValue(control, isDoubleBuffered, null);
        }

        public void SetPrimConnState(PrimConnState state)
        {
            if (tBoxConnState.InvokeRequired)
            {
                var update = new UpdatePrimConnState(SetPrimConnState);
                Invoke(update, state);
            }
            else
            {
                switch (_primOmron.PrimConnStat)
                {
                    case PrimConnState.UnConnected:
                        tBoxConnState.BackColor = Color.Khaki;
                        tBoxConnState.Text = "UnConnected";
                        break;
                    case PrimConnState.Connected:
                        tBoxConnState.BackColor = Color.LightGreen;
                        tBoxConnState.Text = "Connected";
                        break;
                    case PrimConnState.Other:
                        tBoxConnState.BackColor = Color.Khaki;
                        tBoxConnState.Text = "UnConnected";
                        break;
                }
            }
        }

        public void SetPrimRunState(PrimRunState state)
        {
            if (tBoxRunState.InvokeRequired)
            {
                var update = new UpdatePrimRunState(SetPrimRunState);
                Invoke(update, state);
            }
            else
            {
                switch (_primOmron.PrimRunStat)
                {
                    case PrimRunState.Idle:
                        tBoxRunState.BackColor = Color.Khaki;
                        tBoxRunState.Text = "Idle";
                        break;
                    case PrimRunState.Running:
                        tBoxRunState.BackColor = Color.LightGreen;
                        tBoxRunState.Text = "Running";
                        break;
                    case PrimRunState.Err:
                        tBoxRunState.BackColor = Color.SandyBrown;
                        tBoxRunState.Text = "Err";
                        break;
                    case PrimRunState.Other:
                        tBoxRunState.BackColor = Color.Khaki;
                        tBoxRunState.Text = "Idle";
                        break;
                }
            }
        }

        private void SpitAddress(string address, out string newAddress, out string index)
        {
            newAddress = null;
            index = null;
            var splitedArrary = address.Split('.');
            newAddress = splitedArrary[0];
            index = splitedArrary[1];
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private byte[] StringToByteArray(string hex)
        {
            if (hex == string.Empty) return new byte[0];
            var byteNumber = hex.Length / 2;
            var bytes = new byte[byteNumber];
            for (var i = 0; i < hex.Length; i += 2) bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private DataReadMode StrTrun2OmronDataReadMode(string dtRdModeStr)
        {
            var rdMode = DataReadMode.Other;
            switch (dtRdModeStr)
            {
                case "Cycle":
                    rdMode = DataReadMode.Cycle;
                    break;
                case "NonCycle":
                    rdMode = DataReadMode.NonCycle;
                    break;
                case "Other":
                    rdMode = DataReadMode.Other;
                    break;
                default:
                    rdMode = DataReadMode.Other;
                    break;
            }

            return rdMode;
        }

        private OmronDataSection StrTrun2OmronDataSection(string dtSectionStr)
        {
            var section = OmronDataSection.Other;
            switch (dtSectionStr)
            {
                case "CIO":
                    section = OmronDataSection.CIO;
                    break;
                case "WR":
                    section = OmronDataSection.WR;
                    break;
                case "D":
                    section = OmronDataSection.D;
                    break;
                case "HR":
                    section = OmronDataSection.HR;
                    break;
                case "AR":
                    section = OmronDataSection.AR;
                    break;
                case "C":
                    section = OmronDataSection.C;
                    break;
                case "T":
                    section = OmronDataSection.T;
                    break;
                case "Other":
                    section = OmronDataSection.Other;
                    break;
                default:
                    section = OmronDataSection.Other;
                    break;
            }

            return section;
        }

        private Type StrTurn2OmronDataType(string dtType)
        {
            var type = typeof(bool);

            switch (dtType)
            {
                case "bool":
                    type = typeof(bool);
                    break;
                case "byte":
                    type = typeof(byte);
                    break;
                case "short":
                    type = typeof(short);
                    break;
                case "int16":
                    type = typeof(short);
                    break;
                case "int32":
                    type = typeof(int);
                    break;
                case "int64":
                    type = typeof(long);
                    break;
                case "float":
                    type = typeof(float);
                    break;
                case "double":
                    type = typeof(double);
                    break;
                case "long":
                    type = typeof(long);
                    break;
                case "string":
                    type = typeof(string);
                    break;
                case "array":
                    type = typeof(Array);
                    break;
                case "struct":
                    break;
                default:
                    type = typeof(bool);
                    break;
            }

            return type;
        }

        private void TableToDatagridview(List<PLCParamRow> list, Dictionary<string, string> dictionary, bool isRead, ref DataGridView dgv)
        {
            string name = null;
            if (isRead)
                foreach (var row in list)
                {
                    var read = -1;
                    name = row.DataName;
                    var dataType = row.DataType;
                    switch (dataType)
                    {
                        case StruDataType.BOOL:
                            _primOmron.ReadBitByName(dictionary, name, out read);
                            break;
                        case StruDataType.WORD:
                            _primOmron.ReadWordByName(dictionary, name, out read);
                            break;
                    }

                    row.DataCurrentValue = read.ToString();
                    Thread.Sleep(20);
                }

            var bindingSource = new BindingSource();
            bindingSource.DataSource = list;
            //dgv.DataSource = bindingSource;
            bindingSource = null;
        }

        private void txtIPAddress_TextChanged(object sender, EventArgs e)
        {
            njCompolet1.PeerAddress = txtIPAddress.Text;
        }

        private void txtRoutePath_TextChanged(object sender, EventArgs e)
        {
            njCompolet1.RoutePath = txtRoutePath.Text;
        }

        private void UpdateUI()
        {
            while (_boolTaskStart)
            {
                Thread.Sleep(1000);
                //SetButtonColor(btnMonitor, _actionState);
                if (_primOmron._omronUdp.IsConnected)
                {
                    tBoxConnState.Text = "Connected";
                    tBoxConnState.BackColor = Color.Green;

                    if (_actionState == ActionState.read) TableToDatagridview(_dgvTable, _nameDictionary, true, ref dgvDataConfig);
                }
                else
                {
                    tBoxConnState.Text = "UnConnected";
                    tBoxConnState.BackColor = Color.Red;
                }
            }
        }

        public int WriteVariableFromAddr(string valAddr, object val)
        {
            var iRet = -1;
            if (string.IsNullOrEmpty(valAddr)) return iRet;

            var addr = valAddr;

            if (valAddr.Contains("."))
            {
                var strs = valAddr.Split('.');
                if (strs.Length < 0) return iRet;

                if (string.IsNullOrEmpty(strs[0])) return iRet;

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) return iRet;

            njCompolet1.WriteVariable(valAddr, val);

            return iRet;
        }

        public int WriteVariableFromName(string valName, object val)
        {
            var iRet = -1;
            if (string.IsNullOrEmpty(valName)) return iRet;

            var valAddr = "";

            foreach (var config in _primOmron._dataConfigs.ListDataConfig)
                if (config.DtName == valName)
                {
                    valAddr = config.DtAddress;
                    break;
                }

            if (string.IsNullOrEmpty(valAddr)) return iRet;

            var addr = valAddr;

            if (valAddr.Contains("."))
            {
                var strs = valAddr.Split('.');
                if (strs.Length < 0) return iRet;

                if (string.IsNullOrEmpty(strs[0])) return iRet;

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) return iRet;

            njCompolet1.WriteVariable(valAddr, val);

            return iRet;
        }

        #region "控件事件代码"

        private void btnApply_Click(object sender, EventArgs e)
        {
            //int errorRowInex;
            //DatagrideviewToTable(dgvDataConfig, ref _dgvTable, ref _nameDictionary, out errorRowInex);
            //if (errorRowInex != -1)
            //{
            //    MessageBox.Show(string.Format("第{0}行 设置报错", errorRowInex));
            //    _actionState = ActionState.error;
            //    return;
            //}

            if (!string.IsNullOrEmpty(tBoxDstIp.Text)) _config.DstIp = tBoxDstIp.Text;

            if (!string.IsNullOrEmpty(tBoxDstPort.Text)) _config.DstPort = tBoxDstPort.Text;

            if (!string.IsNullOrEmpty(tBoxLocalIp.Text)) _config.LocalIp = tBoxLocalIp.Text;

            if (!string.IsNullOrEmpty(comBoxCommMode.Text))
                switch (comBoxCommMode.Text)
                {
                    case "Fins":
                        _config.CommMode = OmronCommMode.Fins;
                        break;
                    case "Complet":
                        _config.CommMode = OmronCommMode.Complet;
                        break;
                }

            if (!string.IsNullOrEmpty(tBoxTableDataPath.Text)) _config.DataTablePath = tBoxTableDataPath.Text;

            //int cycTime = -1;
            //int.TryParse(doUpDown.Text, out cycTime);
            //_config.CycleTime = cycTime;

            //_myConfig.DgvTable = _dgvTable;
            //List<PLCParamDictionary> list = null;
            //DictionaryToList(_nameDictionary, out list);
            //_myConfig.QueryList = list;
            //_actionState = ActionState.applyed;
            //list = null;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var port = -1;
            var ipDis = tBoxDstIp.Text.Trim();
            try
            {
                port = Convert.ToInt32(tBoxDstPort.Text.Trim());
            }
            catch
            {
            }

            var ipLocal = tBoxLocalIp.Text.Trim();
            if (ipDis == "")
            {
                MessageBox.Show("PLC的IP不能设置为空！！");
                return;
            }

            if (port < 1)
            {
                MessageBox.Show("端口号不能小于1 ！！");
                return;
            }

            if (ipLocal == "")
            {
                MessageBox.Show("本机IP不能设置为空！！");
                return;
            }

            var ret = _primOmron._omronUdp.Init(ipDis, port, ipLocal);
            if (ret == false)
            {
                tBoxConnState.BackColor = Color.Red;
                MessageBox.Show("connect fail !!");
            }
        }

        private void 增加行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _actionState = ActionState.write;
            Thread.Sleep(2000);
            AddRow(ref _dgvTable, ref dgvDataConfig);
        }

        private void 删除行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_actionState != ActionState.read) RemoveRow(ref _dgvTable, ref dgvDataConfig);
        }

        private void dgvDataConfig_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _actionState = ActionState.write;
        }

        private void dgvDataConfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            var rowInex = _selectedRowIndex;
            var columnIndex = _selectedColumnIndex;

            var newVal = Convert.ToString(dgv.Rows[rowInex].Cells[4].Value).Trim();
            if (string.IsNullOrEmpty(newVal)) return;

            var valAddr = Convert.ToString(dgv.Rows[rowInex].Cells[3].Value).Trim();
            if (string.IsNullOrEmpty(valAddr)) return;

            try
            {
                // write
                //--------------------------------------------------------------------------------
                if (valAddr.StartsWith("_"))
                {
                    MessageBox.Show("The SystemVariable can not write!");
                    return;
                }

                var val = RemoveBrackets(newVal);
                if (njCompolet1.GetVariableInfo(valAddr).Type == VariableType.STRUCT) val = ObjectToByteArray(val);
                njCompolet1.WriteVariable(valAddr, val);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            /*
            bool isError = true;
            DataGridView dgv = sender as DataGridView;
            int rowInex = _selectedRowIndex;
            int columnIndex = _selectedColumnIndex;
            string cellValue0 = Convert.ToString(dgv.Rows[rowInex].Cells[0].Value).Trim();
            string cellValue1 = Convert.ToString(dgv.Rows[rowInex].Cells[1].Value);
            string cellValue2 = Convert.ToString(dgv.Rows[rowInex].Cells[2].Value);
            string cellValue3 = Convert.ToString(dgv.Rows[rowInex].Cells[3].Value);
            string cellValue4 = Convert.ToString(dgv.Rows[rowInex].Cells[4].Value);

            switch (columnIndex)
            {
                case 3:
                    if (cellValue0 == "")
                    {
                        MessageBox.Show("名称不能为空");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    if (cellValue1 == "")
                    {
                        MessageBox.Show("数据类型未选择，请先选择");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    if (cellValue2 == "")
                    {
                        MessageBox.Show("分配区域未选择，请先选择");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    if (CheckDataAddress(cellValue3, cellValue1) == false)
                    {
                        MessageBox.Show("输入地址错误,请重新输入");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    isError = false;
                    break;
                case 4:
                    if (cellValue0 == "")
                    {
                        MessageBox.Show("名称不能为空");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    if (cellValue1 == "")
                    {
                        MessageBox.Show("数据类型未选择，请先选择");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    if (cellValue2 == "")
                    {
                        MessageBox.Show("分配区域未选择，请先选择");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    if (cellValue3 == "")
                    {
                        MessageBox.Show("地址未输入，请先选择");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    if (CheckDataModify(cellValue4, cellValue1) == false)
                    {
                        MessageBox.Show("修改值输入错误,请重新输入");
                        dgv.Rows[rowInex].Cells[columnIndex].Value = "";
                        break;
                    }
                    //如果上面都没有错误，就读取值并把列表刷新一遍
                    //值写入PLC

                    List<PLCParamRow> list = new List<PLCParamRow> { };
                    Dictionary<string, string> dic = new Dictionary<string, string> { };
                    int errorRowIndex;
                    DatagrideviewToTable(dgv, ref list, ref dic, out errorRowIndex);
                    if (errorRowIndex != -1) { MessageBox.Show(string.Format("第{0}行有错误", errorRowIndex)); list = null; dic = null; break; }
                    int setValue = Convert.ToInt32(cellValue4);
                    string name = cellValue0;
                    switch (cellValue1)
                    {
                        case StruDataType.BOOL:
                            _primOmron.WriteBitByName(dic, name, setValue);
                            break;
                        case StruDataType.WORD:
                            _primOmron.WriteWordByName(dic, name, setValue);
                            break;
                    }
                    TableToDatagridview(list, dic, true, ref dgv);
                    list = null; dic = null;
                    isError = false;
                    break;
            }
            if (isError == true)
            {
                _actionState = ActionState.error;
            }
            */
        }

        private void dgvDataConfig_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            _selectedRowIndex = dgv.CurrentCell.RowIndex;
            _selectedColumnIndex = dgv.CurrentCell.ColumnIndex;
        }

        private void dgvDataConfig_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvDataConfig_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            _selectedRowIndex = dgv.CurrentCell.RowIndex;
            _selectedColumnIndex = dgv.CurrentCell.ColumnIndex;
        }

        private void SetButtonColor(Button bt, ActionState writeRead)
        {
            switch (writeRead)
            {
                case ActionState.read:
                    bt.Text = "监视中";
                    bt.BackColor = Color.Green;
                    break;
                case ActionState.applyed:
                    bt.Text = "监视";
                    bt.BackColor = Color.Green;
                    break;
                case ActionState.write:
                    bt.Text = "监视";
                    bt.BackColor = Color.Yellow;
                    break;
                case ActionState.error:
                    bt.Text = "监视";
                    bt.BackColor = Color.Red;
                    break;
            }
        }

        private void dgvDataConfig_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            //foreach(var item in dicUpdateFlag)
            //{
            //    if(item.Value != true) { continue; }

            //    object val = dicValues[item.Key];
            //}

            var rowCount = dgvDataConfig.Rows.Count;

            if (rowCount < 1) return;

            for (var i = 0; i < rowCount; i++)
            {
                var name = (string) dgvDataConfig.Rows[i].Cells[0].Value;

                if (!dicUpdateFlag.ContainsKey(name)) continue;

                if (dicUpdateFlag[name])
                {
                    var strVal = GetValueOfVariables(dicValues[name]);
                    dgvDataConfig.Rows[i].Cells[5].Value = strVal;
                    dicUpdateFlag[name] = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_actionState == ActionState.applyed || _actionState == ActionState.read)
            {
                SaveParam(_path, _myConfig);
                MessageBox.Show("保存完成");
            }
            else
            {
                MessageBox.Show("请先应用配置");
            }
        }

        #endregion
    }

    #region "结构体,类,枚举  声明"

    public class PLCParamRow
    {
        public string DataName { get; set; } //数据名称
        public string DataType { get; set; } //数据类型 :WORD,BOOL
        public string DataSection { get; set; } //数据区域: CIO,MR,D,WR
        public string DataAddress { get; set; } //数据地址
        public string DataModifyValue { get; set; } //修改值
        public string DataCurrentValue { get; set; } //实时值
        public string DataReadMode { get; set; }
        public bool DataIsNotify { get; set; }
    }

    public class PLCParamDictionary
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    //判断datagridview当前处于写还是读的模式
    public enum ActionState
    {
        write,
        read,
        applyed,
        error,
        saved,
        other = 99
    }

    public struct StruDataType
    {
        public const string WORD = "WORD"; //字类型
        public const string BOOL = "BOOL"; //位类型
    }

    public struct StruDataSection
    {
        public const string CIO = "CIO"; //CIO区域
        public const string D = "D"; //D区域
        public const string WR = "WR"; //WR区域
        public const string HR = "HR"; //HR区域
        public const string AR = "AR";
        public const string C = "C";
        public const string T = "T";
    }

    public class MyConfig
    {
        public List<PLCParamRow> DgvTable;
        public List<PLCParamDictionary> QueryList;

        public string DstIp { set; get; }
        public string DstPort { set; get; }
        public string LocalIp { set; get; }
    }

    #endregion
}