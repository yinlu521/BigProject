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
    /// BLL_iNethinkCMS_Special
    /// </summary>
    public partial class BLL_iNethinkCMS_Special
    {
        private readonly iNethinkCMS.DAL.DAL_iNethinkCMS_Special dal = new iNethinkCMS.DAL.DAL_iNethinkCMS_Special();
        public BLL_iNethinkCMS_Special()
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
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Special model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Special model)
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Special GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Special GetModelByCache(int ID)
        {
            string CacheKey = iNethinkCMS.Command.Command_Configuration.GetConfigString("CacheKey");
            CacheKey = CacheKey + "_Model_iNethinkCMS_SpecialModel_" + ID;

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
            return (iNethinkCMS.Model.Model_iNethinkCMS_Special)objModel;

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
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Special> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Special> DataTableToList(DataTable dt)
        {
            List<iNethinkCMS.Model.Model_iNethinkCMS_Special> modelList = new List<iNethinkCMS.Model.Model_iNethinkCMS_Special>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                iNethinkCMS.Model.Model_iNethinkCMS_Special model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new iNethinkCMS.Model.Model_iNethinkCMS_Special();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["SpecialName"] != null && dt.Rows[n]["SpecialName"].ToString() != "")
                    {
                        model.SpecialName = dt.Rows[n]["SpecialName"].ToString();
                    }
                    if (dt.Rows[n]["SpecialTitle"] != null && dt.Rows[n]["SpecialTitle"].ToString() != "")
                    {
                        model.SpecialTitle = dt.Rows[n]["SpecialTitle"].ToString();
                    }
                    if (dt.Rows[n]["SpecialKeyword"] != null && dt.Rows[n]["SpecialKeyword"].ToString() != "")
                    {
                        model.SpecialKeyword = dt.Rows[n]["SpecialKeyword"].ToString();
                    }
                    if (dt.Rows[n]["SpecialDescription"] != null && dt.Rows[n]["SpecialDescription"].ToString() != "")
                    {
                        model.SpecialDescription = dt.Rows[n]["SpecialDescription"].ToString();
                    }
                    if (dt.Rows[n]["SpecialTemplate"] != null && dt.Rows[n]["SpecialTemplate"].ToString() != "")
                    {
                        model.SpecialTemplate = dt.Rows[n]["SpecialTemplate"].ToString();
                    }
                    if (dt.Rows[n]["SpecialUrl"] != null && dt.Rows[n]["SpecialUrl"].ToString() != "")
                    {
                        model.SpecialUrl = dt.Rows[n]["SpecialUrl"].ToString();
                    }
                    if (dt.Rows[n]["SpecialPic"] != null && dt.Rows[n]["SpecialPic"].ToString() != "")
                    {
                        model.SpecialPic = dt.Rows[n]["SpecialPic"].ToString();
                    }
                    if (dt.Rows[n]["SpecialContent"] != null && dt.Rows[n]["SpecialContent"].ToString() != "")
                    {
                        model.SpecialContent = dt.Rows[n]["SpecialContent"].ToString();
                    }
                    if (dt.Rows[n]["Display"] != null && dt.Rows[n]["Display"].ToString() != "")
                    {
                        model.Display = int.Parse(dt.Rows[n]["Display"].ToString());
                    }
                    if (dt.Rows[n]["OrderNum"] != null && dt.Rows[n]["OrderNum"].ToString() != "")
                    {
                        model.OrderNum = int.Parse(dt.Rows[n]["OrderNum"].ToString());
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

