﻿using System.Data.SqlClient;

namespace ZWLDotNetCore.RestApiWithNLayer
{
    internal static class ConnectionStrings
    {

        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DHO-LP-23-006\\SQLSERVER2022",
            InitialCatalog = "DotNetTrainging4",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };

    }
}
