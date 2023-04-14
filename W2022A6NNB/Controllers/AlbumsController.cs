using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using W2022A6NNB.Models;

namespace W2022A6NNB.Controllers
{
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();
        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }
        public ActionResult Details(int? id)
        {

            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }
        [Authorize(Roles = "Clerk")]
        [Route("Album/{id}/AddTrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());

            if (album == null) { return HttpNotFound(); }

            var trackAddform = new TrackAddFormViewModel();
            trackAddform.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            trackAddform.AlbumName = album.Name;
            trackAddform.AlbumId = album.Id;

            return View(trackAddform);
        }


        [Authorize(Roles = "Clerk")]
        [Route("Album/{id}/AddTrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel trackAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddTrack", new { id = trackAddViewModel.AlbumId });
            }

            try
            {
                var addedTrack = m.TrackAdd(trackAddViewModel);

                return addedTrack == null
                        ? RedirectToAction("AddTrack", new { id = trackAddViewModel.AlbumId })
                        : RedirectToAction("Details", "Tracks", new { id = addedTrack.Id });
            }
            catch
            {
                return RedirectToAction("AddTrack", new { id = trackAddViewModel.AlbumId });
            }
        }

    }
}