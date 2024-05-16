namespace EasyREST
{
    partial class frmProjectOptions
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
            lblTitle = new Label();
            lblPort = new Label();
            lblDescription = new Label();
            txtTitle = new TextBox();
            txtDescription = new TextBox();
            txtPort = new TextBox();
            butCancel = new Button();
            butOK = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(29, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(12, 106);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 1;
            lblPort.Text = "Port";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(12, 55);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(12, 27);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(375, 23);
            txtTitle.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(12, 73);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(375, 23);
            txtDescription.TabIndex = 4;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(12, 124);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(375, 23);
            txtPort.TabIndex = 5;
            txtPort.KeyPress += txtPort_KeyPress;
            // 
            // butCancel
            // 
            butCancel.BackColor = SystemColors.ButtonFace;
            butCancel.Location = new Point(226, 175);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(80, 46);
            butCancel.TabIndex = 34;
            butCancel.Text = "&Cancel";
            butCancel.UseVisualStyleBackColor = false;
            // 
            // butOK
            // 
            butOK.BackColor = SystemColors.ButtonFace;
            butOK.Location = new Point(312, 175);
            butOK.Name = "butOK";
            butOK.Size = new Size(75, 46);
            butOK.TabIndex = 33;
            butOK.Text = "OK";
            butOK.UseVisualStyleBackColor = false;
            // 
            // frmProjectOptions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(405, 238);
            Controls.Add(butCancel);
            Controls.Add(butOK);
            Controls.Add(txtPort);
            Controls.Add(txtDescription);
            Controls.Add(txtTitle);
            Controls.Add(lblDescription);
            Controls.Add(lblPort);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProjectOptions";
            ShowInTaskbar = false;
            Text = "Project Options";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblPort;
        private Label lblDescription;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private TextBox txtPort;
        private Button butCancel;
        private Button butOK;
    }
}