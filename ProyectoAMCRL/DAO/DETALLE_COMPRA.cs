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
    
    public partial class DETALLE_COMPRA
    {
        public decimal COD_LINEA_C { get; set; }
        public decimal COD_COMPRA { get; set; }
        public decimal COD_MATERIAL { get; set; }
        public Nullable<decimal> KILOS_COMPRA { get; set; }
        public Nullable<decimal> MONTO_LINEA_C { get; set; }
    
        public virtual COMPRA COMPRA { get; set; }
        public virtual MATERIAL MATERIAL { get; set; }
    }
}