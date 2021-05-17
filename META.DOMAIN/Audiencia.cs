using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace META.DOMAIN
{
    [Table("Audiencia")]
    public class Audiencia
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Pontos_audiencia")]
        public int Pontos_audiencia { get; set; }
        [Column("Data_hora_audiencia")]
        public DateTime Data_hora_audiencia { get; set; }
        [ForeignKey("Emissora_audiencia")]
        [Column("Emissora_audiencia")]
        public string Emissora_audiencia { get; set; }

       
    }
}
