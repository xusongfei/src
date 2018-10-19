using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.View.PrimView
{
    public delegate void RemoveDev(Guid primID);

    public delegate void AddDev(string group, string type, string primName, Guid primID);

    public delegate void ChangeDevName(Guid primID, string dstPrimName);

    public partial class PrimConfigForm : DockContent
    {
        private readonly List<PrimDisplay> _primDisplayList = new List<PrimDisplay>();


        private int _createIdx;

        private PrimDisplay _curPrimDisplay;
        private PrimToolBar _primToolBar;

        public PrimConfigForm()
        {
            InitializeComponent();
        }

        public event AddDev OnAddPrim;

        public event ChangeDevName OnChangePrimName;

        public event RemoveDev OnRemovePrim;

        private void PrimConfigForm_Load(object sender, EventArgs e)
        {
            _primToolBar = new PrimToolBar();
            _primToolBar.Dock = DockStyle.Fill;
            _primToolBar.InitializePrims();
            _primToolBar.Show();
            _primToolBar.CreateDevice += OnCreatePrim;
            splitContainer2.Panel1.Controls.Add(_primToolBar);
        }

        private void PrimConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void OnPrimDisplayMenuClickEvent(int type, string primName, Guid primGuid)
        {
            var prim = DevPrimsManager.Instance.GetPrimByName(primName);
            if (prim != null)
            {
                switch (type)
                {
                    //this.splitContainer2.Panel2.Controls.Clear();
                    case (int) PrimDisplayClick.ShowAll:
                        if (prim.PrimConfigUI != null)
                        {
                            prim.PrimConfigUI.Dock = DockStyle.Top;
                            //dev.PrimConfigUI.BorderStyle = BorderStyle.FixedSingle;
                            splitContainer2.Panel2.Controls.Add(prim.PrimConfigUI);
                        }

                        //prim.PrimDebugUI.Dock = DockStyle.Top;
                        ////dev.PrimDebugUI.BorderStyle = BorderStyle.FixedSingle;
                        //splitContainer2.Panel2.Controls.Add(prim.PrimDebugUI);
                        //prim.PrimOutputUI.Dock = DockStyle.Top;
                        ////dev.PrimOutputUI.BorderStyle = BorderStyle.FixedSingle;
                        //splitContainer2.Panel2.Controls.Add(prim.PrimOutputUI);
                        break;
                    case (int) PrimDisplayClick.ShowDebug:
                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimDebugUI))
                        {
                            return;
                        }

                        if (prim.PrimDebugUI != null)
                        {
                            prim.PrimDebugUI.Dock = DockStyle.Top;
                            //dev.PrimDebugUI.BorderStyle = BorderStyle.FixedSingle;
                            splitContainer2.Panel2.Controls.Add(prim.PrimDebugUI);
                        }

                        break;
                    case (int) PrimDisplayClick.ShowConfig:
                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimConfigUI))
                        {
                            return;
                        }

                        if (prim.PrimConfigUI != null)
                        {
                            prim.PrimConfigUI.Dock = DockStyle.Top;
                            //dev.PrimConfigUI.BorderStyle = BorderStyle.FixedSingle;
                            splitContainer2.Panel2.Controls.Add(prim.PrimConfigUI);
                        }

                        break;
                    case (int) PrimDisplayClick.ShowOutput:
                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimOutputUI))
                        {
                            return;
                        }

                        if (prim.PrimOutputUI != null)
                        {
                            prim.PrimOutputUI.Dock = DockStyle.Top;
                            //dev.PrimOutputUI.BorderStyle = BorderStyle.FixedSingle;
                            splitContainer2.Panel2.Controls.Add(prim.PrimOutputUI);
                        }

                        break;
                    case (int) PrimDisplayClick.HideDebug:
                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimDebugUI))
                        {
                            splitContainer2.Panel2.Controls.Remove(prim.PrimDebugUI);
                        }

                        break;
                    case (int) PrimDisplayClick.HideConfig:
                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimConfigUI))
                        {
                            splitContainer2.Panel2.Controls.Remove(prim.PrimConfigUI);
                        }

                        break;
                    case (int) PrimDisplayClick.HideOutput:
                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimOutputUI))
                        {
                            splitContainer2.Panel2.Controls.Remove(prim.PrimOutputUI);
                        }

                        break;
                    case (int) PrimDisplayClick.Remove:
                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimDebugUI))
                        {
                            splitContainer2.Panel2.Controls.Remove(prim.PrimDebugUI);
                        }

                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimConfigUI))
                        {
                            splitContainer2.Panel2.Controls.Remove(prim.PrimConfigUI);
                        }

                        if (splitContainer2.Panel2.Controls.Contains(prim.PrimOutputUI))
                        {
                            splitContainer2.Panel2.Controls.Remove(prim.PrimOutputUI);
                        }

                        foreach (var primDisplay in _primDisplayList)
                        {
                            if (primDisplay.UIPrimID == primGuid)
                            {
                                splitContainer1.Panel1.Controls.Remove(primDisplay);
                                _primDisplayList.Remove(primDisplay);
                                DevPrimsManager.Instance.RemovePrimByGUID(primGuid);
                                if (OnRemovePrim != null)
                                {
                                    OnRemovePrim(primGuid);
                                }

                                break;
                            }
                        }

                        break;
                }
            }
        }

        private void OnPrimDisplayPropertyChanged(Guid primId, string primName)
        {
            if (OnChangePrimName != null)
            {
                OnChangePrimName(primId, primName);
            }
        }

        public void CreatePrimDisplay(IPrim prim)
        {
            var primCreator = DevPrimsFactoryManager.Instance.FindCreator(prim.PrimTypeName);
            if (primCreator == null)
            {
                MessageBox.Show($"未找到该设备{prim.PrimTypeName}创建器ICreator，请重新加载该设备Dll!");
                return;
            }

            var primDisplay = new PrimDisplay();
            primDisplay.UIPrimIcon = primCreator.PrimProps.Icon;
            primDisplay.UIPrimName = prim.Name;
            primDisplay.UIPrimGroup = primCreator.PrimProps.DisplayGroup;
            primDisplay.UIPrimType = primCreator.PrimProps.DisplayName;
            primDisplay.panel1.AutoSize = false;
            primDisplay.panel1.Size = new Size(56, 52);
            primDisplay.UIPrimID = prim.PrimId;
            primDisplay.UIPrim = prim;
            primDisplay.PrimDisplayMenuClickEvent += OnPrimDisplayMenuClickEvent;
            primDisplay.PrimDisplayPropertyChanged += OnPrimDisplayPropertyChanged;
            primDisplay.Dock = DockStyle.Top;
            splitContainer1.Panel1.Controls.Add(primDisplay);
            _primDisplayList.Add(primDisplay);

            if (OnAddPrim != null)
            {
                OnAddPrim(primDisplay.UIPrimGroup, primDisplay.UIPrimType, primDisplay.UIPrimName, primDisplay.UIPrimID);
            }
        }

        public void OnCreatePrim(string primName)
        {
            var primCreator = DevPrimsFactoryManager.Instance.FindCreator(primName);
            if (primCreator == null)
            {
                MessageBox.Show("未找到该设备创建器ICreator，请重新加载该设备Dll!");
                return;
            }

            var prim = DevPrimsFactoryManager.Instance.InvokeCreator(primName);
            if (prim == null)
            {
                MessageBox.Show("未找到该基元Prim，请重新加载该设备Dll!");
                return;
            }

            prim.PrimId = Guid.NewGuid();


            var primDisplay = new PrimDisplay();
            primDisplay.UIPrimIcon = primCreator.PrimProps.Icon;
            primDisplay.UIPrimName = primCreator.PrimProps.Name + _createIdx;
            primDisplay.UIPrimGroup = primCreator.PrimProps.DisplayGroup;
            primDisplay.UIPrimType = primCreator.PrimProps.DisplayName;
            primDisplay.panel1.AutoSize = false;
            primDisplay.panel1.Size = new Size(56, 52);
            primDisplay.UIPrimID = prim.PrimId;
            primDisplay.UIPrim = prim;
            primDisplay.PrimDisplayMenuClickEvent += OnPrimDisplayMenuClickEvent;
            primDisplay.PrimDisplayPropertyChanged += OnPrimDisplayPropertyChanged;
            primDisplay.Dock = DockStyle.Top;
            splitContainer1.Panel1.Controls.Add(primDisplay);
            _primDisplayList.Add(primDisplay);

            prim.Name = primDisplay.UIPrimName;
            prim.PrimTypeName = primCreator.PrimProps.Name;
            DevPrimsManager.Instance.Prims.Add(prim);


            OnAddPrim?.Invoke(primDisplay.UIPrimGroup, primDisplay.UIPrimType, primDisplay.UIPrimName, primDisplay.UIPrimID);
            _createIdx++;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            var primTypeName = e.Data.GetData(typeof(string)) as string;
            var prim = DevPrimsFactoryManager.Instance.InvokeCreator(primTypeName);
            if (prim == null)
            {
                MessageBox.Show("未找到该设备DevPrim，请重新加载该设备Dll!");
            }
            else
            {
                prim.PrimId = Guid.NewGuid();
                prim.Name = _curPrimDisplay.UIPrimName;
                DevPrimsManager.Instance.Prims.Add(prim);
                _curPrimDisplay.UIPrimID = prim.PrimId;
                _curPrimDisplay.UIPrim = prim;
                _curPrimDisplay.PrimDisplayMenuClickEvent -= OnPrimDisplayMenuClickEvent;
                _curPrimDisplay.PrimDisplayMenuClickEvent += OnPrimDisplayMenuClickEvent;
                _curPrimDisplay.PrimDisplayPropertyChanged -= OnPrimDisplayPropertyChanged;
                _curPrimDisplay.PrimDisplayPropertyChanged += OnPrimDisplayPropertyChanged;
                _curPrimDisplay.Dock = DockStyle.Top;
                _primDisplayList.Add(_curPrimDisplay);
                if (OnAddPrim != null)
                {
                    OnAddPrim(_curPrimDisplay.UIPrimGroup, _curPrimDisplay.UIPrimType, _curPrimDisplay.UIPrimName, _curPrimDisplay.UIPrimID);
                }

                _createIdx++;
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            var primTypeName = e.Data.GetData(typeof(string)) as string;
            var primCreator = DevPrimsFactoryManager.Instance.FindCreator(primTypeName);
            if (primCreator == null)
            {
                MessageBox.Show("未找到该设备创建器ICreator，请重新加载该设备Dll!");
            }
            else
            {
                var primDisplay = new PrimDisplay();
                primDisplay.UIPrimIcon = primCreator.PrimProps.Icon;
                primDisplay.UIPrimName = primCreator.PrimProps.DisplayName + _createIdx;
                primDisplay.UIPrimGroup = primCreator.PrimProps.DisplayGroup;
                primDisplay.UIPrimType = primCreator.PrimProps.DisplayName;
                primDisplay.panel1.AutoSize = false;
                primDisplay.panel1.Size = new Size(56, 52);
                var pPoint = PointToClient(MousePosition);
                primDisplay.Location = new Point(pPoint.X - 26, pPoint.Y - 26);
                _curPrimDisplay = primDisplay;
                splitContainer1.Panel1.Controls.Add(primDisplay);
            }
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            _curPrimDisplay = null;
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            if (_curPrimDisplay != null)
            {
                var pPoint = PointToClient(MousePosition);
                _curPrimDisplay.Location = new Point(pPoint.X - 26, pPoint.Y - 26);
            }
        }

        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            panel1_DragDrop(sender, e);
        }

        private void splitContainer1_Panel1_DragEnter(object sender, DragEventArgs e)
        {
            panel1_DragEnter(sender, e);
        }

        private void splitContainer1_Panel1_DragLeave(object sender, EventArgs e)
        {
            panel1_DragLeave(sender, e);
        }

        private void splitContainer1_Panel1_DragOver(object sender, DragEventArgs e)
        {
            panel1_DragOver(sender, e);
        }

        private void splitContainer1_Panel2_DragDrop_1(object sender, DragEventArgs e)
        {
            panel1_DragDrop(sender, e);
        }

        private void splitContainer1_Panel2_DragEnter_1(object sender, DragEventArgs e)
        {
            panel1_DragEnter(sender, e);
        }

        private void splitContainer1_Panel2_DragLeave_1(object sender, EventArgs e)
        {
            panel1_DragLeave(sender, e);
        }

        private void splitContainer1_Panel2_DragOver_1(object sender, DragEventArgs e)
        {
            panel1_DragOver(sender, e);
        }
    }
}