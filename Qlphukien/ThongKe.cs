using Qlphukien.DAO;
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
    public partial class ThongKe : Form
    {
        SanPhamDao spDao = new SanPhamDao();
        NhanVienDao nvDao = new NhanVienDao();
        List<SPTK> list;
        public ThongKe()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            // custom datetime picker
            dateTimePickerFrom.CustomFormat = "dd/MM/yyyy";
            dateTimePickerto.CustomFormat = "dd/MM/yyyy";
            // set default time
            dateTimePickerFrom.Value = new DateTime(2018, 10, 01);
            dateTimePickerto.Value = DateTime.Now;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            list =  spDao.getAllSPNhap(dateTimePickerFrom.Value.ToString(),dateTimePickerto.Value.ToString());
            MessageBox.Show(dateTimePickerFrom.Value.ToString() + ":" + dateTimePickerto.Value.ToString());
            displayListTodgv(dgvsanpham, list);
            lbtotalmoney.Text = caculateTongtien() + " đ";
        }

        private int caculateTongtien()
        {
            int tongtien = 0;
            foreach(SPTK item in list)
            {
                tongtien += item.tongtien;
            }
            return tongtien;
        }

        private void displayListTodgv(DataGridView dgv, List<SPTK> list)
        {
            dgv.Rows.Clear();

            dgv.ColumnCount = 6;

            int i = 0;
            foreach (SPTK item in list)
            {

                dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = item.Tensp;
                dgv.Rows[i].Cells[1].Value = item.slnhap;
                dgv.Rows[i].Cells[2].Value = item.dongianhap + " đ";
                dgv.Rows[i].Cells[3].Value = item.tongtien + " đ";
                dgv.Rows[i].Cells[4].Value = nvDao.GetNameNhanVien(item.tennvnhap);
                dgv.Rows[i].Cells[5].Value = item.ngaynhap.Split(' ')[0];
             
                i++;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
