namespace kizilay
{
    partial class frmEditFamily
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditFamily));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbHousingList = new System.Windows.Forms.ComboBox();
            this.txtTCNo = new System.Windows.Forms.TextBox();
            this.numPersonCount = new System.Windows.Forms.NumericUpDown();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.group = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rchAddress = new System.Windows.Forms.RichTextBox();
            this.cmbNeig = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbTowns = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCities = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numPriority = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPersonCount)).BeginInit();
            this.group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aile reisine ait T.C No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ailede yaşayan kişi sayısı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ev tipi:";
            // 
            // cmbHousingList
            // 
            this.cmbHousingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHousingList.Enabled = false;
            this.cmbHousingList.FormattingEnabled = true;
            this.cmbHousingList.Location = new System.Drawing.Point(10, 180);
            this.cmbHousingList.Name = "cmbHousingList";
            this.cmbHousingList.Size = new System.Drawing.Size(194, 28);
            this.cmbHousingList.TabIndex = 3;
            // 
            // txtTCNo
            // 
            this.txtTCNo.Location = new System.Drawing.Point(6, 55);
            this.txtTCNo.MaxLength = 11;
            this.txtTCNo.Name = "txtTCNo";
            this.txtTCNo.Size = new System.Drawing.Size(194, 26);
            this.txtTCNo.TabIndex = 1;
            this.txtTCNo.TextChanged += new System.EventHandler(this.txtTCNo_TextChanged);
            this.txtTCNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTCNo_KeyPress);
            // 
            // numPersonCount
            // 
            this.numPersonCount.Enabled = false;
            this.numPersonCount.Location = new System.Drawing.Point(10, 121);
            this.numPersonCount.Name = "numPersonCount";
            this.numPersonCount.Size = new System.Drawing.Size(190, 26);
            this.numPersonCount.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(305, 425);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(108, 29);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Güncelle";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // group
            // 
            this.group.Controls.Add(this.label8);
            this.group.Controls.Add(this.numPriority);
            this.group.Controls.Add(this.btnUpdate);
            this.group.Controls.Add(this.label7);
            this.group.Controls.Add(this.numPersonCount);
            this.group.Controls.Add(this.txtTCNo);
            this.group.Controls.Add(this.cmbHousingList);
            this.group.Controls.Add(this.rchAddress);
            this.group.Controls.Add(this.label2);
            this.group.Controls.Add(this.label3);
            this.group.Controls.Add(this.cmbNeig);
            this.group.Controls.Add(this.label1);
            this.group.Controls.Add(this.label6);
            this.group.Controls.Add(this.cmbTowns);
            this.group.Controls.Add(this.label5);
            this.group.Controls.Add(this.cmbCities);
            this.group.Controls.Add(this.label4);
            this.group.Location = new System.Drawing.Point(22, 22);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(453, 476);
            this.group.TabIndex = 2;
            this.group.TabStop = false;
            this.group.Text = "Aile Bilgileri";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 20);
            this.label7.TabIndex = 6;
            // 
            // rchAddress
            // 
            this.rchAddress.Enabled = false;
            this.rchAddress.Location = new System.Drawing.Point(6, 279);
            this.rchAddress.Name = "rchAddress";
            this.rchAddress.Size = new System.Drawing.Size(407, 140);
            this.rchAddress.TabIndex = 7;
            this.rchAddress.Text = "";
            // 
            // cmbNeig
            // 
            this.cmbNeig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNeig.Enabled = false;
            this.cmbNeig.FormattingEnabled = true;
            this.cmbNeig.Location = new System.Drawing.Point(219, 180);
            this.cmbNeig.Name = "cmbNeig";
            this.cmbNeig.Size = new System.Drawing.Size(194, 28);
            this.cmbNeig.TabIndex = 6;
            this.cmbNeig.SelectedIndexChanged += new System.EventHandler(this.cmbNeig_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Mahalle: ";
            // 
            // cmbTowns
            // 
            this.cmbTowns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTowns.Enabled = false;
            this.cmbTowns.FormattingEnabled = true;
            this.cmbTowns.Location = new System.Drawing.Point(219, 119);
            this.cmbTowns.Name = "cmbTowns";
            this.cmbTowns.Size = new System.Drawing.Size(194, 28);
            this.cmbTowns.TabIndex = 5;
            this.cmbTowns.SelectedIndexChanged += new System.EventHandler(this.cmbTowns_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "İlçe: ";
            // 
            // cmbCities
            // 
            this.cmbCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCities.Enabled = false;
            this.cmbCities.FormattingEnabled = true;
            this.cmbCities.Location = new System.Drawing.Point(219, 55);
            this.cmbCities.Name = "cmbCities";
            this.cmbCities.Size = new System.Drawing.Size(194, 28);
            this.cmbCities.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(219, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "İl: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(219, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "İhtiyaç Öncelik Numarası: 0-5";
            // 
            // numPriority
            // 
            this.numPriority.Enabled = false;
            this.numPriority.Location = new System.Drawing.Point(6, 238);
            this.numPriority.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPriority.Name = "numPriority";
            this.numPriority.Size = new System.Drawing.Size(399, 26);
            this.numPriority.TabIndex = 11;
            // 
            // frmEditFamily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(496, 510);
            this.Controls.Add(this.group);
            this.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmEditFamily";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aile düzenleme ekranı...";
            this.Load += new System.EventHandler(this.frmEditFamily_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPersonCount)).EndInit();
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriority)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbHousingList;
        private System.Windows.Forms.TextBox txtTCNo;
        private System.Windows.Forms.NumericUpDown numPersonCount;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox rchAddress;
        private System.Windows.Forms.ComboBox cmbNeig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTowns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCities;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numPriority;
    }
}