using System;

namespace WRT.Core.BLL
{
    public partial class Competitor
    {
        public Guid Id { get; set; }
        public String CompetitorSid { get; set; }
        public String RaceSid { get; set; }
        public String Number { get; set; }
        public String Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
