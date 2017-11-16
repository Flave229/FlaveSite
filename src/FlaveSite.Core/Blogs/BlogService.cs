using System.Collections.Generic;

namespace FlaveSite.Core.Blogs
{
    public class BlogService
    {
        private readonly BlogRepository _repository;

        public BlogService(BlogRepository repository)
        {
            _repository = repository;
        }

        public List<Blog> GetBlogs()
        {
            return _repository.GetBlogList();
        }
    }
}
