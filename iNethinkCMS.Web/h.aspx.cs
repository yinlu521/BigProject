using iNethinkCMS.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iNethinkCMS.Web
{
    public partial class h : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack){
                writelog("===========" + Command_Function.GetUserIp() + "=============");
                writelog(DateTime.Now.ToString());
                writelog("latitude:"+Request.Cookies["latitude"].Value);
                writelog("longitude:"+Request.Cookies["longitude"].Value);
            }

        }

        private void writelog(string value) {
            if (!File.Exists("C:\\TestTxt.txt"))
            {
                FileStream fs1 = new FileStream("C:\\TestTxt.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(value);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream("C:\\TestTxt.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(value);//开始写入值
                sr.Close();
                fs.Close();
            }
        }
      
}
}