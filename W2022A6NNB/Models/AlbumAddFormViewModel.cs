using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W2022A6NNB.Models
{
    public class AlbumAddFormViewModel : AlbumAddViewModel
    {
        [Display(Name = "Genre List")]
        public SelectList GenreList { get; set; }

        [Display(Name = "Genre Name")]
        public string GenreName { get; set; }

    }
}