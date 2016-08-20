
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace iNethinkCMS.BLL
{
    public class BLL_iNethinkCMS_AdGroup
    {
        iNethinkCMS.Model.Model_iNethinkCMS_AdGroup mode = new Model.Model_iNethinkCMS_AdGroup();

        iNethinkCMS.DAL.DAL_iNethinkCMS_AdGroup dal = new DAL.DAL_iNethinkCMS_AdGroup();
        public int GetMaxID() {
            return dal.GetMaxID();
        }
        public bool Exists(int ID)
        { return dal.Exists(ID); }

        public bool Add(iNethinkCMS.Model.Model_iNethinkCMS_AdGroup model)
        { return dal.Add(model); }

        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_AdGroup model)
        { return dal.Update(model); }

        public bool Delete(int ID)
        { return dal.Delete(ID); }

        public iNethinkCMS.Model.Model_iNethinkCMS_AdGroup GetMode(int Id)
        { return dal.GetMode(Id); }

        public DataSet GetList(string where)
        {
            return dal.GetList(where);
        }

        public int GetRecordCount(string strWhere)
        { return dal.GetRecordCount(strWhere); }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        { return dal.GetListByPage(strWhere, orderby, startIndex, endIndex); }
    }
}
