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
    /// Model_iNethinkCMS_Custom_Pages:实体类
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Custom_Pages
    {
        public Model_iNethinkCMS_Custom_Pages()
        {
        }
        #region Model
        private int _id;
        private string _title;
        private string _templatepath;
        private string _dir;
        private string _keywords;
        private string _description;
        private string _html;
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
        public string Title
        {
            set
            {
                _title = value;
            }
            get
            {
                return _title;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TemplatePath
        {
            set
            {
                _templatepath = value;
            }
            get
            {
                return _templatepath;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dir
        {
            set
            {
                _dir = value;
            }
            get
            {
                return _dir;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Keywords
        {
            set
            {
                _keywords = value;
            }
            get
            {
                return _keywords;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set
            {
                _description = value;
            }
            get
            {
                return _description;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Html
        {
            set
            {
                _html = value;
            }
            get
            {
                return _html;
            }
        }
        #endregion Model

    }
}

