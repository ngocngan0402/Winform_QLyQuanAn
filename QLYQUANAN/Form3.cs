using QLYQUANAN.DAO;
using QLYQUANAN.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLYQUANAN
{
    public partial class Form3 : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public Form3(Account acc)
        {
            InitializeComponent();
            loginAccount = acc;
        }
        void ChangeAccount(Account acc)
        {
            txbUserName.Text = LoginAccount.UserName;
            txbDisplayname.Text = LoginAccount.DisplayName;
        }

        void UpdateAccountInfo()
        {
            string displayName = txbDisplayname.Text;
            string password = txbPassword.Text;
            string newPassword = txbNewpass.Text;
            string reenterPass = txbReenterpass.Text;
            string userName = txbUserName.Text;

            if (newPassword.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới");
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(userName,displayName,password,newPassword))
                {
                    MessageBox.Show("Cập nhật thành công");
                    if(updateAccount != null)
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu");
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; } 
            remove { updateAccount -= value;  }
        }
        private void btnExti_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
        public class AccountEvent : EventArgs
        {
            private Account acc;
            public Account Acc
            {
                get { return acc; }
                set { acc = value; }
            }
            public AccountEvent(Account acc)
            {
                this.acc = acc;
            }
        }

    }
}
