using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace AimpFlyPlugin
{
    public class WSResponseHelper
    {
        private HttpListenerResponse response;

        public WSResponseHelper(ref HttpListenerResponse response)
        {
            this.response = response;
            OutputStream = response.OutputStream;

        }

        public Stream OutputStream { get; set; }

        public class FileObject
        {
            public FileStream fs;
            public byte[] buffer;
        }

        public void WriteToClient(FileStream fs, string fileType)
        {
            if (fileType.Equals("js"))
            {
                response.AddHeader("Content-Type", "application/javascript");
            }
            response.StatusCode = 200;
            byte[] buffer = new byte[1024];
            FileObject obj = new FileObject() { fs = fs, buffer = buffer };
            fs.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(EndWrite), obj);
        }

        void EndWrite(IAsyncResult ar)
        {
            var obj = ar.AsyncState as FileObject;
            var num = obj.fs.EndRead(ar);
            OutputStream.Write(obj.buffer, 0, num);
            if (num < 1)
            {
                obj.fs.Close(); //关闭文件流
                OutputStream.Close();//关闭输出流，如果不关闭，浏览器将一直在等待状态
                return;
            }
            obj.fs.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(EndWrite), obj);
        }

        public void WriteJsonToClient(String jsonStr)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(jsonStr);

            response.StatusCode = 200;
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.AddHeader("Access-Control-Allow-Origin", "*");
            response.AddHeader("Access-Control-Allow-Methods", "POST,PUT,DELETE,GET");
            response.AddHeader("Access-Control-Allow-Headers", "*");
            response.AddHeader("Access-Control-Max-Age", "1728000");
            /*
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            */
            response.WriteContent(buffer);
            response.Close();
        }

         
        public void ReturnError(int errCode)
        {
            response.StatusCode = errCode;
            //response.ContentLength64 = buffer.Length;
            //response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
            
        }

        public void Return202()
        {
            //response.ContentType = "applicaiton/json";
            response.AddHeader("Access-Control-Allow-Origin", "*");
            response.AddHeader("Access-Control-Allow-Methods", "POST,PUT,DELETE,GET");
            response.AddHeader("Access-Control-Allow-Headers", "*");
            response.AddHeader("Access-Control-Max-Age", "1728000");
            //byte[] buffer = Encoding.UTF8.GetBytes(responsetext);
            //response.StatusCode = 202;
            //response.ContentLength64 = buffer.Length;
            //response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();

        }
    }
}