using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SunglassCatiaPlugin.UI;

namespace SunglassCatiaPlugin
{
    /// <summary>
    /// Data returned from the dialog
    /// </summary>
    public interface IDialogOutputData
    {
    }

    /// <summary>
    /// Description of dialog fields
    /// </summary>
    public struct DialogDescription
    {
        public string Caption;
        public string Header;
        public string SubHeader;
        public string OKButtonCaption;
        public string CancelButtonCaption;
        public Image Icon;
    }

    /// <summary>
    /// Data passed to the dialog
    /// </summary>
    public interface IDialogInputData
    {
        DialogDescription DialogDescription
        {
            get;
            set;
        }
    }

    public delegate void CustomNotificationDelegate(frmDialog sender, object args);

    public partial class frmDialog : Form
    {
        public event CustomNotificationDelegate CustomNotification;

        ctrlForm m_Form;

        public frmDialog()
        {
            InitializeComponent();
           
        }

     

        public Color DialogNameLabelColor
        {
            set
            {
                this.lblHeader.ForeColor = value;
                this.lblHeader.Font = new Font(this.lblHeader.Font, FontStyle.Bold);
            }
        }

        public void Init(IDialogInputData inputData, ctrlForm form)
        {
            m_Form = form;
            SetDescription(inputData.DialogDescription);
            InitBody(inputData);
        }

        public void RaiseCustomNotification(object args)
        {
            if (CustomNotification != null)
            {
                CustomNotification(this, args);
            }
        }

        private void InitBody(IDialogInputData inputData)
        {
            FitControl();
            pnlBody.Controls.Add(m_Form);
            m_Form.Dock = DockStyle.Fill;

            m_Form.Init(inputData);
        }

        private void FitControl()
        {
            int widthOffset = this.Width - pnlBody.Width;
            int heigthOffset = this.Height - pnlBody.Height;

            this.Width = m_Form.Width + widthOffset;
            this.Height = m_Form.Height + heigthOffset;
        }

        public IDialogOutputData GetOutputData()
        {
            return m_Form.GetData();
        }
   

        public bool OkEnabled
        {
            set
            {
                btnOK.Enabled = value;
                if (value)
                {
                    btnOK.BackColor = Color.FromArgb(255, 200, 1);
                }
                else
                {
                    btnOK.BackColor = Color.Gray;
                }
            }
        }

        private void SetDescription(DialogDescription desc)
        {
            this.Text = desc.Caption;
            this.lblHeader.Text = desc.Header;
            this.lblSubHeader.Text = desc.SubHeader;

            if (!string.IsNullOrEmpty(desc.CancelButtonCaption))
            {
                this.btnCancel.Text = desc.CancelButtonCaption;
            }
            else
            {
                this.btnCancel.Visible = false;
            }

            if (!string.IsNullOrEmpty(desc.OKButtonCaption))
            {
                this.btnOK.Text = desc.OKButtonCaption;
            }
            else
            {
                this.btnOK.Visible = false;
            }

            if (string.IsNullOrEmpty(desc.CancelButtonCaption) && string.IsNullOrEmpty(desc.OKButtonCaption))
            {
                lpnlButtons.Visible = false;
                lpnlMain.RowStyles[2].Height = 0;
            }

            if (desc.Icon != null)
            {
                this.picIcon.Image = desc.Icon;
            }
            else
            {
                this.picIcon.Visible = false;
                this.tlpHeader.ColumnStyles[0].SizeType = SizeType.AutoSize;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
