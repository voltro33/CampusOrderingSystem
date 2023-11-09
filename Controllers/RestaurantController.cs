﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CampusOrdering.Models;
namespace CampusOrdering.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly OrderingContext _context;


        public RestaurantController(OrderingContext context)
        {
            _context = context;
            _context.SaveChanges(); 
        }


        public IActionResult Index()
        {
            List<Restaurant> restaurants = _context.Restaurants.ToList();
            Restaurant einstein = new Restaurant
            {
                Name = "Einstein Bros. Bagels",
                LogoUrl = "~/images/einstein_logo.png",
                // Set other properties as needed
            };
            _context.Restaurants.Add(einstein);
            _context.SaveChanges();
            return View(restaurants);
        }

        public IActionResult Display(string LogoUrl)
        {
            Restaurant restaurant = _context.Restaurants.Find(LogoUrl);
            if (restaurant == null)
                return NotFound();
            return View(restaurant);
        }

        [HttpPost]
        public IActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(restaurant);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        [HttpPost]
        public IActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Update(restaurant);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        public IActionResult Delete(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ChooseRestaurant()
        {
            var restaurants = _context.Restaurants.ToList();
            return View(restaurants);
        }
    }
}