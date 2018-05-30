using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IPS.Core.BLL;

namespace IPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TrackerService : ITrackerService
    {
        public string RegistreraKoordinater(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet)
        {
            var pos = new Position();
            pos.Sätt(kontainerId, tidpunkt, longitude, latitude, noggranhet);
            return "";
        }
        public string RegistreraKoordinaterOchStatus(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet, string status)
        {
            var pos = new Position();
            pos.Sätt(kontainerId, tidpunkt, longitude, latitude, noggranhet, status);
            return "";
        }
        public List<Kontainer> HämtaKontainrar()
        {
            var kon = new Kontainer();
            return kon.Hämta();
        }
    }
}
