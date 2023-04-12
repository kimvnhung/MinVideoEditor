namespace MinVideoEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openVideoBtn = new Button();
            groupBox1 = new GroupBox();
            videoListLv = new ListView();
            columnHeader1 = new ColumnHeader();
            label1 = new Label();
            randomCb = new CheckBox();
            configGrB = new GroupBox();
            videoEndGrp = new GroupBox();
            videoStartGrp = new GroupBox();
            randomConcatCbb = new CheckBox();
            changeVoiceBtn = new CheckBox();
            insertVoiceCbb = new CheckBox();
            femaleVoiceCbb = new CheckBox();
            maleVoiceCbb = new CheckBox();
            label2 = new Label();
            numericUpDown2 = new NumericUpDown();
            speedVideoCbb = new CheckBox();
            numericUpDown1 = new NumericUpDown();
            amountNud = new NumericUpDown();
            groupBox1.SuspendLayout();
            configGrB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)amountNud).BeginInit();
            SuspendLayout();
            // 
            // openVideoBtn
            // 
            openVideoBtn.Location = new Point(12, 6);
            openVideoBtn.Name = "openVideoBtn";
            openVideoBtn.Size = new Size(84, 22);
            openVideoBtn.TabIndex = 0;
            openVideoBtn.Text = "Mở Video";
            openVideoBtn.UseVisualStyleBackColor = true;
            openVideoBtn.Click += openVideoBtn_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(videoListLv);
            groupBox1.Location = new Point(12, 34);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(256, 404);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Danh sách";
            // 
            // videoListLv
            // 
            videoListLv.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            videoListLv.Location = new Point(6, 22);
            videoListLv.Name = "videoListLv";
            videoListLv.Size = new Size(244, 376);
            videoListLv.TabIndex = 0;
            videoListLv.UseCompatibleStateImageBehavior = false;
            videoListLv.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Width = 240;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(321, 34);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 4;
            label1.Text = "Số lượng";
            // 
            // randomCb
            // 
            randomCb.AutoSize = true;
            randomCb.Location = new Point(321, 60);
            randomCb.Name = "randomCb";
            randomCb.Size = new Size(88, 19);
            randomCb.TabIndex = 6;
            randomCb.Text = "Ngẫu nhiên";
            randomCb.UseVisualStyleBackColor = true;
            randomCb.CheckedChanged += randomCb_CheckedChanged;
            // 
            // configGrB
            // 
            configGrB.Controls.Add(videoEndGrp);
            configGrB.Controls.Add(videoStartGrp);
            configGrB.Controls.Add(randomConcatCbb);
            configGrB.Controls.Add(changeVoiceBtn);
            configGrB.Controls.Add(insertVoiceCbb);
            configGrB.Controls.Add(femaleVoiceCbb);
            configGrB.Controls.Add(maleVoiceCbb);
            configGrB.Controls.Add(label2);
            configGrB.Controls.Add(numericUpDown2);
            configGrB.Controls.Add(speedVideoCbb);
            configGrB.Controls.Add(numericUpDown1);
            configGrB.Location = new Point(321, 85);
            configGrB.Name = "configGrB";
            configGrB.Size = new Size(467, 353);
            configGrB.TabIndex = 7;
            configGrB.TabStop = false;
            configGrB.Text = "Cấu hình ";
            // 
            // videoEndGrp
            // 
            videoEndGrp.Location = new Point(238, 164);
            videoEndGrp.Name = "videoEndGrp";
            videoEndGrp.Size = new Size(164, 189);
            videoEndGrp.TabIndex = 12;
            videoEndGrp.TabStop = false;
            videoEndGrp.Text = "Video cuối";
            // 
            // videoStartGrp
            // 
            videoStartGrp.Location = new Point(68, 164);
            videoStartGrp.Name = "videoStartGrp";
            videoStartGrp.Size = new Size(164, 183);
            videoStartGrp.TabIndex = 11;
            videoStartGrp.TabStop = false;
            videoStartGrp.Text = "Video đầu";
            // 
            // randomConcatCbb
            // 
            randomConcatCbb.AutoSize = true;
            randomConcatCbb.Location = new Point(17, 139);
            randomConcatCbb.Name = "randomConcatCbb";
            randomConcatCbb.Size = new Size(117, 19);
            randomConcatCbb.TabIndex = 10;
            randomConcatCbb.Text = "Ghép ngẫu nhiên";
            randomConcatCbb.UseVisualStyleBackColor = true;
            // 
            // changeVoiceBtn
            // 
            changeVoiceBtn.AutoSize = true;
            changeVoiceBtn.Location = new Point(17, 89);
            changeVoiceBtn.Name = "changeVoiceBtn";
            changeVoiceBtn.Size = new Size(131, 19);
            changeVoiceBtn.TabIndex = 9;
            changeVoiceBtn.Text = "Thay đổi âm thanh: ";
            changeVoiceBtn.UseVisualStyleBackColor = true;
            // 
            // insertVoiceCbb
            // 
            insertVoiceCbb.AutoSize = true;
            insertVoiceCbb.Location = new Point(238, 114);
            insertVoiceCbb.Name = "insertVoiceCbb";
            insertVoiceCbb.Size = new Size(92, 19);
            insertVoiceCbb.TabIndex = 8;
            insertVoiceCbb.Text = "chèn tạp âm";
            insertVoiceCbb.UseVisualStyleBackColor = true;
            // 
            // femaleVoiceCbb
            // 
            femaleVoiceCbb.AutoSize = true;
            femaleVoiceCbb.Location = new Point(158, 114);
            femaleVoiceCbb.Name = "femaleVoiceCbb";
            femaleVoiceCbb.Size = new Size(74, 19);
            femaleVoiceCbb.TabIndex = 7;
            femaleVoiceCbb.Text = "giọng nữ";
            femaleVoiceCbb.UseVisualStyleBackColor = true;
            // 
            // maleVoiceCbb
            // 
            maleVoiceCbb.AutoSize = true;
            maleVoiceCbb.Location = new Point(68, 114);
            maleVoiceCbb.Name = "maleVoiceCbb";
            maleVoiceCbb.Size = new Size(84, 19);
            maleVoiceCbb.TabIndex = 6;
            maleVoiceCbb.Text = "giọng nam";
            maleVoiceCbb.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(123, 62);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 4;
            label2.Text = "->";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(152, 60);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(49, 23);
            numericUpDown2.TabIndex = 3;
            numericUpDown2.TextAlign = HorizontalAlignment.Center;
            // 
            // speedVideoCbb
            // 
            speedVideoCbb.AutoSize = true;
            speedVideoCbb.Location = new Point(17, 35);
            speedVideoCbb.Name = "speedVideoCbb";
            speedVideoCbb.Size = new Size(100, 19);
            speedVideoCbb.TabIndex = 2;
            speedVideoCbb.Text = "Tốc độ video: ";
            speedVideoCbb.UseVisualStyleBackColor = true;
            speedVideoCbb.CheckedChanged += speedVideoCbb_CheckedChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(68, 60);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(49, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            // 
            // amountNud
            // 
            amountNud.Location = new Point(381, 31);
            amountNud.Name = "amountNud";
            amountNud.Size = new Size(57, 23);
            amountNud.TabIndex = 8;
            amountNud.TextAlign = HorizontalAlignment.Center;
            amountNud.ValueChanged += amountNud_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(amountNud);
            Controls.Add(configGrB);
            Controls.Add(randomCb);
            Controls.Add(label1);
            Controls.Add(openVideoBtn);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "MinVideoEditor";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            configGrB.ResumeLayout(false);
            configGrB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)amountNud).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button openVideoBtn;
        private GroupBox groupBox1;
        private ListView videoListLv;
        private Label label1;
        private CheckBox randomCb;
        private GroupBox configGrB;
        private CheckBox randomConcatCbb;
        private CheckBox changeVoiceBtn;
        private CheckBox insertVoiceCbb;
        private CheckBox femaleVoiceCbb;
        private CheckBox maleVoiceCbb;
        private Label label2;
        private NumericUpDown numericUpDown2;
        private CheckBox speedVideoCbb;
        private NumericUpDown numericUpDown1;
        private GroupBox videoEndGrp;
        private GroupBox videoStartGrp;
        private NumericUpDown amountNud;
        private ColumnHeader columnHeader1;
    }
}