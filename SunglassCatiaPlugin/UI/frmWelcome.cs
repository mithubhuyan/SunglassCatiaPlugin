using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SunglassCatiaPlugin
{
    public partial class frmWelcome : Form
    {
        public frmWelcome()
        {
            InitializeComponent();
            
        }

        private void frmWelcome_Load(object sender, EventArgs e)
        {
            this.Height = 50;
            this.Width = 180; 
            
        }

      

        private void btnSync_Click(object sender, EventArgs e)
        {
           
        }
   

        private void btndownload_Click(object sender, EventArgs e)
        {
            //Boolean blnlogin;
            //blnlogin = DialogsManager.ShowLoginDialog("", "", "");
            //if (blnlogin == true)
            //{
            //    DialogsManager.ShowAboutLogin();

            //    DialogsManager.ShowPullProjectsListDialog(0);
            //}
            
        }


        private void frmWelcome_FormClosed(object sender, FormClosedEventArgs e)
        {        
            //UserSettings.Default.SID = "";
            //UserSettings.Default.Token = "";
            //UserSettings.Default.Save();
        }

        private void pbsync(object sender, EventArgs e)
        {
            Boolean blnlogin;
            blnlogin = DialogsManager.ShowLoginDialog("", "", "");
            if (blnlogin == true)
            {
                DialogsManager.ShowPullProjectsListDialog(0);
            }
        }
    }
}
