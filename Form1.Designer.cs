namespace Snith_Alert_System
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_buttons_group = new System.Windows.Forms.GroupBox();
            this.pbTestAlert = new System.Windows.Forms.Button();
            this.rUsers = new System.Windows.Forms.Button();
            this.pb_test = new System.Windows.Forms.Button();
            this.launch_console_client_btn = new System.Windows.Forms.Button();
            this.btn_stop_alerts = new System.Windows.Forms.Button();
            this.btn_run_alerts = new System.Windows.Forms.Button();
            this.btn_edit_config = new System.Windows.Forms.Button();
            this.group_box_output = new System.Windows.Forms.GroupBox();
            this.output_RTB = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.busy_pic_box = new System.Windows.Forms.PictureBox();
            this.appInfo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.appInfoDate = new System.Windows.Forms.Label();
            this.appInfoVersion = new System.Windows.Forms.Label();
            this.read_log_timer = new System.Windows.Forms.Timer(this.components);
            this.update_users_file = new System.Windows.Forms.Timer(this.components);
            this.createPBDevice = new System.Windows.Forms.Button();
            this.pbDeviceName = new System.Windows.Forms.TextBox();
            this.readDevices = new System.Windows.Forms.Button();
            this.readPB = new System.Windows.Forms.Button();
            this.rateTimer = new System.Windows.Forms.Timer(this.components);
            this.sendCMDbtn = new System.Windows.Forms.Button();
            this.cmdText = new System.Windows.Forms.TextBox();
            this.btn_buttons_group.SuspendLayout();
            this.group_box_output.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.busy_pic_box)).BeginInit();
            this.appInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_buttons_group
            // 
            this.btn_buttons_group.Controls.Add(this.pbTestAlert);
            this.btn_buttons_group.Controls.Add(this.rUsers);
            this.btn_buttons_group.Controls.Add(this.pb_test);
            this.btn_buttons_group.Controls.Add(this.launch_console_client_btn);
            this.btn_buttons_group.Controls.Add(this.btn_stop_alerts);
            this.btn_buttons_group.Controls.Add(this.btn_run_alerts);
            this.btn_buttons_group.Controls.Add(this.btn_edit_config);
            this.btn_buttons_group.Location = new System.Drawing.Point(6, 1);
            this.btn_buttons_group.Name = "btn_buttons_group";
            this.btn_buttons_group.Size = new System.Drawing.Size(642, 67);
            this.btn_buttons_group.TabIndex = 0;
            this.btn_buttons_group.TabStop = false;
            this.btn_buttons_group.Text = "Controls";
            // 
            // pbTestAlert
            // 
            this.pbTestAlert.Location = new System.Drawing.Point(274, 13);
            this.pbTestAlert.Name = "pbTestAlert";
            this.pbTestAlert.Size = new System.Drawing.Size(86, 50);
            this.pbTestAlert.TabIndex = 30;
            this.pbTestAlert.Text = "Test Pushbullet (ALERT)";
            this.pbTestAlert.UseVisualStyleBackColor = true;
            this.pbTestAlert.Click += new System.EventHandler(this.pbTestAlert_Click);
            // 
            // rUsers
            // 
            this.rUsers.Location = new System.Drawing.Point(366, 13);
            this.rUsers.Name = "rUsers";
            this.rUsers.Size = new System.Drawing.Size(86, 50);
            this.rUsers.TabIndex = 29;
            this.rUsers.Text = "Reload Enemy Log";
            this.rUsers.UseVisualStyleBackColor = true;
            this.rUsers.Click += new System.EventHandler(this.rUsers_Click);
            // 
            // pb_test
            // 
            this.pb_test.Location = new System.Drawing.Point(184, 13);
            this.pb_test.Name = "pb_test";
            this.pb_test.Size = new System.Drawing.Size(86, 50);
            this.pb_test.TabIndex = 28;
            this.pb_test.Text = "Test Pushbullet (MSG)";
            this.pb_test.UseVisualStyleBackColor = true;
            this.pb_test.Click += new System.EventHandler(this.pb_test_Click);
            // 
            // launch_console_client_btn
            // 
            this.launch_console_client_btn.Location = new System.Drawing.Point(94, 13);
            this.launch_console_client_btn.Name = "launch_console_client_btn";
            this.launch_console_client_btn.Size = new System.Drawing.Size(86, 50);
            this.launch_console_client_btn.TabIndex = 27;
            this.launch_console_client_btn.Text = "Launch Console Client";
            this.launch_console_client_btn.UseVisualStyleBackColor = true;
            this.launch_console_client_btn.Click += new System.EventHandler(this.launch_console_client_btn_Click);
            // 
            // btn_stop_alerts
            // 
            this.btn_stop_alerts.BackColor = System.Drawing.Color.IndianRed;
            this.btn_stop_alerts.Location = new System.Drawing.Point(550, 13);
            this.btn_stop_alerts.Name = "btn_stop_alerts";
            this.btn_stop_alerts.Size = new System.Drawing.Size(86, 50);
            this.btn_stop_alerts.TabIndex = 26;
            this.btn_stop_alerts.Text = "Stop Alerts!";
            this.btn_stop_alerts.UseVisualStyleBackColor = false;
            this.btn_stop_alerts.Click += new System.EventHandler(this.btn_stop_alerts_Click);
            // 
            // btn_run_alerts
            // 
            this.btn_run_alerts.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_run_alerts.Location = new System.Drawing.Point(458, 13);
            this.btn_run_alerts.Name = "btn_run_alerts";
            this.btn_run_alerts.Size = new System.Drawing.Size(86, 50);
            this.btn_run_alerts.TabIndex = 25;
            this.btn_run_alerts.Text = "Run Alerts!";
            this.btn_run_alerts.UseVisualStyleBackColor = false;
            this.btn_run_alerts.Click += new System.EventHandler(this.btn_run_alerts_Click);
            // 
            // btn_edit_config
            // 
            this.btn_edit_config.Location = new System.Drawing.Point(6, 13);
            this.btn_edit_config.Name = "btn_edit_config";
            this.btn_edit_config.Size = new System.Drawing.Size(84, 50);
            this.btn_edit_config.TabIndex = 24;
            this.btn_edit_config.Text = "Edit Configuration";
            this.btn_edit_config.UseVisualStyleBackColor = true;
            this.btn_edit_config.Click += new System.EventHandler(this.btn_edit_config_Click);
            // 
            // group_box_output
            // 
            this.group_box_output.Controls.Add(this.output_RTB);
            this.group_box_output.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.group_box_output.Location = new System.Drawing.Point(6, 74);
            this.group_box_output.Name = "group_box_output";
            this.group_box_output.Size = new System.Drawing.Size(642, 187);
            this.group_box_output.TabIndex = 1;
            this.group_box_output.TabStop = false;
            this.group_box_output.Text = "Output";
            // 
            // output_RTB
            // 
            this.output_RTB.BackColor = System.Drawing.SystemColors.Info;
            this.output_RTB.Location = new System.Drawing.Point(3, 16);
            this.output_RTB.Name = "output_RTB";
            this.output_RTB.Size = new System.Drawing.Size(633, 165);
            this.output_RTB.TabIndex = 0;
            this.output_RTB.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.busy_pic_box);
            this.groupBox3.Location = new System.Drawing.Point(590, 266);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(52, 52);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BUSY";
            // 
            // busy_pic_box
            // 
            this.busy_pic_box.Location = new System.Drawing.Point(5, 12);
            this.busy_pic_box.Name = "busy_pic_box";
            this.busy_pic_box.Size = new System.Drawing.Size(41, 34);
            this.busy_pic_box.TabIndex = 0;
            this.busy_pic_box.TabStop = false;
            // 
            // appInfo
            // 
            this.appInfo.Controls.Add(this.label1);
            this.appInfo.Controls.Add(this.appInfoDate);
            this.appInfo.Controls.Add(this.appInfoVersion);
            this.appInfo.Location = new System.Drawing.Point(6, 276);
            this.appInfo.Name = "appInfo";
            this.appInfo.Size = new System.Drawing.Size(578, 42);
            this.appInfo.TabIndex = 22;
            this.appInfo.TabStop = false;
            this.appInfo.Text = "App Information";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(350, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Created By: jjj5311@gmail.com [Credit Jr]";
            // 
            // appInfoDate
            // 
            this.appInfoDate.AutoSize = true;
            this.appInfoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.appInfoDate.Location = new System.Drawing.Point(215, 16);
            this.appInfoDate.Name = "appInfoDate";
            this.appInfoDate.Size = new System.Drawing.Size(129, 15);
            this.appInfoDate.TabIndex = 2;
            this.appInfoDate.Text = "Publish Date: 01.01.2014";
            // 
            // appInfoVersion
            // 
            this.appInfoVersion.AutoSize = true;
            this.appInfoVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.appInfoVersion.Location = new System.Drawing.Point(6, 16);
            this.appInfoVersion.Name = "appInfoVersion";
            this.appInfoVersion.Size = new System.Drawing.Size(203, 15);
            this.appInfoVersion.TabIndex = 1;
            this.appInfoVersion.Text = "AppETools Version: TESTING VERSION";
            // 
            // read_log_timer
            // 
            this.read_log_timer.Tick += new System.EventHandler(this.read_log_timer_Tick);
            // 
            // update_users_file
            // 
            this.update_users_file.Interval = 900000;
            this.update_users_file.Tick += new System.EventHandler(this.update_users_file_Tick);
            // 
            // createPBDevice
            // 
            this.createPBDevice.Location = new System.Drawing.Point(654, 12);
            this.createPBDevice.Name = "createPBDevice";
            this.createPBDevice.Size = new System.Drawing.Size(170, 25);
            this.createPBDevice.TabIndex = 31;
            this.createPBDevice.Text = "Create PB Device";
            this.createPBDevice.UseVisualStyleBackColor = true;
            this.createPBDevice.Click += new System.EventHandler(this.createPBDevice_Click);
            // 
            // pbDeviceName
            // 
            this.pbDeviceName.Location = new System.Drawing.Point(656, 42);
            this.pbDeviceName.Name = "pbDeviceName";
            this.pbDeviceName.Size = new System.Drawing.Size(167, 20);
            this.pbDeviceName.TabIndex = 32;
            this.pbDeviceName.Text = "device_id_here";
            // 
            // readDevices
            // 
            this.readDevices.Location = new System.Drawing.Point(653, 99);
            this.readDevices.Name = "readDevices";
            this.readDevices.Size = new System.Drawing.Size(170, 25);
            this.readDevices.TabIndex = 33;
            this.readDevices.Text = "Get PB Devices";
            this.readDevices.UseVisualStyleBackColor = true;
            this.readDevices.Click += new System.EventHandler(this.readDevices_Click);
            // 
            // readPB
            // 
            this.readPB.Location = new System.Drawing.Point(653, 130);
            this.readPB.Name = "readPB";
            this.readPB.Size = new System.Drawing.Size(170, 25);
            this.readPB.TabIndex = 34;
            this.readPB.Text = "Read PB Alerts";
            this.readPB.UseVisualStyleBackColor = true;
            this.readPB.Click += new System.EventHandler(this.readPB_Click);
            // 
            // rateTimer
            // 
            this.rateTimer.Tick += new System.EventHandler(this.rateTimer_Tick);
            // 
            // sendCMDbtn
            // 
            this.sendCMDbtn.Location = new System.Drawing.Point(653, 161);
            this.sendCMDbtn.Name = "sendCMDbtn";
            this.sendCMDbtn.Size = new System.Drawing.Size(170, 26);
            this.sendCMDbtn.TabIndex = 31;
            this.sendCMDbtn.Text = "Send MC Command";
            this.sendCMDbtn.UseVisualStyleBackColor = true;
            this.sendCMDbtn.Click += new System.EventHandler(this.sendCMDbtn_Click);
            // 
            // cmdText
            // 
            this.cmdText.Location = new System.Drawing.Point(653, 193);
            this.cmdText.Name = "cmdText";
            this.cmdText.Size = new System.Drawing.Size(170, 20);
            this.cmdText.TabIndex = 35;
            this.cmdText.Text = "Command Text";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(836, 337);
            this.Controls.Add(this.cmdText);
            this.Controls.Add(this.sendCMDbtn);
            this.Controls.Add(this.readPB);
            this.Controls.Add(this.readDevices);
            this.Controls.Add(this.pbDeviceName);
            this.Controls.Add(this.createPBDevice);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.appInfo);
            this.Controls.Add(this.group_box_output);
            this.Controls.Add(this.btn_buttons_group);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Snitch Alerts";
            this.btn_buttons_group.ResumeLayout(false);
            this.group_box_output.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.busy_pic_box)).EndInit();
            this.appInfo.ResumeLayout(false);
            this.appInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox btn_buttons_group;
        private System.Windows.Forms.GroupBox group_box_output;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox busy_pic_box;
        private System.Windows.Forms.GroupBox appInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label appInfoDate;
        private System.Windows.Forms.Label appInfoVersion;
        private System.Windows.Forms.RichTextBox output_RTB;
        private System.Windows.Forms.Button btn_stop_alerts;
        private System.Windows.Forms.Button btn_run_alerts;
        private System.Windows.Forms.Button btn_edit_config;
        private System.Windows.Forms.Timer read_log_timer;
        private System.Windows.Forms.Timer update_users_file;
        private System.Windows.Forms.Button launch_console_client_btn;
        private System.Windows.Forms.Button rUsers;
        private System.Windows.Forms.Button pb_test;
        private System.Windows.Forms.Button pbTestAlert;
        private System.Windows.Forms.Button createPBDevice;
        private System.Windows.Forms.TextBox pbDeviceName;
        private System.Windows.Forms.Button readDevices;
        private System.Windows.Forms.Button readPB;
        private System.Windows.Forms.Timer rateTimer;
        private System.Windows.Forms.Button sendCMDbtn;
        private System.Windows.Forms.TextBox cmdText;
    }
}

