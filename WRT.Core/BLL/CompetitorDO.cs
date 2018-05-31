using System;

namespace WRT.Core.BLL
{
    public partial class Competitor
    {
        public int KontainerId { get; set; }
        public string Namn { get; set; }
        public DateTime Tillverkad { get; set; }
        public string Serienummer { get; set; }
    }
}
