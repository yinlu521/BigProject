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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Web;

namespace iNethinkCMS.Helper
{
    public enum EffentNextType
    {
        /// <summary>
        /// 对其他语句无任何影响 
        /// </summary>
        None,
        /// <summary>
        /// 当前语句必须为"select count(1) from .."格式，如果存在则继续执行，不存在回滚事务
        /// </summary>
        WhenHaveContine,
        /// <summary>
        /// 当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务
        /// </summary>
        WhenNoHaveContine,
        /// <summary>
        /// 当前语句影响到的行数必须大于0，否则回滚事务
        /// </summary>
        ExcuteEffectRows,
        /// <summary>
        /// 引发事件-当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务
        /// </summary>
        SolicitationEvent
    }
    public class CommandInfo
    {
        public object ShareObject = null;
        public object OriginalData = null;
        event EventHandler _solicitationEvent;
        public event EventHandler SolicitationEvent
        {
            add
            {
                _solicitationEvent += value;
            }
            remove
            {
                _solicitationEvent -= value;
            }
        }
        public void OnSolicitationEvent()
        {
            if (_solicitationEvent != null)
            {
                _solicitationEvent(this, new EventArgs());
            }
        }
        public string CommandText;
        public System.Data.Common.DbParameter[] Parameters;
        public EffentNextType EffentNextType = EffentNextType.None;
        public CommandInfo()
        {

        }
        public CommandInfo(string sqlText, SqlParameter[] para)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
        }
        public CommandInfo(string sqlText, SqlParameter[] para, EffentNextType type)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;

        }
    }

    public class GetConnectionString
    {
        /// <summary>
        /// 得到conn.config里配置项的数据库连接字符串。
        /// </summary>
        public static string iGetConn
        {
            get
            {
                string strXmlFile = HttpContext.Current.Server.MapPath("~/config/conn.config");
                string vDataBaseServer = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBaseServer").InnerText.Trim();
                string vDataBasePort = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBasePort").InnerText.Trim();
                string vDataBaseUser = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBaseUser").InnerText.Trim();
                string vDataBasePass = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBasePass").InnerText.Trim();
                string vDataBaseName = XMLHelper.GetXmlNodeByXpath(strXmlFile, "//conn_configuration//DataBaseName").InnerText.Trim();
                string _connectionString;
                if (vDataBasePort != "0")
                {
                    vDataBaseServer = vDataBaseServer + "," + vDataBasePort;
                }
                _connectionString = "Data Source=" + vDataBaseServer + ";Initial Catalog=" + vDataBaseName + ";User ID=" + vDataBaseUser + ";Password=" + vDataBasePass + "";
                return _connectionString;

            }
        }

    }
}
