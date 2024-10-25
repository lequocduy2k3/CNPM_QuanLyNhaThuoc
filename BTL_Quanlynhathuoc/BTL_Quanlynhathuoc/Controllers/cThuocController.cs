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
            try
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

                // Trả về các thuộc tính cần thiết của đối tượng thuoc để tránh lỗi JSON hóa
                return Json(new
                {
                    success = true,
                    thuoc = new
                    {
                        thuoc.smaThuoc,
                        thuoc.stenThuoc,
                        thuoc.smaNCC,
                        thuoc.smaLoaiThuoc,
                        thuoc.isoLuong,
                        thuoc.fgiaThuoc
                    }
                });
            }
            catch (Exception ex)
            {
                // Trả về thông tin lỗi chi tiết cho client nếu xảy ra lỗi bất ngờ
                return Json(new { success = false, error = "Lỗi khi thêm thuốc: " + ex.Message });
            }
        }

        [HttpPost]

        public ActionResult Update(tblThuoc thuoc)
        {
            if (thuoc == null)
            {
                return Json(new { success = false, error = "Thông tin thuốc không hợp lệ." });
            }

            var thuocDB = db.tblThuocs.Find(thuoc.smaThuoc);
            if (thuocDB == null)
            {
                return Json(new { success = false, error = "Không tìm thấy thuốc." });
            }

            thuocDB.stenThuoc = thuoc.stenThuoc;
            thuocDB.smaNCC = thuoc.smaNCC;
            thuocDB.smaLoaiThuoc = thuoc.smaLoaiThuoc;
            thuocDB.fgiaThuoc = thuoc.fgiaThuoc;
            thuocDB.isoLuong = thuoc.isoLuong;

            db.SaveChanges();
            return Json(new { success = true, thuoc = thuocDB });
        }

        public ActionResult GetDrug(string smaThuoc)
        {
            // Kiểm tra xem mã thuốc có hợp lệ không
            if (string.IsNullOrEmpty(smaThuoc))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Mã thuốc không hợp lệ.");
            }

            // Tìm kiếm thuốc trong cơ sở dữ liệu
            var thuoc = db.tblThuocs.FirstOrDefault(row => row.smaThuoc == smaThuoc);
            if (thuoc == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Không tìm thấy thuốc.");
            }

            // Nếu tìm thấy, có thể trả về trạng thái thành công mà không cần dữ liệu
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }




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
