using System;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace WRT.Core.Util
{
    public class LoggHelper
    {
        public static void Information(string Message, State state)
        {
            Information(Message, state, Guid.Empty);
        }
        public static void Information(string Message, State state, Guid Uniq)
        {
            Page site = Base.GetPage();
            HttpContext context = HttpContext.Current;
            string Application = ConfigurationManager.AppSettings["Application.NameSpace"];
            string Machine = Environment.MachineName;
            string IP = (site != null) ? site.Request.UserHostAddress : "Ingen info";
            string Url = (site != null) ? site.Request.Url.ToString() : "Ingen info";

            if (ConfigurationManager.AppSettings["Error.DataBase"] == "true")
                writeToDataBase(Message, state, "", "", "", "Information", Application, Machine, IP, Url, Uniq);
        }
        public static void Warning(string Message, State state, string Function)
        {
            Warning(Message, state, Function, Guid.Empty);
        }
        public static void Warning(string Message, State state, string Function, Guid uniq)
        {
            Page site = Base.GetPage();
            HttpContext context = HttpContext.Current;
            string Application = ConfigurationManager.AppSettings["Application.NameSpace"];
            string Machine = Environment.MachineName;
            string IP = (site != null) ? site.Request.UserHostAddress : "Ingen info";
            string Url = (site != null) ? site.Request.Url.ToString() : "Ingen info";

            if (ConfigurationManager.AppSettings["Error.DataBase"] == "true")
                writeToDataBase(Message, state, "", "", "", "Warning", Application, Machine, IP, Url, uniq);
        }
        public static void Error(string Message, State state, string Function, Exception ex)
        {
            Error(Message, state, Function, ex, Guid.Empty);
        }
        public static void Error(string Message, State state, string Function, Exception ex, Guid uniq)
        {
            Page site = Base.GetPage();
            HttpContext context = HttpContext.Current;
            string Application = ConfigurationManager.AppSettings["Application.NameSpace"];
            string Machine = Environment.MachineName;
            string IP = "Ingen info";
            try { IP = (site != null) ? site.Request.UserHostAddress : "Ingen info"; }
            catch { }
            string Url = "Ingen info";
            try { Url = (site != null) ? site.Request.Url.ToString() : "Ingen info"; }
            catch { }
            string File = "Ingen info";
            try { File = (site != null) ? context.Request.RawUrl : "Ingen info"; }
            catch { }
            string Exception = (ex != null) ? GetErrorMessage(ex) : "";

            if (ConfigurationManager.AppSettings["Error.DataBase"] == "true")
                writeToDataBase(Message, state, "", "", Exception, "Error", Application, Machine, IP, Url, uniq);

            if (ConfigurationManager.AppSettings["Error.Mail"] == "true")
                sendErrorMail(Message, state, File, Function, Exception, "Error", Application, Machine, IP, Url);
        }
        private static void sendErrorMail(string Message, State state, string File, string Function, string Exception, string Level, string Application, string Machine, string IP, string Url)
        {
            string subject = Level + " from " + Application + " at " + DateTime.Now;
            string message = string.Empty;
            message += "Status: " + state + "\n";
            message += "Applikation: " + Application + "\n";
            message += "Tidpunkt: " + DateTime.Now + "\n";
            message += "Maskin: " + Machine + "\n";
            message += "Användarens IP: " + IP + "\n";
            message += "Url: " + Url + "\n";
            message += "File: " + File + "\n";
            message += "Function: " + Function + "\n";
            message += "Internt meddelande: " + Message + "\n\n";
            message += "Exception: " + Exception + "\n";

            const string fromEmail = "info@mailemall.se";
            const string fromName = "Webric Error";
            string toEmail = ConfigurationManager.AppSettings["Mail.FeedBackReceiverAddress"];
            const string toName = "Webric master";

            MailHelper.SendMail(message, toEmail, toName, fromEmail, fromName, subject, false);
        }
        private static void writeToDataBase(string Message, State state, string File, string Function, string Exception, string Level, string Application, string Machine, string IP, string Url, Guid uniq)
        {
            //string query = "INSERT INTO [Log] ( ";
            //query += " [RegDat] ";
            //query += ", [Message] ";
            //query += ", [Application] ";
            //query += ", [File] ";
            //query += ", [Function] ";
            //query += ", [IP] ";
            //query += ", [Level] ";
            //query += ", [Machine] ";
            //query += ", [Url] ";
            //query += ", [Exception] ";
            //query += ", [State] ";
            //query += ", [Uniq] ";
            //query += " ) VALUES ( ";
            //query += " @RegDat ";
            //query += ", @Message ";
            //query += ", @Application ";
            //query += ", @File ";
            //query += ", @Function ";
            //query += ", @IP ";
            //query += ", @Level ";
            //query += ", @Machine ";
            //query += ", @Url ";
            //query += ", @Exception ";
            //query += ", @State ";
            //query += ", @Uniq ";
            //query += " )";

            //SqlParameter[] param = new SqlParameter[12];
            //param[0] = new SqlParameter("@RegDat", SqlDbType.DateTime);
            //param[0].Value = DateTime.Now;
            //param[1] = new SqlParameter("@Message", SqlDbType.VarChar);
            //param[1].Value = Message;
            //param[2] = new SqlParameter("@Application", SqlDbType.VarChar);
            //param[2].Value = Application;
            //param[3] = new SqlParameter("@File", SqlDbType.VarChar);
            //param[3].Value = File;
            //param[4] = new SqlParameter("@Function", SqlDbType.VarChar);
            //param[4].Value = Function;
            //param[5] = new SqlParameter("@IP", SqlDbType.VarChar);
            //param[5].Value = IP;
            //param[6] = new SqlParameter("@Level", SqlDbType.VarChar);
            //param[6].Value = Level;
            //param[7] = new SqlParameter("@Machine", SqlDbType.VarChar);
            //param[7].Value = Machine;
            //param[8] = new SqlParameter("@Url", SqlDbType.VarChar);
            //param[8].Value = Url;
            //param[9] = new SqlParameter("@Exception", SqlDbType.VarChar);
            //param[9].Value = Exception;
            //param[10] = new SqlParameter("@State", SqlDbType.Int);
            //param[10].Value = state;
            //param[11] = new SqlParameter("@Uniq", SqlDbType.UniqueIdentifier);
            //param[11].Value = uniq;

            //IPS.Core.DAL.DALBase.ExecuteQuery(query, ref param, "Data Source=MSDB2.web.surftown.se;Initial Catalog=Madrid_WCore;User Id=Madrid_MeaInL;Password=onug8523");
        }
        private static string GetErrorMessage(Exception exception)
        {
            Exception e = exception;
            String exceptionMessage = String.Empty;

            while (e != null)
            {
                exceptionMessage = e.Message + "\n\n" + "InnerException:\n" + e.InnerException + "\n\n" + "StackTrace:\n" + e.StackTrace + "\n\n";
                e = e.InnerException;
            }

            return exceptionMessage;
        }
        public enum State
        {
            Other,
            TriggerStarts,
            TriggerEnds,
            SenderNumberOfMailToSend,
            SenderNumberOfMailSent,
            LetterCreated,
            LetterUpdated,
            UserLoggedIn,
            UserLoggedOff,
            UserLoggerFailedToLogin,
            UserFailedToAdd,
            UserAdded,
            SQLProblem,
            ConvertError,
            Global,
            Trespassing,
            UserRegisered
        }
        public static void LoggEventLog(string Tjanst, int EventId, int Prioritet, string Meddelande, TraceEventType EventTyp)
        {
            EventLog logg = new EventLog();
            logg.Source = Tjanst;
            logg.WriteEntry(Meddelande, EventLogEntryType.Information);
        }
        //        private static LoggHelper _instance;

        //        NotificationlevelState _notificationlevel;
        //        public static LoggHelper Instance
        //        {
        //            get { return _instance ?? (_instance = new LoggHelper()); }
        //        }
        //        private LoggHelper()
        //        {
        //            _notificationlevel = NotificationlevelState.ERROR;

        //            try
        //            {
        //                String level = "Warning";
        //                level = level.Trim();
        //                _notificationlevel = (NotificationlevelState)Enum.Parse(typeof(NotificationlevelState), level, true);
        //            }
        //            catch (Exception ex) { }


        //            _eventLogg = new EventLog
        //                             {
        //                                 Source = ConfigurationManager.AppSettings["Project.Name"]
        //                             };
        //            // Select werther debug printouts should be present. Always enabled during debugging, but can be 
        //            // enabled during runtime with the appsetting Se.Sjv.UPA.Core.Util.Logger.debug = true;
        //            //string debug = ConfigurationManager.AppSettings["Logger.debug"];
        //            //try
        //            //{
        //            //    Debuging = Boolean.Parse(debug);
        //            //}
        //            //catch (Exception e)
        //            //{
        //            //    Error(String.Format("Se.Sjv.UPA.Core.Util.Logger.debug must be either {0} or {1}, '{2}' was given", Boolean.FalseString, Boolean.TrueString, debug), e);
        //            //}
        //#if DEBUG
        //            Debuging = true;
        //#endif
        //        }
        //        ~LoggHelper()
        //        {
        //            if (_eventLogg != null)
        //                _eventLogg.Close();
        //        }
        //        private bool Debuging { get; set; }
        //        private readonly EventLog _eventLogg;
        //        private EventLog EventLogg
        //        {
        //            get { return _eventLogg; }
        //        }
        //        private const int Maxlen = 32000;
        //        private void Logg(String message, EventLogEntryType type)
        //        {
        //            //if (System.Text.Encoding.Default.GetByteCount(message) < Maxlen)
        //            //{
        //            //    EventLogg.WriteEntry(message, type);
        //            //}
        //            //else
        //            //{
        //            //    int charLength = System.Text.Encoding.Default.GetByteCount("a");
        //            //    string head = message.Substring(0, Maxlen / charLength);
        //            //    string tail = message.Substring(Maxlen / charLength);
        //            //    Logg(tail, type);
        //            //    EventLogg.WriteEntry("Truncated: " + head, type);
        //            //}
        //        }
        //        public void Information(String message)
        //        {
        //            Logg(message, EventLogEntryType.Information);
        //        }
        //        public void Warning(String message)
        //        {
        //            Logg(message, EventLogEntryType.Warning);
        //            SendLoggingNotification("WARNING", message, "");
        //        }
        //        public void Error(String message, Exception exception)
        //        {
        //            Exception e = exception;
        //            String exceptionMessage = String.Empty;

        //            while (e != null)
        //            {
        //                exceptionMessage = e.Message + "\n\n" + "InnerException:\n" + e.InnerException + "\n\n" + "StackTrace:\n" + e.StackTrace + "\n\n";
        //                e = e.InnerException;
        //            }

        //            Logg(message + "." + exceptionMessage, EventLogEntryType.Error);

        //            SendLoggingNotification("ERRROR", message, exceptionMessage);
        //        }
        //        public static void SendLoggingNotification(string notificationLevel, string messageText, string exceptionText)
        //        {
        //            const string fromEmail = "info@mailemall.se";
        //            const string fromName = "Webric Error";
        //            const string toEmail = "richard.segerlund@gmail.com";
        //            const string toName = "Webric master";

        //            //String applicationName = ConfigurationManager["LoggHelper.eventSource"] ;
        //            string applicationName = ConfigurationManager.AppSettings["Application.NameSpace"];
        //            Page site = Base.GetPage();
        //            HttpContext context = HttpContext.Current;
        //            string userIp = (site != null) ? site.Request.UserHostAddress : "Ingen info";
        //            string url = (site != null) ? site.Request.Url.ToString() : "Ingen info";
        //            string subject = notificationLevel + " from " + applicationName + " at " + DateTime.Now;
        //            string message = string.Empty;
        //            message += "Applikation: " + applicationName + "\n";
        //            message += "Tidpunkt: " + DateTime.Now + "\n";
        //            message += "Maskin: " + Environment.MachineName + "\n";
        //            message += "Internt meddelande: " + messageText + "\n\n";
        //            message += "Användarens IP: " + userIp + "\n";
        //            message += "Url: " + url + "\n";
        //            message += "Page location: " + context.Request.RawUrl + "\n";
        //            message += "Exception: " + exceptionText + "\n";

        //            MailHelper.SendMail(message, toEmail, toName, fromEmail, fromName, subject, "", "", "", "", null, false);
        //        }
        //Nedan finns orginalversionerna av logger
        //public static void LoggEventLog(string source, int eventId, string message, TraceEventType eventTyp, EventLogEntryType logEventType)
        //{
        //    EventLog logg = new EventLog { Source = source };

        //    logg.WriteEntry(message, logEventType, eventId);
        //}
    }
}