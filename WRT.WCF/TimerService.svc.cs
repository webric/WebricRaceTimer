using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WRT.Core.BLL;

namespace WRT.WCF
{
    public class TimerService : ITimerService
    {
        public string InitRace(string name)
        {
            var race = Race.Init(name);
            return race.RaceSid;
        }

        public Competitor CreateCompetitor(string number, string name)
        {
            return Competitor.Create(number, name);
        }

        public bool StartCompetitor(string raceSid, string competitorSid)
        {
            return Competitor.Start(raceSid, competitorSid);
        }

        public bool StartAll(string raceSid, DateTime time)
        {
            return Race.StartAll(raceSid, time);
        }

        public bool FinnishCompetitor(string raceSid, string competitorSid)
        {
            return Competitor.Finnish(raceSid, competitorSid);
        }

        public bool FinnishRace(string raceSid)
        {
            return Race.Finnish(raceSid);
        }

        public List<Competitor> GetCompetitors(String raceSid)
        {
            return Competitor.GetCompetitors(raceSid);
        }

        public Competitor GetCompetitor(string competitorSid)
        {
            return Competitor.GetCompetitor(competitorSid);
        }

        public Race GetRace(string raceSid)
        {
            return Race.GetRace(raceSid);
        }

        public bool StartRace(string raceSid)
        {
            return Race.StartRace(raceSid);
        }
    }
}
