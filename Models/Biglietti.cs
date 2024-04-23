namespace HomeTeamWebSite.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Biglietti")]
    public partial class Biglietti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Biglietti()
        {
            DettaglioVendita = new HashSet<DettaglioVendita>();
        }

        [Key]
        public int IdBiglietti { get; set; }

        public int IdPartita { get; set; }

        public int IdTipoPosto { get; set; }

        public int? QuantitaDisponibile { get; set; } = null;

        public string CodiceBiglietto { get; set; }

        [NotMapped]
        public int? Quantita { get; set; }

        public bool IsAcquistato { get; set; }

        public virtual Partite Partite { get; set; }

        public virtual TipoPosto TipoPosto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioVendita> DettaglioVendita { get; set; }
    }
}
