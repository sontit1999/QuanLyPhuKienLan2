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
    public partial class QLNhanVien : Form
    {
        NhanVienDao nvDao = new NhanVienDao();
        public QLNhanVien()
        {

            InitializeComponent();
            // ẩn mật khẩu
            txtMatkhau.PasswordChar = '\u25CF';
            //đặt giá trị mặc định cho combobox Quyền
            cbRole.SelectedIndex = 0;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QLnhanvien_Load(object sender, EventArgs e)
        {
            // hiển thị ra giữa màn hình
            this.CenterToScreen();

            //  tùy chỉnh kiểu hiển thị  của cái ô chọn ngày sinh ý dd là ngày, MM là tháng, yyyy là năm.
            dateTimePickerNgaysinh.CustomFormat = "dd/MM/yyyy";

            // Gán giá trị mặc định cho cái chọn ngày khi đã chạy chuong trình
            dateTimePickerNgaysinh.Value = new DateTime(1997, 10, 10);

            //gán ngày lớn  nhất cho ô chọn ngày sinh là ngày hiện tại khi chưa chạy
            dateTimePickerNgaysinh.MaxDate = DateTime.Now;

            // load tất cả nhân viên to datagridview
            displayNhanVien(dgvNhanVien, nvDao.getAllNhanVien());
        }

         // kiểm tra số điện thoại
        private void txtSodt_TextChanged(object sender, EventArgs e)
        {
            // Chỉ dc nhập số từ 0 -> 9
            if (System.Text.RegularExpressions.Regex.IsMatch(txtSodt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtSodt.Text = "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string manv = txtManv.Text;
            string tennv = txtTenNv.Text;
            string gioitinh = "";
            if (rbNam.Checked)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            string ngaysinh = dateTimePickerNgaysinh.Value.ToString("yyyy-MM-dd");
            string diachi = txtDiachi.Text;
            string sdt = txtSodt.Text;
            string matkhau = txtMatkhau.Text;
            string role = "";

            if(cbRole.SelectedIndex == 0)
            {
                role = "nhan vien";
            }
            else
            {
                role = "admin";
            }
            if(manv.Equals("") || tennv.Equals("") || gioitinh.Equals("") || ngaysinh.Equals("") || diachi.Equals("") || sdt.Equals("") || matkhau.Equals("") || role.Equals(""))
            {
                MessageBox.Show("Không được bỏ trống trường nào !!!");
            }
            else
            {
                NhanVien nvResult = nvDao.CheckPhoneExist(sdt); // có sdt đó hay chua

                NhanVien nv = new NhanVien(manv, tennv, gioitinh, ngaysinh, diachi, sdt, matkhau, role);

                NhanVien nvTimdc = nvDao.CheckNhanVien(nv.MaNv); // có mã nv đó chua
                if(nvTimdc == null) //  nếu mã nv đó chua có 
                {
                    if (nvResult == null) // nếu sdt chua có
                    {
                        nvDao.AddNhanVien(nv); // thêm nv
                        displayNhanVien(dgvNhanVien, nvDao.getAllNhanVien()); // hiển thị
                        clearAllFiled(); // xóa nội dung trong texbox sau khi thêm
                        MessageBox.Show("Đã thêm nhân viên !");
                    }
                    else
                    {
                        MessageBox.Show("Nhân viên " + nvResult.TenNv + " đã sử dụng số điện thoại này !! Vui lòng kiểm tra lại");
                    }
                }
                else
                {
                    MessageBox.Show("Đã tồn tại nhân viên có mã nhân viên: " + nv.MaNv);
                }
            }
        }
        // hàm xóa nội dung trong các textbox 
        private void clearAllFiled()
        {
            txtManv.Text = "";
            txtTenNv.Text = "";
            txtDiachi.Text = "";
            txtSodt.Text = "";
            txtMatkhau.Text = "";
        }

        // hàm hiển thị ds nhân viên lên datagridview
        public void displayNhanVien(DataGridView dgv , List<NhanVien> list)
        {
            dgv.Rows.Clear();
            dgv.ColumnCount = 8;

            int i = 0;
            foreach (NhanVien item in list)
            {
                dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = item.MaNv;
                dgv.Rows[i].Cells[1].Value = item.TenNv;
                dgv.Rows[i].Cells[2].Value = item.Gioitinh;
                dgv.Rows[i].Cells[3].Value = Convert.ToDateTime(item.NgaySinh.ToString()).ToString("dd-MM-yyyy");
                dgv.Rows[i].Cells[4].Value = item.DiaChi;
                dgv.Rows[i].Cells[5].Value = item.SoDienthoai;
                dgv.Rows[i].Cells[6].Value = convertPasswordToHidden(item.MatKhau);
                dgv.Rows[i].Cells[7].Value = item.Role;
                i++;
            }
        }
        public string convertPasswordToHidden(string pass)
        {
            string hiddenStr = "";

            for(int i = 0; i < pass.Length; i++)
            {
                hiddenStr = hiddenStr + "*";
                    
            }
            return hiddenStr;
        }
        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            string manv = txtManv.Text;
            string tennv = txtTenNv.Text;
            string gioitinh = "";
            if (rbNam.Checked)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            string ngaysinh = dateTimePickerNgaysinh.Value.ToString("yyyy-MM-dd");
            string diachi = txtDiachi.Text;
            string sdt = txtSodt.Text;
            string matkhau = txtMatkhau.Text;
            string role = "";
            if (cbRole.SelectedIndex == 0)
            {
                role = "nhan vien";
            }
            else
            {
                role = "admin";
            }
            if (manv.Equals("") || tennv.Equals("") || gioitinh.Equals("") || ngaysinh.Equals("") || diachi.Equals("") || sdt.Equals("") || matkhau.Equals("") || role.Equals(""))
            {
                MessageBox.Show("Không được bỏ trống trường nào !!!");
            }
            else
            {
                NhanVien nv = new NhanVien(manv, tennv, gioitinh, ngaysinh, diachi, sdt, matkhau, role);
                NhanVien nvTimdc = nvDao.CheckNhanVien(nv.MaNv);
                if (nvTimdc == null) // mã nv chua có 
                {
                    MessageBox.Show("Không có nhân viên nào có mã  !" + nv.MaNv);
                }
                else
                {
                    nvDao.UpdateNhanVien(nv); // sửa thông tin nv
                    displayNhanVien(dgvNhanVien, nvDao.getAllNhanVien()); // hiển thị 
                    clearAllFiled(); // xóa thông tin trong texbox
                    MessageBox.Show("Đã cập nhật thông tin nhân viên "  + nv.TenNv);
                }
            }
        }

        // Hàm đổ dữ liệu từ datagview xuông các textbox
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexrow = e.RowIndex;
            if (indexrow >= 0)
            {
                txtManv.Text = dgvNhanVien.Rows[indexrow].Cells[0].Value.ToString();
                txtTenNv.Text = dgvNhanVien.Rows[indexrow].Cells[1].Value.ToString();         
                txtDiachi.Text = dgvNhanVien.Rows[indexrow].Cells[4].Value.ToString();
                txtSodt.Text = dgvNhanVien.Rows[indexrow].Cells[5].Value.ToString();
                txtMatkhau.Text = dgvNhanVien.Rows[indexrow].Cells[6].Value.ToString();
            }  
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string manv = txtManv.Text;
            if (manv.Equals(""))
            {
                MessageBox.Show("Phải nhập vào mã nhân viên để xóa !!");
            }
            else
            {
                NhanVien nv = nvDao.CheckNhanVien(manv);
                if (nv == null) // chua có mã nv này
                {
                    MessageBox.Show("Không tồn tại nhân viên này!");
                }
                else
                {                  
                    DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa nhân viên " + nv.TenNv,"Xóa nhân viên",MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        nvDao.DeleteNhanVien(manv); // xóa nv
                        displayNhanVien(dgvNhanVien, nvDao.getAllNhanVien()); // hiển thị lại
                        clearAllFiled(); // xóa thông tin trong texbox
                        MessageBox.Show("Đã xóa nhân viên " + nv.TenNv + " !");                      
                    }
                }
               
            }
        }

        // Hàm tìm kiếm 
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTukhoa.Text;
         
            if(tukhoa.Equals(""))
            {
                MessageBox.Show("Phải nhập mã nhân viên hoặc tên nhân viên vào ô để tìm kiếm !!!");
            }
            else
            {
                NhanVien nvcantim = new NhanVien(tukhoa, tukhoa, "Nam", "2020-10-10", "Can Tho", "0394121584", "admin", "nhan vien");
                List<NhanVien> listNVtimdc = nvDao.SearchNhanVien(nvcantim);
                if (listNVtimdc.Count > 0) // có nv
                {
                    // hiển thị ds
                    displayNhanVien(dgvNhanVien, listNVtimdc);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào !!!!");
                }
            }
        }

        // làm mới lại ds nv
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            displayNhanVien(dgvNhanVien, nvDao.getAllNhanVien());
        }
    }
}
