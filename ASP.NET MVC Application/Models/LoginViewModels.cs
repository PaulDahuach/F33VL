using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_Application.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        [Display(Name = "Recordar?")]
        public bool Recordar { get; set; }
    }
}
