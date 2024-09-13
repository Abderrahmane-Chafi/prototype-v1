using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClientInformation
    {
        public int Id { get; set; }
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Lec champ nom est obligatoire")]
        public string Name { get; set; }
        [DisplayName("Nom de l'entreprise")]
        [Required(ErrorMessage = "Le champ Nom de l'entreprise est obligatoire")]
        public string CompanyName { get; set; }
        [DisplayName("Numéro de téléphone")]
        [Required(ErrorMessage = "Le champ Numéro de téléphone est obligatoire")]
        [MaxLength(18, ErrorMessage = "Le champ Numéro de téléphone ne peut pas dépasser 18 caractères. ")]
        [MinLength(10, ErrorMessage = "Le champ Numéro de téléphone ne peut pas contenir moins de 10 caractères. ")]
        [RegularExpression(@"^(?:\+212|07|06)[0-9 ]*$", ErrorMessage = "Le numéro de téléphone doit commencer par +212, 07 ou 06 et ne peut contenir que des +, des chiffres et des espaces.")]
        public string PhoneNumber { get; set; }
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Le champ Numéro de téléphone est obligatoire")]

        public string Email { get; set; }
        [DisplayName("Exprimez-vous")]
        [Required(ErrorMessage = "Le champ Exprimez-vous est obligatoire")]

        public string Question { get; set; }
        [DisplayName("Comment nous avez-vous trouvé ?")]
        [Required(ErrorMessage = "Le champ Comment nous avez-vous trouvé ? est obligatoire")]

        public string HowDidYouFindUs { get; set; }
    }
}
