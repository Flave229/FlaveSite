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

        public List<BlogInfo> GetBlogs()
        {
            return _repository.GetBlogList();
        }

        public Blog GetBlog(int id)
        {
            return _repository.GetBlog(id);
        }
    }
}
