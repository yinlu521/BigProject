using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iNethinkCMS.Web
{
    public partial class AddNum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.BLL_Book bll = new BLL.BLL_Book();
            Model.Book model = new Model.Book();
            model.Type = this.DropDownList1.SelectedValue;
            model.Num = float.Parse(this.num.Value);
            string player = "";
            if (this.CheckBox1.Checked)
            {
                player += this.CheckBox1.Text;
            }
            if (this.CheckBox2.Checked)
            {
                player += this.CheckBox2.Text;
            }
            model.Player = player;
            string IO = "";
            if (RadioButton1.Checked)
            {
                IO += RadioButton1.Text;
            }
            if (RadioButton2.Checked)
            {
                IO += RadioButton2.Text;
            }
            model.IO = IO;
            model.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.Remark = Textarea1.Value.Trim();
            bll.Add(model);
        }
    }
}