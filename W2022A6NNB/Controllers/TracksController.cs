using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using W2022A6NNB.Models;

namespace W2022A6NNB.Controllers
{
    //  [Authorize]
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        public ActionResult Details(int? id)
        {
            var o = m.TrackGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        [Authorize(Roles = "Clerk")]
        [Route("Audio/{id}/Clip")]
        public ActionResult TrackAudio(int? id)
        {

            var o = m.TrackClipGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return File(o.Audio, o.AudioContentType);
            }
        }


        [Authorize(Roles = "Clerk")]
        public ActionResult Edit(int? id)
        {
            var obj = m.TrackGetById(id.GetValueOrDefault());

            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new TrackEditFormViewModel();
                form.TrackName = obj.Name;
                form.TrackId = obj.Id;
                return View(form);
            }
        }

        // POST: Tracks/Edit/5
        [Authorize(Roles = "Clerk")]
        [HttpPost]
        public ActionResult Edit(int? id, TrackEditViewModel editTrack)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = editTrack.TrackId });
            }

            if (id.GetValueOrDefault() != editTrack.TrackId)
            {
                return RedirectToAction("Index");
            }

            var track = m.TrackEdit(editTrack);

            return track == null
                    ? RedirectToAction("Edit", new { id = editTrack.TrackId })
                    : RedirectToAction("Details", new { id = editTrack.TrackId });

        }

        [Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id)
        {
            var obj = m.TrackGetById(id.GetValueOrDefault());

            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }

        // POST: Track/Delete/5
        [Authorize(Roles = "Clerk")]
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            m.TrackDelete(id.GetValueOrDefault());

            return RedirectToAction("Index");
        }


    }
}