using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConnectionStringManager
{
    public partial class NewConnection : Form, IGenericView
    {
        public List<ConnectionStringEntry> NewConnections { get; private set; }

        public NewConnection()
        {
            InitializeComponent();
            NewConnections = new List<ConnectionStringEntry>();
            SavedConnections = new List<SavedConnection>();
        }

        public event ConnectionAction TestConnection;

        private List<ConnectionStringEntry> _connectionStrings;
        public IList<ConnectionStringEntry> ConnectionStrings
        {
            get { return _connectionStrings; }
            set { _connectionStrings = value.ToList(); RefreshView(); }
        }

        private List<SavedConnection> _savedConnections;
        public IList<SavedConnection> SavedConnections
        {
            get { return _savedConnections; }
            set { _savedConnections = value.ToList(); PopulateSavedConnectionsList(); }
        }

        public void RefreshView()
        {
            PopulateConfigList();
            PopulateSavedConnectionsList();
        }

        private void PopulateConfigList()
        {
            chkLstConfigs.Items.Clear();

            List<ConnectionStringEntry> unique = _connectionStrings.Distinct<ConnectionStringEntry>(new ConnectionStringEntryComparer()).ToList();

            foreach (ConnectionStringEntry conn in unique )
            {
                chkLstConfigs.Items.Add(conn, false);
            }
        }

        private void PopulateSavedConnectionsList()
        {
            chkLstSavedConnections.Items.Clear();
            foreach (SavedConnection conn in _savedConnections)
            {
                chkLstSavedConnections.Items.Add(conn, false);
            }
        }

        public void SetError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetMessage(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsTestConnection_Click(object sender, EventArgs e)
        {
            if (this.TestConnection != null)
            {
                this.TestConnection(new ConnectionStringEntry() { ConnectionString = txtConnStr.Text }, true);
            }
        }

        private void tsAddConnection_Click(object sender, EventArgs e)
        {
            foreach (var itm in chkLstConfigs.CheckedItems)
            {
                ConnectionStringEntry conn = itm as ConnectionStringEntry;

                if (!string.IsNullOrEmpty(txtConnectionName.Text) && !string.IsNullOrEmpty(txtConnStr.Text))
                {
                    NewConnections.Add(new ConnectionStringEntry()
                    {
                        ConnectionName = txtConnectionName.Text,
                        ConnectionString = txtConnStr.Text,
                        ConfigPath = conn.ConfigPath,
                        ConfigFile = conn.ConfigFile,
                        ProjectName = conn.ProjectName,
                        ProjectItem = conn.ProjectItem
                    });
                }

                foreach (var savedIitm in chkLstSavedConnections.CheckedItems)
                {
                    SavedConnection savedConn = savedIitm as SavedConnection;

                    NewConnections.Add(new ConnectionStringEntry()
                    {
                        ConnectionName = savedConn.Name,
                        ConnectionString = savedConn.ConnectionString,
                        ConfigPath = conn.ConfigPath,
                        ConfigFile = conn.ConfigFile,
                        ProjectName = conn.ProjectName,
                        ProjectItem = conn.ProjectItem
                    });
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
