using System.Data.SqlClient;

namespace ZWLDotNetCore.WinFormsApp
{
    internal static class ConnectionStrings
    {

        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DHO-LP-23-006\\SQLSERVER2022",
            InitialCatalog = "DotNetTrainging4",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };

    }
}
