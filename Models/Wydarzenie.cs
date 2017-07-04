namespace WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sql11176056.Wydarzenie")]
    public partial class Wydarzenie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wydarzenie()
        {
            Klient = new HashSet<Klient>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Nazwa { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public int IloscMiejsc { get; set; }

        [StringLength(100)]
        public string Adres { get; set; }

        public decimal Cena { get; set; }

        [StringLength(255)]
        public string Prowadzacy { get; set; }

        [StringLength(1000)]
        public string Temat { get; set; }

        [StringLength(1000)]
        public string Opis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual HashSet<Klient> Klient { get; set; }
    }
}
