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
namespace iNethinkCMS.Model
{
    /// <summary>
    /// Model_iNethinkCMS_Custom_Tags:实体类
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Custom_Tags
    {
        public Model_iNethinkCMS_Custom_Tags()
        {
        }
        #region Model
        private int _id;
        private string _name;
        private string _remark;
        private string _code;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set
            {
                _remark = value;
            }
            get
            {
                return _remark;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set
            {
                _code = value;
            }
            get
            {
                return _code;
            }
        }
        #endregion Model

    }
}

