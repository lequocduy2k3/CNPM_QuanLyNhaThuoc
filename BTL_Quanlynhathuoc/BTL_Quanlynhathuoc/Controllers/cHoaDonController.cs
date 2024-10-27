using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BTL_Quanlynhathuoc.Models;

namespace BTL_Quanlynhathuoc.Controllers
{
    public class cHoaDonController : Controller
    {
        private QUANLYNHATHUOCEntities db = new QUANLYNHATHUOCEntities();


        // GET: DonThuoc/TimKiemThuoc
        public ActionResult TimKiemThuoc(string sdt)
        {
            if (string.IsNullOrEmpty(sdt))
            {
                ViewBag.Message = "Vui lòng nhập số điện thoại.";
                return View();
            }

            var khachHang = db.tblKhachHangs.FirstOrDefault(kh => kh.sSDT == sdt);
            if (khachHang == null)
            {
                ViewBag.Message = "Không tìm thấy khách hàng với số điện thoại này.";
                return View();
            }

            var danhSachThuoc = db.tblDonThuocs
                .Where(dt => dt.smaKH == khachHang.smaKH)
                .SelectMany(dt => dt.tblChiTietDTs)
                .Select(ct => new DonThuocViewModel
                {
                    TenThuoc = ct.tblThuoc.stenThuoc,
                    SoLuong = ct.isoLuong,
                    GiaBan = ct.fGiaBan
                })
                .ToList();

            ViewBag.KhachHang = khachHang;
            return View(danhSachThuoc);
        }







    }

}
