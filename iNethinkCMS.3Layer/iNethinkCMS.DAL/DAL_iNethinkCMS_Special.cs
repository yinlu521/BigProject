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
    /// 数据访问类:DAL_iNethinkCMS_Special
    /// </summary>
    public partial class DAL_iNethinkCMS_Special
    {
        public DAL_iNethinkCMS_Special()
        {
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_Special");
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
            SqlDataReader sr = Helper.SQLHelper.ExecuteReader("select max(id) from iNethinkCMS_Special");
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
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Special model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_Special(");
            strSql.Append("SpecialName,SpecialTitle,SpecialKeyword,SpecialDescription,SpecialTemplate,SpecialUrl,SpecialPic,SpecialContent,Display,OrderNum)");
            strSql.Append(" values (");
            strSql.Append("@SpecialName,@SpecialTitle,@SpecialKeyword,@SpecialDescription,@SpecialTemplate,@SpecialUrl,@SpecialPic,@SpecialContent,@Display,@OrderNum)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SpecialName", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialTitle", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialKeyword", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@SpecialTemplate", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialPic", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialContent", SqlDbType.Text),
					new SqlParameter("@Display", SqlDbType.SmallInt,2),
					new SqlParameter("@OrderNum", SqlDbType.Int,4)};
            parameters[0].Value = model.SpecialName;
            parameters[1].Value = model.SpecialTitle;
            parameters[2].Value = model.SpecialKeyword;
            parameters[3].Value = model.SpecialDescription;
            parameters[4].Value = model.SpecialTemplate;
            parameters[5].Value = model.SpecialUrl;
            parameters[6].Value = model.SpecialPic;
            parameters[7].Value = model.SpecialContent;
            parameters[8].Value = model.Display;
            parameters[9].Value = model.OrderNum;

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
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Special model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Special set ");
            strSql.Append("SpecialName=@SpecialName,");
            strSql.Append("SpecialTitle=@SpecialTitle,");
            strSql.Append("SpecialKeyword=@SpecialKeyword,");
            strSql.Append("SpecialDescription=@SpecialDescription,");
            strSql.Append("SpecialTemplate=@SpecialTemplate,");
            strSql.Append("SpecialUrl=@SpecialUrl,");
            strSql.Append("SpecialPic=@SpecialPic,");
            strSql.Append("SpecialContent=@SpecialContent,");
            strSql.Append("Display=@Display,");
            strSql.Append("OrderNum=@OrderNum");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@SpecialName", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialTitle", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialKeyword", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@SpecialTemplate", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialPic", SqlDbType.NVarChar,200),
					new SqlParameter("@SpecialContent", SqlDbType.Text),
					new SqlParameter("@Display", SqlDbType.SmallInt,2),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.SpecialName;
            parameters[1].Value = model.SpecialTitle;
            parameters[2].Value = model.SpecialKeyword;
            parameters[3].Value = model.SpecialDescription;
            parameters[4].Value = model.SpecialTemplate;
            parameters[5].Value = model.SpecialUrl;
            parameters[6].Value = model.SpecialPic;
            parameters[7].Value = model.SpecialContent;
            parameters[8].Value = model.Display;
            parameters[9].Value = model.OrderNum;
            parameters[10].Value = model.ID;

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
            strSql.Append("delete from iNethinkCMS_Special ");
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
            strSql.Append("delete from iNethinkCMS_Special ");
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Special GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SpecialName,SpecialTitle,SpecialKeyword,SpecialDescription,SpecialTemplate,SpecialUrl,SpecialPic,SpecialContent,Display,OrderNum from iNethinkCMS_Special ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_Special model = new iNethinkCMS.Model.Model_iNethinkCMS_Special();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SpecialName"] != null && ds.Tables[0].Rows[0]["SpecialName"].ToString() != "")
                {
                    model.SpecialName = ds.Tables[0].Rows[0]["SpecialName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpecialTitle"] != null && ds.Tables[0].Rows[0]["SpecialTitle"].ToString() != "")
                {
                    model.SpecialTitle = ds.Tables[0].Rows[0]["SpecialTitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpecialKeyword"] != null && ds.Tables[0].Rows[0]["SpecialKeyword"].ToString() != "")
                {
                    model.SpecialKeyword = ds.Tables[0].Rows[0]["SpecialKeyword"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpecialDescription"] != null && ds.Tables[0].Rows[0]["SpecialDescription"].ToString() != "")
                {
                    model.SpecialDescription = ds.Tables[0].Rows[0]["SpecialDescription"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpecialTemplate"] != null && ds.Tables[0].Rows[0]["SpecialTemplate"].ToString() != "")
                {
                    model.SpecialTemplate = ds.Tables[0].Rows[0]["SpecialTemplate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpecialUrl"] != null && ds.Tables[0].Rows[0]["SpecialUrl"].ToString() != "")
                {
                    model.SpecialUrl = ds.Tables[0].Rows[0]["SpecialUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpecialPic"] != null && ds.Tables[0].Rows[0]["SpecialPic"].ToString() != "")
                {
                    model.SpecialPic = ds.Tables[0].Rows[0]["SpecialPic"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SpecialContent"] != null && ds.Tables[0].Rows[0]["SpecialContent"].ToString() != "")
                {
                    model.SpecialContent = ds.Tables[0].Rows[0]["SpecialContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Display"] != null && ds.Tables[0].Rows[0]["Display"].ToString() != "")
                {
                    model.Display = int.Parse(ds.Tables[0].Rows[0]["Display"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderNum"] != null && ds.Tables[0].Rows[0]["OrderNum"].ToString() != "")
                {
                    model.OrderNum = int.Parse(ds.Tables[0].Rows[0]["OrderNum"].ToString());
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
            strSql.Append("select ID,SpecialName,SpecialTitle,SpecialKeyword,SpecialDescription,SpecialTemplate,SpecialUrl,SpecialPic,SpecialContent,Display,OrderNum ");
            strSql.Append(" FROM iNethinkCMS_Special ");
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
            strSql.Append(" ID,SpecialName,SpecialTitle,SpecialKeyword,SpecialDescription,SpecialTemplate,SpecialUrl,SpecialPic,SpecialContent,Display,OrderNum ");
            strSql.Append(" FROM iNethinkCMS_Special ");
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
            strSql.Append("select count(1) FROM iNethinkCMS_Special ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_Special Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_Special" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_Special" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());

        }


        #endregion  Method
    }
}

