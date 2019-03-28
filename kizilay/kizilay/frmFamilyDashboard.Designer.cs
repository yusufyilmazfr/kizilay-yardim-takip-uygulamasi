namespace kizilay
{
    partial class frmFamilyDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFamilyDashboard));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnNewFamily = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnShowAllFamily = new System.Windows.Forms.Button();
            this.btnEditFamily = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label2.Location = new System.Drawing.Point(193, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Aileye Üye Ekle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(32, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = " Aile Oluştur";
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddPerson.BackgroundImage")));
            this.btnAddPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddPerson.Location = new System.Drawing.Point(192, 12);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(147, 72);
            this.btnAddPerson.TabIndex = 2;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnNewFamily
            // 
            this.btnNewFamily.BackColor = System.Drawing.Color.Transparent;
            this.btnNewFamily.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewFamily.BackgroundImage")));
            this.btnNewFamily.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewFamily.Location = new System.Drawing.Point(14, 12);
            this.btnNewFamily.Name = "btnNewFamily";
            this.btnNewFamily.Size = new System.Drawing.Size(147, 72);
            this.btnNewFamily.TabIndex = 1;
            this.btnNewFamily.UseVisualStyleBackColor = false;
            this.btnNewFamily.Click += new System.EventHandler(this.btnNewFamily_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label8.Location = new System.Drawing.Point(575, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 22);
            this.label8.TabIndex = 8;
            this.label8.Text = "Kayıtlar";
            // 
            // btnShowAllFamily
            // 
            this.btnShowAllFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAllFamily.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAllFamily.Image")));
            this.btnShowAllFamily.Location = new System.Drawing.Point(539, 12);
            this.btnShowAllFamily.Name = "btnShowAllFamily";
            this.btnShowAllFamily.Size = new System.Drawing.Size(147, 72);
            this.btnShowAllFamily.TabIndex = 9;
            this.btnShowAllFamily.UseVisualStyleBackColor = true;
            this.btnShowAllFamily.Click += new System.EventHandler(this.btnShowAllFamily_Click_1);
            // 
            // btnEditFamily
            // 
            this.btnEditFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditFamily.Image = ((System.Drawing.Image)(resources.GetObject("btnEditFamily.Image")));
            this.btnEditFamily.Location = new System.Drawing.Point(373, 12);
            this.btnEditFamily.Name = "btnEditFamily";
            this.btnEditFamily.Size = new System.Drawing.Size(147, 72);
            this.btnEditFamily.TabIndex = 9;
            this.btnEditFamily.UseVisualStyleBackColor = true;
            this.btnEditFamily.Click += new System.EventHandler(this.btnEditFamily_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label3.Location = new System.Drawing.Point(386, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Aile Düzenle";
            // 
            // frmFamilyDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(704, 137);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnEditFamily);
            this.Controls.Add(this.btnShowAllFamily);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.btnNewFamily);
            this.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmFamilyDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aile Yönetim Ekranı";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnNewFamily;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnShowAllFamily;
        private System.Windows.Forms.Button btnEditFamily;
        private System.Windows.Forms.Label label3;
    }
}