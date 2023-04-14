using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace W2022A6NNB.EntityModels
{
    public class MediaItem
    {
        public MediaItem()
        {
            Timestamp = DateTime.Now;

            //Stringid generator but is it nessecarry??
            long sId = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                sId *= ((int)b + 1);
            }
            StringId = string.Format("{0:x}", sId - DateTime.Now.Ticks);
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Caption { get; set; }

        public byte[] Content { get; set; }

        [StringLength(200)]
        public string ContentType { get; set; }

        [Required, StringLength(100)]
        public string StringId { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public Artist Artist { get; set; }


    }
}