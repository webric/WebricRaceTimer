using System;
using System.Collections.Generic;

namespace IPS.Core.BLL
{
    partial class Kontainer
    {
        public List<Kontainer> Hämta(int kontainerId)
        {
            return DAL.Kontainer.Hämta(kontainerId);
        }
        public List<Kontainer> Hämta()
        {
            return DAL.Kontainer.Hämta(null);
        }
        public void Sätt(string namn, DateTime tillverkad, string serienummer)
        {
            DAL.Kontainer.Sätt(namn, tillverkad, serienummer);
        }
    }
}
