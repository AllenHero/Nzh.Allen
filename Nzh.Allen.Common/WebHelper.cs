using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Common
{
    public class WebHelper
    {
        private HttpContext httpContext;
        public WebHelper(HttpContext context)
        {
            httpContext = context;
        }
        #region Session操作
        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public void WriteSession(string key, string value)
        {
            httpContext.Session.SetString(key, value);
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public string GetSession(string key)
        {
            bool rs = httpContext.Session.TryGetValue(key, out byte[] val);
            string value = "";
            if (rs)
            {
                value = Encoding.UTF8.GetString(val);
                return value;
            }
            value = "";
            return value;
        }
        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public void RemoveSession(string key)
        {
            if (key.IsEmpty())
                return;
            httpContext.Session.Remove(key);
        }
        /// <summary>
        /// 清空session
        /// </summary>
        /// <param name="httpContext"></param>
        public void ClearSession()
        {
            httpContext.Session.Clear();
        }
        #endregion

        #region Cookie操作
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public void WriteCookie(string strName, string strValue, int minutes = 60)
        {
            httpContext.Response.Cookies.Append(strName, strValue, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public string GetCookie(string strName)
        {
            var res = httpContext.Request.Cookies.TryGetValue(strName, out string value);
            string reslut = "";
            if (res)
                return value;
            return reslut;
        }
        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        public void RemoveCookie(string CookiesName)
        {
            httpContext.Response.Cookies.Delete(CookiesName);
        }
        #endregion
    }
}
