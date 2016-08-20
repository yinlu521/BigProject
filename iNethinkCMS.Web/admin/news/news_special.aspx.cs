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

namespace iNethinkCMS.Web.admin.news
{
    public partial class news_special : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Special bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Special();
        iNethinkCMS.Model.Model_iNethinkCMS_Special model = new iNethinkCMS.Model.Model_iNethinkCMS_Special();

        iNethinkCMS.BLL.BLL_iNethinkCMS_Upload bll_upload = new iNethinkCMS.BLL.BLL_iNethinkCMS_Upload();

        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vPage = 1;
        private string vSQL = "";
        private string vKeyType = "";
        private string vKey = "";

        private int vID = 0;
        private string vSpecialName;
        private string vSpecialTitle;
        private string vSpecialKeyword;
        private string vSpecialDescription;
        private string vSpecialTemplate;
        private string vSpecialUrl;
        private string vSpecialPic;
        private string vSpecialContent;
        private int vDisplay;
        private int vOrderNum;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("b");
            #region
            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            if (Request.QueryString["Page"] != null && Request.QueryString["Page"].Trim() != "")
            {
                if (!int.TryParse(Request.QueryString["Page"], out vPage))
                {
                    vPage = 1;
                }
            }
            vKeyType = Request.QueryString["sKeyType"] != null ? Request.QueryString["sKeyType"] : "";
            vKey = Request.QueryString["sKey"] != null ? Request.QueryString["sKey"] : "";
            vKey = vKey.Replace("'", "");
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {
                if (!int.TryParse(Request.QueryString["ID"], out vID))
                {
                    Response.Write("ID Error");
                    Response.End();
                }
            }

            vSQL = "1=1";
            if (vKey.Length > 0)
            {
                if (vKeyType == "ID")
                {
                    if (iNethinkCMS.Command.Command_Validate.IsNumber(vKey) == true)
                    {
                        vSQL += " And " + vKeyType + " = " + vKey + "";
                    }
                    else
                    {
                        vKey = "";
                    }
                }
                else
                {
                    vSQL += " And " + vKeyType + " Like '%" + vKey + "%'";
                }
            }

            #endregion

            this.mainID.Visible = false;
            this.guideID.Visible = false;
            switch (vAct)
            {
                case "guide":
                    this.guideID.Visible = true;
                    if (vID == 0)
                    {
                        this.navInfoID.InnerText = vNavInfo + "专题添加";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Add);
                    }
                    else
                    {
                        this.navInfoID.InnerText = vNavInfo + "专题修改";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Edit);
                    }
                    if (!IsPostBack)
                    {
                        if (Request.UrlReferrer != null)
                        {
                            ViewState["reJumpUrl"] = Request.UrlReferrer.AbsoluteUri;
                        }
                        Fun_SetValue(vID); //获取frm基本信息
                    }
                    break;
                case "delete":
                    Fun_Delete(vID);
                    break;

                default:
                    this.navInfoID.InnerText = vNavInfo + "专题管理";
                    this.mainID.Visible = true;
                    if (!IsPostBack)
                    {
                        this.sKeyType.SelectedValue = vKeyType;
                        this.sKey.Text = vKey;
                    }
                    PageListInfo();
                    break;
            }
        }

        #region mainID 列表
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

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path + "?skeytype=" + this.sKeyType.SelectedValue + "&skey=" + this.sKey.Text);
        }

        protected string Fun_GetDisplay(object byDis)
        {
            return byDis.ToString() == "1" ? "启用" : "未启用";
        }

        #endregion

        #region guideID 信息修改/添加
        private void Fun_SetValue(int byID)
        {

            if (byID == 0)
            {
                this.txtSpecialName.Text = "";
                this.txtSpecialUrl.Text = "";
                this.txtSpecialPic.Text = "";
                this.txtSpecialTemplate.Text = "special.html";
                this.txtDisplay.Checked = true;
                this.txtOrderNum.Text = "0";
                this.txtSpecialContent.Text = "";
                this.txtSpecialTitle.Text = "";
                this.txtSpecialKeyword.Text = "";
                this.txtSpecialDescription.Text = "";
            }
            else
            {
                model = bll.GetModel(byID);
                this.txtSpecialName.Text = model.SpecialName;
                this.txtSpecialUrl.Text = model.SpecialUrl;
                this.txtSpecialPic.Text = model.SpecialPic;
                this.txtSpecialTemplate.Text = model.SpecialTemplate;
                this.txtDisplay.Checked = model.Display == 1 ? true : false;
                this.txtOrderNum.Text = model.OrderNum.ToString();
                this.txtSpecialContent.Text = model.SpecialContent;
                this.txtSpecialTitle.Text = model.SpecialTitle;
                this.txtSpecialKeyword.Text = model.SpecialKeyword;
                this.txtSpecialDescription.Text = model.SpecialDescription;
            }
        }

        private bool Fun_GetValue()
        {
            if (this.txtSpecialName.Text.Trim().Length < 1)
            {
                MessageBox.Show(this, "请输入专题名称!");
                return false;
            }

            if (this.txtSpecialTemplate.Text.Trim().Length < 1)
            {
                MessageBox.Show(this, "请输入模板路径!");
                return false;
            }

            if (!Command_Validate.IsNumber(this.txtOrderNum.Text.Trim()))
            {
                MessageBox.Show(this, "排序权重只能输入数字!");
                return false;
            }

            vSpecialName = this.txtSpecialName.Text.Trim();
            vSpecialTitle = this.txtSpecialTitle.Text.Trim();
            vSpecialKeyword = Command.Command_StringPlus.LostHTML(this.txtSpecialKeyword.Text.Trim());
            vSpecialDescription = Command.Command_StringPlus.LostHTML(this.txtSpecialDescription.Text.Trim());
            vSpecialTemplate = this.txtSpecialTemplate.Text.Trim();
            vSpecialUrl = this.txtSpecialUrl.Text.Trim();
            vSpecialPic = this.txtSpecialPic.Text.Trim();
            vSpecialContent = this.txtSpecialContent.Text;
            vDisplay = this.txtDisplay.Checked == true ? 1 : 0;
            vOrderNum = Convert.ToInt32(this.txtOrderNum.Text.Trim());

            return true;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("SpecialName = '" + vSpecialName + "'") > 0)
                {
                    MessageBox.Show(this, "专题名称 [" + vSpecialName + "] 已经存在!");
                    return;
                }

                model.SpecialName = vSpecialName;
                model.SpecialTitle = vSpecialTitle;
                model.SpecialKeyword = vSpecialKeyword;
                model.SpecialDescription = vSpecialDescription;
                model.SpecialTemplate = vSpecialTemplate;
                model.SpecialUrl = vSpecialUrl;
                model.SpecialPic = vSpecialPic;
                model.SpecialContent = vSpecialContent;
                model.Display = vDisplay;
                model.OrderNum = vOrderNum;
                bll.Add(model);

                int Aid = bll.GetMaxID();
                bll_upload.UpdateUploadFile_One(vSpecialPic, 4, Aid, 0);
                bll_upload.UpdateUploadFile(vSpecialContent, 4, Aid, 0);
                Response.Redirect(Request.Path);
            }

        }

        protected void Button_Submit_Click_Edit(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("ID <> " + vID + " And SpecialName = '" + vSpecialName + "'") > 0)
                {
                    MessageBox.Show(this, "专题名称 [" + vSpecialName + "] 已经存在!");
                    return;
                }
                model.ID = vID;
                model.SpecialName = vSpecialName;
                model.SpecialTitle = vSpecialTitle;
                model.SpecialKeyword = vSpecialKeyword;
                model.SpecialDescription = vSpecialDescription;
                model.SpecialTemplate = vSpecialTemplate;
                model.SpecialUrl = vSpecialUrl;
                model.SpecialPic = vSpecialPic;
                model.SpecialContent = vSpecialContent;
                model.Display = vDisplay;
                model.OrderNum = vOrderNum;
                bll.Update(model);

                bll_upload.UpdateUploadFile_Reset(4, vID, 0);
                bll_upload.UpdateUploadFile_One(vSpecialPic, 4, vID, 0);
                bll_upload.UpdateUploadFile(vSpecialContent, 4, vID, 0);
                Response.Redirect(ViewState["reJumpUrl"].ToString());
            }
        }

        #endregion

        #region Delete 数据删除
        protected void Fun_Delete(int byID)
        {
            if (bll.Delete(byID) == true)
            {
                bll_upload.UpdateUploadFile_Reset(4, byID, 0);  //重置上传表中的数据

                //将新闻内容表中文章对应的专题取消
                Helper.SQLHelper.ExecuteSql("update [iNethinkCMS_Content] set [Sid]=0 Where [Sid] = " + byID);
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
        }
        #endregion

    }
}