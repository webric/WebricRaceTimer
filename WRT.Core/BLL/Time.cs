using System;
using System.Collections.Generic;
using System.Data;

namespace WRT.Core.BLL
{
    partial class Time
    {
        public static bool Insert(string raceSid, string CompetitorSid)
        {
            var time = new Time
            {
                Id = Guid.NewGuid(),
                RaceSid = raceSid,
                CompetitorSid = CompetitorSid,
                TimeStamp = DateTime.Now,
            };

            if (DAL.Time.Insert(time))
                return true;
            else
                return false;
        }

        public static bool StartAll(string raceSid, DateTime time)
        {
            if (DAL.Race.StartAll(raceSid, time))
                return true;
            else
                return false;
        }

        public static bool Finnish(string raceSid, DateTime time)
        {
            if (DAL.Race.Finnish(raceSid, time))
                return true;
            else
                return false;
        }

        public static Race GetRace(string raceSid)
        {
            return DAL.Race.GetRace(raceSid);
        }

    }
}
