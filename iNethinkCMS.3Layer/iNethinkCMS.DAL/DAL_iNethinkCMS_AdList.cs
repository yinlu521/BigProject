using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using iNethinkCMS.Helper;

namespace iNethinkCMS.DAL
{
    public class DAL_iNethinkCMS_AdList
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_AdList");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }

        public bool Add(iNethinkCMS.Model.Model_iNethinkCMS_AdList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_AdList(Title,IndexPic,LinkUrl,[Desc],orderNum,addtime,ParentId )");
            strSql.Append("values(@Title,@IndexPic,@LinkUrl,@Desc,@orderNum,@addtime,@ParentId )");
            SqlParameter[] parms = { 
                                    new SqlParameter("@Title",model.Title),
                                    new SqlParameter("@IndexPic",model.IndexPic),
                                    new SqlParameter("@LinkUrl",model.Linkurl),
                                    new SqlParameter("@Desc",model.Desc),
                                    new SqlParameter("@orderNum",model.OrderNum),
                                    new SqlParameter("@ParentId ",model.ParentId),
                                    new SqlParameter("@addtime",model.AddTime)
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

        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_AdList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_AdList set ");
            strSql.Append(" Title=@Title,");
            strSql.Append(" IndexPic=@IndexPic,");
            strSql.Append(" LinkUrl=@LinkUrl,");
            strSql.Append(" [Desc]=@Desc,");
            strSql.Append(" orderNum=@orderNum,");
            strSql.Append(" parentID=@ParentId");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parms = { 
                                    new SqlParameter("@Title",model.Title),
                                    new SqlParameter("@IndexPic",model.IndexPic),
                                    new SqlParameter("@LinkUrl",model.Linkurl),
                                    new SqlParameter("@Desc",model.Desc),
                                    new SqlParameter("@orderNum",model.OrderNum),
                                    new SqlParameter("@ParentId ",model.ParentId),
                                    new SqlParameter("@Id",model.ID)
                                   };
            try
            {
                int rows = SQLHelper.ExecuteSql(strSql.ToString(), parms);
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from iNethinkCMS_AdList ");
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
            strSql.Append("delete from iNethinkCMS_AdList ");
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
        public iNethinkCMS.Model.Model_iNethinkCMS_AdList GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from iNethinkCMS_AdList ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_AdList model = new iNethinkCMS.Model.Model_iNethinkCMS_AdList();
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
        public iNethinkCMS.Model.Model_iNethinkCMS_AdList DataRowToModel(DataRow row)
        {
            iNethinkCMS.Model.Model_iNethinkCMS_AdList model = new iNethinkCMS.Model.Model_iNethinkCMS_AdList();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (!string.IsNullOrEmpty(row["Title"].ToString()))
                {
                    model.Title = row["Title"].ToString();
                }
                if (!string.IsNullOrEmpty(row["IndexPic"].ToString()))
                {
                    model.IndexPic = row["IndexPic"].ToString();
                }
                if (!string.IsNullOrEmpty(row["LinkUrl"].ToString()))
                {
                    model.Linkurl = row["linkurl"].ToString();
                }
                if (!string.IsNullOrEmpty(row["Desc"].ToString()))
                {
                    model.Desc = row["Desc"].ToString();
                }
                if (!string.IsNullOrEmpty(row["orderNum"].ToString()))
                {
                    model.OrderNum = int.Parse(row["orderNum"].ToString());
                }
                if (!string.IsNullOrEmpty(row["addtime"].ToString()))
                {
                    model.AddTime = DateTime.Parse(row["addtime"].ToString());
                }
                if (!string.IsNullOrEmpty(row["parentId"].ToString()))
                { 
                    model.ParentId=int.Parse(row["parentId"].ToString());
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
            strSql.Append("select id,Title,IndexPic,Linkurl,[Desc],orderNum,addtime,parentId  ");
            strSql.Append(" FROM iNethinkCMS_AdList ");
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
            strSql.Append(" id,Title,IndexPic,Linkurl,Desc,orderNum,addtime,ParentId  ");
            strSql.Append(" FROM iNethinkCMS_AdList ");
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
            strSql.Append("select count(1) FROM iNethinkCMS_AdList ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_AdList Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_AdList" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_AdList" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }
    }
}
