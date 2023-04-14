using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class TrackEditViewModel
    {
        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }

        [Required]
        public int TrackId { get; set; }
    }
}