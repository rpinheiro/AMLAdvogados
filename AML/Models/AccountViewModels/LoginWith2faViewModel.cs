using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AML.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "A senha informada é diferente da senha informada no campo de confirmação.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Código de Autorização")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Lembrar máquina")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
