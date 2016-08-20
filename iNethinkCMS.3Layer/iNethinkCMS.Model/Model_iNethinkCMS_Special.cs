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
    /// Model_iNethinkCMS_Special:实体类
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Special
    {
        public Model_iNethinkCMS_Special()
        {
        }
        #region Model
        private int _id;
        private string _specialname;
        private string _specialtitle;
        private string _specialkeyword;
        private string _specialdescription;
        private string _specialtemplate;
        private string _specialurl;
        private string _specialpic;
        private string _specialcontent;
        private int? _display;
        private int? _ordernum;
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
        public string SpecialName
        {
            set
            {
                _specialname = value;
            }
            get
            {
                return _specialname;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialTitle
        {
            set
            {
                _specialtitle = value;
            }
            get
            {
                return _specialtitle;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialKeyword
        {
            set
            {
                _specialkeyword = value;
            }
            get
            {
                return _specialkeyword;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialDescription
        {
            set
            {
                _specialdescription = value;
            }
            get
            {
                return _specialdescription;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialTemplate
        {
            set
            {
                _specialtemplate = value;
            }
            get
            {
                return _specialtemplate;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialUrl
        {
            set
            {
                _specialurl = value;
            }
            get
            {
                return _specialurl;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialPic
        {
            set
            {
                _specialpic = value;
            }
            get
            {
                return _specialpic;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialContent
        {
            set
            {
                _specialcontent = value;
            }
            get
            {
                return _specialcontent;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Display
        {
            set
            {
                _display = value;
            }
            get
            {
                return _display;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OrderNum
        {
            set
            {
                _ordernum = value;
            }
            get
            {
                return _ordernum;
            }
        }
        #endregion Model

    }
}

