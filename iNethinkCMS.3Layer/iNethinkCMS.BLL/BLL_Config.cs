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
using System.Collections.Generic;
using System.Text;

namespace iNethinkCMS.BLL
{
    public partial class BLL_Config
    {
        private readonly iNethinkCMS.DAL.DAL_Config dal = new iNethinkCMS.DAL.DAL_Config();
        public BLL_Config()
        {
        }

        /// <summary>
        /// 得到一个对象实体 --- 系统配置
        /// </summary>
        public iNethinkCMS.Model.Model_Config GetModel_SysConfig()
        {
            return dal.GetModel_SysConfig();
        }

        /// <summary>
        /// 更新一个对象实体 --- 系统配置
        /// </summary>
        public bool Update_SysConfig(iNethinkCMS.Model.Model_Config model)
        {
            return dal.Update_SysConfig(model);
        }

        /// <summary>
        /// 得到一个对象实体 --- SEO
        /// </summary>
        public iNethinkCMS.Model.Model_Config GetModel_SeoConfig()
        {
            return dal.GetModel_SeoConfig();
        }

        /// <summary>
        /// 更新一个对象实体 --- SEO
        /// </summary>
        public bool Update_SeoConfig(iNethinkCMS.Model.Model_Config model)
        {
            return dal.Update_SeoConfig(model);
        }
    }
}
