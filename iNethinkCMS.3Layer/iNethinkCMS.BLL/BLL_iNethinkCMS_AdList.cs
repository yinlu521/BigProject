using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace iNethinkCMS.BLL
{
    public class BLL_iNethinkCMS_AdList
    {
        iNethinkCMS.Model.Model_iNethinkCMS_AdList model = new Model.Model_iNethinkCMS_AdList();
        iNethinkCMS.DAL.DAL_iNethinkCMS_AdList dal = new DAL.DAL_iNethinkCMS_AdList();
        public bool Exists(int ID)
        { return dal.Exists(ID); }

        public bool Add(iNethinkCMS.Model.Model_iNethinkCMS_AdList model)
        { return dal.Add(model); }

        public bool Update(iNethinkCMS.Model.Model_iNethinkCMS_AdList model)
        {
            return dal.Update(model);
        }

        public bool Delete(int ID)
        { return dal.Delete(ID); }

        public bool DeleteList(string IDlist)
        { return dal.DeleteList(IDlist); }

        public iNethinkCMS.Model.Model_iNethinkCMS_AdList GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public DataSet GetList(string strWhere)
        { return dal.GetList(strWhere);}


        public DataSet GetList(int Top, string strWhere, string filedOrder)
        { return dal.GetList(Top, strWhere, filedOrder); }

        public int GetRecordCount(string strWhere)
        { return dal.GetRecordCount(strWhere); }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        { return dal.GetListByPage(strWhere,orderby,startIndex,endIndex); }
    }
}
