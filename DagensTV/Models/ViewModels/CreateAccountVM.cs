using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class CreateAccountVM
    {
        [Required(ErrorMessage = "Fyll i förnamn")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Fyll i efternamn")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Fyll i ett användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fyll i ett lösenord")]
        public string Password { get; set; }

    }
}