using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class SearchController : Controller
    {
        private readonly MyBlogContext _context;

        public SearchController(MyBlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //根据文章Type进行查询
        [HttpGet]
        public IActionResult Result(string type)
        {
            var article = _context.Article.Where(m => m.Type == type);
            return View(article.ToList());
        }
    }
}