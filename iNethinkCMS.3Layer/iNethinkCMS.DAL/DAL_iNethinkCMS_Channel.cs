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
    /// 数据访问类:DAL_iNethinkCMS_Channel
    /// </summary>
    public partial class DAL_iNethinkCMS_Channel
    {
        public DAL_iNethinkCMS_Channel()
        {
        }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxID()
        {
            int vMaxID = 0;
            SqlDataReader sr = Helper.SQLHelper.ExecuteReader("select max(id) from iNethinkCMS_Channel");
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
            strSql.Append("select count(1) from iNethinkCMS_Channel");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得父栏目深度
        /// </summary>
        public int GetDeepPath(int FatherID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeepPath from iNethinkCMS_Channel");
            strSql.Append(" where ID=@FatherID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FatherID", SqlDbType.Int,4)			};
            parameters[0].Value = FatherID;
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
        /// 增加一条数据
        /// </summary>
        public bool Add(iNethinkCMS.Model.Model_iNethinkCMS_Channel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_Channel(");
            strSql.Append("Mold,Cid,FatherID,ChildID,ChildIDs,DeepPath,Name,OrderNum,Domain,OutSideLink,Templatechannel,Templateclass,Templateview,Picture,Contents,Keywords,Description,Display,Ename)");
            strSql.Append(" values (");
            strSql.Append("@Mold,@Cid,@FatherID,@ChildID,@ChildIDs,@DeepPath,@Name,@OrderNum,@Domain,@OutSideLink,@Templatechannel,@Templateclass,@Templateview,@Picture,@Contents,@Keywords,@Description,@Display,@Ename)");
            SqlParameter[] parameters = {
					//new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@Mold", SqlDbType.SmallInt,2),
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@FatherID", SqlDbType.Int,4),
					new SqlParameter("@ChildID", SqlDbType.NText),
					new SqlParameter("@ChildIDs", SqlDbType.NText),
					new SqlParameter("@DeepPath", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,500),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
					new SqlParameter("@Domain", SqlDbType.NVarChar,200),
					new SqlParameter("@OutSideLink", SqlDbType.Int,4),
					new SqlParameter("@Templatechannel", SqlDbType.NVarChar,200),
					new SqlParameter("@Templateclass", SqlDbType.NVarChar,200),
					new SqlParameter("@Templateview", SqlDbType.NVarChar,200),
					new SqlParameter("@Picture", SqlDbType.NVarChar,200),
                    new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Display", SqlDbType.Int,4),
                    new SqlParameter("@Ename",SqlDbType.NVarChar,50)
                                        };
            //parameters[0].Value = model.ID;
            parameters[0].Value = model.Mold;
            parameters[1].Value = model.Cid;
            parameters[2].Value = model.FatherID;
            parameters[3].Value = model.ChildID;
            parameters[4].Value = model.ChildIDs;
            parameters[5].Value = model.DeepPath;
            parameters[6].Value = model.Name;
            parameters[7].Value = model.OrderNum;
            parameters[8].Value = model.Domain;
            parameters[9].Value = model.OutSideLink;
            parameters[10].Value = model.Templatechannel;
            parameters[11].Value = model.Templateclass;
            parameters[12].Value = model.Templateview;
            parameters[13].Value = model.Picture;
            parameters[14].Value = model.Contents;
            parameters[15].Value = model.Keywords;
            parameters[16].Value = model.Description;
            parameters[17].Value = model.Display;
            parameters[18].Value = model.Ename;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Channel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Channel set ");
            strSql.Append("Mold=@Mold,");
            strSql.Append("Cid=@Cid,");
            strSql.Append("FatherID=@FatherID,");
            strSql.Append("ChildID=@ChildID,");
            strSql.Append("ChildIDs=@ChildIDs,");
            strSql.Append("DeepPath=@DeepPath,");
            strSql.Append("Name=@Name,");
            strSql.Append("OrderNum=@OrderNum,");
            strSql.Append("Domain=@Domain,");
            strSql.Append("OutSideLink=@OutSideLink,");
            strSql.Append("Templatechannel=@Templatechannel,");
            strSql.Append("Templateclass=@Templateclass,");
            strSql.Append("Templateview=@Templateview,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("Keywords=@Keywords,");
            strSql.Append("Description=@Description,");
            strSql.Append("Display=@Display,");
            strSql.Append("Ename=@Ename");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Mold", SqlDbType.SmallInt,2),
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@FatherID", SqlDbType.Int,4),
					new SqlParameter("@ChildID", SqlDbType.NText),
					new SqlParameter("@ChildIDs", SqlDbType.NText),
					new SqlParameter("@DeepPath", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,500),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
					new SqlParameter("@Domain", SqlDbType.NVarChar,200),
					new SqlParameter("@OutSideLink", SqlDbType.Int,4),
					new SqlParameter("@Templatechannel", SqlDbType.NVarChar,200),
					new SqlParameter("@Templateclass", SqlDbType.NVarChar,200),
					new SqlParameter("@Templateview", SqlDbType.NVarChar,200),
					new SqlParameter("@Picture", SqlDbType.NVarChar,200),
                    new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Display", SqlDbType.Int,4),
                    new SqlParameter("@Ename",SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Mold;
            parameters[1].Value = model.Cid;
            parameters[2].Value = model.FatherID;
            parameters[3].Value = model.ChildID;
            parameters[4].Value = model.ChildIDs;
            parameters[5].Value = model.DeepPath;
            parameters[6].Value = model.Name;
            parameters[7].Value = model.OrderNum;
            parameters[8].Value = model.Domain;
            parameters[9].Value = model.OutSideLink;
            parameters[10].Value = model.Templatechannel;
            parameters[11].Value = model.Templateclass;
            parameters[12].Value = model.Templateview;
            parameters[13].Value = model.Picture;
            parameters[14].Value = model.Contents;
            parameters[15].Value = model.Keywords;
            parameters[16].Value = model.Description;
            parameters[17].Value = model.Display;
            parameters[18].Value = model.Ename;
            parameters[19].Value = model.ID;

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
            strSql.Append("delete from iNethinkCMS_Channel ");
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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from iNethinkCMS_Channel ");
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Channel GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Mold,Cid,FatherID,ChildID,ChildIDs,DeepPath,Name,OrderNum,Domain,OutSideLink,Templatechannel,Templateclass,Templateview,Picture,Contents,Keywords,Description,Display,Ename from iNethinkCMS_Channel ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_Channel model = new iNethinkCMS.Model.Model_iNethinkCMS_Channel();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Mold"] != null && ds.Tables[0].Rows[0]["Mold"].ToString() != "")
                {
                    model.Mold = int.Parse(ds.Tables[0].Rows[0]["Mold"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cid"] != null && ds.Tables[0].Rows[0]["Cid"].ToString() != "")
                {
                    model.Cid = int.Parse(ds.Tables[0].Rows[0]["Cid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FatherID"] != null && ds.Tables[0].Rows[0]["FatherID"].ToString() != "")
                {
                    model.FatherID = int.Parse(ds.Tables[0].Rows[0]["FatherID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChildID"] != null && ds.Tables[0].Rows[0]["ChildID"].ToString() != "")
                {
                    model.ChildID = ds.Tables[0].Rows[0]["ChildID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildIDs"] != null && ds.Tables[0].Rows[0]["ChildIDs"].ToString() != "")
                {
                    model.ChildIDs = ds.Tables[0].Rows[0]["ChildIDs"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeepPath"] != null && ds.Tables[0].Rows[0]["DeepPath"].ToString() != "")
                {
                    model.DeepPath = int.Parse(ds.Tables[0].Rows[0]["DeepPath"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OrderNum"] != null && ds.Tables[0].Rows[0]["OrderNum"].ToString() != "")
                {
                    model.OrderNum = int.Parse(ds.Tables[0].Rows[0]["OrderNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Domain"] != null && ds.Tables[0].Rows[0]["Domain"].ToString() != "")
                {
                    model.Domain = ds.Tables[0].Rows[0]["Domain"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OutSideLink"] != null && ds.Tables[0].Rows[0]["OutSideLink"].ToString() != "")
                {
                    model.OutSideLink = int.Parse(ds.Tables[0].Rows[0]["OutSideLink"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Templatechannel"] != null && ds.Tables[0].Rows[0]["Templatechannel"].ToString() != "")
                {
                    model.Templatechannel = ds.Tables[0].Rows[0]["Templatechannel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Templateclass"] != null && ds.Tables[0].Rows[0]["Templateclass"].ToString() != "")
                {
                    model.Templateclass = ds.Tables[0].Rows[0]["Templateclass"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Templateview"] != null && ds.Tables[0].Rows[0]["Templateview"].ToString() != "")
                {
                    model.Templateview = ds.Tables[0].Rows[0]["Templateview"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Picture"] != null && ds.Tables[0].Rows[0]["Picture"].ToString() != "")
                {
                    model.Picture = ds.Tables[0].Rows[0]["Picture"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Contents"] != null && ds.Tables[0].Rows[0]["Contents"].ToString() != "")
                {
                    model.Contents = ds.Tables[0].Rows[0]["Contents"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Keywords"] != null && ds.Tables[0].Rows[0]["Keywords"].ToString() != "")
                {
                    model.Keywords = ds.Tables[0].Rows[0]["Keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Display"] != null && ds.Tables[0].Rows[0]["Display"].ToString() != "")
                {
                    model.Display = int.Parse(ds.Tables[0].Rows[0]["Display"].ToString());
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Ename"].ToString()))
                {
                    model.Ename = ds.Tables[0].Rows[0]["Ename"].ToString();
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
            strSql.Append("select ID,Mold,Cid,FatherID,ChildID,ChildIDs,DeepPath,Name,OrderNum,Domain,OutSideLink,Templatechannel,Templateclass,Templateview,Picture,Contents,Keywords,Description,Display,Ename ");
            strSql.Append(" FROM iNethinkCMS_Channel ");
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
            strSql.Append(" ID,Mold,Cid,FatherID,ChildID,ChildIDs,DeepPath,Name,OrderNum,Domain,OutSideLink,Templatechannel,Templateclass,Templateview,Picture,Contents,Keywords,Description,Display,Ename ");
            strSql.Append(" FROM iNethinkCMS_Channel ");
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
            strSql.Append("select count(1) FROM iNethinkCMS_Channel ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_Channel Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_Channel" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_Channel" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

