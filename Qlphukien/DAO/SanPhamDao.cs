using Qlphukien.model;
using Qlphukien.utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qlphukien.DAO
{
    class SanPhamDao // DAO là Data Access Object nghĩa là truy xuất dữ liệu đối tượng.thường ng ta đặt tên thế

    {
        SqlConnection con;
        public SanPhamDao()
        {
            con = UtilsConnect.getConnection();
        }
        // hàm lấy tất cả sản phẩm
        public List<SanPham> getAllSP()
        {
            List<SanPham> list = new List<SanPham>();
            SanPham SanPham = null;
            con.Open();
            string sql = "select * from SanPham";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                SanPham = new SanPham(dr["MaSanPham"].ToString(), dr["MaLoaiSanPham"].ToString(), dr["TenSanPham"].ToString(), Convert.ToInt32(dr["SoLuong"]), Convert.ToInt32(dr["GiaNhap"]), Convert.ToInt32(dr["GiaBan"]), dr["ThoiGianBaoHanh"].ToString(), dr["DonVi"].ToString(), dr["MoTa"].ToString());
                list.Add(SanPham);
            }
            con.Close();
            return list;
        }
        // hàm lấy tất cả sản phẩm theo mã loại sản phẩm
        public List<SanPham> getAllSPByMaloai(string maloaisp)
        {
            List<SanPham> list = new List<SanPham>();
            SanPham SanPham = null;
            con.Open();
            string sql = "select * from SanPham where MaLoaiSanPham = @malsp";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("malsp", maloaisp);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                SanPham = new SanPham(dr["MaSanPham"].ToString(), dr["MaLoaiSanPham"].ToString(), dr["TenSanPham"].ToString(), Convert.ToInt32(dr["SoLuong"]), Convert.ToInt32(dr["GiaNhap"]), Convert.ToInt32(dr["GiaBan"]), dr["ThoiGianBaoHanh"].ToString(), dr["DonVi"].ToString(), dr["MoTa"].ToString());

                list.Add(SanPham);
            }
            con.Close();
            return list;
        }
        // hàm thêm sản phẩm vào cơ sở dữ liệu
        public bool AddSP(SanPham sp)
        {
            con.Open();

            string sql = "insert into SanPham values(@masp, @malsp, @tensp, @sl, @gianhap, @giaban, @baohanh, @donvi,@mota)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("masp", sp.MaSP);
            cmd.Parameters.AddWithValue("malsp", sp.MaLoaiSP);
            cmd.Parameters.AddWithValue("tensp", sp.TenSP);
            cmd.Parameters.AddWithValue("sl", sp.SoLuong);
            cmd.Parameters.AddWithValue("gianhap", sp.GiaNhap);
            cmd.Parameters.AddWithValue("giaban", sp.GiaBan);
            cmd.Parameters.AddWithValue("baohanh", sp.ThoiGianBaoHanh);
            cmd.Parameters.AddWithValue("donvi", sp.DonVi);
            cmd.Parameters.AddWithValue("mota", sp.MotaSP);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        // hàm sửa thông tin Loại sản phẩm
        public bool Updatesp(SanPham sp)
        {
            con.Open();
            //  string sql = "update SanPham set MaLoaiSanPham = @malsp,TenSanPham = @tensp , SoLuong  = @sl, GiaNhap = @gianhap, GiaBan = @giaban, ThoiGianBaoHanh =  @baohanh,DonVi = @donvi, MoTa = @mota where MaSanPham = @masp";
            string sql = "update SanPham set TenSanPham = @tensp , SoLuong  = @sl, GiaNhap = @gianhap, GiaBan = @giaban, ThoiGianBaoHanh =  @baohanh,DonVi = @donvi, MoTa = @mota where MaSanPham = @masp";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("malsp", sp.MaLoaiSP);
            cmd.Parameters.AddWithValue("tensp", sp.TenSP);
            cmd.Parameters.AddWithValue("sl", sp.SoLuong);
            cmd.Parameters.AddWithValue("gianhap", sp.GiaNhap);
            cmd.Parameters.AddWithValue("giaban", sp.GiaBan);
            cmd.Parameters.AddWithValue("baohanh", sp.ThoiGianBaoHanh);
            cmd.Parameters.AddWithValue("donvi", sp.DonVi);
            cmd.Parameters.AddWithValue("mota", sp.MotaSP);
            cmd.Parameters.AddWithValue("masp", sp.MaSP);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }

        // hàm xóa  sản phẩm
        public bool Deletesp(string masp)
        {
            con.Open();
            string sql = "delete from SanPham where MaSanPham = @masp";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("masp", masp);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        // hàm xóa  sản phẩm theo loại
        public bool DeletespByLoai(string malsp)
        {
            con.Open();

            string sql = "delete from SanPham where MaLoaiSanPham = @malsp";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("malsp", malsp);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        // hàm tìm kiếm  sản phẩm theo mã hoặc theo tên
        public List<SanPham> SearchSP(string keyword) // tìm kiếm theo từ khóa
        {
            List<SanPham> list = new List<SanPham>();
            con.Open();
            string sql = "select * from SanPham where TenSanPham LIKE N'%" + keyword +"%' or MoTa LIKE N'%"+ keyword + "%'";     
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                SanPham sp = new SanPham(dr["MaSanPham"].ToString(), dr["MaLoaiSanPham"].ToString(), dr["TenSanPham"].ToString(), Convert.ToInt32(dr["SoLuong"]), 
                    Convert.ToInt32(dr["GiaNhap"]), Convert.ToInt32(dr["GiaBan"]), dr["ThoiGianBaoHanh"].ToString(), dr["DonVi"].ToString(), dr["MoTa"].ToString());

                list.Add(sp);
            }
            con.Close();
            return list;
        }
        // hàm kiểm tra sản phẩm  đã có hay chưa
        public SanPham CheckSP(string MaSPCantim)
        {
            SanPham sp = null;
            con.Open();
            string sql = "select * from SanPham where MaSanPham = @masp";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("masp", MaSPCantim);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                sp = new SanPham(dr["MaSanPham"].ToString(), dr["MaLoaiSanPham"].ToString(), dr["TenSanPham"].ToString(), Convert.ToInt32(dr["SoLuong"]), Convert.ToInt32(dr["GiaNhap"]), Convert.ToInt32(dr["GiaBan"]), dr["ThoiGianBaoHanh"].ToString(), dr["DonVi"].ToString(), dr["MoTa"].ToString());
                break;
            }
            con.Close();
            return sp;
        }
        // hàm get giá sp from mã
        public float getPriceProduct(string masp)
        {
            float gia = 0;
            con.Open();
            string sql = "select GiaNhap from SanPham where MaSanPham = '"+ masp +"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                gia =  Convert.ToInt32(dr["GiaNhap"]);
            }
            con.Close();
            return gia;
        }
        // hàm get tên sp from mã
        public string getNameProduct(string masp)
        {
            string name = "";
            con.Open();
            string sql = "select * from SanPham where MaSanPham = '" + masp + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                name = dr["TenSanPham"].ToString();
                break;
            }
            con.Close();
            return name;
        }
        // update số lượng sản phẩm khi nhập hàng
        public void UpdateSLSanPham(string masp,int sl)
        {
          
            con.Open();
            string sql = "update SanPham set SoLuong = SoLuong + "+ sl +" where MaSanPham = '" + masp +"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        // hàm lấy tất cả sản phẩm nhập từ ngày a - b 
        public List<SPTK> getAllSPNhap(string from,string to)
        {
            List<SPTK> list = new List<SPTK>();
            NhanVienDao nvDao = new NhanVienDao();
            SPTK SanPham = null;
            con.Open();
            string sql = "select TenSanPham,ChiTiet_HoaDon.SoLuong,GiaNhap,TienPhaiTra,MaNhanVien,NgayLap from HoaDon inner join ChiTiet_HoaDon on HoaDon.MaHoaDon = ChiTiet_HoaDon.MaHoaDon inner join SanPham on SanPham.MaSanPham = ChiTiet_HoaDon.MaSanPham where NgayLap > '"+  from   +"' and NgayLap< '" +to +"'";
            SqlCommand cmd = new SqlCommand(sql, con);
          
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                
                SanPham = new SPTK(dr["TenSanPham"].ToString(), Convert.ToInt32(dr["SoLuong"]), Convert.ToInt32(dr["GiaNhap"]),Convert.ToInt32(dr["TienPhaiTra"]), dr["MaNhanVien"].ToString(), dr["NgayLap"].ToString());

                list.Add(SanPham);
            }
            con.Close();
            return list;
        }
    }
}
