namespace GPM_View
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.APP_URL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sub = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nbThread = new System.Windows.Forms.NumericUpDown();
            this.btnEmail = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSoLanMoLink = new System.Windows.Forms.TextBox();
            this.btnStartKichBan1 = new System.Windows.Forms.Button();
            this.btnStartKichBan2 = new System.Windows.Forms.Button();
            this.btnSelectCommentFile = new System.Windows.Forms.Button();
            this.btn_kichban2b = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbThread)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(5, 8);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(591, 241);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.APP_URL);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.sub);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtChannel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nbThread);
            this.groupBox1.Controls.Add(this.btnEmail);
            this.groupBox1.Location = new System.Drawing.Point(603, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 468);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cài đặt";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(8, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 35);
            this.button1.TabIndex = 19;
            this.button1.Text = "WAITTING";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // APP_URL
            // 
            this.APP_URL.Location = new System.Drawing.Point(75, 181);
            this.APP_URL.Name = "APP_URL";
            this.APP_URL.Size = new System.Drawing.Size(90, 20);
            this.APP_URL.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "APP_URL";
            // 
            // sub
            // 
            this.sub.AutoSize = true;
            this.sub.Location = new System.Drawing.Point(10, 234);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(93, 17);
            this.sub.TabIndex = 15;
            this.sub.Text = "Đăng ký kênh";
            this.sub.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnStop.Location = new System.Drawing.Point(8, 401);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(167, 40);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Channel";
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(22, 156);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(146, 20);
            this.txtChannel.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Keyword";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(22, 102);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(146, 20);
            this.txtKeyword.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thread";
            // 
            // nbThread
            // 
            this.nbThread.Location = new System.Drawing.Point(64, 282);
            this.nbThread.Name = "nbThread";
            this.nbThread.Size = new System.Drawing.Size(90, 20);
            this.nbThread.TabIndex = 4;
            this.nbThread.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(23, 25);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(146, 32);
            this.btnEmail.TabIndex = 0;
            this.btnEmail.Text = "Email";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 256);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(591, 220);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(794, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Số lần mở link ở mô tả:";
            // 
            // txtSoLanMoLink
            // 
            this.txtSoLanMoLink.Location = new System.Drawing.Point(914, 16);
            this.txtSoLanMoLink.Name = "txtSoLanMoLink";
            this.txtSoLanMoLink.Size = new System.Drawing.Size(100, 20);
            this.txtSoLanMoLink.TabIndex = 4;
            this.txtSoLanMoLink.Text = "3";
            // 
            // btnStartKichBan1
            // 
            this.btnStartKichBan1.Location = new System.Drawing.Point(797, 81);
            this.btnStartKichBan1.Name = "btnStartKichBan1";
            this.btnStartKichBan1.Size = new System.Drawing.Size(218, 36);
            this.btnStartKichBan1.TabIndex = 5;
            this.btnStartKichBan1.Text = "View Video De Xuat";
            this.btnStartKichBan1.UseVisualStyleBackColor = true;
            this.btnStartKichBan1.Click += new System.EventHandler(this.btnStartKichBan1_Click);
            // 
            // btnStartKichBan2
            // 
            this.btnStartKichBan2.Location = new System.Drawing.Point(798, 133);
            this.btnStartKichBan2.Name = "btnStartKichBan2";
            this.btnStartKichBan2.Size = new System.Drawing.Size(218, 36);
            this.btnStartKichBan2.TabIndex = 5;
            this.btnStartKichBan2.Text = "View Pll Channel";
            this.btnStartKichBan2.UseVisualStyleBackColor = true;
            this.btnStartKichBan2.Click += new System.EventHandler(this.btnStartKichBan2_Click);
            // 
            // btnSelectCommentFile
            // 
            this.btnSelectCommentFile.Location = new System.Drawing.Point(797, 42);
            this.btnSelectCommentFile.Name = "btnSelectCommentFile";
            this.btnSelectCommentFile.Size = new System.Drawing.Size(218, 33);
            this.btnSelectCommentFile.TabIndex = 6;
            this.btnSelectCommentFile.Text = "Chọn file comment";
            this.btnSelectCommentFile.UseVisualStyleBackColor = true;
            this.btnSelectCommentFile.Click += new System.EventHandler(this.btnSelectCommentFile_Click);
            // 
            // btn_kichban2b
            // 
            this.btn_kichban2b.Location = new System.Drawing.Point(798, 189);
            this.btn_kichban2b.Name = "btn_kichban2b";
            this.btn_kichban2b.Size = new System.Drawing.Size(218, 36);
            this.btn_kichban2b.TabIndex = 7;
            this.btn_kichban2b.Text = "View Link Pll Video Moi";
            this.btn_kichban2b.UseVisualStyleBackColor = true;
            this.btn_kichban2b.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 481);
            this.Controls.Add(this.btn_kichban2b);
            this.Controls.Add(this.btnSelectCommentFile);
            this.Controls.Add(this.btnStartKichBan2);
            this.Controls.Add(this.btnStartKichBan1);
            this.Controls.Add(this.txtSoLanMoLink);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGrid);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube View";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbThread)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nbThread;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox sub;
        private System.Windows.Forms.TextBox APP_URL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSoLanMoLink;
        private System.Windows.Forms.Button btnStartKichBan1;
        private System.Windows.Forms.Button btnStartKichBan2;
        private System.Windows.Forms.Button btnSelectCommentFile;
        private System.Windows.Forms.Button btn_kichban2b;
    }
}

