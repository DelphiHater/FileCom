using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace FileCom
{
    public partial class Form1 : Form
    {
        private void LoadRootNodes()
        {
            // Create the root shell item.
            ShellItem m_shDesktop = new ShellItem();

            // Create the root node.
            TreeNode tvwRoot = new TreeNode();
            tvwRoot.Text = m_shDesktop.DisplayName;
            tvwRoot.ImageIndex = m_shDesktop.IconIndex;
            tvwRoot.SelectedImageIndex = m_shDesktop.IconIndex;
            tvwRoot.Tag = m_shDesktop;

            // Now we need to add any children to the root node.
            ArrayList arrChildren = m_shDesktop.GetSubFolders();
            foreach (ShellItem shChild in arrChildren)
            {
                TreeNode tvwChild = new TreeNode();
                tvwChild.Text = shChild.DisplayName;
                tvwChild.ImageIndex = shChild.IconIndex;
                tvwChild.SelectedImageIndex = shChild.IconIndex;
                tvwChild.Tag = shChild;

                // If this is a folder item and has children then add a place holder node.
                if (shChild.IsFolder && shChild.HasSubFolder)
                    tvwChild.Nodes.Add("PH");
                tvwRoot.Nodes.Add(tvwChild);
            }

            // Add the root node to the tree.
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(tvwRoot);
            tvwRoot.Expand();
        }

        public Form1()
        {
            InitializeComponent();
            светлаяToolStripMenuItem_Click(null, null);
            SystemImageList.SetTVImageList(treeView1.Handle);
            LoadRootNodes();
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public class TestColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return System.Drawing.Color.Blue; ; }
            }

            public override Color MenuBorder  //added for changing the menu border
            {
                get { return Color.White; }
            }
        }

        private void тёмнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new TestColorTable());
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.BackColor = this.BackColor;
            this.menuStrip1.ForeColor = this.ForeColor;
            this.treeView1.ForeColor = this.menuStrip1.ForeColor;
            this.treeView1.BackColor = this.menuStrip1.BackColor;
        }

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ForeColor = System.Drawing.Color.Black;
            this.menuStrip1.BackColor = this.BackColor;
            this.menuStrip1.ForeColor = this.ForeColor;
            this.treeView1.ForeColor = this.menuStrip1.ForeColor;
            this.treeView1.BackColor = this.menuStrip1.BackColor;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
