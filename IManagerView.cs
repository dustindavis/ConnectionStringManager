using System;
using System.Collections.Generic;

namespace ConnectionStringManager
{
    public delegate void ConnectionAction(ConnectionStringEntry entry, bool silent);
    public delegate void ConnectionUpdateAction(string connectionName, ConnectionStringEntry entry, bool silent);
    public delegate void SwapConnectionAction(ConnectionStringEntry entry, SavedConnection savedConn, bool silent);
    
    public interface IManagerView : IGenericView
    {
        event ConnectionUpdateAction UpdateConnection;
        event ConnectionAction OpenContainingConfig;
        event EventHandler RefreshConnections;
        event ConnectionAction AddConnection;
        event ConnectionAction RemoveConnection;

        event ConnectionAction SaveConnection;
        event SwapConnectionAction ChangeConnection;

        IList<ConnectionStringEntry> ConnectionStrings { get; set; }
        IList<SavedConnection> SavedConnections { get; set; }
    }
}
