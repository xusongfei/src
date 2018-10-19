using System.Collections;

namespace Lead.Detect.PrimCameraDalsa.Dalsa
{
    partial class AcqConfigDlg 
    {

       

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

    

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcqConfigDlg));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBox_Device = new System.Windows.Forms.ComboBox();
            this.comboBox_Server = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_configfile = new System.Windows.Forms.CheckBox();
            this.button_browse = new System.Windows.Forms.Button();
            this.textBox_configfile = new System.Windows.Forms.TextBox();
            this.comboBox_configfile = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Device
            // 
            this.comboBox_Device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Device.FormattingEnabled = true;
            this.comboBox_Device.Location = new System.Drawing.Point(219, 42);
            this.comboBox_Device.Name = "comboBox_Device";
            this.comboBox_Device.Size = new System.Drawing.Size(205, 21);
            this.comboBox_Device.TabIndex = 3;
            this.comboBox_Device.SelectedIndexChanged += new System.EventHandler(this.comboBox_Device_SelectedIndexChanged);
            // 
            // comboBox_Server
            // 
            this.comboBox_Server.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Server.FormattingEnabled = true;
            this.comboBox_Server.Location = new System.Drawing.Point(9, 41);
            this.comboBox_Server.Name = "comboBox_Server";
            this.comboBox_Server.Size = new System.Drawing.Size(204, 21);
            this.comboBox_Server.TabIndex = 2;
            this.comboBox_Server.SelectedIndexChanged += new System.EventHandler(this.comboBox_Server_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Acquisition Device:";
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(458, 18);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(84, 29);
            this.button_ok.TabIndex = 4;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_Device);
            this.groupBox1.Controls.Add(this.comboBox_Server);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 90);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Acquisition Server:";
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(458, 54);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(84, 31);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_configfile);
            this.groupBox2.Controls.Add(this.button_browse);
            this.groupBox2.Controls.Add(this.textBox_configfile);
            this.groupBox2.Controls.Add(this.comboBox_configfile);
            this.groupBox2.Location = new System.Drawing.Point(11, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(531, 102);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // checkBox_configfile
            // 
            this.checkBox_configfile.AutoSize = true;
            this.checkBox_configfile.Location = new System.Drawing.Point(29, -1);
            this.checkBox_configfile.Name = "checkBox_configfile";
            this.checkBox_configfile.Size = new System.Drawing.Size(107, 17);
            this.checkBox_configfile.TabIndex = 3;
            this.checkBox_configfile.Text = "Configuration File";
            this.checkBox_configfile.UseVisualStyleBackColor = true;
            this.checkBox_configfile.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(447, 29);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(75, 23);
            this.button_browse.TabIndex = 2;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // textBox_configfile
            // 
            this.textBox_configfile.Location = new System.Drawing.Point(29, 64);
            this.textBox_configfile.Name = "textBox_configfile";
            this.textBox_configfile.Size = new System.Drawing.Size(401, 20);
            this.textBox_configfile.TabIndex = 1;
            // 
            // comboBox_configfile
            // 
            this.comboBox_configfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_configfile.FormattingEnabled = true;
            this.comboBox_configfile.Location = new System.Drawing.Point(29, 29);
            this.comboBox_configfile.Name = "comboBox_configfile";
            this.comboBox_configfile.Size = new System.Drawing.Size(395, 21);
            this.comboBox_configfile.TabIndex = 0;
            this.comboBox_configfile.SelectedIndexChanged += new System.EventHandler(this.comboBox_configfile_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "If no Configuration file exists for your board/camera, ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(329, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "you must run the CamExpert utility to generate your Configuration file.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(11, 218);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(531, 53);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // AcqConfigDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 280);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_cancel);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AcqConfigDlg";
            this.Text = "Acquisition Configuration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Device;
        private System.Windows.Forms.ComboBox comboBox_Server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_configfile;
        private System.Windows.Forms.CheckBox checkBox_configfile;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.TextBox textBox_configfile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private bool m_ConfigFileEnable;
        private bool m_configFileAvailable;
        private string m_currentConfigDir;
        private string m_currentConfigFileName = "";
        private int m_currentConfigFileIndex;
        private bool m_init = false;
        private ArrayList ccffiles = new ArrayList();
        public string m_ServerName = "";
        public string m_ConfigFile = "";
        public int m_ResourceIndex = 0;
        private string m_ProductDir = "";        // Initial directory for finding CAM and VIC files

    }
}

