﻿/*******************************************************************************
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
    /// BLL_iNethinkCMS_Custom_Tags
    /// </summary>
    public partial class BLL_iNethinkCMS_Custom_Tags
    {
        private readonly iNethinkCMS.DAL.DAL_iNethinkCMS_Custom_Tags dal = new iNethinkCMS.DAL.DAL_iNethinkCMS_Custom_Tags();
        public BLL_iNethinkCMS_Custom_Tags()
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
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags model)
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
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags GetModelByCache(int ID)
        {
            string CacheKey = iNethinkCMS.Command.Command_Configuration.GetConfigString("CacheKey");
            CacheKey = CacheKey + "_Model_iNethinkCMS_Custom_TagsModel_" + ID;

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
            return (iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags)objModel;
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
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags> DataTableToList(DataTable dt)
        {
            List<iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags> modelList = new List<iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new iNethinkCMS.Model.Model_iNethinkCMS_Custom_Tags();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["Name"] != null && dt.Rows[n]["Name"].ToString() != "")
                    {
                        model.Name = dt.Rows[n]["Name"].ToString();
                    }
                    if (dt.Rows[n]["Remark"] != null && dt.Rows[n]["Remark"].ToString() != "")
                    {
                        model.Remark = dt.Rows[n]["Remark"].ToString();
                    }
                    if (dt.Rows[n]["Code"] != null && dt.Rows[n]["Code"].ToString() != "")
                    {
                        model.Code = dt.Rows[n]["Code"].ToString();
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

