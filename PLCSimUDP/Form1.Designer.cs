namespace PLCSimUDP
{
    partial class PLCSimUDP
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
            this.bStartService = new System.Windows.Forms.Button();
            this.tLocalPort = new System.Windows.Forms.TextBox();
            this.tRemoteIP = new System.Windows.Forms.TextBox();
            this.tRemotePort = new System.Windows.Forms.TextBox();
            this.iLocalIP = new System.Windows.Forms.Label();
            this.lRemIP = new System.Windows.Forms.Label();
            this.lRemPort = new System.Windows.Forms.Label();
            this.tLocalIP = new System.Windows.Forms.TextBox();
            this.lLocaLIP = new System.Windows.Forms.Label();
            this.cbLifeFrame = new System.Windows.Forms.CheckBox();
            this.BSConn = new System.Windows.Forms.Button();
            this.bSService = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tSkidIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tSkidValue = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bMFPUpdate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // bStartService
            // 
            this.bStartService.Location = new System.Drawing.Point(18, 202);
            this.bStartService.Name = "bStartService";
            this.bStartService.Size = new System.Drawing.Size(133, 23);
            this.bStartService.TabIndex = 0;
            this.bStartService.Text = "Start";
            this.bStartService.UseVisualStyleBackColor = true;
            this.bStartService.Click += new System.EventHandler(this.bStartService_Click);
            // 
            // tLocalPort
            // 
            this.tLocalPort.Location = new System.Drawing.Point(75, 79);
            this.tLocalPort.Name = "tLocalPort";
            this.tLocalPort.Size = new System.Drawing.Size(38, 20);
            this.tLocalPort.TabIndex = 1;
            this.tLocalPort.Text = "2001";
            this.tLocalPort.Leave += new System.EventHandler(this.tRemoteIP_Enter);
            // 
            // tRemoteIP
            // 
            this.tRemoteIP.Location = new System.Drawing.Point(75, 107);
            this.tRemoteIP.Name = "tRemoteIP";
            this.tRemoteIP.Size = new System.Drawing.Size(78, 20);
            this.tRemoteIP.TabIndex = 2;
            this.tRemoteIP.Text = "192.168.5.53";
            this.tRemoteIP.Leave += new System.EventHandler(this.tRemoteIP_Enter);
            // 
            // tRemotePort
            // 
            this.tRemotePort.Location = new System.Drawing.Point(75, 137);
            this.tRemotePort.Name = "tRemotePort";
            this.tRemotePort.Size = new System.Drawing.Size(38, 20);
            this.tRemotePort.TabIndex = 3;
            this.tRemotePort.Text = "9000";
            this.tRemotePort.Leave += new System.EventHandler(this.tRemoteIP_Enter);
            // 
            // iLocalIP
            // 
            this.iLocalIP.AutoSize = true;
            this.iLocalIP.Location = new System.Drawing.Point(14, 82);
            this.iLocalIP.Name = "iLocalIP";
            this.iLocalIP.Size = new System.Drawing.Size(55, 13);
            this.iLocalIP.TabIndex = 4;
            this.iLocalIP.Text = "LoaclPort:";
            // 
            // lRemIP
            // 
            this.lRemIP.AutoSize = true;
            this.lRemIP.Location = new System.Drawing.Point(24, 110);
            this.lRemIP.Name = "lRemIP";
            this.lRemIP.Size = new System.Drawing.Size(45, 13);
            this.lRemIP.TabIndex = 5;
            this.lRemIP.Text = "Rem IP:";
            // 
            // lRemPort
            // 
            this.lRemPort.AutoSize = true;
            this.lRemPort.Location = new System.Drawing.Point(15, 140);
            this.lRemPort.Name = "lRemPort";
            this.lRemPort.Size = new System.Drawing.Size(54, 13);
            this.lRemPort.TabIndex = 6;
            this.lRemPort.Text = "Rem Port:";
            // 
            // tLocalIP
            // 
            this.tLocalIP.Enabled = false;
            this.tLocalIP.Location = new System.Drawing.Point(75, 51);
            this.tLocalIP.Name = "tLocalIP";
            this.tLocalIP.Size = new System.Drawing.Size(78, 20);
            this.tLocalIP.TabIndex = 7;
            this.tLocalIP.Text = "0.0.0.0";
            this.tLocalIP.Leave += new System.EventHandler(this.tRemoteIP_Enter);
            // 
            // lLocaLIP
            // 
            this.lLocaLIP.AutoSize = true;
            this.lLocaLIP.Location = new System.Drawing.Point(20, 54);
            this.lLocaLIP.Name = "lLocaLIP";
            this.lLocaLIP.Size = new System.Drawing.Size(49, 13);
            this.lLocaLIP.TabIndex = 8;
            this.lLocaLIP.Text = "Local IP:";
            // 
            // cbLifeFrame
            // 
            this.cbLifeFrame.AutoSize = true;
            this.cbLifeFrame.Checked = true;
            this.cbLifeFrame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLifeFrame.Location = new System.Drawing.Point(21, 173);
            this.cbLifeFrame.Name = "cbLifeFrame";
            this.cbLifeFrame.Size = new System.Drawing.Size(103, 17);
            this.cbLifeFrame.TabIndex = 10;
            this.cbLifeFrame.Text = "Life comm frame";
            this.cbLifeFrame.ThreeState = true;
            this.cbLifeFrame.UseVisualStyleBackColor = true;
            // 
            // BSConn
            // 
            this.BSConn.BackColor = System.Drawing.SystemColors.GrayText;
            this.BSConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BSConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BSConn.Location = new System.Drawing.Point(86, 16);
            this.BSConn.Name = "BSConn";
            this.BSConn.Size = new System.Drawing.Size(65, 26);
            this.BSConn.TabIndex = 11;
            this.BSConn.TabStop = false;
            this.BSConn.Text = "Connection";
            this.BSConn.UseVisualStyleBackColor = false;
            // 
            // bSService
            // 
            this.bSService.BackColor = System.Drawing.SystemColors.GrayText;
            this.bSService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSService.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bSService.Location = new System.Drawing.Point(17, 16);
            this.bSService.Name = "bSService";
            this.bSService.Size = new System.Drawing.Size(64, 26);
            this.bSService.TabIndex = 12;
            this.bSService.Text = "Service";
            this.bSService.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(7, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 224);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(175, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(637, 224);
            this.panel2.TabIndex = 14;
            // 
            // tSkidIndex
            // 
            this.tSkidIndex.Location = new System.Drawing.Point(152, 9);
            this.tSkidIndex.Name = "tSkidIndex";
            this.tSkidIndex.Size = new System.Drawing.Size(100, 20);
            this.tSkidIndex.TabIndex = 0;
            this.tSkidIndex.Leave += new System.EventHandler(this.tSkidIndex_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Skid Index:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Skid Value:";
            // 
            // tSkidValue
            // 
            this.tSkidValue.Location = new System.Drawing.Point(333, 9);
            this.tSkidValue.Name = "tSkidValue";
            this.tSkidValue.Size = new System.Drawing.Size(100, 20);
            this.tSkidValue.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.bMFPUpdate);
            this.panel3.Controls.Add(this.tSkidValue);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.tSkidIndex);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(15, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(608, 41);
            this.panel3.TabIndex = 15;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(266, 100);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Skid Index";
            // 
            // bMFPUpdate
            // 
            this.bMFPUpdate.Location = new System.Drawing.Point(494, 7);
            this.bMFPUpdate.Name = "bMFPUpdate";
            this.bMFPUpdate.Size = new System.Drawing.Size(96, 23);
            this.bMFPUpdate.TabIndex = 15;
            this.bMFPUpdate.Text = "Update";
            this.bMFPUpdate.UseVisualStyleBackColor = true;
            this.bMFPUpdate.Click += new System.EventHandler(this.bMFPUpdate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(7, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "MFP SKID";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(166, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(38, 20);
            this.textBox2.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(23, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "MFP UPDATE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(115, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Skid ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Derivative Code:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(337, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Colour:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(115, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Body Number:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Roof:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(257, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Track:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(408, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "HoD:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(375, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Spare:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(301, 64);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(28, 20);
            this.textBox3.TabIndex = 25;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(366, 5);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(27, 20);
            this.textBox4.TabIndex = 26;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(193, 93);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(52, 20);
            this.textBox5.TabIndex = 27;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(298, 93);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(13, 20);
            this.textBox6.TabIndex = 28;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(343, 34);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(22, 20);
            this.textBox7.TabIndex = 29;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(446, 5);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(13, 20);
            this.textBox8.TabIndex = 30;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(419, 34);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(40, 20);
            this.textBox9.TabIndex = 31;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.textBox9);
            this.panel4.Controls.Add(this.textBox11);
            this.panel4.Controls.Add(this.textBox7);
            this.panel4.Controls.Add(this.textBox8);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.textBox4);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Location = new System.Drawing.Point(16, 58);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(608, 62);
            this.panel4.TabIndex = 17;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(266, 100);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(100, 20);
            this.textBox11.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(51, 133);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Skid Index";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.textBox10);
            this.panel5.Controls.Add(this.textBox12);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Controls.Add(this.textBox13);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Location = new System.Drawing.Point(16, 152);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(608, 41);
            this.panel5.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label14.Location = new System.Drawing.Point(7, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "VIN_REQ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(494, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Request";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(333, 9);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 20);
            this.textBox10.TabIndex = 3;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(266, 100);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 20);
            this.textBox12.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(268, 12);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Skid ID";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(51, 133);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Skid Index";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(152, 9);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(100, 20);
            this.textBox13.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(84, 12);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Body Number";
            // 
            // PLCSimUDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 242);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bSService);
            this.Controls.Add(this.BSConn);
            this.Controls.Add(this.cbLifeFrame);
            this.Controls.Add(this.lLocaLIP);
            this.Controls.Add(this.tLocalIP);
            this.Controls.Add(this.lRemPort);
            this.Controls.Add(this.lRemIP);
            this.Controls.Add(this.iLocalIP);
            this.Controls.Add(this.tRemotePort);
            this.Controls.Add(this.tRemoteIP);
            this.Controls.Add(this.tLocalPort);
            this.Controls.Add(this.bStartService);
            this.Controls.Add(this.panel1);
            this.Name = "PLCSimUDP";
            this.Text = "PLC Simulator";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStartService;
        private System.Windows.Forms.TextBox tLocalPort;
        private System.Windows.Forms.TextBox tRemoteIP;
        private System.Windows.Forms.TextBox tRemotePort;
        private System.Windows.Forms.Label iLocalIP;
        private System.Windows.Forms.Label lRemIP;
        private System.Windows.Forms.Label lRemPort;
        private System.Windows.Forms.TextBox tLocalIP;
        private System.Windows.Forms.Label lLocaLIP;
        private System.Windows.Forms.CheckBox cbLifeFrame;
        private System.Windows.Forms.Button BSConn;
        private System.Windows.Forms.Button bSService;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bMFPUpdate;
        private System.Windows.Forms.TextBox tSkidValue;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tSkidIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label18;
    }
}

