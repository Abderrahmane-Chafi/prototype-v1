using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FreeGuideEmails
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lec champ est obligatoire")]
        public string Email { get; set; }
    }
}
