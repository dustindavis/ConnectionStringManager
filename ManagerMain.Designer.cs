using EnvDTE;
namespace ConnectionStringManager
{
    partial class ManagerMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerMain));
            this.lstConnectionStrings = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgIcons = new System.Windows.Forms.ImageList(this.components);
            this.tsToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsTestOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.testSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEdit = new System.Windows.Forms.ToolStripButton();
            this.tsOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsFilterList = new System.Windows.Forms.ToolStripDropDownButton();
            this.showAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRemoveSelected = new System.Windows.Forms.ToolStripButton();
            this.tsAddNewConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAddConnectionToSaved = new System.Windows.Forms.ToolStripButton();
            this.tsSetAsSaved = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsSavedConnectionManager = new System.Windows.Forms.ToolStripButton();
            this.tsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstConnectionStrings
            // 
            this.lstConnectionStrings.AllowColumnReorder = true;
            this.lstConnectionStrings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstConnectionStrings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConnectionStrings.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConnectionStrings.FullRowSelect = true;
            this.lstConnectionStrings.HideSelection = false;
            this.lstConnectionStrings.Location = new System.Drawing.Point(0, 25);
            this.lstConnectionStrings.Name = "lstConnectionStrings";
            this.lstConnectionStrings.Size = new System.Drawing.Size(790, 366);
            this.lstConnectionStrings.SmallImageList = this.imgIcons;
            this.lstConnectionStrings.TabIndex = 0;
            this.lstConnectionStrings.UseCompatibleStateImageBehavior = false;
            this.lstConnectionStrings.View = System.Windows.Forms.View.Details;
            this.lstConnectionStrings.DoubleClick += new System.EventHandler(this.lstConnectionStrings_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Project";
            this.columnHeader1.Width = 117;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Config";
            this.columnHeader2.Width = 123;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 128;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Connection String";
            this.columnHeader4.Width = 391;
            // 
            // imgIcons
            // 
            this.imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
            this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgIcons.Images.SetKeyName(0, "Annotate_Help.ico");
            this.imgIcons.Images.SetKeyName(1, "Annotate_Error.ico");
            this.imgIcons.Images.SetKeyName(2, "Annotate_Default.ico");
            // 
            // tsToolStrip
            // 
            this.tsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTestOptions,
            this.tsEdit,
            this.tsOpenFile,
            this.tsRefresh,
            this.toolStripSeparator1,
            this.tsFilterList,
            this.toolStripSeparator2,
            this.tsAddNewConnection,
            this.tsRemoveSelected,
            this.toolStripSeparator3,
            this.tsAddConnectionToSaved,
            this.tsSetAsSaved,
            this.tsSavedConnectionManager});
            this.tsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.tsToolStrip.Name = "tsToolStrip";
            this.tsToolStrip.Size = new System.Drawing.Size(790, 25);
            this.tsToolStrip.TabIndex = 5;
            this.tsToolStrip.Text = "toolStrip1";
            // 
            // tsTestOptions
            // 
            this.tsTestOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsTestOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testSelectedToolStripMenuItem,
            this.testAllToolStripMenuItem});
            this.tsTestOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsTestOptions.Image")));
            this.tsTestOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsTestOptions.Name = "tsTestOptions";
            this.tsTestOptions.Size = new System.Drawing.Size(29, 22);
            this.tsTestOptions.Text = "Test Connection(s)";
            this.tsTestOptions.Click += new System.EventHandler(this.tsTestOptions_Click);
            // 
            // testSelectedToolStripMenuItem
            // 
            this.testSelectedToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("testSelectedToolStripMenuItem.Image")));
            this.testSelectedToolStripMenuItem.Name = "testSelectedToolStripMenuItem";
            this.testSelectedToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.testSelectedToolStripMenuItem.Text = "Test Selected";
            this.testSelectedToolStripMenuItem.Click += new System.EventHandler(this.testSelectedToolStripMenuItem_Click);
            // 
            // testAllToolStripMenuItem
            // 
            this.testAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("testAllToolStripMenuItem.Image")));
            this.testAllToolStripMenuItem.Name = "testAllToolStripMenuItem";
            this.testAllToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.testAllToolStripMenuItem.Text = "Test All";
            this.testAllToolStripMenuItem.Click += new System.EventHandler(this.testAllToolStripMenuItem_Click);
            // 
            // tsEdit
            // 
            this.tsEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsEdit.Image")));
            this.tsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEdit.Name = "tsEdit";
            this.tsEdit.Size = new System.Drawing.Size(23, 22);
            this.tsEdit.Text = "Edit Connection";
            this.tsEdit.ToolTipText = "Edit";
            this.tsEdit.Click += new System.EventHandler(this.tsEdit_Click);
            // 
            // tsOpenFile
            // 
            this.tsOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tsOpenFile.Image")));
            this.tsOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpenFile.Name = "tsOpenFile";
            this.tsOpenFile.Size = new System.Drawing.Size(23, 22);
            this.tsOpenFile.Text = "Open Config File";
            this.tsOpenFile.ToolTipText = "Open config file";
            this.tsOpenFile.Click += new System.EventHandler(this.tsOpenFile_Click);
            // 
            // tsRefresh
            // 
            this.tsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsRefresh.Image")));
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.ToolTipText = "Refresh";
            this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsFilterList
            // 
            this.tsFilterList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsFilterList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAllToolStripMenuItem});
            this.tsFilterList.Image = ((System.Drawing.Image)(resources.GetObject("tsFilterList.Image")));
            this.tsFilterList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFilterList.Name = "tsFilterList";
            this.tsFilterList.Size = new System.Drawing.Size(29, 22);
            this.tsFilterList.Text = "Filter By Project";
            // 
            // showAllToolStripMenuItem
            // 
            this.showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
            this.showAllToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.showAllToolStripMenuItem.Text = "Show All";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsRemoveSelected
            // 
            this.tsRemoveSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRemoveSelected.Image = ((System.Drawing.Image)(resources.GetObject("tsRemoveSelected.Image")));
            this.tsRemoveSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRemoveSelected.Name = "tsRemoveSelected";
            this.tsRemoveSelected.Size = new System.Drawing.Size(23, 22);
            this.tsRemoveSelected.Text = "Delete Connection";
            this.tsRemoveSelected.ToolTipText = "Remove Selected Connection";
            this.tsRemoveSelected.Click += new System.EventHandler(this.tsRemoveSelected_Click);
            // 
            // tsAddNewConnection
            // 
            this.tsAddNewConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAddNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("tsAddNewConnection.Image")));
            this.tsAddNewConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAddNewConnection.Name = "tsAddNewConnection";
            this.tsAddNewConnection.Size = new System.Drawing.Size(23, 22);
            this.tsAddNewConnection.Text = "Add New Connection";
            this.tsAddNewConnection.Click += new System.EventHandler(this.tsAddNewConnection_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsAddConnectionToSaved
            // 
            this.tsAddConnectionToSaved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAddConnectionToSaved.Image = ((System.Drawing.Image)(resources.GetObject("tsAddConnectionToSaved.Image")));
            this.tsAddConnectionToSaved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAddConnectionToSaved.Name = "tsAddConnectionToSaved";
            this.tsAddConnectionToSaved.Size = new System.Drawing.Size(23, 22);
            this.tsAddConnectionToSaved.Text = "Add Connection To Saved";
            this.tsAddConnectionToSaved.Click += new System.EventHandler(this.tsAddConnectionToSaved_Click);
            // 
            // tsSetAsSaved
            // 
            this.tsSetAsSaved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSetAsSaved.Image = ((System.Drawing.Image)(resources.GetObject("tsSetAsSaved.Image")));
            this.tsSetAsSaved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSetAsSaved.Name = "tsSetAsSaved";
            this.tsSetAsSaved.Size = new System.Drawing.Size(29, 22);
            this.tsSetAsSaved.Text = "Change To Saved Connection ";
            // 
            // tsSavedConnectionManager
            // 
            this.tsSavedConnectionManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSavedConnectionManager.Image = ((System.Drawing.Image)(resources.GetObject("tsSavedConnectionManager.Image")));
            this.tsSavedConnectionManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSavedConnectionManager.Name = "tsSavedConnectionManager";
            this.tsSavedConnectionManager.Size = new System.Drawing.Size(23, 22);
            this.tsSavedConnectionManager.Text = "Manage Saved Connections";
            this.tsSavedConnectionManager.Click += new System.EventHandler(this.tsSavedConnectionManager_Click);
            // 
            // ManagerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 391);
            this.Controls.Add(this.lstConnectionStrings);
            this.Controls.Add(this.tsToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagerMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectionString Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagerMain_FormClosing);
            this.Load += new System.EventHandler(this.ManagerMain_Load);
            this.tsToolStrip.ResumeLayout(false);
            this.tsToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstConnectionStrings;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ImageList imgIcons;
        private System.Windows.Forms.ToolStrip tsToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton tsTestOptions;
        private System.Windows.Forms.ToolStripMenuItem testSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsEdit;
        private System.Windows.Forms.ToolStripButton tsOpenFile;
        private System.Windows.Forms.ToolStripButton tsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsFilterList;
        private System.Windows.Forms.ToolStripMenuItem showAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsRemoveSelected;
        private System.Windows.Forms.ToolStripButton tsAddNewConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsAddConnectionToSaved;
        private System.Windows.Forms.ToolStripDropDownButton tsSetAsSaved;
        private System.Windows.Forms.ToolStripButton tsSavedConnectionManager;
    }
}