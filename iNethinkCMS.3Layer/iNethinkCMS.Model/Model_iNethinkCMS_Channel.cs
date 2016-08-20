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
    /// Model_iNethinkCMS_Channel:实体类
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Channel
    {
        public Model_iNethinkCMS_Channel()
        {
        }
        #region Model
        private int _id;
        private int? _mold;
        private int? _cid = 0;
        private int? _fatherid = 0;
        private string _childid;
        private string _childids;
        private int? _deeppath = 0;
        private string _name;
        private int? _ordernum = 0;
        private string _domain;
        private int? _outsidelink = 0;
        private string _templatechannel;
        private string _templateclass;
        private string _templateview;
        private string _picture;
        private string _contents;
        private string _keywords;
        private string _description;
        private int? _display = 0;
        private string _Ename;
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
        public int? Mold
        {
            set { _mold = value; }
            get { return _mold; }
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
        public int? FatherID
        {
            set
            {
                _fatherid = value;
            }
            get
            {
                return _fatherid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChildID
        {
            set
            {
                _childid = value;
            }
            get
            {
                return _childid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChildIDs
        {
            set
            {
                _childids = value;
            }
            get
            {
                return _childids;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DeepPath
        {
            set
            {
                _deeppath = value;
            }
            get
            {
                return _deeppath;
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
        public string Domain
        {
            set
            {
                _domain = value;
            }
            get
            {
                return _domain;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OutSideLink
        {
            set
            {
                _outsidelink = value;
            }
            get
            {
                return _outsidelink;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Templatechannel
        {
            set
            {
                _templatechannel = value;
            }
            get
            {
                return _templatechannel;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Templateclass
        {
            set
            {
                _templateclass = value;
            }
            get
            {
                return _templateclass;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Templateview
        {
            set
            {
                _templateview = value;
            }
            get
            {
                return _templateview;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Picture
        {
            set
            {
                _picture = value;
            }
            get
            {
                return _picture;
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

        public string Ename
        {
            set { _Ename = value; }
            get {
                return _Ename;
            }
        }
        #endregion Model

    }
}

