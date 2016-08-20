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
using System.Data;
using System.Text;
using System.Data.SqlClient;
using iNethinkCMS.Helper;
namespace iNethinkCMS.DAL
{
    /// <summary>
    /// 数据访问类:DAL_iNethinkCMS_Custom_Pages
    /// </summary>
    public partial class DAL_iNethinkCMS_Custom_Pages
    {
        public DAL_iNethinkCMS_Custom_Pages()
        {
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_Custom_Pages");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// MaxID
        /// </summary>
        public int GetMaxID()
        {
            int vMaxID = 0;
            SqlDataReader sr = Helper.SQLHelper.ExecuteReader("select max(id) from iNethinkCMS_Custom_Pages");
            if (sr.Read())
            {
                vMaxID = Convert.ToInt32(sr[0]);
            }
            sr.Close();
            return vMaxID;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Custom_Pages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_Custom_Pages(");
            strSql.Append("Title,TemplatePath,Dir,Keywords,Description,Html)");
            strSql.Append(" values (");
            strSql.Append("@Title,@TemplatePath,@Dir,@Keywords,@Description,@Html)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,400),
					new SqlParameter("@TemplatePath", SqlDbType.NVarChar,400),
					new SqlParameter("@Dir", SqlDbType.NVarChar,400),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,400),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Html", SqlDbType.Text)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.TemplatePath;
            parameters[2].Value = model.Dir;
            parameters[3].Value = model.Keywords;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Html;

            object obj = SQLHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Custom_Pages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Custom_Pages set ");
            strSql.Append("Title=@Title,");
            strSql.Append("TemplatePath=@TemplatePath,");
            strSql.Append("Dir=@Dir,");
            strSql.Append("Keywords=@Keywords,");
            strSql.Append("Description=@Description,");
            strSql.Append("Html=@Html");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,400),
					new SqlParameter("@TemplatePath", SqlDbType.NVarChar,400),
					new SqlParameter("@Dir", SqlDbType.NVarChar,400),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,400),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Html", SqlDbType.Text),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.TemplatePath;
            parameters[2].Value = model.Dir;
            parameters[3].Value = model.Keywords;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Html;
            parameters[6].Value = model.ID;

            int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from iNethinkCMS_Custom_Pages ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from iNethinkCMS_Custom_Pages ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = SQLHelper.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Custom_Pages GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Title,TemplatePath,Dir,Keywords,Description,Html from iNethinkCMS_Custom_Pages ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_Custom_Pages model = new iNethinkCMS.Model.Model_iNethinkCMS_Custom_Pages();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"] != null)
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TemplatePath"] != null)
                {
                    model.TemplatePath = ds.Tables[0].Rows[0]["TemplatePath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Dir"] != null)
                {
                    model.Dir = ds.Tables[0].Rows[0]["Dir"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Keywords"] != null)
                {
                    model.Keywords = ds.Tables[0].Rows[0]["Keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null)
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Html"] != null)
                {
                    model.Html = ds.Tables[0].Rows[0]["Html"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,TemplatePath,Dir,Keywords,Description,Html ");
            strSql.Append(" FROM iNethinkCMS_Custom_Pages ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,Title,TemplatePath,Dir,Keywords,Description,Html ");
            strSql.Append(" FROM iNethinkCMS_Custom_Pages ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SQLHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM iNethinkCMS_Custom_Pages ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SQLHelper.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strWhere = " Where " + strWhere;
            }

            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                orderby = " Order By " + orderby;
            }

            //startIndex = startIndex - 1;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM iNethinkCMS_Custom_Pages Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_Custom_Pages" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_Custom_Pages" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

