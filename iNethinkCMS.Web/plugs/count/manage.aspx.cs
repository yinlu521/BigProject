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

namespace iNethinkCMS.Web.plugs.count
{
    public partial class manage : Admin_BasePage
    {
        private string vXmlPath = @"/plugs/count/setting.xml";
        private string vState = "";
        private string vShow = "";

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
                vShow = XMLHelper.GetXmlAttribute(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"show\"]", "value").Value.Trim();
                this.txtState.SelectedValue = vState;
                this.txtShow.SelectedValue = vShow;
            }
        }

        protected void Button_Submit_Click(object sender, EventArgs e)
        {
            vState = this.txtState.SelectedValue;
            vShow = this.txtShow.SelectedValue;

            XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"state\"]", "value", vState);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(vXmlPath), "//plugs//config//key[@name=\"show\"]", "value", vShow);

            Response.Redirect(ViewState["reJumpUrl"].ToString());
        }
    }
}