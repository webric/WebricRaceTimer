﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WRT.Client.TimerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TimerService.ITimerService")]
    public interface ITimerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/InitRace", ReplyAction="http://tempuri.org/ITimerService/InitRaceResponse")]
        string InitRace(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/CreateCompetitor", ReplyAction="http://tempuri.org/ITimerService/CreateCompetitorResponse")]
        WRT.Core.BLL.Competitor CreateCompetitor(string number, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/StartAll", ReplyAction="http://tempuri.org/ITimerService/StartAllResponse")]
        bool StartAll(string raceSid, System.DateTime time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/FinnishCompetitor", ReplyAction="http://tempuri.org/ITimerService/FinnishCompetitorResponse")]
        bool FinnishCompetitor(string raceSid, string competitorSid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/FinnishRace", ReplyAction="http://tempuri.org/ITimerService/FinnishRaceResponse")]
        bool FinnishRace(string raceSid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/GetCompetitors", ReplyAction="http://tempuri.org/ITimerService/GetCompetitorsResponse")]
        WRT.Core.BLL.Competitor[] GetCompetitors(string raceSid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/GetRace", ReplyAction="http://tempuri.org/ITimerService/GetRaceResponse")]
        WRT.Core.BLL.Race GetRace(string raceSid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITimerService/StartRace", ReplyAction="http://tempuri.org/ITimerService/StartRaceResponse")]
        bool StartRace(string raceSid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITimerServiceChannel : WRT.Client.TimerService.ITimerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TimerServiceClient : System.ServiceModel.ClientBase<WRT.Client.TimerService.ITimerService>, WRT.Client.TimerService.ITimerService {
        
        public TimerServiceClient() {
        }
        
        public TimerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TimerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string InitRace(string name) {
            return base.Channel.InitRace(name);
        }
        
        public WRT.Core.BLL.Competitor CreateCompetitor(string number, string name) {
            return base.Channel.CreateCompetitor(number, name);
        }
        
        public bool StartAll(string raceSid, System.DateTime time) {
            return base.Channel.StartAll(raceSid, time);
        }
        
        public bool FinnishCompetitor(string raceSid, string competitorSid) {
            return base.Channel.FinnishCompetitor(raceSid, competitorSid);
        }
        
        public bool FinnishRace(string raceSid) {
            return base.Channel.FinnishRace(raceSid);
        }
        
        public WRT.Core.BLL.Competitor[] GetCompetitors(string raceSid) {
            return base.Channel.GetCompetitors(raceSid);
        }
        
        public WRT.Core.BLL.Race GetRace(string raceSid) {
            return base.Channel.GetRace(raceSid);
        }
        
        public bool StartRace(string raceSid) {
            return base.Channel.StartRace(raceSid);
        }
    }
}
