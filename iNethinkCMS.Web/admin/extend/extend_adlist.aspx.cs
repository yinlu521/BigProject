using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iNethinkCMS.Web.UI;
using iNethinkCMS.Command;

namespace iNethinkCMS.Web.admin.extend
{
    public partial class extend_adlist : Admin_BasePage
    {
        iNethinkCMS.Model.Model_iNethinkCMS_AdList model = new Model.Model_iNethinkCMS_AdList();
        iNethinkCMS.BLL.BLL_iNethinkCMS_AdList bll = new BLL.BLL_iNethinkCMS_AdList();
        iNethinkCMS.BLL.BLL_iNethinkCMS_AdGroup a_bll = new BLL.BLL_iNethinkCMS_AdGroup();
        iNethinkCMS.Model.Model_iNethinkCMS_AdGroup a_model=new  Model.Model_iNethinkCMS_AdGroup();
        private string vNavInfo = "当前位置：";
        private string vAct = "";
        private int vPage = 1;
        private int vID = 0;
        private string vSQL = "";
        private string vKeyType = "";
        private string vKey = "";
        private string vTitle=string.Empty;
        private string vIndexPic = string.Empty;
        private string vLinkUrl = string.Empty;
        private string vDesc = string.Empty;
        private int vOrderNum = 0;
        private DateTime vAddTime = DateTime.Now;
        private int vParentId = 0;
        
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
                        GetBindGroup();
                        Fun_SetValue(vID); //获取frm基本信息

                    }
                    break;

                case "delete":
                    Fun_Delete(vID);
                    break;

                default:
                    this.navInfoID.InnerText = vNavInfo + "广告列表";
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

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path + "?skeytype=" + this.sKeyType.SelectedValue + "&skey=" + this.sKey.Text);
        }

        #region guideID 信息修改/添加
        private void Fun_SetValue(int byID)
        {
            if (byID == 0)
            {
                txtLinkUrl.Text = string.Empty;
                txtOrderNum.Text = string.Empty;
                txtPicture.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtTitle.Text = string.Empty;
            }
            else
            {
                model = bll.GetModel(byID);
                txtLinkUrl.Text = model.Linkurl;
                txtTitle.Text = model.Title;
                txtRemark.Text = model.Desc;
                txtPicture.Text = model.IndexPic;
                txtOrderNum.Text = model.OrderNum.ToString();
                dropGroup.SelectedValue = model.ParentId.ToString();
            }
        }

        private bool Fun_GetValue()
        {
            if (txtTitle.Text.Length==0) { 
              MessageBox.Show(this,"请输入标题！");
              return false;
            }
            if (!Command_Validate.IsNumber(txtOrderNum.Text))
            {
                MessageBox.Show(this, "排序权重只能输入数字!");
                return false;
            }
            vTitle = txtTitle.Text.Trim();
            vIndexPic = txtPicture.Text.Trim();
            vLinkUrl = txtLinkUrl.Text.Trim();
            vOrderNum =int.Parse(txtOrderNum.Text.Trim());
            vParentId = int.Parse(dropGroup.SelectedValue);
            vDesc = txtRemark.Text.Trim();
            return true;
        }

        protected void Button_Submit_Click_Add(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("  Title='" + txtTitle.Text.Trim() + "'")>0)
                {
                    MessageBox.Show(this,"标题已存在，请重新输入！");
                    return;
                }
                model.IndexPic = vIndexPic;
                model.Linkurl = vLinkUrl;
                model.OrderNum = vOrderNum;
                model.ParentId = vParentId;
                model.Title = vTitle;
                model.Desc = vDesc;
                model.AddTime = DateTime.Now;
                bll.Add(model);
                Response.Redirect(Request.Path);
            }
        }

        protected void Button_Submit_Click_Edit(object sender, EventArgs e)
        {
            if (Fun_GetValue() == true)
            {
                if (bll.GetRecordCount("  Title='" + txtTitle.Text.Trim() + "' and id<>"+vID+"") > 0)
                {
                    MessageBox.Show(this, "标题已存在，请重新输入！");
                    return;
                }
                model.ID = vID;
                model.IndexPic = vIndexPic;

                model.Linkurl = vLinkUrl;
                model.OrderNum = vOrderNum;
                model.ParentId = vParentId;
                model.Title = vTitle;
                model.Desc = vDesc;
                bll.Update(model);
                Response.Redirect(Request.Path);
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

        protected string GetAdGroup(int Id)
        {
            string msg=string.Empty;
            iNethinkCMS.Model.Model_iNethinkCMS_AdGroup  mode= a_bll.GetMode(Id);
            if (mode != null)
                msg=  mode.Title;
            else
                msg="";
           
            return msg;
        }

        private void GetBindGroup()
        {
            dropGroup.DataSource = a_bll.GetList("");
            dropGroup.DataTextField = "Title";
            dropGroup.DataValueField = "id";
            dropGroup.DataBind();
            dropGroup.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
    }
}