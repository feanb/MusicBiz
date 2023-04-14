using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class ArtistMediaInfoViewModel : ArtistBaseViewModel
    {
        public ArtistMediaInfoViewModel()
        {
            MediaItems = new List<MediaItemBaseViewModel>();
        }

        public ICollection<MediaItemBaseViewModel> MediaItems { get; set; }
    }
}