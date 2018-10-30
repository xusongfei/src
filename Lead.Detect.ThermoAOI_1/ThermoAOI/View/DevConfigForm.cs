using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.loadUtils;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.FrameworkExtension.userControls;
using Lead.Detect.Helper;
using Lead.Detect.ThermoAOI.Calibration;
using Lead.Detect.ThermoAOI.Machine.newTasks;
using Lead.Detect.ThermoAOI.Product;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalculator;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI.View
{
    public partial class DevConfigForm : DockContent, IFrameworkControl
    {
        public DevConfigForm()
        {
            InitializeComponent();
        }

        private void DevConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void DevConfigForm_Load(object sender, EventArgs e)
        {
            //di/do tab
            diControl1.DiExs = Machine.Machine.Ins.DiExs.Values.ToList();
            diControl1.LoadFramework();
            doControl1.DoExs = Machine.Machine.Ins.DoExs.Values.ToList();
            doControl1.LoadFramework();
            vioControl1.VioExs = Machine.Machine.Ins.VioExs.Values.ToList();
            vioControl1.LoadFramework();
            cyliderControl1.CyExs = Machine.Machine.Ins.CylinderExs.Values.ToList();
            cyliderControl1.LoadFramework();

            //platform tab
            var platforms = Machine.Machine.Ins.Platforms.Values;
            tabControlPlatform.TabPages.Clear();
            foreach (var p in platforms)
            {
                tabControlPlatform.TabPages.Add(p.Name, p.Description + $"({p.Name})");
                var platformControl = new PlatformControl()
                {
                    Dock = DockStyle.Fill,
                };
                platformControl.LoadPlatform(p);

                tabControlPlatform.TabPages[p.Name].Controls.Add(platformControl);
            }


            //config
            propertyGridCommonConfig.SelectedObject = Machine.Machine.Ins.Settings.Common;
            propertyGridMachineConfig.SelectedObject = Machine.Machine.Ins.Settings;
            richTextBoxMachine.Text = Machine.Machine.Ins.SerializeToString();


            //product
            textBoxLeftProductFile.Text = Machine.Machine.Ins.Settings.LeftProjectFilePath;
            textBoxLeftProductFile.ReadOnly = true;
            textBoxRightProductFile.Text = Machine.Machine.Ins.Settings.RightProjectFilePath;
            textBoxRightProductFile.ReadOnly = true;


            //calib
            stationStateControlLeft.Machine = Machine.Machine.Ins;
            stationStateControlLeft.Station = Machine.Machine.Ins.Find<Station>("LeftStation");
            stationStateControlRight.Machine = Machine.Machine.Ins;
            stationStateControlRight.Station = Machine.Machine.Ins.Find<Station>("RightStation");


            tabControlMain.SelectedTab = tabPageProduct;

            FrameworkActivate();
        }


        public void LoadFramework()
        {
        }

        public void FrameworkActivate()
        {
            diControl1.FrameworkActivate();
            doControl1.FrameworkActivate();
            vioControl1.FrameworkActivate();
            cyliderControl1.FrameworkActivate();
        }

        public void FrameworkDeactivate()
        {
            diControl1.FrameworkDeactivate();
            doControl1.FrameworkDeactivate();
            vioControl1.FrameworkDeactivate();
            cyliderControl1.FrameworkDeactivate();
        }

        public void FrameworkUpdate()
        {
        }

        private void richTextBoxMachine_DoubleClick(object sender, EventArgs e)
        {
            var mf = new MachineForm()
            {
                Machine = Machine.Machine.Ins,
            };

            mf.ShowDialog();
        }


        #region product

        private void buttonProjectEditor_Click(object sender, EventArgs e)
        {
            var prj = new FlatnessProjectEditorForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
            };
            prj.ShowDialog();
        }


        private void buttonProductCalculatorEditor_Click(object sender, EventArgs e)
        {
            var prj = new GeometryCalculatorEditorForm()
            {
                StartPosition = FormStartPosition.CenterScreen,
            };

            prj.ShowDialog();
        }

        private void buttonOpenLeftProductFile_Click(object sender, EventArgs e)
        {
            try
            {
                var prj = new FlatnessProjectEditorForm
                {
                    FlatnessProject = FlatnessProject.Load(Machine.Machine.Ins.Settings.LeftProjectFilePath)
                };
                prj.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LeftProjectFiles Edit Fail: {ex.Message}");
            }
        }

        private void buttonOpenRightProductFile_Click(object sender, EventArgs e)
        {
            try
            {
                var prj = new FlatnessProjectEditorForm
                {
                    FlatnessProject = FlatnessProject.Load(Machine.Machine.Ins.Settings.RightProjectFilePath)
                };
                prj.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RightProjectFiles Edit Fail: {ex.Message}");
            }
        }


        private void buttonBrowseLeftProjectFile_Click(object sender, EventArgs e)
        {
            if (Machine.Machine.Ins.Find<Station>("LeftStation").RunningState != RunningState.WaitReset)
            {
                MessageBox.Show($"左工站未停止，请停止运行后更换测试文件！");
                return;
            }


            var fd = new OpenFileDialog
            {
                InitialDirectory = @".\Config",
                Filter = @"(Flatness Project)|*.fprj",
                Multiselect = false
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Machine.Machine.Ins.Settings.LeftProjectFilePath = fd.FileName;
                textBoxLeftProductFile.Text = fd.FileName;
            }
        }

        private void buttonBrowseRightProjectFile_Click(object sender, EventArgs e)
        {
            if (Machine.Machine.Ins.Find<Station>("RightStation").RunningState != RunningState.WaitReset)
            {
                MessageBox.Show($"右工站未停止，请停止运行后更换测试文件！");
                return;
            }

            var fd = new OpenFileDialog
            {
                InitialDirectory = @".\Config",
                Filter = @"(Flatness Project)|*.fprj",
                Multiselect = false
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Machine.Machine.Ins.Settings.RightProjectFilePath = fd.FileName;
                textBoxRightProductFile.Text = fd.FileName;
            }
        }

        #endregion


        #region calib

        /// <summary>
        /// 载具与上平台xy标定计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProductToUpCalib_Click(object sender, EventArgs e)
        {
            var platform = comboBoxPlatformCalib.Text;
            if (platform != "Left" && platform != "Right")
            {
                MessageBox.Show($"标定平台{platform}选择错误！");
                return;
            }
            try
            {
                var calib = AutoPlatformProduct2UpCalib.CreateProductToUpCalib(platform);
                if (calib != null)
                {
                    if (calib.Station.RunningState != RunningState.WaitRun)
                    {
                        throw new Exception($"{calib.Station.Name} not ready");
                    }

                    calib.LogEvent += (log, level) => { LoggerHelper.Log(@".\Calib", log, level); };

                    calib.DoCalib();
                    richTextBoxCalib.AppendText(calib.DisplayOutput());
                    AutoPlatformProduct2UpCalib.SaveProductToUpCalib(calib);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"上平台XY标定计算异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 上下平台xy标定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlatformAlignCalib_Click(object sender, EventArgs e)
        {
            var platform = comboBoxPlatformCalib.Text;
            if (platform != "Left" && platform != "Right")
            {
                MessageBox.Show($"标定平台{platform}选择错误！");
                return;
            }
            try
            {
                var calib = AutoPlatformUp2DownAlignCalib.CreateAlignCalib(platform);
                if (calib != null)
                {
                    if (calib.Station.RunningState != RunningState.WaitRun)
                    {
                        throw new Exception($"{calib.Station.Name} not ready");
                    }

                    Action<string, LogLevel> logeventDeletate = (log, level) => { LoggerHelper.Log(@".\Calib", log, level); };
                    calib.LogEvent += logeventDeletate;

                    var calibForm = new AutoCalibForm
                    {
                        Calib = calib
                    };
                    if (calibForm.ShowDialog() == DialogResult.OK)
                    {
                        richTextBoxCalib.AppendText(calib.DisplayOutput());
                        AutoPlatformUp2DownAlignCalib.SaveAlignCalib(calib);
                    }

                    calib.LogEvent -= logeventDeletate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{platform} 上下平台XY标定计算异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 上下平台xy标定计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlatformAlignCalc_Click(object sender, EventArgs e)
        {
            var platform = comboBoxPlatformCalib.Text;
            if (platform != "Left" && platform != "Right")
            {
                MessageBox.Show($"标定平台{platform}选择错误！");
                return;
            }
            try
            {
                var calib = AutoPlatformUp2DownAlignCalib.CreateAlignCalib(platform);
                if (calib != null)
                {
                    if (calib.Station.RunningState != RunningState.WaitRun)
                    {
                        throw new Exception($"{calib.Station.Name} not ready");
                    }

                    calib.LogEvent += (log, level) => { LoggerHelper.Log(@".\Calib", log, level); };

                    calib.do_clampy_cy = null;
                    calib.do_gt2_cy = null;
                    calib.PlatformCarrier = null;
                    calib.Platform1 = null;
                    calib.Platform2 = null;

                    calib.DoCalib();

                    richTextBoxCalib.AppendText(calib.DisplayOutput());

                    AutoPlatformUp2DownAlignCalib.SaveAlignCalib(calib);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{platform} 上下平台XY标定计算异常：{ex.Message}");
            }
        }


        /// <summary>
        /// 高度标定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHeightCalib_Click(object sender, EventArgs e)
        {
            var platform = comboBoxPlatformCalib.Text;
            if (platform != "Left" && platform != "Right")
            {
                MessageBox.Show($"标定平台{platform}选择错误！");
                return;
            }
            try
            {
                var calib = AutoPlatformHeightCalib.CreateHeightCalib(platform);
                if (calib != null)
                {
                    Action<string, LogLevel> logeventDelegate = (log, level) => { LoggerHelper.Log(@".\Calib", log, level); };
                    calib.LogEvent += logeventDelegate;

                    var calibForm = new AutoCalibForm
                    {
                        Calib = calib
                    };
                    if (calibForm.ShowDialog() == DialogResult.OK)
                    {
                        var ret = calibForm.Calib as AutoPlatformHeightCalib;
                        if (ret != null)
                        {
                            AutoPlatformHeightCalib.SaveHeightCalib(ret);
                        }
                        richTextBoxCalib.AppendText(calib.DisplayOutput());
                    }

                    calib.LogEvent -= logeventDelegate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{platform} 高度标定异常：{ex.Message}");
            }
        }


        /// <summary>
        /// 高度标定计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHeightCalibCalc_Click(object sender, EventArgs e)
        {
            var platform = comboBoxPlatformCalib.Text;
            if (platform != "Left" && platform != "Right")
            {
                MessageBox.Show($"标定平台{platform}选择错误！");
                return;
            }
            try
            {
                var calib = AutoPlatformHeightCalib.CreateHeightCalib(platform);
                if (calib != null)
                {
                    if (calib.Station.RunningState != RunningState.WaitRun)
                    {
                        throw new Exception($"{calib.Station.Name} not ready");
                    }

                    calib.LogEvent += (log, level) => { LoggerHelper.Log(@".\Calib", log, level); };

                    calib.PlatformCarrier = null;
                    calib.Platform1 = null;
                    calib.Platform2 = null;
                    calib.GtController = null;
                    calib.do_clampy_cy = null;
                    calib.do_gt2_cy = null;

                    calib.DoCalib();

                    richTextBoxCalib.AppendText(calib.DisplayOutput());

                    AutoPlatformHeightCalib.SaveHeightCalib(calib);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{platform} 高度标定计算异常：{ex.Message}");
            }
        }


        #endregion


        #region test

        private void buttonLGTTest_Click(object sender, EventArgs e)
        {
            if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            {
                MessageBox.Show("设备未复位!");
                return;
            }

            var gt = (Machine.Machine.Ins.Find<StationTask>("LeftTrans") as newLeftTransTask)?.GtController;
            if (gt == null)
            {
                return;
            }

            MessageBox.Show(string.Join(",", gt.ReadData().Select(d => d.ToString("F3"))));
        }

        private void buttonRGT_Click(object sender, EventArgs e)
        {
            if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            {
                MessageBox.Show("设备未复位!");
                return;
            }

            var gt = (Machine.Machine.Ins.Find<StationTask>("LeftTrans") as newLeftTransTask)?.GtController;
            if (gt == null)
            {
                return;
            }

            MessageBox.Show(string.Join(",", gt.ReadData().Select(d => d.ToString("F3"))));
        }


        private void buttonLBarcodeOpen_Click(object sender, EventArgs e)
        {
            //if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            //{
            //    MessageBox.Show("设备未复位!");
            //    return;
            //}


            try
            {
                var controller = (Machine.Machine.Ins.Find<StationTask>("LeftMeasureDown") as newMeasureDownTask)?.BarcodeController;
                if (controller == null)
                {
                    return;
                }

                controller.Open();
                MessageBox.Show($"BarcodeOpenStatus:{controller.IsOpen}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLBarcodeClose_Click(object sender, EventArgs e)
        {
            //if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            //{
            //    MessageBox.Show("设备未复位!");
            //    return;
            //}


            try
            {
                var controller = (Machine.Machine.Ins.Find<StationTask>("LeftMeasureDown") as newMeasureDownTask)?.BarcodeController;
                if (controller == null)
                {
                    return;
                }

                controller.Close();
                MessageBox.Show($"BarcodeOpenStatus:{controller.IsOpen}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLBarcodeTrigger_Click(object sender, EventArgs e)
        {
            //if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            //{
            //    MessageBox.Show("设备未复位!");
            //    return;
            //}


            try
            {
                var controller = (Machine.Machine.Ins.Find<StationTask>("LeftMeasureDown") as newMeasureDownTask)?.BarcodeController;
                if (controller == null)
                {
                    return;
                }

                var res = controller.Trigger();
                MessageBox.Show("ReadBarcode:" + res ?? "NULL");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRBacodeOpen_Click(object sender, EventArgs e)
        {
            //if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            //{
            //    MessageBox.Show("设备未复位!");
            //    return;
            //}


            try
            {
                var controller = (Machine.Machine.Ins.Find<StationTask>("RightMeasureDown") as newMeasureDownTask)?.BarcodeController;
                if (controller == null)
                {
                    return;
                }

                controller.Open();
                MessageBox.Show($"BarcodeOpenStatus:{controller.IsOpen}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRBarcodeClose_Click(object sender, EventArgs e)
        {
            //if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            //{
            //    MessageBox.Show("设备未复位!");
            //    return;
            //}


            try
            {
                var controller = (Machine.Machine.Ins.Find<StationTask>("RightMeasureDown") as newMeasureDownTask)?.BarcodeController;
                if (controller == null)
                {
                    return;
                }

                controller.Close();
                MessageBox.Show($"BarcodeOpenStatus:{controller.IsOpen}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRBarcodeTrigger_Click(object sender, EventArgs e)
        {
            //if (Machine.Machine.Ins.RunningState != RunningState.WaitRun)
            //{
            //    MessageBox.Show("设备未复位!");
            //    return;
            //}


            try
            {
                var controller = (Machine.Machine.Ins.Find<StationTask>("RightMeasureDown") as newMeasureDownTask)?.BarcodeController;
                if (controller == null)
                {
                    return;
                }

                var res = controller.Trigger();
                MessageBox.Show("ReadBarcode:" + res ?? "NULL");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}