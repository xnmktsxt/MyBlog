using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using System.Linq;

namespace MyBlog.Components
{
    public class CommentViewComponent : ViewComponent
    {
        public readonly MyBlogContext _context;

        public CommentViewComponent(MyBlogContext context)
        {
            _context = context;
        }

        //根据文章Id查询所有评论
        public IViewComponentResult Invoke(int id)
        {
            var comments = _context.Comment.Where(m => m.ArticleId == id).ToList();
            return View(comments);
        }

    }
}
