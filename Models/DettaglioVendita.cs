namespace HomeTeamWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettaglioVendita")]
    public partial class DettaglioVendita
    {
        [Key]
        public int IdDettaglioVendita { get; set; }

        public int IdOrdine { get; set; }

        public int? IdProdotto { get; set; }

        public int? IdBiglietti { get; set; }

        public int? QuantitaProdotti { get; set; }

        public int? QuantitaBiglietti { get; set; }

        [Column(TypeName = "money")]
        public decimal PrezzoUnitario { get; set; }

        public virtual Biglietti Biglietti { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
