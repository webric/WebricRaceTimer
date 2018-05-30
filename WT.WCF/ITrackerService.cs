﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IPS.Core.BLL; 

namespace IPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
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
