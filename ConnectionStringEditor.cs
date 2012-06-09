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
    public partial class ConnectionStringEditor : Form, IGenericView
    {
        private List<SavedConnection> _savedConnections;
        public IList<SavedConnection> SavedConnections
        {
            get { return _savedConnections; }
            set { _savedConnections = value.ToList(); }
        }

        public string ConnectionString { get { return txtConnStr.Text; } }
        public string ConnectionName { get { return txtConnectionName.Text; } }
        private bool _isSaving = false;

        public ConnectionStringEditor()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string connectionName, string connectionString)
        {
            this.txtConnectionName.Text = connectionName;
            this.txtConnStr.Text = connectionString;
            return this.ShowDialog();
        }

      
        private void ConnectionStringEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isSaving) { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; }
        }

        public event ConnectionAction TestConnection;

        public IList<ConnectionStringEntry> ConnectionStrings { get; set; }
        
        public void RefreshView()
        {
            
        }

        public void SetError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetMessage(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            _isSaving = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void tsTestConnection_Click(object sender, EventArgs e)
        {
            if (this.TestConnection != null)
            {
                this.TestConnection(new ConnectionStringEntry() { ConnectionString = txtConnStr.Text }, true);
            }
        }
    }
}
