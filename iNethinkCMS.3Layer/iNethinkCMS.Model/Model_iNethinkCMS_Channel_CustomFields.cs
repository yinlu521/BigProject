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
    /// 自定义字段
    /// </summary>
    [Serializable]
    public partial class Model_iNethinkCMS_Channel_CustomFields
    {
        public Model_iNethinkCMS_Channel_CustomFields()
        { }
        #region Model
        private int _id;
        private string _cidlist;
        private string _customfieldsname;
        private string _customfieldskey;
        private string _customfieldstype;
        private string _customfieldsvalue;
        private int? _customfieldsrequired;
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
        /// 管理栏目ID列表
        /// </summary>
        public string CIDList
        {
            set { _cidlist = value; }
            get { return _cidlist; }
        }
        /// <summary>
        /// 自定义字段名称
        /// </summary>
        public string CustomFieldsName
        {
            set { _customfieldsname = value; }
            get { return _customfieldsname; }
        }
        /// <summary>
        /// 自定义字段标识
        /// </summary>
        public string CustomFieldsKey
        {
            set { _customfieldskey = value; }
            get { return _customfieldskey; }
        }
        /// <summary>
        /// 自定义字段类型
        /// </summary>
        public string CustomFieldsType
        {
            set { _customfieldstype = value; }
            get { return _customfieldstype; }
        }
        /// <summary>
        /// 自定义字段默认值
        /// </summary>
        public string CustomFieldsValue
        {
            set { _customfieldsvalue = value; }
            get { return _customfieldsvalue; }
        }
        /// <summary>
        /// 自定义字段是否必填[0,否;1,是]
        /// </summary>
        public int? CustomFieldsRequired
        {
            set { _customfieldsrequired = value; }
            get { return _customfieldsrequired; }
        }
        /// <summary>
        /// 自定义字段是否显示[0,不显示;1,显示]
        /// </summary>
        public int? Display
        {
            set { _display = value; }
            get { return _display; }
        }
        /// <summary>
        /// 排序权重
        /// </summary>
        public int? OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        #endregion Model

    }
}

