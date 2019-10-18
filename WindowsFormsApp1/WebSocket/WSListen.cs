using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace FooflyProxy
{
    public class WSListen
    {

   
        public HttpServer httpsv;
        private int port;

        /// <summary>
        /// 跨域参数设置
        /// </summary>
        public string corsOptions { get; set; }

        public WSListen(int _port)
        {
            port = _port;
        }

        /// <summary>
        /// 开启监听
        /// </summary>
        public void start()
        {
            httpsv = new HttpServer(System.Net.IPAddress.Any, port);
            //httpsv = new HttpServer(System.Net.IPAddress.Any, port,true);
            //httpsv.Log.Level = LogLevel.Trace;

            String wwwPath = string.Format(@"{0}/Plugins/aimp_fly/www", Application.StartupPath);

            httpsv.DocumentRootPath = wwwPath;

            httpsv.OnGet += Httpsv_OnGet;
            httpsv.OnPost += Httpsv_OnPost;
            httpsv.OnOptions += Httpsv_OnOptions;

            //register websocket
            //httpsv.WebSocketServices.Add("/wsjsonrpc", () => new WSJsonrpc(ref mbApi));
           // httpsv.WebSocketServices.Add("/wsplaying", () => new WSPlaying(ref mbApi));
            //httpsv.AddWebSocketService("/wsjsonrpc", () => new WSJsonrpc(ref mbApi));
            //httpsv.AddWebSocketService("/wsplaying", () => new WSPlaying(ref mbApi));

            httpsv.Start();
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void stop()
        {
            httpsv.OnGet -= Httpsv_OnGet;
            httpsv.OnPost -= Httpsv_OnPost;
            httpsv.OnOptions -= Httpsv_OnOptions;
            httpsv.Stop();
        }

        /// <summary>
        /// 跨域请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Httpsv_OnOptions(object sender, HttpRequestEventArgs e)
        {
            e.Response.AddHeader("Access-Control-Allow-Origin", "*");
            e.Response.AddHeader("Access-Control-Allow-Methods", "POST,PUT,DELETE,GET");
            e.Response.AddHeader("Access-Control-Allow-Headers", "*");
            e.Response.AddHeader("Access-Control-Max-Age", "1728000");
            e.Response.Close();
        }

        private void Httpsv_OnPost(object sender, HttpRequestEventArgs e)
        {

            string logResult = string.Empty;
            try
            {

                var req = e.Request;
                var res = e.Response;
                //WSRequestHelper requestHelper = new WSRequestHelper(ref req, ref res, ref mbApi);
                //requestHelper.DispatchResources();

                res.Close();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Httpsv_OnGet(object sender, HttpRequestEventArgs e)
        {
            var req = e.Request;
            var res = e.Response;
            try
            {
                //set cache control
                e.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                e.Response.AddHeader("Pragma", "no-cache");
                e.Response.AddHeader("Expires", "0");

                var path = req.RawUrl;
                if (path == "/")
                    path += "index.html";

                byte[] contents;

                char[] urlsplit = { '?' };
                String[] urlArr = path.Split(urlsplit);
                String filename = urlArr[0];
                if (filename.Equals("/getArtwork"))
                {
                    string queryString = req.Url.Query;
                    NameValueCollection col = GetQueryString(queryString,Encoding.UTF8, true);
                    string fileUrl = col["fileUrl"];
                    string base64 = null;
                    if(fileUrl == null || fileUrl == "")
                    {
                        //base64 = mbApi.NowPlaying_GetArtwork();
                    }
                    else
                    {
                       // base64 = mbApi.Library_GetArtwork(fileUrl, 0);
                    }
                    if(base64 == null || base64 == "")
                    {
                        base64 = "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";
                    }
                    //string img = "data:image/jpeg;base64," + base64;
                    //res.ContentType = "image/jpeg; charset=UTF-8";
                    //res.ContentEncoding = Encoding.UTF8;
                    //contents = Encoding.UTF8.GetBytes(img);
                    contents = Convert.FromBase64String(base64);
                    res.WriteContent(contents);
                    //MessageBox.Show("fileUrl=" + fileUrl);
                    return;
                }
 
                if (!e.TryReadFile(path, out contents))
                {
                    res.StatusCode = (int)WebSocketSharp.Net.HttpStatusCode.NotFound;
                    return;
                }

                if (path.EndsWith(".html"))
                {
                    res.ContentType = "text/html";
                    res.ContentEncoding = Encoding.UTF8;
                }
                else if (path.EndsWith(".js"))
                {
                    res.ContentType = "application/javascript";
                    res.ContentEncoding = Encoding.UTF8;
                }else if (path.EndsWith(".svg"))
                {
                    res.ContentType = "image/svg+xml";
                }

                res.WriteContent(contents);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                res.Close();
            }

        }

        ///   <summary> 
        ///  将查询字符串解析转换为名值集合.
        ///   </summary> 
        ///   <param name="queryString"></param> 
        ///   <returns></returns> 
        public static NameValueCollection GetQueryString(string queryString)
        {
            return GetQueryString(queryString, null, true);
        }

        ///   <summary> 
        ///  将查询字符串解析转换为名值集合.
        ///   </summary> 
        ///   <param name="queryString"></param> 
        ///   <param name="encoding"></param> 
        ///   <param name="isEncoded"></param> 
        ///   <returns></returns> 
        public static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
        {
            queryString = queryString.Replace("?", "");
            NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }
                    string key = null;
                    string value = null;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    if (isEncoded)
                    {
                        result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
                    }
                    else
                    {
                        result[key] = value;
                    }
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return result;
        }

        ///   <summary> 
        ///  解码URL.
        ///   </summary> 
        ///   <param name="encoding"> null为自动选择编码 </param> 
        ///   <param name="str"></param> 
        ///   <returns></returns> 
        public static string MyUrlDeCode(string str, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                // 首先用utf-8进行解码                      
                string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
                // 将已经解码的字符再次进行编码. 
                string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
                if (str == encode)
                    encoding = Encoding.UTF8;
                else
                    encoding = Encoding.GetEncoding("gb2312");
            }
            return HttpUtility.UrlDecode(str, encoding);
        }

    }
}
