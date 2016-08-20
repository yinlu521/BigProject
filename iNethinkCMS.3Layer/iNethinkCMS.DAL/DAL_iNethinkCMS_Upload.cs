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
    /// 数据访问类:DAL_iNethinkCMS_Upload
    /// </summary>
    public partial class DAL_iNethinkCMS_Upload
    {
        public DAL_iNethinkCMS_Upload()
        {
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_Upload");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 将所有对应ID的文件，设置为非所属状态
        /// V2013/05/24增加cid=0,原因：word发布时可能存在未选择栏目的情况
        /// </summary>
        public void UpdateUploadFile_Reset(int UpType, int Aid, int Cid)
        {
            Helper.SQLHelper.ExecuteSql("update [iNethinkCMS_Upload] set [UpType]=0 ,[Aid]=0 ,[cid]=0 Where [UpType]=" + UpType + " And Aid = " + Aid + " And (Cid = 0 Or Cid = " + Cid + ")");
        }

        /// <summary>
        /// 对指定的文件,数据进行更新
        /// </summary>
        public void UpdateUploadFile_One(string FilePath, int UpType, int Aid, int Cid)
        {
            Helper.SQLHelper.ExecuteSql("update [iNethinkCMS_Upload] set [UpType]=" + UpType + " ,[Aid]=" + Aid + " ,[cid]=" + Cid + " Where [Dir]='" + FilePath + "' And Aid <=0");
        }

        /// <summary>
        /// 分析编辑器中实际的文件,并对数据进行更新
        /// </summary>
        public void UpdateUploadFile(string Html, int UpType, int Aid, int Cid)
        {
            System.Text.RegularExpressions.Regex RegImg = new System.Text.RegularExpressions.Regex(@"/upload/(.+?)\.(jpeg|gif|jpg|png|bmp|mp3|wma|rmvb|rm|rar|asf|avi|wmv|swf|ra|exe|zip|doc|xls)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.MatchCollection matchCollection = RegImg.Matches(Html);
            foreach (System.Text.RegularExpressions.Match m in matchCollection)
            {
                string vImgInfo = m.Value;
                Helper.SQLHelper.ExecuteSql("update [iNethinkCMS_Upload] set [UpType]=" + UpType + " ,[Aid]=" + Aid + " ,[cid]=" + Cid + " Where [Dir]='" + vImgInfo + "' And Aid <=0");
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Upload model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_Upload(");
            strSql.Append("UpType,Aid,Cid,Dir,Ext,Time)");
            strSql.Append(" values (");
            strSql.Append("@UpType,@Aid,@Cid,@Dir,@Ext,@Time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UpType", SqlDbType.SmallInt,2),
					new SqlParameter("@Aid", SqlDbType.Int,4),
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@Dir", SqlDbType.NVarChar,500),
					new SqlParameter("@Ext", SqlDbType.NVarChar,50),
					new SqlParameter("@Time", SqlDbType.DateTime)};
            parameters[0].Value = model.UpType;
            parameters[1].Value = model.Aid;
            parameters[2].Value = model.Cid;
            parameters[3].Value = model.Dir;
            parameters[4].Value = model.Ext;
            parameters[5].Value = model.Time;

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
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Upload model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Upload set ");
            strSql.Append("UpType=@UpType,");
            strSql.Append("Aid=@Aid,");
            strSql.Append("Cid=@Cid,");
            strSql.Append("Dir=@Dir,");
            strSql.Append("Ext=@Ext,");
            strSql.Append("Time=@Time");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UpType", SqlDbType.SmallInt,2),
					new SqlParameter("@Aid", SqlDbType.Int,4),
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@Dir", SqlDbType.NVarChar,500),
					new SqlParameter("@Ext", SqlDbType.NVarChar,50),
					new SqlParameter("@Time", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UpType;
            parameters[1].Value = model.Aid;
            parameters[2].Value = model.Cid;
            parameters[3].Value = model.Dir;
            parameters[4].Value = model.Ext;
            parameters[5].Value = model.Time;
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
            strSql.Append("delete from iNethinkCMS_Upload ");
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
            strSql.Append("delete from iNethinkCMS_Upload ");
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Upload GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UpType,Aid,Cid,Dir,Ext,Time from iNethinkCMS_Upload ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_Upload model = new iNethinkCMS.Model.Model_iNethinkCMS_Upload();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpType"] != null && ds.Tables[0].Rows[0]["UpType"].ToString() != "")
                {
                    model.UpType = int.Parse(ds.Tables[0].Rows[0]["UpType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Aid"] != null && ds.Tables[0].Rows[0]["Aid"].ToString() != "")
                {
                    model.Aid = int.Parse(ds.Tables[0].Rows[0]["Aid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cid"] != null && ds.Tables[0].Rows[0]["Cid"].ToString() != "")
                {
                    model.Cid = int.Parse(ds.Tables[0].Rows[0]["Cid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Dir"] != null && ds.Tables[0].Rows[0]["Dir"].ToString() != "")
                {
                    model.Dir = ds.Tables[0].Rows[0]["Dir"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ext"] != null && ds.Tables[0].Rows[0]["Ext"].ToString() != "")
                {
                    model.Ext = ds.Tables[0].Rows[0]["Ext"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Time"] != null && ds.Tables[0].Rows[0]["Time"].ToString() != "")
                {
                    model.Time = DateTime.Parse(ds.Tables[0].Rows[0]["Time"].ToString());
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
            strSql.Append("select ID,UpType,Aid,Cid,Dir,Ext,Time ");
            strSql.Append(" FROM iNethinkCMS_Upload ");
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
            strSql.Append(" ID,UpType,Aid,Cid,Dir,Ext,Time ");
            strSql.Append(" FROM iNethinkCMS_Upload ");
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
            strSql.Append("select count(1) FROM iNethinkCMS_Upload ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_Upload Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_Upload" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_Upload" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

