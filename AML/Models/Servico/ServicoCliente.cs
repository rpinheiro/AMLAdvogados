using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AML.Models.Servico
{
    public class ServicoCliente
    {
        public int Id { get; set; }
        [Required]
        [Column("IdCliente")]
        public virtual Cliente.Cliente Cliente { get; set; }
        [Required]
        [Column("IdServico")]
        public virtual Servico Servico { get; set; }
    }
}
