using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ConnectionStringManager
{
    public partial class ManagerMain : Form, IManagerView
    {
        public event ConnectionAction TestConnection;
        public event ConnectionUpdateAction UpdateConnection;
        public event ConnectionAction OpenContainingConfig;
        public event EventHandler RefreshConnections;
        public event ConnectionAction AddConnection;
        public event ConnectionAction RemoveConnection;

        public event ConnectionAction SaveConnection;
        public event SwapConnectionAction ChangeConnection;

        private List<ConnectionStringEntry> _connectionStrings;
        public IList<ConnectionStringEntry> ConnectionStrings
        {
            get { return _connectionStrings; }
            set { _connectionStrings = value.ToList(); BindToGrid(); InitializeToolStrip(); }
        }

        private List<SavedConnection> _savedConnections;
        public IList<SavedConnection> SavedConnections
        {
            get { return _savedConnections; }
            set { _savedConnections = value.ToList(); InitializeSavedConnections(); }
        }

        private string _filter = string.Empty;
        private delegate void InvokeDelegate();

        public ManagerMain()
        {
            InitializeComponent();
        }

        private void InitializeSavedConnections()
        {
            if (tsToolStrip.InvokeRequired)
            {
                tsToolStrip.Invoke(new InvokeDelegate(InitializeSavedConnections));
            }
            else
            {
                tsSetAsSaved.DropDownItems.Clear();

                foreach (SavedConnection conn in _savedConnections)
                {
                    tsSetAsSaved.DropDownItems.Add(conn.Name);
                    tsSetAsSaved.DropDownItems[tsSetAsSaved.DropDownItems.Count - 1].Tag = conn;
                    tsSetAsSaved.DropDownItems[tsSetAsSaved.DropDownItems.Count - 1].Click += new EventHandler(ChangeEntryToSaved_Click);
                }
            }
        }

        private void InitializeToolStrip()
        {
            if (tsToolStrip.InvokeRequired)
            {
                tsToolStrip.Invoke(new InvokeDelegate(InitializeToolStrip));
            }
            else
            {
                tsFilterList.DropDownItems.Clear();
                tsFilterList.DropDownItems.Add("Show All");
                tsFilterList.DropDownItems[0].Click += new EventHandler(ManagerMain_Click);

                foreach (ConnectionStringEntry conn in _connectionStrings.Distinct<ConnectionStringEntry>(new ConnectionStringEntryComparer()).OrderBy(c=>c.ProjectName))
                {
                    tsFilterList.DropDownItems.Add(conn.ProjectName + " -> " + conn.ConfigFile);
                    tsFilterList.DropDownItems[tsFilterList.DropDownItems.Count - 1].Tag = conn;
                    tsFilterList.DropDownItems[tsFilterList.DropDownItems.Count - 1].Click += new EventHandler(ManagerMain_Click);
                }
            }
        }

        void ChangeEntryToSaved_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var connection = item.Tag as SavedConnection;
            var selectedEntry = GetSelectedEntry();

            if(selectedEntry == ConnectionStringEntry.Empty || connection == null) { return; }

            if (this.ChangeConnection != null)
            {
                this.ChangeConnection.Invoke(selectedEntry, connection, false);
            }
        }

        void ManagerMain_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var connection = item.Tag as ConnectionStringEntry;

            if (connection == null) { _filter = string.Empty; }
            else { _filter = connection.ProjectName; }

            BindToGrid();
        }

        private void ManagerMain_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
        }

        private void BindToGrid()
        {
            if (lstConnectionStrings.InvokeRequired)
            {
                lstConnectionStrings.Invoke(new InvokeDelegate(BindToGrid));
            }
            else
            {
                    lstConnectionStrings.Items.Clear();
                    lstConnectionStrings.BeginUpdate();

                    foreach (ConnectionStringEntry entry in _connectionStrings.OrderBy(c => c.ProjectName).ThenBy(c => c.ConfigFile).ThenBy(c => c.ConnectionName))
                    {
                        if (!string.IsNullOrEmpty(_filter) && !entry.ProjectName.Equals(_filter)) { continue; }

                        ListViewItem lvi = new ListViewItem(entry.ProjectName);
                        lvi.ImageIndex = (int)entry.Status;
                        lvi.SubItems.Add(entry.ConfigFile);
                        lvi.SubItems.Add(entry.ConnectionName);
                        lvi.SubItems.Add(entry.ConnectionString);
                        lvi.Tag = entry;
                        lstConnectionStrings.Items.Add(lvi);
                    }

                    this.Cursor = Cursors.Default;

                    lstConnectionStrings.EndUpdate();

                    AdjustListViewColumnWidths();

            }
        }

        private void AdjustListViewColumnWidths()
        {
            if (lstConnectionStrings.Items.Count < 1)
            {
                lstConnectionStrings.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                lstConnectionStrings.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void SetupConnectionTest(ConnectionStringEntry entry, bool silent)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.TestConnection != null)
                {
                    this.TestConnection.Invoke(entry, silent);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        private void lstConnectionStrings_DoubleClick(object sender, EventArgs e)
        {
            OpenConfigFile();
        }

        private void OpenConfigFile()
        {
            ConnectionStringEntry entry = GetSelectedEntry();

            if (entry == ConnectionStringEntry.Empty) { return; }

            if (this.OpenContainingConfig != null)
            {
                this.OpenContainingConfig(entry, false);
            }
        }

        private void ManagerMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        public void RefreshView()
        {
            BindToGrid();
            InitializeToolStrip();
            InitializeSavedConnections();
        }

        public void SetError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetMessage(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private ConnectionStringEntry GetSelectedEntry()
        {
            if (lstConnectionStrings.SelectedIndices.Count < 1) { return ConnectionStringEntry.Empty; }

            ConnectionStringEntry entry = lstConnectionStrings.SelectedItems[0].Tag as ConnectionStringEntry;

            return entry;
        }

        private void tsTestOptions_Click(object sender, EventArgs e)
        {
            TestSelectedConnection(); 
        }

        private void TestAllConnections()
        {
            foreach (ConnectionStringEntry conn in _connectionStrings)
            {
                if (this.TestConnection != null)
                {
                    this.TestConnection.Invoke(conn, true);
                }
            }
        }

        private void testSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestSelectedConnection();
        }

        private void TestSelectedConnection()
        {
            var entry = GetSelectedEntry();
            if (entry == ConnectionStringEntry.Empty) { return; }

            SetupConnectionTest(entry, false);
        }

        private void testAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAllConnections();
        }

        private void tsEdit_Click(object sender, EventArgs e)
        {

            ConnectionStringEntry entry = GetSelectedEntry();
            if (entry == ConnectionStringEntry.Empty) { return; }

            ConnectionStringEditor editor = new ConnectionStringEditor();
            GenericConnectionPresenter presenter = new GenericConnectionPresenter(editor);

            if (editor.ShowDialog(entry.ConnectionName, entry.ConnectionString) == System.Windows.Forms.DialogResult.Cancel) { return; }

            string connectionName = entry.ConnectionName;
            entry.ConnectionString = editor.ConnectionString;
            entry.ConnectionName = editor.ConnectionName;
            lstConnectionStrings.SelectedItems[0].SubItems[2].Text = entry.ConnectionName;
            lstConnectionStrings.SelectedItems[0].SubItems[3].Text = entry.ConnectionString;

            if (this.UpdateConnection != null)
            {
                this.UpdateConnection.Invoke(connectionName, entry, false);
            }
        }

        private void tsRefresh_Click(object sender, EventArgs e)
        {
            if (this.RefreshConnections != null)
            {
                this.RefreshConnections.Invoke(this, EventArgs.Empty);
            }
        }

        private void tsOpenFile_Click(object sender, EventArgs e)
        {
            OpenConfigFile();
        }

        private void tsRemoveSelected_Click(object sender, EventArgs e)
        {
            var entry = GetSelectedEntry();
            if (entry == ConnectionStringEntry.Empty) { return; }

            if (this.RemoveConnection != null)
            {
                this.RemoveConnection.Invoke(entry, false);
            }
        }

        private void tsAddNewConnection_Click(object sender, EventArgs e)
        {
            NewConnection view = new NewConnection();
            GenericConnectionPresenter presenter = new GenericConnectionPresenter(view);

            var result = view.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK) { return; }

            if (this.AddConnection != null)
            {
                foreach (ConnectionStringEntry conn in view.NewConnections)
                {
                    this.AddConnection.Invoke(conn, false);
                }
            }
        }

        private void tsAddConnectionToSaved_Click(object sender, EventArgs e)
        {
            var entry = GetSelectedEntry();
            if (entry == ConnectionStringEntry.Empty) { return; }

            if (this.SaveConnection != null)
            {
                this.SaveConnection.Invoke(entry, false);
            }
        }

        private void tsSavedConnectionManager_Click(object sender, EventArgs e)
        {
            SavedConnectionsManager connMan = new SavedConnectionsManager();
            SavedConnectionPresenter presenter = new SavedConnectionPresenter((ISavedConnectionView)connMan);

            connMan.ShowDialog();
        }
    }
}
