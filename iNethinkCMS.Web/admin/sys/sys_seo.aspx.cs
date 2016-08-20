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

using iNethinkCMS.Command;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.admin.sys
{
    public partial class seo_settings : Admin_BasePage
    {
        iNethinkCMS.Model.Model_Config model = new iNethinkCMS.Model.Model_Config();
        iNethinkCMS.BLL.BLL_Config bll = new iNethinkCMS.BLL.BLL_Config();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("e");

            if (!IsPostBack)
            {
                model = bll.GetModel_SeoConfig();
                this.txtAutoDescription.SelectedValue = model.AutoDescription;
                this.txtSeoTitle.Text = model.SeoTitle;
                this.txtIndexKeywords.Text = model.IndexKeywords;
                this.txtIndexDescription.Text = model.IndexDescription;
            }
        }

        protected void Submit_Seo_Setting_Click(object sender, EventArgs e)
        {
            model.AutoDescription = this.txtAutoDescription.SelectedValue.Trim();
            model.SeoTitle = this.txtSeoTitle.Text.Trim();
            model.IndexKeywords = Command.Command_StringPlus.LostHTML(this.txtIndexKeywords.Text.Trim());
            model.IndexDescription = Command.Command_StringPlus.LostHTML(this.txtIndexDescription.Text.Trim());

            if (bll.Update_SeoConfig(model))
            {
                MessageBox.Show(this, "SEO优化保存成功!");
            }
            else
            {
                MessageBox.Show(this, "SEO优化保存失败!");
            }
        }
    }
}