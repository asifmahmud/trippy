using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trippy.ViewModels
{
    public class SignUpViewModel
    {

        [EmailAddress]
        [Required]
        public string SignupEmail { get; set; }

        [Required]
        public string SignupUsername { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string SignupPassword { get; set; }
    }
}
