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
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.sys
{

    public partial class sys_user : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_User bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_User();
        iNethinkCMS.Model.Model_iNethinkCMS_User model = new iNethinkCMS.Model.Model_iNethinkCMS_User();

        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel bll_column = new iNethinkCMS.BLL.BLL_iNethinkCMS_Channel();

        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vPage = 1;
        private int vID = 0;
        private string vSQL = "";
        private string vKeyType = "";
        private string vKey = "";

        private int vUserType;
        private string vUserName;
        private string vUserPass;
        private string vUserTrueName;
        private string vUserEmail;
        private string vUserPower;
        private string vUserChannelPower;
        private bool vUserChannelPowerAll;
        private DateTime vUserRegTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("e");

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
                        this.navInfoID.InnerText = vNavInfo + "系统用户添加";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Add);
                    }
                    else
                    {
                        this.navInfoID.InnerText = vNavInfo + "系统用户修改";
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

                    this.txtUserChannelPowerAll.Attributes.Add("onclick", "change_channelpower();");
                    break;

                case "delete":
                    Fun_Delete(vID);
                    break;

                default:
                    this.navInfoID.InnerText = vNavInfo + "系统用户管理";
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

        protected string Fun_UserType(object byUserType)
        {
            return byUserType.ToString() == "0" ? "普通用户" : "管理员";
        }
        #endregion

        #region guideID 信息修改/添加
        private void Fun_SetValue(int byID)
        {
            BindDropdownList(this.txtUserChannelPower);

            if (byID == 0)
            {
                this.txtUserType.Value = "1";
                this.txtUserName.Text = "";
                this.txtUserPass.Text = "";
                this.txtUserTrueName.Text = "";
                this.txtUserEmail.Text = "";
                this.txtUserRegTime.Text = DateTime.Now.ToString();

                this.txtUserChannelPowerAll.Checked = true;
            }
            else
            {
                this.txtUserName.ReadOnly = true;

                model = bll.GetModel(byID);
                this.txtUserType.Value = model.UserType.ToString();
                this.txtUserName.Text = model.UserName;
                this.txtUserPass.Attributes["value"] = model.UserPass;
                this.txtUserTrueName.Text = model.UserTrueName;
                this.txtUserEmail.Text = model.UserEmail;
                this.txtUserRegTime.Text = model.UserRegTime.ToString();

                string vUserPower = "," + model.UserPower + ",";
                foreach (ListItem item in this.txtUserPower.Items)
                {
                    if (vUserPower.IndexOf("," + item.Value + ",") > -1)
                    {
                        item.Selected = true;
                    }
                }

                this.txtUserChannelPowerAll.Checked = model.UserChannelPower == "0" ? true : false;

                if (model.UserChannelPower != "0")
                {
                    string tmpUserChannelPower = "," + model.UserChannelPower + ",";
                    foreach (ListItem item in this.txtUserChannelPower.Items)
                    {
                        if (tmpUserChannelPower == ",0,")
                        {
                            item.Selected = true;
                        }
                        else if (tmpUserChannelPower.IndexOf("," + item.Value + ",") > -1)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

        }

        private bool Fun_GetValue()
        {
            if (this.txtUserType.Value.Trim().Length == 0)
            {
                MessageBox.Show(this, "请选择用户类型!");
                return false;
            }
            if (this.txtUserName.Text.Trim().Length < 4)
            {
                MessageBox.Show(this, "用户名不能少于4位!");
                return false;
            }
            if (this.txtUserPass.Text.Trim().Length < 6)
            {
                MessageBox.Show(this, "用户密码不能少于6位!");
                return false;
            }

            vUserType = int.Parse(this.txtUserType.Value.Trim());
            vUserName = this.txtUserName.Text.Trim();
            vUserPass = this.txtUserPass.Text.Trim();
            vUserPower = Fun_Get_txtUserPower();
            vUserChannelPowerAll = this.txtUserChannelPowerAll.Checked;
            vUserTrueName = this.txtUserTrueName.Text.Trim();
            vUserEmail = this.txtUserEmail.Text.Trim();
            vUserRegTime = DateTime.Parse(this.txtUserRegTime.Text);
            if (vUserPass.Length != 32)
            {
                vUserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(vUserPass, "md5").ToLower();
            }

            if (vUserChannelPowerAll == true)
            {
                vUserChannelPower = "0";
            }
            else
            {
                vUserChannelPower = Fun_Get_txtUserChannelPower();
            }
            return true;
        }

        private string Fun_Get_txtUserPower()
        {
            string vUserPowerInfo = "";
            foreach (ListItem item in this.txtUserPower.Items)
            {
                if (item.Selected)
                {
                    vUserPowerInfo += item.Value + ",";
                }
            }
            if (vUserPowerInfo.Length > 0)
            {
                vUserPowerInfo = vUserPowerInfo.Substring(0, vUserPowerInfo.Length - 1);
            }
            return vUserPowerInfo;
        }

        private string Fun_Get_txtUserChannelPower()
        {
            string vUserChannelPowerInfo = "";
            foreach (ListItem item in this.txtUserChannelPower.Items)
            {
                if (item.Selected)
                {
                    vUserChannelPowerInfo += item.Value + ",";
                }
            }
            if (vUserChannelPowerInfo.Length > 0)
            {
                vUserChannelPowerInfo = vUserChannelPowerInfo.Substring(0, vUserChannelPowerInfo.Length - 1);
            }
            return vUserChannelPowerInfo;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("UserName = '" + vUserName + "'") > 0)  //判断用户名是否重复
                {
                    MessageBox.Show(this, "用户名 [" + vUserName + "] 已经存在!");
                    return;
                }

                model.UserType = vUserType;
                model.UserName = vUserName;
                model.UserPass = vUserPass;
                model.UserTrueName = vUserTrueName;
                model.UserEmail = vUserEmail;
                model.UserPower = vUserPower;
                model.UserChannelPower = vUserChannelPower;
                model.UserRegTime = vUserRegTime;

                bll.Add(model);
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
                if (bll.GetRecordCount("ID <> " + vID + " And UserName = '" + vUserName + "'") > 0)  //判断用户名是否重复
                {
                    MessageBox.Show(this, "用户名 [" + vUserName + "] 已经存在!");
                    return;
                }

                model.ID = vID;
                model.UserType = vUserType;
                model.UserName = vUserName;
                model.UserPass = vUserPass;
                model.UserTrueName = vUserTrueName;
                model.UserEmail = vUserEmail;
                model.UserPower = vUserPower;
                model.UserChannelPower = vUserChannelPower;
                model.UserRegTime = vUserRegTime;

                bll.Update(model);
                Response.Redirect(ViewState["reJumpUrl"].ToString());
            }
        }
        #endregion

        #region Delete 数据删除
        protected void Fun_Delete(int byID)
        {
            if (bll.Delete(byID) == true)
            {
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
        }
        #endregion

        #region 栏目列表输出
        /// <summary>
        /// 绑定DropDownList;
        /// </summary>
        protected void BindDropdownList(CheckBoxList byCheckBoxList)
        {
            DataTable dt = bll_column.GetList(0, "", "OrderNum Desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["FatherID"].ToString().Trim() == "0")//绑定根节点
                    {
                        byCheckBoxList.Items.Add(new ListItem(row["Name"].ToString(), row["CID"].ToString()));
                        this.bindChildItem(byCheckBoxList, dt, row["CID"].ToString(), 1);
                    }
                }
            }
            else
            {
                if (byCheckBoxList.ID == "txtCid")
                {
                    byCheckBoxList.Items.Add(new ListItem("尚未添加任何栏目!", "0"));
                }
            }
        }

        protected void bindChildItem(CheckBoxList byCheckBoxList, DataTable dt, string id, int length)
        {
            DataRow[] rows = dt.Select("FatherID=" + id + "", "OrderNum Desc");
            for (int i = 0; i < rows.Length; i++)
            {
                byCheckBoxList.Items.Add(new ListItem(WebUI_Function.SpaceLength(length) + "├ " + rows[i]["Name"].ToString(), rows[i]["CID"].ToString()));
                this.bindChildItem(byCheckBoxList, dt, rows[i]["CID"].ToString(), length + 1);
            }
        }
        #endregion
    }
}