namespace HomeTeamWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Partite")]
    public partial class Partite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partite()
        {
            Biglietti = new HashSet<Biglietti>();
        }

        [Key]
        public int IdPartita { get; set; }

        public DateTime Data { get; set; }

        [Required]
        public string SquadraCasa { get; set; }

        [Required]
        public string SquadraOspite { get; set; }


        public string ImgCasa { get; set; }


        public string ImgOspite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Biglietti> Biglietti { get; set; }
    }
}
