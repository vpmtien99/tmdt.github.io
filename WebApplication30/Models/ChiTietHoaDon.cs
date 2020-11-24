using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaCTHD { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
        public bool Flag { get; set; }
        public int HDId { get; set; }
        public int SanPhamId { get; set; }
        public virtual SanPham SanPhams { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}