using System;
using System.Windows.Forms;

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
            this.waitTimeEnd = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.apiUrl = new System.Windows.Forms.TextBox();
            this.waitTimeStart = new System.Windows.Forms.NumericUpDown();
            this.ckbRepeat = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.ckbSuggest = new System.Windows.Forms.CheckBox();
            this.ckbHome = new System.Windows.Forms.CheckBox();
            this.ckbSearch = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.OpenComment = new System.Windows.Forms.Button();
            this.richComments = new System.Windows.Forms.RichTextBox();
            this.richIDAndSearch = new System.Windows.Forms.RichTextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btnIDandSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.sub = new System.Windows.Forms.CheckBox();
            this.nbThread = new System.Windows.Forms.NumericUpDown();
            this.btnEmail = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.APP_URL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.btnSelectCommentFile = new System.Windows.Forms.Button();
            this.txtSoLanMoLink = new System.Windows.Forms.TextBox();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtChannelID = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtChannelName = new System.Windows.Forms.TextBox();
            this.numForStart = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.numTo = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numFrom = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.richKeySuggest = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numForEnd = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbThread)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numForStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(6, 30);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Size = new System.Drawing.Size(689, 377);
            this.dataGrid.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.waitTimeEnd);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.apiUrl);
            this.groupBox1.Controls.Add(this.waitTimeStart);
            this.groupBox1.Controls.Add(this.ckbRepeat);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.checkBox10);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.ckbSuggest);
            this.groupBox1.Controls.Add(this.ckbHome);
            this.groupBox1.Controls.Add(this.ckbSearch);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.numericUpDown4);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.OpenComment);
            this.groupBox1.Controls.Add(this.richComments);
            this.groupBox1.Controls.Add(this.richIDAndSearch);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.btnIDandSearch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.sub);
            this.groupBox1.Controls.Add(this.nbThread);
            this.groupBox1.Controls.Add(this.btnEmail);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(703, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 695);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SETUP";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // waitTimeEnd
            // 
            this.waitTimeEnd.Location = new System.Drawing.Point(379, 427);
            this.waitTimeEnd.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.waitTimeEnd.Name = "waitTimeEnd";
            this.waitTimeEnd.Size = new System.Drawing.Size(59, 22);
            this.waitTimeEnd.TabIndex = 66;
            this.waitTimeEnd.Value = new decimal(new int[] {
            900,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(239, 563);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 15);
            this.label20.TabIndex = 64;
            this.label20.Text = "API URL:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(225, 429);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 15);
            this.label21.TabIndex = 65;
            this.label21.Text = "wait time(s):";
            // 
            // apiUrl
            // 
            this.apiUrl.Location = new System.Drawing.Point(305, 560);
            this.apiUrl.Name = "apiUrl";
            this.apiUrl.Size = new System.Drawing.Size(129, 22);
            this.apiUrl.TabIndex = 64;
            this.apiUrl.Text = "127.0.0.1:19995";
            // 
            // waitTimeStart
            // 
            this.waitTimeStart.Location = new System.Drawing.Point(305, 427);
            this.waitTimeStart.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.waitTimeStart.Name = "waitTimeStart";
            this.waitTimeStart.Size = new System.Drawing.Size(68, 22);
            this.waitTimeStart.TabIndex = 64;
            this.waitTimeStart.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // ckbRepeat
            // 
            this.ckbRepeat.AutoSize = true;
            this.ckbRepeat.Checked = true;
            this.ckbRepeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRepeat.Location = new System.Drawing.Point(311, 517);
            this.ckbRepeat.Name = "ckbRepeat";
            this.ckbRepeat.Size = new System.Drawing.Size(63, 19);
            this.ckbRepeat.TabIndex = 49;
            this.ckbRepeat.Text = "Repeat";
            this.ckbRepeat.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Teal;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(14, 613);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 72);
            this.button2.TabIndex = 11;
            this.button2.Text = "START";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox10.Location = new System.Drawing.Point(14, 419);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(73, 19);
            this.checkBox10.TabIndex = 48;
            this.checkBox10.Text = "Skip ads";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStop.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(243, 613);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(194, 72);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // ckbSuggest
            // 
            this.ckbSuggest.AutoSize = true;
            this.ckbSuggest.Checked = true;
            this.ckbSuggest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSuggest.Location = new System.Drawing.Point(311, 485);
            this.ckbSuggest.Name = "ckbSuggest";
            this.ckbSuggest.Size = new System.Drawing.Size(103, 19);
            this.ckbSuggest.TabIndex = 46;
            this.ckbSuggest.Text = "Suggest views";
            this.ckbSuggest.UseVisualStyleBackColor = true;
            // 
            // ckbHome
            // 
            this.ckbHome.AutoSize = true;
            this.ckbHome.Checked = true;
            this.ckbHome.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbHome.Location = new System.Drawing.Point(173, 516);
            this.ckbHome.Name = "ckbHome";
            this.ckbHome.Size = new System.Drawing.Size(89, 19);
            this.ckbHome.TabIndex = 45;
            this.ckbHome.Text = "Home views";
            this.ckbHome.UseVisualStyleBackColor = true;
            // 
            // ckbSearch
            // 
            this.ckbSearch.AutoSize = true;
            this.ckbSearch.Checked = true;
            this.ckbSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSearch.Location = new System.Drawing.Point(173, 485);
            this.ckbSearch.Name = "ckbSearch";
            this.ckbSearch.Size = new System.Drawing.Size(97, 19);
            this.ckbSearch.TabIndex = 44;
            this.ckbSearch.Text = "Search views";
            this.ckbSearch.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(119, 464);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(329, 1);
            this.panel3.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 453);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 15);
            this.label10.TabIndex = 38;
            this.label10.Text = "View Config:";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(379, 383);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(58, 22);
            this.numericUpDown4.TabIndex = 43;
            this.numericUpDown4.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox5.Location = new System.Drawing.Point(244, 384);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(97, 19);
            this.checkBox5.TabIndex = 42;
            this.checkBox5.Text = "Dislike (%):";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(156, 383);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(58, 22);
            this.numericUpDown3.TabIndex = 41;
            this.numericUpDown3.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.Location = new System.Drawing.Point(14, 383);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(82, 19);
            this.checkBox4.TabIndex = 40;
            this.checkBox4.Text = "Like (%):";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(106, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(329, 1);
            this.panel2.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(10, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 15);
            this.label9.TabIndex = 38;
            this.label9.Text = "Input Config:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(119, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 1);
            this.panel1.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Action Config:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 563);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 15);
            this.label7.TabIndex = 35;
            this.label7.Text = "Threads:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "Comments:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 15);
            this.label8.TabIndex = 33;
            this.label8.Text = "ID and keywords:";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(12, 485);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(104, 19);
            this.checkBox3.TabIndex = 32;
            this.checkBox3.Text = "Channel views";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 516);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(100, 19);
            this.checkBox2.TabIndex = 31;
            this.checkBox2.Text = "Playlist views";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(379, 347);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(58, 22);
            this.numericUpDown2.TabIndex = 30;
            this.numericUpDown2.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(156, 347);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 22);
            this.numericUpDown1.TabIndex = 29;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(244, 348);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(114, 19);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Comments (%):";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // OpenComment
            // 
            this.OpenComment.Location = new System.Drawing.Point(351, 211);
            this.OpenComment.Name = "OpenComment";
            this.OpenComment.Size = new System.Drawing.Size(86, 35);
            this.OpenComment.TabIndex = 27;
            this.OpenComment.Text = "OPEN";
            this.OpenComment.UseVisualStyleBackColor = true;
            this.OpenComment.Click += new System.EventHandler(this.OpenComment_Click);
            // 
            // richComments
            // 
            this.richComments.Location = new System.Drawing.Point(61, 249);
            this.richComments.Name = "richComments";
            this.richComments.Size = new System.Drawing.Size(376, 57);
            this.richComments.TabIndex = 26;
            this.richComments.Text = "";
            // 
            // richIDAndSearch
            // 
            this.richIDAndSearch.Location = new System.Drawing.Point(58, 143);
            this.richIDAndSearch.Name = "richIDAndSearch";
            this.richIDAndSearch.Size = new System.Drawing.Size(376, 57);
            this.richIDAndSearch.TabIndex = 24;
            this.richIDAndSearch.Text = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(97, 57);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(247, 22);
            this.textBox3.TabIndex = 23;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // btnIDandSearch
            // 
            this.btnIDandSearch.Location = new System.Drawing.Point(351, 105);
            this.btnIDandSearch.Name = "btnIDandSearch";
            this.btnIDandSearch.Size = new System.Drawing.Size(86, 35);
            this.btnIDandSearch.TabIndex = 21;
            this.btnIDandSearch.Text = "OPEN";
            this.btnIDandSearch.UseVisualStyleBackColor = true;
            this.btnIDandSearch.Click += new System.EventHandler(this.btnIDandSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Add emails:";
            // 
            // sub
            // 
            this.sub.AutoSize = true;
            this.sub.Checked = true;
            this.sub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sub.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sub.Location = new System.Drawing.Point(14, 348);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(117, 19);
            this.sub.TabIndex = 15;
            this.sub.Text = "Subscriber (%):";
            this.sub.UseVisualStyleBackColor = true;
            this.sub.CheckedChanged += new System.EventHandler(this.sub_CheckedChanged);
            // 
            // nbThread
            // 
            this.nbThread.Location = new System.Drawing.Point(72, 561);
            this.nbThread.Name = "nbThread";
            this.nbThread.Size = new System.Drawing.Size(117, 22);
            this.nbThread.TabIndex = 4;
            this.nbThread.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(351, 52);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(86, 35);
            this.btnEmail.TabIndex = 0;
            this.btnEmail.Text = "OPEN";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(234, 772);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 40);
            this.button1.TabIndex = 19;
            this.button1.Text = "WAITTING";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnWait_Click);
            // 
            // APP_URL
            // 
            this.APP_URL.Location = new System.Drawing.Point(836, 789);
            this.APP_URL.Name = "APP_URL";
            this.APP_URL.Size = new System.Drawing.Size(116, 22);
            this.APP_URL.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1184, 797);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "APP_URL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(997, 797);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "ID VIDEO";
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(433, 782);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(170, 22);
            this.txtChannel.TabIndex = 8;
            // 
            // btnSelectCommentFile
            // 
            this.btnSelectCommentFile.Location = new System.Drawing.Point(22, 774);
            this.btnSelectCommentFile.Name = "btnSelectCommentFile";
            this.btnSelectCommentFile.Size = new System.Drawing.Size(192, 38);
            this.btnSelectCommentFile.TabIndex = 6;
            this.btnSelectCommentFile.Text = "Chọn file comment";
            this.btnSelectCommentFile.UseVisualStyleBackColor = true;
            this.btnSelectCommentFile.Click += new System.EventHandler(this.btnSelectCommentFile_Click);
            // 
            // txtSoLanMoLink
            // 
            this.txtSoLanMoLink.Location = new System.Drawing.Point(1098, 797);
            this.txtSoLanMoLink.Name = "txtSoLanMoLink";
            this.txtSoLanMoLink.Size = new System.Drawing.Size(79, 22);
            this.txtSoLanMoLink.TabIndex = 4;
            this.txtSoLanMoLink.Text = "3";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(632, 782);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(170, 22);
            this.txtKeyword.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(880, 771);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "Num of Des";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(205, 737);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(93, 24);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "View PLL";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.btnPLL_CheckedChanged_1);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(14, 740);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(124, 24);
            this.radioButton2.TabIndex = 9;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "View Đề Xuất";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.btnViewDX_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(395, 740);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(120, 24);
            this.radioButton3.TabIndex = 10;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "View PLL DX";
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.btnPLL_Video_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel4.Location = new System.Drawing.Point(124, 112);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(329, 1);
            this.panel4.TabIndex = 41;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 15);
            this.label11.TabIndex = 40;
            this.label11.Text = "Suggest views:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.numForEnd);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txtChannelID);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtChannelName);
            this.groupBox2.Controls.Add(this.numForStart);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.numTo);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.numFrom);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.richKeySuggest);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.checkBox13);
            this.groupBox2.Controls.Add(this.checkBox12);
            this.groupBox2.Controls.Add(this.checkBox11);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 414);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(689, 290);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VIEWS DETAIL";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(444, 77);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 15);
            this.label19.TabIndex = 63;
            this.label19.Text = "Channel ID:";
            // 
            // txtChannelID
            // 
            this.txtChannelID.Location = new System.Drawing.Point(541, 74);
            this.txtChannelID.Name = "txtChannelID";
            this.txtChannelID.Size = new System.Drawing.Size(129, 22);
            this.txtChannelID.TabIndex = 62;
            this.txtChannelID.Text = "jack-slives9930";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(445, 50);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 15);
            this.label18.TabIndex = 61;
            this.label18.Text = "Channel Name:";
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(542, 47);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(129, 22);
            this.txtChannelName.TabIndex = 50;
            this.txtChannelName.Text = "JACK-s LIVES";
            // 
            // numForStart
            // 
            this.numForStart.Location = new System.Drawing.Point(124, 74);
            this.numForStart.Name = "numForStart";
            this.numForStart.Size = new System.Drawing.Size(117, 22);
            this.numForStart.TabIndex = 60;
            this.numForStart.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(37, 76);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 15);
            this.label17.TabIndex = 59;
            this.label17.Text = "Num for Acc:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(255, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 15);
            this.label16.TabIndex = 58;
            this.label16.Text = "-";
            // 
            // numTo
            // 
            this.numTo.Location = new System.Drawing.Point(272, 48);
            this.numTo.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numTo.Name = "numTo";
            this.numTo.Size = new System.Drawing.Size(117, 22);
            this.numTo.TabIndex = 57;
            this.numTo.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(18, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 15);
            this.label15.TabIndex = 56;
            this.label15.Text = "Total time(%):";
            // 
            // numFrom
            // 
            this.numFrom.Location = new System.Drawing.Point(124, 46);
            this.numFrom.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numFrom.Name = "numFrom";
            this.numFrom.Size = new System.Drawing.Size(117, 22);
            this.numFrom.TabIndex = 55;
            this.numFrom.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(15, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 15);
            this.label14.TabIndex = 53;
            this.label14.Text = "Time views:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.Location = new System.Drawing.Point(124, 30);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(329, 1);
            this.panel5.TabIndex = 54;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(607, 158);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(64, 24);
            this.button5.TabIndex = 49;
            this.button5.Text = "OPEN";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // richKeySuggest
            // 
            this.richKeySuggest.Location = new System.Drawing.Point(15, 182);
            this.richKeySuggest.Name = "richKeySuggest";
            this.richKeySuggest.Size = new System.Drawing.Size(655, 98);
            this.richKeySuggest.TabIndex = 52;
            this.richKeySuggest.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(147, 15);
            this.label12.TabIndex = 49;
            this.label12.Text = "Videos list / Keyword list:";
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Checked = true;
            this.checkBox13.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox13.Location = new System.Drawing.Point(296, 130);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(112, 19);
            this.checkBox13.TabIndex = 51;
            this.checkBox13.Text = "Default suggest";
            this.checkBox13.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Checked = true;
            this.checkBox12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox12.Location = new System.Drawing.Point(541, 130);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(114, 19);
            this.checkBox12.TabIndex = 50;
            this.checkBox12.Text = "Custom suggest";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point(16, 130);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(116, 19);
            this.checkBox11.TabIndex = 49;
            this.checkBox11.Text = "Random suggest";
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(2, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 15);
            this.label13.TabIndex = 49;
            this.label13.Text = "PROCESSES:";
            // 
            // numForEnd
            // 
            this.numForEnd.Location = new System.Drawing.Point(272, 74);
            this.numForEnd.Name = "numForEnd";
            this.numForEnd.Size = new System.Drawing.Size(117, 22);
            this.numForEnd.TabIndex = 64;
            this.numForEnd.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 708);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.txtChannel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.APP_URL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.txtSoLanMoLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSelectCommentFile);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YOUTUBE VIEW";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbThread)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numForStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nbThread;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox sub;
        private System.Windows.Forms.TextBox APP_URL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSoLanMoLink;
        private System.Windows.Forms.Button btnSelectCommentFile;
        private DataGridViewCellEventHandler dataGrid_CellContentClick;
        private EventHandler textBox1_TextChanged;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private Button button2;
        private TextBox textBox3;
        private Button btnIDandSearch;
        private Label label4;
        private RichTextBox richIDAndSearch;
        private Button OpenComment;
        private RichTextBox richComments;
        private Panel panel2;
        private Label label9;
        private Panel panel1;
        private Label label1;
        private Label label7;
        private Label label2;
        private Label label8;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private CheckBox checkBox1;
        private CheckBox checkBox10;
        private CheckBox ckbSuggest;
        private CheckBox ckbHome;
        private CheckBox ckbSearch;
        private Panel panel3;
        private Label label10;
        private NumericUpDown numericUpDown4;
        private CheckBox checkBox5;
        private NumericUpDown numericUpDown3;
        private CheckBox checkBox4;
        private Panel panel4;
        private Label label11;
        private GroupBox groupBox2;
        private RichTextBox richKeySuggest;
        private Label label12;
        private CheckBox checkBox13;
        private CheckBox checkBox12;
        private CheckBox checkBox11;
        private Label label13;
        private Button button5;
        private CheckBox ckbRepeat;
        private NumericUpDown numForStart;
        private Label label17;
        private Label label16;
        private NumericUpDown numTo;
        private Label label15;
        private NumericUpDown numFrom;
        private Label label14;
        private Panel panel5;
        private Label label19;
        private TextBox txtChannelID;
        private Label label18;
        private TextBox txtChannelName;
        private Label label20;
        private TextBox apiUrl;
        private NumericUpDown waitTimeEnd;
        private Label label21;
        private NumericUpDown waitTimeStart;
        private NumericUpDown numForEnd;
    }
}

