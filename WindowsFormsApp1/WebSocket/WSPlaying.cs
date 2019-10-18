using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AimpFlyPlugin
{
    public class WSPlaying : WebSocketBehavior
    {
        private string _password;

        private AimpInterface mbApi;
        private double interval = 1000;
        private bool loopSend = false;
        private MBControl mbControl;
        private System.Timers.Timer mbTimer;


        public WSPlaying(ref AimpInterface _mbApi)
        {
            this.mbApi = _mbApi;
            mbControl = new MBControl(ref _mbApi);
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


                JObject uploadJson = (JObject)JsonConvert.DeserializeObject(uploadJsonString);

                String action = uploadJson["action"].ToString();
                LogUtil.write("action=" + action);
                String returnJsonString;
                switch (action)
                {
                    case "start":
                        int interval = (int)uploadJson["interval"];
                        LogUtil.write("interval=" + interval);
                        interval = (interval > 5000) ? 5000 : interval;
                        interval = (interval < 500) ? 500 : interval;
                        if (!this.loopSend || this.interval != interval)
                        {
                            this.mbTimer.Stop();
                            this.interval = interval;
                            this.mbTimer.Interval = this.interval;
                            this.mbTimer.AutoReset = true;
                            this.mbTimer.Start();
                        }
                        break;
                    case "stop":
                        this.mbTimer.Stop();
                        break;
                    case "play":
                        returnJsonString = mbControl.Play();
                        break;
                    case "pause":
                        returnJsonString = mbControl.Pause();
                        break;
                    case "playPause":
                        returnJsonString = mbControl.PlayPause();
                        break;
                    case "setMute":
                        bool mute = (bool)uploadJson["mute"];
                        returnJsonString = mbControl.setMute(mute);
                        break;
                    case "setPosition":
                        int position = (int)uploadJson["position"];
                        returnJsonString = mbControl.setPosition(position);
                        break;
                    case "setVolume":
                        int volume = (int)uploadJson["volume"];
                        returnJsonString = mbControl.setVolume(volume);
                        break;
                    case "setVolumep":
                        int volumep = (int)uploadJson["volumep"];
                        returnJsonString = mbControl.setVolumep(volumep);
                        break;
                    case "setVolumef":
                        float volumef = (float)uploadJson["volumef"];
                        returnJsonString = mbControl.setVolumef(volumef);
                        break;
                    case "startAutoDj":
                        returnJsonString = mbControl.startAutoDj();
                        break;
                    case "endAutoDj":
                        returnJsonString = mbControl.endAutoDj();
                        break;
                    case "setShuffle":
                        bool shuffle = (bool)uploadJson["shuffle"];
                        returnJsonString = mbControl.setShuffle(shuffle);
                        break;
                    case "setRepeat":
                        int repeat = (int)uploadJson["repeat"];
                        returnJsonString = mbControl.setRepeat(repeat);
                        break;
                    case "playNext":
                        returnJsonString = mbControl.playNextTrack();
                        break;
                    case "playNow":
                        String fileUrl1 = uploadJson["fileUrl"].ToString();
                        returnJsonString = mbControl.nowPlayingPlayNow(fileUrl1);
                        break;
                    case "playPrev":
                        returnJsonString = mbControl.playPrevTrack();
                        break;
                    case "playlistTracklist":
                        String playlistUrl = uploadJson["playlistUrl"].ToString();
                        returnJsonString = mbControl.playlistTracklist(playlistUrl);
                        break;
                    case "playlistPlayNow":
                        String playlistUrl1 = uploadJson["playlistUrl"].ToString();
                        returnJsonString = mbControl.playlistPlayNow(playlistUrl1);
                        break;
                    case "playTracks":
                        String tracks = uploadJson["tracks"].ToString();
                        //MessageBox.Show(tracks);
                        returnJsonString = mbControl.playTracks(tracks);
                        break;
                    //case "parseCue":
                    //    String fname = "D:\\Music\\APE\\刘芳-刘芳[APE]\\CDImage.ape";
                    //    String cuefile = FileUtil.findCueFile(fname);
                    //    //MessageBox.Show(cuefile);

                    //    String filename = uploadJson["filename"].ToString();
                    //    returnJsonString = JsonConvert.SerializeObject(CueUtil.parseCueFile(filename, null));
                    //    break;
                    case "closeScreen":
                        returnJsonString = mbControl.closeScreen();
                        break;
                    case "shutdown":
                        int minutes = (int)uploadJson["minutes"];
                        returnJsonString = mbControl.shutdown(minutes);
                        break;
                    default:
                        break;
                }
            }catch(Exception ex)
            {
                //MessageBox.Show("WSPlaying:" + ex.Message);
                LogUtil.write("WSPlaying:" + ex.Message);
            }

        }

        private void start()
        {
            this.mbTimer.AutoReset = true;
            this.mbTimer.Start();
        }

        private void stop()
        {
            this.loopSend = false;
            this.mbTimer.Stop();
        }


        protected override void OnClose(CloseEventArgs e)
        {
            this.mbTimer.Stop();
            base.OnClose(e);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _password = Context.QueryString["password"];

            LogUtil.write("wsplaying opened");

            this.mbTimer = new System.Timers.Timer(this.interval);
            this.mbTimer.AutoReset = false;
            this.mbTimer.Elapsed += MbTimer_Elapsed;
            this.loopSend = false;
        }

        private void MbTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string password = Properties.Settings.Default.password;
                if (password != "" && _password != password)
                {
                    Send("{\"error\":\"invalide password\"}");
                    return;
                }
                string returnJsonString = mbControl.getNowPlaying();
                LogUtil.write("nowPlaying=" + returnJsonString);
                Send(returnJsonString);
            }catch(Exception ex)
            {
                LogUtil.write("WSPlaying loop:" + ex.Message);
            }
        }
    }
}