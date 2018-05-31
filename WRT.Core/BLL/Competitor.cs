using System;
using System.Collections.Generic;

namespace WRT.Core.BLL
{
    partial class Competitor
    {
        public List<Competitor> Hämta(int kontainerId)
        {
            return DAL.Competitor.Hämta(kontainerId);
        }
        public List<Competitor> Hämta()
        {
            return DAL.Competitor.Hämta(null);
        }
        public void Sätt(string namn, DateTime tillverkad, string serienummer)
        {
            DAL.Competitor.Sätt(namn, tillverkad, serienummer);
        }
    }
}
