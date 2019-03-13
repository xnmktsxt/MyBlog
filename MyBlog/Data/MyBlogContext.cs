using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyBlog.Models
{
    //在Entity中此类负责数据对象和数据库之间的交互
    //因为对用户登陆注册的寻求,所以将当前类继承自IdentityDbContext
    public class MyBlogContext : IdentityDbContext<User>
    {
        //DbContextOptions对象主要用在当在DI容器中创建DbContext实例时会用到
        //DI容器知道如何初始化配置对象及其依赖的所有对象
        public MyBlogContext (DbContextOptions<MyBlogContext> options)
            : base(options)
        {
        }

        public MyBlogContext()
        {

        }

        public DbSet<MyBlog.Models.User> User { get; set; }
        public DbSet<MyBlog.Models.Article> Article { get; set; }
        public DbSet<MyBlog.Models.Comment> Comment { get; set; }
    }
}
