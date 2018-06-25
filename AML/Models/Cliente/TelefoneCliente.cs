using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AML.Models.Cliente
{
    public class TelefoneCliente
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        [NotMapped]
        public string Tipo { get; set; }
        public string IndTipo
        {
            get
            {
                if (Tipo.ToUpper() == "CELULAR")
                    return "C";
                else
                    return "R";
            }
            set
            {
                if (value != null)
                {
                    if (value.ToUpper() == "C")
                        Tipo = "Celular";
                    else
                        Tipo = "Casa";
                }
                else
                    Tipo = "Casa";
            }
        }
        public string Ativo { get; set; }

        [Required]
        [Column("ClienteId")]
        public virtual Cliente Cliente{get;set;}
    }
}
