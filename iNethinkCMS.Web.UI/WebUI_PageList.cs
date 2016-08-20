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
using iNethinkCMS.Command;

namespace iNethinkCMS.Web.UI
{
    public class WebUI_PageList
    {
        /// <summary>
        /// 分页函数
        /// 总页数,总记录数,当前页,每页记录数,栏目ID
        /// </summary>
        public static string GetPagingInfo_Web(int byPageCount, int byRecordCount, int byPageNum, int byPageSize, int byCID)
        {
            string vBaseUrl = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
            string rUrl = WebUI_Function.Fun_AddQueryToURL(vBaseUrl, "page", "{page}");

            //获取左右数量
            int i;
            int j;
            int loopnum1;
            int loopnum2;
            loopnum1 = 3; //前面数量
            loopnum2 = 3; //后面数量
            i = byPageNum - loopnum1;
            j = byPageNum + loopnum2;
            if (i < 1)
            {
                j = j + (1 - i);
                i = 1;
            }
            if (j > byPageCount)
            {
                i = i + (byPageCount - j);
                j = byPageCount;
                if (i < 1)
                {
                    i = 1;
                }
            }

            //主要链接
            string firstlink;
            string lastlink;
            string prelink;
            string nextlink;
            firstlink = "<a href=\"" + WebUI_Function.Fun_RemoveQueryFromURL(vBaseUrl, "page") + "\">首页</a>";  //首页
            prelink = "<a href=\"" + rUrl.Replace("{page}", (byPageNum - 1).ToString()) + "\">上一页</a>";  //上一页
            nextlink = "<a href=\"" + rUrl.Replace("{page}", (byPageNum + 1).ToString()) + "\">下一页</a>"; //下一页
            lastlink = "<a href=\"" + rUrl.Replace("{page}", byPageCount.ToString()) + "\">尾页</a>";   //尾页

            //上一页链接判断
            if (byPageNum == 2)
            {
                prelink = "<a href=\"" + WebUI_Function.Fun_RemoveQueryFromURL(vBaseUrl, "page") + "\">上一页</a>";
            }
            //第一页无首页及上一页
            if (byPageNum == 1)
            {
                firstlink = "";
                prelink = "";
            }
            // 最后一页无下一页及尾页
            if (byPageNum == byPageCount)
            {
                lastlink = "";
                nextlink = "";
            }
            //不足一次显示数量就不显示首页及尾页
            if (byPageCount <= loopnum1 + loopnum2 + 1)
            {
                firstlink = "";
                lastlink = "";
            }

            //返回链接
            string looplink = "";
            string thislink;
            //int p = 0;

            for (int p = i; p <= j; p++)
            {
                if (p == byPageNum)
                {
                    thislink = "class=\"nowpage\"";
                }
                else
                {
                    thislink = "";
                }

                if (p == 1)
                {
                    looplink = looplink + "<a href=\"" + WebUI_Function.Fun_RemoveQueryFromURL(vBaseUrl, "page") + "\" " + thislink + ">1</a>";
                }
                else
                {
                    looplink = looplink + "<a href=\"" + rUrl.Replace("{page}", p.ToString()) + "\" " + thislink + ">" + p + "</a>";
                }
            }

            return "<div class=\"page_css\">" + firstlink + prelink + looplink + nextlink + lastlink + "</div>";
        }

        /// <summary>
        /// 分页函数
        /// int byPageCount, int byRecordCount, int byPageNum, int byPageSize
        /// </summary>
        public static string GetPagingInfo_Manage(int byPageCount, int byRecordCount, int byPageNum, int byPageSize)
        {

            string vBaseUrl = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
            string rUrl = WebUI_Function.Fun_AddQueryToURL(vBaseUrl, "page", "{page}");

            //获取左右数量
            int i;
            int j;
            int loopnum1;
            int loopnum2;
            loopnum1 = 4; //前面数量
            loopnum2 = 5; //后面数量
            i = byPageNum - loopnum1;
            j = byPageNum + loopnum2;
            if (i < 1)
            {
                j = j + (1 - i);
                i = 1;
            }
            if (j > byPageCount)
            {
                i = i + (byPageCount - j);
                j = byPageCount;
                if (i < 1)
                {
                    i = 1;
                }
            }

            //主要链接
            string firstlink;
            string lastlink;
            string prelink;
            string nextlink;

            string morefirstlink;
            string morenextlink;

            firstlink = "<a href=\"" + WebUI_Function.Fun_RemoveQueryFromURL(vBaseUrl, "page") + "\">首页</a>";  //首页
            morefirstlink = "<a href=\"" + rUrl.Replace("{page}", (i - 1).ToString()) + "\">...</a>";
            prelink = "<a href=\"" + rUrl.Replace("{page}", (byPageNum - 1).ToString()) + "\">上一页</a>";  //上一页
            nextlink = "<a href=\"" + rUrl.Replace("{page}", (byPageNum + 1).ToString()) + "\">下一页</a>"; //下一页
            morenextlink = "<a href=\"" + rUrl.Replace("{page}", (j + 1).ToString()) + "\">...</a>";
            lastlink = "<a href=\"" + rUrl.Replace("{page}", byPageCount.ToString()) + "\">尾页</a>";   //尾页

            //上一页链接判断
            if (byPageNum == 2)
            {
                prelink = "<a href=\"" + WebUI_Function.Fun_RemoveQueryFromURL(vBaseUrl, "page") + "\">上一页</a>";
            }
            //第一页无首页及上一页
            if (byPageNum == 1)
            {
                firstlink = "";
                prelink = "";
            }
            // 最后一页无下一页及尾页
            if (byPageNum == byPageCount)
            {
                lastlink = "";
                nextlink = "";
            }
            //不足一次显示数量就不显示首页及尾页
            if (byPageCount <= loopnum1 + loopnum2 + 1)
            {
                firstlink = "";
                lastlink = "";
            }

            if (byPageNum - loopnum1 < i || i - 1 == 0)
            {
                morefirstlink = "";
            }
            if (j - loopnum2 < byPageNum || j + 1 > byPageCount)
            {
                morenextlink = "";
            }

            //返回链接
            string looplink = "";
            string thislink;
            //int p = 0;

            for (int p = i; p <= j; p++)
            {
                if (p == byPageNum)
                {
                    thislink = "class=\"cpb\"";
                }
                else
                {
                    thislink = "";
                }

                if (p == 1)
                {
                    looplink = looplink + "<a href=\"" + WebUI_Function.Fun_RemoveQueryFromURL(vBaseUrl, "page") + "\" " + thislink + ">1</a>";
                }
                else
                {
                    looplink = looplink + "<a href=\"" + rUrl.Replace("{page}", p.ToString()) + "\" " + thislink + ">" + p + "</a>";
                }
            }

            return firstlink + prelink + morefirstlink + looplink + morenextlink + nextlink + lastlink;

            //return "";
        }
    }
}
