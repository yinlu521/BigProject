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
    public partial class _main_top : Admin_BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("login");

            string vTopInfo = "<span>"
                            + "<a href='/' target='_blank' >网站首页</a>"
                            + "&nbsp;&nbsp;|&nbsp;&nbsp;"
                            + "<a href=\"_main_desk.aspx\" target=\"main\">返回桌面</a>"
                            + "&nbsp;&nbsp;|&nbsp;&nbsp;"
                            + "<a href=\"sys/sys_cache.aspx?act=clearcache\" target=\"main\">清空缓存</a>"
                            + "&nbsp;&nbsp;|&nbsp;&nbsp;"
                            + "<a href=\"sys/sys_settings.aspx?act=copyright\" target=\"main\">版权声明</a>"
                            + "</span>";
            vTopInfo += "当前登录：" + SysLoginUserName + "(" + SysLoginUserTrueName + ")&nbsp;&nbsp;"
                      + "<a href=\"index.aspx?act=loginout\" target=\"_parent\">[退出登录]</a>";
   


            this.topinfo.InnerHtml = vTopInfo;


            string vTopMenuInfo = "";
            if (CheckUserPower("a", "bool") == true)
            {
                vTopMenuInfo += "<a href=\"#\" onclick=\"JumpFrame('_main_left.aspx?menu=news','news/news_content.aspx');\">内容管理</a>";
            }
            if (CheckUserPower("b", "bool") == true)
            {
                vTopMenuInfo += "<a href=\"#\" onclick=\"JumpFrame('_main_left.aspx?menu=column','news/news_column.aspx');\">栏目&专题</a>";
            }
            if (CheckUserPower("c", "bool") == true)
            {
                vTopMenuInfo += "<a href=\"#\" onclick=\"JumpFrame('_main_left.aspx?menu=module','extend/extend_blogroll.aspx');\">扩展模块</a>";
            }
            if (CheckUserPower("d", "bool") == true)
            {
                vTopMenuInfo += "<a href=\"#\" onclick=\"JumpFrame('_main_left.aspx?menu=label','custom/custom_tags.aspx');\">标签&页面</a>";
            }
            if (CheckUserPower("e", "bool") == true)
            {
                vTopMenuInfo += "<a href=\"#\" onclick=\"JumpFrame('_main_left.aspx?menu=sys','sys/sys_user.aspx');\">系统管理</a>";
            }

            this.menuinfo.InnerHtml = vTopMenuInfo;
        }
    }
}