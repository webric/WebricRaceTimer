using System;

namespace WRT.Core.BLL
{
    public partial class Position
    {
        public int PositionId { get; set; }
        public int KontainerId { get; set; }
        public DateTime Tidpunkt { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Noggranhet { get; set; }
        public string Status { get; set; }
    }
}
