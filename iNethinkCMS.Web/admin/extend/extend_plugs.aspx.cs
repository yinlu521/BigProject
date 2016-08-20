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
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iNethinkCMS.Web.UI;
using iNethinkCMS.Helper;

namespace iNethinkCMS.Web.admin.extend
{
    public partial class extend_plugs : Admin_BasePage
    {
        private string vNavInfo = "当前位置：";
        //private string vAct = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("c");

            this.mainID.Visible = false;

            this.navInfoID.InnerText = vNavInfo + "插件管理";
            this.mainID.Visible = true;
            Fun_PlugsList();
        }

        private void Fun_PlugsList()
        {
            //Command.Command_DataCache.SetCache("iskey","---");
            DataTable dt = new DataTable("Datas");
            dt.Columns.Add("i", Type.GetType("System.Int32"));
            dt.Columns.Add("PlugsName", Type.GetType("System.String"));
            dt.Columns.Add("PlugsDescription", Type.GetType("System.String"));
            dt.Columns.Add("PlugsVer", Type.GetType("System.String"));
            dt.Columns.Add("PlugsState", Type.GetType("System.String"));
            dt.Columns.Add("ManageUrl", Type.GetType("System.String"));

            int i = 0;
            string vPath = Server.MapPath("/plugs");
            DirectoryInfo[] dir = new DirectoryInfo(vPath).GetDirectories();
            foreach (DirectoryInfo d in dir)
            {
                i++;
                string vPlugsName = "";
                string vPlugsDescription = "";
                string vPlugsVer = "";
                string vPlugsState = "";
                string vManageUrl = "";

                //读取配置文件
                string vXmlPath = d.FullName + @"\setting.xml";
                vPlugsName = XMLHelper.GetXmlNodeByXpath(vXmlPath, "//plugs//main//name").InnerText.Trim();
                vPlugsDescription = XMLHelper.GetXmlNodeByXpath(vXmlPath, "//plugs//main//description").InnerText.Trim();
                vPlugsVer = XMLHelper.GetXmlNodeByXpath(vXmlPath, "//plugs//main//version").InnerText.Trim();
                vPlugsState = XMLHelper.GetXmlAttribute(vXmlPath, "//plugs//config//key[@name=\"state\"]", "value").Value.Trim();
                vPlugsState = vPlugsState == "0" ? "停用" : "启用";
                vManageUrl = XMLHelper.GetXmlNodeByXpath(vXmlPath, "//plugs//main//manageurl").InnerText.Trim();
                vManageUrl = "<a href=\"" + vManageUrl + "\">管理</a>";

                dt.Rows.Add(new object[] { i, vPlugsName, vPlugsDescription, vPlugsVer, vPlugsState, vManageUrl });
            }

            Repeater.DataSource = dt;
            Repeater.DataBind();

            this.iNoInfo.Visible = i == 0 ? true : false;
        }
    }
}