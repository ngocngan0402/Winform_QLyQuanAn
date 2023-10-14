﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLYQUANAN.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            private set => instance = value;
        }
        private MenuDAO() { }

        public List<QLYQUANAN.DTO.Menu> GetListMenuByTable(int id)
        {
            List<QLYQUANAN.DTO.Menu>  listMenu = new List<QLYQUANAN.DTO.Menu>();
            string query = "SELECT  f.name,bi.count, f.price,f.price*bi.count As totalPrice from dbo.BillInfo As bi, dbo.Bill AS b, dbo.Food AS f\r\nWHERE bi.idBill = b.id AND bi.idFood = f.id AND b.idTable = " +id;
            DataTable data = DataProvider.Instance.ExecuteQuery("query");
            foreach (DataRow item in data.Rows)
            {
                QLYQUANAN.DTO.Menu menu = new QLYQUANAN.DTO.Menu(item);
                listMenu.Add(menu);

            }
            return listMenu;
        }
    }
}