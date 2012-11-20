namespace SunglassCatiaPlugin.UI
{
    partial class ctrlProjectDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlProjectDetails));
            this.pnlLoad = new System.Windows.Forms.Panel();
            this.lblRev = new System.Windows.Forms.Label();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSynDate = new System.Windows.Forms.Label();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblProjDesc = new System.Windows.Forms.Label();
            this.pnlBackground.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLoad
            // 
            this.pnlLoad.BackColor = System.Drawing.Color.Transparent;
            this.pnlLoad.BackgroundImage = global::SunglassCatiaPlugin.Properties.Resources.arrow_16;
            this.pnlLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoad.ForeColor = System.Drawing.Color.Silver;
            this.pnlLoad.Location = new System.Drawing.Point(407, 2);
            this.pnlLoad.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLoad.Name = "pnlLoad";
            this.pnlLoad.Size = new System.Drawing.Size(74, 45);
            this.pnlLoad.TabIndex = 8;
            this.pnlLoad.MouseLeave += new System.EventHandler(this.pnlLoad_MouseLeave);
   
            this.pnlLoad.Click += new System.EventHandler(this.pnlLoad_Click_1);
            this.pnlLoad.MouseHover += new System.EventHandler(this.pnlLoad_MouseHover);
            // 
            // lblRev
            // 
            this.lblRev.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRev.AutoSize = true;
            this.lblRev.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRev.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblRev.Location = new System.Drawing.Point(9, 12);
            this.lblRev.Margin = new System.Windows.Forms.Padding(0);
            this.lblRev.Name = "lblRev";
            this.lblRev.Size = new System.Drawing.Size(30, 24);
            this.lblRev.TabIndex = 4;
            this.lblRev.Text = "10";
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlBackground.BackgroundImage")));
            this.pnlBackground.Controls.Add(this.tlpMain);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.ForeColor = System.Drawing.Color.Silver;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(483, 49);
            this.pnlBackground.TabIndex = 10;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpMain.Controls.Add(this.pnlLoad, 2, 0);
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tlpMain.Controls.Add(this.lblRev, 0, 0);
            this.tlpMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.ForeColor = System.Drawing.Color.Silver;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(2);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(483, 49);
            this.tlpMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblSynDate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblUpdate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblProjectName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblProjDesc, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(51, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(352, 41);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblSynDate
            // 
            this.lblSynDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSynDate.AutoSize = true;
            this.lblSynDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSynDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSynDate.Location = new System.Drawing.Point(257, 24);
            this.lblSynDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblSynDate.Name = "lblSynDate";
            this.lblSynDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSynDate.Size = new System.Drawing.Size(95, 13);
            this.lblSynDate.TabIndex = 6;
            this.lblSynDate.Text = "Synchronised date";
            this.lblSynDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblUpdate
            // 
            this.lblUpdate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUpdate.Location = new System.Drawing.Point(205, 3);
            this.lblUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUpdate.Size = new System.Drawing.Size(147, 13);
            this.lblUpdate.TabIndex = 5;
            this.lblUpdate.Text = "New Update From Last - days";
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblProjectName.Location = new System.Drawing.Point(0, 1);
            this.lblProjectName.Margin = new System.Windows.Forms.Padding(0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(99, 18);
            this.lblProjectName.TabIndex = 2;
            this.lblProjectName.Text = "Project Name";
            // 
            // lblProjDesc
            // 
            this.lblProjDesc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProjDesc.AutoSize = true;
            this.lblProjDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjDesc.Location = new System.Drawing.Point(0, 24);
            this.lblProjDesc.Margin = new System.Windows.Forms.Padding(0);
            this.lblProjDesc.Name = "lblProjDesc";
            this.lblProjDesc.Size = new System.Drawing.Size(96, 13);
            this.lblProjDesc.TabIndex = 3;
            this.lblProjDesc.Text = "Project Description";
            // 
            // ctrlProjectDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBackground);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ctrlProjectDetails";
            this.Size = new System.Drawing.Size(483, 49);
            this.pnlBackground.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSynDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblProjDesc;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlLoad;
        private System.Windows.Forms.Label lblRev;
        private System.Windows.Forms.Panel pnlBackground;
    }
}
