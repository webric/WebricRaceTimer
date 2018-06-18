using System;

namespace WRT.Core.BLL
{
    public partial class Race
    {
        public Guid? Id { get; set; }
        public String RaceSid { get; set; }
        public String Name { get; set; }
        public String AdminId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public bool? Finnished { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
