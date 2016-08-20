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
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.sys
{
    public partial class sys_upclear : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Upload bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Upload();
        iNethinkCMS.Model.Model_iNethinkCMS_Upload model = new iNethinkCMS.Model.Model_iNethinkCMS_Upload();

        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vPage = 1;
        private string vSQL = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("e");

            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            if (Request.QueryString["Page"] != null && Request.QueryString["Page"].Trim() != "")
            {
                if (!int.TryParse(Request.QueryString["Page"], out vPage))
                {
                    vPage = 1;
                }
            }
            //vSQL = "UpType > 0 And Aid > 0";

            this.navButtonID.Visible = false;
            this.mainID.Visible = false;
            this.clearUpFileID.Visible = false;
            this.clearThumbFileID.Visible = false;
            switch (vAct)
            {
                case "clearupfile":
                    this.navInfoID.InnerText = vNavInfo + "上传文件清理";
                    this.clearUpFileID.Visible = true;
                    break;

                case "clearupfile_do":
                    //读取系统内所有无用的文件
                    DataTable dt = bll.GetList("UpType = 0 And Aid = 0").Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string vDir = dt.Rows[i]["Dir"].ToString();
                        vDir = Server.MapPath(vDir);
                        System.IO.File.Delete(vDir);
                        //int vID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        //bll.Delete(vID);
                    }
                    iNethinkCMS.Helper.SQLHelper.ExecuteSql("Delete From [iNethinkCMS_Upload] Where [UpType] = 0 And [Aid] = 0");

                    Response.Write(dt.Rows.Count.ToString());
                    Response.End();
                    break;

                case "clearthumbfile":
                    this.navInfoID.InnerText = vNavInfo + "清空缩略图";
                    this.clearThumbFileID.Visible = true;
                    break;

                case "clearthumbfile_do":
                    int rInfo = 0;
                    string vThumbPath = Server.MapPath(@"/upload/thumbnail");
                    if (System.IO.Directory.Exists(vThumbPath))
                    {
                        rInfo = System.IO.Directory.GetFiles(vThumbPath).Length;
                        System.IO.Directory.Delete(vThumbPath, true);
                    }
                    Response.Write(rInfo.ToString());
                    Response.End();
                    break;


                default:
                    this.navInfoID.InnerText = vNavInfo + "上传文件管理";
                    this.navButtonID.Visible = true;
                    this.mainID.Visible = true;
                    PageListInfo();
                    break;
            }
        }

        protected string Fun_Switch_UpType(object byUpType)
        {
            string vBackInfo = "";
            switch (byUpType.ToString())
            {
                case "1":
                    vBackInfo = "新闻信息相关";
                    break;
                case "2":
                    vBackInfo = "自定义标签相关";
                    break;
                case "3":
                    vBackInfo = "自定义页面相关";
                    break;
                case "4":
                    vBackInfo = "专题形象图";
                    break;
                case "5":
                    vBackInfo = "栏目形象图";
                    break;
                case "6":
                    vBackInfo = "友情链接LOGO";
                    break;
                default:
                    vBackInfo = "-";
                    break;
            }
            return vBackInfo;
        }

        protected string Fun_Switch_FileValid(object byUpType)
        {
            return byUpType.ToString() == "0" ? "<font color=\"#ff0000\">无效</font>" : "有效";
        }

        protected void PageListInfo()
        {
            int vPageSize = int.Parse(siteConfig.PageListNum);
            int vRecordCount = bll.GetRecordCount(vSQL);

            Repeater.DataSource = bll.GetListByPage(vSQL, "ID Desc", (vPage - 1) * vPageSize, vPage * vPageSize);
            Repeater.DataBind();

            string pTemp = "";
            int vPageCount = 1;
            if (vRecordCount > 0)
            {
                vPageCount = (int)Math.Ceiling((double)vRecordCount / (double)vPageSize);
                pTemp = WebUI_PageList.GetPagingInfo_Manage(vPageCount, vRecordCount, vPage, vPageSize);
            }

            this.pagelist.InnerHtml = pTemp;
            this.iNoInfo.Visible = vRecordCount == 0 ? true : false;
        }
    }
}