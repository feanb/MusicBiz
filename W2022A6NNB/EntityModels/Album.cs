﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace W2022A6NNB.EntityModels
{
    public class Album
    {
        public Album()
        {
            ReleaseDate = DateTime.Now;
            Artists = new List<Artist>();
            Tracks = new List<Track>();
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        // Get from Apple iTunes Preview, Amazon, or Wikipedia
        [Required, StringLength(512)]
        public string UrlAlbum { get; set; }

        [Required]
        public string Genre { get; set; }

        // User name who looks after the album
        [Required, StringLength(200)]
        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album's background")]
        public string Background { get; set; }

        public ICollection<Artist> Artists { get; set; }

        public ICollection<Track> Tracks { get; set; }

    }
}