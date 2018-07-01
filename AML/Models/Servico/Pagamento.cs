using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AML.Models.Servico
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string IndTipoPagamento { get; set; }
        public string Descricao { get; set; }
        public float Valor { get; set; }
        [Required]
        [Column("IdServico")]
        public virtual Servico Servico { get; set; }
    }
}
