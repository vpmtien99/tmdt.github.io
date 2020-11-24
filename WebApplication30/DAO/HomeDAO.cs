using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class HomeDAO
    {
        SanPhamContext db = null;
        public HomeDAO()
        {
            db = new SanPhamContext();
        }
        public List<SanPham>GetListTop4(int idLoai,int idNSX,int idMH)
        {
            var list = db.SanPhams.Where(i => i.LoaiId == idLoai && i.NSXId == idNSX && i.Flag == true && i.MaSP != idMH).OrderByDescending(i=>i.LuotXem).Take(4).ToList();
            return list;
            
        }
        public List<SanPham> GetAll()
        {
            return db.SanPhams.Where(i => i.Flag == true).ToList();
        }
        public List<SanPham> GetListXemNhieuAll()
        {
            return db.SanPhams.Where(i => i.Flag == true).OrderByDescending(i => i.LuotXem).ToList();
        }
        public List<SanPham> GetListXemNhieu()
        {
            return db.SanPhams.Where(i => i.Flag == true).OrderByDescending(i => i.LuotXem).Take(8).ToList();
        }
        public List<SanPham> GetListMoiAll()
        {
            return db.SanPhams.Where(i => i.Flag == true).OrderByDescending(i => i.NgayXB).ToList();
        }
        public List<SanPham> GetListMoi()
        {
            return db.SanPhams.Where(i => i.Flag == true).OrderByDescending(i => i.NgayXB).Take(8).ToList();
        }
        public List<SanPham> GetListBanChayAll()
        {
            HoaDonDAO dao = new HoaDonDAO();
            List<SanPham> listDC = new List<SanPham>();
            List<XemNhieuModel> list = new List<XemNhieuModel>();
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            var listHD = db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            foreach (var item in listHD)
            {
                foreach (var item1 in dao.GetListCTHD(item.MaHD))
                {
                    listCTHD.Add(item1);
                }
            }


            foreach (var item in db.SanPhams.Where(i => i.Flag == true).ToList())
            {
                XemNhieuModel model = new XemNhieuModel();
                model.IDMH = item.MaSP;
                model.SoLuong = 0;
                foreach (var item1 in listCTHD)
                {
                    if (item1.SanPhamId == model.IDMH)
                    {
                        model.SoLuong = model.SoLuong + item1.SoLuong;
                    }
                }
                list.Add(model);
            }
            list = list.OrderByDescending(i => i.SoLuong).ToList();
            foreach (var item in list)
            {
                listDC.Add(db.SanPhams.Find(item.IDMH));
            }
            return listDC;
        }
        public List<SanPham> Search(string name, string loai, string nsx, string min, string max,out string title)
        {
            var list = db.SanPhams.Where(i => i.Flag == true).ToList();
            title = "Tìm Kiếm Theo: ";
            if (String.IsNullOrEmpty(name) != true)
            {
                list = list.Where(i => i.TenSP.Contains(name)).ToList();
                title = title + "+ Tên: " + name+";     ";
            }
            if (loai != "All")
            {
                
                int idLoai = Convert.ToInt32(loai);
                title = title + "+ Loại: " + db.Loais.Find(idLoai).TenLoai+";     ";
                list = list.Where(i => i.LoaiId == idLoai).ToList();
            }
            if (nsx != "All")
            {
                int idNSX = Convert.ToInt32(nsx);
                title = title + "+ Nhà Sản Xuất: " + db.NSXs.Find(idNSX).TenNSX+";     ";
                list = list.Where(i => i.NSXId == idNSX).ToList();
            }
            if (String.IsNullOrEmpty(min) != true)
            {
                title = title + "+ Min: " + min + "VNĐ ;     ";
                double tienMin = Convert.ToDouble(min);
                list = list.Where(i => i.Gia >= tienMin).ToList();
            }
            if (String.IsNullOrEmpty(max) != true)
            {
                title = title + "+ Max: " + max+"VNĐ ;     ";
                double tienMax = Convert.ToDouble(max);
                list = list.Where(i => i.Gia <= tienMax).ToList();
            }
            if(title=="Tìm Kiếm Theo: ")
            {
                title = "Tất Cả";
            }
            return list;
        }
        public List<SanPham> GetListTheoKM(int idKM)
        {            
            return db.SanPhams.Where(i => i.Flag == true && i.KhuyenMaiId == idKM).ToList();
        }
        public List<SanPham> GetListTheoNSX(int idNSX)
        {
            return db.SanPhams.Where(i => i.Flag == true && i.NSXId == idNSX).ToList();
        }
        public List<XemNhieuModel> GetListBanChay()
        {
            HoaDonDAO dao = new HoaDonDAO();
            List<XemNhieuModel> list = new List<XemNhieuModel>();
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            var listHD = db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            foreach (var item in listHD)
            {
                foreach (var item1 in dao.GetListCTHD(item.MaHD))
                {
                    listCTHD.Add(item1);
                }
            }


            foreach (var item in db.SanPhams.Where(i=>i.Flag==true).ToList())
            {
                XemNhieuModel model = new XemNhieuModel();
                model.IDMH = item.MaSP;
                model.SoLuong = 0;
                foreach (var item1 in listCTHD)
                {
                    if (item1.SanPhamId == model.IDMH)
                    {
                        model.SoLuong = model.SoLuong + item1.SoLuong;
                    }
                }
                list.Add(model);
            }
            list = list.OrderByDescending(i => i.SoLuong).Take(8).ToList();
            return list;
        }
    }
}