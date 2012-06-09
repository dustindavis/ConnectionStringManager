using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectionStringManager
{
    public interface IGenericView
    {
        event ConnectionAction TestConnection;

        IList<ConnectionStringEntry> ConnectionStrings { get; set; }
        IList<SavedConnection> SavedConnections { get; set; }
            
        void RefreshView();
        void SetError(string message);
        void SetMessage(string message);
        void Show();
    }
}
