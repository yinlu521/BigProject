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
    /// 留言信息
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Plugs_Guestbook
    {
        public Model_iNethinkCMS_Plugs_Guestbook()
        { }
        #region Model
        private int _id;
        private string _guestbookusername;
        private string _guestbookuserip;
        private string _guestbookcompany;
        private string _guestbookaddress;
        private string _guestbooktel;
        private string _guestbookemail;
        private string _guestbookqq;
        private string _guestbookcontent;
        private DateTime? _guestbooktime;
        private string _replyusername;
        private string _replycontent;
        private DateTime? _replytime;
        private int? _display;
        private int? _ordernum;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 留言人姓名
        /// </summary>
        public string GuestbookUserName
        {
            set { _guestbookusername = value; }
            get { return _guestbookusername; }
        }
        /// <summary>
        /// 留言人IP地址
        /// </summary>
        public string GuestbookUserIP
        {
            set { _guestbookuserip = value; }
            get { return _guestbookuserip; }
        }
        /// <summary>
        /// 留言人公司
        /// </summary>
        public string GuestbookCompany
        {
            set { _guestbookcompany = value; }
            get { return _guestbookcompany; }
        }
        /// <summary>
        /// 留言人地址
        /// </summary>
        public string GuestbookAddress
        {
            set { _guestbookaddress = value; }
            get { return _guestbookaddress; }
        }
        /// <summary>
        /// 留言人电话
        /// </summary>
        public string GuestbookTel
        {
            set { _guestbooktel = value; }
            get { return _guestbooktel; }
        }
        /// <summary>
        /// 留言人电子邮箱
        /// </summary>
        public string GuestbookEmail
        {
            set { _guestbookemail = value; }
            get { return _guestbookemail; }
        }
        /// <summary>
        /// 留言人QQ
        /// </summary>
        public string GuestbookQQ
        {
            set { _guestbookqq = value; }
            get { return _guestbookqq; }
        }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string GuestbookContent
        {
            set { _guestbookcontent = value; }
            get { return _guestbookcontent; }
        }
        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime? GuestbookTime
        {
            set { _guestbooktime = value; }
            get { return _guestbooktime; }
        }
        /// <summary>
        /// 留言回复人
        /// </summary>
        public string ReplyUserName
        {
            set { _replyusername = value; }
            get { return _replyusername; }
        }
        /// <summary>
        /// 留言回复内容
        /// </summary>
        public string ReplyContent
        {
            set { _replycontent = value; }
            get { return _replycontent; }
        }
        /// <summary>
        /// 留言回复时间
        /// </summary>
        public DateTime? ReplyTime
        {
            set { _replytime = value; }
            get { return _replytime; }
        }
        /// <summary>
        /// 留言是否审核通过[0,未审核;1,审核通过]
        /// </summary>
        public int? Display
        {
            set { _display = value; }
            get { return _display; }
        }
        /// <summary>
        /// 留言排序权重
        /// </summary>
        public int? OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        #endregion Model

    }
}

