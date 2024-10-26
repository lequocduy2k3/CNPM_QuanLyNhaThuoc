using BTL_Quanlynhathuoc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BTL_Quanlynhathuoc.Controllers
{
    public class cDonThuocController : Controller
    {
        QUANLYNHATHUOCEntities db = new QUANLYNHATHUOCEntities();

        public ActionResult Index()
        {

            var DonThuocs = db.tblDonThuocs.ToList();
            return View("vDonThuoc", DonThuocs);
        }

        // Hiển thị trang quản lý đơn đơn thuốc
        public ActionResult vDonThuoc()
        {

            var DonThuocs = db.tblDonThuocs.ToList(); // Lấy danh sách đơn thuốc
            return View(Donthuocs); // Trả về view vDonThuoc.cshtml
        }

        // Thêm đơn thuốc mới
        [HttpPost]
        public ActionResult Add(tblDonThuoc DonThuoc)
        {
            try
            {
                var existingNV = db.tblNhanViens.Find(DonThuoc.smaNV);
                if (existingNV == null)
                {
                    return Json(new { success = false, error = "Mã nhân viên không hợp lệ." });
                }

                // Kiểm tra mã hoá đơn đã tồn tại
                if (db.tblDonThuocs.Any(t => t.sMaHD == DonThuoc.sMaHD))
                {
                    return Json(new { success = false, error = "Mã hoá đơn đã tồn tại." });
                }

                // Kiểm tra mã kh
                var existingKH = db.tblKhachHangs.Find(DonThuoc.smaKH);
                if (existingKH == null)
                {
                    return Json(new { success = false, error = "Mã khách hàng không hợp lệ." });
                }
                if (db.tblDonThuocs.Any(t => t.dNgayLapHD == null ))
                {
                    return Json(new { success = false, error = "Ngày lập hoá đơn không hợp lệ." });
                }
                if (db.tblDonThuocs.Any(t => t.dNgayGiaoHang == null ))
                {
                    return Json(new { success = false, error = "Ngày giao hàng không hợp lệ." });
                }
                if (db.tblDonThuocs.Any(t => t.sDiaChiGH == null ))
                {
                    return Json(new { success = false, error = "Địa chỉ giao hàng không hợp lệ." });
                }
                // Thêm đơn thuốc vào cơ sở dữ liệu
                db.tblDonThuocs.Add(DonThuoc);
                db.SaveChanges();

                // Trả về các thuộc tính cần thiết của đối tượng thuoc để tránh lỗi JSON hóa
                return Json(new
                {
                    success = true,
                    DonThuoc = new
                    {
                        DonThuoc.smaHD,
                        DonThuoc.smaNV,
                        DonThuoc.smaKH,
                        DonThuoc.dNgayLapHD,
                        DonThuoc.dNgayGiaoHang,
                        DonThuoc.sDiachiGH
                    }
                });
            }
            catch (Exception ex)
            {
                // Trả về thông tin lỗi chi tiết cho client nếu xảy ra lỗi bất ngờ
                return Json(new { success = false, error = "Lỗi khi thêm đơn thuốc: " + ex.Message });
            }
        }
