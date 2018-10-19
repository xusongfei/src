using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Xml.Schema;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Lead.Detect.Interfaces;

using OMRON.Compolet.CIPCompolet64;
using System.Collections;

namespace Lead.Detect.PrimPlcOmron
{
    public delegate void UpdatePrimConnState(PrimConnState state);
    public delegate void UpdatePrimRunState(PrimRunState state);

    public delegate void MonitorVariableChanged(string valName, string valAddr, string valType, object valLast, object valCur);

    public partial class PrimConfigControl : UserControl
    {
        MyConfig _myConfig = null;
        string _path = "d:\\my_config.xml";
        int _selectedRowIndex = -1; int _selectedColumnIndex = -1;

        private PrimOmron _primOmron = null;
        private OmronConfig _config = null;

        private ActionState _actionState;
        private List<PLCParamRow> _dgvTable;
        private Dictionary<string, string> _nameDictionary;
        private Task _taskUI; bool _boolTaskStart = false;

        public Dictionary<string, object> dicValues = new Dictionary<string, object>();
        public Dictionary<string, bool> dicUpdateFlag = new Dictionary<string, bool>();

        public event MonitorVariableChanged OnMonitorVariableChanged;

        private Thread _taskCycleRead;

        private bool _cycleReadFlag = false;
        private bool _getCommInfoFlag = false;
        private bool _getVarListFlag = false;
        public string PrimOmronName
        {
            set
            {
                tTaskBoxName.Text = value;
            }
        }

        public PrimConfigControl()
        {
            InitializeComponent();
        }
        public object LoadFromXml(System.Type type, string filename)
        {
            using (XmlReader xRead = new XmlTextReader(filename))
            {
                XmlSerializer sl = new XmlSerializer(type);
                object obj = sl.Deserialize(xRead);
                xRead.Close();
                return obj;
            }
        }
        public void SaveToXml(string filename, System.Type type, object target)
        {
            using (XmlTextWriter xWrite = new XmlTextWriter(filename, null))
            {
                XmlSerializer sl = new XmlSerializer(type);
                sl.Serialize(xWrite, target);
                xWrite.Close();
            }

        }
        public PrimConfigControl(PrimOmron prim)
        {
            InitializeComponent();
            _primOmron = prim;
            _config = _primOmron._config;

        }
        private BindingSource _bindingSource;
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
            if (File.Exists(_path))
            {
                GetParam(_path, out _myConfig);
            }
            if (_myConfig == null)
            {
                _myConfig = new MyConfig();
                _myConfig.DgvTable = new List<PLCParamRow> { };
                _myConfig.QueryList = new List<PLCParamDictionary> { };
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
        private void GetParam(string path, out MyConfig myConfig)
        {
            myConfig = null;
            myConfig = (MyConfig)LoadFromXml(typeof(MyConfig), path);
        }
        private void SaveParam(string path, MyConfig myConfig)
        {
            SaveToXml(path, typeof(MyConfig), myConfig);
        }
        private void ListToDictionary(List<PLCParamDictionary> list, out Dictionary<string, string> dic)
        {
            dic = null;
            dic = list.ToDictionary(item => item.Key, item => item.Value);
        }
        private void DictionaryToList(Dictionary<string, string> dic, out List<PLCParamDictionary> list)
        {
            list = null;
            List<string> name = new List<string> { };
            List<string> value = new List<string> { };
            foreach (var item in dic)
            {
                name.Add(item.Key);
                value.Add(item.Value);
            }
            List<PLCParamDictionary> listTemp = new List<PLCParamDictionary> { };
            int count = name.Count;
            string[] nameArrary = name.ToArray();
            string[] valueArrary = value.ToArray();
            for (int i = 0; i < count; i++)
            {
                PLCParamDictionary dr = new PLCParamDictionary();
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
        private void GetLoacalIP(out string ip)
        {
            ip = "";
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    ip = ipa.ToString();
            }
        }

        private void lbTitle_Click(object sender, EventArgs e)
        {

        }

        private void SpitAddress(string address, out string newAddress, out string index)
        {
            newAddress = null; index = null;
            string[] splitedArrary = address.Split('.');
            newAddress = splitedArrary[0];
            index = splitedArrary[1];
        }

        private void TableToDatagridview(List<PLCParamRow> list, Dictionary<string, string> dictionary, bool isRead, ref DataGridView dgv)
        {
            string name = null;
            if (isRead == true)
            {
                foreach (PLCParamRow row in list)
                {
                    int read = -1;
                    name = row.DataName;
                    string dataType = row.DataType;
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
            }
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = list;
            //dgv.DataSource = bindingSource;
            bindingSource = null;
        }
        private void DatagrideviewToTable(DataGridView dgv, ref List<PLCParamRow> list, ref Dictionary<string, string> dic, out int errorRowInex)
        {
            errorRowInex = -1;//-1表示无错误
            int rowCount = dgv.Rows.Count;
            dic.Clear();
            List<PLCParamRow> list1 = new List<PLCParamRow> { };
            for (int i = 0; i < rowCount; i++)
            {
                PLCParamRow row = new PLCParamRow();
                row.DataName = (string)dgv.Rows[i].Cells[0].Value;
                row.DataType = (string)dgv.Rows[i].Cells[1].Value;
                row.DataSection = (string)dgv.Rows[i].Cells[2].Value;
                row.DataAddress = (string)dgv.Rows[i].Cells[3].Value;
                row.DataModifyValue = (string)dgv.Rows[i].Cells[4].Value;
                row.DataCurrentValue = "";
                if (row.DataName.Trim() == "") { errorRowInex = i + 1; return; }
                if (row.DataType == "") { errorRowInex = i + 1; return; }
                if (row.DataSection == "") { errorRowInex = i + 1; return; }
                if (CheckDataAddress(row.DataAddress, row.DataType) == false) { errorRowInex = i + 1; return; }
                list1.Add(row);
                string address = null; string index = null;
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
                string value = string.Format("{0}{1},{2}", row.DataSection, address, index);
                dic.Add(row.DataName, value);
                row = null;
            }
            list = list1;
            list1 = null;
        }
        private bool CheckDataAddress(string address, string dataType)
        {
            bool ret = false;
            System.Text.RegularExpressions.Regex rex;
            switch (dataType)
            {
                case StruDataType.WORD:
                    rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
                    ret = rex.IsMatch(address);
                    break;
                case StruDataType.BOOL:
                    rex = new System.Text.RegularExpressions.Regex(@"^([0-9]{1,}[.][0-9]{1,2})$");
                    ret = rex.IsMatch(address);
                    break;
            }
            return ret;
        }
        private bool CheckDataModify(string address, string dataType)
        {
            bool ret = false;
            System.Text.RegularExpressions.Regex rex;
            switch (dataType)
            {
                case StruDataType.WORD:
                    rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
                    ret = rex.IsMatch(address);
                    break;
                case StruDataType.BOOL:
                    rex = new System.Text.RegularExpressions.Regex(@"^([0-1]{1,1})$");
                    ret = rex.IsMatch(address);
                    break;
            }
            return ret;
        }
        private void AddRow(ref List<PLCParamRow> list, ref DataGridView dgv)
        {
            PLCParamRow row = new PLCParamRow();
            list.Add(row);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = list;
            //dgv.DataSource = bindingSource;
        }
        private void RemoveRow(ref List<PLCParamRow> list, ref DataGridView dgv)
        {
            //_selectedRowIndex在cellClick中获取，表示当前选中行
            if (_selectedRowIndex > -1)
            {
                list.RemoveAt(_selectedRowIndex);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = list;
                //dgv.DataSource = bindingSource;
            }
            if (_selectedRowIndex == 0) { _selectedRowIndex = -1; }

        }

        //设置datagridview为双缓存，解决datagridview更新时闪烁的问题
        public void SetDoubleBufferedForDataGridView(ref DataGridView control, bool isDoubleBuffered)
        {
            //获取控件的Type
            Type dgvType = control.GetType();
            //通过Type获取控件的指定属性
            //BindingFlags.Instance                    指定实例成员将包括在搜索中
            //BindingFlags.NonPublic                 指定非公共成员将包括在搜索中
            PropertyInfo properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            //为控件的属性设置值
            properInfo.SetValue(control, isDoubleBuffered, null);
        }
        private void UpdateUI()
        {
            while (_boolTaskStart)
            {
                Thread.Sleep(1000);
                //SetButtonColor(btnMonitor, _actionState);
                if (_primOmron._omronUdp.IsConnected == true)
                {
                    tBoxConnState.Text = "Connected";
                    tBoxConnState.BackColor = Color.Green;

                    if (_actionState == ActionState.read)
                    {
                        TableToDatagridview(_dgvTable, _nameDictionary, true, ref dgvDataConfig);
                    }

                }
                else
                {
                    tBoxConnState.Text = "UnConnected";
                    tBoxConnState.BackColor = Color.Red;
                }

            }

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

            if (!string.IsNullOrEmpty(tBoxDstIp.Text))
            {
                _config.DstIp = tBoxDstIp.Text;
            }

            if (!string.IsNullOrEmpty(tBoxDstPort.Text))
            {
                _config.DstPort = tBoxDstPort.Text;
            }

            if (!string.IsNullOrEmpty(tBoxLocalIp.Text))
            {
                _config.LocalIp = tBoxLocalIp.Text;
            }

            if (!string.IsNullOrEmpty(comBoxCommMode.Text))
            {
                switch (comBoxCommMode.Text)
                {
                    case "Fins":
                        _config.CommMode = OmronCommMode.Fins;
                        break;
                    case "Complet":
                        _config.CommMode = OmronCommMode.Complet;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(tBoxTableDataPath.Text))
            {
                _config.DataTablePath = tBoxTableDataPath.Text;
            }

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
            int port = -1;
            string ipDis = tBoxDstIp.Text.Trim();
            try { port = Convert.ToInt32(tBoxDstPort.Text.Trim()); } catch { }
            string ipLocal = tBoxLocalIp.Text.Trim();
            if (ipDis == "") { MessageBox.Show("PLC的IP不能设置为空！！"); return; }
            else if (port < 1) { MessageBox.Show("端口号不能小于1 ！！"); return; }
            else if (ipLocal == "") { MessageBox.Show("本机IP不能设置为空！！"); return; }

            bool ret = _primOmron._omronUdp.Init(ipDis, port, ipLocal);
            if (ret == false)
            {
                tBoxConnState.BackColor = Color.Red;
                MessageBox.Show("connect fail !!");
                return;
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
            if (_actionState != ActionState.read)
            {
                RemoveRow(ref _dgvTable, ref dgvDataConfig);
            }

        }

        private void dgvDataConfig_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _actionState = ActionState.write;
        }

        private void dgvDataConfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            int rowInex = _selectedRowIndex;
            int columnIndex = _selectedColumnIndex;

            string newVal = Convert.ToString(dgv.Rows[rowInex].Cells[4].Value).Trim();
            if (string.IsNullOrEmpty(newVal)) { return; }

            string valAddr = Convert.ToString(dgv.Rows[rowInex].Cells[3].Value).Trim();
            if (string.IsNullOrEmpty(valAddr)) { return; }

            try
            {
                // write
                //--------------------------------------------------------------------------------
                if (valAddr.StartsWith("_"))
                {
                    MessageBox.Show("The SystemVariable can not write!");
                    return;
                }
                object val = this.RemoveBrackets(newVal);
                if (this.njCompolet1.GetVariableInfo(valAddr).Type == VariableType.STRUCT)
                {
                    val = this.ObjectToByteArray(val);
                }
                this.njCompolet1.WriteVariable(valAddr, val);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            DataGridView dgv = sender as DataGridView;
            _selectedRowIndex = dgv.CurrentCell.RowIndex;
            _selectedColumnIndex = dgv.CurrentCell.ColumnIndex;
        }
        private void dgvDataConfig_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvDataConfig_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
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

            int rowCount = dgvDataConfig.Rows.Count;

            if (rowCount < 1) { return; }

            for (int i = 0; i < rowCount; i++)
            {
                string name = (string)dgvDataConfig.Rows[i].Cells[0].Value;

                if (!dicUpdateFlag.ContainsKey(name)) { continue; }

                if(dicUpdateFlag[name])
                {
                    string strVal = this.GetValueOfVariables(dicValues[name]);
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
        public void DataTableUpdate()
        {
            if(_primOmron == null) { return; }

            if(_primOmron._dataConfigs == null) { return; }

            if(_primOmron._dataConfigs.ListDataConfig == null) { return; }

            dgvDataConfig.Rows.Clear();

            if(_primOmron._dataConfigs.ListDataConfig.Count < 1) { return; }

            foreach(DataConfig config in _primOmron._dataConfigs.ListDataConfig)
            {
                int index = this.dgvDataConfig.Rows.Add();
                this.dgvDataConfig.Rows[index].Cells[0].Value = config.DtName;
                this.dgvDataConfig.Rows[index].Cells[1].Value = config.DtType;
                this.dgvDataConfig.Rows[index].Cells[2].Value = config.DtSection.ToString();
                this.dgvDataConfig.Rows[index].Cells[3].Value = config.DtAddress;
                this.dgvDataConfig.Rows[index].Cells[6].Value = config.DtRdMode.ToString();
                this.dgvDataConfig.Rows[index].Cells[7].Value = config.DtIsNotify;
            }

            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = _primOmron._dataConfigs.ListDataConfig;
            //dgvDataConfig.DataSource = bindingSource;
        }


        private void btnAddRow_Click(object sender, EventArgs e)
        {
            dgvDataConfig.Rows.Add();
            //_primOmron.AddDataConfig();
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            int _curSelectedIdx = dgvDataConfig.CurrentCell.RowIndex;
            dgvDataConfig.Rows.RemoveAt(_curSelectedIdx);
            //_primOmron.RemoveDataConfig(_idx);
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _primOmron.IPrimInit();
        }

        private void btnTableBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tBoxTableDataPath.Text = openFileDialog.FileName;

                if (string.IsNullOrEmpty(tBoxTableDataPath.Text))
                {
                    return;
                }

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
                int iRet = _primOmron.ImportDataTable(_config.DataTablePath);
                if (iRet != 0)
                {
                    return;
                }

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
                    string savePath = saveFileDialog.FileName;
                    _primOmron.ExportDataTable(savePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private OmronDataSection StrTrun2OmronDataSection(string dtSectionStr)
        {
            OmronDataSection section = OmronDataSection.Other;
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

        private DataReadMode StrTrun2OmronDataReadMode(string dtRdModeStr)
        {
            DataReadMode rdMode = DataReadMode.Other;
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

        private Type StrTurn2OmronDataType(string dtType)
        {
            Type type = typeof(bool);

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
                    type = typeof(Int16);
                    break;
                case "int32":
                    type = typeof(Int32);
                    break;
                case "int64":
                    type = typeof(Int64);
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

        private void btnConfigApply_Click(object sender, EventArgs e)
        {
            int rowCount = dgvDataConfig.Rows.Count;

            if(rowCount < 1) { return; }

            _primOmron._dataConfigs.ListDataConfig.Clear();

            for (int i = 0; i < rowCount; i++)
            {
                DataConfig dtConfig = new DataConfig();

                if(dgvDataConfig.Rows[i].Cells[0].Value == null)
                {
                    dtConfig.DtName = "";
                }
                else
                {
                    dtConfig.DtName = (string)dgvDataConfig.Rows[i].Cells[0].Value;
                }
                
                if(dgvDataConfig.Rows[i].Cells[1].Value == null)
                {
                    dtConfig.DtType = "";
                }
                else
                {
                    //StrTurn2OmronDataType
                    dtConfig.DtType = (string)dgvDataConfig.Rows[i].Cells[1].Value;
                }
                
                if(dgvDataConfig.Rows[i].Cells[2].Value == null)
                {
                    dtConfig.DtSection = OmronDataSection.Other;
                }
                else
                {
                    dtConfig.DtSection = StrTrun2OmronDataSection((string)dgvDataConfig.Rows[i].Cells[2].Value);
                }
                
                if(dgvDataConfig.Rows[i].Cells[3].Value == null)
                {
                    dtConfig.DtAddress = "";
                }else
                {
                    dtConfig.DtAddress = (string)dgvDataConfig.Rows[i].Cells[3].Value;
                }
                
                //dtConfig.DtModifyValue = (string)dgvDataConfig.Rows[i].Cells[4].Value;

                if(dgvDataConfig.Rows[i].Cells[6].Value == null)
                {
                    dtConfig.DtRdMode = DataReadMode.Other;
                }
                else
                {
                    dtConfig.DtRdMode = StrTrun2OmronDataReadMode((string)dgvDataConfig.Rows[i].Cells[6].Value);
                }
                
                //dtConfig.DtCurrentValue = "";
                if(dgvDataConfig.Rows[i].Cells[7].Value == null)
                {
                    dtConfig.DtIsNotify = false;
                }
                else
                {
                    dtConfig.DtIsNotify = (bool)dgvDataConfig.Rows[i].Cells[7].Value;
                }
                
                _primOmron._dataConfigs.ListDataConfig.Add(dtConfig);
            }
        }

        public void SetPrimConnState(PrimConnState state)
        {
            if (tBoxConnState.InvokeRequired)
            {
                UpdatePrimConnState update = new UpdatePrimConnState(SetPrimConnState);
                this.Invoke(update, new object[1] { state });
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
                UpdatePrimRunState update = new UpdatePrimRunState(SetPrimRunState);
                this.Invoke(update, new object[1] { state });
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

        private void DisplayVariableInfomation(string str)
        {
            ListViewItem item = this.listViewOfVariableNames.Items.Add(str);
            VariableInfo info = this.njCompolet1.GetVariableInfo(str);
            if (info.IsArray)
            {
                string text = info.Type.ToString();
                foreach (long num in info.NumberOfElements)
                {
                    text += "[" + num.ToString() + "]";
                }
                item.SubItems.Add(text);
            }
            else
            {
                item.SubItems.Add(info.Type.ToString());
            }
            item.SubItems.Add(string.Empty);
        }

        private bool IsMustElementAccess(VariableInfo info)
        {
            bool toReturn = false;
            if (info.IsArray)
            {
                if (info.Type == VariableType.STRING || info.Type == VariableType.UNION)
                {
                    toReturn = true;
                }
            }

            return toReturn;
        }

        private bool IsMustMemberAccess(VariableInfo info)
        {
            bool toReturn = false;

            if (info.Type == VariableType.UNION)
            {
                toReturn = true;
            }

            return toReturn;
        }

        private String GetAccessablePath(String path)
        {
            String newPath = String.Empty;
            newPath += path;
            VariableInfo info = this.njCompolet1.GetVariableInfo(path);
            if (this.IsMustElementAccess(info))
            {
                // get only first element
                for (int i = 0; i < info.Dimension; i++)
                {
                    newPath += "[" + info.StartArrayElements[i].ToString() + "]";
                }
                return this.GetAccessablePath(newPath);
            }
            else if (this.IsMustMemberAccess(info))
            {
                // get only first member
                newPath += "." + info.StructMembers[0].Name;
                return this.GetAccessablePath(newPath);
            }
            else
            {
                return newPath;
            }
        }

        private void btnGetVaiable_Click(object sender, EventArgs e)
        {
            try
            {
                this.listViewOfVariableNames.Items.Clear();
                String[] vars = null;
                if (this.chkSystemVariable.Checked)
                {
                    vars = this.njCompolet1.SystemVariableNames;
                }
                else
                {
                    vars = this.njCompolet1.VariableNames;
                }

                foreach (string var in vars)
                {
                    String accessablePath = this.GetAccessablePath(var);
                    this.DisplayVariableInfomation(accessablePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.njCompolet1.Active = this.chkActive.Checked;
                if (this.chkActive.Checked)
                {
                    if (!this.njCompolet1.IsConnected)
                    {
                        if (this.radioPeerAddress.Checked)
                        {
                            MessageBox.Show("Connection failed !" + System.Environment.NewLine + "Please check PeerAddress.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Connection failed !" + System.Environment.NewLine + "Please check RoutePath.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        this.njCompolet1.Active = false;
                        this.chkActive.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.njCompolet1.Active = false;
                this.chkActive.Checked = false;
            }

            _primOmron._completInfo.Active = this.njCompolet1.Active;
        }

        private void btnGetCommunication_Click(object sender, EventArgs e)
        {
            if (this.radioPeerAddress.Checked)
            {
                this.listViewOfEachValue.Items[2].SubItems[1].Text = this.njCompolet1.LocalPort.ToString();
                this.listViewOfEachValue.Items[3].SubItems[1].Text = this.njCompolet1.PeerAddress;
                this.listViewOfEachValue.Items[4].SubItems[1].Text = string.Empty;
                this.listViewOfEachValue.Items[5].SubItems[1].Text = string.Empty;
            }
            else if (this.radioRoutePath.Checked)
            {
                this.listViewOfEachValue.Items[5].SubItems[1].Text = this.njCompolet1.RoutePath;
                this.listViewOfEachValue.Items[2].SubItems[1].Text = string.Empty;
                this.listViewOfEachValue.Items[3].SubItems[1].Text = string.Empty;
            }

            this.listViewOfEachValue.Items[0].SubItems[1].Text = this.njCompolet1.Active.ToString();
            this.listViewOfEachValue.Items[1].SubItems[1].Text = this.njCompolet1.ConnectionType.ToString();
            this.listViewOfEachValue.Items[4].SubItems[1].Text = this.njCompolet1.UseRoutePath.ToString();
            this.listViewOfEachValue.Items[6].SubItems[1].Text = this.njCompolet1.IsConnected.ToString();
            this.listViewOfEachValue.Items[7].SubItems[1].Text = this.njCompolet1.TypeName;
            this.listViewOfEachValue.Items[8].SubItems[1].Text = this.njCompolet1.DontFragment.ToString();

            _primOmron._completInfo.Active = this.njCompolet1.Active;
            _primOmron._completInfo.ConnectionType = this.njCompolet1.ConnectionType.ToString();
            _primOmron._completInfo.UseRoutePath = this.njCompolet1.UseRoutePath;
            _primOmron._completInfo.IsConnected = this.njCompolet1.IsConnected;
            _primOmron._completInfo.TypeName = this.njCompolet1.TypeName;
            _primOmron._completInfo.DontFragment = this.njCompolet1.DontFragment;
        }

        private string GetValueString(object val)
        {
            if (val is float || val is double)
            {
                return string.Format("{0:R}", val);
            }
            else
            {
                return val.ToString();
            }
        }

        private string GetValueOfVariables(object val)
        {
            string valStr = string.Empty;
            if (val.GetType().IsArray)
            {
                Array valArray = val as Array;
                if (valArray.Rank == 1)
                {
                    valStr += "[";
                    foreach (object a in valArray)
                    {
                        valStr += this.GetValueString(a) + ",";
                    }
                    valStr = valStr.TrimEnd(',');
                    valStr += "]";
                }
                else if (valArray.Rank == 2)
                {
                    for (int i = 0; i <= valArray.GetUpperBound(0); i++)
                    {
                        valStr += "[";
                        for (int j = 0; j <= valArray.GetUpperBound(1); j++)
                        {
                            valStr += this.GetValueString(valArray.GetValue(i, j)) + ",";
                        }
                        valStr = valStr.TrimEnd(',');
                        valStr += "]";
                    }
                }
                else if (valArray.Rank == 3)
                {
                    for (int i = 0; i <= valArray.GetUpperBound(0); i++)
                    {
                        for (int j = 0; j <= valArray.GetUpperBound(1); j++)
                        {
                            valStr += "[";
                            for (int z = 0; z <= valArray.GetUpperBound(2); z++)
                            {
                                valStr += this.GetValueString(valArray.GetValue(i, j, z)) + ",";
                            }
                            valStr = valStr.TrimEnd(',');
                            valStr += "]";
                        }
                    }
                }
            }
            else
            {
                valStr = this.GetValueString(val);
            }
            return valStr;
        }


        private void btnReadVariableMultiple_Click(object sender, EventArgs e)
        {
            try
            {
                string[] varlist = this.txtVariableName.Text.Replace(" ", String.Empty).Split(',');

                Hashtable retVals = this.njCompolet1.ReadVariableMultiple(varlist);
                if (retVals == null)
                {
                    throw new NotSupportedException();
                }

                string multival = string.Empty;
                for (int index = 0; index < varlist.Length; index++)
                {
                    string varName = varlist[index];
                    object val = retVals[varName];
                    string valStr = this.GetValueOfVariables(val);
                    if (this.listViewOfVariableNames.SelectedItems.Count > index)
                    {
                        if (this.listViewOfVariableNames.SelectedItems[index].SubItems[0].Text == varlist[index])
                        {
                            this.listViewOfVariableNames.SelectedItems[index].SubItems.Add(string.Empty);
                            this.listViewOfVariableNames.SelectedItems[index].SubItems[2].Text = valStr;
                        }
                    }
                    multival += valStr + ",";
                }
                multival = multival.TrimEnd(',');
                this.txtValue.Text = multival;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReadVariable_Click(object sender, EventArgs e)
        {
            try
            {
                string varname = this.txtVariableName.Text;
                object obj = this.njCompolet1.ReadVariable(varname);
                if (obj == null)
                {
                    throw new NotSupportedException();
                }

                VariableInfo info = this.njCompolet1.GetVariableInfo(varname);
                string str = this.GetValueOfVariables(obj);
                if (this.listViewOfVariableNames.SelectedItems.Count > 0)
                {
                    if (this.listViewOfVariableNames.SelectedItems[0].SubItems[0].Text == varname)
                    {
                        this.listViewOfVariableNames.SelectedItems[0].SubItems.Add(string.Empty);
                        this.listViewOfVariableNames.SelectedItems[0].SubItems[2].Text = str;
                    }
                }
                this.txtValue.Text = str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private object RemoveBrackets(string val)
        {
            object obj = string.Empty;
            if (val.IndexOf("[") >= 0)
            {
                string str = val.Trim('[', ']');
                str = str.Replace("][", ",");
                obj = str.Split(',');
            }
            else
            {
                obj = val;
            }
            return obj;
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj is Array)
            {
                Array arr = obj as Array;
                Byte[] bin = new Byte[arr.Length];
                for (int i = 0; i < bin.Length; i++)
                {
                    bin[i] = Convert.ToByte(arr.GetValue(i));
                }
                return bin;
            }
            else
            {
                return new Byte[1] { Convert.ToByte(obj) };
            }
        }

        private void btnWriteVariable_Click(object sender, EventArgs e)
        {
            try
            {
                // write
                //--------------------------------------------------------------------------------
                string valWrite = this.txtVariableName.Text;
                if (valWrite.StartsWith("_"))
                {
                    MessageBox.Show("The SystemVariable can not write!");
                    return;
                }
                object val = this.RemoveBrackets(this.txtValue.Text);
                if (this.njCompolet1.GetVariableInfo(this.txtVariableName.Text).Type == VariableType.STRUCT)
                {
                    val = this.ObjectToByteArray(val);
                }
                this.njCompolet1.WriteVariable(this.txtVariableName.Text, val);

                // read
                this.btnReadVariable_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string ByteArrayToString(byte[] ba)
        {
            if (ba.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return BitConverter.ToString(ba);
            }
        }

        private void btnReadRawDataMultiple_Click(object sender, EventArgs e)
        {
            try
            {
                string[] varlist = this.txtBinaryVariableName.Text.Replace(" ", String.Empty).Split(',');

                Hashtable retVals = this.njCompolet1.ReadRawDataMultiple(varlist);
                string multival = string.Empty;
                for (int index = 0; index < varlist.Length; index++)
                {
                    string varName = varlist[index];
                    string val = this.ByteArrayToString(retVals[varName] as byte[]);

                    if (this.listViewOfVariableNames.SelectedItems.Count > index)
                    {
                        if (this.listViewOfVariableNames.SelectedItems[index].SubItems[0].Text == varlist[index])
                        {
                            this.listViewOfVariableNames.SelectedItems[index].SubItems.Add(string.Empty);
                            this.listViewOfVariableNames.SelectedItems[index].SubItems[2].Text = val;
                        }
                    }
                    multival += val + ",";
                }
                multival = multival.TrimEnd(',');
                this.txtBinaryValue.Text = multival;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReadRaw_Click(object sender, EventArgs e)
        {
            try
            {
                string varname = this.txtBinaryVariableName.Text;
                object obj = this.njCompolet1.ReadRawData(varname);
                VariableInfo info = this.njCompolet1.GetVariableInfo(varname);
                string val = this.ByteArrayToString(obj as byte[]);

                if (this.listViewOfVariableNames.SelectedItems.Count > 0)
                {
                    if (this.listViewOfVariableNames.SelectedItems[0].SubItems[0].Text == varname)
                    {
                        this.listViewOfVariableNames.SelectedItems[0].SubItems.Add(string.Empty);
                        this.listViewOfVariableNames.SelectedItems[0].SubItems[2].Text = val;
                    }
                }
                this.txtBinaryValue.Text = val;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.StackTrace, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private byte[] StringToByteArray(string hex)
        {
            if (hex == String.Empty)
            {
                return new Byte[0];
            }
            int byteNumber = hex.Length / 2;
            byte[] bytes = new byte[byteNumber];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private void btnWriteRaw_Click(object sender, EventArgs e)
        {
            try
            {
                // write
                if (this.txtBinaryVariableName.Text.StartsWith("_"))
                {
                    MessageBox.Show("The SystemVariable can not write!");
                    return;
                }
                byte[] val = this.StringToByteArray(this.txtBinaryValue.Text.Replace("-", string.Empty));
                this.njCompolet1.WriteRawData(this.txtBinaryVariableName.Text, val);

                // read
                this.btnReadRaw_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetUnitName_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtUnitName.Text = this.njCompolet1.UnitName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetUnitName_Click(object sender, EventArgs e)
        {
            try
            {
                this.njCompolet1.UnitName = this.txtUnitName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetClock_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtClock.Text = this.njCompolet1.Clock.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetClock_Click(object sender, EventArgs e)
        {
            try
            {
                this.njCompolet1.Clock = DateTime.Parse(this.txtClock.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void groupBoxClock_Enter(object sender, EventArgs e)
        {

        }

        private void btnGetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.njCompolet1.RunMode)
                {
                    case NJCompolet.RunModeTypes.Program:
                        this.cmbRunMode.SelectedIndex = 0;
                        break;
                    case NJCompolet.RunModeTypes.Run:
                        this.cmbRunMode.SelectedIndex = 1;
                        break;
                }

                switch (this.njCompolet1.RunStatus)
                {
                    case 0x00:
                        this.cmbRunStatus.SelectedIndex = 0;
                        this.cmbCPUStatus.Text = "Normal";
                        break;
                    case 0x01:
                        this.cmbRunStatus.SelectedIndex = 1;
                        this.cmbCPUStatus.Text = "Normal";
                        break;
                    case 0x80:
                        this.cmbRunStatus.SelectedIndex = 0;
                        this.cmbCPUStatus.Text = "Standby";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.cmbRunMode.Text)
                {
                    case "Program":
                        this.njCompolet1.RunMode = NJCompolet.RunModeTypes.Program;
                        break;
                    case "Run":
                        this.njCompolet1.RunMode = NJCompolet.RunModeTypes.Run;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetMessageTimeLimit_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtReceiveTimeLimit.Text = this.njCompolet1.ReceiveTimeLimit.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetMessageTimeLimit_Click(object sender, EventArgs e)
        {
            try
            {
                this.njCompolet1.ReceiveTimeLimit = long.Parse(this.txtReceiveTimeLimit.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetPlcEncoding_Click(object sender, EventArgs e)
        {
            try
            {
                this.cmbPlcEncodingName.Text = this.njCompolet1.PlcEncoding.WebName.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSetPlcEncoding_Click(object sender, EventArgs e)
        {
            try
            {
                this.njCompolet1.PlcEncoding = System.Text.Encoding.GetEncoding(this.cmbPlcEncodingName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbConnectionType.SelectedIndex == 0)
            {
                this.njCompolet1.ConnectionType = ConnectionType.UCMM;
            }
            else
            {
                this.njCompolet1.ConnectionType = ConnectionType.Class3;
            }
        }

        private void numPortNo_ValueChanged(object sender, EventArgs e)
        {
            this.njCompolet1.LocalPort = (int)this.numPortNo.Value;
        }

        private void txtIPAddress_TextChanged(object sender, EventArgs e)
        {
            this.njCompolet1.PeerAddress = this.txtIPAddress.Text;
        }

        private void txtRoutePath_TextChanged(object sender, EventArgs e)
        {
            this.njCompolet1.RoutePath = this.txtRoutePath.Text;
        }

        private void radioPeerAddress_CheckedChanged(object sender, EventArgs e)
        {
            this.labelPortNo.Enabled = this.radioPeerAddress.Checked;
            this.numPortNo.Enabled = this.radioPeerAddress.Checked;
            this.labelIPAddress.Enabled = this.radioPeerAddress.Checked;
            this.txtIPAddress.Enabled = this.radioPeerAddress.Checked;
            this.labelRoutePath.Enabled = !this.radioPeerAddress.Checked;
            this.txtRoutePath.Enabled = !this.radioPeerAddress.Checked;

            this.njCompolet1.UseRoutePath = !this.radioPeerAddress.Checked;
        }

        private void chkDoNotFragment_CheckedChanged(object sender, EventArgs e)
        {
            this.njCompolet1.DontFragment = this.chkDoNotFragment.Checked;
        }

        private void listViewOfVariableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewOfVariableNames.SelectedItems.Count == 0)
            {
                return;
            }
            if (this.listViewOfVariableNames.SelectedItems.Count == 1)
            {
                this.txtVariableName.Text = this.listViewOfVariableNames.SelectedItems[0].Text;
                this.txtBinaryVariableName.Text = this.listViewOfVariableNames.SelectedItems[0].Text;
            }
            else
            {
                string[] varlist = new string[this.listViewOfVariableNames.SelectedItems.Count];
                string variables = string.Empty;
                for (int i = 0; i < this.listViewOfVariableNames.SelectedItems.Count; i++)
                {
                    varlist[i] = this.listViewOfVariableNames.SelectedItems[i].Text;
                    variables += varlist[i] + ",";
                }
                variables = variables.TrimEnd(',');
                this.txtVariableName.Text = variables;
                this.txtBinaryVariableName.Text = variables;
            }
            this.txtValue.Text = string.Empty;
            this.txtBinaryValue.Text = string.Empty;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            int cycTime = -1;
            int.TryParse(doUpDown.Text, out cycTime);
            _config.CycleTime = cycTime;
        }
        public void SetActiveFlag(bool active)
        {
            try
            {
                this.chkActive.Checked = active;
                this.njCompolet1.Active = this.chkActive.Checked;
                if (this.chkActive.Checked)
                {
                    if (!this.njCompolet1.IsConnected)
                    {
                        if (this.radioPeerAddress.Checked)
                        {
                            MessageBox.Show("Connection failed !" + System.Environment.NewLine + "Please check PeerAddress.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Connection failed !" + System.Environment.NewLine + "Please check RoutePath.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        this.njCompolet1.Active = false;
                        this.chkActive.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.njCompolet1.Active = false;
                this.chkActive.Checked = false;
            }

            _primOmron._completInfo.Active = this.njCompolet1.Active;
        }

        public void GetCommunicationInfo()
        {
            if (this.radioPeerAddress.Checked)
            {
                this.listViewOfEachValue.Items[2].SubItems[1].Text = this.njCompolet1.LocalPort.ToString();
                this.listViewOfEachValue.Items[3].SubItems[1].Text = this.njCompolet1.PeerAddress;
                this.listViewOfEachValue.Items[4].SubItems[1].Text = string.Empty;
                this.listViewOfEachValue.Items[5].SubItems[1].Text = string.Empty;

                _primOmron._completInfo.LocalPort = this.njCompolet1.LocalPort;
                _primOmron._completInfo.PeerAddress = this.njCompolet1.PeerAddress;
            }
            else if (this.radioRoutePath.Checked)
            {
                this.listViewOfEachValue.Items[5].SubItems[1].Text = this.njCompolet1.RoutePath;
                this.listViewOfEachValue.Items[2].SubItems[1].Text = string.Empty;
                this.listViewOfEachValue.Items[3].SubItems[1].Text = string.Empty;

                _primOmron._completInfo.RoutePath = this.njCompolet1.RoutePath;
            }

            this.listViewOfEachValue.Items[0].SubItems[1].Text = this.njCompolet1.Active.ToString();
            this.listViewOfEachValue.Items[1].SubItems[1].Text = this.njCompolet1.ConnectionType.ToString();
            this.listViewOfEachValue.Items[4].SubItems[1].Text = this.njCompolet1.UseRoutePath.ToString();
            this.listViewOfEachValue.Items[6].SubItems[1].Text = this.njCompolet1.IsConnected.ToString();
            this.listViewOfEachValue.Items[7].SubItems[1].Text = this.njCompolet1.TypeName;
            this.listViewOfEachValue.Items[8].SubItems[1].Text = this.njCompolet1.DontFragment.ToString();

            _primOmron._completInfo.Active = this.njCompolet1.Active;
            _primOmron._completInfo.ConnectionType = this.njCompolet1.ConnectionType.ToString();
            _primOmron._completInfo.UseRoutePath = this.njCompolet1.UseRoutePath;
            _primOmron._completInfo.IsConnected = this.njCompolet1.IsConnected;
            _primOmron._completInfo.TypeName = this.njCompolet1.TypeName;
            _primOmron._completInfo.DontFragment = this.njCompolet1.DontFragment;
        }

        public void GetSysVariableList()
        {
            try
            {
                if(_primOmron._completInfo.SysVariableList == null)
                {
                    _primOmron._completInfo.SysVariableList = new List<string>();
                }
                else
                {
                    _primOmron._completInfo.SysVariableList.Clear();
                }

                this.listViewOfVariableNames.Items.Clear();
                String[] vars = null;

                vars = this.njCompolet1.SystemVariableNames;

                foreach (string var in vars)
                {
                    _primOmron._completInfo.SysVariableList.Add(var);

                    String accessablePath = this.GetAccessablePath(var);
                    this.DisplayVariableInfomation(accessablePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void GetVariableList()
        {
            try
            {
                if (_primOmron._completInfo.VariableList == null)
                {
                    _primOmron._completInfo.VariableList = new List<string>();
                }
                else
                {
                    _primOmron._completInfo.VariableList.Clear();
                }

                this.listViewOfVariableNames.Items.Clear();
            //    String[] vars = null;


                foreach (DataConfig config in _primOmron._dataConfigs.ListDataConfig)
                {               
                    if (config.DtAddress.Contains("."))
                    {
                        string[] strs = config.DtAddress.Split('.');
                 
                        if (!string.IsNullOrEmpty(strs[0]))
                        {
                            if (!_primOmron._completInfo.VariableList.Contains(strs[0]))
                            {
                                _primOmron._completInfo.VariableList.Add(strs[0]);
                                String accessablePath = this.GetAccessablePath(strs[0]);
                                this.DisplayVariableInfomation(accessablePath);
                            }
                        }

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
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void CycleReadTaskSuspend()
        {
            _taskCycleRead.Suspend();
        }

        public void CycleReadTaskStart()
        {   
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if(!_primOmron._completInfo.Active)
                {
                    SetActiveFlag(true);
                }

                if(!_getCommInfoFlag)
                {
                    GetCommunicationInfo();
                    _getCommInfoFlag = true;
                }
                
                //GetSysVariableList();
                if(!_getVarListFlag)
                {
                    GetVariableList();
                    _getVarListFlag = true;
                }
                
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {

            }

            if(_taskCycleRead == null)
            {
                _taskCycleRead = new Thread(() => this.CycleRead());
                _cycleReadFlag = true;
                _taskCycleRead.Start();
            }

            if (_taskCycleRead.ThreadState == ThreadState.Running)
            {
                return;
            }
            else
            {
                _taskCycleRead.Resume();
            }

            

        }

        private void ReadSingleVariableFromComplet(DataConfig config)
        {
            if (string.IsNullOrEmpty(config.DtName)) { return; }

            if(config.DtRdMode == DataReadMode.NonCycle) { return; }

            string addr = config.DtAddress;

            if (config.DtAddress.Contains("."))
            {
                string[] strs = config.DtAddress.Split('.');
                if(strs.Length < 0) { return; }

                if (string.IsNullOrEmpty(strs[0])) { return; }

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) { return; }

            object obj = this.njCompolet1.ReadVariable(config.DtAddress);

            if(!dicValues.ContainsKey(config.DtName))
            {
                //Call Event 调用事件
                if (config.DtIsNotify && OnMonitorVariableChanged != null)
                {
                    OnMonitorVariableChanged(config.DtName, config.DtAddress, config.DtType, null, obj);
                }

                dicValues.Add(config.DtName, obj);
                dicUpdateFlag.Add(config.DtName, true);

            }
            else
            {
                object lastVal = dicValues[config.DtName];
                string lastValStr = lastVal.ToString();
                string curValStr = obj.ToString();
                if (!lastValStr.Equals(curValStr))
                {
                    //Call Event 调用事件
                    if (config.DtIsNotify && OnMonitorVariableChanged != null)
                    {
                        OnMonitorVariableChanged(config.DtName, config.DtAddress, config.DtType, lastVal, obj);
                    }

                    dicValues[config.DtName] = obj;
                    dicUpdateFlag[config.DtName] = true;

                }
            }
        }
        private void ReadSingleVariableFromFins(DataConfig config)
        {

        }

        private void ReadSingleVariable(DataConfig config)
        {
            if (_config.CommMode == OmronCommMode.Complet)
            {
                ReadSingleVariableFromComplet(config);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {
                ReadSingleVariableFromFins(config);
            }
        }

        private void CycleRead()
        {
            // 阻塞等触发
            while (_cycleReadFlag)
            {
                if(_getVarListFlag)
                {
                    foreach (DataConfig config in _primOmron._dataConfigs.ListDataConfig)
                    {
                        ReadSingleVariable(config);
                    }
                }
                Application.DoEvents();
                Thread.Sleep(100);
                Thread.Sleep(_config.CycleTime);
            }
        }

        private void cBoxMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if(cBoxMonitor.Checked)
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

        public int WriteVariableFromAddr(string valAddr, object val)
        {
            int iRet = -1;
            if (string.IsNullOrEmpty(valAddr)) { return iRet; }

            string addr = valAddr;

            if (valAddr.Contains("."))
            {
                string[] strs = valAddr.Split('.');
                if (strs.Length < 0) { return iRet; }

                if (string.IsNullOrEmpty(strs[0])) { return iRet; }

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) { return iRet; }

            this.njCompolet1.WriteVariable(valAddr, val);

            return iRet;
        }

        public object ReadVariableFromAddr(string valAddr)
        {
            object val = null;
            if (string.IsNullOrEmpty(valAddr)) { return null; }

            string addr = valAddr;

            if (valAddr.Contains("."))
            {
                string[] strs = valAddr.Split('.');
                if (strs.Length < 0) { return null; }

                if (string.IsNullOrEmpty(strs[0])) { return null; }

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) { return null; }

            val = this.njCompolet1.ReadVariable(valAddr);
          
            return val;
        }

        public int WriteVariableFromName(string valName, object val)
        {
            int iRet = -1;
            if (string.IsNullOrEmpty(valName)) { return iRet; }

            string valAddr = "";

            foreach (DataConfig config in _primOmron._dataConfigs.ListDataConfig)
            {
                if (config.DtName == valName)
                {
                    valAddr = config.DtAddress;
                    break;
                }
            }

            if (string.IsNullOrEmpty(valAddr)) { return iRet; }

            string addr = valAddr;

            if (valAddr.Contains("."))
            {
                string[] strs = valAddr.Split('.');
                if (strs.Length < 0) { return iRet; }

                if (string.IsNullOrEmpty(strs[0])) { return iRet; }

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) { return iRet; }

            this.njCompolet1.WriteVariable(valAddr, val);

            return iRet;
        }

        public object ReadVariableFromName(string valName)
        {
            object val = null;
            if (string.IsNullOrEmpty(valName)) { return null; }

            string valAddr = "";

            foreach(DataConfig config in _primOmron._dataConfigs.ListDataConfig)
            {
                if(config.DtName == valName)
                {
                    valAddr = config.DtAddress;
                    break;
                }
            }

            if (string.IsNullOrEmpty(valAddr)) { return null; }

            string addr = valAddr;

            if (valAddr.Contains("."))
            {
                string[] strs = valAddr.Split('.');
                if (strs.Length < 0) { return null; }

                if (string.IsNullOrEmpty(strs[0])) { return null; }

                addr = strs[0];
            }

            if (!_primOmron._completInfo.VariableList.Contains(addr)) { return null; }

            val = this.njCompolet1.ReadVariable(valAddr);

            return val;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    #region "结构体,类,枚举  声明"
    public class PLCParamRow
    {
        public string DataName { get; set; }//数据名称
        public string DataType { get; set; }//数据类型 :WORD,BOOL
        public string DataSection { get; set; }//数据区域: CIO,MR,D,WR
        public string DataAddress { get; set; }//数据地址
        public string DataModifyValue { get; set; }//修改值
        public string DataCurrentValue { get; set; }//实时值
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
        public const string WORD = "WORD";//字类型
        public const string BOOL = "BOOL";//位类型
    }
    public struct StruDataSection
    {
        public const string CIO = "CIO";//CIO区域
        public const string D = "D";//D区域
        public const string WR = "WR";//WR区域
        public const string HR = "HR";//HR区域
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
