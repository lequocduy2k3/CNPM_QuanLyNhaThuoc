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
    
    public partial class tblChiTietHDNhapthuoc
    {
        public string imaHD { get; set; }
        public string smaThuoc { get; set; }
        public Nullable<double> fGiaNhap { get; set; }
        public Nullable<int> isoLuongNhap { get; set; }
    
        public virtual tblHoaDonNhapThuoc tblHoaDonNhapThuoc { get; set; }
        public virtual tblThuoc tblThuoc { get; set; }
    }
}
