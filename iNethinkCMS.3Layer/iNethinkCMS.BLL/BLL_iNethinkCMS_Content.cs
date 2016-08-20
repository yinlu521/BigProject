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
using System.Collections.Generic;
using iNethinkCMS.Command;
using iNethinkCMS.Model;
namespace iNethinkCMS.BLL
{
    /// <summary>
    /// BLL_iNethinkCMS_Content
    /// </summary>
    public partial class BLL_iNethinkCMS_Content
    {
        private readonly iNethinkCMS.DAL.DAL_iNethinkCMS_Content dal = new iNethinkCMS.DAL.DAL_iNethinkCMS_Content();
        public BLL_iNethinkCMS_Content()
        {
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// MaxID
        /// </summary>
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Content model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Content model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            return dal.Delete(ID);
        }
        /// <summary>
        /// 通过栏目ID批量删除数据
        /// </summary>
        public bool DeleteList_ByCID(string CIDlist)
        {
            return dal.DeleteList_ByCID(CIDlist);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 批量审核数据
        /// </summary>
        public bool AuditList(string IDlist)
        {
            return dal.AuditList(IDlist);
        }

        /// <summary>
        /// 批量移动数据 至 相应栏目
        /// </summary>
        public bool MoveList(int Cid, string IDlist)
        {
            return dal.MoveList(Cid, IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Content GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Content GetModelByCache(int ID)
        {
            string CacheKey = iNethinkCMS.Command.Command_Configuration.GetConfigString("CacheKey");
            CacheKey = CacheKey + "_Model_iNethinkCMS_ContentModel_" + ID;

            object objModel = iNethinkCMS.Command.Command_DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = iNethinkCMS.Command.Command_Configuration.GetConfigInt("CacheTime");
                        iNethinkCMS.Command.Command_DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddSeconds(ModelCache), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (iNethinkCMS.Model.Model_iNethinkCMS_Content)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Content> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Content> DataTableToList(DataTable dt)
        {
            List<iNethinkCMS.Model.Model_iNethinkCMS_Content> modelList = new List<iNethinkCMS.Model.Model_iNethinkCMS_Content>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                iNethinkCMS.Model.Model_iNethinkCMS_Content model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new iNethinkCMS.Model.Model_iNethinkCMS_Content();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["Cid"] != null && dt.Rows[n]["Cid"].ToString() != "")
                    {
                        model.Cid = int.Parse(dt.Rows[n]["Cid"].ToString());
                    }
                    if (dt.Rows[n]["Sid"] != null && dt.Rows[n]["Sid"].ToString() != "")
                    {
                        model.Sid = int.Parse(dt.Rows[n]["Sid"].ToString());
                    }
                    if (dt.Rows[n]["Title"] != null && dt.Rows[n]["Title"].ToString() != "")
                    {
                        model.Title = dt.Rows[n]["Title"].ToString();
                    }
                    if (dt.Rows[n]["SubTitle"] != null && dt.Rows[n]["SubTitle"].ToString() != "")
                    {
                        model.SubTitle = dt.Rows[n]["SubTitle"].ToString();
                    }
                    if (dt.Rows[n]["Title_Color"] != null && dt.Rows[n]["Title_Color"].ToString() != "")
                    {
                        model.Title_Color = dt.Rows[n]["Title_Color"].ToString();
                    }
                    if (dt.Rows[n]["Title_Style"] != null && dt.Rows[n]["Title_Style"].ToString() != "")
                    {
                        model.Title_Style = dt.Rows[n]["Title_Style"].ToString();
                    }
                    if (dt.Rows[n]["Author"] != null && dt.Rows[n]["Author"].ToString() != "")
                    {
                        model.Author = dt.Rows[n]["Author"].ToString();
                    }
                    if (dt.Rows[n]["Source"] != null && dt.Rows[n]["Source"].ToString() != "")
                    {
                        model.Source = dt.Rows[n]["Source"].ToString();
                    }
                    if (dt.Rows[n]["Jumpurl"] != null && dt.Rows[n]["Jumpurl"].ToString() != "")
                    {
                        model.Jumpurl = dt.Rows[n]["Jumpurl"].ToString();
                    }
                    if (dt.Rows[n]["Keywords"] != null && dt.Rows[n]["Keywords"].ToString() != "")
                    {
                        model.Keywords = dt.Rows[n]["Keywords"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["Indexpic"] != null && dt.Rows[n]["Indexpic"].ToString() != "")
                    {
                        model.Indexpic = dt.Rows[n]["Indexpic"].ToString();
                    }
                    if (dt.Rows[n]["Views"] != null && dt.Rows[n]["Views"].ToString() != "")
                    {
                        model.Views = int.Parse(dt.Rows[n]["Views"].ToString());
                    }
                    if (dt.Rows[n]["Commend"] != null && dt.Rows[n]["Commend"].ToString() != "")
                    {
                        model.Commend = int.Parse(dt.Rows[n]["Commend"].ToString());
                    }
                    if (dt.Rows[n]["IsComment"] != null && dt.Rows[n]["IsComment"].ToString() != "")
                    {
                        model.IsComment = int.Parse(dt.Rows[n]["IsComment"].ToString());
                    }
                    if (dt.Rows[n]["Display"] != null && dt.Rows[n]["Display"].ToString() != "")
                    {
                        model.Display = int.Parse(dt.Rows[n]["Display"].ToString());
                    }
                    if (dt.Rows[n]["Createtime"] != null && dt.Rows[n]["Createtime"].ToString() != "")
                    {
                        model.Createtime = DateTime.Parse(dt.Rows[n]["Createtime"].ToString());
                    }
                    if (dt.Rows[n]["Modifytime"] != null && dt.Rows[n]["Modifytime"].ToString() != "")
                    {
                        model.Modifytime = DateTime.Parse(dt.Rows[n]["Modifytime"].ToString());
                    }
                    if (dt.Rows[n]["OrderNum"] != null && dt.Rows[n]["OrderNum"].ToString() != "")
                    {
                        model.OrderNum = int.Parse(dt.Rows[n]["OrderNum"].ToString());
                    }
                    if (dt.Rows[n]["Contents"] != null && dt.Rows[n]["Contents"].ToString() != "")
                    {
                        model.Contents = dt.Rows[n]["Contents"].ToString();
                    }
                    if (dt.Rows[n]["FieldsInfo"] != null && dt.Rows[n]["FieldsInfo"].ToString() != "")
                    {
                        model.FieldsInfo = dt.Rows[n]["FieldsInfo"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  Method
    }
}

