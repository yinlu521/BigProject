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
    /// 数据访问类:DAL_iNethinkCMS_Content
    /// </summary>
    public partial class DAL_iNethinkCMS_Content
    {
        public DAL_iNethinkCMS_Content()
        {
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from iNethinkCMS_Content");
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
            SqlDataReader sr = Helper.SQLHelper.ExecuteReader("select max(id) from iNethinkCMS_Content");
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
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Content model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into iNethinkCMS_Content(");
            strSql.Append("Cid,Sid,Title,SubTitle,Title_Color,Title_Style,Author,Source,Jumpurl,Keywords,Description,Indexpic,Views,Commend,IsComment,Display,Createtime,Modifytime,OrderNum,Contents,FieldsInfo)");
            strSql.Append(" values (");
            strSql.Append("@Cid,@Sid,@Title,@SubTitle,@Title_Color,@Title_Style,@Author,@Source,@Jumpurl,@Keywords,@Description,@Indexpic,@Views,@Commend,@IsComment,@Display,@Createtime,@Modifytime,@OrderNum,@Contents,@FieldsInfo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@Sid", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,500),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@Title_Color", SqlDbType.NVarChar,20),
					new SqlParameter("@Title_Style", SqlDbType.NVarChar,30),
					new SqlParameter("@Author", SqlDbType.NVarChar,200),
					new SqlParameter("@Source", SqlDbType.NVarChar,200),
					new SqlParameter("@Jumpurl", SqlDbType.NVarChar,400),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Indexpic", SqlDbType.NVarChar,500),
					new SqlParameter("@Views", SqlDbType.Int,4),
					new SqlParameter("@Commend", SqlDbType.Int,4),
					new SqlParameter("@IsComment", SqlDbType.Int,4),
					new SqlParameter("@Display", SqlDbType.Int,4),
					new SqlParameter("@Createtime", SqlDbType.DateTime),
					new SqlParameter("@Modifytime", SqlDbType.DateTime),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
                    new SqlParameter("@Contents", SqlDbType.NText),
					new SqlParameter("@FieldsInfo", SqlDbType.NText)};

            parameters[0].Value = model.Cid;
            parameters[1].Value = model.Sid;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.SubTitle;
            parameters[4].Value = model.Title_Color;
            parameters[5].Value = model.Title_Style;
            parameters[6].Value = model.Author;
            parameters[7].Value = model.Source;
            parameters[8].Value = model.Jumpurl;
            parameters[9].Value = model.Keywords;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.Indexpic;
            parameters[12].Value = model.Views;
            parameters[13].Value = model.Commend;
            parameters[14].Value = model.IsComment;
            parameters[15].Value = model.Display;
            parameters[16].Value = model.Createtime;
            parameters[17].Value = model.Modifytime;
            parameters[18].Value = model.OrderNum;
            parameters[19].Value = model.Contents;
            parameters[20].Value = model.FieldsInfo;

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
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Content model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Content set ");
            strSql.Append("Cid=@Cid,");
            strSql.Append("Sid=@Sid,");
            strSql.Append("Title=@Title,");
            strSql.Append("SubTitle=@SubTitle,");
            strSql.Append("Title_Color=@Title_Color,");
            strSql.Append("Title_Style=@Title_Style,");
            strSql.Append("Author=@Author,");
            strSql.Append("Source=@Source,");
            strSql.Append("Jumpurl=@Jumpurl,");
            strSql.Append("Keywords=@Keywords,");
            strSql.Append("Description=@Description,");
            strSql.Append("Indexpic=@Indexpic,");
            strSql.Append("Views=@Views,");
            strSql.Append("Commend=@Commend,");
            strSql.Append("IsComment=@IsComment,");
            strSql.Append("Display=@Display,");
            strSql.Append("Createtime=@Createtime,");
            strSql.Append("Modifytime=@Modifytime,");
            strSql.Append("OrderNum=@OrderNum,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("FieldsInfo=@FieldsInfo");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@Sid", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,500),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,250),
					new SqlParameter("@Title_Color", SqlDbType.NVarChar,20),
					new SqlParameter("@Title_Style", SqlDbType.NVarChar,30),
					new SqlParameter("@Author", SqlDbType.NVarChar,200),
					new SqlParameter("@Source", SqlDbType.NVarChar,200),
					new SqlParameter("@Jumpurl", SqlDbType.NVarChar,400),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@Indexpic", SqlDbType.NVarChar,500),
					new SqlParameter("@Views", SqlDbType.Int,4),
					new SqlParameter("@Commend", SqlDbType.Int,4),
					new SqlParameter("@IsComment", SqlDbType.Int,4),
					new SqlParameter("@Display", SqlDbType.Int,4),
					new SqlParameter("@Createtime", SqlDbType.DateTime),
					new SqlParameter("@Modifytime", SqlDbType.DateTime),
					new SqlParameter("@OrderNum", SqlDbType.Int,4),
					new SqlParameter("@Contents", SqlDbType.NText),
                    new SqlParameter("@FieldsInfo", SqlDbType.NText),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Cid;
            parameters[1].Value = model.Sid;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.SubTitle;
            parameters[4].Value = model.Title_Color;
            parameters[5].Value = model.Title_Style;
            parameters[6].Value = model.Author;
            parameters[7].Value = model.Source;
            parameters[8].Value = model.Jumpurl;
            parameters[9].Value = model.Keywords;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.Indexpic;
            parameters[12].Value = model.Views;
            parameters[13].Value = model.Commend;
            parameters[14].Value = model.IsComment;
            parameters[15].Value = model.Display;
            parameters[16].Value = model.Createtime;
            parameters[17].Value = model.Modifytime;
            parameters[18].Value = model.OrderNum;
            parameters[19].Value = model.Contents;
            parameters[20].Value = model.FieldsInfo;
            parameters[21].Value = model.ID;

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
            strSql.Append("delete from iNethinkCMS_Content ");
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
            strSql.Append("delete from iNethinkCMS_Content ");
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
        /// 通过栏目ID批量删除数据
        /// </summary>
        public bool DeleteList_ByCID(string CIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from iNethinkCMS_Content ");
            strSql.Append(" where Cid in (" + CIDlist + ")  ");
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
        /// 批量审核数据
        /// </summary>
        public bool AuditList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Content set Display=1");
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
        /// 批量移动数据 至 相应栏目
        /// </summary>
        public bool MoveList(int Cid, string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update iNethinkCMS_Content set Cid=" + Cid);
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Content GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Cid,Sid,Title,SubTitle,Title_Color,Title_Style,Author,Source,Jumpurl,Keywords,Description,Indexpic,Views,Commend,IsComment,Display,Createtime,Modifytime,OrderNum,Contents,FieldsInfo from iNethinkCMS_Content ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            iNethinkCMS.Model.Model_iNethinkCMS_Content model = new iNethinkCMS.Model.Model_iNethinkCMS_Content();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cid"] != null && ds.Tables[0].Rows[0]["Cid"].ToString() != "")
                {
                    model.Cid = int.Parse(ds.Tables[0].Rows[0]["Cid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sid"] != null && ds.Tables[0].Rows[0]["Sid"].ToString() != "")
                {
                    model.Sid = int.Parse(ds.Tables[0].Rows[0]["Sid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SubTitle"] != null && ds.Tables[0].Rows[0]["SubTitle"].ToString() != "")
                {
                    model.SubTitle = ds.Tables[0].Rows[0]["SubTitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Title_Color"] != null && ds.Tables[0].Rows[0]["Title_Color"].ToString() != "")
                {
                    model.Title_Color = ds.Tables[0].Rows[0]["Title_Color"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Title_Style"] != null && ds.Tables[0].Rows[0]["Title_Style"].ToString() != "")
                {
                    model.Title_Style = ds.Tables[0].Rows[0]["Title_Style"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Author"] != null && ds.Tables[0].Rows[0]["Author"].ToString() != "")
                {
                    model.Author = ds.Tables[0].Rows[0]["Author"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Source"] != null && ds.Tables[0].Rows[0]["Source"].ToString() != "")
                {
                    model.Source = ds.Tables[0].Rows[0]["Source"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jumpurl"] != null && ds.Tables[0].Rows[0]["Jumpurl"].ToString() != "")
                {
                    model.Jumpurl = ds.Tables[0].Rows[0]["Jumpurl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Keywords"] != null && ds.Tables[0].Rows[0]["Keywords"].ToString() != "")
                {
                    model.Keywords = ds.Tables[0].Rows[0]["Keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Indexpic"] != null && ds.Tables[0].Rows[0]["Indexpic"].ToString() != "")
                {
                    model.Indexpic = ds.Tables[0].Rows[0]["Indexpic"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Views"] != null && ds.Tables[0].Rows[0]["Views"].ToString() != "")
                {
                    model.Views = int.Parse(ds.Tables[0].Rows[0]["Views"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Commend"] != null && ds.Tables[0].Rows[0]["Commend"].ToString() != "")
                {
                    model.Commend = int.Parse(ds.Tables[0].Rows[0]["Commend"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsComment"] != null && ds.Tables[0].Rows[0]["IsComment"].ToString() != "")
                {
                    model.IsComment = int.Parse(ds.Tables[0].Rows[0]["IsComment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Display"] != null && ds.Tables[0].Rows[0]["Display"].ToString() != "")
                {
                    model.Display = int.Parse(ds.Tables[0].Rows[0]["Display"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Createtime"] != null && ds.Tables[0].Rows[0]["Createtime"].ToString() != "")
                {
                    model.Createtime = DateTime.Parse(ds.Tables[0].Rows[0]["Createtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Modifytime"] != null && ds.Tables[0].Rows[0]["Modifytime"].ToString() != "")
                {
                    model.Modifytime = DateTime.Parse(ds.Tables[0].Rows[0]["Modifytime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderNum"] != null && ds.Tables[0].Rows[0]["OrderNum"].ToString() != "")
                {
                    model.OrderNum = int.Parse(ds.Tables[0].Rows[0]["OrderNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Contents"] != null && ds.Tables[0].Rows[0]["Contents"].ToString() != "")
                {
                    model.Contents = ds.Tables[0].Rows[0]["Contents"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FieldsInfo"] != null && ds.Tables[0].Rows[0]["FieldsInfo"].ToString() != "")
                {
                    model.FieldsInfo = ds.Tables[0].Rows[0]["FieldsInfo"].ToString();
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
            strSql.Append("select ID,Cid,Sid,Title,SubTitle,Title_Color,Title_Style,Author,Source,Jumpurl,Keywords,Description,Indexpic,Views,Commend,IsComment,Display,Createtime,Modifytime,OrderNum,Contents,FieldsInfo ");
            strSql.Append(" FROM iNethinkCMS_Content ");
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
            strSql.Append(" ID,Cid,Sid,Title,SubTitle,Title_Color,Title_Style,Author,Source,Jumpurl,Keywords,Description,Indexpic,Views,Commend,IsComment,Display,Createtime,Modifytime,OrderNum,Contents,FieldsInfo ");
            strSql.Append(" FROM iNethinkCMS_Content ");
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
            strSql.Append("select count(1) FROM iNethinkCMS_Content ");
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
            strSql.Append("SELECT * FROM iNethinkCMS_Content Where ID Not IN ");
            strSql.Append("(Select Top " + startIndex + " ID From iNethinkCMS_Content" + strWhere + orderby + ")");
            strSql.Append(" And ID In ");
            strSql.Append("(Select Top " + endIndex + " ID From iNethinkCMS_Content" + strWhere + orderby + ")");
            strSql.Append(orderby);
            return SQLHelper.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

