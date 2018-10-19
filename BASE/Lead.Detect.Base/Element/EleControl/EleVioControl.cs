using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Element.EleControl;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element
{
    public partial class EleVioControl : UserControl, IEleUpdate
    {
        public static object EleCollections;

        public EleVio CurEle;

        public EleVioControl()
        {
            InitializeComponent();
            CurEle = new EleVio();
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

        public EleVioType EleType
        {
            set
            {
                CurEle.Type = value;
                lbType.Text = $"{CurEle.Type}:{CurEle.Driver}:{CurEle.Port}";
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

        private void EleDOControl_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 55;


            if (EleCollections != null)
            {
                var eleName = tBoxName.Text;

                var props = EleCollections.GetType().GetProperties().ToList().FindAll(p => p.PropertyType == typeof(EleVio));
                tBoxName.Items.AddRange(props.Select(p => (object) p.Name).ToArray());


                //load from default name
                if (eleName != null)
                {
                    tBoxName.Text = eleName;
                    ReloadEle();
                    tBoxName.Select(0,0);
                    tBoxName.SelectionLength = 0;
                }
            }

            label1.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (CurEle == null)
            {
                return;
            }

            var configControl = new EleVioConfigControl();
            configControl.StartPosition = FormStartPosition.CenterParent;
            configControl.LoadDo(CurEle);
            configControl.ShowDialog();

            EleType = CurEle.Type;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(PointToScreen(label3.Location));
        }


        public void LoadEle(EleVio newEle)
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
                var props = EleCollections.GetType().GetProperties().ToList().FindAll(p => p.PropertyType == typeof(EleVio));

                var ele = tBoxName.Text.ToString();

                var res = props.Find(pr => pr.Name == ele);
                if (res != null)
                {
                    LoadEle(res.GetValue(EleCollections) as EleVio);
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

        public void UpdateEleStatus(IMotionCard _motion)
        {
            if (CurEle.Enable && _motion != null)
            {
                var val = -1;
                _motion.ReadSingleDOutput(_motion.DevIndex, 0, CurEle.Port, out val);
                if (val != -1)
                {
                    tBoxName.BackColor = (val == 0) ? Color.LightGray : System.Drawing.Color.Lime;
                }
            }
        }

        private void lbType_Click(object sender, EventArgs e)
        {
            if (CurEle == null)
            {
                return;
            }

            var motion = DevPrimsManager.Instance.Prims.FirstOrDefault(p => p.Name == CurEle.Driver) as IMotionCard;
            if (motion != null)
            {
                if (tBoxName.BackColor == Color.Lime)
                {
                    motion.WriteSingleDOutput(motion.DevIndex, 0, CurEle.Port, 0);
                }
                if (tBoxName.BackColor == Color.LightGray)
                {
                    motion.WriteSingleDOutput(motion.DevIndex, 0, CurEle.Port, 1);
                }
            }
        }
    }
}