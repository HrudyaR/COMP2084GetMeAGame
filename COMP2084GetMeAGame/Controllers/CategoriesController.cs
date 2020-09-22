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
            var categories = new List<Category>();
            for (var i = 1; i <= 10; i++)
            {
                categories.Add(new Category { Id = 1, Name = "Category" + i.ToString() });
            }
            return View(categories);
        }

        public IActionResult Browse(string categoryName)
        {
            //take the category name passed in with the link and store it in the viewbag for display
            ViewBag.categoryName = categoryName;
            return View();
        }
    }
}
