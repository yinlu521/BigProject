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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace iNethinkCMS.Web.UI
{
    public class WebUI_Function
    {
        // 输出指定间隔的半角空格
        public static string SpaceLength(int i)
        {
            string space = "";
            for (int j = 0; j < i; j++)
            {
                space += System.Web.HttpContext.Current.Server.HtmlDecode("&nbsp;&nbsp;&nbsp;");
            }
            return space;
        }

        //获得文件扩展名
        public static string GetFileExt(string FullPath)
        {
            if (FullPath != "")
                return FullPath.Substring(FullPath.LastIndexOf('.') + 1).ToLower();
            else
                return "";
        }

        //创建文件夹
        public static void CreateFolder(string FolderPath)
        {
            if (!System.IO.Directory.Exists(FolderPath))
            {
                System.IO.Directory.CreateDirectory(FolderPath);
            }
        }

        //获取网站访问URL
        public static string Fun_GetHttpUrl()
        {
            string vAbsoluteUri = HttpContext.Current.Request.Url.AbsoluteUri;
            string vPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;

            return vAbsoluteUri.Replace(vPathAndQuery, "");
        }

        //获取顶级域名
        public static string Fun_GetDomain()
        {
            string vHost = HttpContext.Current.Request.Url.Host;
            string[] BeReplacedStrs = new string[] { ".com.cn", ".edu.cn", ".net.cn", ".org.cn", ".co.jp", ".gov.cn", ".co.uk", "ac.cn", ".edu", ".tv", ".info", ".com", ".ac", ".ag", ".am", ".at", ".be", ".biz", ".bz", ".cc", ".cn", ".com", ".de", ".es", ".eu", ".fm", ".gs", ".hk", ".in", ".info", ".io", ".it", ".jp", ".la", ".md", ".ms", ".name", ".net", ".nl", ".nu", ".org", ".pl", ".ru", ".sc", ".se", ".sg", ".sh", ".tc", ".tk", ".tv", ".tw", ".us", ".co", ".uk", ".vc", ".vg", ".ws", ".il", ".li", ".nz" };

            foreach (string oneBeReplacedStr in BeReplacedStrs)
            {
                string BeReplacedStr = oneBeReplacedStr + " ";
                if (vHost.IndexOf(BeReplacedStr) != -1)
                {
                    vHost = vHost.Replace(BeReplacedStr, string.Empty);
                    break;
                }
            }

            int dotIndex = vHost.LastIndexOf(".");
            vHost = vHost.Substring(dotIndex + 1);
            return vHost;
        }

        //清空系统缓存
        public static void Fun_CacheDel()
        {
            string vCacheKey = Command.Command_Configuration.GetConfigString("CacheKey");

            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            System.Collections.IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                if (Command.Command_StringPlus.Left(CacheEnum.Key.ToString(), vCacheKey.Length) == vCacheKey)
                {
                    objCache.Remove(CacheEnum.Key.ToString());
                }
            }
        }

        //获取站点路径(通过栏目ID循环)
        public static string Fun_GetSitePath(int byCID)
        {
            BLL.BLL_iNethinkCMS_Channel bll = new BLL.BLL_iNethinkCMS_Channel();
            Model.Model_iNethinkCMS_Channel model = new Model.Model_iNethinkCMS_Channel();

            string vBackInfo = "";
            int vFatherID = byCID;
            do
            {
                model = bll.GetModel(vFatherID);
                vBackInfo = " > <a href=\"channel.aspx?id=" + model.Cid + "\">" + model.Name + "</a>" + vBackInfo;
                vFatherID = Convert.ToInt32(model.FatherID);
            } while (vFatherID > 0);

            vBackInfo = "<a href=\"/\">首页</a>" + vBackInfo;

            return vBackInfo;
        }

        //获取站点路径(通过标题)
        public static string Fun_GetSitePath_FromTit(string byTit)
        {
            string vBackInfo = "<a href=\"/\">首页</a>";
            if (byTit.Length > 0)
            {
                vBackInfo += " > " + byTit;
            }
            return vBackInfo;
        }

        //获取缩略图
        public static string Fun_GetThumbnail(string byPicUrl, string byWidth, string byHeight, string byMode, string byQuality)
        {
            string vPicUrl = HttpContext.Current.Server.MapPath(byPicUrl);
            string rInfo = @"/upload/thumbnail/thumb_" + byMode + "_" + byQuality + "_" + byWidth + "_" + byHeight + "_" + Path.GetFileNameWithoutExtension(vPicUrl)
                + Path.GetExtension(vPicUrl);
            string vThumbPicUrl = HttpContext.Current.Server.MapPath(rInfo);

            int vWidth = Convert.ToInt32(byWidth);
            int vHeight = Convert.ToInt32(byHeight);
            int vQuality = Convert.ToInt32(byQuality);

            //判断小图是否存在
            if (File.Exists(vThumbPicUrl) == false)
            {
                try
                {
                    CreateFolder(Path.GetDirectoryName(vThumbPicUrl));
                    Helper.Helper_Thumbnails.CreationThumbnail(vPicUrl, vThumbPicUrl, vWidth, vHeight, byMode, vQuality);
                }
                catch
                {
                    rInfo = byPicUrl;
                }
            }
            return rInfo;
        }


        //URL参数添加/修改
        public static string Fun_AddQueryToURL(string url, string key, string value)
        {
            int fragPos = url.LastIndexOf("#");
            string fragment = string.Empty;
            if (fragPos > -1)
            {
                fragment = url.Substring(fragPos);
                url = url.Substring(0, fragPos);
            }
            int querystart = url.IndexOf("?");
            if (querystart < 0)
            {
                url += "?" + key + "=" + value;
            }
            else
            {
                Regex reg = new Regex(@"(?<=[&\?])" + key + @"=[^\s&#]*", RegexOptions.Compiled);
                if (reg.IsMatch(url))
                    url = reg.Replace(url, key + "=" + value);
                else
                    url += "&" + key + "=" + value;
            }
            return url + fragment;
        }

        //URL参数删除
        public static string Fun_RemoveQueryFromURL(string url, string key)
        {
            Regex reg = new Regex(@"[&\?]" + key + @"=[^\s&#]*&?", RegexOptions.Compiled);
            return reg.Replace(url, new MatchEvaluator(PutAwayGarbageFromURL));
        }

        private static string PutAwayGarbageFromURL(Match match)
        {
            string value = match.Value;
            if (value.EndsWith("&"))
            {
                return value.Substring(0, 1);
            }
            else
            {
                return string.Empty;
            }
        }

        //获取字典类型
        public static string Fun_GetDictTypeName_FromDictType(object byDictType)
        {
            string vDictName;
            int vDictType = int.Parse(byDictType.ToString());
            switch (vDictType)
            {
                case 1:
                    vDictName = "友情链接";
                    break;
                default:
                    vDictName = "-";
                    break;
            }
            return vDictName;
        }

        //获取指定自定义字段信息
        public static string Fun_GetFieldsInfo(string byCustomFieldsKey, string byFieldsInfo)
        {
            string vFieldsInfo = "";
            //byCustomFieldsKey = byCustomFieldsKey.ToLower();
            int i = byFieldsInfo.IndexOf("<" + byCustomFieldsKey + ">", StringComparison.OrdinalIgnoreCase);
            int j = byFieldsInfo.IndexOf("</" + byCustomFieldsKey + ">", StringComparison.OrdinalIgnoreCase);

            if (i >= 0 && j >= 0)
            {
                i = i + byCustomFieldsKey.Length + 2;
                j = j - i;
                vFieldsInfo = Command.Command_StringPlus.Mid(byFieldsInfo, i, j);
            }

            return vFieldsInfo;
        }

        //获取地址
        public static string Fun_UrlRewriter(string byString)
        {
            //string vTmp = byString;
            int vUrlMode = Command.Command_Configuration.GetConfigInt("UrlMode");
            if (vUrlMode == 1)
            {
                string vRewriteExtName = Command.Command_Configuration.GetConfigString("RewriteExtName");
                string vRewriteChannelPrefix = Command.Command_Configuration.GetConfigString("RewriteChannelPrefix");
                string vRewriteSpecialPrefix = Command.Command_Configuration.GetConfigString("RewriteSpecialPrefix");
                string vRewriteContentPrefix = Command.Command_Configuration.GetConfigString("RewriteContentPrefix");
                string vRewriteGuestbookPrefix = Command.Command_Configuration.GetConfigString("RewriteGuestbookPrefix");

                byString = Fun_ReplaceX(byString, @"(content.aspx\?id=)(\d+)(&page=)(\d+)", vRewriteContentPrefix + "_$2_$4." + vRewriteExtName);
                byString = Fun_ReplaceX(byString, @"(content.aspx\?id=)(\d+)", vRewriteContentPrefix + "_$2." + vRewriteExtName);

                byString = Fun_ReplaceX(byString, @"(channel.aspx\?id=)(\d+)(&page=)(\d+)", vRewriteChannelPrefix + "_$2_$4." + vRewriteExtName);
                byString = Fun_ReplaceX(byString, @"(channel.aspx\?id=)(\d+)", vRewriteChannelPrefix + "_$2." + vRewriteExtName);

                byString = Fun_ReplaceX(byString, @"(special.aspx\?id=)(\d+)(&page=)(\d+)", vRewriteSpecialPrefix + "_$2_$4." + vRewriteExtName);
                byString = Fun_ReplaceX(byString, @"(special.aspx\?id=)(\d+)", vRewriteSpecialPrefix + "_$2." + vRewriteExtName);

                byString = Fun_ReplaceX(byString, @"(plugs/guestbook/index.aspx\?page=)(\d+)", vRewriteGuestbookPrefix + "_$2." + vRewriteExtName);
                byString = Fun_ReplaceX(byString, @"(plugs/guestbook/index.aspx)", vRewriteGuestbookPrefix + vRewriteExtName);

                byString = Fun_ReplaceX(byString, @"(index.aspx\?page=)(\d+)", "index_$2." + vRewriteExtName);
                byString = Fun_ReplaceX(byString, @"(index.aspx)", "index." + vRewriteExtName);

            }
            return byString;
        }

        public static string Fun_ReplaceX(string byHtml, string byPatterns, string byReplaceval)
        {
            Regex myRegex = new Regex(byPatterns, RegexOptions.IgnoreCase);
            return myRegex.Replace(byHtml, byReplaceval);
        }

    }
}
