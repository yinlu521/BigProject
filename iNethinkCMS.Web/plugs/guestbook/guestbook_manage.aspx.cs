using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.plugs.guestbook
{
    public partial class guestbook_manage : Admin_BasePage
    {
        private string vAct = "";
        private int vPage = 1;
        private string vSQL = "";

        BLL.BLL_iNethinkCMS_Plugs_Guestbook bll = new BLL.BLL_iNethinkCMS_Plugs_Guestbook();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("a");
            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            if (Request.QueryString["Page"] != null && Request.QueryString["Page"].Trim() != "")
            {
                if (!int.TryParse(Request.QueryString["Page"], out vPage))
                {
                    vPage = 1;
                }
            }

            if (!IsPostBack)
            {
                PageListInfo();
            }
        }
        protected void PageListInfo()
        {
            int vPageSize = int.Parse(siteConfig.PageListNum);
            int vRecordCount = bll.GetRecordCount(vSQL);

            rptList.DataSource = bll.GetListByPage(vSQL, "ID Desc", (vPage - 1) * vPageSize, vPage * vPageSize);
            rptList.DataBind();

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