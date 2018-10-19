using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.View.PrimView
{
    public delegate void PrimRemove(string group, string type, string devName);

    public delegate void PrimAdd(string group, string type, string devName);

    public partial class PrimMgrForm : DockContent
    {
        public PrimMgrForm()
        {
            InitializeComponent();
        }

        private void btnPrimAdd_Click(object sender, EventArgs e)
        {
        }

        private void btnPrimDel_Click(object sender, EventArgs e)
        {
        }

        private void treePrimMng_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        //public event PrimRemove OnDevRemove;

        //public event PrimAdd OnDevAdd;

        public void PrimAdd(string group, string type, string primName, Guid primID)
        {
            int cntGroup = this.treePrimMng.Nodes.Count;
            bool haveGroup = false;
            int idxGroup = -1;
            bool haveType = false;
            int idxType = -1;
            bool haveDev = false;
            for (int i = 0; i < cntGroup; i++)
            {
                if (this.treePrimMng.Nodes[i].Text == group)
                {
                    haveGroup = true;
                    idxGroup = i;
                    break;
                }
            }

            if (!haveGroup)
            {
                TreeNode tn = new TreeNode(group);
                this.treePrimMng.Nodes.Add(tn);
                idxGroup = cntGroup;
            }

            int cntType = this.treePrimMng.Nodes[idxGroup].Nodes.Count;
            for (int j = 0; j < cntType; j++)
            {
                if (this.treePrimMng.Nodes[idxGroup].Nodes[j].Text == type)
                {
                    haveType = true;
                    idxType = j;
                    break;
                }
            }

            if (!haveType)
            {
                TreeNode tn = new TreeNode(type);
                this.treePrimMng.Nodes[idxGroup].Nodes.Add(tn);
                idxType = cntType;
            }

            int cntDev = this.treePrimMng.Nodes[idxGroup].Nodes[idxType].Nodes.Count;
            for (int k = 0; k < cntDev; k++)
            {
                if (this.treePrimMng.Nodes[idxGroup].Nodes[idxType].Nodes[k].Text == primName)
                {
                    haveDev = true;
                    break;
                }
            }

            if (!haveDev)
            {
                TreeNode tn = new TreeNode(primName);
                tn.Tag = primID;
                this.treePrimMng.Nodes[idxGroup].Nodes[idxType].Nodes.Add(tn);
            }
        }

        public void PrimRemove(string group, string type, string devName, Guid primID)
        {
        }

        public void PrimRemove(Guid primID)
        {
            int cntGroup = this.treePrimMng.Nodes.Count;
            if (cntGroup >= 1)
            {
                for (int i = 0; i < cntGroup; i++)
                {
                    int cntType = this.treePrimMng.Nodes[i].Nodes.Count;
                    if (cntType >= 1)
                    {
                        for (int j = 0; j < cntType; j++)
                        {
                            int cntDev = this.treePrimMng.Nodes[i].Nodes[j].Nodes.Count;
                            if (cntDev >= 1)
                            {
                                for (int k = 0; k < cntDev; k++)
                                {
                                    if ((Guid) this.treePrimMng.Nodes[i].Nodes[j].Nodes[k].Tag == primID)
                                    {
                                        this.treePrimMng.Nodes[i].Nodes[j].Nodes.RemoveAt(k);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void PrimNameChange(Guid primID, string dstName)
        {
            int cntGroup = this.treePrimMng.Nodes.Count;
            if (cntGroup >= 1)
            {
                for (int i = 0; i < cntGroup; i++)
                {
                    int cntType = this.treePrimMng.Nodes[i].Nodes.Count;
                    if (cntType >= 1)
                    {
                        for (int j = 0; j < cntType; j++)
                        {
                            int cntDev = this.treePrimMng.Nodes[i].Nodes[j].Nodes.Count;
                            if (cntDev >= 1)
                            {
                                for (int k = 0; k < cntDev; k++)
                                {
                                    if ((Guid) this.treePrimMng.Nodes[i].Nodes[j].Nodes[k].Tag == primID)
                                    {
                                        this.treePrimMng.Nodes[i].Nodes[j].Nodes[k].Text = dstName;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}