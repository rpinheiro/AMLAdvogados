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

        [StringLength(100, ErrorMessage ="O endereço deve conter no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [StringLength(100, ErrorMessage = "O endereço deve conter no máximo 100 caracteres")]
        public string Complemento { get; set; }

        [StringLength(50, ErrorMessage = "O endereço deve conter no máximo 100 caracteres")]
        public string Bairro { get; set; }

        [StringLength(15, ErrorMessage = "O endereço deve conter no máximo 100 caracteres")]
        public string CEP { get; set; }

        [StringLength(50, ErrorMessage = "O endereço deve conter no máximo 100 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]        
        [RegularExpression(@"[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}", ErrorMessage =
            "O CPF informado é inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O RG é obrigatório")]
        public string RG { get; set; }

        public virtual IList<EmailCliente> Email{ get; set; }
        public virtual IList<TelefoneCliente> Telefone { get; set; }

    }
}
