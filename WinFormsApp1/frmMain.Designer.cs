namespace EasyREST
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadProjectToolStripMenuItem = new ToolStripMenuItem();
            saveProjectToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            newProjectToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            toolStripMenuItem4 = new ToolStripMenuItem();
            outputToolStripMenuItem = new ToolStripMenuItem();
            pHPToolStripMenuItem = new ToolStripMenuItem();
            djangoToolStripMenuItem = new ToolStripMenuItem();
            rustToolStripMenuItem = new ToolStripMenuItem();
            cToolStripMenuItem = new ToolStripMenuItem();
            cToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            AddPathToolStripMenuItem = new ToolStripMenuItem();
            projectToolStripMenuItem = new ToolStripMenuItem();
            projectOptionsToolStripMenuItem = new ToolStripMenuItem();
            lblLoading = new Label();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = false;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.White;
            webView2.Location = new Point(12, 130);
            webView2.Name = "webView2";
            webView2.Size = new Size(850, 498);
            webView2.TabIndex = 1;
            webView2.ZoomFactor = 1D;
            webView2.NavigationCompleted += webView2_NavigationCompleted;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, projectToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1120, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadProjectToolStripMenuItem, saveProjectToolStripMenuItem, toolStripMenuItem1, newProjectToolStripMenuItem, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem2, AddPathToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // loadProjectToolStripMenuItem
            // 
            loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            loadProjectToolStripMenuItem.Size = new Size(180, 22);
            loadProjectToolStripMenuItem.Text = "&Load Project";
            loadProjectToolStripMenuItem.Click += loadProjectToolStripMenuItem_Click;
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            saveProjectToolStripMenuItem.Size = new Size(180, 22);
            saveProjectToolStripMenuItem.Text = "&Save Project";
            saveProjectToolStripMenuItem.Click += saveProjectToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // newProjectToolStripMenuItem
            // 
            newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            newProjectToolStripMenuItem.Size = new Size(180, 22);
            newProjectToolStripMenuItem.Text = "&New Project";
            newProjectToolStripMenuItem.Click += newProjectToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(177, 6);
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.DropDownItems.AddRange(new ToolStripItem[] { outputToolStripMenuItem });
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(180, 22);
            toolStripMenuItem4.Text = "&Project...";
            // 
            // outputToolStripMenuItem
            // 
            outputToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pHPToolStripMenuItem, djangoToolStripMenuItem, rustToolStripMenuItem, cToolStripMenuItem, cToolStripMenuItem1 });
            outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            outputToolStripMenuItem.Size = new Size(180, 22);
            outputToolStripMenuItem.Text = "&Output";
            // 
            // pHPToolStripMenuItem
            // 
            pHPToolStripMenuItem.Name = "pHPToolStripMenuItem";
            pHPToolStripMenuItem.Size = new Size(180, 22);
            pHPToolStripMenuItem.Text = "PHP";
            pHPToolStripMenuItem.Click += pHPToolStripMenuItem_Click;
            // 
            // djangoToolStripMenuItem
            // 
            djangoToolStripMenuItem.Name = "djangoToolStripMenuItem";
            djangoToolStripMenuItem.Size = new Size(180, 22);
            djangoToolStripMenuItem.Text = "Django";
            // 
            // rustToolStripMenuItem
            // 
            rustToolStripMenuItem.Name = "rustToolStripMenuItem";
            rustToolStripMenuItem.Size = new Size(180, 22);
            rustToolStripMenuItem.Text = "Rust";
            // 
            // cToolStripMenuItem
            // 
            cToolStripMenuItem.Name = "cToolStripMenuItem";
            cToolStripMenuItem.Size = new Size(180, 22);
            cToolStripMenuItem.Text = "C++";
            // 
            // cToolStripMenuItem1
            // 
            cToolStripMenuItem1.Name = "cToolStripMenuItem1";
            cToolStripMenuItem1.Size = new Size(180, 22);
            cToolStripMenuItem1.Text = "C";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(177, 6);
            // 
            // AddPathToolStripMenuItem
            // 
            AddPathToolStripMenuItem.Name = "AddPathToolStripMenuItem";
            AddPathToolStripMenuItem.Size = new Size(180, 22);
            AddPathToolStripMenuItem.Text = "&Add Path";
            AddPathToolStripMenuItem.Click += AddPathToolStripMenuItem_Click;
            // 
            // projectToolStripMenuItem
            // 
            projectToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { projectOptionsToolStripMenuItem });
            projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            projectToolStripMenuItem.Size = new Size(56, 20);
            projectToolStripMenuItem.Text = "Project";
            // 
            // projectOptionsToolStripMenuItem
            // 
            projectOptionsToolStripMenuItem.Name = "projectOptionsToolStripMenuItem";
            projectOptionsToolStripMenuItem.Size = new Size(180, 22);
            projectOptionsToolStripMenuItem.Text = "Options";
            projectOptionsToolStripMenuItem.Click += projectOptionsToolStripMenuItem_Click;
            // 
            // lblLoading
            // 
            lblLoading.AutoSize = true;
            lblLoading.Font = new Font("Segoe UI", 20F);
            lblLoading.Location = new Point(481, 297);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(132, 37);
            lblLoading.TabIndex = 3;
            lblLoading.Text = "Loading...";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1120, 668);
            Controls.Add(lblLoading);
            Controls.Add(webView2);
            Controls.Add(menuStrip1);
            Name = "frmMain";
            Text = "Easy REST";
            Load += Form1_Load;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadProjectToolStripMenuItem;
        private ToolStripMenuItem saveProjectToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem newProjectToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem AddPathToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem outputToolStripMenuItem;
        private ToolStripMenuItem pHPToolStripMenuItem;
        private ToolStripMenuItem djangoToolStripMenuItem;
        private ToolStripMenuItem rustToolStripMenuItem;
        private ToolStripMenuItem cToolStripMenuItem;
        private ToolStripMenuItem cToolStripMenuItem1;
        private Label lblLoading;
        private ToolStripMenuItem projectToolStripMenuItem;
        private ToolStripMenuItem projectOptionsToolStripMenuItem;
    }
}
