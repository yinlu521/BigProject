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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iNethinkCMS.Helper;

namespace iNethinkCMS.Web.plugs.count
{
    public partial class count : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rID;
            rID = Request.QueryString["ID"];
            rID = iNethinkCMS.Command.Command_Validate.SqlTextClear(rID);

            if (rID != null && Command.Command_Validate.IsNumber(rID) == true)
            {
                string vXmlPath = Server.MapPath(@"/plugs/count/setting.xml");
                int vID = Convert.ToInt32(rID);
                int vState = Convert.ToInt32(XMLHelper.GetXmlAttribute(vXmlPath, "//plugs//config//key[@name=\"state\"]", "value").Value.Trim());
                int vShow = Convert.ToInt32(XMLHelper.GetXmlAttribute(vXmlPath, "//plugs//config//key[@name=\"show\"]", "value").Value.Trim());

                if (vState == 0)
                {
                    Response.End();
                }

                iNethinkCMS.Helper.SQLHelper.ExecuteSql("Update [iNethinkCMS_Content] Set [Views] = [Views] + 1 Where [ID] = " + vID);
                if (vShow == 1)
                {
                    BLL.BLL_iNethinkCMS_Content bll = new BLL.BLL_iNethinkCMS_Content();
                    Model.Model_iNethinkCMS_Content model = new Model.Model_iNethinkCMS_Content();
                    model = bll.GetModel(vID);
                    if (model != null)
                    {
                        Response.Write("document.write(" + model.Views + ")");
                    }
                }
            }

        }
    }
}