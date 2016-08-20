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
    public partial class news_column : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Channel();
        iNethinkCMS.Model.Model_iNethinkCMS_Channel model = new iNethinkCMS.Model.Model_iNethinkCMS_Channel();
        iNethinkCMS.BLL.BLL_iNethinkCMS_Content bll_content = new iNethinkCMS.BLL.BLL_iNethinkCMS_Content();
        iNethinkCMS.BLL.BLL_iNethinkCMS_Upload bll_upload = new iNethinkCMS.BLL.BLL_iNethinkCMS_Upload();

        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vParentID;

        private int vID;
        private int vMold;
        //private int vCid;
        private int vFatherID;
        private string vChildID;
        private int vDeepPath;
        private string vName;
        private string vDomain;
        private int vOutSideLink;
        private string vTemplatechannel;
        private string vTemplateclass;
        private string vTemplateview;
        private string vPicture;
        private string vContents;
        private string vKeywords;
        private string vDescription;
        private int vDisplay;
        private int vOrderNum;
        private string vEname;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("b");

            #region
            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {
                if (!int.TryParse(Request.QueryString["ID"], out vID))
                {
                    Response.Write("ID Error");
                    Response.End();
                }
            }

            if (!int.TryParse(Request.QueryString["ParentID"], out vParentID))
            {
                vParentID = 0;
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
                        this.navInfoID.InnerText = vNavInfo + "网站栏目添加";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Add);
                    }
                    else
                    {
                        this.navInfoID.InnerText = vNavInfo + "网站栏目修改";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Edit);
                    }
                    if (!IsPostBack)
                    {
                        Fun_SetValue(vID); //获取frm基本信息
                    }
                    break;

                case "delete":
                    Fun_Delete(vID);
                    break;

                default:
                    this.navInfoID.InnerText = vNavInfo + "网站栏目管理";
                    this.mainID.Visible = true;
                    BindDateList();
                    break;
            }
        }

        #region List
        protected string BindDateList()
        {
            string tmpListInfo = "";
            DataTable dt = bll.GetList(0, "", "OrderNum Desc,ID Asc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["FatherID"].ToString().Trim() == "0")//绑定根节点
                    {
                        tmpListInfo += "<tr class=\"classp\">";
                        tmpListInfo += "<td>";
                        tmpListInfo += row["ID"].ToString();
                        tmpListInfo += "</td>";
                        tmpListInfo += "<td class=\"tdleft\">";
                        tmpListInfo += "<span class=\"classico2\"></span>" + row["Name"];
                        tmpListInfo += "</td>";
                        tmpListInfo += "<td>";
                        tmpListInfo += row["Mold"].ToString() == "1" ? "新闻列表" : "单页显示";
                        tmpListInfo += "</td>";
                        tmpListInfo += "<td>";
                        tmpListInfo += row["Display"].ToString() == "0" ? "不显示" : "显示";
                        tmpListInfo += "</td>";
                        tmpListInfo += "<td>";
                        tmpListInfo += row["OrderNum"].ToString();
                        tmpListInfo += "</td>";
                        tmpListInfo += "<td>";
                        tmpListInfo += "<a href=\"news_column_fields.aspx?cid=" + row["ID"].ToString() + "\">内容字段</a>&nbsp;&nbsp;";
                        tmpListInfo += "<a href=\"?act=guide&parentid=" + row["ID"].ToString() + "\">添加</a>&nbsp;&nbsp;";
                        tmpListInfo += "<a href=\"?act=guide&id=" + row["ID"].ToString() + "\">编辑</a>&nbsp;&nbsp;";
                        tmpListInfo += "<a href=\"javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&id=" + row["ID"].ToString() + "';}\">删除</a>";
                        tmpListInfo += "</td>";
                        tmpListInfo += "</tr>";
                        tmpListInfo += this.BindChildDateList(dt, row["CID"].ToString(), 1);
                    }
                }
            }
            return tmpListInfo;
        }

        string BindChildDateList(DataTable dt, string id, int length)
        {
            string tmpListInfo = "";
            DataRow[] rows = dt.Select("FatherID=" + id + "", "OrderNum Desc,ID Asc");
            for (int i = 0; i < rows.Length; i++)
            {
                tmpListInfo += "<tr>";
                tmpListInfo += "<td>";
                tmpListInfo += rows[i]["ID"].ToString();
                tmpListInfo += "</td>";
                tmpListInfo += "<td class=\"tdleft\">";
                tmpListInfo += WebUI_Function.SpaceLength(length) + "<span style=\"color:#CCC\">|&#8211;</span>&nbsp;" + rows[i]["Name"];
                tmpListInfo += "</td>";
                tmpListInfo += "<td>";
                tmpListInfo += rows[i]["Mold"].ToString() == "1" ? "新闻列表" : "单页显示";
                tmpListInfo += "</td>";
                tmpListInfo += "<td>";
                tmpListInfo += rows[i]["Display"].ToString() == "0" ? "不显示" : "显示";
                tmpListInfo += "</td>";
                tmpListInfo += "<td>";
                tmpListInfo += rows[i]["OrderNum"].ToString();
                tmpListInfo += "</td>";
                tmpListInfo += "<td>";
                tmpListInfo += "<a href=\"news_column_fields.aspx?cid=" + rows[i]["ID"].ToString() + "\">内容字段</a>&nbsp;&nbsp;";
                tmpListInfo += "<a href=\"?act=guide&parentid=" + rows[i]["ID"].ToString() + "\">添加</a>&nbsp;&nbsp;";
                tmpListInfo += "<a href=\"?act=guide&id=" + rows[i]["ID"].ToString() + "\">编辑</a>&nbsp;&nbsp;";
                tmpListInfo += "<a href=\"javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&id=" + rows[i]["ID"].ToString() + "';}\">删除</a>";
                tmpListInfo += "</td>";
                tmpListInfo += "</tr>";

                tmpListInfo += this.BindChildDateList(dt, rows[i]["CID"].ToString(), length + 1);

            }
            return tmpListInfo;

        }
        #endregion

        #region guideID 信息修改/添加
        private void Fun_SetValue(int byID)
        {
            BindDropdownList();
            if (byID == 0)
            {
                this.txtFatherID.Text = vParentID.ToString();
                this.txtChildID.Value = "0";
                this.txtDeepPath.Value = "0";
                this.txtMold.SelectedValue = "1";
                this.txtName.Text = "";
                this.txtPicture.Text = "";
                this.txtContents.Text = "";
                this.txtOutSideLink.Text = "0";
                this.txtDomain.Text = "";
                this.txtTemplatechannel.Text = "channel.html";
                this.txtTemplateclass.Text = "list.html";
                this.txtTemplateview.Text = "article.html";
                this.txtKeywords.Text = "";
                this.txtDescription.Text = "";
                this.txtDisplay.Checked = true;
                this.txtOrderNum.Text = "0";
                this.txtEname.Text = string.Empty;
            }
            else
            {
                model = bll.GetModel(byID);
                this.txtFatherID.Text = model.FatherID.ToString();
                this.txtChildID.Value = model.ChildID;
                this.txtDeepPath.Value = model.DeepPath.ToString();
                this.txtMold.SelectedValue = model.Mold.ToString();
                this.txtName.Text = model.Name;
                this.txtPicture.Text = model.Picture;
                this.txtContents.Text = model.Contents;
                this.txtOutSideLink.Text = model.OutSideLink.ToString();
                this.txtDomain.Text = model.Domain;
                this.txtTemplatechannel.Text = model.Templatechannel;
                this.txtTemplateclass.Text = model.Templateclass;
                this.txtTemplateview.Text = model.Templateview;
                this.txtKeywords.Text = model.Keywords;
                this.txtDescription.Text = model.Description;
                this.txtDisplay.Checked = model.Display == 1 ? true : false;
                this.txtOrderNum.Text = model.OrderNum.ToString();
                this.txtEname.Text = model.Ename;
            }
        }

        private bool Fun_GetValue()
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入栏目名称!");
                return false;
            }
            if (this.txtTemplatechannel.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入大类模板路径!");
                return false;
            }
            if (this.txtTemplateclass.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入小类模板路径!");
                return false;
            }
            if (this.txtTemplateview.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入内容页模板路径!");
                return false;
            }
            if (!Command_Validate.IsNumber(this.txtOrderNum.Text.Trim()))
            {
                MessageBox.Show(this, "排序权重只能输入数字!");
                return false;
            }

            vFatherID = int.Parse(this.txtFatherID.Text.Trim());
            vChildID = this.txtChildID.Value.Trim();
            vDeepPath = int.Parse(this.txtDeepPath.Value.Trim());
            vMold = int.Parse(this.txtMold.SelectedValue);
            vName = this.txtName.Text.Trim();
            vPicture = this.txtPicture.Text.Trim();
            vContents = this.txtContents.Text;
            vOrderNum = int.Parse(this.txtOrderNum.Text.Trim());
            vDomain = this.txtDomain.Text.Trim();
            vOutSideLink = int.Parse(this.txtOutSideLink.Text.Trim());
            vTemplatechannel = this.txtTemplatechannel.Text.Trim();
            vTemplateclass = this.txtTemplateclass.Text.Trim();
            vTemplateview = this.txtTemplateview.Text.Trim();
            vKeywords = Command_StringPlus.LostHTML(this.txtKeywords.Text.Trim());
            vDescription = Command_StringPlus.LostHTML(this.txtDescription.Text.Trim());
            vDisplay = this.txtDisplay.Checked == true ? 1 : 0;
            vEname = this.txtEname.Text.Trim();
            return true;
        }

        private void Fun_ReloadChildID()
        {
            string Tmp;
            int vMaxDeepPath;
            string tmpChildID;
            string tmpChildIDs;

            object obj = SQLHelper.GetSingle("Select Top 1 [DeepPath] From [iNethinkCMS_Channel] Order By [DeepPath] Desc");
            if (obj == null)
            {
                vMaxDeepPath = 0;
            }
            else
            {
                vMaxDeepPath = Convert.ToInt32(obj);
            }

            SqlDataReader sqlRead = SQLHelper.ExecuteReader("Select [ID],[ChildID],[ChildIDs],[DeepPath] From [iNethinkCMS_Channel]");
            while (sqlRead.Read())
            {
                if (Convert.ToInt32(sqlRead["DeepPath"]) == vMaxDeepPath) //最后一层没有子栏目
                {
                    tmpChildID = "";
                    tmpChildIDs = sqlRead["ID"].ToString();
                }
                else
                {
                    Tmp = Fun_GetChildID(Convert.ToInt32(sqlRead["ID"]), 0);  //找出所有一级子栏目
                    if (vMaxDeepPath - 1 == Convert.ToInt32(sqlRead["DeepPath"])) //最后第二层的一级子栏目和所有子栏目相同
                    {
                        tmpChildID = Tmp;
                        if (Tmp.Length == 0)
                        {
                            Tmp = sqlRead["ID"].ToString();
                        }
                        else
                        {
                            Tmp = sqlRead["ID"].ToString() + "," + Tmp;
                        }
                        tmpChildIDs = Tmp;
                    }
                    else
                    {
                        tmpChildID = Tmp;
                        if (Tmp.Length == 0)
                        {
                            Tmp = sqlRead["ID"].ToString();
                        }
                        else
                        {
                            Tmp = sqlRead["ID"].ToString() + "," + Fun_GetChildID(Convert.ToInt32(sqlRead["ID"]), 1);
                        }
                        tmpChildIDs = Tmp;
                    }
                }
                SQLHelper.ExecuteSql("Update [iNethinkCMS_Channel] Set [ChildID]='" + tmpChildID + "',[ChildIDs]='" + tmpChildIDs + "' Where [ID] = " + Convert.ToInt32(sqlRead["ID"]));
            }
            sqlRead.Close();
        }

        private string Fun_GetChildID(int byID, int byDeep)
        {
            string tmpGetChildID = "";
            SqlDataReader sqlRead = SQLHelper.ExecuteReader("Select [ID] From [iNethinkCMS_Channel] Where [FatherID]=" + byID + " Order By [OrderNum] Desc,ID Asc");
            while (sqlRead.Read())
            {
                if (byDeep == 0)
                {
                    tmpGetChildID = tmpGetChildID + "," + sqlRead["ID"].ToString(); // 仅下级栏目
                }
                else
                {
                    tmpGetChildID = tmpGetChildID + "," + sqlRead["ID"].ToString() + "," + Fun_GetChildID(Convert.ToInt32(sqlRead["ID"]), 1); // 仅下级栏目
                }
            }
            sqlRead.Close();
            tmpGetChildID = tmpGetChildID.Replace(",,", ",");
            tmpGetChildID = Command_StringPlus.Right(tmpGetChildID, 1) == "," ? Command_StringPlus.Left(tmpGetChildID, tmpGetChildID.Length - 1) : tmpGetChildID;
            tmpGetChildID = Command_StringPlus.Left(tmpGetChildID, 1) == "," ? Command_StringPlus.Right(tmpGetChildID, tmpGetChildID.Length - 1) : tmpGetChildID;
            return tmpGetChildID;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (vDomain.Length > 0)
                {
                    if (bll.GetRecordCount("Domain = '" + vDomain + "'") > 0)
                    {
                        MessageBox.Show(this, "绑定域名 [" + vDomain + "] 已经存在!");
                        return;
                    }
                }

                //计算栏目深度
                if (vFatherID > 0)
                {
                    vDeepPath = bll.GetDeepPath(vFatherID) + 1;
                }
                else
                {
                    vDeepPath = 0;
                }

                model.FatherID = vFatherID;
                model.ChildID = "";
                model.ChildIDs = "";
                model.DeepPath = vDeepPath;
                model.Mold = vMold;
                model.Name = vName;
                model.Picture = vPicture;
                model.Contents = vContents;
                model.OrderNum = vOrderNum;
                model.Domain = vDomain;
                model.OutSideLink = vOutSideLink;
                model.Templatechannel = vTemplatechannel;
                model.Templateclass = vTemplateclass;
                model.Templateview = vTemplateview;
                model.Keywords = vKeywords;
                model.Description = vDescription;
                model.Display = vDisplay;
                model.Ename = vEname;
                bll.Add(model);

                SQLHelper.ExecuteSql("Update [iNethinkCMS_Channel] Set [Cid]=[ID]");  //更新CID
                Fun_ReloadChildID();

                int Aid = bll.GetMaxID();
                bll_upload.UpdateUploadFile_One(vPicture, 5, Aid, 0);
                bll_upload.UpdateUploadFile(vContents, 5, Aid, 0);
                Response.Redirect(Request.Path);

            }
        }

        protected void Button_Submit_Click_Edit(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (vDomain.Length > 0)
                {
                    if (bll.GetRecordCount("Domain = '" + vDomain + "' And ID <> " + vID) > 0)
                    {
                        MessageBox.Show(this, "绑定域名 [" + vDomain + "] 已经存在!");
                        return;
                    }
                }

                //计算栏目深度
                if (vFatherID > 0)
                {
                    vDeepPath = bll.GetDeepPath(vFatherID) + 1;
                }
                else
                {
                    vDeepPath = 0;
                }

                string vChilds;
                int ChildDeepPath;
                DataTable dt = bll.GetList("[ID] = " + vID).Tables[0];
                if (dt.Rows[0]["ChildIDs"].ToString().Length - vID + ",".ToString().Length > 0)
                {
                    vChilds = Command_StringPlus.Right(dt.Rows[0]["ChildIDs"].ToString(), dt.Rows[0]["ChildIDs"].ToString().Length - vID + ",".ToString().Length);
                    if (vChilds.Length > 0)
                    {
                        ChildDeepPath = Convert.ToInt32(dt.Rows[0]["DeepPath"]) - vDeepPath;
                        SQLHelper.ExecuteSql("Update [iNethinkCMS_Channel] Set [DeepPath] = [DeepPath] - " + ChildDeepPath + " Where [ID] In (" + dt.Rows[0]["ChildIDs"].ToString() + ")");
                    }
                }

                model.ID = vID;
                model.FatherID = vFatherID;
                //model.ChildID = "";
                //model.ChildIDs = "";
                model.DeepPath = vDeepPath;
                model.Mold = vMold;
                model.Name = vName;
                model.Picture = vPicture;
                model.Contents = vContents;
                model.OrderNum = vOrderNum;
                model.Domain = vDomain;
                model.OutSideLink = vOutSideLink;
                model.Templatechannel = vTemplatechannel;
                model.Templateclass = vTemplateclass;
                model.Templateview = vTemplateview;
                model.Keywords = vKeywords;
                model.Description = vDescription;
                model.Display = vDisplay;
                model.Ename = vEname;
                bll.Update(model);

                SQLHelper.ExecuteSql("Update [iNethinkCMS_Channel] Set [Cid]=[ID]");  //更新CID
                Fun_ReloadChildID();

                bll_upload.UpdateUploadFile_Reset(5, vID, 0);
                bll_upload.UpdateUploadFile_One(vPicture, 5, vID, 0);
                bll_upload.UpdateUploadFile(vContents, 5, vID, 0);
                Response.Redirect(Request.Path);

            }
        }
        #endregion

        #region Delete 数据删除
        protected void Fun_Delete(int byID)
        {
            model = bll.GetModel(byID);
            Helper.SQLHelper.ExecuteSql("update [iNethinkCMS_Upload] set [UpType]=0 ,[Aid]=0 ,[Cid]=0 Where [Cid] in (" + model.ChildIDs + ") And [UpType]=1"); //重置上传表中的数据
            bll_upload.UpdateUploadFile_Reset(5, byID, 0);  //重置上传表中的数据

            bll_content.DeleteList_ByCID(model.ChildIDs);//删除相应文章信息
            bll.DeleteList(model.ChildIDs);   //删除栏目信息（含下属栏目）
            Fun_ReloadChildID();    //重建栏目信息

            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        #endregion

        #region 栏目下拉菜单无限极分类
        /// <summary>
        /// 绑定DropDownList;
        /// </summary>
        protected void BindDropdownList()
        {
            DataTable dt = bll.GetList(0, "", "OrderNum Desc,ID Asc").Tables[0];
            this.txtFatherID.Items.Insert(0, new ListItem("设为顶级栏目", "0"));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["FatherID"].ToString().Trim() == "0")//绑定根节点
                    {
                        if (vID != Convert.ToInt32(row["CID"])) //不能指定本类以级本类以下的栏目为上级分类
                        {
                            this.txtFatherID.Items.Add(new ListItem(row["Name"].ToString(), row["CID"].ToString()));
                            this.bindChildItem(this.txtFatherID, dt, row["CID"].ToString(), 1);
                        }
                    }
                }
            }
        }

        void bindChildItem(DropDownList d, DataTable dt, string id, int length)
        {
            DataRow[] rows = dt.Select("FatherID=" + id + "", "OrderNum Desc,ID Asc");
            for (int i = 0; i < rows.Length; i++)
            {
                if (vID != Convert.ToInt32(rows[i]["CID"])) //不能指定本类以级本类以下的栏目为上级分类
                {
                    this.txtFatherID.Items.Add(new ListItem(WebUI_Function.SpaceLength(length) + "├ " + rows[i]["Name"].ToString(), rows[i]["CID"].ToString()));
                    this.bindChildItem(d, dt, rows[i]["CID"].ToString(), length + 1);
                }
            }
        }

        #endregion

    }
}