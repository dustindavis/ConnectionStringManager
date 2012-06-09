using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ConnectionStringManager
{
    public partial class SavedConnectionsManager : Form, ISavedConnectionView
    {
        public SavedConnectionsManager()
        {
            InitializeComponent();
        }

        public event ConnectionAction TestConnection;
        public IList<ConnectionStringEntry> ConnectionStrings { get; set; }
        public IList<SavedConnection> SavedConnections
        {
            get { return _savedConnections; }
            set
            {
                _savedConnections = value.ToList();
                BindToGrid();
            }
        }
        public event UpdateConnectionAction UpdateSavedConnections;
        
        private delegate void InvokeMethod();
        private List<SavedConnection> _savedConnections;

        private void BindToGrid()
        {
            if (lstConnections.InvokeRequired)
            {
                lstConnections.Invoke(new InvokeMethod(BindToGrid));
            }
            else
            {
                lstConnections.Items.Clear();
                foreach (SavedConnection conn in SavedConnections)
                {
                    ListViewItem itm = new ListViewItem();
                    itm.Text = conn.Name;
                    itm.SubItems.Add(conn.ConnectionString);
                    itm.Tag = conn;

                    lstConnections.Items.Add(itm);
                }

                if (lstConnections.Items.Count < 1)
                {
                    lstConnections.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                else
                {
                    lstConnections.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
        }

        public void RefreshView()
        {
            BindToGrid();
        }

        public void SetError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetMessage(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

         private void tsAddNewConnection_Click(object sender, EventArgs e)
        {
            SavedConnection newConn = new SavedConnection() { Name = "New Connection", ConnectionString = "DataSource=.;Initial Catalog=;Trusted Connection=true;" };
            ListViewItem itm = new ListViewItem();
            itm.Text = newConn.Name;
            itm.SubItems.Add(newConn.ConnectionString);
            itm.Tag = newConn;
            lstConnections.Items.Add(itm);

            SavedConnections.Add(newConn);
        }

        private SavedConnection GetSelectedItem()
        {
            if (lstConnections.SelectedIndices.Count < 1) { return SavedConnection.Empty; }

            SavedConnection conn = lstConnections.SelectedItems[0].Tag as SavedConnection;

            return conn;
        }

        private void tsRemove_Click(object sender, EventArgs e)
        {
            if (lstConnections.SelectedIndices.Count < 1) { return; }

            lstConnections.Items.RemoveAt(lstConnections.SelectedIndices[0]);
        }

        private void lstConnections_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var entry = GetSelectedItem();
            if (entry == SavedConnection.Empty) { return; }

            ConnectionStringEditor editor = new ConnectionStringEditor();

            var result = editor.ShowDialog(entry.Name, entry.ConnectionString);

            if (result != System.Windows.Forms.DialogResult.OK) { return; }

            entry.Name = editor.ConnectionName;
            entry.ConnectionString = editor.ConnectionString;
            lstConnections.SelectedItems[0].SubItems[0].Text = entry.Name;
            lstConnections.SelectedItems[0].SubItems[1].Text = entry.ConnectionString;
            BindToGrid();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            PersistSavedEntries();
        }

        private void PersistSavedEntries()
        {
            List<SavedConnection> connections = new List<SavedConnection>();
            foreach (ListViewItem itm in lstConnections.Items)
            {
                connections.Add(new SavedConnection() { Name = itm.Text, ConnectionString = itm.SubItems[1].Text });
            }

            this.SavedConnections.Clear();
            this.SavedConnections = connections;

            if (this.UpdateSavedConnections != null)
            {
                this.UpdateSavedConnections.Invoke(connections);
            }

        }
    }
}
