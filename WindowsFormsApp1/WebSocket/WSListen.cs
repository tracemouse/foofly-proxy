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
        private PCControl pcControl;
        private FooHttpClient fooHttpClient;

        /// <summary>
        /// 跨域参数设置
        /// </summary>
        public string corsOptions { get; set; }

        public WSListen(int _port)
        {
            port = _port;
            this.pcControl = new PCControl();
            this.fooHttpClient = new FooHttpClient();
        }

        /// <summary>
        /// 开启监听
        /// </summary>
        public void start()
        {

            httpsv = new HttpServer(System.Net.IPAddress.Any, port);
            //httpsv = new HttpServer(System.Net.IPAddress.Any, port,true);
            //httpsv.Log.Level = LogLevel.Trace;

            //String wwwPath = string.Format(@"{0}/Plugins/aimp_fly/www", Application.StartupPath);
            String wwwPath = "D:\\Tools\\Player\\httpcontrol\\foofly";

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
                res.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                res.AddHeader("Pragma", "no-cache");
                res.AddHeader("Expires", "0");
                res.AddHeader("Access-Control-Allow-Origin", "*");
                res.AddHeader("Access-Control-Allow-Methods", "POST,PUT,DELETE,GET");
                res.AddHeader("Access-Control-Allow-Headers", "*");
                res.AddHeader("Access-Control-Max-Age", "1728000");

                var path = req.RawUrl;

                byte[] contents;
                string result = "{\"isSucc\":false}";

                LogUtil.write("rawUrl=" + req.RawUrl);

                
                if (path.StartsWith("/foofly/"))
                {
                    res.ContentType = "text/html";
                    res.ContentEncoding = Encoding.UTF8;

                    string queryString = req.Url.Query;
                    NameValueCollection col = GetQueryString(queryString, Encoding.UTF8, true);
                    string cmd = col["cmd"];
                    LogUtil.write("cmd=" + cmd);

                    switch (cmd)
                    {
                        case null:
                            if (path.EndsWith(".jpg"))
                            {
                                res.ContentType = "image/jpeg";
                            }
                            //result = this.fooHttpClient.HttpGet(req.RawUrl);
                            //contents = result.GetUTF8EncodedBytes();
                            contents = this.fooHttpClient.HttpGetBytes(req.RawUrl);
                            System.Text.UTF8Encoding utf8 = new UTF8Encoding();
                            //LogUtil.write(utf8.GetString(contents));
                            break;
                        case "closeScreen":
                            try
                            {
                                result = this.pcControl.closeScreen();
                            }
                            catch (Exception ex) { }
                            contents = result.GetUTF8EncodedBytes();
                            break;
                        case "shutdown":
                            try
                            {
                                string param1 = col["param1"];
                                int minutes = int.Parse(param1);
                                result = this.pcControl.shutdown(minutes);
                            }
                            catch (Exception ex) { }
                            contents = result.GetUTF8EncodedBytes();
                            break;
                        default:
                            result = this.fooHttpClient.HttpGet(req.RawUrl);
                            contents = result.GetUTF8EncodedBytes();
                            break;
                    }
                    res.WriteContent(contents);
                    return;
                }
                
                if (path == "/")
                    path += "index.html";

                if (path.EndsWith(".html"))
                {
                    res.ContentType = "text/html";
                    res.ContentEncoding = Encoding.UTF8;
                }
                else if (path.EndsWith(".js"))
                {
                    res.ContentType = "application/javascript";
                    res.ContentEncoding = Encoding.UTF8;
                }

                if (!e.TryReadFile(path, out contents))
                {
                    res.StatusCode = (int)WebSocketSharp.Net.HttpStatusCode.NotFound;
                    return;
                }


                res.WriteContent(contents);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                LogUtil.write(ex.Message);
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
