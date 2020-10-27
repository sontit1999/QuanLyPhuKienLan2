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
    public partial class QLHoaDon : Form
    {
        List<SanPham> list = new List<SanPham>();
        HoaDonDao hdDao = new HoaDonDao();
        ChiTietHoaDonDAO ChiTietHoaDonDAO = new ChiTietHoaDonDAO();
        SanPhamDao spDao = new SanPhamDao();
        NhanVienDao nvDao = new NhanVienDao();
        string mahdSelected = null;
        int indexSanPhamSelected = -5;
        public QLHoaDon()
        {
            InitializeComponent();
            btnCapnhat.Hide();
            btnXoa.Hide();
        }

        private void QliHoadon_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            displayHD(dgvHoaDon, hdDao.getAllHD());
            // display comboboxSanPham
            cbSanPham.DataSource = spDao.getAllSP();
            cbSanPham.DisplayMember = "TenSP";
            cbSanPham.ValueMember = "MaSP";
        }

        private void btnRefreshHD_Click(object sender, EventArgs e)
        {
            displayHD(dgvHoaDon, hdDao.getAllHD());
        }

        private void displayHD(DataGridView dgv, List<HoaDon> list)
        {
            dgv.Rows.Clear();

            dgv.ColumnCount = 4;

            int i = 0;
            foreach (HoaDon item in list)
            {
                dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = item.MaHD;
                dgv.Rows[i].Cells[1].Value = nvDao.GetNameNhanVien(item.MaNV);
                dgv.Rows[i].Cells[2].Value = item.NgayLap.Split(null)[0];
                dgv.Rows[i].Cells[3].Value = item.TongTienHD + " VND";
                i++;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtTenLoaisp_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            QLSanPham qLSanPham = new QLSanPham(this);
            qLSanPham.Show();
        }
        public int checkSPinListExist(string masp)
        {
           for(int i=0;i<list.Count;i++)
            {
                if (list[i].MaSP.Equals(masp))
                {
                    return i;
                }
            }
            return -1;
        }
        private void disPlayListToGDV(DataGridView dgv,List<SanPham> list)
        {
            dgv.Rows.Clear();

            dgv.ColumnCount = 4;

            int i = 0;
            foreach (SanPham item in list)
            {

                dgv.Rows.Add();
    
                dgv.Rows[i].Cells[0].Value = spDao.getNameProduct(item.MaSP);
                dgv.Rows[i].Cells[1].Value = item.SoLuong;
                dgv.Rows[i].Cells[2].Value = (item.SoLuong * item.GiaNhap) + " VND"; 
                i++;
            }
        }
        private void disPlayListCTHDToGDV(DataGridView dgv, List<ChiTietHoaDon> list)
        {
            dgv.Rows.Clear();

            dgv.ColumnCount = 4;

            int i = 0;
            foreach (ChiTietHoaDon item in list)
            {

                dgv.Rows.Add();
               
                dgv.Rows[i].Cells[0].Value = spDao.getNameProduct(item.MaSP);
                dgv.Rows[i].Cells[1].Value = item.SoLuongSP;
                dgv.Rows[i].Cells[2].Value = (item.SoLuongSP * spDao.getPriceProduct(item.MaSP)) + " VND";
                i++;
            }
        }
        public void reFreshComboboxSanPham()
        {
            cbSanPham.DataSource = spDao.getAllSP();
            cbSanPham.DisplayMember = "TenSP";
            cbSanPham.ValueMember = "MaSP";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(list.Count == 0)
            {
                MessageBox.Show("Phải nhập ít nhất 1 sản phẩm!");
            }
            else
            {
                Random rnd = new Random();
                int number = rnd.Next(1,50000);
                string mahd = "HD" + number;
                HoaDon hd = new HoaDon(mahd, SingleToneUser.nv.MaNv, DateTime.Now.ToString("yyyy-MM-dd"), getTongtien());
                hdDao.AddHoaDon(hd);
                foreach (SanPham item in list)
                {
                    ChiTietHoaDon cthd = new ChiTietHoaDon(mahd, item.MaSP, item.SoLuong, item.SoLuong * item.GiaNhap);
                    cthd.MaCTHD = "CTHD" + rnd.Next(1, 50000);
                    spDao.UpdateSLSanPham(item.MaSP, item.SoLuong);
                    ChiTietHoaDonDAO.AddCTHoaDon(cthd);
                }

                displayHD(dgvHoaDon, hdDao.getAllHD());
                list.Clear();
                disPlayListToGDV(dgvDSSP, list); 
                MessageBox.Show("thêm hóa đơn Thành công ");
            }
        }

        private int getTongtien()
        {
            int tongtien = 0;
            foreach (SanPham item in list)
            {

                tongtien += item.SoLuong * item.GiaNhap;
            }
            return tongtien;
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
           if(index > 0 && index < dgvHoaDon.RowCount-1)
            {
                string mhd = dgvHoaDon.Rows[index].Cells[0].Value.ToString();
                mahdSelected = mhd;
                disPlayListCTHDToGDV(dgvDSSP, ChiTietHoaDonDAO.getChiTietHDFromMaHD(mhd));
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnAddSPtoHoaDon_Click(object sender, EventArgs e)
        {
            SanPham sp = spDao.CheckSP(cbSanPham.SelectedValue.ToString());
            if (sp != null)
            {
                int index = checkSPinListExist(sp.MaSP);
                if (index >= 0)
                {

                    list[index].SoLuong = list[index].SoLuong + Convert.ToInt32(txtSoluong.Text);
                }
                else
                {
                    if (txtSoluong.Text.Equals(""))
                    {
                        MessageBox.Show("Số lượng phải dc nhập và là số !!");
                    }
                    else
                    {
                        sp.SoLuong = Convert.ToInt32(txtSoluong.Text);
                        list.Add(sp);
                    }
                    
                }


            }
            disPlayListToGDV(dgvDSSP, list);
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTuKhoa.Text;
            if (keyword.Equals(""))
            {
                MessageBox.Show("Phải nhập nội dung vào ô tìm kiếm( tên sp/mo ta )");
            }
            else
            {
                displayHD(dgvHoaDon, hdDao.SearchHD(keyword));
            }
        }

        private void dgvDSSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexSanPhamSelected = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(indexSanPhamSelected>=0 && indexSanPhamSelected < list.Count)
            {
                list.RemoveAt(indexSanPhamSelected);
                disPlayListToGDV(dgvDSSP, list);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (indexSanPhamSelected >= 0 && indexSanPhamSelected < list.Count)
            {
                list[indexSanPhamSelected].SoLuong = Convert.ToInt32(txtSoluong.Text);
                disPlayListToGDV(dgvDSSP, list);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            disPlayListToGDV(dgvDSSP, list);
        }
    }
}
