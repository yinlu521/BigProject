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

namespace iNethinkCMS.Command
{
    public class Command_Function
    {
        #region 获得用户IP
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp()
        {
            string ip;
            string[] temp;
            bool isErr = false;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
            if (ip.Length > 15)
                isErr = true;
            else
            {
                temp = ip.Split('.');
                if (temp.Length == 4)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].Length > 3) isErr = true;
                    }
                }
                else
                    isErr = true;
            }

            if (isErr)
                return "0.0.0.0";
            else
                return ip;
        }
        #endregion


        /// <summary>
        /// 判断是否外部提交
        /// </summary>
        public static bool Fun_CheckPost()
        {
            string server_v1 = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
            string server_v2 = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
            int changdu = server_v2.Length;
            if (server_v1.Substring(7, changdu) != server_v2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
