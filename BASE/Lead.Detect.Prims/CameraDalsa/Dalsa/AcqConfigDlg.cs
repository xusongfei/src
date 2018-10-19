using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DALSA.SaperaLT.SapClassBasic;
using Microsoft.Win32;

namespace Lead.Detect.PrimCameraDalsa.Dalsa
{
    public partial class AcqConfigDlg : Form
    {
        /// <summary>
        ///     Enum of server category
        /// </summary>
        public enum ServerCategory
        {
            ServerAll,
            ServerAcq,
            ServerAcqDevice
        }

        private static readonly string ConfigKeyName = "Camera Name";
        private static readonly string CompanyKeyName = "Company Name";
        private static readonly string ModelKeyName = "Model Name";
        private static readonly string VicName = "Vic Name";
        private static readonly string Acquisition_Default_folder = "\\CamFiles\\User";

        public AcqConfigDlg()
        {
            InitializeComponent();
        }

        public AcqConfigDlg(ServerCategory serverCategory)
        {
            ServCategory = serverCategory;
            // Load parameters from registry
            LoadSettings();
            InitializeComponent();
        }


        public AcqConfigDlg(SapLocation loc, string productDir, ServerCategory serverCategory)
        {
            m_ProductDir = productDir;
            ServCategory = serverCategory;

            if (loc != null)
            {
                m_ServerName = loc.ServerName;
                m_ResourceIndex = loc.ServerIndex;
            }

            // Load parameters from registry
            LoadSettings();
            InitializeComponent();
        }

        /// <summary>
        ///     Property of the form
        /// </summary>
        public string ConfigFile
        {
            get
            {
                if (m_ConfigFileEnable)
                    return m_ConfigFile;
                return "";
            }
        }

        public ServerCategory ServCategory { get; private set; } = ServerCategory.ServerAll;

        public SapLocation ServerLocation => new SapLocation(m_ServerName, m_ResourceIndex);

        private void button_browse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Select a directory.";
            folderBrowserDialog1.SelectedPath = m_currentConfigDir;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                    UpdateCurrentDir(folderBrowserDialog1.SelectedPath);
                var countItems = comboBox_configfile.Items.Count;
                comboBox_configfile.Items.Clear();
                UpdateNames();
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (checkBox_configfile.Checked)
                m_ConfigFile = m_currentConfigDir + "\\" + m_currentConfigFileName;
            else
                m_ConfigFile = "";

            if (ServCategory == ServerCategory.ServerAll)
            {
                if (
                    SapManager.GetResourceCount(new SapLocation(m_ServerName, m_ResourceIndex),
                        SapManager.ResourceType.Acq) > 0)
                    ServCategory = ServerCategory.ServerAcq;
                else if (
                    SapManager.GetResourceCount(new SapLocation(m_ServerName, m_ResourceIndex),
                        SapManager.ResourceType.AcqDevice) > 0)
                    ServCategory = ServerCategory.ServerAcqDevice;
            }

            SaveSettings();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_ConfigFileEnable = checkBox_configfile.Checked;
            UpdateNames();
        }


        private void comboBox_configfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_currentConfigFileIndex = comboBox_configfile.SelectedIndex;
            m_currentConfigFileName = (string) ccffiles[m_currentConfigFileIndex];
        }

        private void comboBox_Device_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_init)
                UpdateNames();
        }

        private void comboBox_Server_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_init)
            {
                InitResourceCombo();
                UpdateNames();
                UpdateBoxAvailability();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Servers Combo
            if (!InitServerCombo())
            {
                Close();
            }
            else
            {
                // Initialize directories
                SetDirectories();
                // Scan all files in the current directory and fill the list box
                UpdateNames();
            }

            m_init = true;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

        private void InitResourceCombo()
        {
            comboBox_Device.Items.Clear();
            var i = 0;
            var selectedItem = comboBox_Server.SelectedItem;
            // Add "Acq" resources (cameras) to combo
            for (i = 0; i < SapManager.GetResourceCount(selectedItem.ToString(), SapManager.ResourceType.Acq); i++)
            {
                var resourceName = SapManager.GetResourceName(selectedItem.ToString(), SapManager.ResourceType.Acq, i);
                if (SapManager.IsResourceAvailable(selectedItem.ToString(), SapManager.ResourceType.Acq, i))
                {
                    comboBox_Device.Items.Add(resourceName);
                    if (i == m_ResourceIndex)
                        comboBox_Device.SelectedItem = resourceName;
                }
                else
                {
                    comboBox_Device.Items.Add("Not Available - Resource in Use");
                    comboBox_Device.SelectedIndex = 0;
                }
            }

            // Add "AcqDevice" resources (cameras) to combo
            for (i = 0;
                i < SapManager.GetResourceCount(selectedItem.ToString(), SapManager.ResourceType.AcqDevice);
                i++)
            {
                string resourceName;

                //Removed this code to fix a crash with the latest GigE driver. To be reapplied for CLHS
                /*
                SapLocation location = new SapLocation(m_ServerName, i);
                SapAcqDevice camera = new SapAcqDevice(location, false);

		        bool status = camera.Create();
		        int nPort = 1; //default to 1 streaming port
                if (status && camera.IsParameterAvailable(SapAcqDevice.Prm.NUM_PORTS))
                    camera.GetParameter(SapAcqDevice.Prm.NUM_PORTS, out nPort);

		        // Destroy acquisition device object
		        if (!camera.Destroy()) 
                    continue;
	            if (nPort == 0)
                {
		            continue; //skip this AcqDevice since it doesn't have a video streaming port.
                }
                */
                resourceName = SapManager.GetResourceName(selectedItem.ToString(), SapManager.ResourceType.AcqDevice, i);
                comboBox_Device.Items.Add(resourceName);
                if (i == m_ResourceIndex)
                    comboBox_Device.SelectedItem = resourceName;
            }

            m_ResourceIndex = comboBox_Device.SelectedIndex;
        }

        private bool InitServerCombo()
        {
            comboBox_Server.Items.Clear();
            for (var i = 0; i < SapManager.GetServerCount(); i++)
            {
                // Does this server support "Acq" (frame-grabber) or "AcqDevice" (camera)?
                var bAcq = (ServCategory == ServerCategory.ServerAcq ||
                            ServCategory == ServerCategory.ServerAll) &&
                           SapManager.GetResourceCount(i, SapManager.ResourceType.Acq) > 0;
                var bAcqDevice = (ServCategory == ServerCategory.ServerAcqDevice ||
                                  ServCategory == ServerCategory.ServerAll) &&
                                 SapManager.GetResourceCount(i, SapManager.ResourceType.AcqDevice) > 0;

                if (bAcq)
                {
                    var serverName = SapManager.GetServerName(i);
                    comboBox_Server.Items.Add(new MyListBoxItem(serverName, true));
                }
                else if (bAcqDevice)
                {
                    var serverName = SapManager.GetServerName(i);
                    if (serverName.Contains("CameraLink_") == false)
                        comboBox_Server.Items.Add(new MyListBoxItem(serverName, false));
                }
            }

            if (comboBox_Server.Items.Count <= 0) return false;
            if (string.IsNullOrEmpty(m_ServerName) || comboBox_Server.FindString(m_ServerName, 0) == -1)
            {
                comboBox_Server.SelectedIndex = 0;
                m_ServerName = comboBox_Server.SelectedItem.ToString();
            }
            else
            {
                comboBox_Server.SelectedIndex = comboBox_Server.FindString(m_ServerName, 0);
            }

            InitResourceCombo();
            return true;
        }

        private void LoadSettings()
        {
            var KeyPath = "Software\\Teledyne DALSA\\" + Application.ProductName + "\\SapAcquisition";
            var RegKey = Registry.CurrentUser.OpenSubKey(KeyPath);
            if (RegKey != null)
            {
                // Read location (server and resource) and file name
                m_ServerName = RegKey.GetValue("Server", "").ToString();
                m_ResourceIndex = (int) RegKey.GetValue("Resource", 0);
                if (File.Exists(RegKey.GetValue("ConfigFile", "").ToString()))
                    m_ConfigFile = RegKey.GetValue("ConfigFile", "").ToString();
            }
        }

        private void SaveSettings()
        {
            var KeyPath = "Software\\Teledyne DALSA\\" + Application.ProductName + "\\SapAcquisition";
            var RegKey = Registry.CurrentUser.CreateSubKey(KeyPath);
            // Write config file name and location (server and resource)
            RegKey.SetValue("Server", m_ServerName);
            RegKey.SetValue("ConfigFile", m_ConfigFile);
            RegKey.SetValue("Resource", m_ResourceIndex);
        }

        private void SetDirectories()
        {
            var productDirectory = "";

            if (!string.IsNullOrEmpty(m_ProductDir))
                productDirectory = Environment.GetEnvironmentVariable(m_ProductDir);

            var saperaDir = Environment.GetEnvironmentVariable("SAPERADIR");

            if (m_ConfigFile.Length != 0)
            {
                m_currentConfigDir = Path.GetDirectoryName(m_ConfigFile);
                m_currentConfigFileName = Path.GetFileName(m_ConfigFile);
                textBox_configfile.Text = m_currentConfigDir;
            }
            else
            {
                m_currentConfigFileName = "";
                if (!string.IsNullOrEmpty(productDirectory))
                    m_currentConfigDir = productDirectory;
                else if (saperaDir.Length != 0)
                    m_currentConfigDir = saperaDir + Acquisition_Default_folder;
                textBox_configfile.Text = m_currentConfigDir;
            }
        }


        private void UpdateBoxAvailability()
        {
            // Is config file is required by this type of server?
            var item = (MyListBoxItem) comboBox_Server.SelectedItem;
            var configFileRequired = item.ItemData;
            checkBox_configfile.Enabled = !configFileRequired;

            if (configFileRequired)
            {
                m_ConfigFileEnable = configFileRequired;
                checkBox_configfile.Checked = configFileRequired;
            }
            else
            {
                m_ConfigFileEnable = checkBox_configfile.Checked;
            }

            comboBox_configfile.Enabled = m_ConfigFileEnable && m_configFileAvailable;
            textBox_configfile.Enabled = m_ConfigFileEnable;
            button_browse.Enabled = m_ConfigFileEnable;
            button_ok.Enabled = !m_ConfigFileEnable || m_configFileAvailable;
        }

        private void UpdateCurrentDir(string newCurrentDir)
        {
            if (string.Compare(newCurrentDir, m_currentConfigDir, StringComparison.Ordinal) != 0)
            {
                textBox_configfile.Text = newCurrentDir;
                m_currentConfigDir = newCurrentDir;
            }
        }

        private void UpdateNames()
        {
            //Delete ccf name file list 
            ccffiles.Clear();

            var currentDir = m_currentConfigDir;
            var keyName = ConfigKeyName;
            var curServerName = comboBox_Server.SelectedItem.ToString();
            m_ResourceIndex = comboBox_Device.SelectedIndex;
            m_ServerName = curServerName;
            curServerName = curServerName.Substring(0, curServerName.Length - 2);

            var dir = new DirectoryInfo(currentDir);
            if (dir.Exists)
            {
                var ccffileInfo = dir.GetFiles("*.ccf");
                comboBox_configfile.Items.Clear();

                foreach (var f in ccffileInfo)
                {
                    var filePath = m_currentConfigDir + "\\" + f.Name;
                    var tempServerName = new StringBuilder(512);
                    var sbCameraName = new StringBuilder(512);
                    var sbCompanyName = new StringBuilder(512);
                    var sbModelName = new StringBuilder(512);
                    var sbVicName = new StringBuilder(512);
                    var companyName = "";
                    var modelName = "";
                    var cameraDesc = "";

                    GetPrivateProfileString("Board", "Server name", "Unknow", tempServerName, 512, filePath);

                    // Check if the current configuration file has been created for the current server 
                    if (string.Compare(curServerName, tempServerName.ToString(), StringComparison.OrdinalIgnoreCase) !=
                        0)
                        continue;
                    // Add ccf File name
                    ccffiles.Add(f.Name);

                    GetPrivateProfileString("General", keyName, "Unknown", sbCameraName, 512, filePath);
                    GetPrivateProfileString("General", CompanyKeyName, "", sbCompanyName, 512, filePath);
                    GetPrivateProfileString("General", ModelKeyName, "", sbModelName, 512, filePath);
                    GetPrivateProfileString("General", VicName, "", sbVicName, 512, filePath);

                    if (sbCompanyName.ToString().Length != 0 && sbModelName.ToString().Length != 0)
                        companyName = sbCompanyName + ", ";
                    if (sbModelName.ToString().Length != 0 && sbCameraName.ToString().Length != 0)
                        modelName = sbModelName + ", ";

                    cameraDesc = companyName + modelName + sbCameraName + " - " + sbVicName;

                    var item = new MyListBoxItem(cameraDesc, true);
                    comboBox_configfile.Items.Add(item);
                }
            }

            if (comboBox_configfile.Items.Count != 0)
            {
                m_configFileAvailable = true;
                var newFileIndex = 0;

                // Try to find the current camera file selected
                for (var i = 0; i < ccffiles.Count; i++)
                {
                    var currentccf = (string) ccffiles[i];
                    if (string.Compare(m_currentConfigFileName, currentccf, StringComparison.Ordinal) == 0)
                        newFileIndex = i;
                }

                comboBox_configfile.SelectedIndex = newFileIndex;
            }
            else
            {
                m_configFileAvailable = false;
            }

            UpdateBoxAvailability();
        }

        /// <summary>
        ///     Class to handle listbox item
        /// </summary>
        private class MyListBoxItem
        {
            private readonly string Camdesc;

            public MyListBoxItem(string Text)
            {
                Camdesc = Text;
                ItemData = false;
            }

            public MyListBoxItem(string Text, bool ItemData)
            {
                Camdesc = Text;
                this.ItemData = ItemData;
            }

            public bool ItemData { get; }

            public override string ToString()
            {
                return Camdesc;
            }
        }
    }
}