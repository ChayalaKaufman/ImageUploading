using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageUploading.Data;
using homework_040119_ImageUploading.Models;

namespace homework_040119_ImageUploading.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image, string password)
        {
            string ext = Path.GetExtension(image.FileName);
            string fileName = $"{Guid.NewGuid()}{ext}";
            string fullPath = $"{Server.MapPath("/UploadedImages")}\\{fileName}";
            image.SaveAs(fullPath);
            var mgr = new ImageManager(Properties.Settings.Default.ConStr);
            int id =
                mgr.UploadImage(new Image
                {
                    FileName = fileName,
                    Password = password
                });
            return Redirect($"/home/submitMessage?id={id}&password={password}");
        }

        public ActionResult SubmitMessage(int id, string password)
        {
            SubmitMessageViewModel vm = new SubmitMessageViewModel();
            vm.Link = $"http://localhost:65079/home/viewimages?id={id}";
            vm.Password = password;
            return View(vm);
        }

        public ActionResult ViewImages(int id)
        {
            ImageManager mgr = new ImageManager(Properties.Settings.Default.ConStr);
            ImageViewModel vm = new ImageViewModel();
            vm.Image = mgr.GetImage(id);

            if (Session["BeenHere"] != null)
            {
                vm.BeenHere = true;
            }
            else
            {
                vm.BeenHere = false;
                Session["BeenHere"] = true;
            }
            return View(vm);
        }
    }
}