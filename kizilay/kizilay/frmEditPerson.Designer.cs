namespace kizilay
{
    partial class frmEditPerson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditPerson));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.mskPhone = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chckNotWorking = new System.Windows.Forms.RadioButton();
            this.chckWorking = new System.Windows.Forms.RadioButton();
            this.numSalary = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txtJob = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbSocialSecurity = new System.Windows.Forms.ComboBox();
            this.cmbEducationalStatus = new System.Windows.Forms.ComboBox();
            this.txtFatherName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMotherName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFatherNo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.cmbMarried = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chckWomen = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.chckMan = new System.Windows.Forms.RadioButton();
            this.dateBirthDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCitizenship = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPlaceOfBirth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTcNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalary)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.mskPhone);
            this.groupBox3.Location = new System.Drawing.Point(526, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 103);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "İletişim Bilgileri";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(136, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "Telefon Numarası:";
            // 
            // mskPhone
            // 
            this.mskPhone.Location = new System.Drawing.Point(12, 51);
            this.mskPhone.Mask = "(999) 000-0000";
            this.mskPhone.Name = "mskPhone";
            this.mskPhone.Size = new System.Drawing.Size(213, 26);
            this.mskPhone.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.numSalary);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtJob);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(260, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 280);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "İş Bilgileri";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chckNotWorking);
            this.panel2.Controls.Add(this.chckWorking);
            this.panel2.Location = new System.Drawing.Point(10, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 31);
            this.panel2.TabIndex = 15;
            // 
            // chckNotWorking
            // 
            this.chckNotWorking.AutoSize = true;
            this.chckNotWorking.Checked = true;
            this.chckNotWorking.Location = new System.Drawing.Point(111, 5);
            this.chckNotWorking.Name = "chckNotWorking";
            this.chckNotWorking.Size = new System.Drawing.Size(103, 24);
            this.chckNotWorking.TabIndex = 17;
            this.chckNotWorking.TabStop = true;
            this.chckNotWorking.Text = "Çalışmıyor";
            this.chckNotWorking.UseVisualStyleBackColor = true;
            this.chckNotWorking.CheckedChanged += new System.EventHandler(this.chckNotWorking_CheckedChanged);
            // 
            // chckWorking
            // 
            this.chckWorking.AutoSize = true;
            this.chckWorking.Checked = true;
            this.chckWorking.Location = new System.Drawing.Point(3, 3);
            this.chckWorking.Name = "chckWorking";
            this.chckWorking.Size = new System.Drawing.Size(90, 24);
            this.chckWorking.TabIndex = 16;
            this.chckWorking.TabStop = true;
            this.chckWorking.Text = "Çalışıyor";
            this.chckWorking.UseVisualStyleBackColor = true;
            this.chckWorking.CheckedChanged += new System.EventHandler(this.chckWorking_CheckedChanged);
            // 
            // numSalary
            // 
            this.numSalary.Location = new System.Drawing.Point(10, 160);
            this.numSalary.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSalary.Name = "numSalary";
            this.numSalary.Size = new System.Drawing.Size(199, 26);
            this.numSalary.TabIndex = 14;
            this.numSalary.Value = new decimal(new int[] {
            1850,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "Çalışma Durumu";
            // 
            // txtJob
            // 
            this.txtJob.Location = new System.Drawing.Point(10, 103);
            this.txtJob.MaxLength = 25;
            this.txtJob.Name = "txtJob";
            this.txtJob.Size = new System.Drawing.Size(199, 26);
            this.txtJob.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Maaş:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Çalıştığı İş:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbSocialSecurity);
            this.groupBox4.Controls.Add(this.cmbEducationalStatus);
            this.groupBox4.Controls.Add(this.txtFatherName);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtMotherName);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txtFatherNo);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Location = new System.Drawing.Point(260, 308);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(513, 226);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Aile ve Diğer Bilgiler";
            // 
            // cmbSocialSecurity
            // 
            this.cmbSocialSecurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSocialSecurity.FormattingEnabled = true;
            this.cmbSocialSecurity.Location = new System.Drawing.Point(281, 162);
            this.cmbSocialSecurity.Name = "cmbSocialSecurity";
            this.cmbSocialSecurity.Size = new System.Drawing.Size(184, 28);
            this.cmbSocialSecurity.TabIndex = 21;
            // 
            // cmbEducationalStatus
            // 
            this.cmbEducationalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEducationalStatus.FormattingEnabled = true;
            this.cmbEducationalStatus.Location = new System.Drawing.Point(25, 163);
            this.cmbEducationalStatus.Name = "cmbEducationalStatus";
            this.cmbEducationalStatus.Size = new System.Drawing.Size(184, 28);
            this.cmbEducationalStatus.TabIndex = 20;
            // 
            // txtFatherName
            // 
            this.txtFatherName.Location = new System.Drawing.Point(281, 110);
            this.txtFatherName.MaxLength = 25;
            this.txtFatherName.Name = "txtFatherName";
            this.txtFatherName.Size = new System.Drawing.Size(183, 26);
            this.txtFatherName.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(277, 87);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 20);
            this.label15.TabIndex = 4;
            this.label15.Text = "Baba Adı:";
            // 
            // txtMotherName
            // 
            this.txtMotherName.Location = new System.Drawing.Point(25, 110);
            this.txtMotherName.MaxLength = 25;
            this.txtMotherName.Name = "txtMotherName";
            this.txtMotherName.Size = new System.Drawing.Size(183, 26);
            this.txtMotherName.TabIndex = 18;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(277, 141);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(132, 20);
            this.label17.TabIndex = 5;
            this.label17.Text = "Sosyal Güvence:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 141);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 20);
            this.label16.TabIndex = 6;
            this.label16.Text = "Eğitim Bilgisi:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 87);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 20);
            this.label14.TabIndex = 7;
            this.label14.Text = "Anne Adı:";
            // 
            // txtFatherNo
            // 
            this.txtFatherNo.Location = new System.Drawing.Point(25, 53);
            this.txtFatherNo.MaxLength = 11;
            this.txtFatherNo.Name = "txtFatherNo";
            this.txtFatherNo.Size = new System.Drawing.Size(183, 26);
            this.txtFatherNo.TabIndex = 17;
            this.txtFatherNo.TextChanged += new System.EventHandler(this.txtFatherNo_TextChanged);
            this.txtFatherNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTcNo_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(24, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(192, 20);
            this.label18.TabIndex = 8;
            this.label18.Text = "Aile Reisinin T.C Numarası:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Location = new System.Drawing.Point(626, 541);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(147, 37);
            this.btnUpdate.TabIndex = 27;
            this.btnUpdate.Text = "Güncelle";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTransfer);
            this.groupBox1.Controls.Add(this.cmbMarried);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.dateBirthDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtCitizenship);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPlaceOfBirth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSurname);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTcNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 525);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kişi Bilgileri";
            // 
            // btnTransfer
            // 
            this.btnTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransfer.Location = new System.Drawing.Point(198, 49);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(29, 26);
            this.btnTransfer.TabIndex = 23;
            this.btnTransfer.Text = ">";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // cmbMarried
            // 
            this.cmbMarried.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarried.FormattingEnabled = true;
            this.cmbMarried.Items.AddRange(new object[] {
            "Evli",
            "Bekar",
            "Dul"});
            this.cmbMarried.Location = new System.Drawing.Point(9, 462);
            this.cmbMarried.MaxDropDownItems = 9;
            this.cmbMarried.Name = "cmbMarried";
            this.cmbMarried.Size = new System.Drawing.Size(183, 28);
            this.cmbMarried.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chckWomen);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.chckMan);
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 51);
            this.panel1.TabIndex = 9;
            // 
            // chckWomen
            // 
            this.chckWomen.AutoSize = true;
            this.chckWomen.Location = new System.Drawing.Point(121, 24);
            this.chckWomen.Name = "chckWomen";
            this.chckWomen.Size = new System.Drawing.Size(71, 24);
            this.chckWomen.TabIndex = 8;
            this.chckWomen.Text = "Kadın";
            this.chckWomen.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Cinsiyet";
            // 
            // chckMan
            // 
            this.chckMan.AutoSize = true;
            this.chckMan.Checked = true;
            this.chckMan.Location = new System.Drawing.Point(11, 24);
            this.chckMan.Name = "chckMan";
            this.chckMan.Size = new System.Drawing.Size(69, 24);
            this.chckMan.TabIndex = 7;
            this.chckMan.TabStop = true;
            this.chckMan.Text = "Erkek";
            this.chckMan.UseVisualStyleBackColor = true;
            // 
            // dateBirthDate
            // 
            this.dateBirthDate.Location = new System.Drawing.Point(9, 284);
            this.dateBirthDate.Name = "dateBirthDate";
            this.dateBirthDate.Size = new System.Drawing.Size(184, 26);
            this.dateBirthDate.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Doğum Tarihi:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 439);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Medeni Hali:";
            // 
            // txtCitizenship
            // 
            this.txtCitizenship.Location = new System.Drawing.Point(9, 352);
            this.txtCitizenship.MaxLength = 5;
            this.txtCitizenship.Name = "txtCitizenship";
            this.txtCitizenship.Size = new System.Drawing.Size(183, 26);
            this.txtCitizenship.TabIndex = 6;
            this.txtCitizenship.Text = "T.C";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Uyruk:";
            // 
            // txtPlaceOfBirth
            // 
            this.txtPlaceOfBirth.Location = new System.Drawing.Point(10, 219);
            this.txtPlaceOfBirth.MaxLength = 25;
            this.txtPlaceOfBirth.Name = "txtPlaceOfBirth";
            this.txtPlaceOfBirth.Size = new System.Drawing.Size(183, 26);
            this.txtPlaceOfBirth.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Doğum Yeri:";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(10, 160);
            this.txtSurname.MaxLength = 25;
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(183, 26);
            this.txtSurname.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Soyadı:";
            // 
            // txtTcNo
            // 
            this.txtTcNo.Location = new System.Drawing.Point(11, 52);
            this.txtTcNo.MaxLength = 11;
            this.txtTcNo.Name = "txtTcNo";
            this.txtTcNo.Size = new System.Drawing.Size(183, 26);
            this.txtTcNo.TabIndex = 1;
            this.txtTcNo.TextChanged += new System.EventHandler(this.txtTcNo_TextChanged);
            this.txtTcNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTcNo_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "T.C No:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(10, 104);
            this.txtName.MaxLength = 25;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(183, 26);
            this.txtName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adı:";
            // 
            // frmEditPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(778, 587);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmEditPerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Personel düzenleme ekranı";
            this.Load += new System.EventHandler(this.frmEditPerson_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalary)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox mskPhone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numSalary;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtJob;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbSocialSecurity;
        private System.Windows.Forms.ComboBox cmbEducationalStatus;
        private System.Windows.Forms.TextBox txtFatherName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMotherName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtFatherNo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.ComboBox cmbMarried;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton chckWomen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton chckMan;
        private System.Windows.Forms.DateTimePicker dateBirthDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCitizenship;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPlaceOfBirth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTcNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton chckNotWorking;
        private System.Windows.Forms.RadioButton chckWorking;
    }
}