//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTL_Quanlynhathuoc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDonThuoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDonThuoc()
        {
            this.tblChiTietDTs = new HashSet<tblChiTietDT>();
        }
    
        public int imaHD { get; set; }
        public string smaNV { get; set; }
        public string smaKH { get; set; }
        public Nullable<System.DateTime> dngayLapHD { get; set; }
        public Nullable<System.DateTime> dngayGiaoHang { get; set; }
        public string sDiaChiGH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChiTietDT> tblChiTietDTs { get; set; }
        public virtual tblKhachHang tblKhachHang { get; set; }
        public virtual tblNhanVien tblNhanVien { get; set; }
    }
}
