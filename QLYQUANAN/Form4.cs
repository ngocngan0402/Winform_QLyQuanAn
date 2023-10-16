﻿using QLYQUANAN.DAO;
using QLYQUANAN.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
            txbFoodname.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name",true, DataSourceUpdateMode.Never));
            txbFoodId.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
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

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodname.Text;
            int categoryID = (cbFoodcategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                if(insertFood != null)
                       insertFood(this,new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodname.Text;
            int categoryID = (cbFoodcategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodId.Text);
            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if(updateFood != null)
                    updateFood(this,new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodId.Text);
            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();
                if(deleteFood != null)
                        deleteFood(this,new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; } 
            remove{ insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
    }
        

    }


       



