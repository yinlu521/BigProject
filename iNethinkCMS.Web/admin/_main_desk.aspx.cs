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

using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin
{
    public partial class _main_desk : Admin_BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("login");

            this.sInfo_A.InnerHtml = "如果您在使用当中遇到任何问题与建议，可以通过QQ群：252731842与大家共同交流！";
            this.sInfo_B.InnerHtml = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.sInfo_C.InnerHtml = "<a href=\"http://cms.inethink.com\" target=\"_blank\">http://cms.inethink.com</a>";
            this.sInfo_D.InnerHtml = "69991000@qq.com";
            this.sInfo_E.InnerHtml = "69991000";
        }
    }
}