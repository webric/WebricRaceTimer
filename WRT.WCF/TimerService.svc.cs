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
        public string Init(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet)
        {
            var pos = new Position();
            pos.Sätt(kontainerId, tidpunkt, longitude, latitude, noggranhet);
            return "";
        }
        public string SaveCompetitor(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet, string status)
        {
            return "";
        }
        public string FinnishCompetitor(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet, string status)
        {
            return "";
        }
        public string Start(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet, string status)
        {
            var pos = new Position();
            pos.Sätt(kontainerId, tidpunkt, longitude, latitude, noggranhet, status);
            return "";
        }
        public string Stopp(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet, string status)
        {
            var pos = new Position();
            pos.Sätt(kontainerId, tidpunkt, longitude, latitude, noggranhet, status);
            return "";
        }

        public List<Competitor> GetCompetitors()
        {
            var kon = new Competitor();
            return kon.Hämta();
        }
    }
}
