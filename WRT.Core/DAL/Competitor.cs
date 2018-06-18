using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WRT.Core.DAL
{
    public class Competitor : DALBase
    {
        public static bool Create(BLL.Competitor competitor)
        {
            competitor.Id = Guid.NewGuid();
            competitor.TimeStamp = DateTime.Now;
            Insert(competitor);

            return true;
        }

        public static bool Start(string raceId, string competitorId, DateTime time)
        {
            var com = new BLL.Competitor
            {
                Id = Guid.NewGuid(),
                CompetitorSid = competitorId,
                RaceSid = raceId,
                TimeStamp = DateTime.Now
            };

            Insert(com);

            return true;
        }

        public static bool Stop(string raceSId, string competitorSid, DateTime time)
        {
            var com = new BLL.Competitor
            {
                CompetitorSid = competitorSid,
                RaceSid = raceSId,
                TimeStamp = DateTime.Now
            };

            Insert(com);

            return true;
        }

        private static bool Insert(BLL.Competitor competitor)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [CompetitorLog] (");
            builder.Append(" [Id] ");
            builder.Append(" ,[CompetitorSid] ");
            builder.Append(" ,[RaceSid] ");
            builder.Append(" ,[Number] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[TimeStamp] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @Id ");
            builder.Append(", @CompetitorSid ");
            builder.Append(", @RaceSid ");
            builder.Append(", @Number ");
            builder.Append(", @Name ");
            builder.Append(", @TimeStamp ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = competitor.Id };
            parameters[1] = new SqlParameter("@CompetitorSid", SqlDbType.VarChar, 6) { Value = competitor.CompetitorSid };
            parameters[2] = new SqlParameter("@RaceSid", SqlDbType.VarChar, 6) { Value = competitor.RaceSid };
            parameters[3] = new SqlParameter("@Number", SqlDbType.VarChar, 6) { Value = competitor.Number };
            parameters[4] = new SqlParameter("@Name", SqlDbType.VarChar, 100) { Value = competitor.Name };
            parameters[5] = new SqlParameter("@TimeStamp", SqlDbType.DateTime) { Value = competitor.TimeStamp };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static List<BLL.Competitor> GetCompetitors(string raceSid)
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
            parameters[0] = new SqlParameter("@RaceId", SqlDbType.VarChar, 6) { Value = raceSid };

            var result = ExecuteQuery(builder.ToString(), ref parameters);

            var competitors = new List<BLL.Competitor>();

            foreach (DataRow dr in result.Rows)
            {
                competitors.Add(PopulateObject(dr));
            }

            return competitors;
        }

        public static BLL.Competitor GetCompetitor(string competitorSid)
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
            parameters[0] = new SqlParameter("@Id", SqlDbType.VarChar) { Value = competitorSid };

            var result = ExecuteQuery(builder.ToString(), ref parameters);

            var competitor = new BLL.Competitor();

            competitor = PopulateObject(result.Rows[0]);

            return competitor;
        }

        private static BLL.Competitor PopulateObject(DataRow dr)
        {
            var com = new BLL.Competitor
            {
                Id = (Guid)dr.ItemArray[0],
                RaceSid = dr.ItemArray[1].ToString(),
                CompetitorSid = dr.ItemArray[2].ToString(),
                Number = dr.ItemArray[2].ToString(),
                Name = dr.ItemArray[3].ToString(),
                TimeStamp = DateTime.Parse(dr.ItemArray[4].ToString()),
            };

            return com;
        }
    }
}
