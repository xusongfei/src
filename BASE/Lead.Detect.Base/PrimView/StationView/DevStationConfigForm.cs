using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalStation;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.View.StationView
{
    public partial class DevStationConfigForm : Form
    {
        private readonly string _stConfigFilePath;
        private readonly Dictionary<string, MoveProfile> profile = new Dictionary<string, MoveProfile>();

        public DevStationConfigForm()
        {
            InitializeComponent();
        }

        public DevStationConfigForm(string stConfigFilePath)
        {
            InitializeComponent();

            _stConfigFilePath = stConfigFilePath;
        }

        private void DevConfigForm_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < dtGridViewStation.RowCount-1; i++)
            //{
            //    int curSelectedIdx = dtGridViewStation.CurrentCell.RowIndex;
            //    Guid stationId = (Guid)dtGridViewStation.Rows[curSelectedIdx].Cells[3].Value;

            //    IStation station = StationMgr.Settings.GetStationByGUID(stationId);
            //    Control configControl = station.GetStationConfigControl();
            //    //configControl.Dock = DockStyle.Fill;
            //    splitContainer1.Panel2.Controls.Add(configControl);

            //}
        }

        private void DevConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("是否要增加工站", "重要消息提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;
            var rowIdx = dtGridViewStation.Rows.Add();
            var id = Guid.NewGuid();
            dtGridViewStation.Rows[rowIdx].Cells[3].Value = id;
        }

        private void applyToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("是否要创建工站", "重要消息提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;
            var curSelectedIdx = dtGridViewStation.CurrentCell.RowIndex;

            var stationId = (Guid) dtGridViewStation.Rows[curSelectedIdx].Cells[3].Value;

            var stationName = "";
            if (dtGridViewStation.Rows[curSelectedIdx].Cells[0].Value != null)
                stationName = (string) dtGridViewStation.Rows[curSelectedIdx].Cells[0].Value;

            var stationEnable = (bool) dtGridViewStation.Rows[curSelectedIdx].Cells[1].Value;

            var stationInfo = "";
            if (dtGridViewStation.Rows[curSelectedIdx].Cells[4].Value != null)
                stationInfo = (string) dtGridViewStation.Rows[curSelectedIdx].Cells[4].Value;

            if (dtGridViewStation.Rows[curSelectedIdx].Cells[2].Value == null)
            {
            }
            else
            {
                if (StationMgr.Instance.GetStationByGUID(stationId) != null) return;

                var stTypeStr = (string) dtGridViewStation.Rows[curSelectedIdx].Cells[2].Value;
                var stType = stTypeStr;
                var station = StationFactory.Instance.Create(stType);
                if (station != null)
                {
                    station.Name = stationName;
                    station.StationId = stationId;
                    //station.StationType = stType;
                    //station.Description = stationInfo;
                    //station.Enable = stationEnable;

                    StationMgr.Instance.Stations.Add(station);
                }
            }
        }

        private void contxtMenuStrip_Opening(object sender, CancelEventArgs e)
        {
        }

        public void ExportStationListConfig(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            //List<SingleStationConfig> listStationConfig = new List<SingleStationConfig>();
            //listStationConfig.Clear();

            //foreach (var item in StationMgr.Settings.Station)
            //{
            //    SingleStationConfig singleStation = new SingleStationConfig();
            //    XmlNode stationXml = item.ExportConfig();
            //    if(stationXml != null)
            //    {
            //        singleStation.stationXmlNode = stationXml;
            //        singleStation.stationType = item.StationType;

            //        listStationConfig.Add(singleStation);
            //    }
            //}

            //if (listStationConfig.Count1 > 0)
            //{
            //    bool bRet = XmlSerializerHelper.WriteXML(listStationConfig, filePath, typeof(List<SingleStationConfig>));
            //}
        }

        public void LoadStations(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            if (!File.Exists(filePath)) return;

            //List<SingleStationConfig> listStationConfig = new List<SingleStationConfig>();
            //listStationConfig.Clear();

            //if (File.Exists(filePath))
            //{
            //    listStationConfig = (List<SingleStationConfig>)XmlSerializerHelper.ReadXML(filePath, typeof(List<SingleStationConfig>));
            //}

            //if(listStationConfig.Count1 < 1) { return; }

            //foreach(var item in listStationConfig)
            //{
            //    IStation station = StationFactory.Settings.Create(item.stationType, item.stationXmlNode);
            //    if (station != null)
            //    {
            //        StationMgr.Settings.Station.Add(station);

            //        Debug.WriteLine("Station 3: Create Station " + station.Name);
            //    }
            //}

            foreach (var item in StationMgr.Instance.Stations)
            {
                var rowIdx = dtGridViewStation.Rows.Add();

                dtGridViewStation.Rows[rowIdx].Cells[3].Value = item.StationId;
                dtGridViewStation.Rows[rowIdx].Cells[0].Value = item.Name;
                //dtGridViewStation.Rows[rowIdx].Cells[1].Value = item.Enable;
                //dtGridViewStation.Rows[rowIdx].Cells[2].Value = item.StationType.ToString();
                //dtGridViewStation.Rows[rowIdx].Cells[4].Value = item.Description;

                Debug.WriteLine("Station 4: Add Station to dtGv " + item.Name);
            }
        }

        private void removeStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("是否要移除工站", "重要消息提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;
            var curSelectedIdx = dtGridViewStation.CurrentCell.RowIndex;
            var stationId = (Guid) dtGridViewStation.Rows[curSelectedIdx].Cells[3].Value;
            StationMgr.Instance.RemoveStationByGUID(stationId);

            dtGridViewStation.Rows.RemoveAt(curSelectedIdx);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var curSelectedIdx = dtGridViewStation.CurrentCell.RowIndex;
            var stationId = (Guid) dtGridViewStation.Rows[curSelectedIdx].Cells[3].Value;

            var station = StationMgr.Instance.GetStationByGUID(stationId);
            var configControl = station.ConfigControl;
            //configControl.Dock = DockStyle.Fill;

            if (configControl != null)
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(configControl);
            }
        }


        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            toolStripLabel1.BackColor = Color.Yellow;
            var result = MessageBox.Show("是否保存工程文件配置", "重要消息提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;

            ExportStationListConfig(_stConfigFilePath);
        }

        private void toolStripLabel1_MouseEnter(object sender, EventArgs e)
        {
            toolStripLabel1.BackColor = Color.LightGreen;
        }

        private void toolStripLabel1_MouseHover(object sender, EventArgs e)
        {
            toolStripLabel1.BackColor = Color.LightGreen;
        }

        private void toolStripLabel1_MouseLeave(object sender, EventArgs e)
        {
            toolStripLabel1.BackColor = SystemColors.Control;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("是否加载工程文件配置", "重要消息提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;
            LoadStations(_stConfigFilePath);
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            var file = new MoveProfile();
            file.Name = "xxxx";
            file.AxisName = "aaaa";
            profile.Add("aaa", file);
            profile.Add("bbb", new MoveProfile());
            profile.Add("ccc", new MoveProfile());
            var bRet = XmlSerializerHelper.WriteXML(profile, @"D:\Lead\TaskConfigs\Config.xml", typeof(Dictionary<string, MoveProfile>));
        }
    }
}