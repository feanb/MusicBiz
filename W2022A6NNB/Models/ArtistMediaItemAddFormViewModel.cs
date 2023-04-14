using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class ArtistMediaItemAddFormViewModel
    {
        [Required, StringLength(100)]
        [Display(Name = "Description")]
        public string Caption { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? ArtistId { get; set; }

        public string ArtistName { get; set; }

        [Required]
        [Display(Name = "Media item"), DataType(DataType.Upload)]
        public String MediaUpload { get; set; }
    }
}