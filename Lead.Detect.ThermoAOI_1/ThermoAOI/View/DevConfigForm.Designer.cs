using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.userControls;

namespace Lead.Detect.ThermoAOI.View
{
    partial class DevConfigForm
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param Name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tPageIO = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.diControl1 = new Lead.Detect.FrameworkExtension.userControls.DiControl();
            this.doControl1 = new Lead.Detect.FrameworkExtension.userControls.DoControl();
            this.tabPageCylinder = new System.Windows.Forms.TabPage();
            this.cyliderControl1 = new Lead.Detect.FrameworkExtension.userControls.CylinderControl();
            this.tPageVio = new System.Windows.Forms.TabPage();
            this.vioControl1 = new Lead.Detect.FrameworkExtension.userControls.VioControl();
            this.tabPagePlatforms = new System.Windows.Forms.TabPage();
            this.tabControlPlatform = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.platformXyzControl1 = new Lead.Detect.FrameworkExtension.platforms.motionPlatforms.PlatformControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.platformXyzControl2 = new Lead.Detect.FrameworkExtension.platforms.motionPlatforms.PlatformControl();
            this.tPageCalib = new System.Windows.Forms.TabPage();
            this.richTextBoxCalib = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPlatformCalib = new System.Windows.Forms.ComboBox();
            this.buttonPlatformAlignCalc = new System.Windows.Forms.Button();
            this.buttonPlatformAlignTest = new System.Windows.Forms.Button();
            this.buttonXYPlatformCalib = new System.Windows.Forms.Button();
            this.buttonHeightCalibCalc = new System.Windows.Forms.Button();
            this.buttonHeightCalib = new System.Windows.Forms.Button();
            this.stationStateControlRight = new Lead.Detect.FrameworkExtension.stateMachine.StationStateControl();
            this.stationStateControlLeft = new Lead.Detect.FrameworkExtension.stateMachine.StationStateControl();
            this.tPageSettings = new System.Windows.Forms.TabPage();
            this.tabControlConfig = new System.Windows.Forms.TabControl();
            this.tabPageCOMMON = new System.Windows.Forms.TabPage();
            this.propertyGridCommonConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPageSETTINGS = new System.Windows.Forms.TabPage();
            this.propertyGridMachineConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPageMachine = new System.Windows.Forms.TabPage();
            this.richTextBoxMachine = new System.Windows.Forms.RichTextBox();
            this.tabPageProduct = new System.Windows.Forms.TabPage();
            this.measureProjectSelctionControl2 = new Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Project.MeasureProjectSelctionControl();
            this.measureProjectSelctionControl1 = new Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Project.MeasureProjectSelctionControl();
            this.tabPageTest = new System.Windows.Forms.TabPage();
            this.buttonProductCalculatorEditor = new System.Windows.Forms.Button();
            this.buttonRBarcodeTrigger = new System.Windows.Forms.Button();
            this.buttonRBarcodeClose = new System.Windows.Forms.Button();
            this.buttonRBacodeOpen = new System.Windows.Forms.Button();
            this.buttonRGT = new System.Windows.Forms.Button();
            this.buttonLBarcodeTrigger = new System.Windows.Forms.Button();
            this.buttonLBarcodeClose = new System.Windows.Forms.Button();
            this.buttonLBarcodeOpen = new System.Windows.Forms.Button();
            this.buttonLGTTest = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tPageIO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageCylinder.SuspendLayout();
            this.tPageVio.SuspendLayout();
            this.tabPagePlatforms.SuspendLayout();
            this.tabControlPlatform.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tPageCalib.SuspendLayout();
            this.tPageSettings.SuspendLayout();
            this.tabControlConfig.SuspendLayout();
            this.tabPageCOMMON.SuspendLayout();
            this.tabPageSETTINGS.SuspendLayout();
            this.tabPageMachine.SuspendLayout();
            this.tabPageProduct.SuspendLayout();
            this.tabPageTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tPageIO);
            this.tabControlMain.Controls.Add(this.tabPageCylinder);
            this.tabControlMain.Controls.Add(this.tPageVio);
            this.tabControlMain.Controls.Add(this.tabPagePlatforms);
            this.tabControlMain.Controls.Add(this.tPageCalib);
            this.tabControlMain.Controls.Add(this.tPageSettings);
            this.tabControlMain.Controls.Add(this.tabPageProduct);
            this.tabControlMain.Controls.Add(this.tabPageTest);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControlMain.ItemSize = new System.Drawing.Size(60, 35);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1008, 661);
            this.tabControlMain.TabIndex = 21;
            // 
            // tPageIO
            // 
            this.tPageIO.BackColor = System.Drawing.SystemColors.Control;
            this.tPageIO.Controls.Add(this.splitContainer1);
            this.tPageIO.Location = new System.Drawing.Point(4, 39);
            this.tPageIO.Margin = new System.Windows.Forms.Padding(2);
            this.tPageIO.Name = "tPageIO";
            this.tPageIO.Padding = new System.Windows.Forms.Padding(2);
            this.tPageIO.Size = new System.Drawing.Size(1000, 618);
            this.tPageIO.TabIndex = 10;
            this.tPageIO.Text = "输入输出";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.diControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.doControl1);
            this.splitContainer1.Size = new System.Drawing.Size(996, 614);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 4;
            // 
            // diControl1
            // 
            this.diControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.diControl1.Location = new System.Drawing.Point(0, 0);
            this.diControl1.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.diControl1.Name = "diControl1";
            this.diControl1.Size = new System.Drawing.Size(500, 614);
            this.diControl1.TabIndex = 3;
            // 
            // doControl1
            // 
            this.doControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.doControl1.Location = new System.Drawing.Point(0, 0);
            this.doControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.doControl1.Name = "doControl1";
            this.doControl1.Size = new System.Drawing.Size(492, 614);
            this.doControl1.TabIndex = 2;
            // 
            // tabPageCylinder
            // 
            this.tabPageCylinder.Controls.Add(this.cyliderControl1);
            this.tabPageCylinder.Location = new System.Drawing.Point(4, 39);
            this.tabPageCylinder.Name = "tabPageCylinder";
            this.tabPageCylinder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCylinder.Size = new System.Drawing.Size(1000, 618);
            this.tabPageCylinder.TabIndex = 18;
            this.tabPageCylinder.Text = "气缸";
            this.tabPageCylinder.UseVisualStyleBackColor = true;
            // 
            // cyliderControl1
            // 
            this.cyliderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cyliderControl1.Location = new System.Drawing.Point(3, 3);
            this.cyliderControl1.Margin = new System.Windows.Forms.Padding(4);
            this.cyliderControl1.Name = "cyliderControl1";
            this.cyliderControl1.Size = new System.Drawing.Size(994, 612);
            this.cyliderControl1.TabIndex = 0;
            // 
            // tPageVio
            // 
            this.tPageVio.Controls.Add(this.vioControl1);
            this.tPageVio.Location = new System.Drawing.Point(4, 39);
            this.tPageVio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tPageVio.Name = "tPageVio";
            this.tPageVio.Size = new System.Drawing.Size(1000, 618);
            this.tPageVio.TabIndex = 15;
            this.tPageVio.Text = "交互信号";
            this.tPageVio.UseVisualStyleBackColor = true;
            // 
            // vioControl1
            // 
            this.vioControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vioControl1.Location = new System.Drawing.Point(0, 0);
            this.vioControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.vioControl1.Name = "vioControl1";
            this.vioControl1.Size = new System.Drawing.Size(1000, 618);
            this.vioControl1.TabIndex = 1;
            // 
            // tabPagePlatforms
            // 
            this.tabPagePlatforms.Controls.Add(this.tabControlPlatform);
            this.tabPagePlatforms.Location = new System.Drawing.Point(4, 39);
            this.tabPagePlatforms.Name = "tabPagePlatforms";
            this.tabPagePlatforms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlatforms.Size = new System.Drawing.Size(1000, 618);
            this.tabPagePlatforms.TabIndex = 17;
            this.tabPagePlatforms.Text = "平台示教";
            this.tabPagePlatforms.UseVisualStyleBackColor = true;
            // 
            // tabControlPlatform
            // 
            this.tabControlPlatform.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlPlatform.Controls.Add(this.tabPage1);
            this.tabControlPlatform.Controls.Add(this.tabPage5);
            this.tabControlPlatform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPlatform.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControlPlatform.ItemSize = new System.Drawing.Size(96, 40);
            this.tabControlPlatform.Location = new System.Drawing.Point(3, 3);
            this.tabControlPlatform.Name = "tabControlPlatform";
            this.tabControlPlatform.SelectedIndex = 0;
            this.tabControlPlatform.Size = new System.Drawing.Size(994, 612);
            this.tabControlPlatform.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.platformXyzControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(986, 564);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "P1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // platformXyzControl1
            // 
            this.platformXyzControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.platformXyzControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.platformXyzControl1.Location = new System.Drawing.Point(3, 3);
            this.platformXyzControl1.Margin = new System.Windows.Forms.Padding(4);
            this.platformXyzControl1.Name = "platformXyzControl1";
            this.platformXyzControl1.Size = new System.Drawing.Size(980, 558);
            this.platformXyzControl1.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.platformXyzControl2);
            this.tabPage5.Location = new System.Drawing.Point(4, 44);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(986, 564);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "P2";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // platformXyzControl2
            // 
            this.platformXyzControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.platformXyzControl2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.platformXyzControl2.Location = new System.Drawing.Point(3, 3);
            this.platformXyzControl2.Margin = new System.Windows.Forms.Padding(4);
            this.platformXyzControl2.Name = "platformXyzControl2";
            this.platformXyzControl2.Size = new System.Drawing.Size(980, 558);
            this.platformXyzControl2.TabIndex = 1;
            // 
            // tPageCalib
            // 
            this.tPageCalib.Controls.Add(this.richTextBoxCalib);
            this.tPageCalib.Controls.Add(this.label5);
            this.tPageCalib.Controls.Add(this.label6);
            this.tPageCalib.Controls.Add(this.comboBoxPlatformCalib);
            this.tPageCalib.Controls.Add(this.buttonPlatformAlignCalc);
            this.tPageCalib.Controls.Add(this.buttonPlatformAlignTest);
            this.tPageCalib.Controls.Add(this.buttonXYPlatformCalib);
            this.tPageCalib.Controls.Add(this.buttonHeightCalibCalc);
            this.tPageCalib.Controls.Add(this.buttonHeightCalib);
            this.tPageCalib.Controls.Add(this.stationStateControlRight);
            this.tPageCalib.Controls.Add(this.stationStateControlLeft);
            this.tPageCalib.Location = new System.Drawing.Point(4, 39);
            this.tPageCalib.Margin = new System.Windows.Forms.Padding(2);
            this.tPageCalib.Name = "tPageCalib";
            this.tPageCalib.Size = new System.Drawing.Size(1000, 618);
            this.tPageCalib.TabIndex = 14;
            this.tPageCalib.Text = "平台标定";
            this.tPageCalib.UseVisualStyleBackColor = true;
            // 
            // richTextBoxCalib
            // 
            this.richTextBoxCalib.Location = new System.Drawing.Point(273, 39);
            this.richTextBoxCalib.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBoxCalib.Name = "richTextBoxCalib";
            this.richTextBoxCalib.Size = new System.Drawing.Size(466, 531);
            this.richTextBoxCalib.TabIndex = 4;
            this.richTextBoxCalib.Text = "";
            this.richTextBoxCalib.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(287, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "标定数据";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(784, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "标定平台";
            // 
            // comboBoxPlatformCalib
            // 
            this.comboBoxPlatformCalib.FormattingEnabled = true;
            this.comboBoxPlatformCalib.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.comboBoxPlatformCalib.Location = new System.Drawing.Point(859, 39);
            this.comboBoxPlatformCalib.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxPlatformCalib.Name = "comboBoxPlatformCalib";
            this.comboBoxPlatformCalib.Size = new System.Drawing.Size(122, 25);
            this.comboBoxPlatformCalib.TabIndex = 1;
            // 
            // buttonPlatformAlignCalc
            // 
            this.buttonPlatformAlignCalc.Location = new System.Drawing.Point(746, 212);
            this.buttonPlatformAlignCalc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonPlatformAlignCalc.Name = "buttonPlatformAlignCalc";
            this.buttonPlatformAlignCalc.Size = new System.Drawing.Size(235, 37);
            this.buttonPlatformAlignCalc.TabIndex = 0;
            this.buttonPlatformAlignCalc.Text = "上下平台 坐标系标定计算";
            this.buttonPlatformAlignCalc.UseVisualStyleBackColor = true;
            this.buttonPlatformAlignCalc.Click += new System.EventHandler(this.buttonPlatformAlignCalc_Click);
            // 
            // buttonPlatformAlignTest
            // 
            this.buttonPlatformAlignTest.Enabled = false;
            this.buttonPlatformAlignTest.Location = new System.Drawing.Point(746, 167);
            this.buttonPlatformAlignTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonPlatformAlignTest.Name = "buttonPlatformAlignTest";
            this.buttonPlatformAlignTest.Size = new System.Drawing.Size(235, 37);
            this.buttonPlatformAlignTest.TabIndex = 0;
            this.buttonPlatformAlignTest.Text = "上下平台 坐标系标定测试";
            this.buttonPlatformAlignTest.UseVisualStyleBackColor = true;
            this.buttonPlatformAlignTest.Click += new System.EventHandler(this.buttonPlatformAlignCalib_Click);
            // 
            // buttonXYPlatformCalib
            // 
            this.buttonXYPlatformCalib.Location = new System.Drawing.Point(745, 94);
            this.buttonXYPlatformCalib.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonXYPlatformCalib.Name = "buttonXYPlatformCalib";
            this.buttonXYPlatformCalib.Size = new System.Drawing.Size(236, 37);
            this.buttonXYPlatformCalib.TabIndex = 0;
            this.buttonXYPlatformCalib.Text = "载具坐标系与上XY平台 标定计算";
            this.buttonXYPlatformCalib.UseVisualStyleBackColor = true;
            this.buttonXYPlatformCalib.Click += new System.EventHandler(this.buttonProductToUpCalib_Click);
            // 
            // buttonHeightCalibCalc
            // 
            this.buttonHeightCalibCalc.Location = new System.Drawing.Point(747, 360);
            this.buttonHeightCalibCalc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonHeightCalibCalc.Name = "buttonHeightCalibCalc";
            this.buttonHeightCalibCalc.Size = new System.Drawing.Size(235, 38);
            this.buttonHeightCalibCalc.TabIndex = 0;
            this.buttonHeightCalibCalc.Text = "上下平台 GT高度标定计算";
            this.buttonHeightCalibCalc.UseVisualStyleBackColor = true;
            this.buttonHeightCalibCalc.Click += new System.EventHandler(this.buttonHeightCalibCalc_Click);
            // 
            // buttonHeightCalib
            // 
            this.buttonHeightCalib.Location = new System.Drawing.Point(747, 314);
            this.buttonHeightCalib.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonHeightCalib.Name = "buttonHeightCalib";
            this.buttonHeightCalib.Size = new System.Drawing.Size(235, 38);
            this.buttonHeightCalib.TabIndex = 0;
            this.buttonHeightCalib.Text = "上下平台 GT高度自动标定";
            this.buttonHeightCalib.UseVisualStyleBackColor = true;
            this.buttonHeightCalib.Click += new System.EventHandler(this.buttonHeightCalib_Click);
            // 
            // stationStateControlRight
            // 
            this.stationStateControlRight.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stationStateControlRight.Location = new System.Drawing.Point(13, 325);
            this.stationStateControlRight.Margin = new System.Windows.Forms.Padding(8, 11, 8, 11);
            this.stationStateControlRight.Name = "stationStateControlRight";
            this.stationStateControlRight.Size = new System.Drawing.Size(249, 245);
            this.stationStateControlRight.TabIndex = 2;
            // 
            // stationStateControlLeft
            // 
            this.stationStateControlLeft.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stationStateControlLeft.Location = new System.Drawing.Point(13, 39);
            this.stationStateControlLeft.Margin = new System.Windows.Forms.Padding(8, 11, 8, 11);
            this.stationStateControlLeft.Name = "stationStateControlLeft";
            this.stationStateControlLeft.Size = new System.Drawing.Size(249, 249);
            this.stationStateControlLeft.TabIndex = 1;
            // 
            // tPageSettings
            // 
            this.tPageSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tPageSettings.Controls.Add(this.tabControlConfig);
            this.tPageSettings.Location = new System.Drawing.Point(4, 39);
            this.tPageSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tPageSettings.Name = "tPageSettings";
            this.tPageSettings.Padding = new System.Windows.Forms.Padding(2);
            this.tPageSettings.Size = new System.Drawing.Size(1000, 618);
            this.tPageSettings.TabIndex = 9;
            this.tPageSettings.Text = "参数设置";
            // 
            // tabControlConfig
            // 
            this.tabControlConfig.Controls.Add(this.tabPageCOMMON);
            this.tabControlConfig.Controls.Add(this.tabPageSETTINGS);
            this.tabControlConfig.Controls.Add(this.tabPageMachine);
            this.tabControlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlConfig.Location = new System.Drawing.Point(2, 2);
            this.tabControlConfig.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlConfig.Name = "tabControlConfig";
            this.tabControlConfig.SelectedIndex = 0;
            this.tabControlConfig.Size = new System.Drawing.Size(996, 614);
            this.tabControlConfig.TabIndex = 0;
            // 
            // tabPageCOMMON
            // 
            this.tabPageCOMMON.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCOMMON.Controls.Add(this.propertyGridCommonConfig);
            this.tabPageCOMMON.Location = new System.Drawing.Point(4, 26);
            this.tabPageCOMMON.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageCOMMON.Name = "tabPageCOMMON";
            this.tabPageCOMMON.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageCOMMON.Size = new System.Drawing.Size(988, 584);
            this.tabPageCOMMON.TabIndex = 2;
            this.tabPageCOMMON.Text = "功能设定";
            // 
            // propertyGridCommonConfig
            // 
            this.propertyGridCommonConfig.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGridCommonConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridCommonConfig.Location = new System.Drawing.Point(2, 2);
            this.propertyGridCommonConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGridCommonConfig.Name = "propertyGridCommonConfig";
            this.propertyGridCommonConfig.Size = new System.Drawing.Size(984, 580);
            this.propertyGridCommonConfig.TabIndex = 62;
            // 
            // tabPageSETTINGS
            // 
            this.tabPageSETTINGS.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSETTINGS.Controls.Add(this.propertyGridMachineConfig);
            this.tabPageSETTINGS.Location = new System.Drawing.Point(4, 26);
            this.tabPageSETTINGS.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageSETTINGS.Name = "tabPageSETTINGS";
            this.tabPageSETTINGS.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageSETTINGS.Size = new System.Drawing.Size(988, 584);
            this.tabPageSETTINGS.TabIndex = 0;
            this.tabPageSETTINGS.Text = "所有参数";
            // 
            // propertyGridMachineConfig
            // 
            this.propertyGridMachineConfig.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGridMachineConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridMachineConfig.Location = new System.Drawing.Point(2, 2);
            this.propertyGridMachineConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGridMachineConfig.Name = "propertyGridMachineConfig";
            this.propertyGridMachineConfig.Size = new System.Drawing.Size(984, 580);
            this.propertyGridMachineConfig.TabIndex = 59;
            // 
            // tabPageMachine
            // 
            this.tabPageMachine.Controls.Add(this.richTextBoxMachine);
            this.tabPageMachine.Location = new System.Drawing.Point(4, 26);
            this.tabPageMachine.Name = "tabPageMachine";
            this.tabPageMachine.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMachine.Size = new System.Drawing.Size(988, 584);
            this.tabPageMachine.TabIndex = 3;
            this.tabPageMachine.Text = "设备信息";
            this.tabPageMachine.UseVisualStyleBackColor = true;
            // 
            // richTextBoxMachine
            // 
            this.richTextBoxMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMachine.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxMachine.Name = "richTextBoxMachine";
            this.richTextBoxMachine.ReadOnly = true;
            this.richTextBoxMachine.Size = new System.Drawing.Size(982, 578);
            this.richTextBoxMachine.TabIndex = 0;
            this.richTextBoxMachine.Text = "";
            this.richTextBoxMachine.WordWrap = false;
            this.richTextBoxMachine.DoubleClick += new System.EventHandler(this.richTextBoxMachine_DoubleClick);
            // 
            // tabPageProduct
            // 
            this.tabPageProduct.Controls.Add(this.measureProjectSelctionControl2);
            this.tabPageProduct.Controls.Add(this.measureProjectSelctionControl1);
            this.tabPageProduct.Location = new System.Drawing.Point(4, 39);
            this.tabPageProduct.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageProduct.Name = "tabPageProduct";
            this.tabPageProduct.Size = new System.Drawing.Size(1000, 618);
            this.tabPageProduct.TabIndex = 13;
            this.tabPageProduct.Text = "测量产品设置";
            this.tabPageProduct.UseVisualStyleBackColor = true;
            // 
            // measureProjectSelctionControl2
            // 
            this.measureProjectSelctionControl2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.measureProjectSelctionControl2.Location = new System.Drawing.Point(502, 4);
            this.measureProjectSelctionControl2.Margin = new System.Windows.Forms.Padding(4);
            this.measureProjectSelctionControl2.Name = "measureProjectSelctionControl2";
            this.measureProjectSelctionControl2.Size = new System.Drawing.Size(489, 605);
            this.measureProjectSelctionControl2.TabIndex = 6;
            // 
            // measureProjectSelctionControl1
            // 
            this.measureProjectSelctionControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.measureProjectSelctionControl1.Location = new System.Drawing.Point(4, 4);
            this.measureProjectSelctionControl1.Margin = new System.Windows.Forms.Padding(4);
            this.measureProjectSelctionControl1.Name = "measureProjectSelctionControl1";
            this.measureProjectSelctionControl1.Size = new System.Drawing.Size(489, 605);
            this.measureProjectSelctionControl1.TabIndex = 6;
            // 
            // tabPageTest
            // 
            this.tabPageTest.Controls.Add(this.buttonProductCalculatorEditor);
            this.tabPageTest.Controls.Add(this.buttonRBarcodeTrigger);
            this.tabPageTest.Controls.Add(this.buttonRBarcodeClose);
            this.tabPageTest.Controls.Add(this.buttonRBacodeOpen);
            this.tabPageTest.Controls.Add(this.buttonRGT);
            this.tabPageTest.Controls.Add(this.buttonLBarcodeTrigger);
            this.tabPageTest.Controls.Add(this.buttonLBarcodeClose);
            this.tabPageTest.Controls.Add(this.buttonLBarcodeOpen);
            this.tabPageTest.Controls.Add(this.buttonLGTTest);
            this.tabPageTest.Location = new System.Drawing.Point(4, 39);
            this.tabPageTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageTest.Name = "tabPageTest";
            this.tabPageTest.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageTest.Size = new System.Drawing.Size(1000, 618);
            this.tabPageTest.TabIndex = 16;
            this.tabPageTest.Text = "测试";
            this.tabPageTest.UseVisualStyleBackColor = true;
            // 
            // buttonProductCalculatorEditor
            // 
            this.buttonProductCalculatorEditor.Location = new System.Drawing.Point(17, 95);
            this.buttonProductCalculatorEditor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonProductCalculatorEditor.Name = "buttonProductCalculatorEditor";
            this.buttonProductCalculatorEditor.Size = new System.Drawing.Size(139, 207);
            this.buttonProductCalculatorEditor.TabIndex = 3;
            this.buttonProductCalculatorEditor.Text = "产品计算文件编辑";
            this.buttonProductCalculatorEditor.UseVisualStyleBackColor = true;
            this.buttonProductCalculatorEditor.Click += new System.EventHandler(this.buttonProductCalculatorEditor_Click);
            // 
            // buttonRBarcodeTrigger
            // 
            this.buttonRBarcodeTrigger.Location = new System.Drawing.Point(708, 218);
            this.buttonRBarcodeTrigger.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRBarcodeTrigger.Name = "buttonRBarcodeTrigger";
            this.buttonRBarcodeTrigger.Size = new System.Drawing.Size(170, 84);
            this.buttonRBarcodeTrigger.TabIndex = 0;
            this.buttonRBarcodeTrigger.Text = "R BARCODE TRIGGER";
            this.buttonRBarcodeTrigger.UseVisualStyleBackColor = true;
            this.buttonRBarcodeTrigger.Click += new System.EventHandler(this.buttonRBarcodeTrigger_Click);
            // 
            // buttonRBarcodeClose
            // 
            this.buttonRBarcodeClose.Location = new System.Drawing.Point(532, 218);
            this.buttonRBarcodeClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRBarcodeClose.Name = "buttonRBarcodeClose";
            this.buttonRBarcodeClose.Size = new System.Drawing.Size(170, 84);
            this.buttonRBarcodeClose.TabIndex = 0;
            this.buttonRBarcodeClose.Text = "R BARCODE CLOSE";
            this.buttonRBarcodeClose.UseVisualStyleBackColor = true;
            this.buttonRBarcodeClose.Click += new System.EventHandler(this.buttonRBarcodeClose_Click);
            // 
            // buttonRBacodeOpen
            // 
            this.buttonRBacodeOpen.Location = new System.Drawing.Point(356, 218);
            this.buttonRBacodeOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRBacodeOpen.Name = "buttonRBacodeOpen";
            this.buttonRBacodeOpen.Size = new System.Drawing.Size(170, 84);
            this.buttonRBacodeOpen.TabIndex = 0;
            this.buttonRBacodeOpen.Text = "R BARCODE OPEN";
            this.buttonRBacodeOpen.UseVisualStyleBackColor = true;
            this.buttonRBacodeOpen.Click += new System.EventHandler(this.buttonRBacodeOpen_Click);
            // 
            // buttonRGT
            // 
            this.buttonRGT.Location = new System.Drawing.Point(180, 218);
            this.buttonRGT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRGT.Name = "buttonRGT";
            this.buttonRGT.Size = new System.Drawing.Size(170, 84);
            this.buttonRGT.TabIndex = 0;
            this.buttonRGT.Text = "RGT";
            this.buttonRGT.UseVisualStyleBackColor = true;
            this.buttonRGT.Click += new System.EventHandler(this.buttonRGT_Click);
            // 
            // buttonLBarcodeTrigger
            // 
            this.buttonLBarcodeTrigger.Location = new System.Drawing.Point(708, 95);
            this.buttonLBarcodeTrigger.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonLBarcodeTrigger.Name = "buttonLBarcodeTrigger";
            this.buttonLBarcodeTrigger.Size = new System.Drawing.Size(170, 84);
            this.buttonLBarcodeTrigger.TabIndex = 0;
            this.buttonLBarcodeTrigger.Text = "L BARCODE TRIGGER";
            this.buttonLBarcodeTrigger.UseVisualStyleBackColor = true;
            this.buttonLBarcodeTrigger.Click += new System.EventHandler(this.buttonLBarcodeTrigger_Click);
            // 
            // buttonLBarcodeClose
            // 
            this.buttonLBarcodeClose.Location = new System.Drawing.Point(532, 95);
            this.buttonLBarcodeClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonLBarcodeClose.Name = "buttonLBarcodeClose";
            this.buttonLBarcodeClose.Size = new System.Drawing.Size(170, 84);
            this.buttonLBarcodeClose.TabIndex = 0;
            this.buttonLBarcodeClose.Text = "L BARCODE CLOSE";
            this.buttonLBarcodeClose.UseVisualStyleBackColor = true;
            this.buttonLBarcodeClose.Click += new System.EventHandler(this.buttonLBarcodeClose_Click);
            // 
            // buttonLBarcodeOpen
            // 
            this.buttonLBarcodeOpen.Location = new System.Drawing.Point(356, 95);
            this.buttonLBarcodeOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonLBarcodeOpen.Name = "buttonLBarcodeOpen";
            this.buttonLBarcodeOpen.Size = new System.Drawing.Size(170, 84);
            this.buttonLBarcodeOpen.TabIndex = 0;
            this.buttonLBarcodeOpen.Text = "L BARCODE OPEN";
            this.buttonLBarcodeOpen.UseVisualStyleBackColor = true;
            this.buttonLBarcodeOpen.Click += new System.EventHandler(this.buttonLBarcodeOpen_Click);
            // 
            // buttonLGTTest
            // 
            this.buttonLGTTest.Location = new System.Drawing.Point(180, 95);
            this.buttonLGTTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonLGTTest.Name = "buttonLGTTest";
            this.buttonLGTTest.Size = new System.Drawing.Size(170, 84);
            this.buttonLGTTest.TabIndex = 0;
            this.buttonLGTTest.Text = "LGT";
            this.buttonLGTTest.UseVisualStyleBackColor = true;
            this.buttonLGTTest.Click += new System.EventHandler(this.buttonLGTTest_Click);
            // 
            // DevConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.tabControlMain);
            this.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DevConfigForm";
            this.TabText = "配置";
            this.Text = "配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.DevConfigForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tPageIO.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageCylinder.ResumeLayout(false);
            this.tPageVio.ResumeLayout(false);
            this.tabPagePlatforms.ResumeLayout(false);
            this.tabControlPlatform.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tPageCalib.ResumeLayout(false);
            this.tPageCalib.PerformLayout();
            this.tPageSettings.ResumeLayout(false);
            this.tabControlConfig.ResumeLayout(false);
            this.tabPageCOMMON.ResumeLayout(false);
            this.tabPageSETTINGS.ResumeLayout(false);
            this.tabPageMachine.ResumeLayout(false);
            this.tabPageProduct.ResumeLayout(false);
            this.tabPageTest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tPageSettings;
        private System.Windows.Forms.TabControl tabControlConfig;
        private System.Windows.Forms.TabPage tabPageSETTINGS;
        private System.Windows.Forms.TabPage tabPageCOMMON;
        private System.Windows.Forms.TabPage tPageIO;
        private System.Windows.Forms.TabPage tabPageProduct;
        private System.Windows.Forms.TabPage tPageCalib;
        private System.Windows.Forms.PropertyGrid propertyGridMachineConfig;
        private System.Windows.Forms.PropertyGrid propertyGridCommonConfig;
        private System.Windows.Forms.TabPage tPageVio;
        private FrameworkExtension.stateMachine.StationStateControl stationStateControlLeft;
        private FrameworkExtension.stateMachine.StationStateControl stationStateControlRight;
        private System.Windows.Forms.Button buttonHeightCalib;
        private System.Windows.Forms.Button buttonPlatformAlignTest;
        private System.Windows.Forms.RichTextBox richTextBoxCalib;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPlatformCalib;
        private System.Windows.Forms.Button buttonPlatformAlignCalc;
        private System.Windows.Forms.Button buttonHeightCalibCalc;
        private System.Windows.Forms.TabPage tabPageTest;
        private System.Windows.Forms.Button buttonLGTTest;
        private System.Windows.Forms.Button buttonRGT;
        private System.Windows.Forms.Button buttonXYPlatformCalib;
        private System.Windows.Forms.Button buttonRBacodeOpen;
        private System.Windows.Forms.Button buttonLBarcodeOpen;
        private System.Windows.Forms.Button buttonRBarcodeTrigger;
        private System.Windows.Forms.Button buttonRBarcodeClose;
        private System.Windows.Forms.Button buttonLBarcodeTrigger;
        private System.Windows.Forms.Button buttonLBarcodeClose;
        private System.Windows.Forms.TabPage tabPagePlatforms;
        private System.Windows.Forms.TabControl tabControlPlatform;
        private System.Windows.Forms.TabPage tabPage1;
        private PlatformControl platformXyzControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private PlatformControl platformXyzControl2;
        private System.Windows.Forms.TabPage tabPageCylinder;
        private VioControl vioControl1;
        private CylinderControl cyliderControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DiControl diControl1;
        private DoControl doControl1;
        private System.Windows.Forms.TabPage tabPageMachine;
        private System.Windows.Forms.RichTextBox richTextBoxMachine;
        private ThermoAOIFlatnessCalcLib.Thermo.Project.MeasureProjectSelctionControl measureProjectSelctionControl1;
        private System.Windows.Forms.Button buttonProductCalculatorEditor;
        private ThermoAOIFlatnessCalcLib.Thermo.Project.MeasureProjectSelctionControl measureProjectSelctionControl2;
    }
}
