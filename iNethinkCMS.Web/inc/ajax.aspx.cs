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
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.inc
{
    public partial class ajax : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel_CustomFields bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Channel_CustomFields();
        iNethinkCMS.Model.Model_iNethinkCMS_Channel_CustomFields model = new iNethinkCMS.Model.Model_iNethinkCMS_Channel_CustomFields();

        iNethinkCMS.BLL.BLL_iNethinkCMS_Content bll_content = new iNethinkCMS.BLL.BLL_iNethinkCMS_Content();

        private string vAct = "";
        private int vCID = 0;
        private int vID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("login");

            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            if (vAct.Length == 0)
            {
                Response.Write("");
                Response.End();
            }

            switch (vAct)
            {
                case "getcustomfields":
                    #region
                    string vSQL_CustomFields = "";

                    if (Request.QueryString["CID"] != null && Request.QueryString["CID"].Trim() != "")
                    {
                        if (!int.TryParse(Request.QueryString["CID"], out vCID))
                        {
                            Response.Write("");
                            Response.End();
                        }
                    }

                    if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
                    {
                        if (!int.TryParse(Request.QueryString["ID"], out vID))
                        {
                            Response.Write("");
                            Response.End();
                        }
                    }

                    vSQL_CustomFields += "[Display] =  1 And CharIndex('," + vCID + ",' , ',' + CAST([CIDList] AS varchar(8000)) + ',') > 0";


                    //读取内容中的字段填写信息
                    string vFieldsInfo = "";
                    if (vID > 0)
                    {
                        vFieldsInfo = bll_content.GetModel(vID).FieldsInfo;
                    }
                    if (String.IsNullOrEmpty(vFieldsInfo))
                    {
                        vFieldsInfo = "";
                    }

                    //读取自定义字段信息
                    string tmpInfoBase = "<dl{0}><dt>{1}：</dt>{2}</dl>";
                    string tmpInfo = "";
                    DataTable dt = bll.GetList(0, vSQL_CustomFields, "OrderNum Desc,ID Desc").Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string vStyle = "";
                        if (i == 0) { vStyle = " style=\"border: 0;\""; }

                        string vCustomFieldsTypeInfo = "";
                        string vCustomFieldsName = dt.Rows[i]["CustomFieldsName"].ToString();
                        string vCustomFieldsKey = dt.Rows[i]["CustomFieldsKey"].ToString();
                        string vCustomFieldsType = dt.Rows[i]["CustomFieldsType"].ToString();
                        string vCustomFieldsValue = dt.Rows[i]["CustomFieldsValue"].ToString();

                        string vFieldsInfoTMP = vFieldsInfo.Length > 0 ? UI.WebUI_Function.Fun_GetFieldsInfo(vCustomFieldsKey, vFieldsInfo) : "";

                        string[] vCustomFieldsValueArr;
                        string[] vCustomFieldsValueArr_ChildArr;
                        switch (vCustomFieldsType)
                        {
                            case "my_textfield":
                                if (vID > 0)
                                {
                                    vCustomFieldsValue = vFieldsInfoTMP;
                                }
                                vCustomFieldsTypeInfo = "<input type=\"text\" name=\"" + vCustomFieldsKey + "\" id=\"" + vCustomFieldsKey + "\" value=\"" + vCustomFieldsValue + "\" class=\"int\" style=\"width:250px;\" />";
                                vCustomFieldsTypeInfo = "<dd>" + vCustomFieldsTypeInfo + "</dd>";
                                break;

                            case "my_datetext":
                                if (vID > 0)
                                {
                                    vCustomFieldsValue = vFieldsInfoTMP;
                                }
                                vCustomFieldsTypeInfo = "<input type=\"text\" name=\"" + vCustomFieldsKey + "\" id=\"" + vCustomFieldsKey + "\" value=\"" + vCustomFieldsValue + "\" class=\"int\" style=\"width:250px;\" onfocus=\"WdatePicker({startDate:this.value,dateFmt:'yyyy/M/d H:mm:ss',isShowClear:false,errDealMode:1,autoPickDate:true,readOnly:true})\" />";
                                vCustomFieldsTypeInfo = "<dd>" + vCustomFieldsTypeInfo + "</dd>";
                                break;
                            case "my_textarea":
                                if (vID > 0)
                                {
                                    vCustomFieldsValue = vFieldsInfoTMP;
                                }
                                vCustomFieldsTypeInfo = "<textarea name=\"" + vCustomFieldsKey + "\" id=\"" + vCustomFieldsKey + "\" rows=\"6\" class=\"int\" style=\"width:500px;\">" + vCustomFieldsValue + "</textarea>";
                                vCustomFieldsTypeInfo = "<dd>" + vCustomFieldsTypeInfo + "</dd>";
                                break;

                            case "my_richtextarea":
                                if (vID > 0)
                                {
                                    vCustomFieldsValue = vFieldsInfoTMP;
                                }
                                vCustomFieldsTypeInfo = "<textarea name=\"" + vCustomFieldsKey + "\" id=\"" + vCustomFieldsKey + "\" rows=\"8\">" + vCustomFieldsValue + "</textarea>";
                                vCustomFieldsTypeInfo += "<script type=\"text/javascript\">initEditor('" + vCustomFieldsKey + "');</script>";
                                vCustomFieldsTypeInfo = "<dd style=\"line-height: 0; width: 89%\">" + vCustomFieldsTypeInfo + "</dd>";
                                break;

                            case "my_checkbox":
                                vCustomFieldsValueArr = vCustomFieldsValue.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int ii = 0; ii < vCustomFieldsValueArr.Length; ii++)
                                {
                                    vCustomFieldsValueArr_ChildArr = vCustomFieldsValueArr[ii].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (vCustomFieldsValueArr_ChildArr.Length > 1)
                                    {
                                        vFieldsInfoTMP = "," + vFieldsInfoTMP + ",";
                                        string vCustomFieldsValueArr_ChildArrTMP = "," + vCustomFieldsValueArr_ChildArr[1] + ",";
                                        string vChecked = vFieldsInfoTMP.IndexOf(vCustomFieldsValueArr_ChildArrTMP) > -1 ? " checked=\"checked\"" : "";
                                        vCustomFieldsTypeInfo += "<input name=\"" + vCustomFieldsKey + "\" type=\"checkbox\" id=\"" + vCustomFieldsKey + "\" value=\"" + vCustomFieldsValueArr_ChildArr[1] + "\"" + vChecked + ">" + vCustomFieldsValueArr_ChildArr[0] + "&nbsp;&nbsp;";
                                    }
                                }
                                vCustomFieldsTypeInfo = "<dd>" + vCustomFieldsTypeInfo + "</dd>";
                                break;

                            case "my_radio":
                                vCustomFieldsValueArr = vCustomFieldsValue.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int ii = 0; ii < vCustomFieldsValueArr.Length; ii++)
                                {
                                    vCustomFieldsValueArr_ChildArr = vCustomFieldsValueArr[ii].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (vCustomFieldsValueArr_ChildArr.Length > 1)
                                    {
                                        string vChecked = vFieldsInfoTMP == vCustomFieldsValueArr_ChildArr[1] ? " checked=\"checked\"" : "";
                                        vCustomFieldsTypeInfo += "<input name=\"" + vCustomFieldsKey + "\" type=\"radio\" id=\"" + vCustomFieldsKey + "\" value=\"" + vCustomFieldsValueArr_ChildArr[1] + "\"" + vChecked + ">" + vCustomFieldsValueArr_ChildArr[0] + "&nbsp;&nbsp;";
                                    }
                                }
                                vCustomFieldsTypeInfo = "<dd>" + vCustomFieldsTypeInfo + "</dd>";
                                break;

                            case "my_select":
                                vCustomFieldsTypeInfo = "<select name=\"" + vCustomFieldsKey + "\" id=\"" + vCustomFieldsKey + "\">";
                                vCustomFieldsValueArr = vCustomFieldsValue.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int ii = 0; ii < vCustomFieldsValueArr.Length; ii++)
                                {
                                    vCustomFieldsValueArr_ChildArr = vCustomFieldsValueArr[ii].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (vCustomFieldsValueArr_ChildArr.Length > 1)
                                    {
                                        string vSelected = vFieldsInfoTMP == vCustomFieldsValueArr_ChildArr[1] ? " selected" : "";
                                        vCustomFieldsTypeInfo += "<option value=\"" + vCustomFieldsValueArr_ChildArr[1] + "\"" + vSelected + ">" + vCustomFieldsValueArr_ChildArr[0] + "</option>";
                                    }
                                }
                                vCustomFieldsTypeInfo += "</select>";
                                vCustomFieldsTypeInfo = "<dd>" + vCustomFieldsTypeInfo + "</dd>";
                                break;

                            case "my_file":
                                if (vID > 0)
                                {
                                    vCustomFieldsValue = vFieldsInfoTMP;
                                }

                                vCustomFieldsTypeInfo = "<dd>";
                                vCustomFieldsTypeInfo += "<input type=\"text\" name=\"" + vCustomFieldsKey + "\" id=\"" + vCustomFieldsKey + "\" value=\"" + vCustomFieldsValue + "\" class=\"int\" style=\"width:250px;\" />";
                                vCustomFieldsTypeInfo += "</dd>";
                                vCustomFieldsTypeInfo += "<dd>";
                                vCustomFieldsTypeInfo += "<iframe src=\"/inc/upload_base.aspx?rname=" + vCustomFieldsKey + "\" scrolling=\"no\" frameborder=\"0\"height=\"25px\"></iframe>";
                                vCustomFieldsTypeInfo += "</dd>";
                                break;

                        }

                        tmpInfo += String.Format(tmpInfoBase, vStyle, vCustomFieldsName, vCustomFieldsTypeInfo);
                        if (vCustomFieldsType == "my_file")
                        {
                            tmpInfo += "<dl style=\"border: none; display: none;\" id=\"iUpInfo_" + vCustomFieldsKey + "\"><dt>&nbsp;</dt><dd id=\"iUpInfo_msg_" + vCustomFieldsKey + "\"></dd></dl>";
                        }
                    }

                    Response.Write(tmpInfo);

                    #endregion
                    break;

                case "checktitle":
                    #region
                    string vTitle = Request.QueryString["Title"];

                    if (String.IsNullOrEmpty(vTitle))
                    {
                        Response.Write("");
                        Response.End();
                    }

                    if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
                    {
                        if (!int.TryParse(Request.QueryString["ID"], out vID))
                        {
                            Response.Write("");
                            Response.End();
                        }
                    }

                    if (bll_content.GetRecordCount("Title = '" + vTitle + "' And ID <> " + vID) > 0)
                    {
                        Response.Write("标题名称 [" + vTitle + "] 已经存在!");
                    }
                    else
                    {
                        Response.Write("");
                    }
                    #endregion
                    break;

            }

        }
    }
}