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
    /// 数据访问类:DAL_iNethinkCMS_Extend_Blogroll
    /// </summary>
    public partial class DAL_iNethinkCMS_Extend_Blogroll
    {
        public DAL_iNethinkCMS_Extend_Blogroll()
        { }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_Extend_Blogroll");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxID()
        {
            int vMaxID = 0;
            SqlDataReader sr = Helper.SQLHelper.ExecuteReader("select max(id) from iNethinkCMS_Extend_Blogroll");
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
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_Extend_Blogroll(");
            strSql.Append("BlogrollClass,BlogrollName,BlogrollImg,BlogrollUrl,Display,OrderNum)");
            strSql.Append(" values (");
            strSql.Append("@BlogrollClass,@BlogrollName,@BlogrollImg,@BlogrollUrl,@Display,@OrderNum)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogrollClass", SqlDbType.Int,4),
					new SqlParameter("@BlogrollName", SqlDbType.NVarChar,100),
					new SqlParameter("@BlogrollImg", SqlDbType.NVarChar,500),
					new SqlParameter("@BlogrollUrl", SqlDbType.NVarChar,400),
					new SqlParameter("@Display", SqlDbType.SmallInt,2),
					new SqlParameter("@OrderNum", SqlDbType.Int,4)};
            parameters[0].Value = model.BlogrollClass;
            parameters[1].Value = model.BlogrollName;
            parameters[2].Value = model.BlogrollImg;
            parameters[3].Value = model.BlogrollUrl;
            parameters[4].Value = model.Display;
            parameters[5].Value = model.OrderNum;

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
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Extend_Blogroll set ");
            strSql.Append("BlogrollClass=@BlogrollClass,");
            strSql.Append("BlogrollName=@BlogrollName,");
            strSql.Append("BlogrollImg=@BlogrollImg,");
            strSql.Append("BlogrollUrl=@BlogrollUrl,");
            strSql.Append("Display=@Display,");
            strSql.Append("OrderNum=@OrderNum");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@BlogrollClass", SqlDbType.Int,4),
					new SqlParameter("@BlogrollName", SqlDbType.NVarChar,100),
					new SqlParameter("@BlogrollImg", SqlDbType.NVarChar,500),
					new SqlParameter("@BlogrollUrl", SqlDbType.NVarChar,400),
					new SqlParameter("@Display", SqlDbType.SmallInt,2),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.BlogrollClass;
            parameters[1].Value = model.BlogrollName;
            parameters[2].Value = model.BlogrollImg;
            parameters[3].Value = model.BlogrollUrl;
            parameters[4].Value = model.Display;
            parameters[5].Value = model.OrderNum;
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
            strSql.Append("delete from iNethinkCMS_Extend_Blogroll ");
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
            strSql.Append("delete from iNethinkCMS_Extend_Blogroll ");
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,BlogrollClass,BlogrollName,BlogrollImg,BlogrollUrl,Display,OrderNum from iNethinkCMS_Extend_Blogroll ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll model = new iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll DataRowToModel(DataRow row)
        {
            iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll model = new iNethinkCMS.Model.Model_iNethinkCMS_Extend_Blogroll();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["BlogrollClass"] != null && row["BlogrollClass"].ToString() != "")
                {
                    model.BlogrollClass = int.Parse(row["BlogrollClass"].ToString());
                }
                if (row["BlogrollName"] != null)
                {
                    model.BlogrollName = row["BlogrollName"].ToString();
                }
                if (row["BlogrollImg"] != null)
                {
                    model.BlogrollImg = row["BlogrollImg"].ToString();
                }
                if (row["BlogrollUrl"] != null)
                {
                    model.BlogrollUrl = row["BlogrollUrl"].ToString();
                }
                if (row["Display"] != null && row["Display"].ToString() != "")
                {
                    model.Display = int.Parse(row["Display"].ToString());
                }
                if (row["OrderNum"] != null && row["OrderNum"].ToString() != "")
                {
                    model.OrderNum = int.Parse(row["OrderNum"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,BlogrollClass,BlogrollName,BlogrollImg,BlogrollUrl,Display,OrderNum ");
            strSql.Append(" FROM iNethinkCMS_Extend_Blogroll ");
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
            strSql.Append(" ID,BlogrollClass,BlogrollName,BlogrollImg,BlogrollUrl,Display,OrderNum ");
            strSql.Append(" FROM iNethinkCMS_Extend_Blogroll ");
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
            strSql.Append("select count(1) FROM iNethinkCMS_Extend_Blogroll ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_Extend_Blogroll Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_Extend_Blogroll" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_Extend_Blogroll" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }


    }
}

