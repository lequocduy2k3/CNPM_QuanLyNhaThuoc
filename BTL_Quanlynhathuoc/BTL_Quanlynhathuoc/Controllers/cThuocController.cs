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
    public class cThuocController : Controller
    {
        // Tạo đối tượng kết nối với cơ sở dữ liệu
        QUANLYNHATHUOCEntities db = new QUANLYNHATHUOCEntities();

        // Trang chính, trả về danh sách thuốc
        public ActionResult Index()
        {
            var thuocs = db.tblThuocs.ToList();
            return View("vThuoc", thuocs);
        }

        // Hiển thị trang quản lý thuốc
        public ActionResult vThuoc()
        {
            var thuocs = db.tblThuocs.ToList(); // Lấy danh sách thuốc
            return View(thuocs); // Trả về view vThuoc.cshtml
        }

        // Thêm thuốc mới
        [HttpPost]
        public ActionResult Add(tblThuoc thuoc)
        {
            // Kiểm tra mã nhà cung cấp
            var existingNCC = db.tblNhaCungCaps.Find(thuoc.smaNCC);
            if (existingNCC == null)
            {
                return Json(new { success = false, error = "Mã nhà cung cấp không hợp lệ." });
            }

            // Kiểm tra mã thuốc đã tồn tại
            var existingThuoc = db.tblThuocs.FirstOrDefault(t => t.smaThuoc == thuoc.smaThuoc);
            if (existingThuoc != null)
            {
                return Json(new { success = false, error = "Mã thuốc đã tồn tại." });
            }

            // Kiểm tra mã loại thuốc
            var existingLoaiThuoc = db.tblLoaiThuocs.Find(thuoc.smaLoaiThuoc);
            if (existingLoaiThuoc == null)
            {
                return Json(new { success = false, error = "Mã loại thuốc không hợp lệ." });
            }

            // Thêm thuốc vào cơ sở dữ liệu
            db.tblThuocs.Add(thuoc);
            db.SaveChanges();

            return Json(new { success = true, thuoc });
        }

        // Cập nhật thông tin thuốc
        [HttpPost]
        public ActionResult Update(tblThuoc thuoc)
        {
            var thuocDB = db.tblThuocs.FirstOrDefault(row => row.smaThuoc == thuoc.smaThuoc);
            if (thuocDB == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { error = "Không tìm thấy thuốc." });
            }

            // Cập nhật thông tin thuốc
            thuocDB.stenThuoc = thuoc.stenThuoc;
            thuocDB.smaNCC = thuoc.smaNCC;
            thuocDB.fgiaThuoc = thuoc.fgiaThuoc;
            thuocDB.isoLuong = thuoc.isoLuong;
            thuocDB.smaLoaiThuoc = thuoc.smaLoaiThuoc;

            db.SaveChanges();
            return Json(new { success = true });
        }

        // Xóa thuốc dựa trên mã thuốc
        [HttpPost]
        public ActionResult Delete(string sMathuoc)
        {
            var thuocDB = db.tblThuocs.FirstOrDefault(row => row.smaThuoc == sMathuoc);
            if (thuocDB == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { error = "Không tìm thấy thuốc." });
            }

            // Xóa thuốc khỏi cơ sở dữ liệu
            db.tblThuocs.Remove(thuocDB);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}
