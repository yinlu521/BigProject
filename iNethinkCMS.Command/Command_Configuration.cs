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
using System.Configuration;
using System.Xml;
namespace iNethinkCMS.Command
{
    public class Command_Configuration
    {
        static string strXmlFile = System.Web.HttpContext.Current.Server.MapPath("~/config/sys.config");

        /// <summary>
        /// string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string byKey)
        {
            string rInfo = iNethinkCMS.Helper.XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//" + byKey).InnerText.Trim();
            return rInfo;
        }

        /// <summary>
        /// string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetVersionsString(string byKey)
        {
            string rInfo = iNethinkCMS.Helper.XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_versions//" + byKey).InnerText.Trim();
            return rInfo;
        }

        /// <summary>
        /// Bool
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string byKey)
        {
            bool result = false;
            string cfgVal = iNethinkCMS.Helper.XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//" + byKey).InnerText.Trim();
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {

                }
            }
            return result;
        }
        /// <summary>
        /// Decimal
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string byKey)
        {
            decimal result = 0;
            string cfgVal = iNethinkCMS.Helper.XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//" + byKey).InnerText.Trim();
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {

                }
            }

            return result;
        }
        /// <summary>
        /// int
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string byKey)
        {
            int result = 0;
            string cfgVal = iNethinkCMS.Helper.XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration//" + byKey).InnerText.Trim();
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {

                }
            }

            return result;
        }
        /**
         * 20160803
         * lu.yin
         * 增加方法
         * 将xml配置文件读取并转为json字符串
         * 共webapi调用返回
         **/
        public static string GetConfigToJson() {            
            XmlNode xmlNode = iNethinkCMS.Helper.XMLHelper.GetXmlNodeByXpath(strXmlFile, "//sys_configuration");
            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlNode);
            json=json.Replace("#cdata-section", "Value");
            return json;
        }
    }
}