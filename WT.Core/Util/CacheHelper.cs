using System;
using System.Web;
using System.Web.UI;

namespace IPS.Core.Util
{
    public class CacheHelper
    {
        #region "Session"
        public static void SetSession(string name, object obj)
        {
            Page site = Base.GetPage();
            if (site != null)
                site.Session.Add(name, obj);
        }
        public static object GetSession(string name)
        {
            Page site = Base.GetPage();
            return site != null ? site.Session[name] : null;
        }
        public static object GetSession(string name, object defaultObject)
        {
            object o = GetSession(name);
            return (o ?? defaultObject);
        }
        public static void RemoveSession(string name)
        {
            Page site = Base.GetPage();
            if (site != null)
                site.Session.Remove(name);
        }
        #endregion
        #region "Application"
        public static void SetApplication(string name, object obj)
        {
            Page site = Base.GetPage();
            if (site != null)
                site.Cache.Insert(name, obj);
        }
        public static object GetApplication(string name)
        {
            Page site = Base.GetPage();
            return site != null ? site.Cache.Get(name) : null;
        }

        public static object GetApplication(string name, object defaultObject)
        {
            object o = GetApplication(name);
            return (o ?? defaultObject);
        }
        public static void RemoveApplication(string name)
        {
            Page site = Base.GetPage();
            if (site != null)
                site.Cache.Remove(name);
        }
        #endregion
        #region "Cookie"
        public static void SetCookie(string name, string value)
        {
            Page site = Base.GetPage();
            if (site != null)
                site.Response.Cookies.Add(new HttpCookie(name, value));
        }
        public static void SetCookie(string name, string value, DateTime expires)
        {
            Page site = Base.GetPage();
            HttpCookie c = new HttpCookie(name, value) { Expires = expires };
            if (site != null)
                site.Response.Cookies.Add(c);
        }
        public static object GetCookie(string name)
        {
            Page site = Base.GetPage();
            return site != null ? site.Response.Cookies[name].Value : null;
        }

        public static void RemoveCookie(string name)
        {
            Page site = Base.GetPage();
            if (site != null)
                site.Response.Cookies.Remove(name);
        }
        #endregion
    }
}
