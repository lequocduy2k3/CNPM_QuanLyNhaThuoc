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
    
    public partial class tblKhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblKhachHang()
        {
            this.tblDonThuocs = new HashSet<tblDonThuoc>();
        }
    
        public string smaKH { get; set; }
        public string stenKH { get; set; }
        public Nullable<System.DateTime> dngaySinh { get; set; }
        public string sDiaChi { get; set; }
        public string sSDT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDonThuoc> tblDonThuocs { get; set; }
    }
}
