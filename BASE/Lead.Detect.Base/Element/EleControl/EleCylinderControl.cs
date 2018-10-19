using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Element.EleControl;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element
{
    public partial class EleCylinderControl : UserControl, IEleUpdate
    {
        public static object EleCollections;


        public EleCylinder CurEle;

        public EleCylinderControl()
        {
            InitializeComponent();
            CurEle = new EleCylinder();
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

        public string EleDescription
        {
            set
            {
                CurEle.Description = value;
                tBoxInfo.Text = CurEle.Description;
            }

            get { return CurEle.Description; }
        }

        public EleDoType EleType
        {
            set
            {
                CurEle.Type = value;
                lbType.Text = $"{CurEle.Type}:{CurEle.Driver}：{CurEle.DiOrg}:{CurEle.DiWork}:{CurEle.DoOrg}:{CurEle.DoWork}";
            }
            get { return CurEle.Type; }
        }

        private void EleCylinder_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 55;

            if (EleCollections != null)
            {
                var eleName = tBoxName.Text;

                var props = EleCollections.GetType().GetProperties().ToList().FindAll(p => p.PropertyType == typeof(EleCylinder));
                tBoxName.Items.AddRange(props.Select(p => (object)p.Name).ToArray());


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


        public void LoadEle(EleCylinder newEle)
        {
            if (newEle != null)
            {
                EleName = newEle.Name;
                EleDescription = newEle.Description;
                EleType = newEle.Type;

                CurEle = newEle;

                label1.BackColor = Color.Lime;
            }
            else
            {
                label1.BackColor = Color.LightGray;
            }
        }

        private void labelName_Click(object sender, EventArgs e)
        {
            if (CurEle == null)
            {
                return;
            }


            var configControl = new EleCylinderConfigControl();
            configControl.StartPosition = FormStartPosition.CenterParent;
            configControl.LoadCylinder(CurEle);
            configControl.ShowDialog();


            EleType = CurEle.Type;
        }

        private void EleCylinder_Click(object sender, EventArgs e)
        {
            if (CurEle == null)
            {
                return;
            }


            var configControl = new EleCylinderConfigControl();
            configControl.StartPosition = FormStartPosition.CenterParent;
            configControl.LoadCylinder(CurEle);
            configControl.ShowDialog();
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
                    motion.WriteSingleDOutput(motion.DevIndex, 0, CurEle.DoOrg, 1);
                    motion.WriteSingleDOutput(motion.DevIndex, 0, CurEle.DoWork, 0);
                }
                if (tBoxName.BackColor == Color.LightGray)
                {
                    motion.WriteSingleDOutput(motion.DevIndex, 0, CurEle.DoOrg, 0);
                    motion.WriteSingleDOutput(motion.DevIndex, 0, CurEle.DoWork, 1);
                }
            }
        }

        private void labelDetails_Click(object sender, EventArgs e)
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
                var props = EleCollections.GetType().GetProperties().ToList().FindAll(p => p.PropertyType == typeof(EleCylinder));

                var ele = tBoxName.Text.ToString();

                var res = props.Find(pr => pr.Name == ele);
                if (res != null)
                {
                    LoadEle(res.GetValue(EleCollections) as EleCylinder);
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
                var axis = ele.Element as EleCylinder;

                var props = typeof(EleCylinder).GetProperties();
                foreach (var p in props)
                {
                    p.SetValue(CurEle, p.GetValue(axis));
                }


                EleType = CurEle.Type;
            }
        }


        public void UpdateEleStatus(IMotionCard motion)
        {
            if (motion != null)
            {
                var org = 0;
                var work = 0;
                motion.ReadSingleDInput(motion.DevIndex, 0, CurEle.DiOrg, out org);
                motion.ReadSingleDInput(motion.DevIndex, 0, CurEle.DiWork, out work);
                if (org == 1 && work == 0)
                {
                    tBoxName.BackColor = Color.LightGray;
                }
                else if (org == 0 && work == 1)
                {
                    tBoxName.BackColor = Color.Lime;
                }
                else
                {
                    tBoxName.BackColor = Color.Red;
                }
            }
        }

    }
}