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
using iNethinkCMS.Helper;

namespace iNethinkCMS.Web.plugs.search
{
    public partial class index : BasePage
    {

        iNethinkCMS.BLL.BLL_iNethinkCMS_Content bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Content();
        iNethinkCMS.Model.Model_iNethinkCMS_Content model = new iNethinkCMS.Model.Model_iNethinkCMS_Content();

        private string vXmlPath = @"/plugs/search/setting.xml";
        private string vState;
        private string vTemplatepath;
        private string vKeywordlengthMin;
        private string vKeywordlengthMax;
        private string vSearchMode;

        protected void Page_Load(object sender, EventArgs e)
        {
            vState = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"state\"]", "value").Value.Trim();
            vTemplatepath = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"templatepath\"]", "value").Value.Trim();
            vKeywordlengthMin = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"keywordlengthmin\"]", "value").Value.Trim();
            vKeywordlengthMax = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"keywordlengthmax\"]", "value").Value.Trim();
            vSearchMode = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"searchmode\"]", "value").Value.Trim();

            //判断是否开启
            if (vState == "0")
            {
                Response.Write("<H3>搜索功能尚未开启!</H3>");
                Response.End();
            }

            string vKeyWord;
            vKeyWord = Request.QueryString["skeyword"];
            if (vKeyWord == null) { vKeyWord = ""; }
            vKeyWord = Command.Command_Validate.SqlTextClear(vKeyWord);

            string rPage;
            rPage = Request.QueryString["page"];

            int vPage = 1;
            if (rPage != string.Empty && rPage != null && Command.Command_Validate.IsNumber(rPage))
            {
                vPage = Convert.ToInt32(rPage);
            }

            string vHtml = Fun_GetSearchContent(vPage, vKeyWord);
            Response.Write(vHtml);
        }


        private string Fun_GetSearchContent(int byPage, string byKeyWord)
        {
            string vTemplateUrl = vTemplatepath;
            WebUI_Template wt = new WebUI_Template();
            wt.Load_Template(vTemplateUrl);
            wt.vPage = byPage;

            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:title}"), siteConfig.WebName, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:seotitle}"), seoConfig.SeoTitle, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:keywords}"), seoConfig.IndexKeywords, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:description}"), seoConfig.IndexDescription, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{sys:sitepath}"), "<a href=\"/\">首页</a>&nbsp;>&nbsp;网站搜索", RegexOptions.IgnoreCase);

            //搜索专用-开始
            string vSqlWhere = "[Display]=1";
            int vCountNum = 0;
            string vErrorInfo = "";

            if (byKeyWord.Length < int.Parse(vKeywordlengthMin) || byKeyWord.Length > int.Parse(vKeywordlengthMax))
            {
                vErrorInfo = "关键字长度请控制在" + vKeywordlengthMin + "至" + vKeywordlengthMax + "位之间!";
                wt.vContent = Fun_GetShowContent(wt.vContent, "yesinfo");
                wt.vContent = Fun_GetShowContent(wt.vContent, "noinfo");
            }
            else
            {
                int vSearchModeInt = int.Parse(vSearchMode);
                if (vSearchModeInt >= 1)
                {
                    vSqlWhere += " And [Title] Like '%" + byKeyWord + "%'";
                }
                if (vSearchModeInt >= 2)
                {
                    vSqlWhere += " Or [SubTitle] Like '%" + byKeyWord + "%'";
                }
                if (vSearchModeInt >= 3)
                {
                    vSqlWhere += " Or [Keywords] Like '%" + byKeyWord + "%'";
                }
                if (vSearchModeInt >= 4)
                {
                    vSqlWhere += " Or [Description] Like '%" + byKeyWord + "%'";
                }
                if (vSearchModeInt >= 4)
                {
                    vSqlWhere += " Or [Contents] Like '%" + byKeyWord + "%'";
                }

                DataSet ds = bll.GetList(vSqlWhere);
                vCountNum = ds.Tables[0].Rows.Count;

                //过滤掉相应的内容
                if (vCountNum == 0)
                {
                    wt.vContent = Fun_GetShowContent(wt.vContent, "yesinfo");
                    wt.vContent = Fun_GetShowContent(wt.vContent, "errorinfo");
                }
                else
                {
                    wt.vContent = Fun_GetShowContent(wt.vContent, "noinfo");
                    wt.vContent = Fun_GetShowContent(wt.vContent, "errorinfo");
                }
            }

            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{search:keyword}"), byKeyWord, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{search:countnum}"), vCountNum.ToString(), RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{search:error}"), vErrorInfo, RegexOptions.IgnoreCase);
            wt.vContent = Regex.Replace(wt.vContent, Regex.Escape("{search:sqlwhere}"), vSqlWhere, RegexOptions.IgnoreCase);
            //搜索专用-结束

            wt.Parser_MyTag();
            wt.Parser_List();
            wt.Parser_Page();
            wt.Parser_IF();
            return wt.vContent;
        }

        private string Fun_GetShowContent(string byContent, string byTagsstr)
        {
            Regex regex = new Regex(@"<!--(search:" + byTagsstr + @")-->([\s\S]*?)<!--search:" + byTagsstr + "-->", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(byContent);
            foreach (Match m in matchCollection)
            {
                byContent = byContent.Replace(m.Value, "");
            }
            return byContent;
        }
    }
}