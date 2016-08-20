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

using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin
{
    public partial class _main_bottom : Admin_BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("login");
            string vSysVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            /*this.sInfo_A.InnerHtml = "<a href=\"http://Cms.iNethink.com\" target=\"_blank\">iNethinkCMS</a> V" + vSysVer
                                    + "（" + Command.Command_Configuration.GetVersionsString("VersionsTime") + "）"
                                    + "&nbsp;&nbsp;Powered by <a href=\"http://www.iNethink.com\" target=\"_blank\">iNethink.com</a>";*/
        }
    }
}