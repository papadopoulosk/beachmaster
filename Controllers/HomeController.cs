using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using beachmaster;

namespace beachmaster
{
    public class HomeController : Controller
    {
        beachmaster.Models.beachMasterEntities storedB = new beachmaster.Models.beachMasterEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
           
            /*
            var beach1 = new beachmaster.Models.beach {beachId = 1,  name = "Xalkidiki 1", latitude = "40.029712", longitude = "23.360201" };
            var beach2 = new beachmaster.Models.beach {beachId = 2, name = "Xalkidiki 2", latitude = "40.041284", longitude = "23.483963" };

            storedB.beach.Add(beach1);
            storedB.beach.Add(beach2);
            storedB.SaveChanges();*/
            /*
            var query = "SELECT beaches.beachId, COUNT(reviews.reviewId) AS Count "
                        + "FROM reviews INNER JOIN beaches ON reviews.beachId = beaches.beachId "
                        + "GROUP BY beaches.beachId;";
            */
            var beach = storedB.beach;
            var Rreview = storedB.review;

            var data = (from r in Rreview
                        group r by new {r.beachId} into g
                        select new
                        {
                            beachId = g.Key.beachId,
                            cnt = g.Count()
                        }).ToList();
            int count = 0;
            foreach (var b in beach)
            {
                count++;
                foreach (var r in data)
                {
                    if (r.beachId == b.beachId) 
                    {
                        b.numReviews=r.cnt;// = b.numReviews + 1;
                    }
                }
            }
            //ViewBag.counts = data.ToList;
            return View(beach.ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public string Create(beachmaster.Models.beach beach)
        {
            if (ModelState.IsValid)
            {
                var newBeach = new beachmaster.Models.beach { 
                    name = beach.name, 
                    latitude = beach.latitude, 
                    longitude = beach.longitude, 
                    rate = 0, 
                    totalRates = 0, 
                    description = beach.description , 
                    approved = false, 
                    numReviews = 0, 
                    votes = 0 
                };

                storedB.beach.Add(newBeach);
                storedB.SaveChanges();
                //return RedirectToAction("Index");
                return ("Ok");
            }
            else {
                return ("Not Ok");    
            }
            
        }

        [HttpPost]
        public string Delete(int id)
        {
            var beachToDel = new beachmaster.Models.beach();
            beachToDel = storedB.beach.Find(id);
            storedB.beach.Remove(beachToDel);
            storedB.SaveChanges();

            return ("Success");
        }
    }
}
