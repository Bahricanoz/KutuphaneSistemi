//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KutuphaneSistemi.Models.Entitiy
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Personel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Personel()
        {
            this.Tbl_Hareket = new HashSet<Tbl_Hareket>();
        }
    
        public int Id { get; set; }
        public string Personelad { get; set; }
        public string Personelsoyad { get; set; }
        public Nullable<bool> Durum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Hareket> Tbl_Hareket { get; set; }
    }
}
