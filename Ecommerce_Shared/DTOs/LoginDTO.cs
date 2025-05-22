using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class LoginDTO
    {
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "Debe iongresar el correo")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(6, ErrorMessage ="El cambo {0} debe tener al menos {1} caracter")]

        public string Password { get; set; }    
    }
}
