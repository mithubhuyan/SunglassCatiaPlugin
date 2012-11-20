using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunglassCatiaPlugin.Core;
using SunglassCatiaPlugin.Properties;
using SunglassCatiaPlugin.UI;
using SunglassCatiaPlugin.Utils;
using System.Reflection;
using System.Drawing;


namespace SunglassCatiaPlugin
{
    public static class DialogsManager
    {
        public static bool ShowLoginDialog(string defUsername, string defPassword, string errorMsg)
        {
            if (SunglassMgr.IsLoggedIn())
            {
                return true;
            }

            LoginDialogInputData inputData =
                new LoginDialogInputData()
                {
                    DefaultUsername = defUsername,
                    DefaultPassword = defPassword,
                    ErrorMessage = errorMsg,
                    DialogDescription = new DialogDescription()
                    {
                        Caption = Resources.IDS_DLG_SUNGLASS_MAIN_CAPTION,
                        Header = Resources.IDS_DLG_LOGIN_HEADER,
                        SubHeader = Resources.IDS_DLG_LOGIN_SUB_HEADER,
                        CancelButtonCaption = Resources.IDS_BTN_CANCEL_CAPTION,
                        OKButtonCaption = Resources.IDS_BTN_LOGIN_CAPTION,
                        Icon = null
                    }
                };

            frmDialog frmLogin = new frmDialog();
            frmLogin.Init(inputData, new ctrlLoginForm());
            if (frmLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoginDialogOutputData creds = (LoginDialogOutputData)frmLogin.GetOutputData();

                bool loginRes = SunglassMgr.Connect(creds.Username, creds.Password);
                string errMsg = Resources.IDS_ERR_LOGIN_FAIL;

                if (!loginRes)
                {
                    return ShowLoginDialog(creds.Username, creds.Password, errMsg);
                }
                else
                {
                    Properties.Settings.Default.LastUsername = creds.Username;
                    Properties.Settings.Default.LastPassword = creds.Password;
                    Properties.Settings.Default.Save();                   
                    return true;
                }
            }

            return false;
        }


        public static ProjectsDialogOutputData? ShowPullProjectsListDialog(int activeProjectId)
        {
            return ShowProjectsList(new DialogDescription()
            {
                Caption = Resources.IDS_DLG_SUNGLASS_MAIN_CAPTION,
                Header = Resources.IDS_DLG_PROJECTS_PULL_HEADER,
                SubHeader = Resources.IDS_DLG_PROJECTS_PULL_SUB_HEADER,
                CancelButtonCaption = null,
                OKButtonCaption = null,
                Icon = null
            }, activeProjectId, false, OperationType_e.Pull);
        }

        private static ProjectsDialogOutputData? ShowProjectsList(DialogDescription dlgDesc, int activeProjectId, bool createNewAvailable, OperationType_e type)
        {
            List<Project> projects = SunglassMgr.GetAllProjects();

            System.Diagnostics.Debug.Assert(projects != null);

            ProjectsDialogInputData inputData = new ProjectsDialogInputData()
            {
                DialogDescription = dlgDesc,
                ActiveProjectId = activeProjectId,
                Projects = projects,
                CreateNewAvailable = createNewAvailable,
                OperationType = type
            };

            frmDialog frmProjects = new frmDialog();
            frmProjects.CustomNotification += new CustomNotificationDelegate(OnFormProjectsCustomNotification);
            frmProjects.Init(inputData, new ctrlProjectsForm());

            if (frmProjects.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return (ProjectsDialogOutputData)frmProjects.GetOutputData();
            }

            return null;
        }


        public static SunglassCatiaPlugin.UI.ctrlMessageFormExt.MessageExtDialogOutputData? ShowNoProjectWarningDialog(string message)
        {
            DialogDescription dlgDesc = new DialogDescription()
            {
                Caption = Resources.IDS_DLG_SUNGLASS_MAIN_CAPTION,
                Header = Resources.IDS_DLG_FIRST_PUSH_HEADER,
                SubHeader = Resources.IDS_DLG_FIRST_PUSH_SUB_HEADER,
                Icon = Resources.warning_128,
                OKButtonCaption = "",
                CancelButtonCaption = ""
            };

            frmDialog frmMessage = CreateMessageDialog(new ctrlMessageFormExt(), dlgDesc, message, new Size(800, 400));
            frmMessage.DialogNameLabelColor = Color.FromArgb(255, 200, 1);
            if (frmMessage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return (SunglassCatiaPlugin.UI.ctrlMessageFormExt.MessageExtDialogOutputData?)frmMessage.GetOutputData();
            }
            else
            {
                return null;
            }
        }



        private static frmDialog CreateMessageDialog(ctrlForm messageForm, DialogDescription dlgDesc, string message, Size? size)
        {
            frmDialog frmMessage = new frmDialog();
            MessageDialogInputData inputData = new MessageDialogInputData();
            inputData.DialogDescription = dlgDesc;
            inputData.Message = message;
            frmMessage.Init(inputData, messageForm);
            if (size != null)
            {
                frmMessage.Size = (Size)size;
            }
            return frmMessage;
        }




        private static bool ShowMessage(DialogDescription dlgDesc, string message, Size? size, Color? color)
        {
            frmDialog frmMessage = CreateMessageDialog(new ctrlMessageForm(), dlgDesc, message, size);
            if (color != null)
            {
                frmMessage.DialogNameLabelColor = (Color)color;
            }
            return frmMessage.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        private static bool ShowMessage(DialogDescription dlgDesc, string message, Size? size)
        {
            return ShowMessage(dlgDesc, message, size, null);
        }

        private static bool ShowMessage(DialogDescription dlgDesc, string message)
        {
            return ShowMessage(dlgDesc, message, null, null);
        }

        private static bool ShowMessage(DialogDescription dlgDesc, string message, Color dlgNameColor)
        {
            return ShowMessage(dlgDesc, message, null);
        }
        static void OnFormProjectsCustomNotification(frmDialog sender, object args)
        {
            switch ((ctrlProjectsForm.ProjectsFormCustomNotification_e)args)
            {
                case ctrlProjectsForm.ProjectsFormCustomNotification_e.AboutClicked:
                    ShowAboutDialog();
                    break;
                case ctrlProjectsForm.ProjectsFormCustomNotification_e.LogoutClicked:
                    SunglassMgr.Logout();
                    sender.Close();
                    break;
            }
        }

        public static void ShowAboutDialog()
        {
            frmDialog frmAbout = new frmDialog();
            AboutDialogInputData inputData = new AboutDialogInputData();
            inputData.DialogDescription =
                new DialogDescription()
                {
                    Caption = string.Format(Resources.IDS_DLG_ABOUT_CAPTION, AssemblyTitle),
                    Header = AssemblyTitle,
                    SubHeader = string.Format(Resources.IDS_DLG_ABOUT_DESCRIPTION, AssemblyVersion),
                    Icon = Resources.sg_logo_128,
                    OKButtonCaption = Resources.IDS_BTN_OK_CAPTION,
                    CancelButtonCaption = ""
                };
            frmAbout.Init(inputData, new ctrlAboutForm());
            frmAbout.ShowDialog();
        }


        public static void ShowAboutLogin()
        {
            frmDialog frmAbout = new frmDialog();
            AboutDialogmessages inputData = new AboutDialogmessages();
            inputData.DialogDescription =
                new DialogDescription()
                {
                    Caption = Resources.IDS_DLG_LOGIN_DESCRIPTION, 
                    Header = Resources.IDS_DLG_LOGIN_DESCRIPTION,
                    SubHeader = Resources.IDS_DLG_LOGIN_WELCOME, 
                    Icon = Resources.success_128,
                    OKButtonCaption = Resources.IDS_BTN_OK_CAPTION,
                    CancelButtonCaption = ""
                };
            frmAbout.Init(inputData, new ctrlMessage());
            frmAbout.ShowDialog();
        }
        private static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        private static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    
    
    
    
    }

   

}
