using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebMotors.TransferObject.Entities
{
    [Table("tb_AnuncioWebmotors")]
    public class Anuncio
    {
        public int ID { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string versao { get; set; }
        public int ano { get; set; }
        public int quilometragem { get; set; }
        public string observacao { get; set; }
    }
}
