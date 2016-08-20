using iNethinkCMS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;

namespace iNethinkCMS.Web.webapi
{
    /// <summary>
    /// bannerService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    //若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
    [System.Web.Script.Services.ScriptService]
    public class bannerService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string GetIndexBanner(string token) {
            BLL_iNethinkCMS_AdList bll = new BLL.BLL_iNethinkCMS_AdList();
            DataSet ds=bll.GetList(" 1=1 order by orderNum desc");
            //ds.Tables[0].Rows;
            string json=Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return json;
        }
    }
}
