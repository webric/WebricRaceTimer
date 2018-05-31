using System;
using System.Collections.Generic;
using System.Data;

namespace WRT.Core.BLL
{
    partial class Position
    {
        public List<Position> Hämta(int positionId, bool baraKorrektaPositioner)
        {
            if (baraKorrektaPositioner)
                return RensaBortOkorrektaPositioner(DAL.Position.Hämta(positionId));
            else
                return DAL.Position.Hämta(positionId);
        }

        public DataTable HämtaTillLista(int? positionId, DateTime? from, DateTime? tom)
        {
            var dt  =  RensaBortOkorrektaPositioner(DAL.Position.HämtaTillLista(positionId, from, tom));


            //Lägg till vilka poster som ska animeras 
            if (positionId == null)
            {
                //Animera alla positioner
                foreach (DataRow row in dt.Rows)
                    row["Special"] = "google.maps.Animation.BOUNCE";
            }
            else
            {
                //Animera senaste positionen för varje kontainer
                //TODO
            }

            return dt;
        }

        public List<Position> HämtaAlla(bool baraKorrektaPositioner)
        {
            var positioner = DAL.Position.Hämta(null);

            if (baraKorrektaPositioner)
                return RensaBortOkorrektaPositioner(positioner);
            else
                return positioner;
        }

        private static List<Position> RensaBortOkorrektaPositioner(List<Position> positioner)
        {
            var utPositioner = new List<Position>();

            foreach (var position in positioner)
            {
                if (position.Latitude != "" && position.Longitude != "")
                    utPositioner.Add(position);
            }
            return utPositioner;
        }
        
        private static DataTable RensaBortOkorrektaPositioner(DataTable positioner)
        {
            var utPositioner = positioner.Clone();

            foreach (DataRow position in positioner.Rows)
            {
                if (position["Latitude"].ToString() != "" && position["Longitude"].ToString() != "")
                    utPositioner.ImportRow(position);
            }
            return utPositioner;
        }

        public void Sätt(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet,
                         string status)
        {
            DAL.Position.Sätt(kontainerId, tidpunkt, longitude, latitude, noggranhet, status);
        }

        public void Sätt(int kontainerId, DateTime tidpunkt, string longitude, string latitude, string noggranhet)
        {
            DAL.Position.Sätt(kontainerId, tidpunkt, longitude, latitude, noggranhet, "");
        }
    }
}
