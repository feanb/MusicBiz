using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class MediaItemContentViewModel
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}