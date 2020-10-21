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
    class HoaDonDao
    {
        SqlConnection con;
        public HoaDonDao()
        {
            con = UtilsConnect.getConnection();
        }
        // hàm get all hóa đơn
        public List<HoaDon> getAllHD()
        {
            List<HoaDon> list = new List<HoaDon>();
            HoaDon hoadon = null;
            con.Open();
            string sql = "select * from HoaDon";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                hoadon = new HoaDon(dr["MaHoaDon"].ToString(), dr["MaNhanVien"].ToString(), dr["NgayLap"].ToString(), Convert.ToInt32(dr["TongTienHoaDon"]));
                list.Add(hoadon);
            }
            con.Close();
            return list;
        }
        // hàm get all hóa đơn by name SP
        public List<HoaDon> getAllHDByNameSP(string namesp)
        {
            List<HoaDon> list = new List<HoaDon>();
            HoaDon hoadon = null;
            con.Open();
            string sql = "select * from HoaDon";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                hoadon = new HoaDon(dr["MaHoaDon"].ToString(), dr["MaNhanVien"].ToString(), dr["NgayLap"].ToString(), Convert.ToInt32(dr["TongTienHoaDon"]));
                list.Add(hoadon);
            }
            con.Close();
            return list;
        }

        // hàm thêm hóa đơn vào cơ sở dữ liệu
        public bool AddHoaDon(HoaDon hd)
        {
            con.Open();

            string sql = "insert into HoaDon values(@mahd, @manv, @ngay,@tongtien)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("mahd", hd.MaHD);
            cmd.Parameters.AddWithValue("manv", hd.MaNV);
            cmd.Parameters.AddWithValue("ngay", hd.NgayLap);
            cmd.Parameters.AddWithValue("tongtien", hd.TongTienHD);
           
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        // hàm xóa hóa đơn 
        public bool DeleteHoaDon(string mahd)
        {
            con.Open();

            string sql = "delete  from HoaDon where MaHoaDon = @mahd";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("mahd", mahd);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        public List<HoaDon> SearchHD(string keyword)
        {
            List<HoaDon> list = new List<HoaDon>();
            HoaDon hoadon = null;
            con.Open();
            string sql = "select * from HoaDon where MaHoaDon in ( select MaHoaDon from ChiTiet_HoaDon where MaSanPham in ((select MaSanPham from SanPham where TenSanPham like N'%" + keyword+ "%' or MoTa like N'%" + keyword + "%')))";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                hoadon = new HoaDon(dr["MaHoaDon"].ToString(), dr["MaNhanVien"].ToString(), dr["NgayLap"].ToString(), Convert.ToInt32(dr["TongTienHoaDon"]));
                list.Add(hoadon);
            }
            con.Close();
            return list;
           
        }
    }
}
