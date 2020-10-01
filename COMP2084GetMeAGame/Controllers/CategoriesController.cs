using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP2084GetMeAGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2084GetMeAGame.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            //User the category model to create 10 fake categories and send to the view for display
            //Create an empty list of categories
            var categories = new List<Category>();

            //Use a loop to create 10 fake categories
            for (var i = 1; i <= 10; i++)
            {
                categories.Add(new Category { Id = 1, Name = "Category" + i.ToString() });
            }

            //Pass the final list to the view for display
            return View(categories);
        }

        public IActionResult Browse(string categoryName)
        {
            //take the category name passed in with the link and store it in the viewbag for display
            ViewBag.categoryName = categoryName;
            return View();
        }

        public IActionResult AddCategory()
        {
            //Display an empty form where user could add new category
            return View();
        }
    }
}
