/*******************************************************************************@version 1.3.6.0 (2013-08-14)
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
using System.Text.RegularExpressions;
using System.Data;

namespace iNethinkCMS.Web.UI
{
    public class WebUI_Template
    {
        iNethinkCMS.Model.Model_Config siteConfig = new iNethinkCMS.BLL.BLL_Config().GetModel_SysConfig();

        public string vContent = "";
        public string vTemplate = "";   //模板路径
        public int vCID = 0;    //当前栏目ID
        public int vSID = 0;    //专题ID
        public int vPage;   //当前页码

        //载入模板
        public void Load_Template(string byTemplate)
        {
            vTemplate = System.Web.HttpContext.Current.Server.MapPath(siteConfig.TemplateDir + byTemplate);

            bool vTemplateCache = Command.Command_Configuration.GetConfigBool("TemplateCache"); //判断是否启用了模板缓存
            if (vTemplateCache == false)
            {
                Load_Template_File(); //读取模板信息
            }
            else
            {
                //模板缓存
                string templateCacheKey = Command.Command_Configuration.GetConfigString("CacheKey") + "_TemplateCache_" + byTemplate;
                object templateCacheInfo = Command.Command_DataCache.GetCache(templateCacheKey);

                if (templateCacheInfo == null)
                {
                    Load_Template_File(); //读取模板信息
                    Command.Command_DataCache.SetCache(templateCacheKey, (object)vContent);
                }
                else
                {
                    vContent = templateCacheInfo.ToString();
                }
            }
        }

        //读取模板文件
        public void Load_Template_File()
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(vTemplate, System.Text.Encoding.UTF8);
                vContent = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                vContent = "<font color='#ff0000'>" + ex.Message + "</font>";
            }

            //分析内容中是否含有嵌套模板
            Regex regex = new Regex(@"\{template:(.+?)\}", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(vContent);
            foreach (Match m in matchCollection)
            {
                string vNestTemplate = m.Groups[1].Value;
                vNestTemplate = System.Web.HttpContext.Current.Server.MapPath(siteConfig.TemplateDir + vNestTemplate);

                vContent = vContent.Replace(m.Value, Load_Template_NestFile(vNestTemplate));
            }

        }

        //读取模板内容并返回
        public string Load_Template_NestFile(string byNestTemplate)
        {
            string vNestFile = "";
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(byNestTemplate, System.Text.Encoding.UTF8);
                vNestFile = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                vNestFile = "<font color='#ff0000'>" + ex.Message + "</font>";
            }

            return vNestFile;
        }

        //读取自定义标签
        public void Parser_MyTag()
        {
            //读出所有标签的信息
            BLL.BLL_iNethinkCMS_Custom_Tags bll_tags = new BLL.BLL_iNethinkCMS_Custom_Tags();
            DataTable dt = bll_tags.GetAllList().Tables[0];

            Regex regex = new Regex(@"{MyTag:([\s\S]*?)}", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(vContent);
            foreach (Match m in matchCollection)
            {
                string vValueKey = m.Groups[1].Value;
                // vContent = vContent.Replace(m.Value, vValueKey);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Name"].ToString().ToLower() == vValueKey.ToLower())
                    {
                        vContent = vContent.Replace(m.Value, dt.Rows[i]["Code"].ToString());
                    }
                }
            }
        }

        //读取列表信息
        public void Parser_List()
        {
            Regex regex = new Regex(@"<!--(.+?):\{(.+?)\}-->([\s\S]*?)<!--\1-->", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(vContent);
            foreach (Match m in matchCollection)
            {
                string vBackValue = "";
                string vTagLabs = m.Groups[1].Value;	  //标签名称
                string vTagsstr = m.Groups[2].Value;	  //属性信息
                string vLoopstr = m.Groups[3].Value;	  //innerText
                if (vTagLabs.ToLower() != "page")
                {
                    //int vTag_Row = Convert.ToInt32(GetAttr(vTagsstr, "row"));	//列数量
                    string vTag_SQL = Fun_GetAttr(vTagsstr, "sql").Trim();	//单独SQL查询
                    //vContent = vContent.Replace(m.Value, vTag_SQL);

                    //读取DataList
                    DataSet ds = Helper.SQLHelper.Query(vTag_SQL);
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vBackValue = vBackValue + Parser_Tags(i + 1, @"\[" + vTagLabs + @":(.+?)\]", vLoopstr, dt.Rows[i]);
                    }
                    vContent = vContent.Replace(m.Value, vBackValue);

                    //循环调用
                    if (Fun_RegExists(@"<!--(.+?):\{(.+?)\}-->([\s\S]*?)<!--\1-->", vContent) == true)
                    {
                        Parser_List();
                    }
                }
            }
        }

        //读取分页信息
        public void Parser_Page()
        {
            Regex regex = new Regex(@"<!--Page:\{(.+?)\}-->([\s\S]*?)<!--Page-->", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(vContent);
            foreach (Match m in matchCollection)
            {
                string vBackValue = "";
                string vTagsstr = m.Groups[1].Value;	  //属性信息
                string vLoopstr = m.Groups[2].Value;	  //innerText

                string vTag_Table = Fun_GetAttr(vTagsstr, "sqltable").Trim();
                string vTag_Select = Fun_GetAttr(vTagsstr, "sqlselect").Trim();
                string vTag_Where = Fun_GetAttr(vTagsstr, "sqlwhere").Trim();
                string vTag_Orderby = Fun_GetAttr(vTagsstr, "sqlorderby").Trim();

                int vTag_PageSize = 10;
                string vTag_PageSize_Tmp = Fun_GetAttr(vTagsstr, "pagesize").Trim();
                if (vTag_PageSize_Tmp != string.Empty && vTag_PageSize_Tmp != null && Command.Command_Validate.IsNumber(vTag_PageSize_Tmp))
                {
                    vTag_PageSize = Convert.ToInt32(vTag_PageSize_Tmp);
                }

                int vStartIndex = ((vPage - 1) * vTag_PageSize) + 1;
                int vEndIndex = vPage * vTag_PageSize;

                DataSet ds = GetListByPage(vTag_Table, vTag_Select, vTag_Where, vTag_Orderby, vStartIndex, vEndIndex);
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vBackValue = vBackValue + Parser_Tags(vStartIndex + i, @"\[Page:(.+?)\]", vLoopstr, dt.Rows[i]);
                }
                vContent = vContent.Replace(m.Value, vBackValue);

                //分页显示
                int vPageCount;  //总页数
                int vRecordCount; //总记录

                string vSql = "select Count([ID]) From " + vTag_Table + " Where 1 = 1 And " + vTag_Where;
                vRecordCount = Convert.ToInt32(Helper.SQLHelper.GetSingle(vSql));

                if (vRecordCount == 0)
                {
                    vPageCount = 1;
                }
                else
                {
                    vPageCount = (int)Math.Ceiling((double)vRecordCount / (double)vTag_PageSize);
                }
                string vPagingInfo = "";
                if (dt.Rows.Count > 0)
                {
                    vPagingInfo = WebUI_PageList.GetPagingInfo_Web(vPageCount, vRecordCount, vPage, vTag_PageSize, vCID);
                }
                vContent = vContent.Replace("{tag:paging}", vPagingInfo);
            }
        }

        //If Else End If
        public void Parser_IF()
        {
            Regex regex = new Regex(@"{If:(.+?)}([\s\S]*?){End If}", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(vContent);
            foreach (Match m in matchCollection)
            {
                string rInfo = m.Value;
                string vTestInfo = m.Groups[2].Value;
                string vTestBase = Fun_GetAttr(m.Groups[1].Value, "testbase").Trim();
                string vTestValue = Fun_GetAttr(m.Groups[1].Value, "testvalue").Trim();
                string vTestMode = Fun_GetAttr(m.Groups[1].Value, "testmode").Trim();

                if (vTestBase == string.Empty || vTestValue == string.Empty || vTestMode == string.Empty)
                {
                    rInfo = "<font color=red>判断语句相应条件错误，请参见模板手册进行修改!</font>";
                }
                else
                {
                    #region

                    //去掉取值的左右两侧引号
                    if (Command.Command_StringPlus.Left(vTestBase, 1) == "\"")
                    {
                        vTestBase = Command.Command_StringPlus.Mid(vTestBase, 1, vTestBase.Length);
                    }

                    if (Command.Command_StringPlus.Right(vTestBase, 1) == "\"")
                    {
                        vTestBase = Command.Command_StringPlus.Mid(vTestBase, 0, vTestBase.Length - 1);
                    }

                    if (Command.Command_StringPlus.Left(vTestValue, 1) == "\"")
                    {
                        vTestValue = Command.Command_StringPlus.Mid(vTestValue, 1, vTestValue.Length);
                    }

                    if (Command.Command_StringPlus.Right(vTestValue, 1) == "\"")
                    {
                        vTestValue = Command.Command_StringPlus.Mid(vTestValue, 0, vTestValue.Length - 1);
                    }

                    string vTestTrue = "";
                    string vTestFalse = "";
                    if (vTestInfo.ToLower().IndexOf("{else}") > -1)
                    {
                        string[] sArray = Regex.Split(vTestInfo, "{else}", RegexOptions.IgnoreCase);
                        vTestTrue = sArray[0];
                        vTestFalse = sArray[1];
                        //vTestTrue = vTestInfo.Split(new char[6] { '{', 'e', 'l', 's', 'e', '}' }, StringSplitOptions.RemoveEmptyEntries)[0];
                        //vTestFalse = vTestInfo.Split(new char[6] { '{', 'e', 'l', 's', 'e', '}' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    }
                    else
                    {
                        vTestTrue = vTestInfo;
                        vTestFalse = "";
                    }

                    bool vTestBool = false;
                    switch (vTestMode.ToLower())
                    {
                        case "empty": //值为空
                            if (vTestBase == string.Empty)
                            {
                                vTestBool = true;
                            }
                            break;
                        case "notempty": //值不为空
                            if (vTestBase != string.Empty)
                            {
                                vTestBool = true;
                            }
                            break;
                        case "equals": //值等于
                            if (vTestBase == vTestValue)
                            {
                                vTestBool = true;
                            }
                            break;
                        case "notequals": //值不等于
                            if (vTestBase != vTestValue)
                            {
                                vTestBool = true;
                            }
                            break;
                        case "in": //值属于
                            vTestBase = "," + vTestBase + ",";
                            if (vTestBase.IndexOf(vTestValue) >= 0)
                            {
                                vTestBool = true;
                            }
                            break;
                        case "notin": //值不属于
                            vTestBase = "," + vTestBase + ",";
                            if (vTestBase.IndexOf(vTestValue) < 0)
                            {
                                vTestBool = true;
                            }
                            break;
                        case "greatthan": //值大于（限整数符串）
                            if (Command.Command_Validate.IsNumber(vTestBase) == false || Command.Command_Validate.IsNumber(vTestValue) == false)
                            {
                                vTestFalse = "<font color=red>基本值和测试值 必须是整数字符串!</font>";
                            }
                            else
                            {
                                if (Convert.ToInt32(vTestBase) > Convert.ToInt32(vTestValue))
                                {
                                    vTestBool = true;
                                }
                            }
                            break;
                        case "lessthan": //值小于（限整数符串）
                            if (Command.Command_Validate.IsNumber(vTestBase) == false || Command.Command_Validate.IsNumber(vTestValue) == false)
                            {
                                vTestFalse = "<font color=red>基本值和测试值 必须是整数字符串!</font>";
                            }
                            else
                            {
                                if (Convert.ToInt32(vTestBase) < Convert.ToInt32(vTestValue))
                                {
                                    vTestBool = true;
                                }
                            }
                            break;

                        case "datediff": //日期比较
                            if (Command.Command_Validate.IsDateTime(vTestBase) == false)
                            {
                                vTestFalse = "<font color=red>基本值 必须为日期!</font>";
                            }
                            else if (Command.Command_Validate.IsNumber(vTestValue) == false)
                            {
                                vTestFalse = "<font color=red>测试值 必须是整数字符串!</font>";
                            }
                            else
                            {
                                TimeSpan ts = Command.Command_StringPlus.DateDiff(Convert.ToDateTime(vTestBase), DateTime.Now);
                                if (ts.Days < Convert.ToInt32(vTestValue))
                                {
                                    vTestBool = true;
                                }
                            }
                            break;
                    }

                    if (vTestBool == true)
                    {
                        rInfo = vTestTrue;
                    }
                    else
                    {
                        rInfo = vTestFalse;
                    }
                    #endregion
                }
                vContent = vContent.Replace(m.Value, rInfo);
            }

        }

        //替换列表中的字段信息
        public string Parser_Tags(int byI, string byPattern, string byLoopstr, DataRow byDR)
        {

            Regex regex = new Regex(byPattern, RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(byLoopstr);
            foreach (Match m in matchCollection)
            {
                string vTagsstr = m.Groups[1].Value;

                //缩略图相关S
                string vTag_ThumbnailsMode = Fun_GetAttr(vTagsstr, "thumbmode").Trim();
                string vTag_ThumbnailsQuality = Fun_GetAttr(vTagsstr, "thumbquality").Trim();
                if (Command.Command_Validate.IsNumber(vTag_ThumbnailsQuality) == false)
                {
                    vTag_ThumbnailsQuality = "100";
                }
                string vTag_ThumbnailsW = Fun_GetAttr(vTagsstr, "thumbw").Trim();
                string vTag_ThumbnailsH = Fun_GetAttr(vTagsstr, "thumbh").Trim();
                //缩略图相关E

                string vTag_Len = Fun_GetAttr(vTagsstr, "len").Trim();
                string vTag_Lens = Fun_GetAttr(vTagsstr,"lens").Trim();
                string vTag_Format = Fun_GetAttr(vTagsstr, "formatdate").Trim();
                string vTag_Replace = Fun_GetAttr(vTagsstr, "replace").Trim();
                string vTag_Function = Fun_GetAttr(vTagsstr, "function").Trim();
                string vTagsval = vTagsstr.Split(new Char[] { ' ' })[0];

                bool vTagTitle = false;

                switch (vTagsval.ToLower())
                {
                    case "i":   //i
                        vTagsval = byI.ToString();
                        break;

                    case "titlex":   //含有颜色属性的标题
                        vTagsval = byDR["Title"].ToString();
                        vTagTitle = true;
                        break;
                    case "cname":   //栏目名称
                        iNethinkCMS.BLL.BLL_iNethinkCMS_Channel bll_column = new iNethinkCMS.BLL.BLL_iNethinkCMS_Channel();
                        iNethinkCMS.Model.Model_iNethinkCMS_Channel model_column = new iNethinkCMS.Model.Model_iNethinkCMS_Channel();
                        model_column = bll_column.GetModel(Convert.ToInt32(byDR["CID"]));
                        if (model_column != null)
                        {
                            vTagsval = model_column.Name;
                        }
                        else
                        {
                            vTagsval = "-";
                        }

                        break;
                    case "sname":   //专题名称
                        iNethinkCMS.BLL.BLL_iNethinkCMS_Special bll_special = new BLL.BLL_iNethinkCMS_Special();
                        iNethinkCMS.Model.Model_iNethinkCMS_Special model_special = new iNethinkCMS.Model.Model_iNethinkCMS_Special();
                        model_special = bll_special.GetModel(Convert.ToInt32(byDR["SID"]));
                        if (model_special != null)
                        {
                            vTagsval = model_special.SpecialName;
                        }
                        else
                        {
                            vTagsval = "-";
                        }
                        break;
                    default:
                        if (Command.Command_StringPlus.Left(vTagsval, 9).ToLower() == "myfields_")
                        {
                            vTagsval = UI.WebUI_Function.Fun_GetFieldsInfo(vTagsval, byDR["FieldsInfo"].ToString());
                        }
                        else
                        {
                            try
                            {
                                vTagsval = byDR[vTagsval].ToString();
                            }
                            catch
                            {
                                vTagsval = m.Value;
                            }
                        }
                        break;
                }

                //vTagsval = Command.Command_Validate.Decode(vTagsval);

                //replace
                if (vTag_Replace.Length > 0)
                {
                    string[] sp = vTag_Replace.Split(new Char[3] { '#', '#', '#' });
                    if (sp.Length == 2)
                    {
                        vTagsval = vTagsval.Replace(sp[0], sp[1]);
                    }
                }

                //格式化日期
                if (vTag_Format.Length > 0)
                {
                    if (Command.Command_Validate.IsDateTime(vTagsval))
                    {
                        DateTime vTagsval_DateTime = Convert.ToDateTime(vTagsval);
                        vTagsval = vTagsval_DateTime.ToString(vTag_Format);
                    }
                }

                //字符串截取
                if (vTag_Len.Length > 0)
                {
                    vTagsval = Command.Command_StringPlus.Left(vTagsval, Convert.ToInt32(vTag_Len));
                }
                if (vTag_Lens.Length > 0)
                {
                    vTagsval = Command.Command_StringPlus.Left(Command.Command_StringPlus.LostHTML(vTagsval), Convert.ToInt32(vTag_Lens));
                }
                //函数操作
                if (vTag_Function.Length > 0)
                {
                    string[] sp = vTag_Function.Split(new Char[] { ',' });
                    for (int i = 0; i < sp.Length; i++)
                    {
                        switch (sp[i].ToLower())
                        {
                            case "urlencode":
                                vTagsval = System.Web.HttpUtility.UrlEncode(vTagsval, Encoding.UTF8);
                                break;
                            case "urldecode":
                                vTagsval = System.Web.HttpUtility.UrlDecode(vTagsval, Encoding.UTF8);
                                break;
                            case "htmlencode":
                                vTagsval = System.Web.HttpUtility.HtmlEncode(vTagsval);
                                break;
                            case "htmldecode":
                                vTagsval = System.Web.HttpUtility.HtmlDecode(vTagsval);
                                break;
                            case "trim":
                                vTagsval = vTagsval.Trim();
                                break;
                            case "lower":
                                vTagsval = vTagsval.ToLower();
                                break;
                            case "upper":
                                vTagsval = vTagsval.ToUpper();
                                break;
                            case "clearhtml":
                                vTagsval = Command.Command_StringPlus.LostHTML(vTagsval);
                                break;
                        }
                    }
                }

                //缩略图操作
                if (vTag_ThumbnailsMode.Length > 0 && Command.Command_Validate.IsNumber(vTag_ThumbnailsW) && Command.Command_Validate.IsNumber(vTag_ThumbnailsH))
                {
                    //如果原图存在
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(vTagsval)))
                    {
                        vTagsval = WebUI_Function.Fun_GetThumbnail(vTagsval, vTag_ThumbnailsW, vTag_ThumbnailsH, vTag_ThumbnailsMode, vTag_ThumbnailsQuality);
                    }
                }

                //标题样式处理
                if (vTagTitle == true)
                {
                    string vTitle_Color = byDR["Title_Color"].ToString();
                    string vTitle_Style = byDR["Title_Style"].ToString();
                    string vStlye = "";
                    if (vTitle_Color.Length > 0)
                    {
                        vStlye = vTitle_Color;
                    }
                    if (vTitle_Style.Length > 0)
                    {
                        vStlye = vStlye + vTitle_Style;
                    }
                    if (vTitle_Color.Length > 0 || vTitle_Style.Length > 0)
                    {
                        vTagsval = "<font style='" + vStlye + "'>" + vTagsval + "</font>";
                    }
                }
                byLoopstr = byLoopstr.Replace(m.Value, vTagsval);
            }
            return byLoopstr;
        }

        //获取指定标签属性的值
        public string Fun_GetAttr(string byTagsstr, string byAttrName)
        {
            string vBackGetAttr = "";
            //判断是否为空
            if (byTagsstr.ToLower().IndexOf("$" + byAttrName + "=") >= 0)
            {
                Regex regex = new Regex(@"\$" + byAttrName + @"=(.+?) \$", RegexOptions.IgnoreCase);
                MatchCollection matchCollection = regex.Matches(byTagsstr + " $");
                foreach (Match m in matchCollection)
                {
                    vBackGetAttr = m.Groups[1].Value;
                }
            }
            return vBackGetAttr;
        }


        //是否存在此类标签
        public bool Fun_RegExists(string byPattern, string byContent)
        {
            return Regex.IsMatch(byContent, byPattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strTable, string strSelect, string strWhere, string strOrderby, int startIndex, int endIndex)
        {
            if (string.IsNullOrEmpty(strSelect.Trim()))
            {
                strSelect = "*";
            }

            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strWhere = " Where " + strWhere;
            }

            if (!string.IsNullOrEmpty(strOrderby.Trim()))
            {
                strOrderby = " Order By " + strOrderby;
            }

            startIndex = startIndex - 1;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + strSelect + " FROM " + strTable + " Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From " + strTable + strWhere + strOrderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From " + strTable + strWhere + strOrderby + ")");
            strSql.Append(strOrderby);
            return Helper.SQLHelper.Query(strSql.ToString());
        }

    }
}
