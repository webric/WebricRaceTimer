using System;
using System.Collections.Generic;

namespace WRT.Core.BLL
{
    partial class Competitor
    {
        public static Competitor Create(string number, string name)
        {
            var competitor = new Competitor
            {
                Id = Guid.NewGuid(),
                Number = number,
                Name = name
            };

            if (DAL.Competitor.Create(competitor))
                return competitor;
            else
                return null;
        }

        public static bool Start(Guid raceId, Guid competitorId, DateTime time)
        {
            if (DAL.Competitor.Start(raceId, competitorId, time))
                return true;
            else
                return false;
        }

        public static bool Finnish(Guid raceId, Guid competitorId, DateTime time)
        {
            if (DAL.Competitor.Stop(raceId, competitorId, time))
                return true;
            else
                return false;
        }

        public static Competitor GetCompetitor(Guid competitorId)
        {
            return DAL.Competitor.GetCompetitor(competitorId);
        }

        public static List<Competitor> GetCompetitors(Guid raceId)
        {
            return DAL.Competitor.GetCompetitors(raceId);
        }
    }
}
