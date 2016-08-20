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
using System.Text.RegularExpressions;

using iNethinkCMS.Helper;
using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.news
{
    public partial class news_content : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Content bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Content();
        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel bll_column = new iNethinkCMS.BLL.BLL_iNethinkCMS_Channel();
        iNethinkCMS.BLL.BLL_iNethinkCMS_Special bll_special = new iNethinkCMS.BLL.BLL_iNethinkCMS_Special();
        iNethinkCMS.BLL.BLL_iNethinkCMS_Upload bll_upload = new iNethinkCMS.BLL.BLL_iNethinkCMS_Upload();
        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel_CustomFields bll_customfields = new BLL.BLL_iNethinkCMS_Channel_CustomFields();

        iNethinkCMS.Model.Model_iNethinkCMS_Content model = new iNethinkCMS.Model.Model_iNethinkCMS_Content();

        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vPage = 1;
        private string vSQL = "";
        private string vKeyType = "";
        private string vKey = "";

        private string v_sCID;
        private string v_sSID;
        private string v_sDisplay;
        private string v_sCommend;
        private string v_sIsComment;

        public int vID = 0;
        public int vCid;
        private int vSid;
        private string vTitle;
        private string vSubTitle;
        private string vTitle_Color;
        private string vTitle_Style;
        private string vAuthor;
        private string vSource;
        private string vJumpurl;
        private string vKeywords;
        private string vDescription;
        private string vIndexpic;
        private int vViews;
        private int vCommend;
        private int vIsComment;
        private int vDisplay;
        private DateTime vCreatetime;
        private DateTime vModifytime;
        private int vOrderNum;
        private string vContents;
        private string vFieldsInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("a");

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
            //栏目权限控制
            if (String.IsNullOrEmpty(SysLoginUserChannelPower))
            {
                vSQL += " And [CID] = 0";
            }
            else if (SysLoginUserChannelPower != "0")
            {
                vSQL += " And [CID] In (" + SysLoginUserChannelPower + ")";
            }

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

            v_sCID = Request.QueryString["sCID"] != null ? Request.QueryString["sCID"] : "";
            v_sSID = Request.QueryString["sSID"] != null ? Request.QueryString["sSID"] : "";
            v_sDisplay = Request.QueryString["sDisplay"] != null ? Request.QueryString["sDisplay"] : "";
            v_sCommend = Request.QueryString["sCommend"] != null ? Request.QueryString["sCommend"] : "";
            v_sIsComment = Request.QueryString["sIsComment"] != null ? Request.QueryString["sIsComment"] : "";

            if (v_sCID.Length > 0)
            {
                vSQL += " And [CID] = " + v_sCID;
            }
            if (v_sSID.Length > 0)
            {
                vSQL += " And [SID] = " + v_sSID;
            }
            if (v_sDisplay.Length > 0)
            {
                vSQL += " And [Display] = " + v_sDisplay;
            }
            if (v_sCommend.Length > 0)
            {
                vSQL += " And [Commend] = " + v_sCommend;
            }
            if (v_sIsComment.Length > 0)
            {
                vSQL += " And [IsComment] = " + v_sIsComment;
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
                        this.navInfoID.InnerText = vNavInfo + "内容信息添加";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Add);
                    }
                    else
                    {
                        this.navInfoID.InnerText = vNavInfo + "内容信息修改";
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
                case "deletes": //批量删除
                    Fun_Deletes();
                    break;
                case "audit": //批量审核
                    Fun_Audit();
                    break;
                case "move": //批量移动
                    Fun_Move();
                    break;

                default:
                    this.navInfoID.InnerText = vNavInfo + "内容信息管理";
                    this.mainID.Visible = true;
                    if (!IsPostBack)
                    {
                        BindDropdownList(this.sCID);
                        BindDropdownList_Special(this.sSID);
                        BindDropdownList(this.moveCID);

                        this.sKeyType.SelectedValue = vKeyType;
                        this.sKey.Text = vKey;

                        this.sCID.SelectedValue = v_sCID;
                        this.sSID.SelectedValue = v_sSID;
                        this.sDisplay.SelectedValue = v_sDisplay;
                        this.sCommend.SelectedValue = v_sCommend;
                        this.sIsComment.SelectedValue = v_sIsComment;

                        this.sKeyType.SelectedValue = vKeyType;
                        this.sKey.Text = vKey;
                    }
                    PageListInfo();
                    break;
            }

            this.txtCid.Attributes.Add("onchange", "ajax_content_customfields(" + vID + ");");
            this.txtTitle.Attributes.Add("onblur", "ajax_content_checktitle(" + vID + ");");
            this.txtCreatetime.Attributes.Add("onfocus", "WdatePicker({startDate:this.value,dateFmt:'yyyy/M/d H:mm:ss',isShowClear:false,errDealMode:1,autoPickDate:true,readOnly:true})");
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
            if (siteConfig.DisplayTitleRule == "true")
            {
                this.txtTitle.CssClass = "int int_rule";
                this.txtSubTitle.CssClass = "int int_rule";
            }

            BindDropdownList(this.txtCid);
            BindDropdownList(this.sCID);
            BindDropdownList_Special(this.txtSid);
            BindDropdownList_Special(this.sSID);

            if (byID == 0)
            {
                this.txtCid.Text = "0";
                this.txtSid.Text = "0";
                this.txtTitle.Text = "";
                this.txtSubTitle.Text = "";
                this.txtTitle_Color.Text = "";
                this.txtTitle_Style.Text = "";
                this.txtAuthor.Text = "";
                this.txtSource.Text = "";
                this.txtJumpurl.Text = "";
                this.txtKeywords.Text = "";
                if (seoConfig.AutoDescription == "1")
                {
                    this.txtAutoGetDescription.Checked = true;
                    //this.txtDescription.Visible = false;
                }
                this.txtDescription.Text = "";
                this.txtPicture.Text = "";
                this.txtViews.Text = "0";
                this.txtCommend.Checked = false;
                this.txtIsComment.Checked = true;
                this.txtDisplay.Checked = true;
                this.txtCreatetime.Text = DateTime.Now.ToString();
                this.txtModifytime.Text = DateTime.Now.ToString();
                this.txtOrderNum.Text = "0";
                this.txtContents.Text = "";
            }
            else
            {
                model = bll.GetModel(byID);
                this.txtCid.Text = model.Cid.ToString();
                this.txtSid.Text = model.Sid.ToString();
                this.txtTitle.Text = model.Title;
                this.txtSubTitle.Text = model.SubTitle;
                this.txtTitle_Color.Text = model.Title_Color;
                this.txtTitle_Style.Text = model.Title_Style;
                this.txtAuthor.Text = model.Author;
                this.txtSource.Text = model.Source;
                this.txtJumpurl.Text = model.Jumpurl;
                this.txtKeywords.Text = model.Keywords;
                if (seoConfig.AutoDescription == "1")
                {
                    this.txtAutoGetDescription.Checked = true;
                    //this.txtDescription.Visible = false;
                }
                this.txtDescription.Text = model.Description;

                this.txtPicture.Text = model.Indexpic;
                this.txtViews.Text = model.Views.ToString();
                if (model.Commend == 1)
                {
                    this.txtCommend.Checked = true;
                }
                if (model.IsComment == 1)
                {
                    this.txtIsComment.Checked = true;
                }
                if (model.Display == 1)
                {
                    this.txtDisplay.Checked = true;
                }
                this.txtCreatetime.Text = model.Createtime.ToString();
                this.txtModifytime.Text = model.Modifytime.ToString();
                this.txtOrderNum.Text = model.OrderNum.ToString();
                this.txtContents.Text = model.Contents;
            }
        }

        private bool Fun_GetValue()
        {
            if (this.txtCid.Text.Trim() == "0")
            {
                MessageBox.Show(this, "请选择所属栏目!");
                return false;
            }
            if (this.txtTitle.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入标题!");
                return false;
            }
            if (this.txtContents.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入内容!");
                return false;
            }


            if (!Command_Validate.IsNumber(this.txtOrderNum.Text.Trim()))
            {
                MessageBox.Show(this, "排序权重只能输入数字!");
                return false;
            }

            if (!Command_Validate.IsNumber(this.txtViews.Text.Trim()))
            {
                MessageBox.Show(this, "访问次数只能输入数字!");
                return false;
            }

            vCid = int.Parse(this.txtCid.Text);
            vSid = int.Parse(this.txtSid.Text);
            vTitle = this.txtTitle.Text.Trim();
            vSubTitle = this.txtSubTitle.Text.Trim();
            vTitle_Color = this.txtTitle_Color.Text;
            vTitle_Style = this.txtTitle_Style.Text;
            vAuthor = this.txtAuthor.Text.Trim();
            vSource = this.txtSource.Text.Trim();
            vJumpurl = this.txtJumpurl.Text;
            vKeywords = this.txtKeywords.Text;
            if (this.txtAutoGetDescription.Checked == true)
            {
                vDescription = Command_StringPlus.Left(Command_StringPlus.LostHTML(this.txtContents.Text), 500);
            }
            else
            {
                vDescription = Command_StringPlus.LostHTML(this.txtDescription.Text);
            }
            vIndexpic = this.txtPicture.Text;
            vViews = int.Parse(this.txtViews.Text);
            vCommend = this.txtCommend.Checked == true ? 1 : 0;
            vIsComment = this.txtIsComment.Checked == true ? 1 : 0;
            vDisplay = this.txtDisplay.Checked == true ? 1 : 0;
            vCreatetime = DateTime.Parse(this.txtCreatetime.Text);
            vModifytime = DateTime.Now;
            vOrderNum = int.Parse(this.txtOrderNum.Text);
            vContents = this.txtContents.Text;

            vFieldsInfo = "";
            string vSQL_CustomFields = "[Display] =  1 And CharIndex('," + vCid + ",' , ',' + CAST([CIDList] AS varchar(8000)) + ',') > 0";
            DataTable dt = bll_customfields.GetList(0, vSQL_CustomFields, "OrderNum Desc,ID Desc").Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string vCustomFieldsKey = dt.Rows[i]["CustomFieldsKey"].ToString();
                string vFieldsInfoTmp = Request.Form["" + vCustomFieldsKey + ""];
                if (!String.IsNullOrEmpty(vFieldsInfoTmp))
                {
                    //vFieldsInfoTmp = Command_Validate.Decode(vFieldsInfoTmp);
                }
                vFieldsInfoTmp = "<" + vCustomFieldsKey + ">" + vFieldsInfoTmp + "</" + vCustomFieldsKey + ">";
                vFieldsInfo += vFieldsInfoTmp;
            }

            return true;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("Title = '" + vTitle + "'") > 0)
                {
                    MessageBox.Show(this, "标题名称 [" + vTitle + "] 已经存在!");
                    return;
                }

                model.Cid = vCid;
                model.Sid = vSid;
                model.Title = vTitle;
                model.SubTitle = vSubTitle;
                model.Title_Color = vTitle_Color;
                model.Title_Style = vTitle_Style;
                model.Author = vAuthor;
                model.Source = vSource;
                model.Jumpurl = vJumpurl;
                model.Keywords = vKeywords;
                model.Description = vDescription;
                model.Indexpic = vIndexpic;
                model.Views = vViews;
                model.Commend = vCommend;
                model.IsComment = vIsComment;
                model.Display = vDisplay;
                model.Createtime = vCreatetime;
                model.Modifytime = vModifytime;
                model.OrderNum = vOrderNum;
                model.Contents = vContents;
                model.FieldsInfo = vFieldsInfo;
                bll.Add(model);

                int maxID = bll.GetMaxID();
                bll_upload.UpdateUploadFile(vContents, 1, maxID, vCid);
                bll_upload.UpdateUploadFile(vFieldsInfo, 1, maxID, vCid);

                Response.Redirect(Request.Path);
            }

        }

        protected void Button_Submit_Click_Edit(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("ID <> " + vID + " And Title = '" + vTitle + "'") > 0)
                {
                    MessageBox.Show(this, "标题名称 [" + vTitle + "] 已经存在!");
                    return;
                }
                model.ID = vID;
                model.Cid = vCid;
                model.Sid = vSid;
                model.Title = vTitle;
                model.SubTitle = vSubTitle;
                model.Title_Color = vTitle_Color;
                model.Title_Style = vTitle_Style;
                model.Author = vAuthor;
                model.Source = vSource;
                model.Jumpurl = vJumpurl;
                model.Keywords = vKeywords;
                model.Description = vDescription;
                model.Indexpic = vIndexpic;
                model.Views = vViews;
                model.Commend = vCommend;
                model.IsComment = vIsComment;
                model.Display = vDisplay;
                model.Createtime = vCreatetime;
                model.Modifytime = vModifytime;
                model.OrderNum = vOrderNum;
                model.Contents = vContents;
                model.FieldsInfo = vFieldsInfo;
                bll.Update(model);

                bll_upload.UpdateUploadFile_Reset(1, vID, vCid);  //重置上传表中的数据
                bll_upload.UpdateUploadFile(vContents, 1, vID, vCid);
                bll_upload.UpdateUploadFile(vFieldsInfo, 1, vID, vCid);

                Response.Redirect(ViewState["reJumpUrl"].ToString());
            }
        }

        #endregion

        #region Delete 数据删除
        protected void Fun_Delete(int byID)
        {
            int byCID = int.Parse(bll.GetModel(byID).Cid.ToString());
            if (bll.Delete(byID) == true)
            {
                bll_upload.UpdateUploadFile_Reset(1, byID, byCID);  //重置上传表中的数据
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
        }
        #endregion

        #region 数据批量操作
        protected void Fun_Deletes()
        {
            string vIDs;
            vIDs = Request.Form["ids"].ToString();
            if (bll.DeleteList(vIDs) == true)
            {
                //标记上传表中的数据
                Helper.SQLHelper.ExecuteSql("update [iNethinkCMS_Upload] set [UpType]=0 ,[Aid]=0 ,[Cid]=0 Where [Aid] In (" + vIDs + ") And [UpType]=1");

                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
        }

        protected void Fun_Audit()
        {
            string vIDs;
            vIDs = Request.Form["ids"].ToString();
            if (bll.AuditList(vIDs) == true)
            {
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
        }

        protected void Fun_Move()
        {
            int vMoveCid;
            string vIDs;

            vMoveCid = int.Parse(this.moveCID.Text);
            vIDs = Request.Form["ids"].ToString();
            if (bll.MoveList(vMoveCid, vIDs) == true)
            {
                Helper.SQLHelper.ExecuteSql("update [iNethinkCMS_Upload] set [Cid]=" + vMoveCid + " Where [UpType]=1 And [Aid] In (" + vIDs + ")");
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
        }
        #endregion

        #region 转换/获取相关信息

        //栏目名称
        protected string Fun_ChannleInfo(object byCID)
        {
            byCID = Convert.ToInt32(byCID);
            DataTable dt = bll_column.GetList("Cid=" + byCID).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Name"].ToString();
            }
            else
            {
                return "";
            }

        }

        //专题名称
        protected string Fun_SpecialInfo(object bySID)
        {
            bySID = Convert.ToInt32(bySID);
            DataTable dt = bll_special.GetList("ID=" + bySID).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return "<span title=\"" + dt.Rows[0]["SpecialName"].ToString() + "\">[专]</span>";
            }
            else
            {
                return "";
            }

        }

        protected string Fun_DisplayInfo(object byDisplay)
        {

            return byDisplay.ToString() == "0" ? "<font color=\"#ff0000\">未发布</font>" : "发布";
        }

        protected string Fun_ExpanInfo(object byCommend, object byIsComment)
        {
            string tmpInfo = "";
            tmpInfo = byCommend.ToString() == "1" ? "<img alt=\"推荐\" src=\"../skin/images/ico_01.gif\" />" : "";
            //tmpInfo += byIsComment.ToString() == "1" ? "&nbsp;[评]" : "";
            return tmpInfo;
        }

        #endregion

        #region  扩展搜索功能
        protected void Do_ExtSearch(object sender, EventArgs e)
        {
            v_sCID = Request.Form["sCID"] != null ? Request.Form["sCID"] : "";
            v_sSID = Request.Form["sSID"] != null ? Request.Form["sSID"] : "";
            v_sDisplay = Request.Form["sDisplay"] != null ? Request.Form["sDisplay"] : "";
            v_sCommend = Request.Form["sCommend"] != null ? Request.Form["sCommend"] : "";
            v_sIsComment = Request.Form["sIsComment"] != null ? Request.Form["sIsComment"] : "";

            Response.Redirect(Request.Path + "?scid=" + this.sCID.SelectedValue + "&ssid=" + this.sSID.SelectedValue + "&sdisplay=" + this.sDisplay.Text + "&scommend=" + this.sCommend.Text + "&siscomment=" + this.sIsComment.Text);
        }
        #endregion

        #region 专题下拉列表输出
        protected void BindDropdownList_Special(DropDownList byDropDownList)
        {
            DataTable dt = bll_special.GetList(0, "[Display] = 1", "OrderNum Desc").Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byDropDownList.Items.Add(new ListItem(dt.Rows[i]["SpecialName"].ToString(), dt.Rows[i]["ID"].ToString()));
            }
        }
        #endregion

        #region 栏目下拉列表输出
        /// <summary>
        /// 绑定DropDownList;
        /// </summary>
        protected void BindDropdownList(DropDownList byDropDownList)
        {
            string vSqlWhere = "[Mold] = 1";
            if (String.IsNullOrEmpty(SysLoginUserChannelPower))
            {
                vSqlWhere += " And [ID] = 0";
            }
            else if (SysLoginUserChannelPower != "0")
            {
                vSqlWhere += " And [ID] In (" + SysLoginUserChannelPower + ")";
            }

            DataTable dt = bll_column.GetList(0, vSqlWhere, "OrderNum Desc").Tables[0];
            //this.txtCid.Items.Insert(0, new ListItem("请选择所属栏目", "0"));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["FatherID"].ToString().Trim() == "0")//绑定根节点
                    {
                        byDropDownList.Items.Add(new ListItem(row["Name"].ToString(), row["CID"].ToString()));
                        this.bindChildItem(byDropDownList, dt, row["CID"].ToString(), 1);
                    }
                }
            }
            else
            {
                if (byDropDownList.ID == "txtCid")
                {
                    byDropDownList.Items.Add(new ListItem("尚未添加任何栏目/无栏目管理权限!", "0"));
                }
            }
        }

        protected void bindChildItem(DropDownList byDropDownList, DataTable dt, string id, int length)
        {
            DataRow[] rows = dt.Select("FatherID=" + id + "", "OrderNum Desc");
            for (int i = 0; i < rows.Length; i++)
            {
                byDropDownList.Items.Add(new ListItem(WebUI_Function.SpaceLength(length) + "├ " + rows[i]["Name"].ToString(), rows[i]["CID"].ToString()));
                this.bindChildItem(byDropDownList, dt, rows[i]["CID"].ToString(), length + 1);
            }
        }
        #endregion

    }
}