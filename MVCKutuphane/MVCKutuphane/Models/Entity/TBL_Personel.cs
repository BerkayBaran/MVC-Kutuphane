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
    
    public partial class TBL_Personel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Personel()
        {
            this.TBL_OduncKitap = new HashSet<TBL_OduncKitap>();
        }
    
        public int ID { get; set; }
        public string Personel_Adi { get; set; }
        public string Personel_Soyadi { get; set; }
        public string Personel_Kullanci_Adi { get; set; }
        public string Telefon { get; set; }
        public string Cinsiyet { get; set; }
        public string Sifre { get; set; }
        public Nullable<bool> Durum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_OduncKitap> TBL_OduncKitap { get; set; }
    }
}