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
    /// BLL_iNethinkCMS_User
    /// </summary>
    public partial class BLL_iNethinkCMS_User
    {
        private readonly iNethinkCMS.DAL.DAL_iNethinkCMS_User dal = new iNethinkCMS.DAL.DAL_iNethinkCMS_User();
        public BLL_iNethinkCMS_User()
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
        /// 验证用户
        /// </summary>
        public bool Exists(int UserType, string UserName, string UserPass)
        {
            return dal.Exists(UserType, UserName, UserPass);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(iNethinkCMS.Model.Model_iNethinkCMS_User model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_User model)
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
        public iNethinkCMS.Model.Model_iNethinkCMS_User GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_User GetModel(string UserName)
        {

            return dal.GetModel(UserName);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public iNethinkCMS.Model.Model_iNethinkCMS_User GetModelByCache(int ID)
        {
            string CacheKey = iNethinkCMS.Command.Command_Configuration.GetConfigString("CacheKey");
            CacheKey = CacheKey + "_Model_iNethinkCMS_UserModel_" + ID;

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
            return (iNethinkCMS.Model.Model_iNethinkCMS_User)objModel;
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
        public List<iNethinkCMS.Model.Model_iNethinkCMS_User> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<iNethinkCMS.Model.Model_iNethinkCMS_User> DataTableToList(DataTable dt)
        {
            List<iNethinkCMS.Model.Model_iNethinkCMS_User> modelList = new List<iNethinkCMS.Model.Model_iNethinkCMS_User>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                iNethinkCMS.Model.Model_iNethinkCMS_User model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new iNethinkCMS.Model.Model_iNethinkCMS_User();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["UserType"] != null && dt.Rows[n]["UserType"].ToString() != "")
                    {
                        model.UserType = int.Parse(dt.Rows[n]["UserType"].ToString());
                    }
                    if (dt.Rows[n]["UserName"] != null && dt.Rows[n]["UserName"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserName"].ToString();
                    }
                    if (dt.Rows[n]["UserPass"] != null && dt.Rows[n]["UserPass"].ToString() != "")
                    {
                        model.UserPass = dt.Rows[n]["UserPass"].ToString();
                    }
                    if (dt.Rows[n]["UserTrueName"] != null && dt.Rows[n]["UserTrueName"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserTrueName"].ToString();
                    }
                    if (dt.Rows[n]["UserEmail"] != null && dt.Rows[n]["UserEmail"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserEmail"].ToString();
                    }
                    if (dt.Rows[n]["UserPower"] != null && dt.Rows[n]["UserPower"].ToString() != "")
                    {
                        model.UserPower = dt.Rows[n]["UserPower"].ToString();
                    }
                    if (dt.Rows[n]["UserChannelPower"] != null && dt.Rows[n]["UserChannelPower"].ToString() != "")
                    {
                        model.UserChannelPower = dt.Rows[n]["UserChannelPower"].ToString();
                    }
                    if (dt.Rows[n]["UserRegTime"] != null && dt.Rows[n]["UserRegTime"].ToString() != "")
                    {
                        model.UserRegTime = DateTime.Parse(dt.Rows[n]["UserRegTime"].ToString());
                    }
                    if (dt.Rows[n]["SecurityCode"] != null && dt.Rows[n]["SecurityCode"].ToString() != "")
                    {
                        model.SecurityCode = dt.Rows[n]["SecurityCode"].ToString();
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

