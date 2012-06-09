using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectionStringManager
{
    public class ConnectionStringEntryComparer : IEqualityComparer<ConnectionStringEntry>
    {
        public bool Equals(ConnectionStringEntry x, ConnectionStringEntry y)
        {
            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(ConnectionStringEntry obj)
        {
            return obj.ToString().GetHashCode();


        }
    }

    public class SavedConnectionComparer : IEqualityComparer<SavedConnection>
    {
        public bool Equals(SavedConnection x, SavedConnection y)
        {
            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(SavedConnection obj)
        {
            return obj.ToString().GetHashCode();


        }
    }
}
