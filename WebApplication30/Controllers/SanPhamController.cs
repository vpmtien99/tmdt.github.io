using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: DoChoi
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    SanPhamDAO dao = new SanPhamDAO();
                    Session["ListMH"] = dao.GetList();
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult TimKiem(string tenmh, string gia, int loai, int nsx, int km, string tinhtrang)
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    SanPhamDAO dao = new SanPhamDAO();
                    Session["ListMH"] = dao.Search(tenmh, gia, loai, nsx, km, tinhtrang);
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Reset()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    SanPhamDAO dao = new SanPhamDAO();
                    Session["ListMH"] = dao.GetList();
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Update(int idMH)
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    SanPhamDAO dao = new SanPhamDAO();
                    var mh = dao.GetMH(idMH);
                    if (mh == null)
                    {
                        ViewBag.Alert = "Không thể tìm thấy mô hình";
                    }
                    else
                    {
                        Session["UpdateMH"] = mh;
                    }
                    return View("Index");

                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Delete(bool confirm, int idMH)
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    if (confirm == true)
                    {

                        SanPhamDAO dao = new SanPhamDAO();
                        if (Session["UpdateMH"] != null)
                        {
                            var mh = Session["UpdateMH"] as SanPham;
                            if (mh.MaSP == idMH)
                            {
                                Session["UpdateMH"] = null;
                            }
                        }
                        dao.DeleteDoChoi(tk.MaKH, idMH);
                        Session["ListMH"] = dao.GetList();
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult Index(string tenmh, string gia, string sl, HttpPostedFileBase anhbia, HttpPostedFileBase anh1, HttpPostedFileBase anh2, int loai, int nsx, int km, DateTime ngay, string mota)
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {

                    SanPhamDAO dao = new SanPhamDAO();
                    if (Session["UpdateMH"] == null)
                    {
                        int rs = dao.AdđoChoi(tk.MaKH, tenmh, gia, sl, anhbia, anh1, anh2, loai, nsx, km, ngay, mota);
                        if (rs == 1)
                        {
                            ViewBag.Gia = "Giá phải lớn hơn 0";
                        }
                        else if (rs == 2)
                        {
                            ViewBag.SL = "Số lượng phải lớn hơn bằng 0";
                        }
                        else
                        {
                            Session["ListMH"] = dao.GetList();
                        }
                    }
                    else
                    {
                        var mh = Session["UpdateMH"] as SanPham;
                        var rs = dao.UpdateDoChoi(tk.MaKH, mh.MaSP, tenmh, gia, sl, anhbia, anh1, anh2, loai, nsx, km, ngay, mota);
                        if (rs == 1)
                        {
                            ViewBag.Gia = "Giá phải lớn hơn 0";
                        }
                        else if (rs == 2)
                        {
                            ViewBag.SL = "Số lượng phải lớn hơn bằng 0";
                        }
                        else
                        {
                            Session["ListMH"] = dao.GetList();
                            Session["UpdateMH"] = null;
                        }
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
    }
}