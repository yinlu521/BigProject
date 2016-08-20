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
using System.Text;
using System.Web;
using System.Xml;
using iNethinkCMS.Helper;

namespace iNethinkCMS.DAL
{
    public partial class DAL_Config
    {
        public DAL_Config()
        {
        }

        /// <summary>
        /// 得到一个对象实体 --- 系统配置
        /// </summary>
        public iNethinkCMS.Model.Model_Config GetModel_SysConfig()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/config/sys.config");

            iNethinkCMS.Model.Model_Config model = new iNethinkCMS.Model.Model_Config();
            model.WebName = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//WebName").InnerText.Trim();
            model.InstallDir = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//InstallDir").InnerText.Trim();
            model.UrlMode = int.Parse(XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//UrlMode").InnerText.Trim());
            model.TemplateCache = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//TemplateCache").InnerText.Trim();
            model.WebPageCache = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//WebPageCache").InnerText.Trim();
            model.TemplateDir = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//TemplateDir").InnerText.Trim();
            model.CacheKey = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//CacheKey").InnerText.Trim();
            model.CacheTime = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//CacheTime").InnerText.Trim();

            model.IndexTemplateName = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//IndexTemplateName").InnerText.Trim();
            model.DebugMode = XMLHelper.GetXmlAttribute(HttpContext.Current.Server.MapPath("~/Web.config"), @"/configuration/system.web/compilation", "debug").Value.Trim();


            model.RewriteExtName = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//RewriteExtName").InnerText.Trim();
            model.RewriteChannelPrefix = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//RewriteChannelPrefix").InnerText.Trim();
            model.RewriteSpecialPrefix = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//RewriteSpecialPrefix").InnerText.Trim();
            model.RewriteContentPrefix = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//RewriteContentPrefix").InnerText.Trim();
            model.RewriteGuestbookPrefix = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//RewriteGuestbookPrefix").InnerText.Trim();

            model.RemoteImgDown = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//RemoteImgDown").InnerText.Trim();
            model.UpFileType = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//UpFileType").InnerText.Trim();
            model.UpFileMaxSize = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//UpFileMaxSize").InnerText.Trim();

            model.PageListNum = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//PageListNum").InnerText.Trim();
            model.DisplayTitleRule = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//DisplayTitleRule").InnerText.Trim();
            model.ImageSeconds = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//ImageSeconds").InnerText.Trim();



            return model;
        }

        /// <summary>
        /// 更新 --- 系统配置
        /// </summary>
        public bool Update_SysConfig(iNethinkCMS.Model.Model_Config model)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/config/sys.config");
            string strWebConfigPath = HttpContext.Current.Server.MapPath("~/Web.config");

            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "WebName", "<![CDATA[" + model.WebName + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "InstallDir", "<![CDATA[" + model.InstallDir + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "UrlMode", model.UrlMode.ToString());
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "TemplateCache", model.TemplateCache);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "WebPageCache", model.WebPageCache);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "TemplateDir", "<![CDATA[" + model.TemplateDir + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "CacheTime", model.CacheTime);

            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "IndexTemplateName", model.IndexTemplateName);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(strWebConfigPath, @"/configuration/system.web/compilation", "debug", model.DebugMode);

            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "RewriteExtName", "<![CDATA[" + model.RewriteExtName + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "RewriteChannelPrefix", "<![CDATA[" + model.RewriteChannelPrefix + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "RewriteSpecialPrefix", "<![CDATA[" + model.RewriteSpecialPrefix + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "RewriteContentPrefix", "<![CDATA[" + model.RewriteContentPrefix + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "RewriteGuestbookPrefix", "<![CDATA[" + model.RewriteGuestbookPrefix + "]]>");

            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "RemoteImgDown", model.RemoteImgDown);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "UpFileType", "<![CDATA[" + model.UpFileType + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "UpFileMaxSize", model.UpFileMaxSize);

            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "PageListNum", model.PageListNum);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "DisplayTitleRule", model.DisplayTitleRule);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//sys_configuration", "ImageSeconds", model.ImageSeconds);

            //修改Web.Config中的RewriterRule后缀名
            XmlDocument docX = new XmlDocument();
            try
            {
                docX.Load(strWebConfigPath);
                XmlNodeList nodeList = docX.SelectNodes("/configuration/RewriterConfig/Rules/RewriterRule");

                foreach (XmlNode xn in nodeList)
                {
                    string vLookFor = xn.SelectSingleNode("LookFor").InnerText;
                    string vExt = System.IO.Path.GetExtension(vLookFor);
                    string vNewName = vLookFor.Replace(vExt, "") + "." + model.RewriteExtName;

                    xn.SelectSingleNode("LookFor").InnerText = vNewName;
                }
                docX.Save(strWebConfigPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 得到一个对象实体 --- SEO
        /// </summary>
        public iNethinkCMS.Model.Model_Config GetModel_SeoConfig()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/config/seo.config");

            iNethinkCMS.Model.Model_Config model = new iNethinkCMS.Model.Model_Config();
            model.AutoDescription = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//seo_configuration//AutoDescription").InnerText.Trim();
            model.SeoTitle = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//seo_configuration//SeoTitle").InnerText.Trim();
            model.IndexKeywords = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//seo_configuration//IndexKeywords").InnerText.Trim();
            model.IndexDescription = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//seo_configuration//IndexDescription").InnerText.Trim();

            return model;
        }

        /// <summary>
        /// 更新 --- SEO
        /// </summary>
        public bool Update_SeoConfig(iNethinkCMS.Model.Model_Config model)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/config/seo.config");

            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//seo_configuration", "AutoDescription", model.AutoDescription);
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//seo_configuration", "SeoTitle", "<![CDATA[" + model.SeoTitle + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//seo_configuration", "IndexKeywords", "<![CDATA[" + model.IndexKeywords + "]]>");
            XMLHelper.CreateOrUpdateXmlNodeByXPath(strXmlFile, "//seo_configuration", "IndexDescription", "<![CDATA[" + model.IndexDescription + "]]>");

            return true;
        }
    }
}
