﻿using System;
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

        public static bool Start(string raceId, string competitorSid)
        {
            var competitor = GetCompetitor(competitorSid);

            competitor.StartTime = DateTime.Now;
            competitor.TimeStamp = DateTime.Now;

            Insert(competitor);

            return true;
        }

        public static bool Start(string raceId, string competitorSid, DateTime? startTime)
        {
            var competitor = GetCompetitor(competitorSid);

            competitor.StartTime = startTime;
            competitor.TimeStamp = DateTime.Now;

            Insert(competitor);

            return true;
        }

        public static bool Stop(string raceSId, string competitorSid)
        {
            var competitor = GetCompetitor(competitorSid);

            competitor.StopTime = DateTime.Now;
            competitor.TimeStamp = DateTime.Now;

            Insert(competitor);

            return true;
        }

        private static bool Insert(BLL.Competitor competitor)
        {
            var builder = new StringBuilder();
            builder.Append(" INSERT INTO [CompetitorLog] (");
            builder.Append(" [Id] ");
            builder.Append(" ,[CompetitorSid] ");
            builder.Append(" ,[RaceSid] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" ,[TimeStamp] ");
            builder.Append(" ) VALUES ( ");
            builder.Append(" @Id ");
            builder.Append(", @CompetitorSid ");
            builder.Append(", @RaceSid ");
            builder.Append(", @Name ");
            builder.Append(", @StartTime ");
            builder.Append(", @StopTime ");
            builder.Append(", @TimeStamp ");
            builder.Append(" ) ");

            var parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = competitor.Id };
            parameters[1] = new SqlParameter("@CompetitorSid", SqlDbType.VarChar, 6) { Value = competitor.CompetitorSid };
            parameters[2] = new SqlParameter("@RaceSid", SqlDbType.VarChar, 6) { Value = competitor.RaceSid };
            parameters[3] = new SqlParameter("@Name", SqlDbType.VarChar, 100) { Value = competitor.Name };
            parameters[4] = new SqlParameter("@StartTime", SqlDbType.DateTime, 100) { Value = competitor.StartTime ?? (object)DBNull.Value };
            parameters[5] = new SqlParameter("@StopTime", SqlDbType.DateTime, 100) { Value = competitor.StopTime ?? (object)DBNull.Value };
            parameters[6] = new SqlParameter("@TimeStamp", SqlDbType.DateTime) { Value = competitor.TimeStamp ?? (object)DBNull.Value };

            ExecuteQuery(builder.ToString(), ref parameters);

            return true;
        }

        public static List<BLL.Competitor> GetCompetitors(string raceSid)
        {
            var builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" [Id] ");
            builder.Append(" ,[CompetitorSid] ");
            builder.Append(" ,[RaceSid] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" FROM [Competitor] ");
            builder.Append(" WHERE [RaceSid] = @RaceSid ");

            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@RaceSid", SqlDbType.VarChar, 50) { Value = raceSid };

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
            builder.Append(" ,[CompetitorSid] ");
            builder.Append(" ,[RaceSid] ");
            builder.Append(" ,[Name] ");
            builder.Append(" ,[StartTime] ");
            builder.Append(" ,[StopTime] ");
            builder.Append(" FROM [Competitor] ");
            builder.Append(" WHERE [CompetitorSid] = @CompetitorSid ");

            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@CompetitorSid", SqlDbType.VarChar) { Value = competitorSid };

            var result = ExecuteQuery(builder.ToString(), ref parameters);

            var competitor = new BLL.Competitor();

            if (result.Rows.Count > 0)
                competitor = PopulateObject(result.Rows[0]);

            return competitor;
        }

        private static BLL.Competitor PopulateObject(DataRow dr)
        {
            var com = new BLL.Competitor
            {
                Id = (Guid)dr.ItemArray[0],
                CompetitorSid = dr.ItemArray[1].ToString(),
                RaceSid = dr.ItemArray[2].ToString(),
                Name = dr.ItemArray[3].ToString(),
                StartTime = dr.ItemArray[4].ToString() == "" ? (DateTime?)null : DateTime.Parse(dr.ItemArray[4].ToString()),
                StopTime = dr.ItemArray[5].ToString() == "" ? (DateTime?)null : DateTime.Parse(dr.ItemArray[5].ToString())
            };

            return com;
        }
    }
}
