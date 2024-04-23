namespace HomeTeamWebSite.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Utenti")]
    public partial class Utenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utenti()
        {
            Ordini = new HashSet<Ordini>();
        }

        [Key]
        public int IdUtente { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [StringLength(250)]
        [Display(Name = "Name")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [StringLength(250)]
        [Display(Name = "Surname")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [StringLength(255)]

        public string Mail { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [StringLength(250)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Psw { get; set; }

        public short Ruolo { get; set; } = 1; // Settiamo un valore di defualt

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "Username")]
        public string Username { get; set; }


        [StringLength(150)]
        public string TipoAccount { get; set; } = "Standard"; // Settiamo un valore di defualt

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
