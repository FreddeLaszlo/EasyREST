namespace EasyREST
{
    partial class frmAddPath
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPath = new TextBox();
            lblInfo = new Label();
            butCancel = new Button();
            butOK = new Button();
            SuspendLayout();
            // 
            // txtPath
            // 
            txtPath.Font = new Font("Segoe UI", 12F);
            txtPath.Location = new Point(20, 36);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(491, 29);
            txtPath.TabIndex = 0;
            txtPath.KeyPress += txtPath_KeyPress;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(20, 18);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(398, 15);
            lblInfo.TabIndex = 1;
            lblInfo.Text = "Path to add from root e.g. shops/id/books/id  (use id to denote a number)";
            // 
            // butCancel
            // 
            butCancel.BackColor = SystemColors.ButtonFace;
            butCancel.Location = new Point(361, 76);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(80, 46);
            butCancel.TabIndex = 3;
            butCancel.Text = "&Cancel";
            butCancel.UseVisualStyleBackColor = false;
            // 
            // butOK
            // 
            butOK.BackColor = SystemColors.ButtonFace;
            butOK.Location = new Point(447, 76);
            butOK.Name = "butOK";
            butOK.Size = new Size(64, 46);
            butOK.TabIndex = 4;
            butOK.Text = "&OK";
            butOK.UseVisualStyleBackColor = false;
            // 
            // frmAddPath
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(530, 134);
            Controls.Add(butOK);
            Controls.Add(butCancel);
            Controls.Add(lblInfo);
            Controls.Add(txtPath);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAddPath";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Add Path";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPath;
        private Label lblInfo;
        private Button butCancel;
        private Button butOK;
    }
}