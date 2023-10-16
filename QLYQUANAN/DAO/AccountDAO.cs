using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLYQUANAN.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }
        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @username , @password";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {userName , passWord});

            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int resuft = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName, @displayName,@password, @newPassword", new object[] {userName,displayName,pass,newPass});
            return resuft > 0;
        }

        public DTO.Account GetAccountByUserName(string userName)
        {
           DataTable data=  DataProvider.Instance.ExecuteQuery("select * from account where user = '" + userName + "'");
            foreach(DataRow item in data.Rows)
            {
                return new DTO.Account(item);
            }
            return null;
        }
    }
}




    


