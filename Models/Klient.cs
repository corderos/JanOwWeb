namespace WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sql11176056.Klient")]
    public partial class Klient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klient()
        {
            Wydarzenie = new HashSet<Wydarzenie>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Imie { get; set; }

        [Required]
        [StringLength(20)]
        public string Nazwisko { get; set; }

        public bool Typ_konta { get; set; }

        [Required]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]
        [StringLength(20)]
        public string Haslo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wydarzenie> Wydarzenie { get; set; }
    }
}
