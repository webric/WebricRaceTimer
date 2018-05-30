using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Diagnostics;

namespace IPS.Core.Util
{
    public static class Base
    {
        //Anropa Codebehind från Eval
        //aspx: '<%#ShowInfo(Eval("MyValue")) %>'
        //c#: public string ShowInfo(object value)
        //  {If(value == 3)
        //      return "false";
        //  else
        //      return "true";}

        //TODO
        //Änra style css från code behind
        //tabs_Produkter_left.Attributes.Remove("class")
        //tabs_Produkter_left.Attributes.Add("class", "tabs-inactive-left")

        /// <summary>
        /// Returnerar en lista med stackfram
        /// </summary>
        /// <returns></returns>
        public static List<String> GetStackFrames()
        {
            //Dim words() As String = Split(New StackFrame(skipFrames, False).GetMethod().ToString())
            //Return words(1).Split("("c)(0)


            List<String> list = new List<string>();
            int counter = 0;
            string method = new StackFrame(counter, false).GetMethod().ToString();

            while (method != "")
            {
                list.Add(method);
                counter++;
                method = new StackFrame(counter, false).GetMethod().ToString();
            }

            return list;
        }
        /// <summary>
        /// Döljer en panel och visar en annan
        /// Förutsätter att CSS har: "Smooth"
        /// Usage: MEAGUI, ADGUI
        /// </summary>
        public static void HidePanel(Panel panToHide, Panel panToUnSmooth)
        {
            panToHide.Visible = false;
            panToUnSmooth.CssClass = "";
        }
        /// <summary>
        /// Döljer en panel och visar en annan
        /// Förutsätter att CSS har:
        /// Smoth {filter: alpha(opacity=30); -moz-opacity: 0.3; opacity: 0.3;}
        /// OnTop {z-index: 1002; position: absolute; }
        /// Usage: MEAGUI, ADGUI
        /// </summary>
        public static void ShowPanel(Panel panToShow, Panel panToSmooth)
        {
            panToShow.Visible = true;
            panToSmooth.CssClass = "Smooth";
        }
        public static void SetAllControlsVisibilityInPanel(this Panel pan, bool visible)
        {
            foreach (Control ctrl in pan.Controls)
            {
                Panel p = ctrl as Panel;
                if (p != null)
                    p.Visible = visible;
            }
        }
        public static void LoadUserControlIntoContentPlaceHolder(string userControlPath, ContentPlaceHolder contentPlaceHolder)
        {
            UserControl userControlParent = new UserControl();
            UserControl userControl = userControlParent.LoadControl(userControlPath) as UserControl;
            contentPlaceHolder.Controls.Clear();
            contentPlaceHolder.Controls.Add(userControl);
        }
        public static void UnLoadUserControlsFromContentPlaceHolder(string userControlPath, ContentPlaceHolder contentPlaceHolder)
        {
            //UserControl userControlParent = new UserControl();
            //UserControl userControl = userControlParent.LoadControl(userControlPath) as UserControl;
            contentPlaceHolder.Controls.Clear();
            //contentPlaceHolder.Controls.Remove(userControl);
        }
        public static void SetFocus(this TextBox tb)
        {
            tb.Attributes.Add("onfocusin", " select();");
            tb.Focus();
        }
        public static void SetFocus(this Button b)
        {
            b.Attributes.Add("onfocusin", " select();");
            b.Focus();
        }
        public static void SetFocus(this DropDownList ddl)
        {
            ddl.Attributes.Add("onfocusin", " select();");
            ddl.Focus();
        }
        public static void SetFocus(object o)
        {
            try
            {
                var txt = (TextBox)o;
                txt.Attributes.Add("onfocusin", " select();");
                txt.Focus();
            }
            catch (Exception)
            {
                try
                {
                    var btn = (Button)o;
                    btn.Attributes.Add("onfocusin", " select();");
                    btn.Focus();
                }
                catch (Exception)
                {
                    try
                    {
                        var ddl = (DropDownList)o;
                        ddl.Attributes.Add("onfocusin", " select();");
                        ddl.Focus();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        /// <summary>
        /// http://stackoverflow.com/questions/6406/how-to-access-net-element-on-master-page-from-a-content-page
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static object GetControlInMasterPage(string controlName)
        {
            Page site = GetPage();
            return site.Master.FindControl(controlName);
        }
        /// <summary>
        /// Används för att från UserControl hitta elemnt i parent-sidan
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static object GetControlInParent(string controlName)
        {
            Page site = GetPage();
            return site.Parent.FindControl(controlName);
        }

        private static void SendICalender(DateTime start, DateTime end, string location, string subject, string description)
        {
            //PARAMETERS

            //DateTime beginDate = DateTime.Parse("2008-04-04 04:00:00");
            //DateTime endDate = DateTime.Parse("2008-04-04 17:00:00");
            //string myLocation = "Computer Room";
            //string mySubject = "Training";
            //string myDescription = "Event details";
            //INITIALIZATION

            MemoryStream mStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(mStream) { AutoFlush = true };
            //HEADER

            writer.WriteLine("BEGIN:VCALENDAR");
            writer.WriteLine("PRODID:-//Flo Inc.//FloSoft//EN");
            writer.WriteLine("BEGIN:VEVENT");
            //BODY

            writer.WriteLine("DTSTART:" + start.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z"));
            writer.WriteLine("DTEND:" + end.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z"));
            writer.WriteLine("LOCATION:" + location);
            writer.WriteLine("DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" + description);
            writer.WriteLine("SUMMARY:" + subject);
            //FOOTER

            writer.WriteLine("PRIORITY:3");
            writer.WriteLine("END:VEVENT");
            writer.WriteLine("END:VCALENDAR");
            //MAKE IT DOWNLOADABLE

            Page site = GetPage();

            if (site != null)
            {
                site.Response.Clear();
                //clears the current output content from the buffer

                site.Response.AppendHeader("Content-Disposition", "attachment; filename=Add2Calendar.vcs");
                site.Response.AppendHeader("Content-Length", mStream.Length.ToString());
                site.Response.ContentType = "application/download";
                site.Response.BinaryWrite(mStream.ToArray());
                site.Response.End();
            }
        }
        public static void Sleep(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }
        public static void AddMetaTags(string title, string keywords, string description, string robots, string copyright, string author, string pathToIcon)
        {
            Page site = GetPage();

            if (site != null)
            {
                HtmlTitle _title = new HtmlTitle { Text = title };
                site.Header.Controls.Add(_title);

                HtmlMeta _meta = new HtmlMeta { Name = "keywords", Content = keywords };
                site.Header.Controls.Add(_meta);

                _meta = new HtmlMeta { Name = "description", Content = description };
                site.Header.Controls.Add(_meta);

                _meta = new HtmlMeta { Name = "robots", Content = robots };
                site.Header.Controls.Add(_meta);

                _meta = new HtmlMeta { Name = "copyright", Content = copyright };
                site.Header.Controls.Add(_meta);

                _meta = new HtmlMeta { Name = "author", Content = author };
                site.Header.Controls.Add(_meta);

                if (pathToIcon != "")
                {
                    HtmlLink link = new HtmlLink { Href = pathToIcon, ID = "SHORTCUT ICON" };
                    site.Header.Controls.Add(link);
                }
            }
        }
        public static Page GetPage()
        {
            Page site = HttpContext.Current.Handler as Page;
            return site ?? null;
        }
        public static DateTime GetNullDate()
        {
            return DateTime.Parse("2000-01-01 00:00:00");
        }
        public static string GetWebApplicationPath(string virtualPath)
        {
            return GetPage().MapPath(virtualPath);
        }
        public static string GetFormsApplicationPath()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }
        /// <summary>
        /// Returnerar användarens IPAdress
        /// </summary>
        public static string GetUsersIpAddress()
        {
            Page site = Base.GetPage();
            return site != null ? site.Request.UserHostAddress : "";
        }
        /// <summary>
        /// Returnerar alla användarens IPAdress (om användaren ansluter sig genom en proxy)
        /// </summary>
        public static string[] GetUsersIpAddressArray()
        {
            Page site = Base.GetPage();

            if (site != null)
            {
                string ip = site.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                    return ip.Split(',');
                else
                {
                    string[] ipBack = { site.Request.ServerVariables["REMOTE_ADDR"].ToString() };
                    return ipBack;
                }
            }
            else
            {
                string[] ipBack = { "" };
                return ipBack;
            }
        }
        /// <summary>
        /// Svensk cultureinfo.
        /// </summary>
        public static System.Globalization.CultureInfo SwedishCultureInfo = new System.Globalization.CultureInfo("sv-SE", false);
        /// <summary>
        /// Amerikanskt cultureinfoobjekt.
        /// </summary>
        public static System.Globalization.CultureInfo AmericanCultureInfo = new System.Globalization.CultureInfo("en-US", false);
    }
}

