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
using System.IO;
using System.Net;
using iNethinkCMS.Web.UI;

namespace iNethinkCMS.Web.inc
{
    public partial class upload_remote : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Upload bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Upload();
        iNethinkCMS.Model.Model_iNethinkCMS_Upload model = new iNethinkCMS.Model.Model_iNethinkCMS_Upload();

        private string upExt;  //上传扩展名
        private string attachDir;        //上传文件保存路径，结尾不要带/
        private int dirType;                    // 1:按天存入目录 2:按月存入目录 3:按扩展名存目录  建议使用按天存
        private int maxAttachSize; // 最大上传大小，默认是2M

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("a");

            upExt = ",jpg,jpeg,gif,png,bmp,";
            attachDir = "/upload";
            dirType = 1;
            maxAttachSize = Convert.ToInt32(siteConfig.UpFileMaxSize) * 1024 ;

            Response.Charset = "UTF-8";

            string[] arrUrl = Request["urls"].Split('|');
            for (int i = 0; i < arrUrl.Length; i++)
            {
                string localUrl = saveRemoteImg(arrUrl[i]);
                if (localUrl != "")
                {
                    arrUrl[i] = localUrl;//有效图片替换
                }
            }

            Response.Write(String.Join("|", arrUrl));
            Response.End();
        }

        string saveRemoteImg(string sUrl)
        {
            byte[] fileContent;
            string sExt;
            string sFile;
            if (sUrl.StartsWith("data:image"))
            {
                // base64编码的图片，可能出现在firefox粘贴，或者某些网站上，例如google图片
                int pstart = sUrl.IndexOf('/') + 1;
                sExt = sUrl.Substring(pstart, sUrl.IndexOf(';') - pstart).ToLower();

                if (upExt.IndexOf("," + sExt + ",") == -1)
                {
                    return "";
                }
                fileContent = Convert.FromBase64String(sUrl.Substring(sUrl.IndexOf("base64,") + 7));
            }
            else
            {
                // 图片网址
                sExt = sUrl.Substring(sUrl.LastIndexOf('.') + 1).ToLower();
                if (upExt.IndexOf("," + sExt + ",") == -1)
                {
                    return "";
                }
                fileContent = getUrl(sUrl);
            }

            if (fileContent == null)
            {
                return "";
            }

            //超过最大上传大小忽略
            if (fileContent.Length > maxAttachSize)
            {
                return "";
            }

            //有效图片保存
            sFile = getLocalPath(sExt);
            File.WriteAllBytes(Server.MapPath(sFile), fileContent);

            //存入数据库
            model.UpType = 0;
            model.Aid = 0;
            model.Cid = 0;
            model.Dir = sFile;
            model.Ext = sExt;
            model.Time = DateTime.Now;
            bll.Add(model);

            return sFile;
        }

        string getLocalPath(string extension)
        {
            string attach_dir, attach_subdir, filename;
            switch (dirType)
            {
                case 1:
                    attach_subdir = "day_" + DateTime.Now.ToString("yyyyMMdd");
                    break;
                case 2:
                    attach_subdir = "month_" + DateTime.Now.ToString("yyMM");
                    break;
                default:
                    attach_subdir = "ext_" + extension;
                    break;
            }
            attach_dir = attachDir + "/" + attach_subdir + "/";

            if (!Directory.Exists(Server.MapPath(attach_dir)))
            {
                Directory.CreateDirectory(Server.MapPath(attach_dir));
            }

            // 生成随机文件名
            Random random = new Random(DateTime.Now.Millisecond);
            filename = "r" + DateTime.Now.ToString("yyyyMMddhhmmss") + random.Next(10000) + "." + extension;
            return attach_dir + filename;
        }

        byte[] getUrl(string sUrl)
        {
            WebClient wc = new WebClient();
            try
            {
                return wc.DownloadData(sUrl);
            }
            catch
            {
                return null;
            }
        }
    }
}