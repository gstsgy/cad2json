using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Dwg2Json.Util
{
    /// <summary>
    /// 文件读写工具类
    /// </summary>
    public sealed class FileUtil
    {
        private FileUtil()
        {
        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="fileAllPath">文件全路径名</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ReadFile(string fileAllPath, Encoding encoding)
        {
            string content = "";
            StreamReader sr = null;
            try
            {
                if (encoding == null)
                {
                    encoding = Encoding.Default;
                }
                sr = new StreamReader(fileAllPath, encoding);
                string lineStr;
                while ((lineStr = sr.ReadLine()) != null)
                {
                    content = content + lineStr.Trim() + "\n";
                }
            }
            catch (Exception ee)
            {

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            return content;
        }
        public static void DeleFile(string filePath)
        {
            //判断文件是不是存在
            if (File.Exists(filePath))
            {
                //如果存在则删除
                File.Delete(filePath);

            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="fileAllPath">文件全路径名</param>
        /// <returns></returns>
        public static string ReadFile(string fileAllPath)
        {
            return ReadFile(fileAllPath, Encoding.Default);
        }

        /// <summary>
        /// 按行读取文件
        /// </summary>
        /// <param name="fileAllPath"></param>
        /// <param name="encoding"></param>
        /// <param name="nStartRowNo">开始行号，从1开始</param>
        /// <param name="nRowNum">读取的行数</param>
        /// <returns></returns>
        public static List<string> ReadFile(string fileAllPath, Encoding encoding, int nStartRowNo, int nRowNum)
        {
            var content = new List<string>();
            StreamReader sr = null;
            try
            {
                if (encoding == null)
                {
                    encoding = Encoding.Default;
                }
                sr = new StreamReader(fileAllPath, encoding);
                string lineStr;
                int nBegin = 1;
                int nCount = 0;
                while ((lineStr = sr.ReadLine()) != null)
                {
                    if (nBegin >= nStartRowNo && nCount < nRowNum)
                    {
                        content.Add(lineStr.Trim());
                        nCount++;
                    }
                    else if (nCount == nRowNum)
                    {
                        break;
                    }
                    nBegin++;
                }
            }
            catch (Exception ee)
            {

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            return content;
        }


        /// <summary>
        /// 字符串写入文件
        /// </summary>
        /// <param name="fileAllPath">文件全路径名</param>
        /// <param name="data">要写入的字符</param>
        /// <param name="encoding">编码</param>
        /// <param name="fileMode">模式</param>
        public static string WriteFile(string fileAllPath, string data, Encoding encoding, FileMode fileMode)
        {
            string errorMsg = "";
            FileStream fs = null;
            try
            {
                if (encoding == null)
                {
                    encoding = Encoding.Default;
                }
                //这里的FileMode.create是创建这个文件,如果文件名存在则覆盖重新创建
                fs = new FileStream(fileAllPath, fileMode);
                //存储时时二进制,所以这里需要把我们的字符串转成二进制
                byte[] bytes = encoding.GetBytes(data);
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ee)
            {
                errorMsg = ee.Message;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return errorMsg;
        }
        /// <summary>
        /// 获取指定文件下同一类型的文件
        /// </summary>
        /// <param name="fileAllPath">文件夹路径</param>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public static string[] ReadFileALL(string fileAllPath, string data)
        {
            var files = Directory.GetFiles(fileAllPath, data);
            int len = files.Length;
            if (len == 0)
            {
                return null;
            }
            string[] filesstr = new string[len];
            for (int i = 0; i < len; i++)
            {
                filesstr[i] = files[i].ToString();
            }
            return filesstr;
        }
        /// <summary>
        /// 删除指定文件夹下所有文件
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)
                    {            //判断是否文件夹
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

