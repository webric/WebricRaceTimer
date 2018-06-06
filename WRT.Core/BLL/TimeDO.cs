using System;

namespace WRT.Core.BLL
{
    public partial class Time
    {
        public Guid Id { get; set; }
        public String RaceSid { get; set; }
        public String CompetitorSid { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
