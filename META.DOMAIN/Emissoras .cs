using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace META.DOMAIN
{
    [Table("Emissoras")]
    public class Emissoras
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("EMISSORAS")]
        [Required(ErrorMessage = "O Campo Não pode Ser Nulo")]
        public string EMISSORAS { get; set; }
     
        public Audiencia audiencias { get; set; }

    }


    


}
