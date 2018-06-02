using System;
using System.Collections.Generic;
using System.Data;

namespace WRT.Core.BLL
{
    partial class Race
    {
        public Race Init(string name)
        {
            var race = new Race
            {
                Id = Guid.NewGuid(),
                Name = name,
                LookId = "123456",
                AdminId = "78",
                StartTime = DateTime.MinValue,
                StopTime = DateTime.MinValue,
                Finnished = false
            };

            if (DAL.Race.Init(race))
                return race;
            else
                return null;
        }
        public bool StartAll(Guid raceId, DateTime time)
        {
            if (DAL.Race.StartAll(raceId, time))
                return true;
            else
                return false;
        }
        public bool Finnish(Guid raceId, DateTime time)
        {
            if (DAL.Race.Finnish(raceId, time))
                return true;
            else
                return false;
        }
    }
}
