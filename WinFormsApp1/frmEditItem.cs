using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyREST
{
    public partial class frmEditItem : Form
    {
        public string Noun { get => this.txtTitle.Text; }
        public string Description { get => this.txtDescription.Text; }
        
        // GET expected data and returned data
        public bool UseGET { get => this.chkGET.Checked; }
        public bool AuthoriseGET { get => this.chkAuthoriseGET.Checked; }
        public bool ReturnsArrayGET { get => this.cbArrayGET.Checked; }
        public bool ReturnsCahceableGET {  get => this.cbCacheableGET.Checked; }
        public Dictionary<string, string> DictionaryGetExpected { get => this.getGridView(this.dgvGetExpected); }
        public Dictionary<string, string> DictionaryGetReturned { get => this.getGridView(this.dgvGetReturned); }
        
        // POST expected and returned data
        public bool UsePOST { get => this.chkPOST.Checked; }
        public bool AuthorisePOST { get => this.chkAuthorisePOST.Checked; }
        public bool ReturnsArrayPOST { get => this.cbArrayPOST.Checked; }
        public bool ReturnsCahceablePOST { get => this.cbCacheablePOST.Checked; }
        public Dictionary<string, string> DictionaryPostExpected { get => this.getGridView(this.dgvPostExpected); }
        public Dictionary<string, string> DictionaryPostReturned { get => this.getGridView(this.dgvPostReturned); }

        // PATCH expected and returned data
        public bool UsePATCH { get => this.chkPATCH.Checked; }
        public bool AuthorisePATCH { get => this.chkAuthorisePATCH.Checked; }
        public bool ReturnsArrayPATCH { get => this.cbArrayPATCH.Checked; }
        public bool ReturnsCahceablePATCH { get => this.cbCacheablePATCH.Checked; }
        public Dictionary<string, string> DictionaryPatchExpected { get => this.getGridView(this.dgvPatchExpected); }
        public Dictionary<string, string> DictionaryPatchReturned { get => this.getGridView(this.dgvPatchReturned); }


        // DELETE expected and returned data
        public bool UseDELETE { get => this.chkDELETE.Checked; }
        public bool AuthoriseDELETE { get => this.chkAuthoriseDELETE.Checked; }
        public bool ReturnsArrayDELETE{ get => this.cbArrayDELETE.Checked; }
        public bool ReturnsCahceableDELETE { get => this.cbCacheableDELETE.Checked; }

        public Dictionary<string, string> DictionaryDeleteExpected { get => this.getGridView(this.dgvDeleteExpected); }
        public Dictionary<string, string> DictionaryDeleteReturned { get => this.getGridView(this.dgvDeleteReturned); }


        public frmEditItem(class_rest item)
        {
            InitializeComponent();
            this.Text = "Edit Item ID: " + item.ID.ToString();
            this.txtTitle.Text = item.Noun;
            this.txtTitle.Enabled = item.Noun == "/" ? false : true;
            this.txtDescription.Text = item.Description;

            // Set GET controls
            this.chkGET.Checked = item.UseGET;
            ((Control)tabGET).Enabled = item.UseGET;
            this.chkAuthoriseGET.Checked = item.UseGETAuthorisation;
            this.setGridView(dgvGetExpected, item.ExpectsGET);
            this.setGridView(dgvGetReturned, item.ReturnsGET);
            this.cbArrayGET.Checked = item.ReturnsArrayGET;
            this.cbCacheableGET.Checked = item.ReturnsCacheableGET;

            // Set POST controls
            this.chkPOST.Checked = item.UsePOST;
            ((Control)tabPOST).Enabled = item.UsePOST;
            this.chkAuthorisePOST.Checked = item.UsePOSTAuthorisation;
            this.setGridView(dgvPostExpected, item.ExpectsPOST);
            this.setGridView(dgvPostReturned, item.ReturnsPOST);
            this.cbArrayPOST.Checked = item.ReturnsArrayPOST;
            this.cbCacheablePOST.Checked = item.ReturnsCacheablePOST;

            // Set PATCH controls
            this.chkPATCH.Checked = item.UsePATCH;
            ((Control)tabPATCH).Enabled = item.UsePATCH;
            this.chkAuthorisePATCH.Checked = item.UsePATCHAuthorisation;
            this.setGridView(dgvPatchExpected, item.ExpectsPATCH);
            this.setGridView(dgvPatchReturned, item.ReturnsPATCH);
            this.cbArrayPATCH.Checked = item.ReturnsArrayPATCH;
            this.cbCacheablePATCH.Checked = item.ReturnsCacheablePATCH;

            // Set DELETE controls
            this.chkDELETE.Checked = item.UseDELETE;
            ((Control)tabDELETE).Enabled = item.UseDELETE;
            this.chkAuthoriseDELETE.Checked = item.UseDELETEAuthorisation;
            this.setGridView(dgvDeleteExpected, item.ExpectsDELETE);
            this.setGridView(dgvDeleteReturned, item.ReturnsDELETE);
            this.cbArrayDELETE.Checked = item.ReturnsArrayDELETE;
            this.cbCacheableDELETE.Checked = item.ReturnsCacheableDELETE;

            butCancel.DialogResult = DialogResult.Cancel;
            butOK.DialogResult = DialogResult.OK;
            this.AcceptButton = butOK;
            CancelButton = butCancel;
            tab.Width = this.ClientRectangle.Width;
            tab.Height = this.ClientRectangle.Height;
            tab.Left = this.ClientRectangle.Left;
            tab.Top = this.ClientRectangle.Top;
            StartPosition = FormStartPosition.CenterScreen;
            tab.Invalidate();
        }

        private void setGridView(DataGridView grid, Dictionary<string, string> dict)
        {
            if (grid != null && dict != null)
            {
                foreach (string key in dict.Keys)
                {
                    int rowId = grid.Rows.Add();
                    DataGridViewRow row = grid.Rows[rowId];
                    row.Cells[0].Value = key;
                    row.Cells[1].Value = dict[key];
                }
            }

        }

        private Dictionary<string, string> getGridView(DataGridView grid)
        {
            Dictionary<string, string> ret = [];
            if (grid != null)
            {
                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    DataGridViewRow row = grid.Rows[i];
                    if (row != null)
                    {
                        string key = (string)row.Cells[0].Value!;
                        string value = (string)row.Cells[1].Value!;
                        if (key != null && value != null)
                        {
                            key = key.Trim();
                            value = value.Trim();
                            // Note only one key of same name is added, might need to alert user
                            if (key.Length > 0 && !ret.ContainsKey(key) && value.Length > 0)
                            {
                                ret.Add(key, value);
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "^[a-zA-Z0-9\b]*$"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }

        }

        private void chkGET_CheckedChanged(object sender, EventArgs e)
        {
            chkAuthoriseGET.Enabled = chkGET.Checked;
            ((Control)this.tabGET).Enabled = chkGET.Checked;
            tab.Invalidate();
        }

        private void chkPOST_CheckedChanged(object sender, EventArgs e)
        {

            chkAuthorisePOST.Enabled = chkPOST.Checked;
            ((Control)this.tabPOST).Enabled = chkPOST.Checked;
            tab.Invalidate();
        }

        private void chkPATCH_CheckedChanged(object sender, EventArgs e)
        {
            chkAuthorisePATCH.Enabled = chkPATCH.Checked;
            ((Control)this.tabPATCH).Enabled = chkPATCH.Checked;
            tab.Invalidate();
        }

        private void chkDELETE_CheckedChanged(object sender, EventArgs e)
        {
            chkAuthoriseDELETE.Enabled = chkDELETE.Checked;
            ((Control)this.tabDELETE).Enabled = chkDELETE.Checked;
            tab.Invalidate();
        }

        private void tab_DrawItem(object sender, DrawItemEventArgs e)
        {
            Control tp = tab.TabPages[e.Index];
            using (SolidBrush brush = new SolidBrush(tp.Enabled ? tp.BackColor : tp.BackColor))
            using (SolidBrush textBrush = new SolidBrush(tp.Enabled ? tp.ForeColor : SystemColors.GrayText))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
                e.Graphics.DrawString(tp.Text, e.Font!, textBrush, e.Bounds.X + 3, e.Bounds.Y + 4);
            }
        }

        private void tab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage != null)
            {
                Control tabPage = e.TabPage;
                if (!tabPage.Enabled)
                {
                    e.Cancel = true;
                }
            }
        }

        private void dgv_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewRow row = e.Row;
            row.Cells[1].Value = "string";
        }
    }
}
