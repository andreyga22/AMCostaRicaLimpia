//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAO
{
    using System;
    using System.Collections.Generic;
    
    public partial class STOCK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STOCK()
        {
            this.AJUSTEs = new HashSet<AJUSTE>();
        }
    
        public int ID_STOCK { get; set; }
        public decimal COD_MATERIAL { get; set; }
        public decimal ID_BODEGA { get; set; }
        public decimal KILOS_STOCK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AJUSTE> AJUSTEs { get; set; }
        public virtual BODEGA BODEGA { get; set; }
        public virtual MATERIAL MATERIAL { get; set; }
    }
}
