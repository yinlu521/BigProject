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

using iNethinkCMS.Helper;
using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.news
{

    public partial class news_column_fields : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel_CustomFields bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Channel_CustomFields();
        iNethinkCMS.Model.Model_iNethinkCMS_Channel_CustomFields model = new iNethinkCMS.Model.Model_iNethinkCMS_Channel_CustomFields();

        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel bll_channel = new iNethinkCMS.BLL.BLL_iNethinkCMS_Channel();

        private string vNavInfo = "当前位置：";
        private string vNavChannaleName = "";  //当前栏目名称
        private string vAct = "";
        private int vPage = 1;
        public int vCID = 0;
        private int vID = 0;
        private string vSQL = "";
        private string vKeyType = "";
        private string vKey = "";

        private string vCIDList;
        private string vCustomFieldsName;
        private string vCustomFieldsKey;
        private string vCustomFieldsType;
        private string vCustomFieldsValue;
        private int vCustomFieldsRequired;
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
            if (Request.QueryString["CID"] != null && Request.QueryString["CID"].Trim() != "")
            {
                if (!int.TryParse(Request.QueryString["CID"], out vCID))
                {
                    Response.Write("CID Error");
                    Response.End();
                }
            }
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {
                if (!int.TryParse(Request.QueryString["ID"], out vID))
                {
                    Response.Write("ID Error");
                    Response.End();
                }
            }

            if (vCID > 0)
            {
                vNavChannaleName = "[" + bll_channel.GetModel(vCID).Name + "] ";
                vSQL += "CharIndex('," + vCID + ",' , ',' + CAST([CIDList] AS varchar(8000)) + ',') > 0";
            }
            else
            {
                vSQL += " 1 = 1";
            }

            if (vKey.Length > 0)
            {
                if (vKeyType == "ID")
                {
                    if (iNethinkCMS.Command.Command_Validate.IsNumber(vKey) == true)
                    {
                        vSQL += " And" + vKeyType + " = " + vKey + "";
                    }
                    else
                    {
                        vKey = "";
                    }
                }
                else
                {
                    vSQL += " And" + vKeyType + " Like '%" + vKey + "%'";
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
                        this.navInfoID.InnerText = vNavInfo + vNavChannaleName + "自定义字段添加";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Add);
                    }
                    else
                    {
                        this.navInfoID.InnerText = vNavInfo + vNavChannaleName + "自定义字段修改";
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
                    this.navInfoID.InnerText = vNavInfo + vNavChannaleName + "自定义字段管理";
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
            Response.Redirect(Request.Path + "?cid=" + vCID + "&skeytype=" + this.sKeyType.SelectedValue + "&skey=" + this.sKey.Text);
        }

        #endregion

        #region guideID 信息修改/添加
        private void Fun_SetValue(int byID)
        {
            if (byID == 0)
            {
                this.txtCIDList.Text = vCID.ToString();
                this.txtCustomFieldsName.Text = "";
                this.txtCustomFieldsKey.Text = Command_StringPlus.RandomCode("maxen", 4) + Command_StringPlus.RandomCode("minien&num", 6);
                this.txtCustomFieldsType.Text = "";
                this.txtCustomFieldsValue.Text = "";
                this.txtCustomFieldsRequired.Checked = false;
                this.txtDisplay.Checked = true;
                this.txtOrderNum.Text = "0";
            }
            else
            {
                this.txtCustomFieldsKey.ReadOnly = true;

                model = bll.GetModel(byID);
                this.txtCIDList.Text = model.CIDList;
                this.txtCustomFieldsName.Text = model.CustomFieldsName;
                this.txtCustomFieldsKey.Text = model.CustomFieldsKey.Replace("myfields_", "");
                this.txtCustomFieldsType.Text = model.CustomFieldsType;
                this.txtCustomFieldsValue.Text = model.CustomFieldsValue;
                this.txtCustomFieldsRequired.Checked = model.CustomFieldsRequired == 1 ? true : false;
                this.txtDisplay.Checked = model.Display == 1 ? true : false;
                this.txtOrderNum.Text = model.OrderNum.ToString();

            }
        }

        private bool Fun_GetValue()
        {
            if (this.txtCustomFieldsName.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入字段名称!");
                return false;
            }
            if (this.txtCustomFieldsKey.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入字段标识!");
                return false;
            }
            if (this.txtCustomFieldsType.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请选择字段类型!");
                return false;
            }
            if (this.txtCIDList.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请选择字段所属栏目!");
                return false;
            }

            if (!Command_Validate.IsNumber(this.txtOrderNum.Text.Trim()))
            {
                MessageBox.Show(this, "排序权重只能输入数字!");
                return false;
            }

            vCIDList = this.txtCIDList.Text;
            vCustomFieldsName = this.txtCustomFieldsName.Text.Trim();
            vCustomFieldsKey = "myfields_" + this.txtCustomFieldsKey.Text.Trim();
            vCustomFieldsType = this.txtCustomFieldsType.Text.Trim();
            vCustomFieldsValue = this.txtCustomFieldsValue.Text.Trim();
            vCustomFieldsRequired = this.txtCustomFieldsRequired.Checked == true ? 1 : 0;
            vDisplay = this.txtDisplay.Checked == true ? 1 : 0;
            vOrderNum = int.Parse(this.txtOrderNum.Text.Trim());

            return true;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("CustomFieldsName = '" + vCustomFieldsName + "' and cast(CIDList as varchar)="+vCID+"") > 0)
                {
                    MessageBox.Show(this, "字段名称 [" + vCustomFieldsName + "] 已经存在!");
                    return;
                }
                if (bll.GetRecordCount("CustomFieldsKey = '" + vCustomFieldsKey + "'") > 0)
                {
                    MessageBox.Show(this, "字段标识 [" + vCustomFieldsKey + "] 已经存在!");
                    return;
                }

                model.CIDList = vCIDList;
                model.CustomFieldsName = vCustomFieldsName;
                model.CustomFieldsKey = vCustomFieldsKey;
                model.CustomFieldsType = vCustomFieldsType;
                model.CustomFieldsValue = vCustomFieldsValue;
                model.CustomFieldsRequired = vCustomFieldsRequired;
                model.Display = vDisplay;
                model.OrderNum = vOrderNum;
                bll.Add(model);

                Response.Redirect(Request.Path + "?cid=" + vCID);
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
                if (bll.GetRecordCount("ID <> " + vID + " And CustomFieldsName = '" + vCustomFieldsName + "'  and cast(CIDList as varchar)=" + vCID + "") > 0)
                {
                    MessageBox.Show(this, "字段名称 [" + vCustomFieldsName + "] 已经存在!");
                    return;
                }
                if (bll.GetRecordCount("ID <> " + vID + " And CustomFieldsKey = '" + vCustomFieldsKey + "'") > 0)
                {
                    MessageBox.Show(this, "字段标识 [" + vCustomFieldsKey + "] 已经存在!");
                    return;
                }

                model.ID = vID;
                model.CIDList = vCIDList;
                model.CustomFieldsName = vCustomFieldsName;
                model.CustomFieldsKey = vCustomFieldsKey;
                model.CustomFieldsType = vCustomFieldsType;
                model.CustomFieldsValue = vCustomFieldsValue;
                model.CustomFieldsRequired = vCustomFieldsRequired;
                model.Display = vDisplay;
                model.OrderNum = vOrderNum;
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

        #region 栏目CheckBox输出
        protected string BindCheckBoxList()
        {
            string tmpListInfo = "";
            DataTable dt = bll_channel.GetList(0, "", "OrderNum Desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                string vCIDListTmp = "," + this.txtCIDList.Text + ",";
                foreach (DataRow row in dt.Rows)
                {
                    if (row["FatherID"].ToString().Trim() == "0")//绑定根节点
                    {
                        tmpListInfo += "<input type=\"checkbox\" name=\"txtCheckBoxCID\" value=\"" + row["ID"].ToString() + "\" id=\"txtCheckBoxCID_" + row["ID"].ToString() + "\"";
                        if (vCIDListTmp.IndexOf("," + row["ID"].ToString() + ",") >= 0)
                        {
                            tmpListInfo += " checked";
                        }
                        tmpListInfo += " />" + row["Name"] + "<br />";
                        tmpListInfo += this.BindChildCheckBoxList(dt, row["CID"].ToString(), 1);
                    }
                }
            }
            return tmpListInfo;
        }

        string BindChildCheckBoxList(DataTable dt, string id, int length)
        {
            string tmpListInfo = "";
            DataRow[] rows = dt.Select("FatherID=" + id + "", "OrderNum Desc");

            string vCIDListTmp = "," + this.txtCIDList.Text + ",";
            for (int i = 0; i < rows.Length; i++)
            {
                tmpListInfo += WebUI_Function.SpaceLength(length) + "|&#8211;&nbsp;<input type=\"checkbox\" name=\"txtCheckBoxCID\" value=\"" + rows[i]["ID"].ToString() + "\" id=\"txtCheckBoxCID_" + rows[i]["ID"].ToString() + "\"";
                if (vCIDListTmp.IndexOf("," + rows[i]["ID"].ToString() + ",") >= 0)
                {
                    tmpListInfo += " checked";
                }
                tmpListInfo += " />" + rows[i]["Name"] + "<br />";
                tmpListInfo += this.BindChildCheckBoxList(dt, rows[i]["CID"].ToString(), length + 1);

            }
            return tmpListInfo;

        }
        #endregion

        protected string Fun_Switch_CustomFieldsType(object byFieldsType)
        {
            string vBackInfo = "";
            switch (byFieldsType.ToString())
            {
                case "my_textfield":
                    vBackInfo = "文本域";
                    break;
                case "my_textarea":
                    vBackInfo = "文本区域";
                    break;
                case "my_richtextarea":
                    vBackInfo = "富文本区域";
                    break;
                case "my_checkbox":
                    vBackInfo = "复选框";
                    break;
                case "my_radio":
                    vBackInfo = "单选按钮";
                    break;
                case "my_select":
                    vBackInfo = "选择（列表菜单）";
                    break;
                case "my_file":
                    vBackInfo = "文件域";
                    break;
                default:
                    vBackInfo = "-";
                    break;
            }
            return vBackInfo;
        }
    }
}