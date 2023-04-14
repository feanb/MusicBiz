using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class AlbumAddViewModel
    {
        public AlbumAddViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        [Required]
        [StringLength(160)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [DataType(DataType.Date), Required]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Url to Album Image (Cover art)")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album's primary genre")]
        public String Genre { get; set; }

        [StringLength(220)]
        [Display(Name = "Coordinator who looks after the album")]
        public string Coordinator { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Artist Id")]
        public int? ArtistId { get; set; }

        [StringLength(120)]
        [Display(Name = "Artist name or stage name")]
        public string ArtistName { get; set; }

        [Display(Name = "Album Background")]
        [DataType(DataType.MultilineText)]
        public string Background { get; set; }

    }
}