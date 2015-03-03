using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Snith_Alert_System
{

    public partial class Form1 : Form
    {
        //global color variables
        public Color normal_text = Color.Black;
        public Color alertText = Color.Orange;
        public Color errorText = Color.Red;

        List<string> bad_guys_names = new List<string>();
        List<string> bad_guys_status = new List<string>();
        List<string> bad_guys_info = new List<string>();

        public Process proc;

        public long log_file_length;

        public bool log_file_exist;

        public bool rateLimit;


        public Form1()
        {
            InitializeComponent();
            startup_function();
        }

        #region aux_functions
        public void loadInfo()
        {
            /* 
             * Hardcode Version
            */
            String version = "1.0.0.8-pre";
            appInfoVersion.Text = "Snitch Alerts Version: " + version;
            String update = "Publish Date: 03.02.2015";
            appInfoDate.Text = update;
        }

        public string loadKey(string key)
        {
            /*
             * Function to load configuration settings into the app dynamically
             */
            var appSettings = ConfigurationManager.AppSettings;
            string value = "default";
            try
            {
                value = appSettings.Get(key).ToString();
            }
            catch
            {
                DisplayText("Must enter a value for config option: " + key, alertText, false);
            }
            return value;
        }

        public void DisplayText(string displayText, Color LineColor, bool log)
        {
            /*
             * Function to output text to display box user can see
             * used for errors, info and all sorts of stuff
             */
            output_RTB.SelectionColor = LineColor;
            output_RTB.SelectedText = displayText + Environment.NewLine;
            output_RTB.ScrollToCaret();
            output_RTB.Update();
            //log text to file as well
            if (log)
            {
                writeToLog(displayText);
            }

        }

        private void circle_busy()
        {
            //edit picturebox region to be circle
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, busy_pic_box.Width - 2, busy_pic_box.Height - 1);
            Region rg = new Region(gp);
            busy_pic_box.Region = rg;
            busy_pic_box.BackColor = Color.Green;
        }

        private void writeToLog(String text)
        {
            //log information to log file 
            string logfile = loadKey("app_log_file");
            if (File.Exists(logfile))
            {
            }
            else
            {
                if (log_file_exist)
                {
                    DisplayText("File at config \"app_log_file\" does not exist", alertText, false);
                    log_file_exist = false;
                    return;
                }
                else
                {
                    return;
                }

            }
            DateTime date = DateTime.Now;
            using (StreamWriter s = new StreamWriter(logfile, true))
            {
                s.WriteLine(date + "," + text);
                s.Close();
            }
        }

        private void startup_function()
        {
            //startup function to run on application startup
            circle_busy();
            loadInfo();
            log_file_exist = true;
            rateLimit = false;
            int configTime = Int32.Parse(loadKey("rateLimit"));
            rateTimer.Interval = configTime * 1000;
        }

        public void PushBullet_note(String title, String body, String apiKey, String channelTag)
        {
            string pb_enabled = loadKey("pb_enabled");
            if (Convert.ToBoolean(pb_enabled))
            {
                try
                {
                    //send note to pushbullet server
                    using (var wb = new WebClient())
                    {
                        var auth = new NameValueCollection();
                        auth["Authorization"] = "Basic " +
                            Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(apiKey + ":"));
                        wb.Headers.Add(auth);
                        var data = new NameValueCollection();
                        data["type"] = "note";
                        data["title"] = title;
                        data["body"] = body;
                        data["channel_tag"] = channelTag;
                        wb.UploadValues("https://api.pushbullet.com/v2/pushes", "POST", data);
                    }
                    DisplayText("Pushbullet link sent!", alertText, true);
                }
                catch (Exception e)
                {
                    DisplayText("Error sending pushbullet: " + e, errorText, true);
                }

            }
            else
            {
                DisplayText("PushBullet currently disabled", alertText, false);
            }

            rateLimit = true;
            rateLimitStart();

        }

        public void rateLimitStart(){
            Edit_Config ec = new Edit_Config();
            ec.update_config_setting("pb_enabled", "false");
            rateTimer.Start();
        }

        public void PushBullet_note_msg(String title, String body, String apiKey)
        {
            try
            {
                //send note to pushbullet server
                using (var wb = new WebClient())
                {
                    var auth = new NameValueCollection();
                    auth["Authorization"] = "Basic " +
                        Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(apiKey + ":"));
                    wb.Headers.Add(auth);
                    var data = new NameValueCollection();
                    data["type"] = "note";
                    data["title"] = title;
                    data["body"] = body;
                    wb.UploadValues("https://api.pushbullet.com/v2/pushes", "POST", data);
                }
                DisplayText("Pushbullet link sent!", alertText, true);
            }
            catch (Exception e)
            {
                DisplayText("Error sending pushbullet: " + e, errorText, true);
            }

        }

        public void delete_Push(String pushID, String apiKey)
        {

                try
                {
                    WebRequest wr = WebRequest.Create("https://api.pushbullet.com/v2/pushes/" + pushID);
                    var auth = new NameValueCollection();
                    auth["Authorization"] = "Basic " +
                        Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(apiKey + ":"));
                    wr.Headers.Add(auth);
                    wr.Method = "DELETE";
                    wr.GetResponse();
                    DisplayText("Deleted Push ID: " + pushID +" successfully", normal_text, true);
                }
                catch (Exception e)
                {
                    DisplayText("Error sending pushbullet: " + e, errorText, true);
                }
        }

        public void pb_Create_Device(String nickname, String type, String apiKey)
        {
            string pbResponse = null;
            try
            {
                //create new device on pb server
                using (var wb = new WebClient())
                {
                    var auth = new NameValueCollection();
                    auth["Authorization"] = "Basic " +
                        Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(apiKey + ":"));
                    wb.Headers.Add(auth);
                    wb.Headers[HttpRequestHeader.Accept] = "applicationg/json";
                    var data = new NameValueCollection();
                    data["nickname"] = nickname;
                    data["type"] = "stream";
                    byte[] result = wb.UploadValues("https://api.pushbullet.com/v2/devices", "POST", data);
                    //use better json parsing here
                    pbResponse = System.Text.Encoding.Default.GetString(result);
                    int start = pbResponse.IndexOf("iden") - 1;
                    pbResponse = pbResponse.Substring(start);
                    pbResponse = pbResponse.Substring(0, pbResponse.IndexOf(','));
                    string id = pbResponse.Replace("\"", " ");
                    string[] did = id.Split(':');
                    setDeviceID(did[1].Trim());
                    
                    DisplayText("Device ID: " + did[1], normal_text, false);
                }
            }
            catch (Exception e)
            {
                DisplayText("Error sending pushbullet: " + e, errorText, true);
            }
        }

        public void setDeviceID(String id)
        {
            Edit_Config ec = new Edit_Config();
            ec.update_config_setting("app_device_id", id);
        }

        public void pb_Reciever()
        {
            string apiKey = loadKey("pushbullet_msg_token");
            WebClient wc = new WebClient();
            string authEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
            wc.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", authEncoded);
            string result = null;
            //Set webclient header to accept json
            wc.Headers[HttpRequestHeader.Accept] = "application/json";
            try
            {
                try
                {
                    //Get the string
                    Stream wcResult = wc.OpenRead("https://api.pushbullet.com/v2/pushes?active=true");
                    StreamReader sr = new StreamReader(wcResult);
                    result = sr.ReadToEnd();
                    JObject jsonResult = JObject.Parse(result);       
                    foreach (var push in jsonResult["pushes"])
                    {
                        string target_device = (string)push["target_device_iden"];
                        string pushType = (string)push["type"];
                        string pushID = (string)push["iden"];
                        string title = (string)push["title"];
                        string body = (string)push["body"];
                        string app_ID = loadKey("app_device_id");
                        if (target_device.Equals(app_ID))
                        {
                            //we have a push sent to the program do something with it
                            //has to be a note type push with cmd in the subject
                            if (pushType.Equals("note"))
                            {
                                if (title.Equals("cmd"))
                                {
                                    //received a command
                                    DisplayText("Received PushBullet Command Push: " + pushID + " \n Push Command: " + body, normal_text, true);
                                    executeCommand(body);
                                    //delete push once it has run
                                    delete_Push(pushID, apiKey);
                                    
                                }
                            }
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    //Oh no something went wrong, but what?
                    result = ex.ToString();
                }
                if (result != null)
                {
                    //DisplayText(result, normal_text, false);
                }
                else
                {
                    MessageBox.Show("We failed and the site returned 404");
                }
                

            }
            catch (Exception ex)
            {
                //Oops, something went wrong!
                DisplayText(ex.ToString(), normal_text, false);
                DisplayText(result, normal_text, false);
            }

        }
            


        public void GetDevices(string apiKey = null)
        {
            WebClient wc = new WebClient();
            string authEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
            wc.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", authEncoded);
            //Check if there is an apikey supplied and create an authenticated webclient
            //Clean up and set default values
            string result = null;
            //Set webclient header to accept json
            wc.Headers[HttpRequestHeader.Accept] = "application/json";
            try
            {
                try
                {
                    //Get the devices
                    result = wc.DownloadString("https://api.pushbullet.com/api/devices");
                }
                catch (Exception ex)
                {
                    //Oh no something went wrong, but what?
                    result = ex.ToString();
                }
                if (result != null)
                {
                    List<string> devicesList = new List<string>();
                    //Let's parse the json so we can get the device information
                    JObject json = (JObject)JsonConvert.DeserializeObject(result);
                    string csrf = (string)json["csrf"];
                    JArray devices = (JArray)json["devices"];
                    JArray shared_devices = (JArray)json["shared_devices"];
                    string temp = null;
                    foreach (JObject o in devices)
                    {
                        DisplayText(devices.ToString(), normal_text, false);
                    }
                    if (shared_devices != null)
                    {
                        foreach (JObject o in shared_devices)
                        {
                            if (o["extras"]["model"] != null)
                            {
                                temp = o["extras"]["model"].ToString();
                                devicesList.Add(temp);
                            }
                            else
                            {
                                temp = o["extras"]["model"].ToString();
                                devicesList.Add(temp);
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("We failed and the site returned 404");
                }
                //Set selected device to first and display popup that the login was successfull
                
            }
            catch (Exception ex)
            {
                //Oops, something went wrong!
                DisplayText(ex.ToString(), normal_text, false);
                DisplayText(result, normal_text, false);
            }
        }


        public void load_users()
        {
            DisplayText("Reading users to alert file", normal_text, false);
            //load in list of bad dudes
            string bad_guys_path = loadKey("alert_users_list");
            //if its a url download the file
            bad_guys_info.Clear();
            bad_guys_names.Clear();
            bad_guys_status.Clear();
            string bad_guys = "";
            if (bad_guys_path.Contains("http"))
            {
                bad_guys = GetCSV(bad_guys_path);
            }
            else
            {
                DisplayText("Alert users file is local to pc", normal_text, false);
                bad_guys = System.IO.File.ReadAllText(bad_guys_path);
            }
            if (bad_guys.Equals(""))
            {
                DisplayText("Error Reading Users File", alertText, true);
            }
            else
            {
                string[] bad_guys_lines = bad_guys.Split('\n');
                foreach (string s in bad_guys_lines)
                {
                    String[] sPlit = s.Split('\t');
                    bad_guys_names.Add(sPlit[0]);
                    bad_guys_status.Add(sPlit[1]);
                    bad_guys_info.Add(sPlit[2]);
                }
                int alert_on_users_count = bad_guys_names.Count();
                DisplayText("Alert Users read with: " + alert_on_users_count + " Users", normal_text, true);
            }

        }

        public string GetCSV(string url)
        {
            //pass url to any seperated value web address and return the contents
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }

        public void read_log(string log_key)
        {
            string path = loadKey(log_key);
            //read in chat log, only read new stuff from log_line_count and on
            FileInfo fi;
            //Attempt to read file, if locked by another process skip it 
            try
            {
                fi = new FileInfo(path);
            }
            catch (Exception e)
            {
                return;
            }
            string newlines = string.Empty;

            if (log_file_length == fi.Length)
            {
                //files are same size do nothing
                return;
            }
            else
            {
                try
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        sr.BaseStream.Position = log_file_length;
                        newlines = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    return;
                }

                log_file_length = fi.Length;
                send_update(newlines);
            }
        }

        public void read_log(bool firstrun, string log_key)
        {
            string path = loadKey(log_key);
            //read in chat log, only read new stuff from log_line_count and on
            FileInfo fi = new FileInfo(path);

            string newlines = string.Empty;

            if (log_file_length == fi.Length)
            {
                //files are same size do nothing
                return;
            }
            else
            {

                try
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        sr.BaseStream.Position = log_file_length;
                        newlines = sr.ReadToEnd();
                    }
                    log_file_length = fi.Length;
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }


        private void send_update(string newlines)
        {
            //new lines added to chat log read them
            newlines = newlines.Replace("\r", "");
            string[] split_newlines = newlines.Split('\n');
            foreach (string s in split_newlines)
            {
                if (s.Equals("")) { }
                else
                {
                    if (s.Contains("From"))
                    {
                        DisplayText("Message recieved in game", normal_text, false);
                        DisplayText(s, normal_text, true);
                        //message from another user process and send pushbullet link
                        PushBullet_note_msg("[Snitch Alert System] MESSAGE", s, loadKey("pushbullet_msg_token"));
                    }
                    else if (s.Contains("snitch"))
                    {
                        if (s.Contains("alert"))
                        {
                            //alert snitch send alert no matter who it is
                            DisplayText(s, normal_text, true);
                            //do the data mining here
                            string name = s.Substring(s.IndexOf('*') + 2);
                            string temp_name = s.Substring(s.IndexOf("snitch") + 9);
                            string snitch_name = temp_name;
                            string temp_user_name = name.Substring(0, name.IndexOf(' '));
                            name = temp_user_name;
                            DisplayText("ALERT SNITCH TRIGGERED", alertText, true);
                            //call pushbullet function
                            PushBullet_note("Carbon Alert", String.Format("{0} hit snitch {1}", name, snitch_name), loadKey("pushbullet_access_token"), loadKey("pushbullet_channel_tag"));
                        }
                        else
                        {
                            DisplayText(s, normal_text, true);
                            //do the data mining here
                            string name = s.Substring(s.IndexOf('*') + 2);
                            string temp_name = s.Substring(s.IndexOf("snitch") + 9);
                            string snitch_name = temp_name;
                            string temp_user_name = name.Substring(0, name.IndexOf(' '));
                            name = temp_user_name;
                            if (bad_guys_names.Any( x => x.Equals(name, StringComparison.OrdinalIgnoreCase)))
                            {
                                int index = bad_guys_names.IndexOf(name);
                                //get info to put in Note
                                String status = bad_guys_status[index];
                                String notes = bad_guys_info[index];
                                DisplayText("ENEMY TRIGGERED SNITCH", alertText, true);
                                //call pushbullet function
                                PushBullet_note("Carbon Alert", String.Format("{0} hit snitch {1} \n Status: {2} \n Notes: {3}", name, snitch_name, status, notes), loadKey("pushbullet_access_token"), loadKey("pushbullet_channel_tag"));

                            }
                        }
                    }
                    else if (s.Contains("Performance Monitor"))
                    {
                        //performance data incoming
                        DisplayText("Performance Monitor Running", normal_text, true);
                    }
                    else if (s.Contains("Free allocated memory:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Free allocated memory:")), normal_text, true);

                    }
                    else if (s.Contains("Server log size:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Server log size:")), normal_text, true);
                    }
                    else if (s.Contains("Free disk size:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Free disk size:")), normal_text, true);
                    }
                    else if (s.Contains("Current world size:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Current world size:")), normal_text, true);
                    }
                    else if (s.Contains("Loaded chunks in this world:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Loaded chunks in this world:")), normal_text, true);
                    }
                    else if (s.Contains("Living entities in this world:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Living entities in this world:")), normal_text, true);
                    }
                    else if (s.Contains("Entities in this world:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Entities in this world:")), normal_text, true);
                    }
                    else if (s.Contains("Online players:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("Online players")), normal_text, true);
                    }
                    else if (s.Contains("TPS:"))
                    {
                        DisplayText(s.Substring(s.IndexOf("TPS")), normal_text, true);
                    }
                }
            }
        }

        #endregion

        #region button/events

        private void pb_test_Click(object sender, EventArgs e)
        {
            //send test Pushbullet message
            String token = loadKey("pushbullet_msg_token");
            if (token == null)
            {
                DisplayText("You need to specify a pushbullet_msg_token in config", alertText, false);
                return;
            }
            PushBullet_note_msg("[Snitch Alert System]", "This is a test Note", token);
        }

        private void pbTestAlert_Click(object sender, EventArgs e)
        {
            //send test Pushbullet message
            String token = loadKey("pushbullet_access_token");
            if (token == null)
            {
                DisplayText("You need to specify a pushbullet_msg_token in config", alertText, false);
                return;
            }
            PushBullet_note("[Snitch Alert System]", "This is a test Alert!", token, loadKey("pushbullet_channel_tag"));
        }

        private void rUsers_Click(object sender, EventArgs e)
        {
            DisplayText("Starting Reload of Alert file", normal_text, true);
            load_users();
        }


        private void btn_edit_config_Click(object sender, EventArgs e)
        {

            DisplayText("User editting configuration file", normal_text, false);
            //check if user is in run alerts mode
            if (busy_pic_box.BackColor == Color.Orange)
            {
            }
            else
            {
                busy_pic_box.BackColor = Color.Red;
            }
            //allow user to view and edit configuration
            Edit_Config ec = new Edit_Config();
            ec.ShowDialog();
            bool config_saved = ec.config_saved_bool;
            if (config_saved)
            {
                DisplayText("Configuration file saved", alertText, false);
                //user saved config file
                /*TODO
                 * check users config settings to see whats up
                 */
            }
            if (busy_pic_box.BackColor == Color.Orange)
            {

            }
            else
            {
                busy_pic_box.BackColor = Color.Green;
            }
        }


        private void btn_run_alerts_Click(object sender, EventArgs e)
        {
            //check if both log files are loaded
            read_log(true, "minecraft_log_1");
            load_users();
            DisplayText("Alert System Started...", alertText, false);
            read_log_timer.Start();
            update_users_file.Start();
            busy_pic_box.BackColor = Color.Orange;
        }

        private void read_log_timer_Tick(object sender, EventArgs e)
        {
            read_log("minecraft_log_1");
            pb_Reciever();
        }

        private void btn_stop_alerts_Click(object sender, EventArgs e)
        {
            read_log_timer.Stop();

            busy_pic_box.BackColor = Color.Green;
        }

        private void update_users_file_Tick(object sender, EventArgs e)
        {
            load_users();
        }


        private void createPBDevice_Click(object sender, EventArgs e)
        {
            DisplayText("Attempting to create Device", normal_text, false);
            pb_Create_Device(pbDeviceName.Text, "stream", loadKey("pushbullet_msg_token"));
        }



        private void readDevices_Click(object sender, EventArgs e)
        {
            GetDevices(loadKey("pushbullet_msg_token"));
        }

        private void readPB_Click(object sender, EventArgs e)
        {
            pb_Reciever();
        }

        private void rateTimer_Tick(object sender, EventArgs e)
        {
            //set pb to enabled again
            Edit_Config ec = new Edit_Config();
            ec.update_config_setting("pb_enabled", "true");
            ec.Dispose();
            rateTimer.Stop();
            rateLimit = false;
        }


        #endregion

        #region consoleClient

        private void launch_console_client_btn_Click(object sender, EventArgs e)
        {
            DisplayText("User should have all configuration done in console client already, this will simply start the client", alertText, false);
            string exe_path = loadKey("console_client_path");
            string cmdLineArgs = loadKey("cmd_line_args");
            proc = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(exe_path);
            psi.RedirectStandardError = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = false;
            psi.Arguments = " " + cmdLineArgs;
            psi.UseShellExecute = false;
            proc.StartInfo = psi;
            proc.Start();
        }

        public void executeCommand(string cmd)
        {
            //pass command to minecraft console client
            DisplayText("Attempting to run command: " + cmd, normal_text, true);
            proc.StandardInput.WriteLine(cmd);
        }

        #endregion

        private void sendCMDbtn_Click(object sender, EventArgs e)
        {
            executeCommand(cmdText.Text);
        }


    }
}
