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
                var existingNCC = db.tblNhaCungCaps.Find(thuoc.smaNCC);
                if (existingNCC == null)
                {
                    return Json(new { success = false, error = "Mã nhà cung cấp không hợp lệ." });
                }

                // Kiểm tra mã thuốc đã tồn tại
                if (db.tblThuocs.Any(t => t.smaThuoc == thuoc.smaThuoc))
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
            // Kiểm tra xem dữ liệu thuốc có hợp lệ không
            if (thuoc == null)
            {
                return Json(new { success = false, error = "Thông tin thuốc không hợp lệ." });
            }

            // Tìm kiếm thuốc trong cơ sở dữ liệu theo mã thuốc được gửi từ client
            var thuocDB = db.tblThuocs.Find(thuoc.smaThuoc);
            if (thuocDB == null)
            {
                return Json(new { success = false, error = "Không tìm thấy thuốc." });
            }

            // Cập nhật thông tin thuốc mà không kiểm tra mã thuốc trùng lặp
            thuocDB.stenThuoc = thuoc.stenThuoc;
            thuocDB.smaNCC = thuoc.smaNCC;
            thuocDB.smaLoaiThuoc = thuoc.smaLoaiThuoc;
            thuocDB.fgiaThuoc = thuoc.fgiaThuoc;
            thuocDB.isoLuong = thuoc.isoLuong;

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            // Trả về kết quả cập nhật thành công
            return Json(new
            {
                success = true,
                thuoc = new
                {
                    thuocDB.smaThuoc,
                    thuocDB.stenThuoc,
                    thuocDB.smaNCC,
                    thuocDB.smaLoaiThuoc,
                    thuocDB.fgiaThuoc,
                    thuocDB.isoLuong
                }
            });
        }



        public ActionResult GetDrug(string smaThuoc)
        {
            if (string.IsNullOrEmpty(smaThuoc))
            {
                return Json(new { success = false, error = "Mã thuốc không hợp lệ." }, JsonRequestBehavior.AllowGet);
            }

            var thuoc = db.tblThuocs.FirstOrDefault(row => row.smaThuoc == smaThuoc);
            if (thuoc == null)
            {
                return Json(new { success = false, error = "Không tìm thấy thuốc." }, JsonRequestBehavior.AllowGet);
            }

            // Trả về thông tin thuốc
            return Json(new
            {
                success = true,
                thuoc = new
                {
                    thuoc.smaThuoc,
                    thuoc.stenThuoc,
                    thuoc.smaNCC,
                    thuoc.smaLoaiThuoc,
                    thuoc.fgiaThuoc,
                    thuoc.isoLuong
                }
            }, JsonRequestBehavior.AllowGet);
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
