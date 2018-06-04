using System;

namespace WRT.Core.BLL
{
    public partial class Competitor
    {
        public Guid Id { get; set; }
        public Guid RaceId { get; set; }
        public string Number{ get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
