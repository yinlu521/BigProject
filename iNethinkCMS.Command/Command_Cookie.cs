/*******************************************************************************
 * iNethinkCMS - 网站内容管理系统
 * Copyright (C) 2012-2013 inethink.com
 * 
 * @author jackyang <69991000@qq.com>
 * @website http://cms.inethink.com
 * @version 1.3.6.0 (2013-08-14)
 * 
 * This is licensed under the GNU LGPL, version 3.0 or later.
 * For details, see: http://www.gnu.org/licenses/gpl-3.0.html
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace iNethinkCMS.Command
{
    public class Command_Cookie
    {
        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="CookieValue">Cookie值</param>
        /// <param name="CookieTime">Cookie过期时间(分钟),0为关闭页面失效</param>
        public static void SaveCookie(string CookieName, string CookieValue, double CookieTime)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            DateTime now = DateTime.Now;
            myCookie.Value = CookieValue;

            if (CookieTime != 0)
            {
                myCookie.Expires = now.AddHours(CookieTime);
                if (HttpContext.Current.Response.Cookies[CookieName] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove(CookieName);
                }
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            else
            {
                if (HttpContext.Current.Response.Cookies[CookieName] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove(CookieName);
                }
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }


        /// <summary>
        /// 取得CookieValue
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <returns>Cookie的值</returns>
        public static string GetCookie(string CookieName)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            myCookie = HttpContext.Current.Request.Cookies[CookieName];

            if (myCookie != null)
            {
                return myCookie.Value;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 清除CookieValue
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        public static void ClearCookie(string CookieName)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            DateTime now = DateTime.Now;

            myCookie.Expires = now.AddYears(-2);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

    }
}