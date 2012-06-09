using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectionStringManager
{
    public class SavedConnection
    {
        public static readonly SavedConnection Empty = new SavedConnection() { Name = string.Empty, ConnectionString = string.Empty };

        public string Name { get; set; }
        public string ConnectionString { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
