using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunglassCatiaPlugin.UI;

namespace SunglassCatiaPlugin
{
    public struct LoginDialogInputData : IDialogInputData
    {
        #region IDialogInputData Members
        public string DefaultUsername;
        public string DefaultPassword;
        public string ErrorMessage;

        public DialogDescription DialogDescription
        {
            get;
            set;
            //return new DialogDescription()
            //{
            //    Caption = "Sunglass",
            //    DialogName = "Login",
            //    DialogDescriptions = "Login to Sunglass",
            //    CancelButtonCaption = "Cancel",
            //    OKButtonCaption = "Login",
            //    Icon = null
            //};
        }

        #endregion
    }

    public struct LoginDialogOutputData : IDialogOutputData
    {
        public string Username;
        public string Password;
    }
    public partial class ctrlLoginForm : ctrlForm
    {
        public ctrlLoginForm()
        {
            InitializeComponent();
      
        }
        public override void Init(IDialogInputData inputData)
        {
            LoginDialogInputData loginData = (LoginDialogInputData)inputData;
            this.txtUserName.Text = loginData.DefaultUsername;
            this.txtPassword.Text = loginData.DefaultPassword;
            ShowErrorMessage(loginData.ErrorMessage);
        }

        public override IDialogOutputData GetData()
        {
            return new LoginDialogOutputData()
            {
                Username = this.txtUserName.Text,
                Password = this.txtPassword.Text
            };
        }

        private void ShowErrorMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                this.lblErrorMsg.Visible = true;
                this.lblErrorMsg.Text = message;
            }
            else
            {
                this.lblErrorMsg.Visible = false;
            }
        }
    }
}
