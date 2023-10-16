using QLYQUANAN.DAO;
using QLYQUANAN.DTO;
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
        BindingSource foodList = new BindingSource();
        public Form4()
        {
            InitializeComponent();
            Load();

        }
        
        void Load()
        {
            dtgvFood.DataSource = foodList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFormDate.Value, dtpkFormDate.Value);
            LoadListFood();
            LoadCategoryIntoCombobox(cbFoodcategory);
            AddFoodBinding();
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
        void AddFoodBinding()
        {
            txbFoodname.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name"));
            txbFoodId.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID"));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price"));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        private void btnViewbill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFormDate.Value, dtpkFormDate.Value);
        }

        private void txbFoodId_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

               
                Category category = CategoryDAO.Instance.GetCategoryByID(id);
                cbFoodcategory.SelectedItem = category;

                int index = -1;
                int i = 0;
                foreach(Category item in cbFoodcategory.Items)
                {
                    if(item.ID == category.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                cbFoodcategory.SelectedIndex = index;
            }
            
        }
    }
        

    }


       



