﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLYQUANAN.DTO
{
    public class Food
    {
        public Food(int id,string name,int categoryID,float price)
        {
        this.ID = id;
            this.Name = name;
            this.CategoryID = categoryID;
            this.Price = price;
        }
        public Food(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.Name = (string)row["Name"];
            this.categoryID = (int)row["idcategory"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }
        private float price;
        public float Price
        {
            get { return price; }
            set { price = value; } 
        }
        private int categoryID;
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int id;
        public int ID 
        { 
            get { return id; }
            set { id = value; }
        }
    }
}