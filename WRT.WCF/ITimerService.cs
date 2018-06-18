﻿using System;
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
    public interface ITimerService
    {
        [OperationContract]
        string InitRace(string name);

        [OperationContract]
        Competitor CreateCompetitor(string number, string name);

        [OperationContract]
        bool StartCompetitor(string raceSid, string competitorSid, DateTime time);

        [OperationContract]
        bool StartAll(string raceSid, DateTime time);

        [OperationContract]
        bool FinnishCompetitor(string raceSid, string competitorSid, DateTime time);

        [OperationContract]
        bool FinnishRace(string raceSid, DateTime time);

        [OperationContract]
        List<Competitor> GetCompetitors(string raceSid);

        [OperationContract]
        Race GetRace(string raceSid);

        [OperationContract]
        bool StartRace(string raceSid);
    }
}
