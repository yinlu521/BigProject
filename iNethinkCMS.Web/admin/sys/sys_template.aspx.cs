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
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.sys
{
    public partial class sys_template : Admin_BasePage
    {
        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private string vTemplateFatherDir = "/template/";
        private string vTemplate = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("e");

            #region
            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            vTemplate = Request.QueryString["Template"];
            #endregion

            this.navInfoID.InnerText = vNavInfo + "模板管理";
            this.mainID.Visible = false;

            switch (vAct)
            {
                case "enabled":
                    string strXmlFile = HttpContext.Current.Server.MapPath("~/config/sys.config");
                    Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "TemplateDir", "<![CDATA[" + vTemplateFatherDir + vTemplate + "/]]>");

                    string vSqlScriptFile = HttpContext.Current.Server.MapPath(vTemplateFatherDir + vTemplate + "/sqlscript.sql");
                    if (System.IO.File.Exists(vSqlScriptFile))
                    {
                        string[] vDatabaseTable = { "iNethinkCMS_Channel", "iNethinkCMS_Channel_CustomFields", "iNethinkCMS_Custom_Pages", "iNethinkCMS_Custom_Tags", "iNethinkCMS_Special" };
                        for (int i = 0; i < vDatabaseTable.Length; i++)
                        {
                            Helper.SQLHelper.ExecuteSql("delete from [" + vDatabaseTable[i] + "]");
                            Helper.SQLHelper.ExecuteSql("DBCC CHECKIDENT([" + vDatabaseTable[i] + "],reseed,0)");
                        }

                        //执行脚本文件
                        string sqlContent = File.ReadAllText(vSqlScriptFile, System.Text.Encoding.UTF8);
                        if (!string.IsNullOrEmpty(sqlContent))
                        {
                            Helper.SQLHelper.ExecuteSql(sqlContent);
                        }
                        //文件重命名
                        File.Move(vSqlScriptFile, vSqlScriptFile.Replace("sqlscript.sql", "sqlscript.sqlbak"));
                    }

                    Web.UI.WebUI_Function.Fun_CacheDel();   //清空一次系统缓存
                    MessageBox.ShowAndRedirect(this, "", "?");
                    break;

                case "release":
                    string vConfigXml = Server.MapPath(vTemplateFatherDir + vTemplate + @"/config.xml");

                    if (!File.Exists(vConfigXml))
                    {
                        Helper.XMLHelper.CreateXmlDocument(vConfigXml, "skin", "1.0", "utf-8", null);   //创建XML文件

                        Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(vConfigXml, "//skin", "Name", "<![CDATA[" + vTemplate + "]]>");
                        Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(vConfigXml, "//skin", "Version", "<![CDATA[" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "]]>");
                        Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(vConfigXml, "//skin", "Author", "<![CDATA[" + SysLoginUserTrueName + "]]>");
                        Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(vConfigXml, "//skin", "Contacts", "<![CDATA[http://cms.inethink.com]]>");
                        Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(vConfigXml, "//skin", "Thumbnail", "<![CDATA[thumb.jpg]]>");
                        Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(vConfigXml, "//skin", "Announcements", "<![CDATA[]]>");
                    }

                    Helper.XMLHelper.CreateOrUpdateXmlNodeByXPath(vConfigXml, "//skin", "Createdate", "<![CDATA[" + DateTime.Now + "]]>");


                    #region 输出SQL脚本[待开发完善]
                    //string[] vDatabaseTable = { "iNethinkCMS_Channel", "iNethinkCMS_Channel_CustomFields", "iNethinkCMS_Custom_Pages", "iNethinkCMS_Custom_Tags", "iNethinkCMS_Special" };
                    //string rTemp = "";
                    //for (int i = 0; i < vDatabaseTable.Length; i++)
                    //{
                    //    rTemp += "SET IDENTITY_INSERT [" + vDatabaseTable[i] + "] ON";
                    //    rTemp += System.Environment.NewLine + System.Environment.NewLine;

                    //    string strSQL = "SELECT  syscolumns.name,systypes.name,syscolumns.isnullable,syscolumns.length FROM  syscolumns, systypes WHERE"
                    //                    + " syscolumns.xusertype = systypes.xusertype And syscolumns.id = object_id ('[" + vDatabaseTable[i] + "]')";

                    //    rTemp += "SQL语句";
                    //    rTemp += System.Environment.NewLine + System.Environment.NewLine;
                    //    rTemp += "SET IDENTITY_INSERT [" + vDatabaseTable[i] + "] OFF" + System.Environment.NewLine;
                    //    rTemp += System.Environment.NewLine;
                    //}

                    //string vSqlScriptlFile = HttpContext.Current.Server.MapPath(vTemplateFatherDir + vTemplate + "/sqlscript.sql");
                    //StreamWriter sw = null;
                    //try
                    //{
                    //    sw = new StreamWriter(vSqlScriptlFile, false, System.Text.Encoding.Default);
                    //    sw.WriteLine(rTemp);
                    //}
                    //catch { }
                    //finally
                    //{
                    //    if (sw != null)
                    //    {
                    //        sw.Flush();
                    //        sw.Close();
                    //    }
                    //}
                    #endregion

                    MessageBox.ShowAndRedirect(this, @"模板发布成功!\n您可以将此模板(文件夹)提供给其他用户使用!", "?");
                    break;

                default:
                    this.mainID.Visible = true;
                    Fun_TemplateList();
                    break;
            }
        }

        private void Fun_TemplateList()
        {
            DataTable dt = new DataTable("Datas");
            dt.Columns.Add("SkinThumbnail", Type.GetType("System.String"));
            dt.Columns.Add("SkinInfo", Type.GetType("System.String"));
            dt.Columns.Add("Announcements", Type.GetType("System.String"));
            dt.Columns.Add("SkinState", Type.GetType("System.String"));
            dt.Columns.Add("SkinManage", Type.GetType("System.String"));

            int i = 0;
            string vTemplateDir = Server.MapPath(vTemplateFatherDir);
            DirectoryInfo di = new DirectoryInfo(vTemplateDir);

            foreach (DirectoryInfo isFolder in di.GetDirectories())
            {
                string vSkinThumbnail = "";
                string vSkinInfo = "";
                string vAnnouncements = "";
                string vSkinState = "0";


                string vConfigXml = isFolder.FullName + @"\config.xml";
                if (File.Exists(vConfigXml))
                {
                    try
                    {
                        i++;
                        vSkinThumbnail = Helper.XMLHelper.GetXmlNodeByXpath(vConfigXml, "//skin//Thumbnail").InnerText.Trim();
                        vSkinThumbnail = vTemplateFatherDir + isFolder.Name + "/" + vSkinThumbnail;
                        vSkinInfo = "模板路径：<font color=\"#ff0000\">" + vTemplateFatherDir + isFolder.Name + "/</font>"
                                    + "<br />模板名称：" + Helper.XMLHelper.GetXmlNodeByXpath(vConfigXml, "//skin//Name").InnerText.Trim()
                                    + "<br />适用版本：" + Helper.XMLHelper.GetXmlNodeByXpath(vConfigXml, "//skin//Version").InnerText.Trim()
                                    + "<br />模板作者：" + Helper.XMLHelper.GetXmlNodeByXpath(vConfigXml, "//skin//Author").InnerText.Trim()
                                    + "<br />联系方式：" + Helper.XMLHelper.GetXmlNodeByXpath(vConfigXml, "//skin//Contacts").InnerText.Trim()
                                    + "<br />发布时间：" + Helper.XMLHelper.GetXmlNodeByXpath(vConfigXml, "//skin//Createdate").InnerText.Trim();

                        vAnnouncements = Helper.XMLHelper.GetXmlNodeByXpath(vConfigXml, "//skin//Announcements").InnerText.Trim();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ShowAndRedirect(this, ex.Message, "?");
                    }
                }
                else
                {
                    vSkinInfo = "模板路径：<font color=\"#ff0000\">" + vTemplateFatherDir + isFolder.Name + "/</font>";
                }

                vSkinState = siteConfig.TemplateDir.Replace(vTemplateFatherDir, "").Replace("/", "") == isFolder.Name ? "1" : "0";  //模板是否启用

                #region 管理操作按钮
                string vSkinManage = "";
                if (vSkinState == "0")
                {
                    string vMsgInfo = @"该模板文件夹下含有[sqlscript.sql]数据库脚本文件!\n强烈建议您备份数据库后进行该操作!"
                                    + @"\n\n系统将清空以下表中内容“iNethinkCMS_Channel, iNethinkCMS_Channel_CustomFields,iNethinkCMS_Custom_Pages,iNethinkCMS_Custom_Tags,iNethinkCMS_Special”!"
                                    + @"\n\n这将不可逆转!您确定执行该操作吗?";

                    string vSqlScriptFile = HttpContext.Current.Server.MapPath(vTemplateFatherDir + isFolder.Name + "/sqlscript.sql");
                    if (System.IO.File.Exists(vSqlScriptFile))
                    {
                        vSkinManage += "<a href=\"javascript:if(confirm('" + vMsgInfo + "')){location.href='?act=enabled&template=" + isFolder.Name + "';}\">启用模板</a><br />";
                    }
                    else
                    {
                        vSkinManage += "<a href=\"?act=enabled&template=" + isFolder.Name + "\">启用模板</a><br />";
                    }
                }
                vSkinManage += "<a href=\"?act=release&template=" + isFolder.Name + "\">发布模板</a>";
                #endregion

                dt.Rows.Add(new object[] { vSkinThumbnail, vSkinInfo, vAnnouncements, vSkinState, vSkinManage });
            }

            Repeater.DataSource = dt;
            Repeater.DataBind();

            this.iNoInfo.Visible = dt.Rows.Count == 0 ? true : false;
        }
    }
}