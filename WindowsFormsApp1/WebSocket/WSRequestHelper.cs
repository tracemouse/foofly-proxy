using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WebSocketSharp.Net;

namespace AimpFlyPlugin
{
    public class WSRequestHelper
    {
        private AimpInterface mbApi;

        private HttpListenerRequest request;
        private HttpListenerResponse response;
        private WSResponseHelper responseHelper;

        public WSRequestHelper(ref HttpListenerRequest request, ref HttpListenerResponse response, ref AimpInterface mbApi)
        {
            this.mbApi = mbApi;

            this.request = request;
            this.response = response;
            this.responseHelper = new WSResponseHelper(ref response); 
        }

        public Stream RequestStream { get; set; }

        public void ExtracHeader()
        {
            RequestStream = request.InputStream;
        }

        public delegate void ExecutingDispatch(FileStream fs);

        //public void DispatchResources(ExecutingDispatch action)
        public void DispatchResources()
        {

            String rawUrl = request.RawUrl;//资源默认放在执行程序的wwwroot文件下，默认文档为index.html

            rawUrl = System.Web.HttpUtility.UrlDecode(rawUrl);

            char[] urlsplit = { '?' };
            String[] urlArr = rawUrl.Split(urlsplit);
            String filename = urlArr[0];
            /*
            String urlParmString = (urlArr.Length > 1) ? urlArr[1] : "";
            urlsplit[0] = '&';
            String[] urlParmArr = urlParmString.Split(urlsplit);
            Dictionary<string, string> urlParm = new Dictionary<string, string>();
            foreach(String keyValue in urlParmArr)
            {
                urlsplit[0]='=';
                String[] keyValueArr = keyValue.Split(urlsplit);
                String key = keyValueArr[0];
                String value = (keyValueArr.Length > 1) ? keyValueArr[1] : "";
                urlParm.Add(key, value);
            }
            */
            //MessageBox.Show(filename);

            //MessageBox.Show(filename);

            String filePath = "";
            if (filename.Equals("/jsonrpc"))
            {
                //get post data
                String uploadJsonString = "";

                using (Stream inputStream = request.InputStream)
                using (StreamReader reader = new StreamReader(inputStream))
                {
                    uploadJsonString = reader.ReadToEnd();
                }
                //MessageBox.Show(uploadJsonString);                                       
                String returnJsonString = "";
                try
                {
                    MBService mbService = new MBService(uploadJsonString, ref mbApi);
                    returnJsonString = mbService.invokeMB();
                }catch
                {
                    responseHelper.ReturnError(500);
                    return;
                }
                responseHelper.WriteJsonToClient(returnJsonString);
                return;
            }
            else
            {
                responseHelper.ReturnError(404);
                return;
            }
        }

        public void ResponseQuerys()
        {
            var querys = request.QueryString;
            foreach (string key in querys.AllKeys)
            {
                VarityQuerys(key, querys[key]);
            }
        }

        private void VarityQuerys(string key, string value)
        {
            switch (key)
            {
                case "pic": Pictures(value); break;
                case "text": Texts(value); break;
                default: Defaults(value); break;
            }
        }

        private void Pictures(string id)
        {

        }

        private void Texts(string id)
        {

        }

        private void Defaults(string id)
        {

        }

        
    }
}