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
    public partial class _main_left :Admin_BasePage
    {
        public string vMenuName = "";
        public string vMenuCon = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("login");

            string vMenu = Request["menu"];
            if (vMenu == null || vMenu.Length < 1)
            {
                vMenu = "news";
            }
            //System.Environment.NewLine
            switch (vMenu)
            {
                case "news":
                    vMenuName = "内容管理";
                    vMenuCon = "<a href='news/news_content.aspx?act=guide' target='main'>添加内容</a>";
                    vMenuCon += "<a href='news/news_content.aspx' target='main'>内容管理</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='message/message_list.aspx' target='main'>留言管理</a>";
                    break;
                case "column":
                    vMenuName = "栏目&专题";
                    vMenuCon = "<a href='news/news_column.aspx?act=guide' target='main'>添加栏目</a>";
                    vMenuCon += "<a href='news/news_column.aspx' target='main'>栏目管理</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='news/news_special.aspx' target='main'>专题管理</a>";
                    break;
                case "module":
                    vMenuName = "扩展模块";
                    vMenuCon += "<a href='extend/extend_blogroll.aspx' target='main'>友情连接</a>";
                    vMenuCon += "<a href='extend/extend_dict.aspx' target='main'>数据字典</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='extend/extend_adgroup.aspx' target='main'>广告位</a>";
                    vMenuCon += "<a href='extend/extend_adlist.aspx' target='main'>广告列表</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='extend/extend_plugs.aspx' target='main'>插件管理</a>";
                    break;
                case "label":
                    vMenuName = "标签&页面";
                    vMenuCon = "<a href='custom/custom_tags.aspx' target='main'>自定义标签</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='custom/custom_pages.aspx' target='main'>自定义页面</a>";
                    break;
                case "sys":
                    vMenuName = "系统设置";
                    vMenuCon = "<a href='sys/sys_user.aspx' target='main'>系统用户管理</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='sys/sys_upclear.aspx' target='main'>上传文件管理</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='sys/sys_cache.aspx' target='main'>缓存管理</a>";
                    vMenuCon += "<p></p>";
                    vMenuCon += "<a href='sys/sys_template.aspx' target='main'>模板管理</a>";
                    vMenuCon += "<a href='sys/sys_seo.aspx' target='main'>SEO优化</a>";
                    vMenuCon += "<a href='sys/sys_settings.aspx' target='main'>系统设置</a>";
                    break;
            }
        }
    }
}