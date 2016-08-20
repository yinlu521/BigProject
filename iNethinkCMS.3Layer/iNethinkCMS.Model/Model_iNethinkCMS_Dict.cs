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
    /// 数据字典
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Dict
    {
        public Model_iNethinkCMS_Dict()
        { }
        #region Model
        private int _id;
        private int? _dicttype;
        private string _dictname;
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
        /// 数据字典类型
        /// </summary>
        public int? DictType
        {
            set { _dicttype = value; }
            get { return _dicttype; }
        }
        /// <summary>
        /// 数据字典名称
        /// </summary>
        public string DictName
        {
            set { _dictname = value; }
            get { return _dictname; }
        }
        /// <summary>
        /// 数据字典是否启用[0,不启用;1,启用]
        /// </summary>
        public int? Display
        {
            set { _display = value; }
            get { return _display; }
        }
        /// <summary>
        /// 数据字典排序权重
        /// </summary>
        public int? OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        #endregion Model

    }
}

