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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin
{
    public partial class index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = siteConfig.WebName + "_管理中心_iNethinkCMS";

            string vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            if (vAct == "loginout")
            {
                Command_Session.Del("admin_username");
                Command_Session.Del("admin_usertruename");
                Command_Session.Del("admin_userpass");
                Command_Session.Del("admin_userpower");
                Command_Session.Del("admin_userchannelpower");

                Command_Cookie.ClearCookie("cookie_admin_username");
                Command_Cookie.ClearCookie("cookie_admin_password");

                Response.Redirect(this.Request.Path);
            }

        }

        protected void Button_Login_Click(object sender, EventArgs e)
        {

            if (this.txtUserName.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入用户名!");
                return;
            }
            if (this.txtUserPass.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入密码!");
                return;
            }
            if (this.txtVerificationCode.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入验证码!");
                return;
            }

            string UserName = this.txtUserName.Text.Trim();
            string UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtUserPass.Text.Trim(), "MD5").ToLower();
            string vVerificationCode = this.txtVerificationCode.Text.Trim().ToLower();

            //判断验证码是否输入正确
            if (vVerificationCode != Command.Command_Session.Get("verificationcode"))
            {
                this.txtVerificationCode.Text = "";
                MessageBox.Show(this, "验证码输入错误!");
                return;
            }

            iNethinkCMS.BLL.BLL_iNethinkCMS_User bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_User();
            if (bll.Exists(1, UserName, UserPass) == true)
            {
                //写入Seesion
                iNethinkCMS.Model.Model_iNethinkCMS_User model = new iNethinkCMS.Model.Model_iNethinkCMS_User();
                model = bll.GetModel(UserName);
                Command_Session.Add("admin_username", model.UserName);
                Command_Session.Add("admin_usertruename", model.UserTrueName);
                Command_Session.Add("admin_userpass", model.UserPass);
                Command_Session.Add("admin_userpower", model.UserPower);
                Command_Session.Add("admin_userchannelpower", model.UserChannelPower);

                //写入COOKIE
                string vRndStr = Command_MD5.md5(Command_StringPlus.RandomCode("all", 8) + model.UserName);
                string vSecurityCode = Command_MD5.md5(siteConfig.CacheKey + Command_Function.GetUserIp() + vRndStr);

                Command_Cookie.SaveCookie("cookie_admin_username", model.UserName, 0);
                Command_Cookie.SaveCookie("cookie_admin_password", vSecurityCode, 0);

                iNethinkCMS.Helper.SQLHelper.ExecuteSql("Update [iNethinkCMS_User] Set SecurityCode='" + vRndStr + "' Where UserName='" + model.UserName + "'");

                Response.Redirect("main.aspx");
            }
            else
            {
                MessageBox.Show(this, "用户名或密码输入错误!\\n请检查后重新进行登陆!");
                return;
            }
        }

    }
}