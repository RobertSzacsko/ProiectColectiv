//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProiectColectiv
{
    using System;
    using System.Collections.Generic;
    
    public partial class Internari
    {
        public int id_Internare { get; set; }
        public string Motiv { get; set; }
        public Nullable<System.DateTime> DataInternarii { get; set; }
        public Nullable<System.DateTime> DataExternarii { get; set; }
        public string Observatii { get; set; }
    
        public virtual FisaMedicala FisaMedicala { get; set; }
    }
}
