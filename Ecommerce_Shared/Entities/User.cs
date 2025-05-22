using Ecommerce.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entities
{
    public class User : IdentityUser
    {
        [Display(Name ="El Documento es Obligatorio")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [MaxLength(100)]

        public string Document {  get; set; }

        [Display(Name ="Nombre")]
        [MaxLength (50, ErrorMessage ="El campo {0} debe tener minimos {1} caracter")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener minimos {1} caracter")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string LastName { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener minimos {1} caracter")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Address { get; set; }

        [Display(Name ="Tipo de usuario")]

        public UserType UserType { get; set; }

        public City? City { get; set; }

        [Display(Name ="Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage ="Debes seleccionar una {0}.")]

        public int CityId { get; set; }

        [Display(Name ="Usuario")]

        public string FullName => $"{FirstName} {LastName}";

    }
}
