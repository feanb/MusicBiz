using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class ArtistWithDetailsViewModel : ArtistBaseViewModel
    {
        public ArtistWithDetailsViewModel()
        {
            AlbumNames = new List<AlbumBaseViewModel>();
        }
        public IEnumerable<AlbumBaseViewModel> AlbumNames { get; set; }


    }
}