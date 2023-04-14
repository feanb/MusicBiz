using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class AlbumBaseViewModel
    {
        public AlbumBaseViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album Image (Cover art)")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album Background")]
        public string Background { get; set; }
    }
}