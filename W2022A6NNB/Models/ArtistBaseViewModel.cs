using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class ArtistBaseViewModel
    {
        public ArtistBaseViewModel()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth date or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist profile")]
        public string Portrayal { get; set; }
    }
}