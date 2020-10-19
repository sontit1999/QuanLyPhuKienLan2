using Qlphukien.model;
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
    public partial class MenuForm : Form
    {
        NhanVien nv;
        public MenuForm()
        {
            InitializeComponent();
        }

        // Kiểm tra quyền 
        public MenuForm(NhanVien nhanvien)
        {
            InitializeComponent();

            this.nv = nhanvien;
            if (nv.Role.Equals("admin")) // khác quyền admin
            {
               
                btnLoaiSP.Enabled = false;
                btnSanPham.Enabled = false;
            }
            else
            {
                btnNhanVien.Enabled = false;
            }
        }
        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            if (nv != null)
            {
                lblChaomung.Text = "Chào mừng " + nv.TenNv + " đến với hệ thống quản lý phụ kiện <3";
            }
        }

        // nút quan ly nv
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            QLNhanVien qLnhanvien = new QLNhanVien();
            qLnhanvien.Show();
        }

        
        private void btnLoaiSP_Click(object sender, EventArgs e)
        {
            QLLoaiSanPham qLloaiSP = new QLLoaiSanPham();
            qLloaiSP.Show();
        }

        
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            QLSanPham qLSanPham = new QLSanPham(null);
            qLSanPham.isBack = false;
            qLSanPham.Show();
        }

        
        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            QLHoaDon qliHoadon = new QLHoaDon();
            qliHoadon.Show();
        }

        
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
