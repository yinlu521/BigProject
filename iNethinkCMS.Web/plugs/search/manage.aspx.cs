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
using iNethinkCMS.Helper;
using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.plugs.search
{
    public partial class manage : Admin_BasePage
    {
        private string vXmlPath = @"/plugs/search/setting.xml";
        private string vState;
        private string vTemplatepath;
        private string vKeywordlengthMin;
        private string vKeywordlengthMax;
        private string vSearchMode;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("c");
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["reJumpUrl"] = Request.UrlReferrer.AbsoluteUri;
                }
                else
                {
                    ViewState["reJumpUrl"] = Request.Url.AbsoluteUri;
                }

                vState = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"state\"]", "value").Value.Trim();
                vTemplatepath = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"templatepath\"]", "value").Value.Trim();
                vKeywordlengthMin = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"keywordlengthmin\"]", "value").Value.Trim();
                vKeywordlengthMax = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"keywordlengthmax\"]", "value").Value.Trim();
                vSearchMode = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"searchmode\"]", "value").Value.Trim();

                this.txtState.SelectedValue = vState;
                this.txtTemplatepath.Text = vTemplatepath;
                this.txtKeywordlengthMin.Text = vKeywordlengthMin;
                this.txtKeywordlengthMax.Text = vKeywordlengthMax;
                this.txtSearchMode.SelectedValue = vSearchMode;
            }
        }

        protected void Button_Submit_Click(object sender, EventArgs e)
        {
            vState = this.txtState.SelectedValue;
            vTemplatepath = this.txtTemplatepath.Text.Trim();
            vKeywordlengthMin = this.txtKeywordlengthMin.Text.Trim();
            vKeywordlengthMax = this.txtKeywordlengthMax.Text.Trim();
            vSearchMode = this.txtSearchMode.SelectedValue;

            if (this.vTemplatepath.Length == 0)
            {
                MessageBox.Show(this, "请输入模板路径!");
                return;
            }
            if (!Command_Validate.IsNumber(vKeywordlengthMin))
            {
                MessageBox.Show(this, "关键字长度（最小）只能为数字!");
                return;
            }
            if (!Command_Validate.IsNumber(vKeywordlengthMax))
            {
                MessageBox.Show(this, "关键字长度（最大）只能为数字!");
                return;
            }

            XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"state\"]", "value", vState);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"templatepath\"]", "value", vTemplatepath);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"keywordlengthmin\"]", "value", vKeywordlengthMin);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"keywordlengthmax\"]", "value", vKeywordlengthMax);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"searchmode\"]", "value", vSearchMode);

            Response.Redirect(ViewState["reJumpUrl"].ToString());


        }
    }
}