using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        public readonly MyBlogContext _context;
        
        public HomeController(MyBlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Article.ToList().OrderByDescending(m => m.Id));
        }

        //public IActionResult Category()
        //{
        //    return View();
        //}

        //public IActionResult Archive()
        //{
        //    return View();
        //}

        public IActionResult About()
        {
            return View();
        }

        //http缓存相关知识
        //一定要允许匿名访问改方法，以便在未登录下显示错误页面
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
