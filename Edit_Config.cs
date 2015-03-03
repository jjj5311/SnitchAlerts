using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Snith_Alert_System
{
    public partial class Edit_Config : Form
    {
        public bool config_saved_bool { get; set; }
       
        public Edit_Config()
        {
            InitializeComponent();
            //determine if pushbullet is on or off
            bool pb_e = Convert.ToBoolean(load_config_values("pb_enabled"));
            pb_enabled.Checked = pb_e;
            config_1_tb.Text = load_config_values("app_log_file");
            label1.Text = "app_log_file";
            config_2_tb.Text = load_config_values("pushbullet_access_token");
            label2.Text = "pushbullet_access_token";
            config_3_tb.Text = load_config_values("alert_users_list");
            label3.Text = "alert_users_list";
            config_4_tb.Text = load_config_values("minecraft_log_1");
            label4.Text = "minecraft_log_1";
            config_5_tb.Text = load_config_values("minecraft_log_2");
            label5.Text = "minecraft_log_2";
            config_6_tb.Text = load_config_values("pushbullet_msg_token");
            label6.Text = "pushbullet_msg_token";
            config_7_tb.Text = load_config_values("pushbullet_channel_tag");
            label7.Text = "pushbullet_channel_tag";
            config_8_tb.Text = load_config_values("console_client_path");
            label8.Text = "console_client_path";
            config_9_tb.Text = load_config_values("app_device_id");
            label10.Text = "app_device_id";
            config_10_tb.Text = load_config_values("rateLimit");
            label11.Text = "rateLimit [seconds]";
            config_11_tb.Text = load_config_values("cmd_line_args");
            label12.Text = "CommandLineArguments";
        }

        public string load_config_values(string key)
        {

            config_saved_bool = false;
            //load the configuration values to the form and display them
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings.Get(key).ToString();
        }

        private void btn_save_config_Click(object sender, EventArgs e)
        {
            config_saved_bool = true;
            update_config_setting("app_log_file", config_1_tb.Text);
            update_config_setting("pushbullet_access_token", config_2_tb.Text);
            update_config_setting("alert_users_list", config_3_tb.Text);
            update_config_setting("minecraft_log_1", config_4_tb.Text);
            update_config_setting("minecraft_log_2", config_5_tb.Text);
            update_config_setting("pushbullet_msg_token", config_6_tb.Text);
            update_config_setting("pushbullet_channel_tag", config_7_tb.Text);
            update_config_setting("console_client_path", config_8_tb.Text);
            update_config_setting("app_device_id", config_9_tb.Text);
            update_config_setting("rateLimit", config_10_tb.Text);
            update_config_setting("cmd_line_args", config_11_tb.Text);
            this.Close();
        }

        public void update_config_setting(string key, string value)
        {
            
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");

        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pb_enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (pb_enabled.Checked)
            {
                //pushbullet enabled
                update_config_setting("pb_enabled", "true");
            }
            else
            {
                update_config_setting("pb_enabled", "false");
            }
        }

        private string runFolderBrowse()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "error";
        }

        private void config_1_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_1_tb.Text = runFolderBrowse();
        }

        private void config_2_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_2_tb.Text = "Pushbullet Access Token needs to be pasted here";
        }

        private void config_3_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_3_tb.Text = "You probably need a link here";
        }

        private void config_4_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_4_tb.Text = runFolderBrowse();
        }

        private void config_5_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_5_tb.Text = runFolderBrowse();
        }

        private void config_6_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_6_tb.Text = "Pushbullet access token should be pasted here";
        }

        private void config_7_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_7_tb.Text = "You should type in the channel name";
        }

        private void config_8_tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            config_8_tb.Text = runFolderBrowse();
        }


    }
}
