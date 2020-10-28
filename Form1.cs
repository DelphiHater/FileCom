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
using System.Management;
using System.Globalization;
using System.Security.Cryptography;

namespace FileCom
{
    public partial class Form1 : Form
    {
        public class TestColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return System.Drawing.Color.Beige; ; }
            }

            public override Color MenuBorder  //added for changing the menu border
            {
                get { return Color.White; }
            }
        }

        private void PopulateDriveList(TreeView tvFolders, string n, string q)
        {
            TreeNode nodeTreeNode;
            int imageIndex = 0;
            int selectIndex = 0;

            const int Removable = 2;
            const int LocalDisk = 3;
            const int Network = 4;
            const int CD = 5;
            //const int RAMDrive = 6;

            //clear TreeView
            tvFolders.Nodes.Clear();
            nodeTreeNode = new TreeNode(n, 0, 0);
            tvFolders.Nodes.Add(nodeTreeNode);

            //set node collection
            TreeNodeCollection nodeCollection = nodeTreeNode.Nodes;

            //Get Drive list
            ManagementObjectCollection queryCollection = getDrives(q);
            foreach (ManagementObject mo in queryCollection)
            {

                switch (int.Parse(mo["DriveType"].ToString()))
                {
                    case Removable:         //removable drives
                        imageIndex = 5;
                        selectIndex = 5;
                        break;
                    case LocalDisk:         //Local drives
                        imageIndex = 6;
                        selectIndex = 6;
                        break;
                    case CD:                //CD rom drives
                        imageIndex = 7;
                        selectIndex = 7;
                        break;
                    case Network:           //Network drives
                        imageIndex = 8;
                        selectIndex = 8;
                        break;
                    default:                //defalut to folder
                        imageIndex = 2;
                        selectIndex = 3;
                        break;
                }
                //create new drive node
                nodeTreeNode = new TreeNode(mo["Name"].ToString() + "\\", imageIndex, selectIndex);

                //add new node
                nodeCollection.Add(nodeTreeNode);
            }
        }

        protected void InitListView(ListView lvFiles)
        {
            //init ListView control
            lvFiles.Clear();        //clear control
                                    //create column header for ListView
            lvFiles.Columns.Add("Name", 130, System.Windows.Forms.HorizontalAlignment.Left);
            lvFiles.Columns.Add("Size", 80, System.Windows.Forms.HorizontalAlignment.Right);
            lvFiles.Columns.Add("Created", 120, System.Windows.Forms.HorizontalAlignment.Left);
            lvFiles.Columns.Add("Modified", 120, System.Windows.Forms.HorizontalAlignment.Left);

        }

        protected ManagementObjectCollection getDrives(string q)
        {
            //get drive collection
            ManagementObjectSearcher query = new ManagementObjectSearcher(q);
            ManagementObjectCollection queryCollection = query.Get();

            return queryCollection;
        }

        public Form1()
        {
            InitializeComponent();
            светлаяToolStripMenuItem_Click(null, null);
            скрытьВторойЭкранToolStripMenuItem_Click(null, null);
            string n = "My Computer";
            string q = "SELECT * From Win32_LogicalDisk ";
            PopulateDriveList(tvFolders, n, q);
            tvFolders.Nodes[0].ExpandAll();
            this.ContextMenuStrip = this.contextMenuStrip1;
        }

        private void тёмнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new TestColorTable());
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.BackColor = this.BackColor;
            this.menuStrip1.ForeColor = this.ForeColor;
            this.label1.ForeColor = this.ForeColor;
            this.button1.ForeColor = this.ForeColor;
            this.button1.BackColor = this.BackColor;
            this.button5.ForeColor = this.ForeColor;
            this.button5.BackColor = this.BackColor;
            this.tvFolders.ForeColor = this.ForeColor;
            this.tvFolders.BackColor = this.BackColor;
            this.lvFiles.ForeColor = this.ForeColor;
            this.lvFiles.BackColor = this.BackColor;
            this.listView1.ForeColor = this.ForeColor;
            this.listView1.BackColor = this.BackColor;
            this.m_imageListTreeView.TransparentColor = this.BackColor;
        }

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ForeColor = System.Drawing.Color.Black;
            this.menuStrip1.BackColor = this.BackColor;
            this.menuStrip1.ForeColor = this.ForeColor;
            this.label1.ForeColor = this.ForeColor;
            this.button1.ForeColor = this.ForeColor;
            this.button1.BackColor = this.BackColor;
            this.button5.ForeColor = this.ForeColor;
            this.button5.BackColor = this.BackColor;
            this.tvFolders.ForeColor = this.ForeColor;
            this.tvFolders.BackColor = this.BackColor;
            this.lvFiles.ForeColor = this.ForeColor;
            this.lvFiles.BackColor = this.BackColor;
            this.listView1.ForeColor = this.ForeColor;
            this.listView1.BackColor = this.BackColor;
            this.m_imageListTreeView.TransparentColor = this.BackColor;
        }

        TreeNode lastPath;
        TreeNode nodeCurrent;

        private void button1_Click(object sender, EventArgs e)
        {
            TreeViewEventArgs t = new TreeViewEventArgs(lastPath);
            tvFolders_AfterSelect(tvFolders, t);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            isHide = !isHide;
        }

        bool isHide = false;            //для второго экрана
        bool isSec = false;            //для второго экрана

        private void скрытьВторойЭкранToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSec)
            {
                this.Width = this.Width *4 / 7;
                groupBox2.Visible = false;
                isHide = true;
                button5.Enabled = false;
            }
            isSec = true;
        }

        private void второйЭкранToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSec)
            {
                this.Width = this.Width * 7 / 4;
                groupBox2.Visible = true;
                isHide = false;
                button5.Enabled = true;
            }
            isSec = false;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название нового текстового документа", "Ввод", "Текст", 100, 100);
                File.Create(textBox1.Text + "//" + name + ".txt");
                TreeViewEventArgs t = new TreeViewEventArgs(nodeCurrent);
                tvFolders_AfterSelect(tvFolders, t);
            }
            catch
            {
                MessageBox.Show("Файл не добавлен. Проверьте путь к каталогу");
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvFiles.SelectedItems.Count != 0)
                {
                    if (textBox1.Text.Contains(lvFiles.SelectedItems[0].Text))
                    {
                        File.Delete(textBox1.Text);
                        TreeViewEventArgs t = new TreeViewEventArgs(lastPath);
                        tvFolders_AfterSelect(tvFolders, t);
                    }
                }
                else
                {
                    Directory.Delete(textBox1.Text);
                    TreeViewEventArgs t = new TreeViewEventArgs(nodeCurrent);
                    tvFolders_AfterSelect(tvFolders, t);
                }
            }
            catch
            {
                MessageBox.Show("Выберите файл для удаления");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tvFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Populate folders and files when a folder is selected
            //this.Cursor = Cursors.WaitCursor;

            //get current selected drive or folder
            lastPath = nodeCurrent;
            nodeCurrent = e.Node;
            string S2 = "My Computer\\";
            string S1;
            try
            {
                S1 = nodeCurrent.FullPath;
            }
            catch
            {
                S1 = nodeCurrent.Text;
            }

            int index = S1.IndexOf(S2);
            if (index != -1)
            {
                S1 = S1.Remove(index, S2.Length);
            }
            textBox1.Text = S1;

            //clear all sub-folders
            nodeCurrent.Nodes.Clear();

            if (nodeCurrent.SelectedImageIndex == 0)
            {
                //Selected My Computer - repopulate drive list
                string n = "My Computer";
                string q = "SELECT * From Win32_LogicalDisk ";
                PopulateDriveList(tvFolders, n, q);
            }
            else
            {
                //populate sub-folders and folder files
                PopulateDirectory(nodeCurrent, nodeCurrent.Nodes);
            }
        }

        protected void PopulateDirectory(TreeNode nodeCurrent, TreeNodeCollection nodeCurrentCollection)
        {
            TreeNode nodeDir;
            int imageIndex = 2;     //unselected image index
            int selectIndex = 3;	//selected image index

            if (nodeCurrent.SelectedImageIndex != 0)
            {
                //populate treeview with folders
                try
                {
                    //check path
                    if (Directory.Exists(getFullPath(nodeCurrent.FullPath)) == false)
                    {
                        MessageBox.Show("Directory or path " + nodeCurrent.ToString() + " does not exist.");
                    }
                    else
                    {
                        //populate files
                        if (isHide)
                        {
                            PopulateFiles(nodeCurrent, lvFiles);
                        }
                        else if(!isSec)
                        {
                            PopulateFiles(nodeCurrent, listView1);
                        }
                        else
                        {
                            PopulateFiles(nodeCurrent, lvFiles);
                        }

                        string[] stringDirectories = Directory.GetDirectories(getFullPath(nodeCurrent.FullPath));
                        string stringFullPath = "";
                        string stringPathName = "";

                        //loop throught all directories
                        foreach (string stringDir in stringDirectories)
                        {
                            stringFullPath = stringDir;
                            stringPathName = GetPathName(stringFullPath);

                            //create node for directories
                            nodeDir = new TreeNode(stringPathName.ToString(), imageIndex, selectIndex);
                            nodeCurrentCollection.Add(nodeDir);
                        }
                    }
                }
                catch (IOException e)
                {
                    MessageBox.Show("Error: Drive not ready or directory does not exist.");
                }
                catch (UnauthorizedAccessException e)
                {
                    MessageBox.Show("Error: Drive or directory access denided.");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e);
                }
            }
        }

        protected string getFullPath(string stringPath)
        {
            //Get Full path
            string stringParse = "";
            //remove My Computer from path.
            stringParse = stringPath.Replace("My Computer\\", "");

            return stringParse;
        }

        protected string GetPathName(string stringPath)
        {
            //Get Name of folder
            string[] stringSplit = stringPath.Split('\\');
            int _maxIndex = stringSplit.Length;
            return stringSplit[_maxIndex - 1];
        }

        protected void PopulateFiles(TreeNode nodeCurrent, ListView lvFiles)
        {
            //Populate listview with files
            string[] lvData = new string[4];

            //clear list
            InitListView(lvFiles);

            if (nodeCurrent.SelectedImageIndex != 0)
            {
                //check path
                if (Directory.Exists((string)getFullPath(nodeCurrent.FullPath)) == false)
                {
                    MessageBox.Show("Directory or path " + nodeCurrent.ToString() + " does not exist.");
                }
                else
                {
                    try
                    {
                        string[] stringFiles = Directory.GetFiles(getFullPath(nodeCurrent.FullPath));
                        string stringFileName = "";
                        DateTime dtCreateDate, dtModifyDate;
                        Int64 lFileSize = 0;

                        //loop throught all files
                        foreach (string stringFile in stringFiles)
                        {
                            stringFileName = stringFile;
                            FileInfo objFileSize = new FileInfo(stringFileName);
                            lFileSize = objFileSize.Length;
                            dtCreateDate = objFileSize.CreationTime; //GetCreationTime(stringFileName);
                            dtModifyDate = objFileSize.LastWriteTime; //GetLastWriteTime(stringFileName);

                            //create listview data
                            lvData[0] = GetPathName(stringFileName);
                            lvData[1] = formatSize(lFileSize);

                            //check if file is in local current day light saving time
                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dtCreateDate) == false)
                            {
                                //not in day light saving time adjust time
                                lvData[2] = formatDate(dtCreateDate.AddHours(1));
                            }
                            else
                            {
                                //is in day light saving time adjust time
                                lvData[2] = formatDate(dtCreateDate);
                            }

                            //check if file is in local current day light saving time
                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dtModifyDate) == false)
                            {
                                //not in day light saving time adjust time
                                lvData[3] = formatDate(dtModifyDate.AddHours(1));
                            }
                            else
                            {
                                //not in day light saving time adjust time
                                lvData[3] = formatDate(dtModifyDate);
                            }


                            //Create actual list item
                            ListViewItem lvItem = new ListViewItem(lvData, 0);
                            lvFiles.Items.Add(lvItem);


                        }
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show("Error: Drive not ready or directory does not exist.");
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        MessageBox.Show("Error: Drive or directory access denided.");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e);
                    }
                }
            }
        }

        protected string formatDate(DateTime dtDate)
        {
            //Get date and time in short format
            string stringDate = "";

            stringDate = dtDate.ToShortDateString().ToString() + " " + dtDate.ToShortTimeString().ToString();

            return stringDate;
        }

        protected string formatSize(Int64 lSize)
        {
            //Format number to KB
            string stringSize = "";
            NumberFormatInfo myNfi = new NumberFormatInfo();

            Int64 lKBSize = 0;

            if (lSize < 1024)
            {
                if (lSize == 0)
                {
                    //zero byte
                    stringSize = "0";
                }
                else
                {
                    //less than 1K but not zero byte
                    stringSize = "1";
                }
            }
            else
            {
                //convert to KB
                lKBSize = lSize / 1024;
                //format number with default format
                stringSize = lKBSize.ToString("n", myNfi);
                //remove decimal
                stringSize = stringSize.Replace(".00", "");
            }

            return stringSize + " KB";

        }

        private void lvFiles_SelectedIndexChanged(object sender, MouseEventArgs e)
        {
            string S2 = "My Computer\\";
            string S1;
            try
            {
                S1 = nodeCurrent.FullPath;
            }
            catch
            {
                S1 = nodeCurrent.Text;
            }

            int index = S1.IndexOf(S2);
            if (index != -1)
            {
                S1 = S1.Remove(index, S2.Length);
            }
            textBox1.Text = S1 + "\\" + lvFiles.SelectedItems[0].Text;
            listView1.SelectedItems.Clear();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string S2 = "My Computer\\";
            string S1;
            try
            {
                S1 = nodeCurrent.FullPath;
            }
            catch
            {
                S1 = nodeCurrent.Text;
            }

            int index = S1.IndexOf(S2);
            if (index != -1)
            {
                S1 = S1.Remove(index, S2.Length);
            }
            textBox1.Text = S1 + "\\" + listView1.SelectedItems[0].Text;
            listView1.SelectedItems.Clear();
        }

        private void переместитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = String.Empty; ;
            try
            {
                //Open browser dialog allows you to select the path
                using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите новое расположение" })
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        s = fbd.SelectedPath + "\\" + lvFiles.SelectedItems[0].Text; ; 
                        File.Move(textBox1.Text, s);
                        TreeViewEventArgs t = new TreeViewEventArgs(nodeCurrent);
                        tvFolders_AfterSelect(tvFolders, t);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Сначала выберите файл, имя которого ещё не используеться в конечном каталоге");
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = String.Empty;
            try
            {
                //Open browser dialog allows you to select the path
                using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите место для копирования" })
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        string S1 = textBox1.Text;
                        int index = S1.IndexOf("\\\\");
                        if (index != -1)
                        {
                            S1 = S1.Remove(index, 1);
                        }
                        s = fbd.SelectedPath + "\\" + lvFiles.SelectedItems[0].Text;
                        if (s == S1)
                        {
                            s = fbd.SelectedPath + "\\копия---" + lvFiles.SelectedItems[0].Text;
                            File.Copy(textBox1.Text, s);
                            TreeViewEventArgs t = new TreeViewEventArgs(nodeCurrent);
                            tvFolders_AfterSelect(tvFolders, t);
                        }
                        else
                        {
                            File.Copy(textBox1.Text, s);
                            TreeViewEventArgs t = new TreeViewEventArgs(nodeCurrent);
                            tvFolders_AfterSelect(tvFolders, t);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Сначала выберите файл");
            }
        }

        private void создатьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название новой папки", "Ввод", "Текст", 100, 100);
                Directory.CreateDirectory(textBox1.Text + "\\" + name);
                TreeViewEventArgs t = new TreeViewEventArgs(nodeCurrent);
                tvFolders_AfterSelect(tvFolders, t);
            }
            catch
            {
                MessageBox.Show("Папка не создана");
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            создатьToolStripMenuItem_Click(null, null);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            создатьПапкуToolStripMenuItem_Click(null, null);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            удалитьToolStripMenuItem_Click(null, null);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            переместитьToolStripMenuItem_Click(null, null);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            копироватьToolStripMenuItem_Click(null, null);
        }
    }
}
