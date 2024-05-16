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
    public partial class frmProjectOptions : Form
    {
        public string Title { get => this.txtTitle.Text; set { this.txtTitle.Text = value; } }
        public string Description { get => this.txtDescription.Text; set { this.txtDescription.Text = value; } }
        public int Port { get
            {
                try
                {
                    return Int32.Parse(this.txtPort.Text);
                } catch(Exception ex)
                {
                    return 8080;
                }
            }
            set 
            { 
                this.txtPort.Text = value.ToString(); 
            } 
        }

        public frmProjectOptions()
        {
            InitializeComponent();
            butCancel.DialogResult = DialogResult.Cancel;
            butOK.DialogResult = DialogResult.OK;
            this.AcceptButton = butOK;
            CancelButton = butCancel;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "^[0-9/\b]*$"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }

        }
    }
}
