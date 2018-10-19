using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.userControls;

namespace Lead.Detect.ThermoAOI2.MachineB.View
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.diControl1 = new Lead.Detect.FrameworkExtension.userControls.DiControl();
            this.doControl1 = new Lead.Detect.FrameworkExtension.userControls.DoControl();
            this.tabPageCylinder = new System.Windows.Forms.TabPage();
            this.cyliderControl1 = new Lead.Detect.FrameworkExtension.userControls.CylinderControl();
            this.tabPageVio = new System.Windows.Forms.TabPage();
            this.vioControl1 = new Lead.Detect.FrameworkExtension.userControls.VioControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.tabControlConfig = new System.Windows.Forms.TabControl();
            this.tabPageCOMMON = new System.Windows.Forms.TabPage();
            this.propertyGridCommonConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.propertyGridMachineConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPageMachine = new System.Windows.Forms.TabPage();
            this.richTextBoxMachine = new System.Windows.Forms.RichTextBox();
            this.tabPagePlatforms = new System.Windows.Forms.TabPage();
            this.tabControlPlatform = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.platformXyzControl1 = new Lead.Detect.FrameworkExtension.platforms.motionPlatforms.PlatformControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.platformXyzControl2 = new Lead.Detect.FrameworkExtension.platforms.motionPlatforms.PlatformControl();
            this.tabPageCalib = new System.Windows.Forms.TabPage();
            this.richTextBoxAlignData = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPlatformAlign = new System.Windows.Forms.ComboBox();
            this.buttonPlatformRelationCalib = new System.Windows.Forms.Button();
            this.stationControl = new Lead.Detect.FrameworkExtension.stateMachine.StationStateControl();
            this.tabPageProduct = new System.Windows.Forms.TabPage();
            this.buttonBrowseMeasureProj = new System.Windows.Forms.Button();
            this.buttonOpenMeasureProj = new System.Windows.Forms.Button();
            this.textBoxMeasureProj = new System.Windows.Forms.TextBox();
            this.buttonMeasureProjectEditor = new System.Windows.Forms.Button();
            this.tabPageTest = new System.Windows.Forms.TabPage();
            this.lineLaserExDebugControl2 = new Lead.Detect.MeasureComponents.LaserControl.LineLaserExDebugControl();
            this.lineLaserExDebugControl1 = new Lead.Detect.MeasureComponents.LaserControl.LineLaserExDebugControl();
            this.cameraExDebugControl1 = new Lead.Detect.MeasureComponents.CameraControl.CameraExDebugControl();
            this.tabControl1.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageCylinder.SuspendLayout();
            this.tabPageVio.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabControlConfig.SuspendLayout();
            this.tabPageCOMMON.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPageMachine.SuspendLayout();
            this.tabPagePlatforms.SuspendLayout();
            this.tabControlPlatform.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPageCalib.SuspendLayout();
            this.tabPageProduct.SuspendLayout();
            this.tabPageTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageIO);
            this.tabControl1.Controls.Add(this.tabPageCylinder);
            this.tabControl1.Controls.Add(this.tabPageVio);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPagePlatforms);
            this.tabControl1.Controls.Add(this.tabPageCalib);
            this.tabControl1.Controls.Add(this.tabPageProduct);
            this.tabControl1.Controls.Add(this.tabPageTest);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 35);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1008, 661);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPageIO
            // 
            this.tabPageIO.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageIO.Controls.Add(this.splitContainer1);
            this.tabPageIO.Location = new System.Drawing.Point(4, 39);
            this.tabPageIO.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageIO.Size = new System.Drawing.Size(1000, 618);
            this.tabPageIO.TabIndex = 10;
            this.tabPageIO.Text = "输入输出";
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
            // tabPageVio
            // 
            this.tabPageVio.Controls.Add(this.vioControl1);
            this.tabPageVio.Location = new System.Drawing.Point(4, 39);
            this.tabPageVio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageVio.Name = "tabPageVio";
            this.tabPageVio.Size = new System.Drawing.Size(1000, 618);
            this.tabPageVio.TabIndex = 15;
            this.tabPageVio.Text = "交互信号";
            this.tabPageVio.UseVisualStyleBackColor = true;
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
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSettings.Controls.Add(this.tabControlConfig);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 39);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageSettings.Size = new System.Drawing.Size(1000, 618);
            this.tabPageSettings.TabIndex = 9;
            this.tabPageSettings.Text = "参数设置";
            // 
            // tabControlConfig
            // 
            this.tabControlConfig.Controls.Add(this.tabPageCOMMON);
            this.tabControlConfig.Controls.Add(this.tabPage2);
            this.tabControlConfig.Controls.Add(this.tabPageMachine);
            this.tabControlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlConfig.Location = new System.Drawing.Point(2, 2);
            this.tabControlConfig.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlConfig.Name = "tabControlConfig";
            this.tabControlConfig.SelectedIndex = 0;
            this.tabControlConfig.Size = new System.Drawing.Size(996, 614);
            this.tabControlConfig.TabIndex = 1;
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
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.propertyGridMachineConfig);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(988, 584);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "所有参数";
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
            // tabPageCalib
            // 
            this.tabPageCalib.Controls.Add(this.richTextBoxAlignData);
            this.tabPageCalib.Controls.Add(this.label5);
            this.tabPageCalib.Controls.Add(this.label6);
            this.tabPageCalib.Controls.Add(this.comboBoxPlatformAlign);
            this.tabPageCalib.Controls.Add(this.buttonPlatformRelationCalib);
            this.tabPageCalib.Controls.Add(this.stationControl);
            this.tabPageCalib.Location = new System.Drawing.Point(4, 39);
            this.tabPageCalib.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageCalib.Name = "tabPageCalib";
            this.tabPageCalib.Size = new System.Drawing.Size(1000, 618);
            this.tabPageCalib.TabIndex = 14;
            this.tabPageCalib.Text = "平台标定";
            this.tabPageCalib.UseVisualStyleBackColor = true;
            // 
            // richTextBoxAlignData
            // 
            this.richTextBoxAlignData.Location = new System.Drawing.Point(273, 25);
            this.richTextBoxAlignData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBoxAlignData.Name = "richTextBoxAlignData";
            this.richTextBoxAlignData.Size = new System.Drawing.Size(395, 577);
            this.richTextBoxAlignData.TabIndex = 4;
            this.richTextBoxAlignData.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(271, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "标定数据";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(756, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "运动平台";
            // 
            // comboBoxPlatformAlign
            // 
            this.comboBoxPlatformAlign.FormattingEnabled = true;
            this.comboBoxPlatformAlign.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.comboBoxPlatformAlign.Location = new System.Drawing.Point(815, 21);
            this.comboBoxPlatformAlign.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxPlatformAlign.Name = "comboBoxPlatformAlign";
            this.comboBoxPlatformAlign.Size = new System.Drawing.Size(122, 25);
            this.comboBoxPlatformAlign.TabIndex = 1;
            // 
            // buttonPlatformRelationCalib
            // 
            this.buttonPlatformRelationCalib.Location = new System.Drawing.Point(691, 69);
            this.buttonPlatformRelationCalib.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonPlatformRelationCalib.Name = "buttonPlatformRelationCalib";
            this.buttonPlatformRelationCalib.Size = new System.Drawing.Size(289, 59);
            this.buttonPlatformRelationCalib.TabIndex = 0;
            this.buttonPlatformRelationCalib.Text = "上下坐标系标定";
            this.buttonPlatformRelationCalib.UseVisualStyleBackColor = true;
            // 
            // stationControl
            // 
            this.stationControl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stationControl.Location = new System.Drawing.Point(13, 25);
            this.stationControl.Margin = new System.Windows.Forms.Padding(8, 11, 8, 11);
            this.stationControl.Name = "stationControl";
            this.stationControl.Size = new System.Drawing.Size(249, 577);
            this.stationControl.TabIndex = 1;
            // 
            // tabPageProduct
            // 
            this.tabPageProduct.Controls.Add(this.buttonBrowseMeasureProj);
            this.tabPageProduct.Controls.Add(this.buttonOpenMeasureProj);
            this.tabPageProduct.Controls.Add(this.textBoxMeasureProj);
            this.tabPageProduct.Controls.Add(this.buttonMeasureProjectEditor);
            this.tabPageProduct.Location = new System.Drawing.Point(4, 39);
            this.tabPageProduct.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageProduct.Name = "tabPageProduct";
            this.tabPageProduct.Size = new System.Drawing.Size(1000, 618);
            this.tabPageProduct.TabIndex = 13;
            this.tabPageProduct.Text = "产品测试设置";
            this.tabPageProduct.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseMeasureProj
            // 
            this.buttonBrowseMeasureProj.Location = new System.Drawing.Point(18, 124);
            this.buttonBrowseMeasureProj.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBrowseMeasureProj.Name = "buttonBrowseMeasureProj";
            this.buttonBrowseMeasureProj.Size = new System.Drawing.Size(116, 60);
            this.buttonBrowseMeasureProj.TabIndex = 5;
            this.buttonBrowseMeasureProj.Text = "测试文件 更换";
            this.buttonBrowseMeasureProj.UseVisualStyleBackColor = true;
            this.buttonBrowseMeasureProj.Click += new System.EventHandler(this.buttonBrowseMeasureProj_Click);
            // 
            // buttonOpenMeasureProj
            // 
            this.buttonOpenMeasureProj.Location = new System.Drawing.Point(812, 124);
            this.buttonOpenMeasureProj.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOpenMeasureProj.Name = "buttonOpenMeasureProj";
            this.buttonOpenMeasureProj.Size = new System.Drawing.Size(162, 60);
            this.buttonOpenMeasureProj.TabIndex = 4;
            this.buttonOpenMeasureProj.Text = "产品测试文件编辑";
            this.buttonOpenMeasureProj.UseVisualStyleBackColor = true;
            this.buttonOpenMeasureProj.Click += new System.EventHandler(this.buttonOpenMeasureProj_Click);
            // 
            // textBoxMeasureProj
            // 
            this.textBoxMeasureProj.Location = new System.Drawing.Point(140, 124);
            this.textBoxMeasureProj.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxMeasureProj.Multiline = true;
            this.textBoxMeasureProj.Name = "textBoxMeasureProj";
            this.textBoxMeasureProj.Size = new System.Drawing.Size(666, 60);
            this.textBoxMeasureProj.TabIndex = 3;
            // 
            // buttonMeasureProjectEditor
            // 
            this.buttonMeasureProjectEditor.Location = new System.Drawing.Point(711, 20);
            this.buttonMeasureProjectEditor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonMeasureProjectEditor.Name = "buttonMeasureProjectEditor";
            this.buttonMeasureProjectEditor.Size = new System.Drawing.Size(248, 55);
            this.buttonMeasureProjectEditor.TabIndex = 2;
            this.buttonMeasureProjectEditor.Text = "产品测试文件编辑";
            this.buttonMeasureProjectEditor.UseVisualStyleBackColor = true;
            this.buttonMeasureProjectEditor.Click += new System.EventHandler(this.buttonMeasureProjectEditor_Click);
            // 
            // tabPageTest
            // 
            this.tabPageTest.Controls.Add(this.cameraExDebugControl1);
            this.tabPageTest.Controls.Add(this.lineLaserExDebugControl2);
            this.tabPageTest.Controls.Add(this.lineLaserExDebugControl1);
            this.tabPageTest.Location = new System.Drawing.Point(4, 39);
            this.tabPageTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageTest.Name = "tabPageTest";
            this.tabPageTest.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageTest.Size = new System.Drawing.Size(1000, 618);
            this.tabPageTest.TabIndex = 16;
            this.tabPageTest.Text = "测试";
            this.tabPageTest.UseVisualStyleBackColor = true;
            // 
            // lineLaserExDebugControl2
            // 
            this.lineLaserExDebugControl2.Location = new System.Drawing.Point(509, 10);
            this.lineLaserExDebugControl2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.lineLaserExDebugControl2.Name = "lineLaserExDebugControl2";
            this.lineLaserExDebugControl2.Size = new System.Drawing.Size(483, 283);
            this.lineLaserExDebugControl2.TabIndex = 0;
            // 
            // lineLaserExDebugControl1
            // 
            this.lineLaserExDebugControl1.Location = new System.Drawing.Point(9, 8);
            this.lineLaserExDebugControl1.Margin = new System.Windows.Forms.Padding(4);
            this.lineLaserExDebugControl1.Name = "lineLaserExDebugControl1";
            this.lineLaserExDebugControl1.Size = new System.Drawing.Size(491, 285);
            this.lineLaserExDebugControl1.TabIndex = 0;
            // 
            // cameraExDebugControl1
            // 
            this.cameraExDebugControl1.Location = new System.Drawing.Point(9, 301);
            this.cameraExDebugControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cameraExDebugControl1.Name = "cameraExDebugControl1";
            this.cameraExDebugControl1.Size = new System.Drawing.Size(522, 309);
            this.cameraExDebugControl1.TabIndex = 1;
            // 
            // DevConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DevConfigForm";
            this.TabText = "配置";
            this.Text = "配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.DevConfigForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageCylinder.ResumeLayout(false);
            this.tabPageVio.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabControlConfig.ResumeLayout(false);
            this.tabPageCOMMON.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPageMachine.ResumeLayout(false);
            this.tabPagePlatforms.ResumeLayout(false);
            this.tabControlPlatform.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPageCalib.ResumeLayout(false);
            this.tabPageCalib.PerformLayout();
            this.tabPageProduct.ResumeLayout(false);
            this.tabPageProduct.PerformLayout();
            this.tabPageTest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.TabPage tabPageIO;
        private System.Windows.Forms.TabPage tabPageProduct;
        private System.Windows.Forms.TabPage tabPageCalib;
        private System.Windows.Forms.Button buttonMeasureProjectEditor;
        private System.Windows.Forms.TabPage tabPageVio;
        private FrameworkExtension.stateMachine.StationStateControl stationControl;
        private System.Windows.Forms.Button buttonPlatformRelationCalib;
        private System.Windows.Forms.RichTextBox richTextBoxAlignData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonOpenMeasureProj;
        private System.Windows.Forms.TextBox textBoxMeasureProj;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPlatformAlign;
        private System.Windows.Forms.Button buttonBrowseMeasureProj;
        private System.Windows.Forms.TabPage tabPageTest;
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
        private System.Windows.Forms.TabControl tabControlConfig;
        private System.Windows.Forms.TabPage tabPageCOMMON;
        private System.Windows.Forms.PropertyGrid propertyGridCommonConfig;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid propertyGridMachineConfig;
        private System.Windows.Forms.TabPage tabPageMachine;
        private System.Windows.Forms.RichTextBox richTextBoxMachine;
        private MeasureComponents.LaserControl.LineLaserExDebugControl lineLaserExDebugControl1;
        private MeasureComponents.LaserControl.LineLaserExDebugControl lineLaserExDebugControl2;
        private MeasureComponents.CameraControl.CameraExDebugControl cameraExDebugControl1;
    }
}
