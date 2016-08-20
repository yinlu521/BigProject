using System;
using System.Collections.Generic;
using System.Text;

namespace iNethinkCMS.Model
{
    public class Model_iNethinkCMS_AdGroup
    {
        private int _id;

        private string _title;

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


    }
}
