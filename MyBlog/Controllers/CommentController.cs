using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly MyBlogContext _context;
        public CommentController(MyBlogContext context)
        {
            _context = context;
        }
        
        //创建评论
        [HttpPost]
        public IActionResult Create(int id, Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.ArticleId = id;
                _context.Add(comment);
                _context.SaveChanges();

                return Redirect("/article/details/" + id);
            }
            return View(comment);
        }

        ////根据传来的文章Id查询相关的所有评论
        //[HttpGet]
        //public IActionResult QueryComments(int id)
        //{            
        //    var comments = _context.Comment.Where(m => m.ArticleId == id);
        //    if (comments == null)
        //    {
        //        return Content("查询记录为空");
        //    }
        //    return View(comments.ToList());
        //}
    }
}