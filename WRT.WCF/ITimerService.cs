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
    public interface ITimerService
    {
        [OperationContract]
        Race Init(string name);

        [OperationContract]
        Competitor SaveCompetitor(string number, string name);

        [OperationContract]
        string StartCompetitor(Guid id, DateTime start);

        [OperationContract]
        string FinnishCompetitor(Guid id, DateTime start);

        [OperationContract]
        string StartAll(Guid raceId);

        [OperationContract]
        bool FinnishCompetitor(Guid raceId, Guid competitorId, DateTime time);
    }
}
