using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W2022A6NNB.Controllers
{
    public class MediaItemController : Controller
    {
        Manager m = new Manager();

        // GET: ArtistMediaItem
        public ActionResult Index()
        {
            return View("index", "home");
        }

        // GET: ArtistMediaItem/5
        [Route("media/{stringId}")]
        public ActionResult Details(string stringId = "")
        {
            var o = m.MediaItemGetById(stringId);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return File(o.Content, o.ContentType);
            }
        }

        // GET: ArtistMediaItem/5/Download
        [Route("media/{stringId}/download")]
        public ActionResult DetailsDownload(string stringId = "")
        {
            // Attempt to get the matching object
            var o = m.MediaItemGetById(stringId);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {

                string extension;
                RegistryKey key;
                object value;


                key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + o.ContentType, false);

                value = (key == null) ? null : key.GetValue("Extension", null);

                extension = (value == null) ? string.Empty : value.ToString();

                var fileName = "none";
                if (o.ContentType.Contains("image/"))
                    fileName = "img-";
                else if (o.ContentType.Contains("audio/"))
                    fileName = "audio-";
                else if (o.ContentType.Contains("word"))
                    fileName = "msword-";
                else if (o.ContentType.Contains("pdf"))
                    fileName = "pdf-";
                else if (o.ContentType.Contains("excel"))
                    fileName = "excel-";

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = $"{fileName}{stringId}{extension}",
                    Inline = false
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());

                return File(o.Content, o.ContentType);
            }
        }
    }
}
