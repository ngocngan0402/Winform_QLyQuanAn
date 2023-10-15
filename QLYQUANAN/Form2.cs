using QLYQUANAN.DAO;
using QLYQUANAN.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLYQUANAN
{
    public partial class Form2 : Form
    {
        private object environment;

        public Form2()
        {
            InitializeComponent();
            LoadTable();
            loadCategory();
        }
        #region  Method
        void loadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        void loadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tablesList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tablesList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + environment + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                switch(item.Status)
                {
                    case " Trống ":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:btn.BackColor = Color.Tan; break;
                }


                flpTable.Controls.Add(btn);

            }
        }

        #endregion

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<QLYQUANAN.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (QLYQUANAN.DTO.Menu item in listBillInfo) 
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbTotalPrice.Text = totalPrice.ToString("c");
            
        }

        #region Events
        private void btn_Click(object sender, EventArgs e)
        {
            int TableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(TableID);
        }
        internal void show()
        {
            throw new NotImplementedException();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void addminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();
        }
        #endregion

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instace.GetUncheckBillIDByTableID(table.ID);

            if(idBill != -1)
            {
            if(MessageBox.Show("Bạn có chắc thanh toán hóa đơn cho bàn "+ table.Name,"Thông báo ",MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instace.CheckOut(idBill);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            loadFoodListByCategoryID(id);
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instace.GetUncheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value; 
            if(idBill == -1) 
            { 
                BillDAO.Instace.InserBill(table.ID);
                BillInfoDAO.instacne.InserBillInfo(BillDAO.Instace.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.instacne.InserBillInfo(idBill, foodID, count);
            }
            ShowBill(table.ID);

        }
    }
}

