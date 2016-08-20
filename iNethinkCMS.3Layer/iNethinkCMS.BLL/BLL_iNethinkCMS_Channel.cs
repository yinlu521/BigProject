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
    /// BLL_iNethinkCMS_Channel
    /// </summary>
    public partial class BLL_iNethinkCMS_Channel
    {
        private readonly iNethinkCMS.DAL.DAL_iNethinkCMS_Channel dal = new iNethinkCMS.DAL.DAL_iNethinkCMS_Channel();
        public BLL_iNethinkCMS_Channel()
        {
        }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 获得栏目深度
        /// </summary>
        public int GetDeepPath(int FatherID)
        {
            return dal.GetDeepPath(FatherID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(iNethinkCMS.Model.Model_iNethinkCMS_Channel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_Channel model)
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
        public iNethinkCMS.Model.Model_iNethinkCMS_Channel GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_Channel GetModelByCache(int ID)
        {
            string CacheKey = iNethinkCMS.Command.Command_Configuration.GetConfigString("CacheKey");
            CacheKey = CacheKey + "_Model_iNethinkCMS_ChannelModel_" + ID;

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
            return (iNethinkCMS.Model.Model_iNethinkCMS_Channel)objModel;
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
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Channel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<iNethinkCMS.Model.Model_iNethinkCMS_Channel> DataTableToList(DataTable dt)
        {
            List<iNethinkCMS.Model.Model_iNethinkCMS_Channel> modelList = new List<iNethinkCMS.Model.Model_iNethinkCMS_Channel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                iNethinkCMS.Model.Model_iNethinkCMS_Channel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new iNethinkCMS.Model.Model_iNethinkCMS_Channel();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["Mold"] != null && dt.Rows[n]["Mold"].ToString() != "")
                    {
                        model.Mold = int.Parse(dt.Rows[n]["Mold"].ToString());
                    }
                    if (dt.Rows[n]["Cid"] != null && dt.Rows[n]["Cid"].ToString() != "")
                    {
                        model.Cid = int.Parse(dt.Rows[n]["Cid"].ToString());
                    }
                    if (dt.Rows[n]["FatherID"] != null && dt.Rows[n]["FatherID"].ToString() != "")
                    {
                        model.FatherID = int.Parse(dt.Rows[n]["FatherID"].ToString());
                    }
                    if (dt.Rows[n]["ChildID"] != null && dt.Rows[n]["ChildID"].ToString() != "")
                    {
                        model.ChildID = dt.Rows[n]["ChildID"].ToString();
                    }
                    if (dt.Rows[n]["ChildIDs"] != null && dt.Rows[n]["ChildIDs"].ToString() != "")
                    {
                        model.ChildIDs = dt.Rows[n]["ChildIDs"].ToString();
                    }
                    if (dt.Rows[n]["DeepPath"] != null && dt.Rows[n]["DeepPath"].ToString() != "")
                    {
                        model.DeepPath = int.Parse(dt.Rows[n]["DeepPath"].ToString());
                    }
                    if (dt.Rows[n]["Name"] != null && dt.Rows[n]["Name"].ToString() != "")
                    {
                        model.Name = dt.Rows[n]["Name"].ToString();
                    }
                    if (dt.Rows[n]["OrderNum"] != null && dt.Rows[n]["OrderNum"].ToString() != "")
                    {
                        model.OrderNum = int.Parse(dt.Rows[n]["OrderNum"].ToString());
                    }
                    if (dt.Rows[n]["Domain"] != null && dt.Rows[n]["Domain"].ToString() != "")
                    {
                        model.Domain = dt.Rows[n]["Domain"].ToString();
                    }
                    if (dt.Rows[n]["OutSideLink"] != null && dt.Rows[n]["OutSideLink"].ToString() != "")
                    {
                        model.OutSideLink = int.Parse(dt.Rows[n]["OutSideLink"].ToString());
                    }
                    if (dt.Rows[n]["Templatechannel"] != null && dt.Rows[n]["Templatechannel"].ToString() != "")
                    {
                        model.Templatechannel = dt.Rows[n]["Templatechannel"].ToString();
                    }
                    if (dt.Rows[n]["Templateclass"] != null && dt.Rows[n]["Templateclass"].ToString() != "")
                    {
                        model.Templateclass = dt.Rows[n]["Templateclass"].ToString();
                    }
                    if (dt.Rows[n]["Templateview"] != null && dt.Rows[n]["Templateview"].ToString() != "")
                    {
                        model.Templateview = dt.Rows[n]["Templateview"].ToString();
                    }
                    if (dt.Rows[n]["Picture"] != null && dt.Rows[n]["Picture"].ToString() != "")
                    {
                        model.Picture = dt.Rows[n]["Picture"].ToString();
                    }
                    if (dt.Rows[n]["Contents"] != null && dt.Rows[n]["Contents"].ToString() != "")
                    {
                        model.Contents = dt.Rows[n]["Contents"].ToString();
                    }
                    if (dt.Rows[n]["Keywords"] != null && dt.Rows[n]["Keywords"].ToString() != "")
                    {
                        model.Keywords = dt.Rows[n]["Keywords"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["Display"] != null && dt.Rows[n]["Display"].ToString() != "")
                    {
                        model.Display = int.Parse(dt.Rows[n]["Display"].ToString());
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

