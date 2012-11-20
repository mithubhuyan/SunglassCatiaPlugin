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
    public struct AboutDialogmessages : IDialogInputData
    {
        #region IDialogInputData Members

        public DialogDescription DialogDescription
        {
            get;
            set;
        }

        #endregion
    }

    public partial class ctrlMessage : ctrlForm
    {
        public ctrlMessage()
        {
            InitializeComponent();
        }
    }
}
