using System;

namespace WRT.Core.BLL
{
    public partial class Race
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LookId { get; set; }
        public string AdminId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public bool Finnished { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
