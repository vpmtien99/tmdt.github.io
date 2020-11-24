using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class SanPhamContext : DbContext
    {
        public SanPhamContext() : base("SanPham")
        {
           
        }
        public DbSet<TichLuy> TichLuys { get; set; }
        public DbSet<NSX> NSXs { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
    }
}