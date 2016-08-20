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
using System.Text.RegularExpressions;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web
{
    public partial class index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(DateTime.Parse(DateTime.Now.ToString("o")).ToString());
            string rPage;
            rPage = Request.QueryString["page"];

            int vPage = 1;
            if (rPage != string.Empty && rPage != null && Command.Command_Validate.IsNumber(rPage))
            {
                vPage = Convert.ToInt32(rPage);
            }

            string vHtml = "";
            bool vWebPageCache = Command.Command_Configuration.GetConfigBool("WebPageCache"); //判断是否启用了页面缓存
            if (vWebPageCache == false)
            {
                vHtml = Fun_GetIndexContent(vPage);
            }
            else
            {
                int vCacheTime = Command.Command_Configuration.GetConfigInt("CacheTime");
                string indexCacheKey = Command.Command_Configuration.GetConfigString("CacheKey") + "_IndexCache_" + vPage;
                object indexCacheInfo = Command.Command_DataCache.GetCache(indexCacheKey);

                //判断缓存是否存在
                if (indexCacheInfo == null)
                {
                    vHtml = Fun_GetIndexContent(vPage);
                    Command.Command_DataCache.SetCache(indexCacheKey, (object)vHtml, DateTime.Now.AddSeconds(vCacheTime), TimeSpan.Zero);
                }
                else
                {
                    vHtml = indexCacheInfo.ToString();
                }
            }
            vHtml = WebUI_Function.Fun_UrlRewriter(vHtml);
            Response.Write(vHtml);
        }


        private string Fun_GetIndexContent(int byPage)
        {
            string vTemplateUrl = (siteConfig.IndexTemplateName);
            WebUI_Template wt = new WebUI_Template();
            wt.Load_Template(vTemplateUrl);
            wt.vPage = byPage;

            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:title}"), siteConfig.WebName, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:seotitle}"), seoConfig.SeoTitle, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:keywords}"), seoConfig.IndexKeywords, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:description}"), seoConfig.IndexDescription, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:sitepath}"), "<a href=\"/\">首页</a>", RegexOptions.IgnoreCase);

            wt.Parser_MyTag();
            wt.Parser_List();
            wt.Parser_Page();
            wt.Parser_IF();
            return wt.vContent;
        }
    }
}