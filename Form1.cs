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

        public Form1()
        {
            InitializeComponent();
            светлаяToolStripMenuItem_Click(null, null);
            скрытьВторойЭкранToolStripMenuItem_Click(null, null);
            this.webBrowser1.Url = new Uri("C:\\");
            this.webBrowser2.Url = new Uri("C:\\");
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void тёмнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new TestColorTable());
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.BackColor = this.BackColor;
            this.menuStrip1.ForeColor = this.ForeColor;
            this.label1.ForeColor = this.ForeColor;
            this.label2.ForeColor = this.ForeColor;
            this.button1.ForeColor = this.ForeColor;
            this.button1.BackColor = this.BackColor;
            this.button2.ForeColor = this.ForeColor;
            this.button2.BackColor = this.BackColor;
            this.button3.ForeColor = this.ForeColor;
            this.button3.BackColor = this.BackColor;
            this.button4.ForeColor = this.ForeColor;
            this.button4.BackColor = this.BackColor;
            this.button5.ForeColor = this.ForeColor;
            this.button5.BackColor = this.BackColor;
            this.button6.ForeColor = this.ForeColor;
            this.button6.BackColor = this.BackColor;
        }

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ForeColor = System.Drawing.Color.Black;
            this.menuStrip1.BackColor = this.BackColor;
            this.menuStrip1.ForeColor = this.ForeColor;
            this.label1.ForeColor = this.ForeColor;
            this.label2.ForeColor = this.ForeColor;
            this.button1.ForeColor = this.ForeColor;
            this.button1.BackColor = this.BackColor;
            this.button2.ForeColor = this.ForeColor;
            this.button2.BackColor = this.BackColor;
            this.button3.ForeColor = this.ForeColor;
            this.button3.BackColor = this.BackColor;
            this.button4.ForeColor = this.ForeColor;
            this.button4.BackColor = this.BackColor;
            this.button5.ForeColor = this.ForeColor;
            this.button5.BackColor = this.BackColor;
            this.button6.ForeColor = this.ForeColor;
            this.button6.BackColor = this.BackColor;
        }

        private void видToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string MyString = webBrowser1.Url.ToString();
            char[] MyChar = {'f','i','l','e',':','/','/','/'};
            string NewString = MyString.TrimStart(MyChar);
            textBox1.Text = NewString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
            {
                webBrowser1.GoBack();
                textBox1.Text = webBrowser1.Url.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
            {
                webBrowser1.GoForward();
                textBox1.Text = webBrowser1.Url.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (webBrowser2.CanGoBack)
            {
                webBrowser2.GoBack();
                textBox2.Text = webBrowser2.Url.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (webBrowser2.CanGoForward)
            {
                webBrowser2.GoForward();
                textBox2.Text = webBrowser2.Url.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Open browser dialog allows you to select the path
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                fbd.ShowNewFolderButton = false;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.webBrowser1.Url = new Uri(fbd.SelectedPath);
                    textBox1.Text = webBrowser1.Url.ToString();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Open browser dialog allows you to select the path
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                fbd.ShowNewFolderButton = false;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.webBrowser2.Url = new Uri(fbd.SelectedPath);
                    textBox2.Text = webBrowser2.Url.ToString();
                }
            }
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string MyString = webBrowser2.Url.ToString();
            char[] MyChar = { 'f', 'i', 'l', 'e', ':', '/', '/', '/' };
            string NewString = MyString.TrimStart(MyChar);
            textBox1.Text = NewString;
        }

        bool isHide = false;

        private void скрытьВторойЭкранToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button4.Visible = false;
            button6.Visible = false;
            textBox2.Visible = false;
            label2.Visible = false;
            webBrowser2.Visible = false;
            if (!isHide)
            {
                this.Width = this.Width / 2;
            }
            isHide = true;
        }

        private void второйЭкранToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button4.Visible = true;
            button6.Visible = true;
            textBox2.Visible = true;
            label2.Visible = true;
            webBrowser2.Visible = true;
            if (isHide)
            {
                this.Width = this.Width * 2;
            }
            isHide = false;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
