using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WRT.Core.DAL
{
    public class Time : DALBase
    {
        public static bool Insert(BLL.Time time)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [Time] (");
            builder.Append(" [Id] ");
            builder.Append(" ,[RaceSid] ");
            builder.Append(" ,[CompetitorSid] ");
            builder.Append(" ,[TimeStamp] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @Id ");
            builder.Append(", @RaceSid ");
            builder.Append(", @CompetitorSid ");
            builder.Append(", @TimeStamp ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = time.Id };
            parameters[1] = new SqlParameter("@RaceSid", SqlDbType.UniqueIdentifier) { Value = time.RaceSid };
            parameters[2] = new SqlParameter("@CompetitorSid", SqlDbType.UniqueIdentifier) { Value = time.CompetitorSid };
            parameters[3] = new SqlParameter("@TimeStamp", SqlDbType.DateTime) { Value = time.TimeStamp };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static bool Finnish(Guid raceId, DateTime time)
        {
            var builder = new StringBuilder();
            builder.Append(" UPDATE [Race] SET ");
            builder.Append(" [Finnished] = @Finnished ");
            builder.Append(" [StopTime] = @StopTime");
            builder.Append(" WHERE ");
            builder.Append(" [RaceId] = @RaceId ");

            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@RaceId", SqlDbType.UniqueIdentifier) { Value = raceId };
            parameters[1] = new SqlParameter("@Finnished", SqlDbType.Bit) { Value = true };
            parameters[2] = new SqlParameter("@StopTime", SqlDbType.DateTime) { Value = time };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static bool StartAll(Guid raceId, DateTime time)
        {
            var builder = new StringBuilder();
            builder.Append(" UPDATE [Race] SET ");
            builder.Append(" [StartTime] = @StartTime ");
            builder.Append(" WHERE ");
            builder.Append(" [RaceId] = @RaceId ");

            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@RaceId", SqlDbType.UniqueIdentifier) { Value = raceId };
            parameters[1] = new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = time };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static bool Init(BLL.Race race)
        {
            race.TimeStamp = DateTime.Now;

            Insert(race);

            return true;
        }

        public static BLL.Race GetRace(Guid raceId)
        {
            throw new NotImplementedException();
        }

        public static bool Insert(BLL.Race race)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [Race] (");
            builder.Append(" [Id] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[LookId] ");
            builder.Append(" ,[AdminId] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" ,[Finnished] ");
            builder.Append(" ,[TimeStamp] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @Id ");
            builder.Append(", @Name ");
            builder.Append(", @LookId ");
            builder.Append(", @AdminId ");
            builder.Append(", @StartTime ");
            builder.Append(", @StopTime ");
            builder.Append(", @Finnished ");
            builder.Append(", @TimeStamp ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = race.Id };
            parameters[1] = new SqlParameter("@Name", SqlDbType.VarChar, 50) { Value = race.Name };
            parameters[2] = new SqlParameter("@AdminId", SqlDbType.VarChar, 3) { Value = race.AdminId };
            parameters[3] = new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = race.StartTime };
            parameters[4] = new SqlParameter("@StopTime", SqlDbType.DateTime) { Value = race.StopTime };
            parameters[5] = new SqlParameter("@Finnished", SqlDbType.Bit) { Value = race.Finnished };
            parameters[6] = new SqlParameter("@TimeStamp", SqlDbType.DateTime) { Value = race.TimeStamp };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        private static BLL.Race PopulateObject(DataRow dr)
        {
            var rac = new BLL.Race
            {
                Id = (Guid)dr.ItemArray[0],
                Name = dr.ItemArray[1].ToString(),
                AdminId = dr.ItemArray[2].ToString(),
                StartTime = DateTime.Parse(dr.ItemArray[3].ToString()),
                StopTime = DateTime.Parse(dr.ItemArray[4].ToString()),
                Finnished = (bool)dr.ItemArray[5]
            };

            return rac;
        }
    }
}
