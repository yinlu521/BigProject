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
    /// 友情链接
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Extend_Blogroll
    {
        public Model_iNethinkCMS_Extend_Blogroll()
        { }
        #region Model
        private int _id;
        private int? _blogrollclass;
        private string _blogrollname;
        private string _blogrollimg;
        private string _blogrollurl;
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
        /// 友情链接分类(来源于Dict表)
        /// </summary>
        public int? BlogrollClass
        {
            set { _blogrollclass = value; }
            get { return _blogrollclass; }
        }
        /// <summary>
        /// 友情链接名称
        /// </summary>
        public string BlogrollName
        {
            set { _blogrollname = value; }
            get { return _blogrollname; }
        }
        /// <summary>
        /// 友情链接Logo图片（非空则存在）
        /// </summary>
        public string BlogrollImg
        {
            set { _blogrollimg = value; }
            get { return _blogrollimg; }
        }
        /// <summary>
        /// 友情链接URL地址
        /// </summary>
        public string BlogrollUrl
        {
            set { _blogrollurl = value; }
            get { return _blogrollurl; }
        }
        /// <summary>
        /// 友情链接是否显示[0,不显示;1,显示]
        /// </summary>
        public int? Display
        {
            set { _display = value; }
            get { return _display; }
        }
        /// <summary>
        /// 友情链接排序权重
        /// </summary>
        public int? OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        #endregion Model

    }
}

