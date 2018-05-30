using System;

namespace IPS.Core.BLL
{
    public partial class Kontainer
    {
        public int KontainerId { get; set; }
        public string Namn { get; set; }
        public DateTime Tillverkad { get; set; }
        public string Serienummer { get; set; }
    }
}
