using Qlphukien.DAO;
using Qlphukien.model;
using Qlphukien.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qlphukien
{
    public partial class Form1 : Form
    {
        NhanVienDao nvDao = new NhanVienDao();
        string password = "";
        public Form1()
        {
            InitializeComponent();

            // ẩn mật khẩu khi nhập
            txtMatKhau.PasswordChar = '\u25CF';
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // hiển thị form ra giữa màn hình
        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string taikhoan = txtTaikhoan.Text;
            string matkhau = txtMatKhau.Text;
            if(taikhoan.Equals("") || matkhau.Equals(""))
            {
                MessageBox.Show("Không được bỏ trống trường nào ?");
            }
            else
            {
                NhanVien nv = nvDao.getNhanVien(taikhoan, matkhau);
                if (nv == null)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác !!");
                }
                else
                {
                    SingleToneUser.nv = nv;
                    txtMatKhau.Text = "";
                    txtTaikhoan.Text = "";
                    MenuForm menuForm = new MenuForm(nv);
                    menuForm.Show();
                }
            }
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked)
            {
                //Hides Textbox password
                txtMatKhau.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            }
            else
            {
               
                txtMatKhau.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            password = txtMatKhau.Text;
        }
    }
}
