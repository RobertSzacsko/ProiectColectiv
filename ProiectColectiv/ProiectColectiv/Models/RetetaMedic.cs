using System.Collections.Generic;

namespace ProiectColectiv.Models
{
    public partial class RetetaMedic
    {
        public virtual Retete Reteta { get; set; }
        public virtual Medicamente Medicamente { get; set; }
    }
}