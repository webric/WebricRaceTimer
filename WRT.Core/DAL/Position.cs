using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WRT.Core.DAL
{
    public class Position : DALBase
    {
        public static List<BLL.Position> Hämta(int? containerId)
        {
            DataTable dt;

            var builder = new StringBuilder();
            builder.Append(" SELECT [PositionId] ");
            builder.Append(" ,[ContainerId] ");
            builder.Append(" ,[TimeStamp] ");
            builder.Append(" ,[Longitude] ");
            builder.Append(" ,[Latitude] ");
            builder.Append(" ,[Accuracy] ");
            builder.Append(" ,[Status] ");
            builder.Append(" FROM [Position] ");
            if (containerId != null)
            {
                builder.Append(" WHERE ContainerId = @ContainerId ");
                builder.Append(" ORDER BY TimeStamp DESC ");

                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ContainerId", SqlDbType.Int) { Value = containerId };

                dt = ExecuteQuery(builder.ToString(), ref parameters);
            }
            else
            {
                builder.Append(" ORDER BY TimeStamp DESC ");

                dt = ExecuteQuery(builder.ToString());
            }

            var positioner = new List<BLL.Position>();

            foreach (DataRow row in dt.Rows)
                positioner.Add(PopulateObject(row));

            return positioner;
        }
        public static DataTable HämtaTillLista(int? containerId, DateTime? from, DateTime? tom)
        {
            var builder = new StringBuilder();
            builder.Append(" SELECT [PositionId] ");
            builder.Append(" ,ROW_NUMBER() OVER (ORDER BY [Timestamp]) AS Radnummer ");
            builder.Append(" ,p.[ContainerId] AS KontainerId ");
            builder.Append(" ,[TimeStamp] AS Tidpunkt ");
            builder.Append(" ,[Longitude] ");
            builder.Append(" ,[Latitude] ");
            builder.Append(" ,[Status] ");
            builder.Append(" ,[Name] AS Namn ");
            builder.Append(" ,[Serial] AS Serienummer ");
            builder.Append(" ,'' AS [Special] ");
            builder.Append(" FROM [Position] p ");
            builder.Append(" JOIN [Container] c ");
            builder.Append(" ON c.ContainerId = p.ContainerId ");

            //Ifall fromdatum eftersöks
            if (from != null)
            {
                builder.Append(builder.ToString().Contains("WHERE")
                                   ? " AND [TimeStamp] > @From "
                                   : " WHERE [TimeStamp] > @From ");
            }
            else
            {
                from = DateTime.Now;
            }

            //Ifall tomdatum eftersöks
            if (tom != null)
            {
                builder.Append(builder.ToString().Contains("WHERE")
                                     ? " AND [TimeStamp] < @Tom "
                                     : " WHERE [TimeStamp] < @Tom ");
            }
            else
            {
                tom = DateTime.Now;
            }

            //Ifall en viss kontainer eftersöks
            if (containerId != null)
            {
                builder.Append(builder.ToString().Contains("WHERE")
                     ? " AND p.ContainerId = @ContainerId "
                     : " WHERE p.ContainerId = @ContainerId ");
            }
            else
            {
                builder.Append(builder.ToString().Contains("WHERE")
                     ? " AND p.PositionId IN (SELECT MAX([PositionId]) FROM [Position] GROUP BY ContainerId) "
                     : " WHERE p.PositionId IN (SELECT MAX([PositionId]) FROM [Position] GROUP BY ContainerId) ");
                containerId = 0;
            }
            builder.Append(" ORDER BY [TimeStamp] DESC ");

            var parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@ContainerId", SqlDbType.Int) { Value = containerId };
            parameters[1] = new SqlParameter("@From", SqlDbType.DateTime) { Value = from };
            parameters[2] = new SqlParameter("@Tom", SqlDbType.DateTime) { Value = tom };

            var dt = ExecuteQuery(builder.ToString(), ref parameters);

            return dt;
        }
        public static void Sätt(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet, string status)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [Position] (");
            builder.Append(" [ContainerId] ");
            builder.Append(" ,[TimeStamp] ");
            builder.Append(" ,[Longitude] ");
            builder.Append(" ,[Latitude] ");
            builder.Append(" ,[Accuracy] ");
            builder.Append(" ,[Status] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @ContainerId ");
            builder.Append(", @TimeStamp ");
            builder.Append(", @Longitude ");
            builder.Append(", @Latitude ");
            builder.Append(", @Accuracy ");
            builder.Append(", @Status ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@ContainerId", SqlDbType.Int) { Value = kontainerId };
            parameters[1] = new SqlParameter("@TimeStamp", SqlDbType.DateTime) { Value = tidpunkt };
            parameters[2] = new SqlParameter("@Longitude", SqlDbType.VarChar, 50) { Value = longitude };
            parameters[3] = new SqlParameter("@Latitude", SqlDbType.VarChar, 50) { Value = latitude };
            parameters[4] = new SqlParameter("@Accuracy", SqlDbType.VarChar, 50) { Value = noggranhet };
            parameters[5] = new SqlParameter("@Status", SqlDbType.VarChar, 50) { Value = status };

            ExecuteQuery(builder.ToString(), ref parameters);
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
