using BTL_Quanlynhathuoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Quanlynhathuoc.Controllers
{
    public class cHoaDonController : Controller
    {
        QUANLYNHATHUOCEntities db = new QUANLYNHATHUOCEntities();

        // GET: HoaDon
        public ActionResult Index()
        {

            var DonThuocs = db.tblDonThuocs.ToList();
            return View("vHoaDon", DonThuocs);
        }

        // Hiển thị trang quản lý đơn đơn thuốc
        public ActionResult vHoaDon()
        {
            var DonThuocs = db.tblDonThuocs.ToList(); // Lấy danh sách đơn thuốc
            return View(DonThuocs); // Trả về view vDonThuoc.cshtml
        }

        // Thêm đơn thuốc mới
        [HttpPost]
        public ActionResult Add(tblDonThuoc DonThuoc)
        {
            try
            {
                // Kiểm tra mã nhân viên
                var existingNV = db.tblNhanViens.Find(DonThuoc.smaNV);
                if (existingNV == null)
                {
                    return Json(new { success = false, error = "Mã nhân viên không hợp lệ." });
                }

                // Kiểm tra mã hoá đơn đã tồn tại
                if (db.tblDonThuocs.Any(t => t.imaHD == DonThuoc.imaHD))
                {
                    return Json(new { success = false, error = "Mã hoá đơn đã tồn tại." });
                }

                // Kiểm tra mã khách hàng
                var existingKH = db.tblKhachHangs.Find(DonThuoc.smaKH);
                if (existingKH == null)
                {
                    return Json(new { success = false, error = "Mã khách hàng không hợp lệ." });
                }

                // Kiểm tra các trường bắt buộc
                if (DonThuoc.dngayLapHD == null)
                {
                    return Json(new { success = false, error = "Ngày lập hoá đơn không được để trống." });
                }

                if (DonThuoc.dngayGiaoHang == null)
                {
                    return Json(new { success = false, error = "Ngày giao hàng không được để trống." });
                }

                if (string.IsNullOrEmpty(DonThuoc.sDiaChiGH))
                {
                    return Json(new { success = false, error = "Địa chỉ giao hàng không được để trống." });
                }

                // Thêm đơn thuốc vào cơ sở dữ liệu
                db.tblDonThuocs.Add(DonThuoc);
                db.SaveChanges();

                // Trả về kết quả JSON bao gồm các thuộc tính cần thiết
                return Json(new
                {
                    success = true,
                    DonThuoc = new
                    {
                        DonThuoc.imaHD,
                        DonThuoc.smaNV,
                        DonThuoc.smaKH,
                        dngayLapHD = DonThuoc.dngayLapHD?.ToString("dd/MM/yyyy") ?? string.Empty, // Định dạng ngày, trả về chuỗi rỗng nếu null
                        dngayGiaoHang = DonThuoc.dngayGiaoHang?.ToString("dd/MM/yyyy") ?? string.Empty, // Định dạng ngày, trả về chuỗi rỗng nếu null
                        DonThuoc.sDiaChiGH
                    }
                });
            }
            catch (Exception ex)
            {
                // Trả về thông tin lỗi chi tiết cho client nếu xảy ra lỗi bất ngờ
                return Json(new { success = false, error = "Lỗi khi thêm đơn thuốc: " + ex.Message });
            }
        }


        public ActionResult Search(string searchTerm, string searchPhone)
        {
            // Sử dụng biến ngữ cảnh db đã được khai báo để truy vấn dữ liệu
            var results = db.tblDonThuocs.AsQueryable();

            

            // Tìm kiếm theo số điện thoại khách hàng nếu có giá trị
            if (!string.IsNullOrEmpty(searchPhone))
            {
                results = results.Where(d => d.tblKhachHang.sSDT.Contains(searchPhone)); // Giả sử tblKhachHang có trường sdtKH
            }

            // Gán giá trị cho ViewBag để có thể hiển thị trên view
            ViewBag.Search = searchTerm;
            ViewBag.SearchPhone = searchPhone;

            // Trả về danh sách hóa đơn tìm được đến view
            return View("vHoaDon", results.ToList()); // Đảm bảo trả về view đúng
        }



    }
}