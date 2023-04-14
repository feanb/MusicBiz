using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        [Display(Name = "Album's Name")]
        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }
    }
}