using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Data;
using _4_01ImagePW.Models;
using System.Data.SqlClient;

namespace _4_01ImagePW.Controllers
{
    public class HomeController : Controller
    {
        Manager mgr = new Manager(Properties.Settings.Default.Constr);
         List<string> correct = new List<string>();
        ViewModel vm = new ViewModel();
        public ActionResult Index()
        {
            string message = "";
            if (TempData["Message"] != null)
            {
                message = (string)TempData["Message"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image, string password)
        {            
         string filename = Guid.NewGuid().ToString();
         string extension = Path.GetExtension(image.FileName);
         string fullPath = $"{Server.MapPath("/My_Images")}\\{filename}";
         image.SaveAs(fullPath);
         int id= mgr.AddImage(filename, password);
        TempData["Message"] = $"Your file has been uploaded! You can share the following link: http://localhost:61739/Home/Image?image={id}";
         return Redirect("/home/index");
        }
        public ActionResult Image(int image, string password)
        {
            if (TempData["Message"] != null)
            {
                vm.Message = (string)TempData["Message"];
            }

            //if (password == null && (Session["CorrectPasswords"] == null || fromSession.FirstOrDefault(c => c == password) == "1"))
            //{
            //    return View(new Images { Id = image });
            //}
            //HttpCookie fromRequest = Request.Cookies["Visited"];
            //if (fromRequest != null && int.Parse(fromRequest.ToString()) == image)
            //{
            //    vm.ViewImage = true;
            //}
           Images Current = mgr.Get(image);
            var fromSession = (List<string>)Session["CorrectPasswords"];
            if (password != null)
            {
                string pw = mgr.Get(image).Password;
                if (password != pw)
                {
                    TempData["Message"] = $"Password incorrect. Please try again.";
                    return Redirect($"/home/Image?image={image}");
                }
            }
           if (password!=null || fromSession!=null && fromSession.FirstOrDefault(c => c ==Current.Password )!=null)
            {
                vm.ViewImage = true;
            }
                     
            correct.Add(password);
            Session["CorrectPasswords"] = correct;
            mgr.SetTimesViewed(image);
            vm.ImageCurrent = Current;
            vm.ImageCurrent.TimesViewed = mgr.GetTimes(image);
            return View(vm);
            
        }


    }
}