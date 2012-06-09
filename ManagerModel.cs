using System.Collections.Generic;

namespace ConnectionStringManager
{
    public class ManagerModel
    {
        private static ManagerModel _instance;
        public static ManagerModel Instance
        {
            get
            {
                if (_instance == null) { _instance = new ManagerModel(); }
                return _instance;
            }
        }

        public List<ConnectionStringEntry> Entries { get; set; }

        private ManagerModel() { }
    }
}
