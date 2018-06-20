using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRT.Core.BLL
{
    public class DisplayCompetitor
    {
        public DisplayCompetitor(string competitorSid, string name, string endTime)
        {
            CompetitorSid = competitorSid;
            Name = name;
            EndTime = endTime;
        }

        public String CompetitorSid { get; set; }
        public String Name { get; set; }
        public String EndTime { get; set; }
    }
}
