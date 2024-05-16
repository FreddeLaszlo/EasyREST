using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyREST
{
    public partial class frmAddPath : Form
    {
        public frmAddPath()
        {
            InitializeComponent();
            butCancel.DialogResult = DialogResult.Cancel;
            butOK.DialogResult = DialogResult.OK;
            this.AcceptButton = butOK;
            CancelButton = butCancel;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public string GetPath()
        {
            // remove '/' character from both ends of path
            return txtPath.Text.Trim('/');
        }

        private void txtPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "^[a-zA-Z0-9/\b]*$"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
        }
    }
}
