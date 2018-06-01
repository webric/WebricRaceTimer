using System;
using System.Collections.Generic;

namespace WRT.Core.BLL
{
    partial class Competitor
    {
        public Competitor Save(string number, string name)
        {
            var competitor = new Competitor
            {
                Id = Guid.NewGuid(),
                Number = number,
                Name = name
            };

            if (DAL.Competitor.Save(competitor))
                return competitor;
            else
                return null;
        }

        public bool Start(Guid raceId, Guid competitorId, DateTime time)
        {
            if (DAL.Competitor.Start(raceId, competitorId, time))
                return true;
            else
                return false;
        }

        public bool Finnish(Guid raceId, Guid competitorId, DateTime time)
        {
            if (DAL.Competitor.Stop(raceId, competitorId, time))
                return true;
            else
                return false;
        }

        public List<Competitor> Get(Guid raceId)
        {
            return DAL.Competitor.Get(raceId);
        }
    }
}
