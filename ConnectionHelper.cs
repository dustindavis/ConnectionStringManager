using System.Data.SqlClient;

namespace ConnectionStringManager
{
    public sealed class ConnectionHelper
    {
        private ConnectionHelper() { }
	
        public static bool ValidateConnection(ConnectionStringEntry entry)
        {
            try
            {
                SqlConnection conn = new SqlConnection(entry.ConnectionString);
                conn.Open();
                conn.Close();

                entry.Status = ConnectionStatus.Valid;
                return true;
            }
            catch
            {
                entry.Status = ConnectionStatus.Invalid;
                return false;
            }

        }
    }
}
