using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WRT.Core.DAL
{
    public class DALBase
    {
        public static DataTable ExecuteQuery(string query)
        {
            var result = new DataTable();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IPSDB"].ConnectionString))
            {
                var adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(result);
            }

            return result;
        }

        public static DataTable ExecuteQuery(string query, ref SqlParameter[] parameters)
        {
            var result = new DataTable();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IPSDB"].ConnectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(result);
            }

            return result;
        }

        public static DataTable ExecuteStoredProcedure(string spName, ref SqlParameter[] parameters)
        {
            var result = new DataTable();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IPSDB"].ConnectionString))
            {
                var command = new SqlCommand(spName, connection) {CommandType = CommandType.StoredProcedure};
                command.Parameters.AddRange(parameters);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(result);
            }

            return result;
        }
    }
}
