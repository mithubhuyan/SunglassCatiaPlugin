namespace SunglassCatiaPlugin.UI
{
    partial class ctrlAboutForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMessage = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lnlTermsOfUser = new System.Windows.Forms.LinkLabel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lnkSunglass = new System.Windows.Forms.LinkLabel();
            this.tlpMessage.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMessage
            // 
            this.tlpMessage.BackColor = System.Drawing.Color.Honeydew;
            this.tlpMessage.ColumnCount = 2;
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMessage.Controls.Add(this.pnlMain, 1, 0);
            this.tlpMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMessage.Location = new System.Drawing.Point(0, 0);
            this.tlpMessage.Margin = new System.Windows.Forms.Padding(2);
            this.tlpMessage.Name = "tlpMessage";
            this.tlpMessage.RowCount = 1;
            this.tlpMessage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMessage.Size = new System.Drawing.Size(584, 96);
            this.tlpMessage.TabIndex = 1;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Honeydew;
            this.pnlMain.Controls.Add(this.lnlTermsOfUser);
            this.pnlMain.Controls.Add(this.lblCopyright);
            this.pnlMain.Controls.Add(this.lnkSunglass);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(107, 2);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(475, 92);
            this.pnlMain.TabIndex = 1;
            // 
            // lnlTermsOfUser
            // 
            this.lnlTermsOfUser.AutoSize = true;
            this.lnlTermsOfUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnlTermsOfUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.lnlTermsOfUser.LinkArea = new System.Windows.Forms.LinkArea(9, 13);
            this.lnlTermsOfUser.Location = new System.Drawing.Point(6, 67);
            this.lnlTermsOfUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnlTermsOfUser.Name = "lnlTermsOfUser";
            this.lnlTermsOfUser.Size = new System.Drawing.Size(182, 24);
            this.lnlTermsOfUser.TabIndex = 8;
            this.lnlTermsOfUser.TabStop = true;
            this.lnlTermsOfUser.Text = "Sunglass Terms of User";
            this.lnlTermsOfUser.UseCompatibleTextRendering = true;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.lblCopyright.Location = new System.Drawing.Point(2, 9);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(456, 20);
            this.lblCopyright.TabIndex = 0;
            this.lblCopyright.Text = "Copyright 2012 Tata Technologies Limited. All Rights Reserved.";
            // 
            // lnkSunglass
            // 
            this.lnkSunglass.AutoSize = true;
            this.lnkSunglass.BackColor = System.Drawing.Color.Honeydew;
            this.lnkSunglass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSunglass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.lnkSunglass.LinkArea = new System.Windows.Forms.LinkArea(29, 8);
            this.lnkSunglass.Location = new System.Drawing.Point(6, 37);
            this.lnkSunglass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkSunglass.Name = "lnkSunglass";
            this.lnkSunglass.Size = new System.Drawing.Size(296, 24);
            this.lnkSunglass.TabIndex = 7;
            this.lnkSunglass.TabStop = true;
            this.lnkSunglass.Text = "Sunglass for Catia is a Sunglass project";
            this.lnkSunglass.UseCompatibleTextRendering = true;
            // 
            // ctrlAboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.Controls.Add(this.tlpMessage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ctrlAboutForm";
            this.Size = new System.Drawing.Size(584, 96);
            this.tlpMessage.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMessage;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.LinkLabel lnkSunglass;
        private System.Windows.Forms.LinkLabel lnlTermsOfUser;
    }
}