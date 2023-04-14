using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class GenreBaseViewModel
    {
        [Display(Name = "Genre Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Genre Name")]
        public string Name { get; set; }
    }
}