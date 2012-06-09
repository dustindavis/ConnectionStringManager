using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using ConnectionStringManager.Properties;

namespace ConnectionStringManager
{
    public class ProfileHelper
    {
        private List<SavedConnection> _connections;
        public ReadOnlyCollection<SavedConnection> SavedConnections { get { return _connections.AsReadOnly(); } }

        public ProfileHelper()
        {
            Load();
        }

        private void Load()
        {
            try
            {
                if (Settings.Default["SavedConnections"] == null)
                {
                    _connections = new List<SavedConnection>();
                    Save();
                }
                else
                {
                    _connections = Deserialize(Settings.Default["SavedConnections"].ToString());
                }
            }
            catch
            {
                _connections = new List<SavedConnection>();
            }
        }

        public void Replace(IEnumerable<SavedConnection> connections)
        {
            _connections.Clear();
            _connections.AddRange(connections);
            Save();
        }

        private void Save()
        {
            Settings.Default["SavedConnections"] = Serialize(_connections);
            Settings.Default.Save();
        }

        public void SaveNewConnection(ConnectionStringEntry entry)
        {
            string connKey = string.Format("{0}_{1}", entry.ProjectName, entry.ConnectionName);

            SavedConnection newConn = new SavedConnection()
            {
                ConnectionString = entry.ConnectionString,
                Name = connKey
            };

            SaveNewConnection(newConn);
        }

        public void SaveNewConnection(SavedConnection newConn)
        {
            _connections.Add(newConn);
            Save();
        }

        public void RemoveConnection(SavedConnection connection)
        {
            _connections.Remove(connection);
            Save();
        }

        private static List<SavedConnection> Deserialize(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<SavedConnection>));
            StringReader sr = new StringReader(xml);
            List<SavedConnection> results = (List<SavedConnection>)xs.Deserialize(sr);

            return results;
        }

        private static string Serialize(List<SavedConnection> obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<SavedConnection>));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, obj);
            sw.Flush();
            string result = sw.ToString();
            sw.Close();

            return result;
        }
    }
}
