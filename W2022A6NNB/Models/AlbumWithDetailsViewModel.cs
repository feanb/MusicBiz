using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class AlbumWithDetailsViewModel : AlbumBaseViewModel
    {
        public AlbumWithDetailsViewModel()
        {
            Artists = new List<ArtistBaseViewModel>();
            ArtistsCount = Artists.Count();

        }

        [Display(Name = "List of Artists")]
        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }



        [Display(Name = "Number of tracks on this album")]
        public int TracksCount { get; set; }

        [Display(Name = "Number of artists on this album")]
        public int ArtistsCount { get; set; }
    }
}