using System;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalStation;

namespace Lead.Detect.ThermoAOI.View
{
    public partial class FrmProgress : Form
    {
        public string LabelName = "软件初始化中，请稍等......................";
        public int LabelIndex = 0;
        public int Astep = 0;
        public int Bstep = 0;

        public long HistoryTime = 0;

        public FrmProgress()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = LabelName;

            foreach (IStation station in StationMgr.Instance.Stations)
            {
                //if (station.Name.Contains("A"))
                //{
                //    Astep = station.StationStep;
                //}
                //if (station.Name.Contains("B"))
                //{
                //    Bstep = station.StationStep;
                //}

                if ((Bstep + Astep) >= progressBar1.Maximum)
                    progressBar1.Value = progressBar1.Maximum;
                else
                    progressBar1.Value = Bstep + Astep;
            }


            bool EN = true;
            //foreach (IStation station in StationMgr.Ins.Stations)
            //{
            //    if (station.Enable) EN = EN && station.StationResetSucess;
            //}
            if (EN) progressBar1.Value = progressBar1.Maximum;
            if (progressBar1.Value >= progressBar1.Maximum)
            {
                this.Close();
            }

            if ((Environment.TickCount - HistoryTime) >= 180000)
            {
                this.Close();
            }

            this.BringToFront();
        }

        private void FrmProgress_Load(object sender, EventArgs e)
        {
            //foreach (IStation station in StationMgr.Ins.ListStation)
            //{
            //  if (station.Enable)
            //  {  progressBar1.Maximum = progressBar1.Maximum + 50; }
            //}
            this.Top = 430;
            this.Left = 400;
            this.Opacity = 0.85;
            HistoryTime = Environment.TickCount;
            if (LabelIndex == 0)
            {
                LabelName = "软件复位中，请稍等......................";
                progressBar1.Maximum = 200;
                timer2.Enabled = true;
            }
            else if (LabelIndex == 1)
            {
                LabelName = "软件初始化中，请稍等......................";
                progressBar1.Maximum = 200;
                timer1.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = LabelName;

            foreach (IStation station in StationMgr.Instance.Stations)
            {
                //if (station.Name.Contains("A"))
                //{
                //    if ((progressBar1.Value + station.StationStep) > progressBar1.Maximum)
                //        progressBar1.Value = progressBar1.Maximum;
                //    else
                //        progressBar1.Value = station.StationStep;
                //}
            }

            bool EN = true;
            foreach (IStation station in StationMgr.Instance.Stations)
            {
                // if (station.Enable) EN = EN && station.StationResetSucess;
            }

            if (EN) progressBar1.Value = progressBar1.Maximum;
            if (progressBar1.Value >= progressBar1.Maximum)
            {
                this.Close();
            }

            this.BringToFront();
        }
    }
}