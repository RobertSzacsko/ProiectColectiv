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
    
    public partial class Diagnostic
    {
        public int id_Diagnostic { get; set; }
        public string Descriere { get; set; }
    
        public virtual Programari Programari { get; set; }
    }
}
