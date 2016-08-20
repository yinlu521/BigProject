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
    /// 数据访问类:DAL_iNethinkCMS_User
    /// </summary>
    public partial class DAL_iNethinkCMS_User
    {
        public DAL_iNethinkCMS_User()
        {
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_User");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 验证该用户
        /// </summary>
        public bool Exists(int UserType, string UserName, string UserPass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [iNethinkCMS_User]");
            strSql.Append(" where UserType=@UserType and UserName=@UserName and UserPass=@UserPass ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserType", SqlDbType.SmallInt,2),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,20),
                    new SqlParameter("@UserPass", SqlDbType.NVarChar,32)};
            parameters[0].Value = UserType;
            parameters[1].Value = UserName;
            parameters[2].Value = UserPass;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_User(");
            strSql.Append("UserType,UserName,UserPass,UserTrueName,UserEmail,UserPower,UserChannelPower,UserRegTime,SecurityCode)");
            strSql.Append(" values (");
            strSql.Append("@UserType,@UserName,@UserPass,@UserTrueName,@UserEmail,@UserPower,@UserChannelPower,@UserRegTime,@SecurityCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserType", SqlDbType.SmallInt,2),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserPass", SqlDbType.NVarChar,32),
					new SqlParameter("@UserTrueName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@UserPower", SqlDbType.NVarChar,500),
                    new SqlParameter("@UserChannelPower", SqlDbType.NVarChar,4000),
					new SqlParameter("@UserRegTime", SqlDbType.DateTime),
                    new SqlParameter("@SecurityCode", SqlDbType.NVarChar,32)
                                        };

            parameters[0].Value = model.UserType;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserPass;
            parameters[3].Value = model.UserTrueName;
            parameters[4].Value = model.UserEmail;
            parameters[5].Value = model.UserPower;
            parameters[6].Value = model.UserChannelPower;
            parameters[7].Value = model.UserRegTime;
            parameters[8].Value = model.SecurityCode;

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
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_User set ");
            strSql.Append("UserType=@UserType,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserPass=@UserPass,");
            strSql.Append("UserTrueName=@UserTrueName,");
            strSql.Append("UserEmail=@UserEmail,");
            strSql.Append("UserPower=@UserPower,");
            strSql.Append("UserChannelPower=@UserChannelPower,");
            strSql.Append("UserRegTime=@UserRegTime,");
            strSql.Append("SecurityCode=@SecurityCode");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserType", SqlDbType.SmallInt,2),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserPass", SqlDbType.NVarChar,32),
					new SqlParameter("@UserTrueName", SqlDbType.NVarChar,20),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@UserPower", SqlDbType.NVarChar,500),
                    new SqlParameter("@UserChannelPower", SqlDbType.NVarChar,4000),
					new SqlParameter("@UserRegTime", SqlDbType.DateTime),
                    new SqlParameter("@SecurityCode", SqlDbType.NVarChar,32),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserType;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserPass;
            parameters[3].Value = model.UserTrueName;
            parameters[4].Value = model.UserEmail;
            parameters[5].Value = model.UserPower;
            parameters[6].Value = model.UserChannelPower;
            parameters[7].Value = model.UserRegTime;
            parameters[8].Value = model.SecurityCode;
            parameters[9].Value = model.ID;

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
            strSql.Append("delete from iNethinkCMS_User ");
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
            strSql.Append("delete from iNethinkCMS_User ");
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
        public iNethinkCMS.Model.Model_iNethinkCMS_User GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserType,UserName,UserPass,UserTrueName,UserEmail,UserPower,UserChannelPower,UserRegTime,SecurityCode from iNethinkCMS_User ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_User model = new iNethinkCMS.Model.Model_iNethinkCMS_User();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPass"] != null && ds.Tables[0].Rows[0]["UserPass"].ToString() != "")
                {
                    model.UserPass = ds.Tables[0].Rows[0]["UserPass"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserTrueName"] != null && ds.Tables[0].Rows[0]["UserTrueName"].ToString() != "")
                {
                    model.UserTrueName = ds.Tables[0].Rows[0]["UserTrueName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserEmail"] != null && ds.Tables[0].Rows[0]["UserEmail"].ToString() != "")
                {
                    model.UserEmail = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPower"] != null && ds.Tables[0].Rows[0]["UserPower"].ToString() != "")
                {
                    model.UserPower = ds.Tables[0].Rows[0]["UserPower"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserChannelPower"] != null && ds.Tables[0].Rows[0]["UserChannelPower"].ToString() != "")
                {
                    model.UserChannelPower = ds.Tables[0].Rows[0]["UserChannelPower"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserRegTime"] != null && ds.Tables[0].Rows[0]["UserRegTime"].ToString() != "")
                {
                    model.UserRegTime = DateTime.Parse(ds.Tables[0].Rows[0]["UserRegTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SecurityCode"] != null && ds.Tables[0].Rows[0]["SecurityCode"].ToString() != "")
                {
                    model.SecurityCode = ds.Tables[0].Rows[0]["SecurityCode"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_User GetModel(string UserName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserType,UserName,UserPass,UserTrueName,UserEmail,UserPower,UserChannelPower,UserRegTime,SecurityCode from iNethinkCMS_User ");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,20)
			};
            parameters[0].Value = UserName;

            iNethinkCMS.Model.Model_iNethinkCMS_User model = new iNethinkCMS.Model.Model_iNethinkCMS_User();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPass"] != null && ds.Tables[0].Rows[0]["UserPass"].ToString() != "")
                {
                    model.UserPass = ds.Tables[0].Rows[0]["UserPass"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserTrueName"] != null && ds.Tables[0].Rows[0]["UserTrueName"].ToString() != "")
                {
                    model.UserTrueName = ds.Tables[0].Rows[0]["UserTrueName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserEmail"] != null && ds.Tables[0].Rows[0]["UserEmail"].ToString() != "")
                {
                    model.UserEmail = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPower"] != null && ds.Tables[0].Rows[0]["UserPower"].ToString() != "")
                {
                    model.UserPower = ds.Tables[0].Rows[0]["UserPower"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserChannelPower"] != null && ds.Tables[0].Rows[0]["UserChannelPower"].ToString() != "")
                {
                    model.UserChannelPower = ds.Tables[0].Rows[0]["UserChannelPower"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserRegTime"] != null && ds.Tables[0].Rows[0]["UserRegTime"].ToString() != "")
                {
                    model.UserRegTime = DateTime.Parse(ds.Tables[0].Rows[0]["UserRegTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SecurityCode"] != null && ds.Tables[0].Rows[0]["SecurityCode"].ToString() != "")
                {
                    model.SecurityCode = ds.Tables[0].Rows[0]["SecurityCode"].ToString();
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
            strSql.Append("select ID,UserType,UserName,UserPass,UserTrueName,UserEmail,UserPower,UserChannelPower,UserRegTime,SecurityCode ");
            strSql.Append(" FROM iNethinkCMS_User ");
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
            strSql.Append(" ID,UserType,UserName,UserPass,UserTrueName,UserEmail,UserPower,UserChannelPower,UserRegTime,SecurityCode ");
            strSql.Append(" FROM iNethinkCMS_User ");
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
            strSql.Append("select count(1) FROM iNethinkCMS_User ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_User Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_User" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_User" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

