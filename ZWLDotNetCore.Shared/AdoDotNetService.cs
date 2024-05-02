using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZWLDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query,params AdoDotNetParameters[] param)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (param is not null && param.Length > 0) {
                foreach (var paramItem in param) 
                { 
                    cmd.Parameters.AddWithValue(paramItem.Name, paramItem.Value);
                }
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

           string json= JsonConvert.SerializeObject(dt);
           List<T> result = JsonConvert.DeserializeObject<List<T>>(json)!;
           return result;

        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameters[] param)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (param is not null && param.Length > 0)
            {
                foreach (var paramItem in param)
                {
                    cmd.Parameters.AddWithValue(paramItem.Name, paramItem.Value);
                }
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T> result = JsonConvert.DeserializeObject<List<T>>(json)!;
            return result[0];

        }

        public int Execute(string query, params AdoDotNetParameters[] param)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (param is not null && param.Length > 0)
            {
                foreach (var paramItem in param)
                {
                    cmd.Parameters.AddWithValue(paramItem.Name, paramItem.Value);
                }
            }
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;

        }
    }

    public class AdoDotNetParameters
    {
        public AdoDotNetParameters() { }
        public AdoDotNetParameters(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
