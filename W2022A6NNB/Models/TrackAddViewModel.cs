using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class TrackAddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Composers { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public int AlbumId { get; set; }

        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }
    }
}