﻿// ************************************************************************************
// WEB524 Project Template V3 2221 == 4b104863-bcfb-427e-ae16-be325efe88e6
// Do not change or remove the line above.
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using W2022A6NNB.EntityModels;
using W2022A6NNB.Models;

namespace W2022A6NNB.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailsViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<Artist, ArtistMediaInfoViewModel>();

                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailsViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Track, TrackClipViewModel>();

                cfg.CreateMap<MediaItem, MediaItemBaseViewModel>();
                cfg.CreateMap<ArtistMediaItemAddViewModel, MediaItem>();
                cfg.CreateMap<MediaItem, MediaItemContentViewModel>();


            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


     
        //Genre Methods
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(g => g.Name));
        }


        //Artists Methods
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(
                ds.Artists.OrderBy(a => a.Name)
                );
        }
        public ArtistWithDetailsViewModel ArtistGetById(int id)
        {
            var artist = ds.Artists.SingleOrDefault(a => a.Id == id);
            return artist == null ? null : mapper.Map<Artist, ArtistWithDetailsViewModel>(artist);
        }
        public ArtistWithDetailsViewModel ArtistAdd(ArtistAddViewModel newItem)
        {
            var user = HttpContext.Current.User.Identity.Name;

            var addedItem = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newItem));

            //TODO
            addedItem.Executive = user;
            ds.SaveChanges();
            return addedItem == null ? null : mapper.Map<Artist, ArtistWithDetailsViewModel>(addedItem);

        }

        public ArtistMediaInfoViewModel ArtistGetByIdWithMediaInfo(int id)
        {
            var o = ds.Artists.Include("MediaItems").SingleOrDefault(a => a.Id == id);
            return o == null ? null : mapper.Map<Artist, ArtistMediaInfoViewModel>(o);
        }


        //Album methods
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = ds.Albums.OrderBy(a => a.Name);

            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albums);
        }
        public AlbumWithDetailsViewModel AlbumGetById(int id)
        {
            var album = ds.Albums.Include("Artists").SingleOrDefault(t => t.Id == id);
            return mapper.Map<Album, AlbumWithDetailsViewModel>(album);

        }
        public AlbumBaseViewModel AlbumAdd(AlbumAddViewModel newItem)
        {
            var a = ds.Artists.Find(newItem.ArtistId);

            if (a == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
                addedItem.Artists.Add(a);
                addedItem.Coordinator = User.Name;

                ds.SaveChanges();

                return mapper.Map<Album, AlbumBaseViewModel>(addedItem);

            }

        }

        public AlbumWithDetailsViewModel AlbumGetByIdWithDetail(int id)
        {
            var a = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(v => v.Id == id);
            return mapper.Map<Album, AlbumWithDetailsViewModel>(a);
        }


        //Tracks methods
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var track = ds.Tracks.Include("Albums").OrderBy(t => t.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(track);
        }

        public TrackBaseViewModel TrackGetById(int id)
        {
            var track = ds.Tracks.Find(id);

            return track == null ? null : mapper.Map<Track, TrackBaseViewModel>(track);
        }

        public TrackClipViewModel TrackClipGetById(int id)
        {
            var track = ds.Tracks.Find(id);

            return (track.Audio == null || track.AudioContentType == null) ? null : mapper.Map<Track, TrackClipViewModel>(track);
        }


        public TrackBaseViewModel TrackAdd(TrackAddViewModel trackAddViewModel)
        {
            var album = ds.Albums.Find(trackAddViewModel.AlbumId);

            if (album == null) { return null; }

            var track = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(trackAddViewModel));
            track.Clerk = User.Name;
            track.Albums = new List<Album>() { album };
            byte[] sampleClipBytes = new byte[trackAddViewModel.AudioUpload.ContentLength];

            trackAddViewModel.AudioUpload.InputStream.Read(
                sampleClipBytes,
                0,
                trackAddViewModel.AudioUpload.ContentLength
            );

            track.Audio = sampleClipBytes;
            track.AudioContentType = trackAddViewModel.AudioUpload.ContentType;

            ds.SaveChanges();

            return track == null ? null : mapper.Map<Track, TrackBaseViewModel>(track);
        }

        public TrackBaseViewModel TrackEdit(TrackEditViewModel track)
        {
            var pretrack = ds.Tracks.Find(track.TrackId);

            if (pretrack == null)
            {
                return null;
            }
            else
            {
                byte[] audioByte = new byte[track.AudioUpload.ContentLength];
                track.AudioUpload.InputStream.Read(audioByte, 0, track.AudioUpload.ContentLength);

                pretrack.AudioContentType = track.AudioUpload.ContentType;
                pretrack.Audio = audioByte;

                ds.SaveChanges();

                return (pretrack == null) ? null : mapper.Map<Track, TrackBaseViewModel>(pretrack);
            }
        }
        public bool TrackDelete(int id)
        {
            var deleteTrack = ds.Tracks.SingleOrDefault(t => t.Id == id);

            if (deleteTrack == null)
            {
                return false;
            }
            else
            {
                try
                {
                    ds.Tracks.Remove(deleteTrack);
                    ds.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        //MediaItem Methods
        public MediaItemContentViewModel MediaItemGetById(string stringId)
        {
            var o = ds.MediaItem.SingleOrDefault(a => a.StringId == stringId);
            return o == null ? null : mapper.Map<MediaItem, MediaItemContentViewModel>(o);
        }

        public ArtistMediaInfoViewModel ArtistMediaItemAdd(ArtistMediaItemAddViewModel newItem)
        {
            var artist = ds.Artists.Find(newItem.ArtistId);
            if (artist == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.MediaItem
                    .Add(mapper.Map<ArtistMediaItemAddViewModel, MediaItem>(newItem));
                addedItem.Artist = artist;

                byte[] itemBytes = new byte[newItem.MediaUpload.ContentLength];
                newItem.MediaUpload.InputStream.Read(itemBytes, 0, newItem.MediaUpload.ContentLength);
                addedItem.Content = itemBytes;
                addedItem.ContentType = newItem.MediaUpload.ContentType;

                ds.SaveChanges();

                return addedItem == null ? null : mapper.Map<Artist, ArtistMediaInfoViewModel>(artist);
            }
        }





        // *** Add your methods above this line **


        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        #region Load Data Methods

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // *** Genres ***
            if (ds.Genres.Count() == 0)
            {
                // Add genres here
                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // *** Artists ***
            if (ds.Artists.Count() == 0)
            {
                // Add artists here

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/800px-Adele_2016.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // *** Albums ***
            if (ds.Albums.Count() == 0)
            {
                // Add albums here

                // For "Bryan Adams"
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // *** Tracks ***
            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For "Reckless"
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For "So Far So Good"
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Removedatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    #endregion

    #region RequestUser Class


    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion

}