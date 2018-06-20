using System;
using System.Collections.Generic;

namespace WRT.Core.BLL
{
    partial class Competitor
    {
        public static Competitor Create(string competitorSid, string name, string raceSid)
        {
            var competitor = new Competitor
            {
                Name = name,
                CompetitorSid = competitorSid,
                RaceSid = raceSid
            };

            if (DAL.Competitor.Create(competitor))
                return competitor;
            else
                return null;
        }

        private static string GenerateCompetitorSid()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            return randomNumber.ToString();
        }

        public static bool Start(string raceSid, string competitorSid)
        {
            if (DAL.Competitor.Start(raceSid, competitorSid))
                return true;
            else
                return false;
        }

        public static bool Finnish(string raceSid, string competitorSid)
        {
            //Finnish the competitor
            DAL.Competitor.Stop(raceSid, competitorSid);

            //Set correct starttime (because we dont know it correct starttime was set before)
            var race = Race.GetRace(raceSid);
            DAL.Competitor.Start(raceSid, competitorSid, race.StartTime);
            return true;

        }

        public static Competitor GetCompetitor(string competitorSid)
        {
            return DAL.Competitor.GetCompetitor(competitorSid);
        }

        public static List<Competitor> GetCompetitors(string raceSid)
        {
            return DAL.Competitor.GetCompetitors(raceSid);
        }

    }
}
