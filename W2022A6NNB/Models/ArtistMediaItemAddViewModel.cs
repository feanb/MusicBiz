using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class ArtistMediaItemAddViewModel
    {
        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? ArtistId { get; set; }

        public string ArtistName { get; set; }

        [Required]
        public HttpPostedFileBase MediaUpload { get; set; }
    }
}