using iNethinkCMS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iNethinkCMSWebApi
{
    public partial class IndexService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = Request.QueryString["act"];
            switch (str) { 
                case "getBanner":
                    getBanner();
                    break;
            }
        }
        private void getBanner() {
            string token = Request.QueryString["token"];
            int groupId=int.Parse(Request.QueryString["gid"]);
            BLL_iNethinkCMS_AdList bll = new BLL_iNethinkCMS_AdList();
            string sqlwhere = "";
            DataSet ds = new DataSet();
            ds=bll.GetList("");
            string a=ds.Tables[0].Rows[1][""].ToString();
            
        }
    }
}