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
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iNethinkCMS.Web.UI;
using iNethinkCMS.Command;

namespace iNethinkCMS.Web.inc
{
    public partial class upload_base : Admin_BasePage
    {
        iNethinkCMS.BLL.BLL_iNethinkCMS_Upload bll = new iNethinkCMS.BLL.BLL_iNethinkCMS_Upload();
        iNethinkCMS.Model.Model_iNethinkCMS_Upload model = new iNethinkCMS.Model.Model_iNethinkCMS_Upload();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPower("login");
            //Response.Charset = "UTF-8";
            this.FileUpload.Attributes.Add("onchange", "do_fileselect('" + Request.QueryString["rname"] + "');");
            this.Button_up.Attributes.Add("onclick", "return do_isup('" + Request.QueryString["rname"] + "');");
            this.Button_up.Attributes.Add("disabled", "disabled");
        }

        protected void Button_up_Click(object sender, EventArgs e)
        {
            // 初始化变量
            string inputname = "FileUpload";//表单文件域name
            string rname = Request.QueryString["rname"];//返回信息的input
            string attachdir = "/upload";     // 上传文件保存路径，结尾不要带/
            int dirtype = 1;                 // 1:按天存入目录 2:按月存入目录 3:按扩展名存目录  建议使用按天存
            int maxattachsize = Convert.ToInt32(siteConfig.UpFileMaxSize) * 1024 ;     // 最大上传大小，默认是2M
            string upext = siteConfig.UpFileType;    // 上传扩展名
            byte[] file;                     // 统一转换为byte数组处理
            string localname = "";
            string backmsg = "";

            HttpFileCollection filecollection = Request.Files;
            HttpPostedFile postedfile = filecollection.Get(inputname);

            // 读取原始文件名
            localname = postedfile.FileName;
            // 初始化byte长度.
            file = new Byte[postedfile.ContentLength];

            // 转换为byte类型
            System.IO.Stream stream = postedfile.InputStream;
            stream.Read(file, 0, postedfile.ContentLength);
            stream.Close();

            filecollection = null;

            if (file.Length == 0)
            {
                backmsg = "0|请选择相应的上传文件.";
            }
            else
            {
                #region
                if (file.Length > maxattachsize)
                {
                    backmsg = "0|文件大小超过" + maxattachsize + "字节.";
                }
                else
                {
                    string attach_dir, attach_subdir, filename, extension, target;

                    // 取上载文件后缀名
                    extension = Web.UI.WebUI_Function.GetFileExt(localname);

                    if (("," + upext + ",").IndexOf("," + extension + ",") < 0)
                    {
                        backmsg = "0|上传文件扩展名必需为：" + upext;
                    }
                    else
                    {
                        switch (dirtype)
                        {
                            case 2:
                                attach_subdir = "month_" + DateTime.Now.ToString("yyyyMM");
                                break;
                            case 3:
                                attach_subdir = "ext_" + extension;
                                break;
                            default:
                                attach_subdir = "day_" + DateTime.Now.ToString("yyyyMMdd");
                                break;
                        }
                        attach_dir = attachdir + "/" + attach_subdir + "/";

                        // 生成随机文件名
                        Random random = new Random(DateTime.Now.Millisecond);
                        filename = DateTime.Now.ToString("yyyyMMddhhmmss") + random.Next(10000) + "." + extension;

                        target = attach_dir + filename;
                        try
                        {
                            Web.UI.WebUI_Function.CreateFolder(Server.MapPath(attach_dir));

                            System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath(target), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            fs.Write(file, 0, file.Length);
                            fs.Flush();
                            fs.Close();

                            //存入数据库
                            model.UpType = 0;
                            model.Aid = 0;
                            model.Cid = 0;
                            model.Dir = target;
                            model.Ext = extension;
                            model.Time = DateTime.Now;
                            bll.Add(model);
                        }
                        catch (Exception ex)
                        {
                            backmsg = "0|" + ex.Message.ToString();
                        }

                        backmsg = "1|" + attach_dir + filename;
                    }
                    file = null;
                }
                #endregion
            }

            string[] vBackInfo = backmsg.Split(new Char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            //Response.Write("<script src=\"../admin/skin/js/jquery.min.js\" type=\"text/javascript\"></script>");
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script type=\"text/javascript\">");
            if (vBackInfo[0] == "1")
            {
                Builder.Append("parent.$('#" + rname + "').val('" + vBackInfo[1] + "');");
                Builder.Append("parent.$('#iUpInfo_msg_" + rname + "').html('');");
                Builder.Append("parent.$('#iUpInfo_" + rname + "').css({ display: 'none' })");
            }

            if (vBackInfo[0] == "0")
            {
                Builder.Append("parent.$('#iUpInfo_msg_" + rname + "').html('<span style=\"color:#ff0000\">上传失败：" + vBackInfo[1] + "</span>');");
                Builder.Append("parent.$('#iUpInfo_" + rname + "').css({ display: 'block' })");
            }
            Builder.Append("</script>");
            Response.Write(Builder.ToString());
        }
    }
}