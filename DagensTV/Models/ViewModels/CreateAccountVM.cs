using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DagensTV.Models.ViewModels
{
    public class CreateAccountVM
    {
        [Required(ErrorMessage = "Ange ett förnamn")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Ange ett efternamn")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Ange ett användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ange ett lösenord")]
        public string Password { get; set; }

    }
}