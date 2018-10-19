using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonStruct.Communicate;
using CommonStruct.LC3D;
using TaskControlLib;

namespace Lead.Detect.Prim3DAgl
{
    public partial class PrimOutputControl : UserControl
    {
        private readonly TaskControl _taskctrl = new TaskControl();

        public PrimOutputControl()
        {
            InitializeComponent();
            chkBtn2D.Checked = false;
        }


        public bool Image2D
        {
            set { chkBtn2D.Checked = value; }
        }

        public bool Image3D
        {
            set { chkBtn3D.Checked = value; }
        }

        public string AglName
        {
            set { lbTitle.Text = value + " - Output"; }
        }

        private void chkBtn2D_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkBtn2D.Checked)
            {
                _taskctrl.Graph2D = null;
                splitContainerUI.Panel2Collapsed = true;
            }
            else
            {
                _taskctrl.Graph2D = map2D1;
                splitContainerUI.Panel2Collapsed = false;
            }
        }

        private void chkBtn3D_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkBtn3D.Checked)
            {
                _taskctrl.Graph3D = null;
                _taskctrl.ColorRuler3D = null;
                splitContainerUI.Panel1Collapsed = true;
            }
            else
            {
                _taskctrl.Graph3D = map3D1;
                _taskctrl.ColorRuler3D = colorRuler1;
                splitContainerUI.Panel1Collapsed = false;
            }
        }


        public bool Init()
        {
            _taskctrl.updateMsg += UpdateMonitorMsg;

            return true;
        }

        private void lbTitle_Click(object sender, EventArgs e)
        {
        }


        public void LoadTask(string pattTask)
        {
            _taskctrl.OpenMeasureTask(pattTask);
        }

        private void map2D1_Load(object sender, EventArgs e)
        {
        }

        private void map3D1_Load(object sender, EventArgs e)
        {
        }

        private void PrimOutputControl_Load(object sender, EventArgs e)
        {
            _taskctrl.Graph3D = map3D1;
            _taskctrl.ColorRuler3D = colorRuler1;
            _taskctrl.Graph2D = map2D1;
        }

        public OutputPrimData RunTask(bool StartCount, int Index, List<List<PointFS>> fsPointList)
        {
            var inputData = new InputPrimData();

            //for (int i = 0; i < fsPointList.Count; i++)
            //{
            var pointcloudData = new PointCloudData();
            pointcloudData.SetLaserPoints(fsPointList);
            _taskctrl.FillSingleInputPrimData(Index, pointcloudData);

            //    }


            if (StartCount)
            {
                OutputPrimData outputData = null;
                // if (!_taskctrl.TaskRun(inputData,out outputData))
                if (!_taskctrl.TaskRun(out outputData)) return null;

                return outputData;
            }

            return null;
        }


        public void RunTaskLine(OutputPrimData fsPointList)
        {
            try
            {
                for (var I = 0; I < fsPointList.lstLinePrim.Count; I++) _taskctrl.FillSingleInputPrimData(I, fsPointList.lstLinePrim[I]);
                for (var I = 0; I < fsPointList.lstPlanePrim.Count; I++) _taskctrl.FillSingleInputPrimData(I, fsPointList.lstPlanePrim[I]);
            }
            catch
            {
            }
        }

        private void splitContainerMain_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }


        private void UpdateMonitorMsg(string mes)
        {
        }
    }
}