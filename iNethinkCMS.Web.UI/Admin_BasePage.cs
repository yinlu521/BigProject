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
using System.Data;
using System.Data.SqlClient;
using iNethinkCMS.Command;

namespace iNethinkCMS.Web.UI
{
    public class Admin_BasePage : BasePage
    {
        protected string SysLoginUserName;
        protected string SysLoginUserTrueName;
        protected string SysLoginUserPower;
        protected string SysLoginUserChannelPower;

        /// <summary>
        /// 权限检查
        /// </summary>
        /// <returns></returns>
        public void CheckUserPower(string byUserPower)
        {
            //判断COOKIE信息,在正确情况下,重新写入SESSION
            if (String.IsNullOrEmpty(Command_Session.Get("admin_username")))
            {
                string JC_UserName = Command_Cookie.GetCookie("cookie_admin_username");
                string JC_PassWord = Command_Cookie.GetCookie("cookie_admin_password");

                if (!String.IsNullOrEmpty(JC_UserName) && !String.IsNullOrEmpty(JC_PassWord))
                {
                    JC_UserName = JC_UserName.Replace("'", "").Replace(")", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("%", "");
                    JC_PassWord = JC_PassWord.Replace("'", "").Replace(")", "").Replace(">", "").Replace("*", "").Replace("?", "").Replace("%", "");

                    iNethinkCMS.BLL.BLL_iNethinkCMS_User bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_User();
                    iNethinkCMS.Model.Model_iNethinkCMS_User model = new iNethinkCMS.Model.Model_iNethinkCMS_User();
                    model = bll.GetModel(JC_UserName);
                    if (model != null)
                    {
                        if (Command_MD5.md5(siteConfig.CacheKey + Command_Function.GetUserIp() + model.SecurityCode) == JC_PassWord)
                        {
                            Command_Session.Add("admin_username", model.UserName);
                            Command_Session.Add("admin_usertruename", model.UserTrueName);
                            Command_Session.Add("admin_userpass", model.UserPass);
                            Command_Session.Add("admin_userpower", model.UserPower);
                            Command_Session.Add("admin_userchannelpower", model.UserChannelPower);
                        }
                    }
                }
            }

            SysLoginUserName = Command_Session.Get("admin_username");
            SysLoginUserTrueName = Command_Session.Get("admin_usertruename");
            SysLoginUserPower = "login," + Command_Session.Get("admin_userpower") + ",";
            SysLoginUserChannelPower = Command_Session.Get("admin_userchannelpower");

            if (String.IsNullOrEmpty(SysLoginUserName) || String.IsNullOrEmpty(SysLoginUserPower))
            {

                Response.Clear();
                //Response.Redirect("~/admin/index.aspx");
                Response.Write("<script language=javascript>parent.location.href=\"/admin/index.aspx\";</script>");
                Response.End();
            }
            else
            {
                if (SysLoginUserPower.IndexOf(byUserPower + ",") < 0)
                {
                    Response.Write("您并无当前页面/功能的操作权限!");
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 权限检查
        /// </summary>
        /// <returns></returns>
        public bool CheckUserPower(string byUserPower, string retun)
        {
            SysLoginUserName = Command_Session.Get("admin_username");
            SysLoginUserPower = "login," + Command_Session.Get("admin_userpower") + ",";
            if (SysLoginUserName == null || SysLoginUserPower == null)
            {
                return false;
            }
            else
            {
                if (SysLoginUserPower.IndexOf(byUserPower + ",") < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }


        }

    }
}
