using System;
using System.Data.SqlClient;

namespace CatMash.Core
{
    public class OpenConnection : IDisposable
    {
        public SqlConnection Connection { get; }

        public OpenConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}