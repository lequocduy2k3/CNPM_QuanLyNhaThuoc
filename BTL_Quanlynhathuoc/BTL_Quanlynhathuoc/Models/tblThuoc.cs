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
    
    public partial class tblThuoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblThuoc()
        {
            this.tblChiTietDTs = new HashSet<tblChiTietDT>();
            this.tblChiTietHDNhapthuocs = new HashSet<tblChiTietHDNhapthuoc>();
        }
    
        public string smaThuoc { get; set; }
        public string stenThuoc { get; set; }
        public string smaNCC { get; set; }
        public string smaLoaiThuoc { get; set; }
        public Nullable<int> isoLuong { get; set; }
        public Nullable<double> fgiaThuoc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChiTietDT> tblChiTietDTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChiTietHDNhapthuoc> tblChiTietHDNhapthuocs { get; set; }
        public virtual tblLoaiThuoc tblLoaiThuoc { get; set; }
        public virtual tblNhaCungCap tblNhaCungCap { get; set; }
    }
}
