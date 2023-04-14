using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class TrackEditFormViewModel
    {
        public int TrackId { get; set; }

        public string TrackName { get; set; }

        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }
    }
}