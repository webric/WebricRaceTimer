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
        Competitor CreateCompetitor(string number, string name);

        [OperationContract]
        bool StartCompetitor(Guid raceId, Guid competitorId, DateTime time);

        [OperationContract]
        bool StartAll(Guid raceId, DateTime time);

        [OperationContract]
        bool FinnishCompetitor(Guid raceId, Guid competitorId, DateTime time);

        [OperationContract]
        bool FinnishRace(Guid raceId, DateTime time);

        [OperationContract]
        List<Competitor> GetCompetitors(Guid raceId);
    }
}
