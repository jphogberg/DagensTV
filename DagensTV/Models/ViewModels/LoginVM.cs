using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Användarnamn saknas!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lösenord saknas!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}