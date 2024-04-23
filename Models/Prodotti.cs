namespace HomeTeamWebSite.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            DettaglioVendita = new HashSet<DettaglioVendita>();
        }

        [Key]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(200)]
        public string NomeProdotto { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        public string Immagine { get; set; }

        public string Immagine2 { get; set; }
        public string Immagine3 { get; set; }

        [NotMapped]
        public int? Quantita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioVendita> DettaglioVendita { get; set; }
    }
}
