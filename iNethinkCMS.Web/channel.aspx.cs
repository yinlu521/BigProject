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
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web
{
    public partial class channel : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            # region 页面传值分析
            string rID;
            int vID;
            rID = Request.QueryString["ID"];
            if (rID == null || rID.IndexOf(",", 0) > 0 || Command.Command_Validate.IsNumber(rID) == false)
            {
                Response.Write("<H3>栏目ID错误!</H3>");
                Response.End();
            }
            vID = Convert.ToInt32(rID);

            string rPage;
            rPage = Request.QueryString["page"];

            int vPage = 1;
            if (rPage != string.Empty && rPage != null && Command.Command_Validate.IsNumber(rPage))
            {
                vPage = Convert.ToInt32(rPage);
            }
            #endregion

            #region 获取页面信息
            string vHtml = "";
            bool vWebPageCache = Command.Command_Configuration.GetConfigBool("WebPageCache"); //判断是否启用了页面缓存
            if (vWebPageCache == false)
            {
                vHtml = Fun_GetChannelContent(vID, vPage);
            }
            else
            {
                int vCacheTime = Command.Command_Configuration.GetConfigInt("CacheTime");
                string channelCacheKey = Command.Command_Configuration.GetConfigString("CacheKey") + "_ChannelCache_" + vID + "_" + vPage;
                object channelCacheInfo = Command.Command_DataCache.GetCache(channelCacheKey);

                //判断缓存是否存在
                if (channelCacheInfo == null)
                {
                    vHtml = Fun_GetChannelContent(vID, vPage);
                    Command.Command_DataCache.SetCache(channelCacheKey, (object)vHtml, DateTime.Now.AddSeconds(vCacheTime), TimeSpan.Zero);
                }
                else
                {
                    vHtml = channelCacheInfo.ToString();
                }
            }

            vHtml = WebUI_Function.Fun_UrlRewriter(vHtml);
            Response.Write(vHtml);
            #endregion
        }

        private string Fun_GetChannelContent(int byID, int byPage)
        {
            //获取当前栏目信息
            BLL.BLL_iNethinkCMS_Channel bll = new BLL.BLL_iNethinkCMS_Channel();
            Model.Model_iNethinkCMS_Channel model = new Model.Model_iNethinkCMS_Channel();

            model = bll.GetModel(byID);
            if (model == null)
            {
                Response.Write("<H3>不存在该栏目!</H3>");
                Response.End();
            }

            //当该页面启用了跳转时,则跳转!
            if (model.OutSideLink == 1)
            {
                //Response.Redirect(model.Domain);
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", model.Domain);
                Response.End();
            }


            string vTemplateUrl = "";   //对应的模板
            vTemplateUrl = model.Templatechannel;

            DataTable dt = bll.GetList("[CID] = " + byID).Tables[0];
            DataRow dr = dt.Rows[0];

            WebUI_Template wt = new WebUI_Template();
            wt.Load_Template(vTemplateUrl);
            wt.vPage = byPage;
            wt.vCID = byID;

            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:title}"), siteConfig.WebName, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:seotitle}"), seoConfig.SeoTitle, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:keywords}"), seoConfig.IndexKeywords, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:description}"), seoConfig.IndexDescription, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:sitepath}"), Web.UI.WebUI_Function.Fun_GetSitePath(byID), RegexOptions.IgnoreCase);

            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{field:channelkeywords}"), dr["Keywords"].ToString(), RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{field:channeldescription}"), dr["Description"].ToString(), RegexOptions.IgnoreCase);

            wt.vContent = wt.Parser_Tags(0, @"\{field:(.+?)\}", wt.vContent, dr);   //数据信息替换

            wt.Parser_MyTag();
            wt.Parser_List();
            wt.Parser_Page();
            wt.Parser_IF();
            return wt.vContent;
        }
    }
}