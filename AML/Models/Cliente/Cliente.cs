using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AML.Models.Usuario;

namespace AML.Models.Cliente
{
    public class Cliente
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

        public virtual IList<EmailCliente> Email{ get; set; }
        public virtual IList<TelefoneCliente> Telefone { get; set; }

    }
}
