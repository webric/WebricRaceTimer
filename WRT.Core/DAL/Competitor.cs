using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WRT.Core.DAL
{
    public class Competitor : DALBase
    {
        public static List<BLL.Competitor> Hämta(int? kontainerId)
        {
            DataTable dt;

            var builder = new StringBuilder();
            builder.Append(" SELECT [ContainerId]");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[Created] ");
            builder.Append(" ,[Serial] ");
            builder.Append(" FROM [Container] ");
            if (kontainerId != null)
            {
                builder.Append(" WHERE ContainerId = @ContainerId ");
                builder.Append(" ORDER BY [Name] ASC ");

                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ContainerId", SqlDbType.Int) { Value = kontainerId };

                dt = ExecuteQuery(builder.ToString(), ref parameters);
            }
            else
            {
                builder.Append(" ORDER BY [Name] ASC ");

                dt = ExecuteQuery(builder.ToString());
            }

            var kontainrar = new List<BLL.Kontainer>();

            foreach (DataRow row in dt.Rows)
                kontainrar.Add(PopulateObject(row));

            return kontainrar;
        }
        public static void Sätt(string namn, DateTime tillverkad, string serienummer)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [Container] (");
            builder.Append(" [Name] ");
            builder.Append(" ,[Created] ");
            builder.Append(" ,[Serial] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @Name ");
            builder.Append(", @Created ");
            builder.Append(", @Serial ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Name", SqlDbType.VarChar, 50) { Value = namn };
            parameters[1] = new SqlParameter("@Created", SqlDbType.DateTime) { Value = tillverkad };
            parameters[2] = new SqlParameter("@Serial", SqlDbType.VarChar, 50) { Value = serienummer };

            ExecuteQuery(builder.ToString(), ref parameters);
        }
        private static BLL.Competitor PopulateObject(DataRow dr)
        {
            var pos = new BLL.Competitor
            {
                    KontainerId = (int)dr.ItemArray[0],
                    Namn = dr.ItemArray[1].ToString(),
                    Tillverkad = DateTime.Parse(dr.ItemArray[2].ToString()),
                    Serienummer = dr.ItemArray[3].ToString(),
                };

            return pos;
        }
    }
}
