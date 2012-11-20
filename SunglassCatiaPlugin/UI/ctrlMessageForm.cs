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

    public struct MessageDialogInputData : IDialogInputData
    {
        #region IDialogInputData Members

        public string Message;

        public DialogDescription DialogDescription
        {
            get;
            set;
        }

        #endregion
    }
    public partial class ctrlMessageForm : ctrlForm
    {
        
        public ctrlMessageForm()
        {
            InitializeComponent();
        }

        public override void Init(IDialogInputData inputData)
        {
            MessageDialogInputData messageData = (MessageDialogInputData)inputData;
            this.lblMessage.Text = messageData.Message;
        }
    }
}
