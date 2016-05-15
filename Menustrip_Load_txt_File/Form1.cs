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

namespace Menustrip_Load_txt_File
{
    public partial class Form1 : Form
    {
        string filepath;
        FileInfo info;
        bool status = false; // false => load true => save

        public Form1()
        {
            InitializeComponent();

            ToolStripMenuItem MenuFile = new ToolStripMenuItem("檔案(&F)");
            ToolStripMenuItem MenuClear = new ToolStripMenuItem("清空(&N)");
            ToolStripMenuItem MenuSave = new ToolStripMenuItem("儲存(&S)");
            ToolStripMenuItem MenuLoad = new ToolStripMenuItem("讀取檔案");
            ToolStripMenuItem MenuExit = new ToolStripMenuItem("結束(&X)");
            ToolStripMenuItem MenuFont = new ToolStripMenuItem("字體");
            ToolStripMenuItem MenuSize = new ToolStripMenuItem("大小");
            ToolStripMenuItem MenuSize10 = new ToolStripMenuItem("10");
            ToolStripMenuItem MenuSize20 = new ToolStripMenuItem("20");
            ToolStripMenuItem MenuSize30 = new ToolStripMenuItem("30");
            ToolStripMenuItem MenuColor = new ToolStripMenuItem("顏色");
            ToolStripMenuItem MenuColorY = new ToolStripMenuItem("黃");
            ToolStripMenuItem MenuColorG = new ToolStripMenuItem("綠");
            ToolStripMenuItem MenuColorR = new ToolStripMenuItem("紅");

            MenuFile.ShortcutKeys = Keys.Alt | Keys.F;
            MenuClear.ShortcutKeys = Keys.Control | Keys.N;
            MenuSave.ShortcutKeys = Keys.Control | Keys.S;
            MenuLoad.ShortcutKeys = Keys.Control | Keys.O;
            MenuExit.ShortcutKeys = Keys.Control | Keys.E;

            MenuFile.DropDownItems.AddRange(new ToolStripItem[] { MenuClear, MenuSave, MenuLoad, MenuExit });
            MenuSize.DropDownItems.AddRange(new ToolStripItem[] { MenuSize10, MenuSize20, MenuSize30 });
            MenuColor.DropDownItems.AddRange(new ToolStripItem[] { MenuColorY, MenuColorG, MenuColorR });
            MenuFont.DropDownItems.AddRange(new ToolStripItem[] { MenuSize, MenuColor });

            menuStrip1.Items.Add(MenuFile);
            menuStrip1.Items.Add(MenuFont);

            MenuClear.Click += MenuClear_Click;
            MenuSave.Click += MenuSave_Click;
            MenuLoad.Click += MenuLoad_Click;
            MenuExit.Click += MenuExit_Click;

            MenuSize10.Click += MenuSize10_Click;
            MenuSize20.Click += MenuSize20_Click;
            MenuSize30.Click += MenuSize30_Click;

            MenuColorY.Click += MenuColorY_Click;
            MenuColorG.Click += MenuColorG_Click;
            MenuColorR.Click += MenuColorR_Click;

            progressBar1.Step = 10;
            toolStripProgressBar1.Step = 10;
            toolStripStatusLabel1.Text = "";

            EventArgs e = new EventArgs();
            Form1_Load(this, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filepath = Application.StartupPath + @"\Load.txt";

            info = new FileInfo(filepath);

            //try?
            if (info.Exists)
            {
                StreamReader sr = new StreamReader(filepath);
                textBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                FileStream fs = info.Create();
                fs.Close();
                MessageBox.Show("沒有檔案!!新增一個空的Load檔案!!");
            }

            textBox1.Text ="";
        }

        private void MenuClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            status = true;
            timer1.Enabled = true;
            StreamWriter sw = info.CreateText();
            sw.Write(textBox1.Text);
            sw.Flush();
            sw.Close();
        }

        private void MenuLoad_Click(object sender, EventArgs e)
        {
            status = false;
            timer1.Enabled = true;
            StreamReader sr = new StreamReader(filepath);
            textBox1.Text = sr.ReadToEnd();
            sr.Close();
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuSize10_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font("新細明體", 10.0f);
        }

        private void MenuSize20_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font("新細明體", 20.0f);
        }

        private void MenuSize30_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font("新細明體", 30.0f);
        }

        private void MenuColorY_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Yellow;
        }

        private void MenuColorG_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Green;
        }

        private void MenuColorR_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            if(progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
                timer1.Enabled = false;
            }

            toolStripProgressBar1.PerformStep();
            
            toolStripStatusLabel1.Text = status ? "儲存中" + ((toolStripProgressBar1.Value / toolStripProgressBar1.Maximum) * 100).ToString() + "%"
                                                : "讀取中" + ((toolStripProgressBar1.Value / toolStripProgressBar1.Maximum) * 100).ToString() + "%";
            
            if(toolStripProgressBar1.Value == toolStripProgressBar1.Maximum)
            {
                toolStripProgressBar1.Value = 0;
                toolStripStatusLabel1.Text = status ? "儲存完畢" : "讀取完畢"  ;
                timer1.Enabled = false;
            }
        }
    }
}
