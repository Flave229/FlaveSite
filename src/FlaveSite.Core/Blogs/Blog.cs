using System;

namespace FlaveSite.Core.Blogs
{
    public class BlogInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }

    public class Blog
    {
        public BlogInfo BlogInformation { get; set; }
        public string Content { get; set; }
    }
}