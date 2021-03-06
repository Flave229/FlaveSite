﻿using System.Collections.Generic;
using Npgsql;

namespace FlaveSite.Core.Blogs
{
    public class BlogRepository
    {
        private readonly NpgsqlConnection _connection;

        public BlogRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        
        public List<BlogInfo> GetBlogList()
        {
            _connection.Open();

            try
            {
                const string query = @"SELECT 
                                        Blog.id,
                                        Blog.title,
                                        Blog.date
                                    FROM Blog
                                    ORDER BY Blog.date DESC";
                var command = new NpgsqlCommand(query, _connection);
                var reader = command.ExecuteReader();

                var blogs = new List<BlogInfo>();
                while (reader.Read())
                {
                    var blog = new BlogInfo
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Date = reader.GetDateTime(2)
                    };

                    blogs.Add(blog);
                }

                reader.Close();
                return blogs;
            }
            finally
            {
                _connection.Close();
            }
        }

        public Blog GetBlog(int id)
        {
            _connection.Open();

            try
            {
                string query = $@"SELECT 
                                    Blog.id,
                                    Blog.title,
                                    Blog.date,
                                    Blog.contents
                                FROM Blog
                                WHERE Blog.id = {id}";
                var command = new NpgsqlCommand(query, _connection);
                var reader = command.ExecuteReader();

                Blog blog = null;
                while (reader.Read())
                {
                    blog = new Blog
                    {
                        BlogInformation = new BlogInfo
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Date = reader.GetDateTime(2)
                        },
                        Content = reader.GetString(3)
                    };
                }

                reader.Close();
                return blog;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}