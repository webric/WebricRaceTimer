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
        public Race Init(string name)
        {
            return Race.Init(name);
        }

        public Competitor CreateCompetitor(string number, string name)
        {
            return Competitor.Create(number, name);
        }

        public bool StartCompetitor(Guid raceId, Guid competitorId, DateTime time)
        {
            return Competitor.Start(raceId, competitorId, time);
        }

        public bool StartAll(Guid raceId, DateTime time)
        {
            return Race.StartAll(raceId, time);
        }

        public bool FinnishCompetitor(Guid raceId, Guid competitorId, DateTime time)
        {
            return Competitor.Finnish(raceId, competitorId, time);
        }

        public bool FinnishRace(Guid raceId, DateTime time)
        {
            return Race.Finnish(raceId, time);
        }

        public List<Competitor> GetCompetitors(Guid raceId)
        {
            return Competitor.GetCompetitors(raceId);
        }

        public Competitor GetCompetitor(Guid competitorId)
        {
            return Competitor.GetCompetitor(competitorId);
        }
    }
}
