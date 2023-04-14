using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.Models
{
    public class MediaItemBaseViewModel
    {
        public MediaItemBaseViewModel()
        {
            Timestamp = DateTime.Now;

            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            StringId = string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }


        [Required, StringLength(100)]
        [Display(Name = "Unique identifier")]
        public string StringId { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Added on date/time")]
        public DateTime Timestamp { get; set; }

        public string ContentType { get; set; }
    }
}