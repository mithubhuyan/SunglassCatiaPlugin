using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunglassCatiaPlugin.Utils;
using SunglassCatiaPlugin.UI;

namespace SunglassCatiaPlugin
{
    public struct ProjectsDialogInputData : IDialogInputData
    {
        public int ActiveProjectId;
        public List<Project> Projects;
        public bool CreateNewAvailable;
        public OperationType_e OperationType;

        #region IDialogInputData Members

        public DialogDescription DialogDescription
        {
            get;
            set;
        }

        #endregion
    }

    public struct ProjectsDialogOutputData : IDialogOutputData
    {
        public Project Project;
        public bool AllowChangeSelectedProjectData;
    }

    public partial class ctrlProjectsForm : ctrlForm
    {
        public enum ProjectsFormCustomNotification_e
        {
            LogoutClicked,
            AboutClicked
        }

        private Project m_SelProject;
        private bool m_AllowChangeSelectedProjectData;

        public ctrlProjectsForm()
        {
            InitializeComponent();
        }

        public override void Init(IDialogInputData inputData)
        {
            const int SCROLLBAR_WIDTH = 30;

           ProjectsDialogInputData prjsData = (ProjectsDialogInputData)inputData;
           if (prjsData.Projects != null)
           {
               foreach (Project prj in prjsData.Projects)
               {
                   ctrlProjectDetails ctrlDetails = new ctrlProjectDetails(prj, prjsData.OperationType);
                    ctrlDetails.ProjectSelected += new ProjectSelectedDelegate(OnProjectSelected);
                   Padding margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
                    ctrlDetails.Margin = margin;
                   ctrlDetails.Width = fpnlProjectsList.Width - SCROLLBAR_WIDTH;
        //ctrlDetails.ProjectHighlight += new ProjectHighlightDelegate(OnProjectHighlight);
                    this.fpnlProjectsList.Controls.Add(ctrlDetails);

                   if (prj.id == prjsData.ActiveProjectId)
                    {
                        ctrlDetails.SetDefault();
                   }
              }
        }

        //    if (!prjsData.CreateNewAvailable)
        //    {
        //        int delta = btnCreateNew.Width + btnCreateNew.Margin.Left + btnCreateNew.Margin.Right;

        //        btnCreateNew.Visible = false;
        //        lpnlButtons.ColumnStyles[2].SizeType = SizeType.Absolute;
        //        lpnlButtons.ColumnStyles[2].Width = 1;

        //        lpnlButtons.Width = lpnlButtons.Width - delta;
        //        lpnlButtons.Left = lpnlButtons.Left + delta;
        }



      

        ctrlProjectDetails m_CurlightedHighControl;

        void OnProjectSelected(Project project)
        {
            m_SelProject = project;
            m_AllowChangeSelectedProjectData = true;
            this.ParentForm.DialogResult = DialogResult.OK;
            this.ParentForm.Close();
        }

        public override IDialogOutputData GetData()
        {
            return new ProjectsDialogOutputData()
            {
                Project = m_SelProject,
                AllowChangeSelectedProjectData = m_AllowChangeSelectedProjectData
            };
        }

        private void OnLogout(object sender, EventArgs e)
        {
            (this.ParentForm as frmDialog).RaiseCustomNotification(ProjectsFormCustomNotification_e.LogoutClicked);
        }

        private void OnAbout(object sender, EventArgs e)
        {
            (this.ParentForm as frmDialog).RaiseCustomNotification(ProjectsFormCustomNotification_e.AboutClicked);
        }

        private void OnCreateNew(object sender, EventArgs e)
        {
            //Project prj = Controller.ShowCreateNewProject();
            //if (prj != null)
            //{
            //    m_SelProject = prj;
            //    m_AllowChangeSelectedProjectData = false;
            //    this.ParentForm.DialogResult = DialogResult.OK;
            //    this.ParentForm.Close();
            //}
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
         
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            DialogsManager.ShowAboutDialog();
        }
        
    }
}
