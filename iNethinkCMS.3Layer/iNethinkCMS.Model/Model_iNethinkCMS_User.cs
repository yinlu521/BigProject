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
    /// Model_iNethinkCMS_User:实体类
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_User
    {
        public Model_iNethinkCMS_User()
        {
        }
        #region Model
        private int _id;
        private int? _usertype;
        private string _username;
        private string _userpass;
        private string _usertruename;
        private string _useremail;
        private string _userpower;
        private string _userchannelpower;
        private DateTime? _userregtime;
        private string _securitycode;
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
        public int? UserType
        {
            set
            {
                _usertype = value;
            }
            get
            {
                return _usertype;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set
            {
                _username = value;
            }
            get
            {
                return _username;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPass
        {
            set
            {
                _userpass = value;
            }
            get
            {
                return _userpass;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserTrueName
        {
            set
            {
                _usertruename = value;
            }
            get
            {
                return _usertruename;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserEmail
        {
            set
            {
                _useremail = value;
            }
            get
            {
                return _useremail;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPower
        {
            set
            {
                _userpower = value;
            }
            get
            {
                return _userpower;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserChannelPower
        {
            set
            {
                _userchannelpower = value;
            }
            get
            {
                return _userchannelpower;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UserRegTime
        {
            set
            {
                _userregtime = value;
            }
            get
            {
                return _userregtime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SecurityCode
        {
            set
            {
                _securitycode = value;
            }
            get
            {
                return _securitycode;
            }
        }

        #endregion Model

    }
}

