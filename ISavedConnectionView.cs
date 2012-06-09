using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectionStringManager
{
    public delegate void UpdateConnectionAction(IList<SavedConnection> connections);
    public interface ISavedConnectionView : IGenericView
    {
        event UpdateConnectionAction UpdateSavedConnections;
                
    }
}
