using QLYQUANAN.DTO;
using System.Collections.Generic;
using System.Data;

namespace QLYQUANAN.DAO
{
    public class MenuDAOBase
    {

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            string query = "SELECT  f.name,bi.count, f.price,f.price*bi.count As totalPrice from dbo.BillInfo As bi, dbo.Bill AS b, dbo.Food AS f\r\nWHERE bi.idBill = b.id AND bi.idFood = f.id AND b.idTable = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery("query");
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);

            }
            return listMenu;
        }
    }
}