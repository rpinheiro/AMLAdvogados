using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AML.Models.Servico
{
    public class Servico
    {
        public int Id { get; set; }
        public DateTime DtCadastro { get; set; }
        public string Descricao { get; set; }
        public string IndStatus { get; set; }

        [NotMapped]
        public string Status
        {
            get
            {
                if (IndStatus == "A")
                    return "Andamento";
                if (IndStatus == "P")
                    return "Pendente";
                if (IndStatus == "F")
                    return "Finalizado";

                return string.Empty;
            }
            set
            {
                value = string.Empty;

                if (value == "Andamento")
                    IndStatus = "A";
                else if (value == "Pendente")
                    IndStatus = "P";
                else if (value == "Finalizado")
                    IndStatus = "F";
            }
        }
    }
}
