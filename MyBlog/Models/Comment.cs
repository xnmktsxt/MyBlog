using System;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Comment
    {
        public Comment()
        {

        }

        public Comment(string commenter, int articleId)
        {
            Commenter = commenter;
            ArticleId = articleId;
            ReleaseDateTime = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Commenter { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReleaseDateTime { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int ArticleId { get; set; }
    }
}
