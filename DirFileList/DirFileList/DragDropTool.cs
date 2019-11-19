using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SciTools
{
    /// <summary>
    /// 拖拽处理
    /// </summary>
    public class DragDropTool
    {
        /// <summary>
        /// DragEnter
        /// </summary>
        public static void Form_DragEnter(object sender, DragEventArgs e)
        {
            dragEnter(e);
        }

        /// <summary>
        /// DragDrop
        /// </summary>
        public static /*string[]*/ void Form_DragDrop(object sender, DragEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = dragDrop(e);                 // 获取拖入的文件
            //string[] files = textBox.Text.Split(';');
            // 其他处理逻辑

            //return files;
        }

        /// <summary>
        /// 获取files文件或目录列表下的所有文件信息
        /// </summary>
        public static string[] GetSubFiles(Array files)
        {
            string AllFiles = toSubDirFileNames(files);
            string[] SubFiles = AllFiles.Split(';');

            return SubFiles;
        }

        # region 文件拖拽

        /// <summary>  
        /// 文件拖进事件处理：  
        /// </summary>  
        private static void dragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件  
                e.Effect = DragDropEffects.Link;                //是则将拖动源中的数据连接到控件  
            else e.Effect = DragDropEffects.None;
        }

        /// <summary>  
        /// 文件放下事件处理：  
        /// 放下, 另外需设置对应控件的 AllowDrop = true;   
        /// 获取的文件名形如 "d:\1.txt;d:\2.txt"  
        /// </summary>  
        private static string dragDrop(DragEventArgs e)
        {
            Array file = (System.Array)e.Data.GetData(DataFormats.FileDrop);//将拖来的数据转化为数组存储
            //return toSubDirFileNames(file);
            return toFileName(file);
        }

        // 获取所有files目录下的所有文件，转化为单个串
        private static string toFileName(Array files)
        {
            foreach (object I in files)
            {
                string str = I.ToString();
                return str;
            }
            return "";
        }

        // 获取所有files目录下的所有文件，转化为单个串
        private static string toSubDirFileNames(Array files)
        {
            StringBuilder filesName = new StringBuilder("");

            foreach (object I in files)
            {
                string str = I.ToString();

                System.IO.FileInfo info = new System.IO.FileInfo(str);
                //若为目录，则获取目录下所有子文件名  
                if ((info.Attributes & System.IO.FileAttributes.Directory) != 0)
                {
                    str = getAllFiles(str);
                    if (!str.Equals("")) filesName.Append((filesName.Length == 0 ? "" : ";") + str);
                }
                //若为文件，则获取文件名  
                else if (System.IO.File.Exists(str))
                    filesName.Append((filesName.Length == 0 ? "" : ";") + str);
            }

            return filesName.ToString();
        }

        /// <summary>  
        /// 判断path是否为目录  
        /// </summary>  
        private static bool IsDirectory(String path)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            return (info.Attributes & System.IO.FileAttributes.Directory) != 0;
        }

        /// <summary>  
        /// 获取目录path下所有子文件名  
        /// </summary>  
        private static string getAllFiles(String path)
        {
            StringBuilder str = new StringBuilder("");
            if (System.IO.Directory.Exists(path))
            {
                //所有子文件名  
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                    str.Append((str.Length == 0 ? "" : ";") + file);

                //所有子目录名  
                string[] Dirs = System.IO.Directory.GetDirectories(path);
                foreach (string dir in Dirs)
                {
                    string tmp = getAllFiles(dir);  //子目录下所有子文件名  
                    if (!tmp.Equals("")) str.Append((str.Length == 0 ? "" : ";") + tmp);
                }
            }
            return str.ToString();
        }

        # endregion
    }
}