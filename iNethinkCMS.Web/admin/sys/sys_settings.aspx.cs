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

using System.Xml;

namespace iNethinkCMS.Web.admin
{
    public partial class sys_settings : Admin_BasePage
    {
        iNethinkCMS.Model.Model_Config model = new iNethinkCMS.Model.Model_Config();
        iNethinkCMS.BLL.BLL_Config bll = new iNethinkCMS.BLL.BLL_Config();

        private string vNavInfo = "当前位置：";
        private string vAct = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("e");

            vAct = Request.QueryString["Act"] != null ? Request.QueryString["Act"] : "";
            this.mainID.Visible = false;
            this.copyrightID.Visible = false;

            switch (vAct)
            {
                case "copyright":
                    this.navInfoID.InnerText = vNavInfo + "版权声明";
                    this.copyrightID.Visible = true;

                    #region
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("            <ul class=\"switchs\">");
                    sb.Append("                <li id=\"s101\">许可协议</li>");
                    sb.Append("                <li id=\"s102\" class=\"c\">捐赠</li>");
                    sb.Append("            </ul>");
                    sb.Append("            <div class=\"info\">");
                    sb.Append("                <div id=\"infos101\">");
                    sb.Append("                    <dl style=\"border: 0; line-height: 28px;\">");
                    sb.Append("                        <dd style=\"padding-left: 20px; font-size: 14px; font-weight: bold;\">您只需遵守[LGPL]开源协议，即可永久免费使用。");
                    sb.Append("                        </dd>");
                    sb.Append("                    </dl>");
                    sb.Append("                    <dl style=\"border: 0; line-height: 28px;\">");
                    sb.Append("                        <dd style=\"padding-left: 30px;\">> 如果您不对源代码进行任何修改，直接编译使用，可以以任意方式自由使用：开源、非开源、商业及非商业；<br />");
                    sb.Append("                            > 如果您对源代码进行任何的修改或者衍生，涉及修改部分的额外代码和衍生的代码都必须采用LGPL协议开放源代码；<br />");
                    sb.Append("                            > 无论您对源代码的修改程度如何，版权都归iNethinkCMS开发团队所有，我们保留所有权利；<br />");
                    sb.Append("                            > 无论您对源代码如何修改，都必需在明显和恰当地位置宣告版权声明；<br />");
                    sb.Append("                            > 作者享有对许可证条款修改的权利，除非有书面协议的要求，在任何情况下，原作者对许可证条款修改都不对甲方的损失负有任何责任；<br />");
                    sb.Append("                            > 如果在未购买商业授权的情况下私自去除iNethinkCMS版权信息，我们将对您保留法律诉讼的权利。");
                    sb.Append("                        </dd>");
                    sb.Append("                    </dl>");
                    sb.Append("                </div>");
                    sb.Append("");
                    sb.Append("                <div id=\"infos102\" style=\"display: none;\">");
                    sb.Append("                    <dl style=\"border: 0; line-height: 28px;\">");
                    sb.Append("                        <dd style=\"padding-left: 20px; font-size: 14px; font-weight: bold;\">您的捐赠是iNethinkCMS发展的坚强后盾，将会用于：");
                    sb.Append("                        </dd>");
                    sb.Append("                    </dl>");
                    sb.Append("                    <dl style=\"border: 0; line-height: 28px;\">");
                    sb.Append("                        <dd style=\"padding-left: 30px;\">> 坚持开源免费，不断持续进步；<br />");
                    sb.Append("                            > 团队尽最大可能，为用户提供相关帮助；<br />");
                    sb.Append("                            > 支付该项目中所产生的相关运营费用，如：虚拟主机、带宽等。");
                    sb.Append("                        </dd>");
                    sb.Append("                    </dl>");
                    sb.Append("                    <dl style=\"border: 0; line-height: 28px;\">");
                    sb.Append("                        <dd style=\"padding-left: 30px;\"><a href='http://me.alipay.com/irene22' target='_blank'>");
                    sb.Append("                            <img src='https://img.alipay.com/sys/personalprod/style/mc/btn-index.png' /></a>");
                    sb.Append("                        </dd>");
                    sb.Append("                    </dl>");
                    sb.Append("                    <dl style=\"border: 0; line-height: 28px; \">");
                    sb.Append("                        <dd style=\"padding-left: 30px; font-size: 12px; font-weight: bold;\">捐助后，请通过QQ：69991000与我们联系！<br />1）在非源码扩展开发的情况下，团队将尽最大可能，为您提供专属技术支持！<br />2）您将优先获得iNethinkCMS关联产品！");
                    sb.Append("                            <br />");
                    sb.Append("                            3）在您允许的情况下，我们会及时将捐赠细节列于网站捐款名单中。");
                    sb.Append("                        </dd>");
                    sb.Append("                    </dl>");
                    sb.Append("                </div>");
                    sb.Append("            </div>");
                    #endregion

                    this.copyrightID.InnerHtml = sb.ToString();
                    break;

                default:
                    this.navInfoID.InnerText = vNavInfo + "系统管理";
                    this.mainID.Visible = true;

                    if (!IsPostBack)
                    {
                        model = bll.GetModel_SysConfig();
                        this.txtWebName.Text = model.WebName;
                        this.txtInstallDir.Text = model.InstallDir;
                        this.txtUrlMode.SelectedValue = model.UrlMode.ToString();
                        this.txtTemplateCache.SelectedValue = model.TemplateCache;
                        this.txtWebPageCache.SelectedValue = model.WebPageCache;
                        this.txtCacheKey.Text = model.CacheKey;
                        this.txtCacheTime.Text = model.CacheTime;
                        this.txtTemplateDir.Text = model.TemplateDir;

                        this.txtIndexTemplateName.Text = model.IndexTemplateName;
                        this.txtDebugMode.SelectedValue = model.DebugMode;

                        this.txtRewriteExtName.Text = model.RewriteExtName;
                        this.txtRewriteChannelPrefix.Text = model.RewriteChannelPrefix;
                        this.txtRewriteSpecialPrefix.Text = model.RewriteSpecialPrefix;
                        this.txtRewriteContentPrefix.Text = model.RewriteContentPrefix;
                        this.txtRewriteGuestbookPrefix.Text = model.RewriteGuestbookPrefix;

                        this.txtRemoteImgDown.SelectedValue = model.RemoteImgDown;
                        this.txtUpFileType.Text = model.UpFileType;
                        this.txtUpFileMaxSize.Text = model.UpFileMaxSize;

                        this.txtPageListNum.Text = model.PageListNum;
                        this.txtDisplayTitleRule.SelectedValue = model.DisplayTitleRule;
                        this.txtImageSeconds.Text = model.ImageSeconds;
                    }

                    this.txtUrlMode.Attributes.Add("onchange", "do_showrewriteset();");
                    break;
            }
        }

        protected void Submit_Sys_Setting_Click(object sender, EventArgs e)
        {
            string WebName = this.txtWebName.Text.Trim();
            string InstallDir = this.txtInstallDir.Text.Trim();
            int UrlMode = int.Parse(this.txtUrlMode.SelectedValue);
            string TemplateCache = this.txtTemplateCache.SelectedValue;
            string WebPageCache = this.txtWebPageCache.SelectedValue;
            string CacheKey = this.txtCacheKey.Text.Trim();
            string CacheTime = this.txtCacheTime.Text.Trim();
            string TemplateDir = this.txtTemplateDir.Text.Trim();

            string IndexTemplateName = this.txtIndexTemplateName.Text.Trim();
            string DebugMode = this.txtDebugMode.SelectedValue;

            string RewriteExtName = this.txtRewriteExtName.Text.Trim();
            string RewriteChannelPrefix = this.txtRewriteChannelPrefix.Text.Trim();
            string RewriteSpecialPrefix = this.txtRewriteSpecialPrefix.Text.Trim();
            string RewriteContentPrefix = this.txtRewriteContentPrefix.Text.Trim();
            string RewriteGuestbookPrefix = this.txtRewriteGuestbookPrefix.Text.Trim();

            string RemoteImgDown = this.txtRemoteImgDown.SelectedValue;
            string UpFileType = this.txtUpFileType.Text.Trim();
            string UpFileMaxSize = this.txtUpFileMaxSize.Text.Trim();

            string PageListNum = this.txtPageListNum.Text.Trim();
            string DisplayTitleRule = this.txtDisplayTitleRule.SelectedValue;
            string ImageSeconds = this.txtImageSeconds.Text.Trim();

            if (WebName.Length == 0)
            {
                MessageBox.Show(this, "请输入网站名称!");
                return;
            }
            if (InstallDir.Length == 0)
            {
                MessageBox.Show(this, "请输入安装路径!");
                return;
            }
            if (CacheKey.Length == 0)
            {
                MessageBox.Show(this, "请输入缓存标识!");
                return;
            }
            if (CacheTime.Length == 0)
            {
                MessageBox.Show(this, "请输入缓存时间!");
                return;
            }
            if (!Command_Validate.IsNumber(CacheTime))
            {
                MessageBox.Show(this, "缓存时间只允许输入整数数字!");
                return;
            }
            if (TemplateDir.Length == 0)
            {
                MessageBox.Show(this, "请输入模板路径!");
                return;
            }

            if (IndexTemplateName.Length == 0)
            {
                MessageBox.Show(this, "请输入首页模板!");
                return;
            }

            if (UrlMode == 1)
            {
                if (RewriteExtName.Length == 0)
                {
                    MessageBox.Show(this, "请输入伪静态后缀名");
                    return;
                }
                if (RewriteChannelPrefix.Length == 0)
                {
                    MessageBox.Show(this, "请输入栏目页前缀!");
                    return;
                }
                if (RewriteSpecialPrefix.Length == 0)
                {
                    MessageBox.Show(this, "请输入专题页前缀!");
                    return;
                }
                if (RewriteContentPrefix.Length == 0)
                {
                    MessageBox.Show(this, "请输入内容页前缀!");
                    return;
                }
                if (RewriteGuestbookPrefix.Length == 0)
                {
                    MessageBox.Show(this, "请输入留言板前缀!");
                    return;
                }
            }

            if (UpFileType.Length == 0)
            {
                MessageBox.Show(this, "请输入允许上传类型!");
                return;
            }
            if (UpFileMaxSize.Length == 0)
            {
                MessageBox.Show(this, "请输入允许最大上传!");
                return;
            }
            if (!Command_Validate.IsNumber(UpFileMaxSize))
            {
                MessageBox.Show(this, "允许最大上传只允许输入整数数字!");
                return;
            }

            if (ImageSeconds.Length == 0)
            {
                MessageBox.Show(this, "请输入形象图显示时间!");
                return;
            }

            if (!Command_Validate.IsNumber(PageListNum))
            {
                MessageBox.Show(this, "分页列表数量只允许输入整数数字!");
                return;
            }

            if (!Command_Validate.IsNumber(ImageSeconds))
            {
                MessageBox.Show(this, "形象图显示时间只允许输入整数数字!");
                return;
            }

            model.WebName = WebName;
            model.InstallDir = InstallDir;
            model.UrlMode = UrlMode;
            model.TemplateCache = TemplateCache;
            model.WebPageCache = WebPageCache;
            model.CacheKey = CacheKey;
            model.CacheTime = CacheTime;
            model.TemplateDir = TemplateDir;

            model.IndexTemplateName = IndexTemplateName;
            model.DebugMode = DebugMode;

            model.RewriteExtName = RewriteExtName;
            model.RewriteChannelPrefix = RewriteChannelPrefix;
            model.RewriteSpecialPrefix = RewriteSpecialPrefix;
            model.RewriteContentPrefix = RewriteContentPrefix;
            model.RewriteGuestbookPrefix = RewriteGuestbookPrefix;

            model.RemoteImgDown = RemoteImgDown;
            model.UpFileType = UpFileType;
            model.UpFileMaxSize = UpFileMaxSize;

            model.PageListNum = PageListNum;
            model.DisplayTitleRule = DisplayTitleRule;
            model.ImageSeconds = ImageSeconds;

            if (bll.Update_SysConfig(model))
            {
                Web.UI.WebUI_Function.Fun_CacheDel();
                MessageBox.Show(this, "系统配置保存成功!");
            }
            else
            {
                MessageBox.Show(this, "系统配置保存失败!");
            }

        }
    }
}