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
    
    public partial class Vaccinuri
    {
        public int id_Vaccin { get; set; }
        public Nullable<System.DateTime> DataVaccin { get; set; }
        public string Tip { get; set; }
    
        public virtual FisaMedicala FisaMedicala { get; set; }
    }
}
