using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Article
    {
        public Article()
        {

        }
        public Article(int id, string title, string type)
        {
            Id = id;
            Title = title;
            ReleaseDateTime = DateTime.Now;
            Type = type;
        }
        
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReleaseDateTime { get; set; }

        public string Type { get; set; }

        //文章对应评论应是一对多的关系，设置导航属性Comments后，
        //会自动在的Comment表中生成AritcleId列
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
