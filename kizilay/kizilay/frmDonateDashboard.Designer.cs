namespace kizilay
{
    partial class frmDonateDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDonateDashboard));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNewDonate = new System.Windows.Forms.Button();
            this.btnFamily = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label2.Location = new System.Drawing.Point(272, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 22);
            this.label2.TabIndex = 12;
            this.label2.Text = "Bağış Ekle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(82, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "Aileye Bağışı";
            // 
            // btnNewDonate
            // 
            this.btnNewDonate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewDonate.Image = ((System.Drawing.Image)(resources.GetObject("btnNewDonate.Image")));
            this.btnNewDonate.Location = new System.Drawing.Point(246, 18);
            this.btnNewDonate.Name = "btnNewDonate";
            this.btnNewDonate.Size = new System.Drawing.Size(152, 72);
            this.btnNewDonate.TabIndex = 2;
            this.btnNewDonate.UseVisualStyleBackColor = true;
            this.btnNewDonate.Click += new System.EventHandler(this.btnNewDonate_Click);
            // 
            // btnFamily
            // 
            this.btnFamily.BackColor = System.Drawing.Color.Transparent;
            this.btnFamily.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFamily.BackgroundImage")));
            this.btnFamily.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFamily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFamily.Location = new System.Drawing.Point(68, 18);
            this.btnFamily.Name = "btnFamily";
            this.btnFamily.Size = new System.Drawing.Size(147, 72);
            this.btnFamily.TabIndex = 1;
            this.btnFamily.UseVisualStyleBackColor = false;
            this.btnFamily.Click += new System.EventHandler(this.btnFamily_Click);
            // 
            // frmDonateDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(480, 130);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNewDonate);
            this.Controls.Add(this.btnFamily);
            this.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmDonateDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bağış; yardım yap, bağış ve kategori düzenle..";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewDonate;
        private System.Windows.Forms.Button btnFamily;
    }
}