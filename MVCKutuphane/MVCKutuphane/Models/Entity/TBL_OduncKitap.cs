//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCKutuphane.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_OduncKitap
    {
        public int ID { get; set; }
        public int Kitap_ID { get; set; }
        public int Uye_ID { get; set; }
        public Nullable<int> Personel_ID { get; set; }
        public Nullable<System.DateTime> Alis_Tarihi { get; set; }
        public Nullable<System.DateTime> Iade_Tarihi { get; set; }
        public Nullable<System.DateTime> Teslim_Tarihi { get; set; }
    
        public virtual TBL_Personel TBL_Personel { get; set; }
        public virtual TBL_Uyeler TBL_Uyeler { get; set; }
        public virtual TBL_Kitaplar TBL_Kitaplar { get; set; }
    }
}
