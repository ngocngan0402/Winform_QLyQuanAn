using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLYQUANAN.DTO
{
   public  class Bill
    {
        public Bill(int id, DateTime? dateCheckin, DateTime? dateCheckOut, int status ,int discount =0)
        {
            this.ID= id;
            this.dateChekIn = dateCheckin;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID =(int) row["id"];
            this.dateChekIn = (DateTime?)row["dateCheckin"];
            var dateCheckOutTemp = row["dateCheckOut"];
            if( dateCheckOutTemp.ToString() != "" ) 
                this.DateCheckOut = (DateTime?) dateCheckOutTemp;

            this.Status = (int)row["status"];

            if (row["discount"].ToString() != "")
                this.discount = (int)row["discount"];
        }
        private int discount;
        public int Discount
        {
            get { return discount; }
            set{ this.discount = value; }
        }
        private int Status
        {
            get { return Status; }
            set { status = Value; }
        }
        private DateTime? DateCheckOut;
        private DateTime? dateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }

        }
        private DateTime? dateChekIn
        {
            get { return dateChekIn; }
            set { dateChekIn = value; }

        }

        private int id;
        private object status;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public object Value { get; private set; }
        public int Id { get; internal set; }
    }
}
