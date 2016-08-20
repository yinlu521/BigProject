using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iNethinkCMS.Helper;
namespace iNethinkCMS.DAL
{
    public class DAL_iNethinkCMS_AdGroup
    {
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxID()
        {
            int vMaxID = 0;
            SqlDataReader sr = Helper.SQLHelper.ExecuteReader("select max(id) from iNethinkCMS_AdGroup");
            if (sr.Read())
            {
                vMaxID = Convert.ToInt32(sr[0]);
            }
            sr.Close();
            return vMaxID;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_AdGroup");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }

        public bool Add(iNethinkCMS.Model.Model_iNethinkCMS_AdGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert Into iNethinkCMS_AdGroup (Title) values ( @Title)");
            SqlParameter[] parms = { new SqlParameter("@Title", model.Title) };
            try
            {
                int rows = SQLHelper.ExecuteSql(strSql.ToString(),parms);
                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_AdGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_AdGroup");
            strSql.Append(" set Title=@Title where Id=@ID");
            SqlParameter[] parms = { new SqlParameter("@ID",model.ID),
                                   new SqlParameter("@Title",model.Title)
                                    };
            try
            {
                int rows = SQLHelper.ExecuteSql(strSql.ToString(),parms);
                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from iNethinkCMS_AdGroup ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
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

        public iNethinkCMS.Model.Model_iNethinkCMS_AdGroup GetMode(int Id)
        {
            string sql = "select * from iNethinkCMS_AdGroup where ID=@Id";
            SqlParameter[] parms = { new SqlParameter("@Id",Id)};
            Model.Model_iNethinkCMS_AdGroup model = new Model.Model_iNethinkCMS_AdGroup();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, parms))
            {
                while (dr.Read())
                {
                    model.ID = Convert.ToInt32(dr["Id"]);
                    model.Title = dr["Title"].ToString();
                }
            }
            return model;
        }

        public DataSet GetList(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title from iNethinkCMS_AdGroup ");
            if (where.Trim().Length > 0)
            {
                strSql.AppendFormat(" where id>0 {0} ",where);
            }
            return SQLHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数

        /// </summary>
        /// 
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM iNethinkCMS_AdGroup ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_AdGroup Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_AdGroup" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_AdGroup" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }

    }
}
