using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WRT.Core.DAL
{
    public class Race : DALBase
    {
        public static bool Finnish(string raceSid, DateTime time)
        {
            var builder = new StringBuilder();
            builder.Append(" UPDATE [Race] SET ");
            builder.Append(" [Finnished] = @Finnished ");
            builder.Append(" [StopTime] = @StopTime");
            builder.Append(" WHERE ");
            builder.Append(" [RaceId] = @RaceId ");

            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@RaceId", SqlDbType.VarChar, 6) { Value = raceSid };
            parameters[1] = new SqlParameter("@Finnished", SqlDbType.Bit) { Value = true };
            parameters[2] = new SqlParameter("@StopTime", SqlDbType.DateTime) { Value = time };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static bool StartAll(string raceSid, DateTime time)
        {
            var builder = new StringBuilder();
            builder.Append(" UPDATE [Race] SET ");
            builder.Append(" [StartTime] = @StartTime ");
            builder.Append(" WHERE ");
            builder.Append(" [RaceId] = @RaceId ");

            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@RaceId", SqlDbType.VarChar, 6) { Value = raceSid };
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

        public static bool StartRace(string raceSid)
        {
            var race = GetRace(raceSid);

            race.StartTime = DateTime.Now;
            race.TimeStamp = DateTime.Now;

            Insert(race);

            return true;
        }

        public static BLL.Race GetRace(string raceSid)
        {
            var builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" [Id] ");
            builder.Append(" ,[RaceSid] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[AdminId] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" ,[Finnished] ");
            builder.Append(" FROM [Race] ");
            builder.Append(" WHERE [RaceSid] = @RaceSid ");

            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@RaceSid", SqlDbType.VarChar) { Value = raceSid };

            var result = ExecuteQuery(builder.ToString(), ref parameters);

            var race = new BLL.Race();

            if (result.Rows.Count > 0)
                race = PopulateObject(result.Rows[0]);

            return race;
        }

        public static bool Insert(BLL.Race race)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [RaceLog] (");
            builder.Append(" [Id] ");
            builder.Append(" ,[RaceSid] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[AdminId] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" ,[Finnished] ");
            builder.Append(" ,[TimeStamp] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @Id ");
            builder.Append(", @RaceSid ");
            builder.Append(", @Name ");
            builder.Append(", @AdminId ");
            builder.Append(", @StartTime ");
            builder.Append(", @StopTime ");
            builder.Append(", @Finnished ");
            builder.Append(", @TimeStamp ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = race.Id };
            parameters[1] = new SqlParameter("@RaceSid", SqlDbType.VarChar, 50) { Value = race.RaceSid };
            parameters[2] = new SqlParameter("@Name", SqlDbType.VarChar, 100) { Value = race.Name };
            parameters[3] = new SqlParameter("@AdminId", SqlDbType.VarChar, 10) { Value = race.AdminId };
            parameters[4] = new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = race.StartTime ?? (object)DBNull.Value };
            parameters[5] = new SqlParameter("@StopTime", SqlDbType.DateTime) { Value = race.StopTime ?? (object)DBNull.Value };
            parameters[6] = new SqlParameter("@Finnished", SqlDbType.Bit) { Value = race.Finnished };
            parameters[7] = new SqlParameter("@TimeStamp", SqlDbType.DateTime) { Value = race.TimeStamp ?? (object)DBNull.Value };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        private static BLL.Race PopulateObject(DataRow dr)
        {
            var rac = new BLL.Race
            {
                Id = (Guid)dr.ItemArray[0],
                RaceSid = dr.ItemArray[1].ToString(),
                Name = dr.ItemArray[2].ToString(),
                AdminId = dr.ItemArray[3].ToString(),
                StartTime = dr.ItemArray[4].ToString() == "" ? (DateTime?)null : DateTime.Parse(dr.ItemArray[4].ToString()),
                StopTime = dr.ItemArray[5].ToString() == "" ? (DateTime?)null : DateTime.Parse(dr.ItemArray[5].ToString()),
                Finnished = (bool)dr.ItemArray[6]
            };

            return rac;
        }


    }
}
