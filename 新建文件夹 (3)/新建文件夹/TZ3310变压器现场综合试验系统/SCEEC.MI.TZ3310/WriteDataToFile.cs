using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public enum Mode
    {
        Create = 0, Delete = 1
    }

    public enum MyFileMode
    {
        Create = 0, Delete = 1, Clear = 2
    }
    public static class WriteDataToFile
    {
        /// <summary>
        /// 删除或者创建文件夹，删除所有文件（对文件夹操作）
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="mode"></param>
        public static void DeelDirectoryInfo(string Path,Mode mode)//"D:\Mytask"
        {
            if(mode==Mode.Create)
            {
                try
                {
                    //System.IO.FileInfo fi = new System.IO.FileInfo(Path);
                    //fi.Create().Dispose();
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Path);
                    di.Create();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
               
            }
            if(mode==Mode.Delete)
            {
                try
                {
                    if (File.Exists(Path))
                    {
                        Directory.Delete(Path, true);
                    }
                }
                catch(Exception)
                {
                    throw new Exception("需要删除的文件不是目录");
                }
                    
            }
           
        }
        /// <summary>
        /// 对文件操作
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mode"></param>
        private static void DeelFileLis(string path,Mode mode)
        {
            if (mode == Mode.Create)
            {
                    if(!File.Exists(path))
                    {
                        //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                        //di.Create();
                        System.IO.File.Create(path).Dispose();
                    }
            }
            if (mode == Mode.Delete)
            {
                    if (File.Exists(path))
                    {
                         System.IO.File.Delete(path);
                    }
            }
        }
        public static void FileBaseDeel(string path, MyFileMode fileMode)
        {
            if (fileMode == MyFileMode.Create)
                DeelFileLis(path, Mode.Create);
            if (fileMode == MyFileMode.Delete)
                DeelFileLis(path, Mode.Delete);
            if (fileMode == MyFileMode.Clear)
            {
                DeelFileLis(path, Mode.Delete);
                DeelFileLis(path, Mode.Create);
            }
        }
        /// <summary>
        /// 末尾
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Content"></param>
        public static void WriteFile(string path, string Content)
        {
            FileStream fs = null;
            string filePath = path;
            //将待写的入数据从字符串转换为字节数组  
            Encoding encoder = Encoding.UTF8;
            byte[] bytes = encoder.GetBytes(Content);
          
                // fs = File.Open(filePath, FileMode.Append, FileAccess.ReadWrite);
                fs = File.OpenWrite(filePath);
                //设定书写的開始位置为文件的末尾  
                fs.Position = fs.Length;
                //将待写入内容追加到文件末尾
                fs.Write(bytes, 0, bytes.Length);
                  fs.Dispose();
                //fs.Flush();
                //fs.Close();
        }
        /// <summary>
        /// 指定位置插入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Content"></param>
        /// <param name="position">为0开头添加</param>
        public static void WriteFile(string path, string Content,int position)
        {
            FileStream fs = null;
            string filePath = path;
            //将待写的入数据从字符串转换为字节数组  
            Encoding encoder = Encoding.UTF8;
            byte[] bytes = encoder.GetBytes(Content);
            try
            {
                fs = File.OpenWrite(filePath);
                fs.Position = position;
                fs.Write(bytes, 0, bytes.Length);
                fs.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine("文件打开失败{0}", ex.ToString());
            }
            finally
            {
                fs.Dispose();
                fs.Close();
            }
        }
        public static string ReadFile(string path)
        {
            if(File.Exists(path))
            {
                
                return System.IO.File.ReadAllText(path);
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 替换文件指定位置的指定字符串，替换和UpadataText长度相同的数据
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="StartPosition">开始位置</param>
        /// <param name="UpadataText">需要更新的数据</param>
        public static void UpadataStringOfFile(string FilePath,int StartPosition,string UpadataText)
        {
            if(File.Exists(FilePath))
            {
               
                string FileBuffer = ReadFile(FilePath);
                if(FileBuffer.Length>=UpadataText.Length)
                {
                    char[] charTemp = FileBuffer.ToCharArray();
                    string NeedUpdata = new string(charTemp.Skip(StartPosition).Take(UpadataText.Length).ToArray());
                    StringBuilder sb = new StringBuilder(FileBuffer);
                    sb.Replace(NeedUpdata, UpadataText, StartPosition, UpadataText.Length);
                    FileBuffer = sb.ToString();
                    FileBaseDeel(FilePath, MyFileMode.Clear);
                    WriteFile(FilePath, FileBuffer);
                }
              

            }
        }

    }
} 
