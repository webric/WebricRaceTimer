using System;
using System.Collections.Generic;
using System.Data;

namespace WRT.Core.BLL
{
    partial class Race
    {
        public static Race Init(string name)
        {
            var newRaceSid = GenerateRaceSid();
            while (GetRace(newRaceSid).RaceSid != null)
                newRaceSid = GenerateRaceSid();

            var race = new Race
            {
                Id = Guid.NewGuid(),
                Name = name,
                AdminId = GenerateAdminId(),
                RaceSid = newRaceSid,
                StartTime = null,
                StopTime = null,
                Finnished = false
            };

            if (DAL.Race.Init(race))
                return race;
            else
                return null;
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

        public static bool StartRace(string raceSid)
        {
            return DAL.Race.StartRace(raceSid);
        }
        private static string GenerateAdminId()
        {
            Random random = new Random();
            int randomNumber = random.Next(10, 99);
            return randomNumber.ToString();
        }

        private static string GenerateRaceSid()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            return randomNumber.ToString();
        }
    }
}
