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

namespace iNethinkCMS.Web.UI
{
    public class BasePage : System.Web.UI.Page
    {
        public iNethinkCMS.Model.Model_Config siteConfig = new iNethinkCMS.Model.Model_Config();
        protected iNethinkCMS.Model.Model_Config seoConfig = new iNethinkCMS.Model.Model_Config();

        override protected void OnInit(EventArgs e)
        {
            //Server.ScriptTimeout = 90; //默认脚本过期时间

            siteConfig = new iNethinkCMS.BLL.BLL_Config().GetModel_SysConfig(); //获取站点配置信息
            seoConfig = new iNethinkCMS.BLL.BLL_Config().GetModel_SeoConfig(); //获取SEO配置信息

            base.OnInit(e);
        }
    }
}
