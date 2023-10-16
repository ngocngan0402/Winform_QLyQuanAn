using QLYQUANAN.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLYQUANAN
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            Load();

        }
        
        void Load()
        {
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFormDate.Value, dtpkFormDate.Value);
            LoadListFood();
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFormDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkFormDate.Value = dtpkFormDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instace.GetListBillByDate(checkIn, checkOut);
        }
        
        void LoadListFood()
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
        }

        private void btnViewbill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFormDate.Value, dtpkFormDate.Value);
        }
    }
        

    }


       



