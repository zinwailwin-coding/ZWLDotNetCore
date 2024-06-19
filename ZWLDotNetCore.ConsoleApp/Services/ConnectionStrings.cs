using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWLDotNetCore.ConsoleApp.Services
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder =
            new SqlConnectionStringBuilder()
            {
                DataSource = "DHO-LP-23-006\\SQLSERVER2022",
                InitialCatalog = "DotNetTrainging4",
                UserID = "sa",
                Password = "sa@123",
                TrustServerCertificate = true
                //DataSource = "DHO-LP-23-006\\SQLSERVER2022",
                //InitialCatalog = "DotNetTrainging4",
                //UserID = "sa",
                //Password = "sa@123"
            };
    }
}
