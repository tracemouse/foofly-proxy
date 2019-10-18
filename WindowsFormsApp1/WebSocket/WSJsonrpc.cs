
using System;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace FooflyProxy
{
    public class WSJsonrpc : WebSocketBehavior
    {
        private string _password;


        public WSJsonrpc()
        {

        }

        protected override void OnOpen()
        {
            LogUtil.write("wsjsonrpc thread id=" + Thread.CurrentThread.ManagedThreadId.ToString());
            base.OnOpen();
            _password = Context.QueryString["password"];

        }

        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                string password = Properties.Settings.Default.password;
                if (password != "" && _password != password)
                {
                    Send("{\"error\":\"invalide password\"}");
                    return;
                }
                var uploadJsonString = e.Data;

                /*
                TestTask testTask = new TestTask(ref mbApi, uploadJsonString);
                UIntPtr taskThread;
                Player.ServiceThreadPool.Execute(testTask, out taskThread);
                Player.ServiceThreadPool.WaitFor(taskThread);
                String returnJsonString = testTask.getReturnJsonString();
                LogUtil.write("wsjsonrpc returnJsonString=" + returnJsonString);
                */


            }catch(Exception ex)
            {
                //MessageBox.Show("WSJsonrpc:" + ex.Message);
                LogUtil.write("WSJsonrpc:" + ex.Message);
            }
        }

    }
}