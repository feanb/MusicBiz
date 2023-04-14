using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W2022A6NNB.Models
{
    public class TrackAddFormViewModel
    {
        [Display(Name = "Track name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Composer names (comma-separated")]
        [Required]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        [Required]
        public MultiSelectList GenreList { get; set; }

        [Required]
        [Display(Name = "Sample clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }
    }
}