﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using beachmaster;


namespace beachmaster.Controllers
{
    public class beachController : Controller
    {
        beachmaster.Models.beachMasterEntities storedB = new beachmaster.Models.beachMasterEntities();
        //
        // GET: /beach/

        public ActionResult Index()
        {
            return RedirectToAction("index", "home");
        }

        //
        // GET: /beach/Details/5

        public ActionResult Details(int id = 0)
        {
            if (id != 0) {
                var reviews = storedB.review.Where(i => i.beachId == id).OrderByDescending(i => i.reviewDate);
                ViewBag.beach = storedB.beach.Find(id);
                return View(reviews);
            } else {
                return RedirectToAction("index", "home");
            }

        }

        [HttpGet]
        public string DetailsAsync(int data)
        {
            var beach = storedB.beach.Where(i => i.beachId == data).First();
            string json = "{\"beachId\":" + beach.beachId.ToString() + ",\"name\":\"" + beach.name + "\",\"description\":\"" + beach.description + "\",\"rate\":\"" + beach.rate + "\"}";

            return json;
        }

        public ActionResult findNeighbors(double lat, double lon)
        {
            var beach = storedB.beach.Where(i => Convert.ToDouble(i.longitude) < lon).
                Where(i => Convert.ToDouble(i.latitude) < lat);
            return View(beach.Count());
        }


        //
        // GET: /beach/Create

        [HttpPost]
        public ActionResult Createa(beachmaster.Models.beach beach)
        {
            return View(beach);
        } 

        //
        // POST: /beach/Create

        [HttpPost]
        public ActionResult Create(beachmaster.Models.beach beach)
        {
            if (ModelState.IsValid)
            {
                var newBeach = new beachmaster.Models.beach { 
                    name = beach.name, 
                    latitude = beach.latitude, 
                    longitude = beach.longitude, 
                    description = beach.description,
                    approved = false                  
                };
                storedB.beach.Add(newBeach);
                storedB.SaveChanges();
                return RedirectToAction("index", "home");
                //return ("Ok");
            }
            else
            {
                return RedirectToAction("index", "home");
            }

        }
        
        //
        // GET: /beach/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /beach/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /beach/Delete/5
        /*
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /beach/Delete/5
        */
        [HttpPost]
        public string Delete(int id)
        {
            var beachToDel = new beachmaster.Models.beach();
            beachToDel = storedB.beach.Find(id);
            storedB.beach.Remove(beachToDel);
            storedB.SaveChanges();

            return ("Success");
        }

        //Add comment for existing beach
        public ActionResult addReview(int beachId = 0)
        {
            var review = new beachmaster.Models.review();
            var beachNum = storedB.beach.Where(i => i.beachId == beachId).Count();
            if (beachNum > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public ActionResult addReview(beachmaster.Models.review review)
        {
            if (ModelState.IsValid)
            {
                //var newBeach = new beachmaster.Models.beach { name = beach.name, latitude = beach.latitude, longitude = beach.longitude };
                storedB.review.Add(review);
                storedB.SaveChanges();
                return RedirectToAction("Details", "beach", new { id = review.beachId });
                //return ("Ok");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public string submitRate(int rate = 0)
        {
            Uri MyUrl = Request.UrlReferrer;
            if (Request.UrlReferrer.Segments[1] == "beach/" && Request.UrlReferrer.Segments[2] == "Details/")
            {
                if (rate > 0 && rate < 6)
                {
                    int bId = System.Int16.Parse(Request.UrlReferrer.Segments[3]);
                    beachmaster.Models.beach b = storedB.beach.Find(bId);
                    b.submitRate(rate);
                    storedB.SaveChanges();
                    return b.rate + " - " + b.totalRates;
                }
                else
                {
                    return "Not Ok";
                }
            }
            else
            {
                return "Not Ok";
            }
                   
        }            
    }
}