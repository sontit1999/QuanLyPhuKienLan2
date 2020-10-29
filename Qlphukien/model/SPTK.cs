using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qlphukien.model
{
    class SPTK
    {
        public string Tensp { get; set; }
        public int slnhap { get; set; }
        public int dongianhap { get; set; }
        public string ngaynhap { get; set; }
        public int tongtien { get; set; }
        public string tennvnhap { get; set; }
        public SPTK()
        {

        }
        public SPTK(string tensp,int slnhap,int dongianhap,int tongtien,string tennvnhap,string ngaynhap)
        {
            this.Tensp = tensp;
            this.slnhap = slnhap;
            this.dongianhap = dongianhap;
            this.ngaynhap = ngaynhap;
            this.tongtien = tongtien;
            this.tennvnhap = tennvnhap;
        }
    }
}
