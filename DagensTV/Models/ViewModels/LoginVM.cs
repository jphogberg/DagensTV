using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Ogiltigt användarnamn eller lösenord")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ogiltigt användarnamn eller lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}