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
    /// BLL_iNethinkCMS_Upload
    /// </summary>
    public partial class BLL_iNethinkCMS_Upload
    {
        private readonly iNethinkCMS.DAL.DAL_iNethinkCMS_Upload dal = new iNethinkCMS.DAL.DAL_iNethinkCMS_Upload();
        public BLL_iNethinkCMS_Upload()
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
        /// 将所有对应ID的文件，设置为非所属状态
        /// </summary>
        public void UpdateUploadFile_Reset(int UpType, int Aid, int Cid)
        {
            dal.UpdateUploadFile_Reset( UpType, Aid, Cid);
        }

        /// <summary>
        /// 对指定的文件,数据进行更新
        /// </summary>
        public void UpdateUploadFile_One(string FilePath, int UpType, int Aid, int Cid)
        {
            dal.UpdateUploadFile_One(FilePath, UpType, Aid, Cid);
        }

        /// <summary>
        /// 分析编辑器中实际的文件,并对数据进行更新
        /// </summary>
        public void UpdateUploadFile(string Html, int UpType, int Aid, int Cid)
        {
            dal.UpdateUploadFile(Html, UpType, Aid, Cid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_Upload model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Upload model)
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Upload GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Upload GetModelByCache(int ID)
        {

            string CacheKey = iNethinkCMS.Command.Command_Configuration.GetConfigString("CacheKey");
            CacheKey = CacheKey + "_Model_iNethinkCMS_Upload_" + ID;
            object objModel = iNethinkCMS.Command.Command_DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = iNethinkCMS.Command.Command_Configuration.GetConfigInt("CacheTime");
                        iNethinkCMS.Command.Command_DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (iNethinkCMS.Model.Model_iNethinkCMS_Upload)objModel;
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
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Upload> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Upload> DataTableToList(DataTable dt)
        {
            List<iNethinkCMS.Model.Model_iNethinkCMS_Upload> modelList = new List<iNethinkCMS.Model.Model_iNethinkCMS_Upload>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                iNethinkCMS.Model.Model_iNethinkCMS_Upload model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new iNethinkCMS.Model.Model_iNethinkCMS_Upload();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["UpType"] != null && dt.Rows[n]["UpType"].ToString() != "")
                    {
                        model.UpType = int.Parse(dt.Rows[n]["UpType"].ToString());
                    }
                    if (dt.Rows[n]["Aid"] != null && dt.Rows[n]["Aid"].ToString() != "")
                    {
                        model.Aid = int.Parse(dt.Rows[n]["Aid"].ToString());
                    }
                    if (dt.Rows[n]["Cid"] != null && dt.Rows[n]["Cid"].ToString() != "")
                    {
                        model.Cid = int.Parse(dt.Rows[n]["Cid"].ToString());
                    }
                    if (dt.Rows[n]["Dir"] != null && dt.Rows[n]["Dir"].ToString() != "")
                    {
                        model.Dir = dt.Rows[n]["Dir"].ToString();
                    }
                    if (dt.Rows[n]["Ext"] != null && dt.Rows[n]["Ext"].ToString() != "")
                    {
                        model.Ext = dt.Rows[n]["Ext"].ToString();
                    }
                    if (dt.Rows[n]["Time"] != null && dt.Rows[n]["Time"].ToString() != "")
                    {
                        model.Time = DateTime.Parse(dt.Rows[n]["Time"].ToString());
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

