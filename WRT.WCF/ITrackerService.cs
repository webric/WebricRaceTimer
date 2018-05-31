using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WRT.Core.BLL; 

namespace WRT.WCF
{
    [ServiceContract]
    public interface ITrackerService
    {
        [OperationContract]
        string RegistreraKoordinater(int kontainerId,
                                     DateTime tidpunkt,
                                     string longitude,
                                     string latitude,
                                     string noggranhet);

        [OperationContract]
        string RegistreraKoordinaterOchStatus(int kontainerId,
                             DateTime tidpunkt,
                             string longitude,
                             string latitude,
                             string noggranhet,
                             string status);

        [OperationContract]
        List<Kontainer> HämtaKontainrar();
    }
}
