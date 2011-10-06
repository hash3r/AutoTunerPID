namespace ViewPID
{
    partial class ViewPIDForm
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
          this.tmrPID_Ctrl = new System.Windows.Forms.Timer(this.components);
          this.trackBarSP = new System.Windows.Forms.TrackBar();
          this.progBarOut = new System.Windows.Forms.ProgressBar();
          this.label1 = new System.Windows.Forms.Label();
          this.label2 = new System.Windows.Forms.Label();
          this.lblSP = new System.Windows.Forms.Label();
          this.lblOutput = new System.Windows.Forms.Label();
          this.trackBarInterval = new System.Windows.Forms.TrackBar();
          this.label5 = new System.Windows.Forms.Label();
          this.lblInterval = new System.Windows.Forms.Label();
          this.tmrPV = new System.Windows.Forms.Timer(this.components);
          this.label3 = new System.Windows.Forms.Label();
          this.tbKp = new System.Windows.Forms.TextBox();
          this.tbKi = new System.Windows.Forms.TextBox();
          this.label4 = new System.Windows.Forms.Label();
          this.tbKd = new System.Windows.Forms.TextBox();
          this.label6 = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.lblPV = new System.Windows.Forms.Label();
          this.progBarPV = new System.Windows.Forms.ProgressBar();
          this.label8 = new System.Windows.Forms.Label();
          this.label9 = new System.Windows.Forms.Label();
          this.label10 = new System.Windows.Forms.Label();
          this.lblIntegral = new System.Windows.Forms.Label();
          this.lblDeriv = new System.Windows.Forms.Label();
          this.lblError = new System.Windows.Forms.Label();
          this.cbNoise = new System.Windows.Forms.CheckBox();
          this.nudNoisePercent = new System.Windows.Forms.NumericUpDown();
          this.pictureBox1 = new System.Windows.Forms.PictureBox();
          this.label11 = new System.Windows.Forms.Label();
          this.label12 = new System.Windows.Forms.Label();
          this.label13 = new System.Windows.Forms.Label();
          this.btnStart = new System.Windows.Forms.Button();
          this.tmrChart = new System.Windows.Forms.Timer(this.components);
          this.tmrNoise = new System.Windows.Forms.Timer(this.components);
          this.btnStartAutoTunerPID = new System.Windows.Forms.Button();
          ((System.ComponentModel.ISupportInitialize)(this.trackBarSP)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.nudNoisePercent)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
          this.SuspendLayout();
          // 
          // tmrPID_Ctrl
          // 
          this.tmrPID_Ctrl.Enabled = true;
          this.tmrPID_Ctrl.Tick += new System.EventHandler(this.tmrPID_Ctrl_Tick);
          // 
          // trackBarSP
          // 
          this.trackBarSP.Location = new System.Drawing.Point(5, 188);
          this.trackBarSP.Maximum = 1000;
          this.trackBarSP.Name = "trackBarSP";
          this.trackBarSP.Size = new System.Drawing.Size(299, 45);
          this.trackBarSP.TabIndex = 1;
          this.trackBarSP.TickFrequency = 50;
          this.trackBarSP.Scroll += new System.EventHandler(this.trackBarSP_Scroll);
          // 
          // progBarOut
          // 
          this.progBarOut.Location = new System.Drawing.Point(12, 266);
          this.progBarOut.Maximum = 1000;
          this.progBarOut.Name = "progBarOut";
          this.progBarOut.Size = new System.Drawing.Size(285, 19);
          this.progBarOut.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
          this.progBarOut.TabIndex = 2;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(18, 172);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(69, 13);
          this.label1.TabIndex = 3;
          this.label1.Text = "SP (Setpoint)";
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(18, 247);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(62, 13);
          this.label2.TabIndex = 4;
          this.label2.Text = "MV (output)";
          // 
          // lblSP
          // 
          this.lblSP.AutoSize = true;
          this.lblSP.Location = new System.Drawing.Point(93, 172);
          this.lblSP.Name = "lblSP";
          this.lblSP.Size = new System.Drawing.Size(16, 13);
          this.lblSP.TabIndex = 5;
          this.lblSP.Text = "---";
          // 
          // lblOutput
          // 
          this.lblOutput.AutoSize = true;
          this.lblOutput.Location = new System.Drawing.Point(86, 247);
          this.lblOutput.Name = "lblOutput";
          this.lblOutput.Size = new System.Drawing.Size(16, 13);
          this.lblOutput.TabIndex = 6;
          this.lblOutput.Text = "---";
          // 
          // trackBarInterval
          // 
          this.trackBarInterval.Location = new System.Drawing.Point(5, 322);
          this.trackBarInterval.Maximum = 1000;
          this.trackBarInterval.Minimum = 1;
          this.trackBarInterval.Name = "trackBarInterval";
          this.trackBarInterval.Size = new System.Drawing.Size(299, 45);
          this.trackBarInterval.TabIndex = 7;
          this.trackBarInterval.TickFrequency = 50;
          this.trackBarInterval.Value = 1;
          this.trackBarInterval.Scroll += new System.EventHandler(this.trackBarInterval_Scroll);
          // 
          // label5
          // 
          this.label5.AutoSize = true;
          this.label5.Location = new System.Drawing.Point(18, 306);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(64, 13);
          this.label5.TabIndex = 8;
          this.label5.Text = "Interval (ms)";
          // 
          // lblInterval
          // 
          this.lblInterval.AutoSize = true;
          this.lblInterval.Location = new System.Drawing.Point(88, 306);
          this.lblInterval.Name = "lblInterval";
          this.lblInterval.Size = new System.Drawing.Size(16, 13);
          this.lblInterval.TabIndex = 9;
          this.lblInterval.Text = "---";
          // 
          // tmrPV
          // 
          this.tmrPV.Enabled = true;
          this.tmrPV.Interval = 17;
          this.tmrPV.Tick += new System.EventHandler(this.tmrPV_Tick);
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Location = new System.Drawing.Point(18, 9);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(20, 13);
          this.label3.TabIndex = 10;
          this.label3.Text = "Kp";
          // 
          // tbKp
          // 
          this.tbKp.Location = new System.Drawing.Point(44, 6);
          this.tbKp.Name = "tbKp";
          this.tbKp.Size = new System.Drawing.Size(52, 20);
          this.tbKp.TabIndex = 11;
          this.tbKp.Text = "0.02";
          this.tbKp.TextChanged += new System.EventHandler(this.tbKp_TextChanged);
          // 
          // tbKi
          // 
          this.tbKi.Location = new System.Drawing.Point(44, 32);
          this.tbKi.Name = "tbKi";
          this.tbKi.Size = new System.Drawing.Size(52, 20);
          this.tbKi.TabIndex = 13;
          this.tbKi.Text = "0.007";
          this.tbKi.TextChanged += new System.EventHandler(this.tbKi_TextChanged);
          // 
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.Location = new System.Drawing.Point(18, 35);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(16, 13);
          this.label4.TabIndex = 12;
          this.label4.Text = "Ki";
          // 
          // tbKd
          // 
          this.tbKd.Location = new System.Drawing.Point(44, 58);
          this.tbKd.Name = "tbKd";
          this.tbKd.Size = new System.Drawing.Size(52, 20);
          this.tbKd.TabIndex = 15;
          this.tbKd.Text = "1";
          this.tbKd.TextChanged += new System.EventHandler(this.tbKd_TextChanged);
          // 
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Location = new System.Drawing.Point(18, 61);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(20, 13);
          this.label6.TabIndex = 14;
          this.label6.Text = "Kd";
          // 
          // label7
          // 
          this.label7.AutoSize = true;
          this.label7.Location = new System.Drawing.Point(18, 117);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(98, 13);
          this.label7.TabIndex = 16;
          this.label7.Text = "PV (Process Value)";
          // 
          // lblPV
          // 
          this.lblPV.AutoSize = true;
          this.lblPV.Location = new System.Drawing.Point(122, 117);
          this.lblPV.Name = "lblPV";
          this.lblPV.Size = new System.Drawing.Size(16, 13);
          this.lblPV.TabIndex = 17;
          this.lblPV.Text = "---";
          // 
          // progBarPV
          // 
          this.progBarPV.Location = new System.Drawing.Point(12, 133);
          this.progBarPV.Maximum = 1000;
          this.progBarPV.Name = "progBarPV";
          this.progBarPV.Size = new System.Drawing.Size(285, 19);
          this.progBarPV.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
          this.progBarPV.TabIndex = 18;
          // 
          // label8
          // 
          this.label8.AutoSize = true;
          this.label8.Location = new System.Drawing.Point(112, 9);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(42, 13);
          this.label8.TabIndex = 19;
          this.label8.Text = "Integral";
          // 
          // label9
          // 
          this.label9.AutoSize = true;
          this.label9.Location = new System.Drawing.Point(112, 35);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(55, 13);
          this.label9.TabIndex = 20;
          this.label9.Text = "Derivative";
          // 
          // label10
          // 
          this.label10.AutoSize = true;
          this.label10.Location = new System.Drawing.Point(113, 61);
          this.label10.Name = "label10";
          this.label10.Size = new System.Drawing.Size(29, 13);
          this.label10.TabIndex = 21;
          this.label10.Text = "Error";
          // 
          // lblIntegral
          // 
          this.lblIntegral.AutoSize = true;
          this.lblIntegral.Location = new System.Drawing.Point(178, 9);
          this.lblIntegral.Name = "lblIntegral";
          this.lblIntegral.Size = new System.Drawing.Size(16, 13);
          this.lblIntegral.TabIndex = 22;
          this.lblIntegral.Text = "---";
          // 
          // lblDeriv
          // 
          this.lblDeriv.AutoSize = true;
          this.lblDeriv.Location = new System.Drawing.Point(178, 35);
          this.lblDeriv.Name = "lblDeriv";
          this.lblDeriv.Size = new System.Drawing.Size(16, 13);
          this.lblDeriv.TabIndex = 23;
          this.lblDeriv.Text = "---";
          // 
          // lblError
          // 
          this.lblError.AutoSize = true;
          this.lblError.Location = new System.Drawing.Point(178, 61);
          this.lblError.Name = "lblError";
          this.lblError.Size = new System.Drawing.Size(16, 13);
          this.lblError.TabIndex = 24;
          this.lblError.Text = "---";
          // 
          // cbNoise
          // 
          this.cbNoise.AutoSize = true;
          this.cbNoise.Location = new System.Drawing.Point(21, 89);
          this.cbNoise.Name = "cbNoise";
          this.cbNoise.Size = new System.Drawing.Size(250, 17);
          this.cbNoise.TabIndex = 25;
          this.cbNoise.Text = "Add Noise (Random ±                % of full range )";
          this.cbNoise.UseVisualStyleBackColor = true;
          // 
          // nudNoisePercent
          // 
          this.nudNoisePercent.Location = new System.Drawing.Point(148, 88);
          this.nudNoisePercent.Name = "nudNoisePercent";
          this.nudNoisePercent.Size = new System.Drawing.Size(42, 20);
          this.nudNoisePercent.TabIndex = 26;
          this.nudNoisePercent.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
          this.nudNoisePercent.ValueChanged += new System.EventHandler(this.nudNoisePercent_ValueChanged);
          // 
          // pictureBox1
          // 
          this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.pictureBox1.Location = new System.Drawing.Point(310, 58);
          this.pictureBox1.Name = "pictureBox1";
          this.pictureBox1.Size = new System.Drawing.Size(264, 309);
          this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
          this.pictureBox1.TabIndex = 27;
          this.pictureBox1.TabStop = false;
          // 
          // label11
          // 
          this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.label11.AutoSize = true;
          this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label11.ForeColor = System.Drawing.Color.Green;
          this.label11.Location = new System.Drawing.Point(331, 39);
          this.label11.Name = "label11";
          this.label11.Size = new System.Drawing.Size(69, 13);
          this.label11.TabIndex = 28;
          this.label11.Text = "SP (Setpoint)";
          // 
          // label12
          // 
          this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.label12.AutoSize = true;
          this.label12.ForeColor = System.Drawing.Color.Blue;
          this.label12.Location = new System.Drawing.Point(406, 39);
          this.label12.Name = "label12";
          this.label12.Size = new System.Drawing.Size(98, 13);
          this.label12.TabIndex = 29;
          this.label12.Text = "PV (Provess Value)";
          // 
          // label13
          // 
          this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.label13.AutoSize = true;
          this.label13.ForeColor = System.Drawing.Color.Red;
          this.label13.Location = new System.Drawing.Point(510, 39);
          this.label13.Name = "label13";
          this.label13.Size = new System.Drawing.Size(64, 13);
          this.label13.TabIndex = 30;
          this.label13.Text = "MV (Output)";
          // 
          // btnStart
          // 
          this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnStart.Location = new System.Drawing.Point(449, 4);
          this.btnStart.Name = "btnStart";
          this.btnStart.Size = new System.Drawing.Size(125, 23);
          this.btnStart.TabIndex = 31;
          this.btnStart.Text = "Stop Process";
          this.btnStart.UseVisualStyleBackColor = true;
          this.btnStart.Click += new System.EventHandler(this.btnStartProcess_Click);
          // 
          // tmrChart
          // 
          this.tmrChart.Enabled = true;
          this.tmrChart.Interval = 37;
          this.tmrChart.Tick += new System.EventHandler(this.tmrChart_Tick);
          // 
          // tmrNoise
          // 
          this.tmrNoise.Enabled = true;
          this.tmrNoise.Interval = 61;
          this.tmrNoise.Tick += new System.EventHandler(this.tmrNoise_Tick);
          // 
          // btnStartAutoTunerPID
          // 
          this.btnStartAutoTunerPID.Location = new System.Drawing.Point(310, 4);
          this.btnStartAutoTunerPID.Name = "btnStartAutoTunerPID";
          this.btnStartAutoTunerPID.Size = new System.Drawing.Size(99, 23);
          this.btnStartAutoTunerPID.TabIndex = 32;
          this.btnStartAutoTunerPID.Text = "Start Auto PID";
          this.btnStartAutoTunerPID.UseVisualStyleBackColor = true;
          this.btnStartAutoTunerPID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartAutoTunerPID);
          // 
          // ViewPIDForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(586, 379);
          this.Controls.Add(this.btnStartAutoTunerPID);
          this.Controls.Add(this.btnStart);
          this.Controls.Add(this.label13);
          this.Controls.Add(this.label12);
          this.Controls.Add(this.label11);
          this.Controls.Add(this.pictureBox1);
          this.Controls.Add(this.nudNoisePercent);
          this.Controls.Add(this.cbNoise);
          this.Controls.Add(this.lblError);
          this.Controls.Add(this.lblDeriv);
          this.Controls.Add(this.lblIntegral);
          this.Controls.Add(this.label10);
          this.Controls.Add(this.label9);
          this.Controls.Add(this.label8);
          this.Controls.Add(this.progBarPV);
          this.Controls.Add(this.lblPV);
          this.Controls.Add(this.label7);
          this.Controls.Add(this.tbKd);
          this.Controls.Add(this.label6);
          this.Controls.Add(this.tbKi);
          this.Controls.Add(this.label4);
          this.Controls.Add(this.tbKp);
          this.Controls.Add(this.label3);
          this.Controls.Add(this.lblInterval);
          this.Controls.Add(this.label5);
          this.Controls.Add(this.trackBarInterval);
          this.Controls.Add(this.lblOutput);
          this.Controls.Add(this.lblSP);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.progBarOut);
          this.Controls.Add(this.trackBarSP);
          this.Name = "ViewPIDForm";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "PID example   By Lowell Cady, LowTech LLC ©2009";
          this.Load += new System.EventHandler(this.Form1_Load);
          ((System.ComponentModel.ISupportInitialize)(this.trackBarSP)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.nudNoisePercent)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrPID_Ctrl;
        private System.Windows.Forms.TrackBar trackBarSP;
        private System.Windows.Forms.ProgressBar progBarOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSP;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TrackBar trackBarInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Timer tmrPV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbKp;
        private System.Windows.Forms.TextBox tbKi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbKd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPV;
        private System.Windows.Forms.ProgressBar progBarPV;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblIntegral;
        private System.Windows.Forms.Label lblDeriv;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.CheckBox cbNoise;
        private System.Windows.Forms.NumericUpDown nudNoisePercent;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrChart;
        private System.Windows.Forms.Timer tmrNoise;
        private System.Windows.Forms.Button btnStartAutoTunerPID;
    }
}

