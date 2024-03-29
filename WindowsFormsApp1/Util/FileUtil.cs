﻿using System;
using System.IO;
using System.Text;

namespace FooflyProxy
{
    public class FileUtil
    {

        public static String getFilePath(String filename)
        {
            return Path.GetDirectoryName(filename);
        }

        public static String getExtension(String filename)
        {
            return Path.GetExtension(filename);
        }

        public static byte[] getCoverImg(String fileUrl)
        {
            try
            {
                String imgFileUrl = Path.GetDirectoryName(fileUrl) + "\\cover.jpg";
                if(!File.Exists(imgFileUrl)){
                    imgFileUrl = Path.GetDirectoryName(fileUrl) + "\\folder.jpg";
                }
                if (!File.Exists(imgFileUrl))
                {
                    return null;
                }
                FileStream fs = new FileStream(imgFileUrl, FileMode.Open, FileAccess.Read);
                byte[] contents = new byte[fs.Length];
                fs.Read(contents, 0, contents.Length);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Close();
                return contents;
            }
            catch (Exception ex)
            {
                LogUtil.write(ex.Message);
                return null;
            }
        }

        public static byte[] getDefaultCoverImg()
        {
            try
            {
                string wwwPath = Properties.Settings.Default.wwwRoot + @"\foofly";

                String imgFileUrl = wwwPath + @"\assets\img\cover.jpg";

                if (!File.Exists(imgFileUrl))
                {
                    return null;
                }
                FileStream fs = new FileStream(imgFileUrl, FileMode.Open, FileAccess.Read);
                byte[] contents = new byte[fs.Length];
                fs.Read(contents, 0, contents.Length);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Close();
                return contents;
            }
            catch (Exception ex)
            {
                LogUtil.write(ex.Message);
                return null;
            }
        }

        public static String getFolder(String filename)
        {
            String path = Path.GetDirectoryName(filename);
            int idx = path.LastIndexOf("\\");

            return path.Substring(idx+1);
        }

        public static String findCueFile(String filename)
        {
            String path = Path.GetDirectoryName(filename);
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                String fname = NextFile.FullName;
                if (Path.GetExtension(fname).ToLower().Equals(".cue"))
                {
                    return fname;
                }
            }
            return null;
        }

        /// <summary>
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型
        /// </summary>
        /// <param name="FILE_NAME">文件路径</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            Encoding r = GetType(fs);
            fs.Close();
            return r;
        }


        /// <summary>
        /// 通过给定的文件流，判断文件的编码类型
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
            Encoding reVal = Encoding.Default;


            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;


        }


        /// <summary>
        /// 判断是否是不带 BOM 的 UTF8 格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }


        public static string ByteToHex(byte[] bytes)
        {
            var count = bytes.Length;
            StringBuilder strBuider = new StringBuilder();
            for (int index = 0; index < count; index++)
            {
                strBuider.Append(((int)bytes[index]).ToString("X2"));
            }
            return strBuider.ToString();
        }

        public static byte[] HexToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
    }
}

   