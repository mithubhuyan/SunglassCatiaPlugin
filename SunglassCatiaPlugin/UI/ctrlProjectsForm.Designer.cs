namespace SunglassCatiaPlugin
{
    partial class ctrlProjectsForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.lpnlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.picLogout = new System.Windows.Forms.PictureBox();
            this.btnCreateNew = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.fpnlProjectsList = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.lpnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogout)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlButtons, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.fpnlProjectsList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 332);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.pnlButtons.Controls.Add(this.lpnlButtons);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Location = new System.Drawing.Point(0, 251);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(554, 81);
            this.pnlButtons.TabIndex = 3;
            // 
            // lpnlButtons
            // 
            this.lpnlButtons.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lpnlButtons.ColumnCount = 4;
            this.lpnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.lpnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.lpnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.lpnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.lpnlButtons.Controls.Add(this.picLogout, 0, 0);
            this.lpnlButtons.Controls.Add(this.btnCreateNew, 2, 0);
            this.lpnlButtons.Controls.Add(this.btnLogout, 1, 0);
            this.lpnlButtons.Controls.Add(this.btnAbout, 3, 0);
            this.lpnlButtons.Location = new System.Drawing.Point(158, 9);
            this.lpnlButtons.Margin = new System.Windows.Forms.Padding(2);
            this.lpnlButtons.Name = "lpnlButtons";
            this.lpnlButtons.RowCount = 1;
            this.lpnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lpnlButtons.Size = new System.Drawing.Size(393, 62);
            this.lpnlButtons.TabIndex = 1;
            // 
            // picLogout
            // 
            this.picLogout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogout.Image = global::SunglassCatiaPlugin.Properties.Resources.logout_128;
            this.picLogout.Location = new System.Drawing.Point(2, 2);
            this.picLogout.Margin = new System.Windows.Forms.Padding(2);
            this.picLogout.Name = "picLogout";
            this.picLogout.Size = new System.Drawing.Size(52, 58);
            this.picLogout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogout.TabIndex = 2;
            this.picLogout.TabStop = false;
            // 
            // btnCreateNew
            // 
            this.btnCreateNew.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCreateNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(1)))));
            this.btnCreateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateNew.ForeColor = System.Drawing.Color.White;
            this.btnCreateNew.Location = new System.Drawing.Point(178, 6);
            this.btnCreateNew.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.btnCreateNew.Name = "btnCreateNew";
            this.btnCreateNew.Size = new System.Drawing.Size(98, 49);
            this.btnCreateNew.TabIndex = 4;
            this.btnCreateNew.Text = "New";
            this.btnCreateNew.UseVisualStyleBackColor = false;
            this.btnCreateNew.Click += new System.EventHandler(this.btnCreateNew_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(1)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(64, 6);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(98, 49);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ForeColor = System.Drawing.Color.White;
            this.btnAbout.Location = new System.Drawing.Point(292, 6);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(98, 49);
            this.btnAbout.TabIndex = 0;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // fpnlProjectsList
            // 
            this.fpnlProjectsList.AutoScroll = true;
            this.fpnlProjectsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpnlProjectsList.Location = new System.Drawing.Point(0, 0);
            this.fpnlProjectsList.Margin = new System.Windows.Forms.Padding(0);
            this.fpnlProjectsList.Name = "fpnlProjectsList";
            this.fpnlProjectsList.Size = new System.Drawing.Size(554, 251);
            this.fpnlProjectsList.TabIndex = 4;
            // 
            // ctrlProjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ctrlProjectsForm";
            this.Size = new System.Drawing.Size(554, 332);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.lpnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel lpnlButtons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel fpnlProjectsList;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnCreateNew;
        private System.Windows.Forms.PictureBox picLogout;
        protected System.Windows.Forms.Panel pnlButtons;
    }
}