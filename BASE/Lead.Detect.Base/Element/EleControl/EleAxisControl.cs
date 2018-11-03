using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element.EleControl
{
    public partial class EleAxisControl : UserControl, IEleUpdate
    {
        public static object EleCollections;


        public EleAxis CurEle;

        public EleAxisControl()
        {
            InitializeComponent();
            CurEle = new EleAxis();
        }

        public EleAxisControl(EleAxis curEle)
        {
            InitializeComponent();
            CurEle = curEle;
        }

        public string EleName
        {
            set
            {
                CurEle.Name = value;
                tBoxName.Text = CurEle.Name;
            }

            get { return CurEle.Name; }
        }

        public EleAxisType EleType
        {
            set
            {
                CurEle.Type = value;
                lbType.Text = $"{CurEle.Type}:{CurEle.AxisChannel}:{CurEle.AxisPlsPerRoll}:{CurEle.AxisLead:F2}:{CurEle.AxisSpeed:F0}:{CurEle.AxisAcc:F0}";
            }
            get { return CurEle.Type; }
        }

        public string EleDescription
        {
            set
            {
                CurEle.Description = value;
                tBoxInfo.Text = CurEle.Description;
            }

            get { return CurEle.Description; }
        }

        private void EleAxis_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 55;

            if (EleCollections != null)
            {
                var eleName = tBoxName.Text;

                var props = EleCollections.GetType().GetProperties().ToList().FindAll(p => p.PropertyType == typeof(EleAxis));
                tBoxName.Items.AddRange(props.Select(p => (object) p.Name).ToArray());

                //load from default name
                if (eleName != null)
                {
                    tBoxName.Text = eleName;
                    ReloadEle();
                    tBoxName.Select(0, 0);
                    tBoxName.SelectionLength = 0;
                }
            }

            label1.Focus();
        }

        private void EleAxis_Click(object sender, EventArgs e)
        {
            if (CurEle == null) return;

            var eleAxisConfigControl = new EleAxisConfigControl();
            eleAxisConfigControl.LoadAxis(CurEle);
            eleAxisConfigControl.ShowDialog();
        }

        public void LoadEle(EleAxis newEle)
        {
            if (newEle != null)
            {
                EleName = newEle.Name;
                EleDescription = newEle.Description;
                EleType = newEle.Type;

                CurEle = newEle;

                label1.BackColor = Color.LightGreen;
            }
            else
            {
                label1.BackColor = Color.LightGray;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (CurEle == null) return;

            var configControl = new EleAxisConfigControl();
            configControl.StartPosition = FormStartPosition.CenterParent;
            configControl.LoadAxis(CurEle);
            configControl.ShowDialog();


            EleType = CurEle.Type;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(label3.Location));
        }


        private void tBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadEle();
        }

        private void tBoxName_TextChanged(object sender, EventArgs e)
        {
            CurEle.Name = tBoxName.Text;
            ReloadEle();
        }

        private void ReloadEle()
        {
            if (EleCollections != null)
            {
                var props = EleCollections.GetType().GetProperties().ToList().FindAll(p => p.PropertyType == typeof(EleAxis));

                var ele = tBoxName.Text.ToString();

                var res = props.Find(pr => pr.Name == ele);
                if (res != null)
                {
                    LoadEle(res.GetValue(EleCollections) as EleAxis);
                }
                else
                {
                    label1.BackColor = Color.LightGray;
                }
            }
        }


        private void tBoxInfo_TextChanged(object sender, EventArgs e)
        {
            CurEle.Description = tBoxInfo.Text;
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ele = new ElePropsForm()
            {
                Element = CurEle
            };


            if (ele.ShowDialog() == DialogResult.OK)
            {
                EleType = CurEle.Type;
            }
        }

        public void UpdateEleStatus(IMotionCard motion)
        {
            if (motion != null)
            {
                motion.ReadStatus(CurEle.AxisChannel);

                if (motion.AxisIsAlarm(0,CurEle.AxisChannel))
                {
                    tBoxName.BackColor = Color.Red;
                }
                else
                {
                    tBoxName.BackColor = motion.AxisIsStop(0, CurEle.AxisChannel) ? Color.Lime : Color.LightGray;
                }
            }
        }
    }
}