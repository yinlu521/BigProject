using System;
using System.Collections.Generic;
using System.Text;

namespace iNethinkCMS.Model
{
    public class Model_iNethinkCMS_AdList
    {
        private int _id;
        private string _title;
        private string _indexpic;
        private string _linkurl;
        private string _desc;
        private int? _ordernum;
        private DateTime _addtime;
        private int _parentId;

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string IndexPic
        {
            get { return _indexpic; }
            set { _indexpic = value; }
        }
        public string Linkurl
        {
            get { return _linkurl; }
            set { _linkurl = value; }
        }
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public int? OrderNum
        {
            get { return _ordernum; }
            set { _ordernum = value; }
        }

        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
    }
}
