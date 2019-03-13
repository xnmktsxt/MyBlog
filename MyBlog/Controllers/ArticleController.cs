using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    //对文章控制器开启用户登陆认证
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly MyBlogContext _context;

        public ArticleController(MyBlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article model)
        {
            _context.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var article = _context.Article.Find(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(int id, Article model)
        {
            var article = _context.Article.Find(id);
            article.Title = model.Title;
            article.Content = model.Content;
            article.Type = model.Type;
            _context.SaveChanges();
            return Redirect("/Article/Details/" + id);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var article = _context.Article.Find(id);
            _context.Remove(article);
            _context.SaveChanges();        
            return RedirectToAction("Index", "Home");
        }

        //允许匿名状态下对文章详细页面进行请求
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var article = _context.Article.Find(id);
            return View(article);
        }
    }
}