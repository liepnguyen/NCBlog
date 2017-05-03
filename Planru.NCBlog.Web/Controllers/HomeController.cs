using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Planru.NCBlog.Application.Interfaces;
using Planru.NCBlog.Persistence.EFCore;

namespace Planru.NCBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private IContentDbContext _contentDataContext;

        public HomeController(IContentDbContext dataContext)
        {
            _contentDataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
