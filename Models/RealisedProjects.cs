using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RealisedProjects
    {
        public int Id { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }

    }
}
