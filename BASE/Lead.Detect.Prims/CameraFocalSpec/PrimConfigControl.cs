using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonStruct;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimCameraFocalSpec.FocalSpecCore;

namespace Lead.Detect.PrimCameraFocalSpec
{
    public delegate void UpdatePrimConnState(PrimConnState state);

    public delegate void UpdatePrimRunState(PrimRunState state);

    public partial class PrimConfigControl : UserControl
    {
        public FocalSpecConfig _config;
        public FSCoreConfig _fsCoreConfig;
        public PrimFocalSpec _primFocalSpec;

        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public PrimConfigControl(FocalSpecConfig config, FSCoreConfig fsCoreConfig, PrimFocalSpec prim)
        {
            InitializeComponent();

            _config = config;
            _fsCoreConfig = fsCoreConfig;
            _primFocalSpec = prim;
        }

        public string FocalSpaceName
        {
            set { tTaskBoxName.Text = value; }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxBasicConfigPath.Text)) _config.BasicConfigPath = tBoxBasicConfigPath.Text;

            if (!string.IsNullOrEmpty(comBoxCamIdx.Text)) _config.CamIdxStr = comBoxCamIdx.Text;

            if (!string.IsNullOrEmpty(tBoxZCalibrationFilePath.Text)) _fsCoreConfig.ZCalibrationFile = tBoxZCalibrationFilePath.Text;

            if (!string.IsNullOrEmpty(tBoxXCalibrationFilePath.Text)) _fsCoreConfig.XCalibrationFile = tBoxXCalibrationFilePath.Text;

            if (!string.IsNullOrEmpty(tBoxLedPulseWidth.Text)) _fsCoreConfig.LedPulseWidth = Convert.ToInt32(tBoxLedPulseWidth.Text);

            if (!string.IsNullOrEmpty(tBoxMaxLedPulseWidth.Text)) _fsCoreConfig.MaxLedPulseWidth = Convert.ToInt32(tBoxMaxLedPulseWidth.Text);

            if (!string.IsNullOrEmpty(tBoxFreq.Text)) _fsCoreConfig.Freq = Convert.ToInt32(tBoxFreq.Text);

            if (!string.IsNullOrEmpty(tBoxIpAddress.Text)) _fsCoreConfig.IpAddress = tBoxIpAddress.Text;

            if (!string.IsNullOrEmpty(tBoxUiWindowHeight.Text)) _fsCoreConfig.UiWindowHeight = Convert.ToInt32(tBoxUiWindowHeight.Text);

            _fsCoreConfig.IsAgcEnabled = cBoxIsAgcEnabled.Checked;

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _fsCoreConfig.AgcTargetIntensity = Convert.ToSingle(tBoxAgcTargetIntensity.Text);

            switch (comBoxSelectedLayerConfig.SelectedIndex)
            {
                case 0:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.All;
                    break;
                case 1:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.Top;
                    break;
                case 2:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.Bottom;
                    break;
                case 3:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.Brightest;
                    break;
                default:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.All;
                    break;
            }

            _fsCoreConfig.IsXAxisTrige = cBoxIsXAxisTrige.Checked;

            _fsCoreConfig.IsExternalPulsingEnabled = cBoxIsExternalTrig.Checked;

            if (!string.IsNullOrEmpty(tBoxTrigInterval.Text)) _fsCoreConfig.TrigInterval = Convert.ToSingle(tBoxTrigInterval.Text);

            _config.IsBind = cBoxIsBind.Checked;

            if (listBoxFilePath.Items.Count > 0)
            {
                if (_config.ListTrigPosFilePath == null) _config.ListTrigPosFilePath = new List<string>();

                _config.ListTrigPosFilePath.Clear();

                foreach (var item in listBoxFilePath.Items) _config.ListTrigPosFilePath.Add(item.ToString());
            }


            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum1 = Convert.ToInt32(PionTextBox0.Text);

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum2 = Convert.ToInt32(PionTextBox1.Text);

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum3 = Convert.ToInt32(PionTextBox2.Text);

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum4 = Convert.ToInt32(PionTextBox3.Text);

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum5 = Convert.ToInt32(PionTextBox4.Text);

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum6 = Convert.ToInt32(PionTextBox5.Text);

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum7 = Convert.ToInt32(PionTextBox6.Text);

            if (!string.IsNullOrEmpty(tBoxAgcTargetIntensity.Text)) _config.EachparagraphNum8 = Convert.ToInt32(PionTextBox7.Text);

            //_fsCoreConfig保存
            if (string.IsNullOrEmpty(_config.BasicConfigPath))
                GlobalFunc.SerializeToXml(FSCoreDefines.ApplicationSettingsFile, _fsCoreConfig);
            else
                GlobalFunc.SerializeToXml(_config.BasicConfigPath, _fsCoreConfig);
        }

        private void btnBasicConfigPath_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tBoxBasicConfigPath.Text = openFileDialog.FileName;

                if (string.IsNullOrEmpty(tBoxBasicConfigPath.Text)) return;

                if (!File.Exists(tBoxBasicConfigPath.Text))
                {
                    MessageBox.Show("选择文件不存在");
                    return;
                }

                _config.BasicConfigPath = tBoxBasicConfigPath.Text;

                if (_primFocalSpec != null)
                {
                    //_config.BasicConfigPath = tBoxBasicConfigPath.Text;
                    var iRet = _primFocalSpec.ImportFSCoreConfig(_config.BasicConfigPath);
                    if (iRet != 0) return;

                    PrimConfigControlUpdate();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _primFocalSpec.ClearBatch(99);
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            if (_primFocalSpec == null) return;

            var iRet = _primFocalSpec.IPrimStart();
            if (iRet != 0) return;

            btnGrab.BackColor = Color.LightGreen;
            btnStop.BackColor = Color.Silver;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _primFocalSpec.LoadCameraList();
            UpdateCameraList();
        }

        private void btnLoadTrigPos_Click(object sender, EventArgs e)
        {
            var cnt = _primFocalSpec.LoadTrigPos();

            tBoxPosNum.Text = cnt.ToString();
        }

        private void btnPathAdd_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                if (!string.IsNullOrEmpty(openFileDialog.FileName))
                    listBoxFilePath.Items.Add(openFileDialog.FileName);
        }

        private void btnPathRemove_Click(object sender, EventArgs e)
        {
            var idx = listBoxFilePath.SelectedIndex;
            if (idx < 0) return;

            listBoxFilePath.Items.RemoveAt(idx);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (_primFocalSpec == null) return;

            var msg = "";
            var iRet = 0;
            iRet = _primFocalSpec.IPrimInit();

            if (iRet != 0) return;

            iRet = _primFocalSpec.IPrimConnect(ref msg);

            if (iRet != 0) return;

            btnInit.BackColor = Color.SkyBlue;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var savePath = "";
            var lst1 = new List<int>();

            #region 3D数据的保存

            if (checkBox3D.Checked)
            {
                saveFileDialog.Filter = "*.txt|*.csv";
                saveFileDialog.FileName = "Data";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    savePath = saveFileDialog.FileName;
                else
                    return;


                for (var PiontCount = 0; PiontCount < _primFocalSpec._listTrigPos.Count; PiontCount++)
                {
                    var fsPointList = _primFocalSpec.GetBatch(PiontCount + 1);

                    if (fsPointList == null)
                    {
                        MessageBox.Show("fsPointList is null");
                        return;
                    }

                    if (fsPointList.Count <= 0)
                    {
                        MessageBox.Show("fsPointList.Count is 0");
                        return;
                    }

                    var locationSt = fsPointList[0][0].Location;

                    if (_config.IsBind && _primFocalSpec._listTrigPos[PiontCount].Count > 0)
                        foreach (var points in fsPointList)
                        {
                            var idx = (points[0].Location - locationSt) / 2;
                            lst1.Add(points[0].Location);
                            var posY = _primFocalSpec._listTrigPos[PiontCount][idx];

                            for (var i = 0; i < points.Count; i++) points[i].Y = posY / 1000;
                        }

                    try
                    {
                        if (savePath.Contains(".txt"))
                        {
                            SaveData2Txt(savePath, fsPointList);
                        }
                        else if (savePath.Contains(".csv"))
                        {
                            //Action action = () =>
                            //{
                            FileStream fs = null;
                            StreamWriter sw = null;
                            var count = _primFocalSpec.GetBatchCount();

                            var pathNameStrs = savePath.Split('.');
                            if (pathNameStrs.Length < 2) return;
                            var pathNameStr = pathNameStrs[0];
                            var pathTypeStr = pathNameStrs[1];

                            var path = pathNameStr + PiontCount + "." + pathTypeStr;
                            fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                            sw = new StreamWriter(fs, new UnicodeEncoding());
                            var text0 = new StringBuilder();
                            for (var j = 0; j < fsPointList.Count; j++)
                            {
                                var item = fsPointList[j];
                                foreach (var item1 in item)
                                {
                                    text0.Clear();
                                    text0.Append(item1.X + "," + item1.Y + "," + item1.Z + "," + item1.Intensity + "\r\n");
                                    sw.Write(text0);
                                }
                            }

                            sw.Flush();
                            sw.Close();

                            //};
                            //action.BeginInvoke(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            #endregion

            #region 2D数据的保存

            if (checkBox2D.Checked)
            {
                saveFileDialog.Filter = "*.bmp|*.bmp";
                saveFileDialog.FileName = "Data";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    savePath = saveFileDialog.FileName;
                else
                    return;


                for (var PiontCount = 0; PiontCount < _primFocalSpec._listTrigPos.Count; PiontCount++)
                {
                    var fsPointList = _primFocalSpec.GetBatch(PiontCount + 1);
                    var fsPointListTemp = new List<List<FSPoint>>();
                    if (fsPointList == null)
                    {
                        MessageBox.Show("fsPointList is null");
                        return;
                    }

                    if (fsPointList.Count <= 0)
                    {
                        MessageBox.Show("fsPointList.Count is 0");
                        return;
                    }


                    foreach (var points in fsPointList) fsPointListTemp.Add(points);


                    //foreach (List<FSPoint> points in fsPointListTemp)
                    //{

                    //    for (int i = 0; i < points.Count; i++)
                    //    {
                    //        points[i].X = points[i].X;
                    //    }

                    // }
                    try
                    {
                        if (savePath.Contains(".txt"))
                        {
                            SaveData2Txt(savePath, fsPointList);
                        }
                        else if (savePath.Contains(".bmp"))
                        {
                            //Action action = () =>
                            //{
                            //FileStream fs = null;
                            //StreamWriter sw = null;
                            var count = _primFocalSpec.GetBatchCount();

                            var pathNameStrs = savePath.Split('.');
                            if (pathNameStrs.Length < 2) return;
                            var pathNameStr = pathNameStrs[0];
                            var pathTypeStr = pathNameStrs[1];

                            var path = pathNameStr + PiontCount + "." + pathTypeStr;

                            _primFocalSpec.SaveToPointCloudFile(path, fsPointListTemp, _primFocalSpec._listTrigPos[PiontCount].Count, true);
                            //};
                            //action.BeginInvoke(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            #endregion
        }

        private void btnSave2XML_Click(object sender, EventArgs e)
        {
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_primFocalSpec == null) return;

            var iRet = _primFocalSpec.IPrimStop();
            if (iRet != 0) return;

            btnGrab.BackColor = Color.Silver;
            btnStop.BackColor = Color.LightGreen;
        }

        private void btnXCalibrationFilePath_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) tBoxXCalibrationFilePath.Text = openFileDialog.FileName;
        }

        private void btnZCalibrationFilePath_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) tBoxZCalibrationFilePath.Text = openFileDialog.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PionTextBox0.Text = _primFocalSpec._listTrigPos[0].Count.ToString();
            PionTextBox1.Text = _primFocalSpec._listTrigPos[1].Count.ToString();
            PionTextBox2.Text = _primFocalSpec._listTrigPos[2].Count.ToString();
            PionTextBox3.Text = _primFocalSpec._listTrigPos[3].Count.ToString();
            PionTextBox4.Text = _primFocalSpec._listTrigPos[4].Count.ToString();
            PionTextBox5.Text = _primFocalSpec._listTrigPos[5].Count.ToString();
            PionTextBox6.Text = _primFocalSpec._listTrigPos[6].Count.ToString();
            PionTextBox7.Text = _primFocalSpec._listTrigPos[7].Count.ToString();
            var ALLNUMBER = 0;
            for (var I = 0; I < 8; I++) ALLNUMBER = ALLNUMBER + _primFocalSpec._listTrigPos[I].Count;
            PionTextBox.Text = ALLNUMBER.ToString();
        }

        private void cBoxIsAgcEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _fsCoreConfig.IsAgcEnabled = cBoxIsAgcEnabled.Checked;
        }

        private void cBoxIsBind_CheckedChanged(object sender, EventArgs e)
        {
            _config.IsBind = cBoxIsBind.Checked;
        }

        private void cBoxIsExternalTrig_CheckedChanged(object sender, EventArgs e)
        {
            _fsCoreConfig.IsExternalPulsingEnabled = cBoxIsExternalTrig.Checked;
        }

        private void cBoxIsXAxisTrige_CheckedChanged(object sender, EventArgs e)
        {
            _fsCoreConfig.IsXAxisTrige = cBoxIsXAxisTrige.Checked;
        }

        private void checkBoxUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUpdate.Checked)
                tmrUpdate.Start();
            else
                tmrUpdate.Stop();
        }

        private void comBoxCamIdx_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comIdx = comBoxCamIdx.SelectedIndex;

            if (comIdx < _primFocalSpec._fsCameraList.Count)
            {
                var item = _primFocalSpec._fsCameraList.ElementAt(comIdx);

                if (!item.Value)
                    _primFocalSpec.SetCameraId(item.Key);
                else
                    MessageBox.Show("Camera selected is in used");
            }
        }

        private void comBoxSelectedLayerConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comBoxSelectedLayerConfig.SelectedIndex)
            {
                case 0:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.All;
                    break;
                case 1:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.Top;
                    break;
                case 2:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.Bottom;
                    break;
                case 3:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.Brightest;
                    break;
                default:
                    _fsCoreConfig.SelectedLayerConfig = ExportLayer.All;
                    break;
            }

            _primFocalSpec.SetFSCoreCallBackFuncByExportLayer(_fsCoreConfig.SelectedLayerConfig);
        }

        private string DataTurn2Str(List<List<FSPoint>> data)
        {
            var text = new StringBuilder();

            foreach (var item0 in data)
            foreach (var item1 in item0)
                text.Append(item1.Intensity + " " + item1.X + " " + item1.Y + " " + item1.Z + " " + "\n");

            return text.ToString();
        }

        private void label41_Click(object sender, EventArgs e)
        {
        }

        private void label44_Click(object sender, EventArgs e)
        {
        }

        private void PionTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void PrimConfigControl_Load(object sender, EventArgs e)
        {
            PrimConfigControlUpdate();
        }

        private void PrimConfigControlUpdate()
        {
            tBoxBasicConfigPath.Text = _config.BasicConfigPath;

            comBoxCamIdx.Text = _config.CamIdxStr;

            tBoxZCalibrationFilePath.Text = _fsCoreConfig.ZCalibrationFile;

            tBoxXCalibrationFilePath.Text = _fsCoreConfig.XCalibrationFile;

            tBoxLedPulseWidth.Text = _fsCoreConfig.LedPulseWidth.ToString();

            tBoxMaxLedPulseWidth.Text = _fsCoreConfig.MaxLedPulseWidth.ToString();

            tBoxFreq.Text = _fsCoreConfig.Freq.ToString();

            tBoxIpAddress.Text = _fsCoreConfig.IpAddress;

            tBoxUiWindowHeight.Text = _fsCoreConfig.UiWindowHeight.ToString();

            cBoxIsAgcEnabled.Checked = _fsCoreConfig.IsAgcEnabled;

            tBoxAgcTargetIntensity.Text = _fsCoreConfig.AgcTargetIntensity.ToString();


            switch (_fsCoreConfig.SelectedLayerConfig)
            {
                case ExportLayer.All:
                    comBoxSelectedLayerConfig.SelectedIndex = 0;
                    break;
                case ExportLayer.Top:
                    comBoxSelectedLayerConfig.SelectedIndex = 1;
                    break;
                case ExportLayer.Bottom:
                    comBoxSelectedLayerConfig.SelectedIndex = 2;
                    break;
                case ExportLayer.Brightest:
                    comBoxSelectedLayerConfig.SelectedIndex = 3;
                    break;
                default:
                    comBoxSelectedLayerConfig.SelectedIndex = 0;
                    break;
            }

            cBoxIsXAxisTrige.Checked = _fsCoreConfig.IsXAxisTrige;

            cBoxIsExternalTrig.Checked = _fsCoreConfig.IsExternalPulsingEnabled;

            tBoxTrigInterval.Text = _fsCoreConfig.TrigInterval.ToString();

            FocalSpaceName = _primFocalSpec.Name;

            if (_config.ListTrigPosFilePath != null)
                if (_config.ListTrigPosFilePath.Count > 0)
                    foreach (var path in _config.ListTrigPosFilePath)
                        listBoxFilePath.Items.Add(path);

            cBoxIsBind.Checked = _config.IsBind;

            tBoxRegPulseWidthFloat.Text = _fsCoreConfig.RegPulseWidthFloat.ToString();

            tBoxPeakThreshold.Text = _fsCoreConfig.PeakThreshold.ToString();

            tBoxPeakFirLength.Text = _fsCoreConfig.PeakFirLength.ToString();

            tBoxGain.Text = _fsCoreConfig.Gain.ToString();

            tBoxRegPulseDivider.Text = _fsCoreConfig.RegPulseDivider.ToString();

            tBoxImageHeight.Text = _fsCoreConfig.ImageHeight.ToString();

            tBoxImageOffsety.Text = _fsCoreConfig.ImageOffsetY.ToString();

            tBoxImageHeightZeroPosition.Text = _fsCoreConfig.ImageHeightZeroPosition.ToString();

            tBoxFlipXX0.Text = _fsCoreConfig.FlipXX0.ToString();

            tBoxFlipXEnabled.Text = _fsCoreConfig.FlipXEnabled.ToString();

            tBoxReordering.Text = _fsCoreConfig.Reordering.ToString();

            tBoxDetectMissingFirstLayer.Text = _fsCoreConfig.DetectMissingFirstLayer.ToString();

            tBoxFillGapXMax.Text = _fsCoreConfig.FillGapXMax.ToString();

            tBoxAverageZFilterSize.Text = _fsCoreConfig.AverageZFilterSize.ToString();

            tBoxAverageIntensityFilterSize.Text = _fsCoreConfig.AverageIntensityFilterSize.ToString();

            tBoxResampleLineXResolution.Text = _fsCoreConfig.ResampleLineXResolution.ToString();

            PionTextBox0.Text = _config.EachparagraphNum1.ToString();
            PionTextBox1.Text = _config.EachparagraphNum2.ToString();
            PionTextBox2.Text = _config.EachparagraphNum3.ToString();
            PionTextBox3.Text = _config.EachparagraphNum4.ToString();
            PionTextBox4.Text = _config.EachparagraphNum5.ToString();
            PionTextBox5.Text = _config.EachparagraphNum6.ToString();
            PionTextBox6.Text = _config.EachparagraphNum7.ToString();
            PionTextBox7.Text = _config.EachparagraphNum8.ToString();
        }

        private void SaveData2Csv(string path, List<List<FSPoint>> data)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            var pathStrs = path.Split('.');


            try
            {
                var i = 0;
                var j = 0;
                var pathT0 = pathStrs[0] + j + "." + pathStrs[1];
                fs = new FileStream(pathT0, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, new UnicodeEncoding());
                var text = new StringBuilder();

                foreach (var item0 in data)
                {
                    if (i >= 3000)
                    {
                        i = 0;
                        j++;
                        sw.Flush();
                        sw.Close();
                        var pathT1 = pathStrs[0] + j + "." + pathStrs[1];
                        fs = new FileStream(pathT1, FileMode.Create, FileAccess.Write);
                        sw = new StreamWriter(fs, new UnicodeEncoding());
                    }

                    foreach (var item1 in item0)
                    {
                        text.Clear();
                        text.Append(item1.Intensity + "," + item1.X + "," + item1.Y + "," + item1.Z + "\r\n");
                        sw.Write(text);
                    }

                    i++;
                }

                sw.Flush();
                sw.Close();
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }

        private void SaveData2Txt(string path, List<List<FSPoint>> data)
        {
            var fs = new FileStream(path, FileMode.Create);
            var sw = new StreamWriter(fs);


            var text = new StringBuilder();

            foreach (var item0 in data)
            foreach (var item1 in item0)
            {
                text.Clear();
                text.Append(item1.Intensity + " " + item1.X + " " + item1.Y + " " + item1.Z + "\r\n");
                sw.Write(text);
            }

            sw.Flush();

            sw.Close();
            fs.Close();
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
                switch (_primFocalSpec.PrimConnStat)
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
                switch (_primFocalSpec.PrimRunStat)
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

        private void tBoxAverageIntensityFilterSize_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxAverageIntensityFilterSize.Text)) _fsCoreConfig.AverageIntensityFilterSize = Convert.ToSingle(tBoxAverageIntensityFilterSize.Text);
        }

        private void tBoxAverageZFilterSize_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxAverageZFilterSize.Text)) _fsCoreConfig.AverageZFilterSize = Convert.ToSingle(tBoxAverageZFilterSize.Text);
        }

        private void tBoxDetectMissingFirstLayer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxDetectMissingFirstLayer.Text)) _fsCoreConfig.DetectMissingFirstLayer = Convert.ToSingle(tBoxDetectMissingFirstLayer.Text);
        }

        private void tBoxFillGapXMax_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxFillGapXMax.Text)) _fsCoreConfig.FillGapXMax = Convert.ToSingle(tBoxFillGapXMax.Text);
        }

        private void tBoxFlipXEnabled_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxFlipXEnabled.Text)) _fsCoreConfig.FlipXEnabled = Convert.ToInt32(tBoxFlipXEnabled.Text);
        }

        private void tBoxFlipXX0_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxFlipXX0.Text)) _fsCoreConfig.FlipXX0 = Convert.ToSingle(tBoxFlipXX0.Text);
        }

        private void tBoxFreq_TextChanged(object sender, EventArgs e)
        {
        }

        private void tBoxGain_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxGain.Text)) _fsCoreConfig.Gain = Convert.ToDouble(tBoxGain.Text);
        }

        private void tBoxImageHeight_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxImageHeight.Text)) _fsCoreConfig.ImageHeight = Convert.ToInt32(tBoxImageHeight.Text);
        }

        private void tBoxImageHeightZeroPosition_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxImageHeightZeroPosition.Text)) _fsCoreConfig.ImageHeightZeroPosition = Convert.ToInt32(tBoxImageHeightZeroPosition.Text);
        }

        private void tBoxImageOffsety_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxImageOffsety.Text)) _fsCoreConfig.ImageOffsetY = Convert.ToInt32(tBoxImageOffsety.Text);
        }

        private void tBoxMaxLedPulseWidth_TextChanged(object sender, EventArgs e)
        {
        }

        private void tBoxPeakFirLength_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxPeakFirLength.Text)) _fsCoreConfig.PeakFirLength = Convert.ToInt32(tBoxPeakFirLength.Text);
        }

        private void tBoxPeakThreshold_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxPeakThreshold.Text)) _fsCoreConfig.PeakThreshold = Convert.ToInt32(tBoxPeakThreshold.Text);
        }

        private void tBoxRegPulseDivider_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxRegPulseDivider.Text)) _fsCoreConfig.RegPulseDivider = Convert.ToInt32(tBoxRegPulseDivider.Text);
        }

        private void tBoxRegPulseWidthFloat_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxRegPulseWidthFloat.Text)) _fsCoreConfig.RegPulseWidthFloat = Convert.ToSingle(tBoxRegPulseWidthFloat.Text);
        }

        private void tBoxReordering_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxReordering.Text)) _fsCoreConfig.Reordering = Convert.ToInt32(tBoxReordering.Text);
        }

        private void tBoxResampleLineXResolution_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxResampleLineXResolution.Text)) _fsCoreConfig.ResampleLineXResolution = Convert.ToSingle(tBoxResampleLineXResolution.Text);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            //List<List<FSPoint>> fsPointList = _primFocalSpec.GetBatch();

            //if(fsPointList == null)
            //{ return; }

            //lbCnt.Text = fsPointList.Count.ToString();

            lbCnt.Text = _primFocalSpec.GetBatchCount().ToString();
        }

        private void tPageAdvanceConfig_Click(object sender, EventArgs e)
        {
        }

        private void tPageBasicConfig_Click(object sender, EventArgs e)
        {
        }

        public void UpdateCameraList()
        {
            if (_primFocalSpec == null) return;

            if (_primFocalSpec._fsCameraList == null) return;

            if (_primFocalSpec._fsCameraList.Count <= 0) return;

            foreach (var item in _primFocalSpec._fsCameraList)
            {
                string idStr;
                if (item.Value)
                    idStr = item.Key;
                else
                    idStr = item.Key;

                comBoxCamIdx.Items.Add(idStr);
            }
        }
    }
}