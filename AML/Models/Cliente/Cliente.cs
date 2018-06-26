using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AML.Models.Usuario;

namespace AML.Models.Cliente
{
    public class Cliente
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O RG é obrigatório")]
        public string RG { get; set; }

        public virtual IList<EmailCliente> Email{ get; set; }
        public virtual IList<TelefoneCliente> Telefone { get; set; }

    }
}
