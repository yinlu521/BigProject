using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iNethinkCMS.Web.UI;
using iNethinkCMS.Command;

namespace iNethinkCMS.Web.admin.extend
{
    public partial class extend_adgroup : Admin_BasePage
    {
        iNethinkCMS.Model.Model_iNethinkCMS_AdGroup model = new Model.Model_iNethinkCMS_AdGroup();
        iNethinkCMS.BLL.BLL_iNethinkCMS_AdGroup bll = new BLL.BLL_iNethinkCMS_AdGroup();
        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vPage = 1;
        private int vID = 0;
        private string vSQL = "";
        private string vKeyType = "";
        private string vKey = "";
        private string vTitle = string.Empty;

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
            #region
            switch (vAct)
            {
                case "guide":

                    this.guideID.Visible = true;
                    if (vID == 0)
                    {
                        this.navInfoID.InnerText = vNavInfo + "广告位添加";
                        this.Button_Submit.Click += new EventHandler(Button_Submit_Click_Add);
                    }
                    else
                    {
                        this.navInfoID.InnerText = vNavInfo + "广告位修改";
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
                    this.navInfoID.InnerText = vNavInfo + "数据字典管理";
                    this.mainID.Visible = true;
                    if (!IsPostBack)
                    {
                        this.sKeyType.SelectedValue = vKeyType;
                        this.sKey.Text = vKey;
                    }
                    PageListInfo();
                    break;
            }
            #endregion


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

        #endregion

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path + "?skeytype=" + this.sKeyType.SelectedValue + "&skey=" + this.sKey.Text);
        }

        #region guideID 信息修改/添加
        private void Fun_SetValue(int byID)
        {
            if (byID == 0)
            {
                //this.txtDictType.SelectedValue = "0";
                this.txtTitle.Text = string.Empty;
            }
            else
            {
                model = bll.GetMode(byID);

                this.txtTitle.Text = model.Title;

            }
        }

        private bool Fun_GetValue()
        {
            if (this.txtTitle.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "请输入广告位名称!");
                return false;
            }

            vTitle = txtTitle.Text.Trim();

            return true;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("title = '" + vTitle + "'") > 0)
                {
                    MessageBox.Show(this, "该广告位【" + vTitle + "】 已存在！");
                    return;
                }
                model.Title = vTitle;
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
                if (bll.GetRecordCount("ID <> " + vID + " And title = '" + vTitle + "'") > 0)
                {
                    MessageBox.Show(this, "该广告位【"+vTitle+"】 已存在！");
                    return;
                }

                model.ID = vID;
                model.Title = vTitle;
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


    }
}