using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SunglassCatiaPlugin.UI
{
    public partial class ctrlForm : UserControl
    {
        public ctrlForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Inits the field of form wit hinput data
        /// </summary>
        /// <param name="inputData"></param>
 public virtual void Init (IDialogInputData inputData)
        {
        }
  public virtual IDialogOutputData GetData()
          {
              return null;
          }
          
       
    }
}
