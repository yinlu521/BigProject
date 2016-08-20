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
    /// Model_iNethinkCMS_Content:实体类
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Content
    {
        public Model_iNethinkCMS_Content()
        {
        }
        #region Model
        private int _id;
        private int? _cid = 0;
        private int? _sid = 0;
        private string _title;
        private string _subtitle;
        private string _title_color;
        private string _title_style;
        private string _author;
        private string _source;
        private string _jumpurl;
        private string _keywords;
        private string _description;
        private string _indexpic;
        private int? _views = 0;
        private int? _commend;
        private int? _iscomment = 0;
        private int? _display = 0;
        private DateTime? _createtime = DateTime.Now;
        private DateTime? _modifytime = DateTime.Now;
        private int? _ordernum;
        private string _contents;
        private string _fieldsinfo;
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
        public int? Cid
        {
            set
            {
                _cid = value;
            }
            get
            {
                return _cid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sid
        {
            set
            {
                _sid = value;
            }
            get
            {
                return _sid;
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
        public string SubTitle
        {
            set
            {
                _subtitle = value;
            }
            get
            {
                return _subtitle;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title_Color
        {
            set
            {
                _title_color = value;
            }
            get
            {
                return _title_color;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title_Style
        {
            set
            {
                _title_style = value;
            }
            get
            {
                return _title_style;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Author
        {
            set
            {
                _author = value;
            }
            get
            {
                return _author;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Source
        {
            set
            {
                _source = value;
            }
            get
            {
                return _source;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Jumpurl
        {
            set
            {
                _jumpurl = value;
            }
            get
            {
                return _jumpurl;
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
        public string Indexpic
        {
            set
            {
                _indexpic = value;
            }
            get
            {
                return _indexpic;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Views
        {
            set
            {
                _views = value;
            }
            get
            {
                return _views;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Commend
        {
            set
            {
                _commend = value;
            }
            get
            {
                return _commend;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsComment
        {
            set
            {
                _iscomment = value;
            }
            get
            {
                return _iscomment;
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
        public DateTime? Createtime
        {
            set
            {
                _createtime = value;
            }
            get
            {
                return _createtime;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Modifytime
        {
            set
            {
                _modifytime = value;
            }
            get
            {
                return _modifytime;
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
        /// <summary>
        /// 
        /// </summary>
        public string Contents
        {
            set
            {
                _contents = value;
            }
            get
            {
                return _contents;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FieldsInfo
        {
            set
            {
                _fieldsinfo = value;
            }
            get
            {
                return _fieldsinfo;
            }
        }
        #endregion Model

    }
}

