using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirFileList
{
    public partial class Form1 : Form
    {
        String tipInfo = "请拖动文件夹至此！";
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = tipInfo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(tipInfo) && !textBox1.Text.Equals(""))
            {
                String dir = textBox1.Text.Trim();
                bool containsDir = checkBox1.Checked;
                bool containsFile = checkBox2.Checked;

                textBox2.Text = getSubFiles(dir, containsDir, containsFile);
            }
        }

        /// <summary>
        /// 获取dir目录下所有文件或目录的名称
        /// </summary>
        private String getSubFiles(String dir, bool containsDir = true, bool containsFile = false)
        {
            StringBuilder str = new StringBuilder();
            if(Directory.Exists(dir))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                if (containsDir)
                {
                    DirectoryInfo[] dirs = dirInfo.GetDirectories();
                    foreach (DirectoryInfo info in dirs)
                    {
                        str.AppendLine(info.Name);
                    }
                }

                if(containsFile)
                {
                    FileInfo[] files = dirInfo.GetFiles();
                    foreach(FileInfo info in files)
                    {
                        str.AppendLine(info.Name);
                    }
                }
            }
            return str.ToString();
        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            SciTools.DragDropTool.Form_DragEnter(sender, e);
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            SciTools.DragDropTool.Form_DragDrop(sender, e);
        }
    }
}
