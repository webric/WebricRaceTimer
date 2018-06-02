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
            var race = new Race();
            return race.Init(name);
        }
        public Competitor SaveCompetitor(string number, string name)
        {
            var competitor = new Competitor();
            return competitor.Save(number, name);
        }
        public bool StartCompetitor(Guid raceId, Guid competitorId, DateTime time)
        {
            var competitor = new Competitor();
            return competitor.Start(raceId, competitorId, time);
        }
        public bool StartAll(Guid raceId, DateTime time)
        {
            var race = new Race();
            return race.StartAll(raceId, time);
        }
        public bool FinnishCompetitor(Guid raceId, Guid competitorId, DateTime time)
        {
            var competitor = new Competitor();
            return competitor.Finnish(raceId, competitorId, time);
        }
        public bool FinnishRace(Guid raceId, DateTime time)
        {
            var race = new Race();
            return race.Finnish(raceId, time);
        }
        public List<Competitor> GetCompetitors(Guid raceId)
        {
            var competitor = new Competitor();
            return competitor.Get(raceId);
        }
    }
}
