using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunglassCatiaPlugin.Properties;
using SunglassCatiaPlugin.Utils;

namespace SunglassCatiaPlugin.UI
{
    public enum OperationType_e
    {
        Push,
        Pull
    }

    public delegate void ProjectSelectedDelegate(Project project);
    public partial class ctrlProjectDetails : UserControl
    {
        public event ProjectSelectedDelegate ProjectSelected;
        //public event ProjectHighlightDelegate ProjectHighlight;

        Color COLOR_HIGHLIGHTED = Color.LightSkyBlue;
        Color COLOR_SELECTED = Color.LightGray;

        private Project m_Project;

        public ctrlProjectDetails(Project project, OperationType_e type)
        {
            InitializeComponent();
            this.m_Project = project;
            this.FillProjectDetails();
            switch (type)
            {
                case OperationType_e.Push:
                    pnlLoad.BackgroundImage = Resources.arrow_161;
                    break;
                case OperationType_e.Pull:
                    pnlLoad.BackgroundImage = Resources.arrow_16;
                    break;
            }

        }    
        
        public ctrlProjectDetails()
        {
            InitializeComponent();
        }
        public void FillProjectDetails()
        {
            this.lblProjectName.Text = this.m_Project.name;
            this.lblProjDesc.Text = this.m_Project.description;

            string days = GetLastUpdateDateDifference(this.m_Project.modifiedAt);
            this.lblUpdate.Text = string.Format("New update from {0} days ago", days);

            System.DateTime modifiedDate;
            System.DateTime.TryParse(this.m_Project.modifiedAt, out modifiedDate);
            string syncronizedDate = string.Format("{0}/{1}/{2}", modifiedDate.Month, modifiedDate.Day, modifiedDate.Year);
            this.lblSynDate.Text = "Synchronized " + syncronizedDate; ;
            this.lblRev.Text = "10";
        }


        private void Highlight(bool state)
        {
            if (state)
            {
                pnlLoad.BackColor = COLOR_HIGHLIGHTED;
            }
            else
            {
                if (m_IsDefault)
                {
                    pnlLoad.BackColor = COLOR_SELECTED;
                }
                else
                {
                    pnlLoad.BackColor = Color.Transparent;
                }
            }
        }

    
        bool m_IsDefault = false;
        internal void SetDefault()
        {
            m_IsDefault = true;
            tlpMain.BackColor = Color.LightGray;
        }

        private string GetLastUpdateDateDifference(string lastModifiedDate)
        {
            System.DateTime currentTime = System.DateTime.Now;
            System.DateTime oldDate;
            System.DateTime.TryParse(lastModifiedDate, out oldDate);
            TimeSpan daysDiff = currentTime.Subtract(oldDate);
            return daysDiff.Days.ToString();
        }

       

        private void pnlLoad_MouseLeave(object sender, EventArgs e)
        {
            Highlight(false);
        }

        private void pnlLoad_MouseHover(object sender, EventArgs e)
        {
            Highlight(true);
        }

        private void pnlLoad_Click_1(object sender, EventArgs e)
        {
            if (ProjectSelected != null)
            {
                ProjectSelected(m_Project);
                //ConnectionManager conn=new ConnectionManager(
        
   
            }
        }
    }
}
