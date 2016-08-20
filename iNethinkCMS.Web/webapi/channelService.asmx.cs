using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace iNethinkCMS.Web.webapi
{
    /// <summary>
    /// channelService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class channelService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
       
    }
}
