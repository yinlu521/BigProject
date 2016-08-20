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
    /// Model_iNethinkCMS_Upload:实体类
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Upload
    {
        public Model_iNethinkCMS_Upload()
        {
        }
        #region Model
        private int _id;
        private int? _uptype;
        private int? _aid = 0;
        private int? _cid = 0;
        private string _dir;
        private string _ext;
        private DateTime? _time = DateTime.Now;
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
        /// 1,内容上传;2,自定义标签上传;3,自定义页面上传
        /// </summary>
        public int? UpType
        {
            set
            {
                _uptype = value;
            }
            get
            {
                return _uptype;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Aid
        {
            set
            {
                _aid = value;
            }
            get
            {
                return _aid;
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
        public string Ext
        {
            set
            {
                _ext = value;
            }
            get
            {
                return _ext;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Time
        {
            set
            {
                _time = value;
            }
            get
            {
                return _time;
            }
        }
        #endregion Model

    }
}

