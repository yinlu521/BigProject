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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iNethinkCMS.Helper;
using iNethinkCMS.Command;

namespace iNethinkCMS.Web.install
{
    public partial class index : System.Web.UI.Page
    {
        string vDataBaseServer;
        string vDataBasePort;
        string vDataBaseUser;
        string vDataBasePass;
        string vDataBaseName;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.MainID.Visible = false;
            this.ErrorID.Visible = false;

            if (File.Exists(Server.MapPath("~/install/ok.txt")) == true)
            {
                this.ErrorID.Visible = true;
                this.ErrorID.InnerHtml = "<div class='error'>系统已经安装，如需要重新安装/修改数据库连接配置请删除install目录下的ok.txt文件</div>";
            }
            else
            {
                this.MainID.Visible = true;
                if (!IsPostBack)
                {
                    Fun_GetConnInfo();
                }
            }
        }

        protected void Button_StartInstall_Click(object sender, EventArgs e)
        {
            vDataBaseServer = this.txtDataBaseServer.Text.Trim();
            vDataBasePort = this.txtDataBasePort.Text.Trim();
            vDataBaseUser = this.txtDataBaseUser.Text.Trim();
            vDataBasePass = this.txtDataBasePass.Text.Trim();
            vDataBaseName = this.txtDataBaseName.Text.Trim();

            if (vDataBaseServer.Length == 0)
            {
                MessageBox.Show(this, "请输入SQL数据库服务器地址!");
                return;
            }
            if (vDataBasePort.Length == 0)
            {
                MessageBox.Show(this, "请输入SQL数据库服务器端口!");
                return;
            }
            if (vDataBaseName.Length == 0)
            {
                MessageBox.Show(this, "请输入数据库名!");
                return;
            }
            if (vDataBaseUser.Length == 0)
            {
                MessageBox.Show(this, "请输入数据库帐号!");
                return;
            }

            //判断数据库连接是否正确
            string _connectionString;
            string vDataBaseServerTmp = vDataBaseServer;
            if (vDataBasePort != "0")
            {
                vDataBaseServerTmp = vDataBaseServer + "," + vDataBasePort;
            }
            _connectionString = "Data Source=" + vDataBaseServerTmp + ";Initial Catalog=" + vDataBaseName + ";User ID=" + vDataBaseUser + ";Password=" + vDataBasePass + "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Fun_SetConnInfo(); //储存数据库配置

                    //判断数据库中是否有相应的表
                    string vDBsheetInfo = "";
                    string[] vDBsheet = { "iNethinkCMS_Channel",
                                          "iNethinkCMS_Channel_CustomFields",
                                          "iNethinkCMS_Content",
                                          "iNethinkCMS_Custom_Pages", 
                                          "iNethinkCMS_Custom_Tags", 
                                          "iNethinkCMS_Dict",
                                          "iNethinkCMS_Extend_Blogroll",
                                          "iNethinkCMS_Plugs_Guestbook",
                                          "iNethinkCMS_Special",
                                          "iNethinkCMS_Upload",
                                          "iNethinkCMS_User" };

                    for (int i = 0; i < vDBsheet.Length; i++)
                    {
                        if (SQLHelper.TabExists(vDBsheet[i]) == true)
                        {
                            vDBsheetInfo += "<br>" + vDBsheet[i];
                        }
                    }
                    if (vDBsheetInfo.Length > 0)
                    {
                        this.MainID.Visible = false;
                        this.ErrorID.Visible = true;
                        this.ErrorID.InnerHtml = "<div class='error'><b>由于系统中以下的表已经存在，本次操作仅对[数据库配置信息]进行了修改！</b><br>如要重建，请删除后重新操作!" + vDBsheetInfo + "</div>";
                        Fun_DoComplete();
                        return;
                    }
                    else
                    {
                        //读取SQL脚本，并执行
                        string sqlPath = Server.MapPath("~/install/db.sql");
                        string sqlContent = File.ReadAllText(sqlPath, System.Text.Encoding.UTF8);
                        sqlContent = sqlContent.Replace("###UserRegTime###", DateTime.Now.ToString());

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = sqlContent;
                        cmd.ExecuteNonQuery();
                    }

                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    this.MainID.Visible = false;
                    this.ErrorID.Visible = true;
                    this.ErrorID.InnerHtml = "<div class='error'>数据库操作失败!<br>" + ex.Message.ToString() + "</div>";
                    return;
                }
                finally
                {
                    connection.Close();
                    SqlConnection.ClearAllPools();
                }

                Fun_DoComplete();
                Response.Redirect("/admin/index.aspx");
            }
        }

        protected void Fun_DoComplete()
        {
            string vWebConfigPath = Server.MapPath("~/Web.config");
            Helper.XMLHelper.CreateOrUpdateXmlAttributeByXPath(vWebConfigPath, @"/configuration/system.web/compilation", "debug", "false");

            FileStream fs = File.Create(Server.MapPath("~/install/ok.txt"));
            fs.Close();
        }

        protected void Fun_GetConnInfo()
        {

            string strXmlFile = HttpContext.Current.Server.MapPath("~/config/conn.config");
            vDataBaseServer = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBaseServer").InnerText.Trim();
            vDataBasePort = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBasePort").InnerText.Trim();
            vDataBaseUser = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBaseUser").InnerText.Trim();
            vDataBasePass = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBasePass").InnerText.Trim();
            vDataBaseName = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBaseName").InnerText.Trim();

            this.txtDataBaseServer.Text = vDataBaseServer;
            this.txtDataBasePort.Text = vDataBasePort;
            this.txtDataBaseUser.Text = vDataBaseUser;
            this.txtDataBasePass.Text = vDataBasePass;
            this.txtDataBaseName.Text = vDataBaseName;
        }

        protected void Fun_SetConnInfo()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/config/conn.config");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//conn_configuration", "DataBaseServer", vDataBaseServer);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//conn_configuration", "DataBasePort", vDataBasePort);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//conn_configuration", "DataBaseUser", vDataBaseUser);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//conn_configuration", "DataBasePass", vDataBasePass);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//conn_configuration", "DataBaseName", vDataBaseName);
        }
    }
}