using System;
using System.IO;
using System.Net;

namespace FooflyProxy
{
    public class FooHttpClient
    {

        //private string foofly = "/foofly";
        private string foofly = "";

        public FooHttpClient()
        {

            try
            {
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public string HttpGet(string url)
        {
            int httpcontrolPort = Properties.Settings.Default.httpcontrolPort;
            url = "http://127.0.0.1:" + httpcontrolPort + this.foofly + url;
            LogUtil.write("foo httpcontorl url=" + url);
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        result = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.write(ex.Message);
            }
            LogUtil.write(result);
            return result;
        }


        public byte[] HttpGetBytes(string url)
        {
            byte[] bytes = null; 
            int httpcontrolPort = Properties.Settings.Default.httpcontrolPort;
            url = "http://127.0.0.1:" + httpcontrolPort + this.foofly + url;
            LogUtil.write("foo httpcontorl url=" + url);
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    //bytes = new byte[responseStream.Length];
                    //responseStream.Read(bytes, 0, bytes.Length);
                    // 设置当前流的位置为流的开始
                    //responseStream.Seek(0, SeekOrigin.Begin);

                    var memoryStream = new MemoryStream();
                    //将基础流写入内存流
                    const int bufferLength = 1024;
                    byte[] buffer = new byte[bufferLength];
                    int actual = responseStream.Read(buffer, 0, bufferLength);
                    while (actual > 0)
                    {
                        memoryStream.Write(buffer, 0, actual);
                        actual = responseStream.Read(buffer, 0, bufferLength);
                    }
                    memoryStream.Position = 0;
      
                    bytes = new byte[memoryStream.Length];
                    memoryStream.Read(bytes, 0, bytes.Length);
                    // 设置当前流的位置为流的开始
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
            }
            catch (Exception ex)
            {
                LogUtil.write(ex.Message);
            }
            return bytes;
        }

    }
}