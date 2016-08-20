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
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.extend
{

    public partial class extend_blogroll : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Extend_Blogroll bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Extend_Blogroll();
        iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll model = new iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll();

        iNethinkCMS.BLL.BLL_iNethinkCMS_Dict bll_dict = new BLL.BLL_iNethinkCMS_Dict();
        iNethinkCMS.BLL.BLL_iNethinkCMS_Upload bll_upload = new iNethinkCMS.BLL.BLL_iNethinkCMS_Upload();

        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vPage = 1;
        private int vID = 0;
        private string vSQL = "";
        private string vKeyType = "";
        private string vKey = "";

        private int vBlogrollClass;
        private string vBlogrollName;
        private string vBlogrollImg;
        private string vBlogrollUrl;
        private int vDisplay;
        private int vOrderNum;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("c");

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

            if (vKey.Length > 0)
            {
                if (vKeyType == "ID")
                {
                    if (iNethinkCMS.Command.Command_Validate.IsNumber(vKey) == true)
                    {
                        vSQL += vKeyType + " = " + vKey + "";
                    }
                    else
                    {
                        vKey = "";
                    }
                }
                else
                {
                    vSQL += vKeyType + " Like '%" + vKey + "%'";
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
                        this.navInfoID.InnerText = vNavInfo + "友情链接添加";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Add);
                    }
                    else
                    {
                        this.navInfoID.InnerText = vNavInfo + "友情链接修改";
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
                    this.navInfoID.InnerText = vNavInfo + "友情链接管理";
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

        #endregion

        #region guideID 信息修改/添加
        private void Fun_SetValue(int byID)
        {
            BindDropdownList_Dict(this.txtBlogrollClass);

            if (byID == 0)
            {
                this.txtBlogrollClass.Text = "0";
                this.txtBlogrollName.Text = "";
                this.txtBlogrollImg.Text = "";
                this.txtBlogrollUrl.Text = "http://";
                this.txtDisplay.Checked = true;
                this.txtOrderNum.Text = "0";
            }
            else
            {
                model = bll.GetModel(byID);
                this.txtBlogrollClass.Text = model.BlogrollClass.ToString();
                this.txtBlogrollName.Text = model.BlogrollName;
                this.txtBlogrollImg.Text = model.BlogrollImg;
                this.txtBlogrollUrl.Text = model.BlogrollUrl;
                this.txtDisplay.Checked = model.Display == 1 ? true : false;
                this.txtOrderNum.Text = model.OrderNum.ToString();
            }
        }

        private bool Fun_GetValue()
        {
            if (this.txtBlogrollClass.Text.Trim() == "0")
            {
                MessageBox.Show(this, "请选择友情链接分类!");
                return false;
            }

            if (this.txtBlogrollName.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入友情链接名称!");
                return false;
            }
            if (this.txtBlogrollUrl.Text.Trim().Replace("http://","").Length == 0)
            {
                MessageBox.Show(this, "请输入友情链接地址!");
                return false;
            }
            if (!Command_Validate.IsNumber(this.txtOrderNum.Text.Trim()))
            {
                MessageBox.Show(this, "排序权重只能输入数字!");
                return false;
            }

            vBlogrollClass = int.Parse(this.txtBlogrollClass.Text);
            vBlogrollName = this.txtBlogrollName.Text;
            vBlogrollImg = this.txtBlogrollImg.Text;
            vBlogrollUrl = this.txtBlogrollUrl.Text;
            vDisplay = this.txtDisplay.Checked == true ? 1 : 0;
            vOrderNum = int.Parse(this.txtOrderNum.Text);

            return true;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("BlogrollClass = " + vBlogrollClass + " And BlogrollName = '" + vBlogrollName + "'") > 0)
                {
                    MessageBox.Show(this, "友情链接分类[" + this.txtBlogrollClass.Text + "]中的友情链接名称 [" + vBlogrollName + "] 已经存在!");
                    return;
                }

                model.BlogrollClass = vBlogrollClass;
                model.BlogrollName = vBlogrollName;
                model.BlogrollImg = vBlogrollImg;
                model.BlogrollUrl = vBlogrollUrl;
                model.Display = vDisplay;
                model.OrderNum = vOrderNum;
                bll.Add(model);

                bll_upload.UpdateUploadFile_One(vBlogrollImg, 6, bll.GetMaxID(), 0);
                Response.Redirect(Request.Path);
            }
        }

        protected void Button_Submit_Click_Edit(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("ID = " + vID) == 0)  //判断该ID是否被删除
                {
                    MessageBox.ShowAndRedirect(this, "你所需要更新的记录 [" + vID + "] 不存在!", ViewState["reJumpUrl"].ToString());
                    return;
                }
                if (bll.GetRecordCount("ID <> " + vID + " And BlogrollClass = " + vBlogrollClass + " And BlogrollName = '" + vBlogrollName + "'") > 0)
                {
                    MessageBox.Show(this, "友情链接分类[" + this.txtBlogrollClass.Text + "]中的友情链接名称 [" + vBlogrollName + "] 已经存在!");
                    return;
                }

                model.ID = vID;
                model.BlogrollClass = vBlogrollClass;
                model.BlogrollName = vBlogrollName;
                model.BlogrollImg = vBlogrollImg;
                model.BlogrollUrl = vBlogrollUrl;
                model.Display = vDisplay;
                model.OrderNum = vOrderNum;
                bll.Update(model);

                bll_upload.UpdateUploadFile_Reset(6, vID, 0);
                bll_upload.UpdateUploadFile_One(vBlogrollImg, 6, vID, 0);

                Response.Redirect(ViewState["reJumpUrl"].ToString());
            }
        }
        #endregion

        #region Delete 数据删除
        protected void Fun_Delete(int byID)
        {
            if (bll.Delete(byID) == true)
            {
                bll_upload.UpdateUploadFile_Reset(6, byID, 0);  //重置上传表中的数据
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
        }
        #endregion

        #region 友情链接分类下拉列表输出
        protected void BindDropdownList_Dict(DropDownList byDropDownList)
        {
            DataTable dt = bll_dict.GetList(0, "[DictType] = 1 And [Display] = 1", "OrderNum Desc").Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byDropDownList.Items.Add(new ListItem(dt.Rows[i]["DictName"].ToString(), dt.Rows[i]["ID"].ToString()));
            }
        }
        #endregion

        protected string Fun_DisplayInfo(object byDisplay)
        {
            return byDisplay.ToString() == "0" ? "<font color=\"#ff0000\">不显示</font>" : "显示";
        }

        protected string Fun_GetDictName(int byDictID)
        {
            return bll_dict.GetDictName(byDictID);
        }

    }
}