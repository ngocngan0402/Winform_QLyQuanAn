using QLYQUANAN.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLYQUANAN.DAO
{
    public  class BillInfoDAO
    {
        private static BillInfoDAO instance;
        public static BillInfoDAO instacne 
        {
            get { if (instance == null) instacne = new BillInfoDAO(); return BillInfoDAO.instacne; }
           private  set { BillInfoDAO.instacne = value; }
        }
        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id )

        {
            List<BillInfo> ListBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE idBill = " + id);
            foreach(DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                ListBillInfo.Add(info);
            }
            return ListBillInfo;

        }

    }
}
