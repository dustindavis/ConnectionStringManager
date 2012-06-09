using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;

namespace ConnectionStringManager
{
    public class ConnectionStringEntry
    {
        public static readonly ConnectionStringEntry Empty = new ConnectionStringEntry();

        public string ProjectName { get; set; }
        public string ConfigFile { get; set; }
        public string ConfigPath { get; set; }
        public string ConnectionName { get; set; }
        public string ConnectionString { get; set; }
        public ConnectionStatus Status { get; set; }

        public ProjectItem ProjectItem { get; set; }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", ProjectName, ConfigFile);
        }
    }

    public enum ConnectionStatus
    {
        Unknown = 0,
        Invalid = 1,
        Valid = 2

    }
}
