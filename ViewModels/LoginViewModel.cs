using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trippy.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string LoginUsername { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string LoginPassword { get; set; }
    }
}
