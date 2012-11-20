using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SunglassCatiaPlugin.UI
{
    public partial class ctrlMessageFormExt : ctrlForm
    {
        public struct MessageExtDialogOutputData : IDialogOutputData
        {
            public enum Action_e
            {
                CreateNewProject,
                AddToExistingProject
            }

            public Action_e Action;
        }
        private SunglassCatiaPlugin.UI.ctrlMessageFormExt.MessageExtDialogOutputData.Action_e m_Action;
        public override void Init(IDialogInputData inputData)
        {
            MessageDialogInputData messageData = (MessageDialogInputData)inputData;
            this.lblMessage.Text = messageData.Message;
        }

        public override IDialogOutputData GetData()
        {
            return new MessageExtDialogOutputData()
            {
                Action = m_Action
            };
        }
       
        
        public ctrlMessageFormExt()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            m_Action = MessageExtDialogOutputData.Action_e.AddToExistingProject;
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            m_Action = MessageExtDialogOutputData.Action_e.CreateNewProject;
        }
    }
}
