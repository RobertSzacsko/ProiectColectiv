using System.Collections.Generic;

namespace ProiectColectiv.Models
{
    public partial class RetetaMedic
    {
        public virtual Retete Reteta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicamente> Medicamente { get; set; }
    }
}