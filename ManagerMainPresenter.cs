using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;

namespace ConnectionStringManager
{
    public class ManagerMainPresenter : IDisposable
    {
        private IManagerView _view;
        private Solution _solution;
        private ManagerModel _model;
        private string _solutionPath = string.Empty;
        private ConcurrentBag<string> _configFilePaths;
        private ConcurrentBag<ConnectionStringEntry> _foundConnections;

        public ManagerMainPresenter(Solution solution, IManagerView view)
        {
            if (solution == null || string.IsNullOrEmpty(solution.FullName)) { return; }
            
            _solution = solution;
            _view = view;
            _solutionPath = Directory.GetParent(_solution.FullName).FullName;

            if (string.IsNullOrEmpty(_solutionPath)) { return; }


            _model = ManagerModel.Instance;
            Initialize();

            _configFilePaths = new ConcurrentBag<string>();
            _foundConnections = new ConcurrentBag<ConnectionStringEntry>();
            Task t = Task.Factory.StartNew(new Action(() =>
            {
                ScanFilesystem(_solutionPath);
                ConvertFilesToProjectItems();
                _model.Entries = _foundConnections.ToList();
                _view.ConnectionStrings = _foundConnections.ToList();
            }));

            ProfileHelper ph = new ProfileHelper();
            _view.SavedConnections = ph.SavedConnections;
        }

        private void ScanFilesystem(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            Parallel.ForEach(di.GetDirectories(), new Action<DirectoryInfo>((d) =>
            {
                ScanFilesystem(d.FullName);
            }));

            foreach (FileInfo fi in di.GetFiles("*.config"))
            {
                _configFilePaths.Add(fi.FullName);
            }
        }

        private void ConvertFilesToProjectItems()
        {
            foreach (string config in _configFilePaths)
            {
                var item = _solution.FindProjectItem(config);

                if (item != null)
                {
                    ProcessConfig(config, item);
                }
            }
        }

        private void Initialize()
        {
            _view.OpenContainingConfig += new ConnectionAction(_view_OpenContainingConfig);
            _view.RefreshConnections += new EventHandler(_view_RefreshConnections);
            _view.TestConnection += new ConnectionAction(_view_TestConnection);
            _view.UpdateConnection += new ConnectionUpdateAction(_view_UpdateConnection);
            _view.AddConnection += new ConnectionAction(_view_AddConnection);
            _view.RemoveConnection += new ConnectionAction(_view_DeleteConnection);
            _view.SaveConnection += new ConnectionAction(_view_SaveConnection);
            _view.ChangeConnection += new SwapConnectionAction(_view_ChangeConnection);
        }

        void _view_ChangeConnection(ConnectionStringEntry entry, SavedConnection savedConn, bool silent)
        {
            entry.ConnectionString = savedConn.ConnectionString;
            WriteConfigChanges(entry.ConnectionName, entry);
            _view.RefreshView();
        }

        void _view_SaveConnection(ConnectionStringEntry entry, bool silent)
        {
            ProfileHelper ph = new ProfileHelper();
            ph.SaveNewConnection(entry);
            _view.RefreshView();
        }

        private void ProcessConfig(string configFile, ProjectItem configItem)
        {
            XDocument xdoc = XDocument.Load(configFile);

            var csNode = xdoc.Descendants(XName.Get("connectionStrings")).FirstOrDefault();
            if (csNode == null) { return; }

            foreach (XElement cs in csNode.Elements())
            {
                if (!cs.Name.LocalName.Equals("add")) { continue; }

                ConnectionStringEntry entry = new ConnectionStringEntry();
                entry.ConfigFile = configItem.Name;
                entry.ConfigPath = configFile;
                entry.ProjectName = configItem.ContainingProject.Name;

                entry.ConnectionName = cs.Attribute(XName.Get("name")).Value;
                entry.ConnectionString = cs.Attribute(XName.Get("connectionString")).Value;
                entry.ProjectItem = configItem;

                _foundConnections.Add(entry);
            }
        }

        #region View Event Hanbdlers
        private void _view_AddConnection(ConnectionStringEntry entry, bool silent)
        {
            AddConnectionToConfig(entry);
            _view.ConnectionStrings = _model.Entries;
        }

        private void _view_DeleteConnection(ConnectionStringEntry entry, bool silent)
        {
            RemoveConnectionFromConfig(entry);
            _view.ConnectionStrings = _model.Entries;
            
        }

        private void _view_RefreshConnections(object sender, EventArgs e)
        {
            ScanFilesystem(_solutionPath);
        }

        private void _view_OpenContainingConfig(ConnectionStringEntry entry, bool silent)
        {
            OpenConfigInIDE(entry);
        }

        private void _view_UpdateConnection(string connectionName, ConnectionStringEntry entry, bool silent)
        {
            WriteConfigChanges(connectionName, entry);
        }

        private void _view_TestConnection(ConnectionStringEntry entry, bool silent)
        {
            var result = ConnectionHelper.ValidateConnection(entry);
            if (!silent && result) { _view.SetMessage("Success!"); }
            if (!silent && !result) { _view.SetError("Failed!"); }

            _view.RefreshView();

        }
        #endregion

        private static void OpenConfigInIDE(ConnectionStringEntry entry)
        {
            var file = entry.ProjectItem.Open(Constants.vsViewKindCode);
            file.Visible = true;
        }

        private void AddConnectionToConfig(ConnectionStringEntry entry)
        {
            try
            {
                if (!entry.ProjectItem.IsOpen)
                {
                    entry.ProjectItem.Open(Constants.vsViewKindCode);
                }

                XDocument xdoc = XDocument.Load(entry.ConfigPath);

                var csNode = xdoc.Descendants(XName.Get("connectionStrings")).FirstOrDefault();
                if (csNode == null) { return; }

                XElement newNode = new XElement(XName.Get("add"));
                newNode.SetAttributeValue(XName.Get("name"), entry.ConnectionName);
                newNode.SetAttributeValue(XName.Get("connectionString"), entry.ConnectionString);

                csNode.AddFirst(newNode);

                xdoc.Save(entry.ConfigPath);

                _model.Entries.Add(entry);
            }
            catch(Exception ex)
            {
                _view.SetError(ex.Message);
            }
        }

        private void RemoveConnectionFromConfig(ConnectionStringEntry entry)
        {
            if (!entry.ProjectItem.IsOpen)
            {
                entry.ProjectItem.Open(Constants.vsViewKindCode);
            }

            XDocument xdoc = XDocument.Load(entry.ConfigPath);

            var csNode = xdoc.Descendants(XName.Get("connectionStrings")).FirstOrDefault();
            if (csNode == null) { return; }

            foreach (XElement cs in csNode.Elements())
            {
                if (!cs.Name.LocalName.Equals("add")) { continue; }
                if (!cs.Attribute(XName.Get("name")).Value.Equals(entry.ConnectionName)) { continue; }

                cs.Remove();
                xdoc.Save(entry.ConfigPath);
            }

            _model.Entries.Remove(entry);
        }

        private void WriteConfigChanges( string connectionName, ConnectionStringEntry entry)
        {
            if (!entry.ProjectItem.IsOpen)
            {
                entry.ProjectItem.Open(Constants.vsViewKindCode);
            }

            XDocument xdoc = XDocument.Load(entry.ConfigPath);

            var csNode = xdoc.Descendants(XName.Get("connectionStrings")).FirstOrDefault();
            if (csNode == null) { return; }

            foreach (XElement cs in csNode.Elements())
            {
                if (!cs.Name.LocalName.Equals("add")) { continue; }
                if (!cs.Attribute(XName.Get("name")).Value.Equals(connectionName)) { continue; }

                cs.Attribute(XName.Get("name")).Value = entry.ConnectionName;
                cs.Attribute(XName.Get("connectionString")).Value = entry.ConnectionString;
            }

            xdoc.Save(entry.ConfigPath);
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _view.OpenContainingConfig -= _view_OpenContainingConfig;
                _view.RefreshConnections -= _view_RefreshConnections;
                _view.TestConnection -= _view_TestConnection;
                _view.UpdateConnection -= _view_UpdateConnection;
                _view.AddConnection -= _view_AddConnection;
                _view.RemoveConnection -= _view_DeleteConnection;
            }
        }
    }
}
