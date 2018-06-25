using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AML.Models.Cliente
{
    public class EmailCliente
    {
        public int Id { get; set; }
        public string EnderecoEmail { get; set; }
        public string Ativo { get; set; }
        [Required]
        [Column("ClienteId")]
        public virtual Cliente Cliente { get; set; }
    }
}
