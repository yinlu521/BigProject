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
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.sys
{
    public partial class sys_cache : Admin_BasePage
    {
        private string vNavInfo = "当前位置：";
        private string vAct = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("login");

            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            this.navInfoID.InnerText = vNavInfo + "系统缓存管理";

            switch (vAct)
            {
                case "clearcache":
                    Web.UI.WebUI_Function.Fun_CacheDel();
                    Response.Redirect("sys_cache.aspx");
                    break;

                default:
                    Fun_CacheList();
                    break;
            }
        }

        private void Fun_CacheList()
        {
            DataTable dt = new DataTable("Datas");
            dt.Columns.Add("ID", Type.GetType("System.Int32"));
            dt.Columns.Add("CacheName", Type.GetType("System.String"));
            dt.Columns.Add("CacheInfo", Type.GetType("System.String"));

            int i = 0;
            string vCacheKey = siteConfig.CacheKey;
            System.Collections.IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                if (Command.Command_StringPlus.Left(CacheEnum.Key.ToString(), vCacheKey.Length) == vCacheKey)
                {
                    i++;
                    dt.Rows.Add(new object[] { i, CacheEnum.Key, CacheEnum.Value.ToString().Length });
                }
            }
            Repeater.DataSource = dt;
            Repeater.DataBind();

            this.iNoInfo.Visible = dt.Rows.Count == 0 ? true : false;
        }

    }
}