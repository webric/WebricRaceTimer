using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WRT.Core.DAL
{
    public class Competitor : DALBase
    {
        public static bool Save(BLL.Competitor competitor)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [Competitor] (");
            builder.Append(" [Id] ");
            builder.Append(" ,[RaceId] ");
            builder.Append(" ,[Number] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @Id ");
            builder.Append(", @RaceId ");
            builder.Append(", @Number ");
            builder.Append(", @Name ");
            builder.Append(", @StartTime ");
            builder.Append(", @StopTime ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = competitor.Id };
            parameters[1] = new SqlParameter("@RaceId", SqlDbType.UniqueIdentifier) { Value = competitor.RaceId };
            parameters[2] = new SqlParameter("@Number", SqlDbType.VarChar, 50) { Value = competitor.Number };
            parameters[3] = new SqlParameter("@Name", SqlDbType.VarChar, 50) { Value = competitor.Name };
            parameters[4] = new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = competitor.StartTime };
            parameters[5] = new SqlParameter("@StopTime", SqlDbType.DateTime) { Value = competitor.StopTime };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static bool Start(Guid raceId, Guid competitorId, DateTime time)
        {
            var builder = new StringBuilder();
            builder.Append(" UPDATE [Competitor] SET ");
            builder.Append(" [StartTime] = @StartTime ");
            builder.Append(" WHERE ");
            builder.Append(" [Id] = @Id ");

            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = competitorId };
            parameters[1] = new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = time };
   
            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static bool Stop(Guid raceId, Guid competitorId, DateTime time)
        {
            var builder = new StringBuilder();
            builder.Append(" UPDATE [Competitor] SET ");
            builder.Append(" [StopTime] = @StopTime ");
            builder.Append(" WHERE ");
            builder.Append(" [Id] = @Id ");

            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = competitorId };
            parameters[1] = new SqlParameter("@StopTime", SqlDbType.DateTime) { Value = time };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static List<BLL.Competitor> Get(Guid raceId)
        {
            var builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" [Id] ");
            builder.Append(" ,[RaceId] ");
            builder.Append(" ,[Number] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" FROM [Competitor] ");
            builder.Append(" [Id] = @Id ");

            var parameters = new SqlParameter[0];
            parameters[0] = new SqlParameter("@RaceId", SqlDbType.UniqueIdentifier) { Value = raceId };
            
            var result = ExecuteQuery(builder.ToString(), ref parameters);

            var competitors = new List<BLL.Competitor>();

            foreach (DataRow dr in result.Rows)
            {
                competitors.Add(PopulateObject(dr));
            }

            return competitors;
        }
        private static BLL.Competitor PopulateObject(DataRow dr)
        {
            var com = new BLL.Competitor
            {
                Id = (Guid)dr.ItemArray[0],
                RaceId = (Guid)dr.ItemArray[1],
                Number= dr.ItemArray[2].ToString(),
                Name= dr.ItemArray[3].ToString(),
                StartTime= DateTime.Parse(dr.ItemArray[4].ToString()),
                StopTime = DateTime.Parse(dr.ItemArray[5].ToString())
            };

            return com;
        }
    }
}
