using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP2084GetMeAGame.Data;
using Microsoft.AspNetCore.Mvc;

namespace COMP2084GetMeAGame.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _contaxt;

        public ShopController(ApplicationDbContext context)
        {
            _contaxt = context;
        }
        public IActionResult Index()
        {
            var categories = _contaxt.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }
    }
}
