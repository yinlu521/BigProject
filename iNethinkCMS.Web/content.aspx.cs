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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using iNethinkCMS.Web.UI;
using iNethinkCMS.Helper;

namespace iNethinkCMS.Web
{
    public partial class content : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            # region 页面传值分析
            string rID;
            int vID;
            rID = Request.QueryString["ID"];
            if (rID == null || rID.IndexOf(",", 0) > 0 || Command.Command_Validate.IsNumber(rID) == false)
            {
                Response.Write("<H3>内容ID错误!</H3>");
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
                vHtml = Fun_GetContent(vID, vPage);
            }
            else
            {
                int vCacheTime = Command.Command_Configuration.GetConfigInt("CacheTime");
                string contentCacheKey = Command.Command_Configuration.GetConfigString("CacheKey") + "_ContentCache_" + vID;
                object contentCacheInfo = Command.Command_DataCache.GetCache(contentCacheKey);

                //判断缓存是否存在
                if (contentCacheInfo == null)
                {
                    vHtml = Fun_GetContent(vID, vPage);
                    Command.Command_DataCache.SetCache(contentCacheKey, (object)vHtml, DateTime.Now.AddSeconds(vCacheTime), TimeSpan.Zero);
                }
                else
                {
                    vHtml = contentCacheInfo.ToString();
                }
            }

            vHtml = WebUI_Function.Fun_UrlRewriter(vHtml);
            Response.Write(vHtml);
            #endregion
        }

        private string Fun_GetContent(int byID, int byPage)
        {
            //获取当前内容信息
            BLL.BLL_iNethinkCMS_Content bll = new BLL.BLL_iNethinkCMS_Content();
            Model.Model_iNethinkCMS_Content model = new Model.Model_iNethinkCMS_Content();

            //获取当前栏目信息
            BLL.BLL_iNethinkCMS_Channel bll_channel = new BLL.BLL_iNethinkCMS_Channel();
            Model.Model_iNethinkCMS_Channel model_channel = new Model.Model_iNethinkCMS_Channel();

            model = bll.GetModel(byID);
            if (model == null)
            {
                Response.Write("<H3>不存在该内容信息!</H3>");
                Response.End();
            }

            int vCID = Convert.ToInt32(model.Cid); //栏目ID
            int vSID = Convert.ToInt32(model.Sid); //专题ID

            model_channel = bll_channel.GetModel(vCID);
            if (model_channel == null)
            {
                Response.Write("<H3>不存在相应的栏目信息!</H3>");
                Response.End();
            }

            //当该页面启用了跳转时,则跳转!
            if (model.Jumpurl != null && model.Jumpurl.Length > 0)
            {
                //Response.Redirect(model.Jumpurl);
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", model.Jumpurl);
                Response.End();
            }


            string vTemplateUrl = "";   //对应的模板
            vTemplateUrl = model_channel.Templateview;

            DataTable dt = bll.GetList("[ID] = " + byID).Tables[0];
            DataRow dr = dt.Rows[0];

            WebUI_Template wt = new WebUI_Template();
            wt.Load_Template(vTemplateUrl);
            wt.vPage = byPage;
            wt.vCID = vCID;
            wt.vSID = vSID;

            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:title}"), siteConfig.WebName, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:seotitle}"), seoConfig.SeoTitle, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:keywords}"), seoConfig.IndexKeywords, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:description}"), seoConfig.IndexDescription, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:sitepath}"), Web.UI.WebUI_Function.Fun_GetSitePath(vCID) + " > 正文", RegexOptions.IgnoreCase);

            //替换专用标签
            Regex regex = new Regex(@"{Tag:([\s\S]*?)}", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(wt.vContent);
            foreach (Match m in matchCollection)
            {
                string vContentTmp = "";
                string vValueKey = m.Groups[1].Value;
                switch (vValueKey.ToLower())
                {
                    case "prev":    //上一篇
                        vContentTmp = Get_Page_PrevNext(byID, vCID, "prev");
                        break;
                    case "next":    //下一篇
                        vContentTmp = Get_Page_PrevNext(byID, vCID, "next");
                        break;
                }
                wt.vContent = wt.vContent.Replace(m.Value, vContentTmp);
            }

            //wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{field:contents}"), dr["Contents"].ToString(), RegexOptions.IgnoreCase);
            wt.vContent = wt.Parser_Tags(0, @"\{field:(.+?)\}", wt.vContent, dr);   //数据信息替换

            wt.Parser_MyTag();
            wt.Parser_List();
            wt.Parser_Page();
            wt.Parser_IF();
            return wt.vContent;
        }

        //获取新闻信息的上一篇,下一篇
        private string Get_Page_PrevNext(int byID, int byCID, string byFlag)
        {
            string vBaseUrl = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string vBackInfo = "";
            string vWhere = "";
            string vOrder = "";
            switch (byFlag)
            {
                case "prev":    //上一篇
                    vWhere = "[id] > " + byID + "";
                    vOrder = "[id] Asc";
                    break;
                case "next":    //下一篇
                    vWhere = "[id] < " + byID + "";
                    vOrder = "[id] Desc";
                    break;
            }

            BLL.BLL_iNethinkCMS_Content bll = new BLL.BLL_iNethinkCMS_Content();
            DataTable dt = bll.GetList(1, vWhere, vOrder).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vBaseUrl += "?id=" + dt.Rows[i]["ID"].ToString();
                vBackInfo = "<a href=" + vBaseUrl + ">" + dt.Rows[i]["Title"].ToString() + "</a>";
            }

            return vBackInfo;
        }
    }
}