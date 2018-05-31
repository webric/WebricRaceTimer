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
        public static bool Save(BLL.Competitor competitor)
        {
            throw new NotImplementedException();

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

        public static bool Start(Guid raceId, Guid competitorId, DateTime time)
        {
            throw new NotImplementedException();
            var pos = new BLL.Competitor
            {
                    KontainerId = (int)dr.ItemArray[0],
                    Namn = dr.ItemArray[1].ToString(),
                    Tillverkad = DateTime.Parse(dr.ItemArray[2].ToString()),
                    Serienummer = dr.ItemArray[3].ToString(),
                };

            return pos;
        }

        public static bool Stop(Guid raceId, Guid competitorId, DateTime time)
        {
            throw new NotImplementedException();
            var pos = new BLL.Competitor
            {
                KontainerId = (int)dr.ItemArray[0],
                Namn = dr.ItemArray[1].ToString(),
                Tillverkad = DateTime.Parse(dr.ItemArray[2].ToString()),
                Serienummer = dr.ItemArray[3].ToString(),
            };

            return pos;
        }

        private static BLL.Position PopulateObject(DataRow dr)
        {
            var pos = new BLL.Position
            {
                PositionId = (int)dr.ItemArray[0],
                KontainerId = (int)dr.ItemArray[1],
                Tidpunkt = DateTime.Parse(dr.ItemArray[2].ToString()),
                Longitude = dr.ItemArray[3].ToString(),
                Latitude = dr.ItemArray[4].ToString(),
                Noggranhet = dr.ItemArray[5].ToString(),
                Status = dr.ItemArray[6].ToString()
            };

            return pos;
        }
    }
}
