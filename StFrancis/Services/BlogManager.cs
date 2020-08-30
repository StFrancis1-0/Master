using Microsoft.EntityFrameworkCore;
using StFrancis.Data;
using StFrancis.Interfaces;
using StFrancis.Models;
using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StFrancis.ViewModel.BlogDto;

namespace StFrancis.Services
{
    public class BlogManager : IBlogManager
    {
        private readonly AppDbContext _context;

        public BlogManager(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string, bool)> DeleteBlogPost(int blogId)
        {
            var blog = await _context.Blogs.Where(p => p.Id == blogId).FirstOrDefaultAsync();

            if (blog == null)
            {
                return ("NOTFOUND", false);
            }

            if (blog.Deleted == true)
            {
                return ("NOTFOUND", false);
            }

            //_context.Blogs.Remove(blog);
            blog.Deleted = true;
            await _context.SaveChangesAsync();

            return ("OK", true);

        }

        public async Task<(string, CreatedBlogResponse)> GetBlogPostById(int blogId)
        {
            var blog = await _context.Blogs.Where(p => p.Id == blogId).FirstOrDefaultAsync();

            if (blog == null)
            {
                return ("NOTFOUND", null);
            }

            if (blog.Deleted == true)
            {
                return ("NOTFOUND", null);
            }
            CreatedBlogResponse response = new CreatedBlogResponse
            {
                Id = blog.Id,
                Title = blog.Title,
                Excerpt = blog.Excerpt,
                Content = blog.Content,
                CoverImagePath = blog.CoverImagePath,
                Public = blog.Public,
                Views = blog.Views,
                Deleted = blog.Deleted,
                UpdatedAt = blog.UpdatedAt,
                CreatedAt = blog.CreatedAt
            };
            return ("OK", response);
        }

        public async Task<(string, CreatedBlogResponse)> GetBlogPostByTitle(string title)
        {
            var blog = await _context.Blogs.Where(p => p.Title.ToLower() == title.ToLower()).FirstOrDefaultAsync();

            if (blog == null)
            {
                return ("NOTFOUND", null);
            }

            if (blog.Deleted == true)
            {
                return ("NOTFOUND", null);
            }

            CreatedBlogResponse response = new CreatedBlogResponse
            {
                Id = blog.Id,
                Title = blog.Title,
                Excerpt = blog.Excerpt,
                Content = blog.Content,
                CoverImagePath = blog.CoverImagePath,
                Public = blog.Public,
                Views = blog.Views,
                Deleted = blog.Deleted,
                UpdatedAt = blog.UpdatedAt,
                CreatedAt = blog.CreatedAt
            };
            return ("OK", response);


        }

        public async Task<List<CreatedBlogResponse>> GetBlogPosts()
        {
            var blogPosts = _context.Blogs.Where(x => x.Deleted != true).Select(s => new CreatedBlogResponse
            {
                Id = s.Id,
                Title = s.Title,
                Excerpt = s.Excerpt,
                Content = s.Content,
                Public = s.Public,
                Views = s.Views,
                CoverImagePath = s.CoverImagePath,
                UpdatedAt = s.UpdatedAt,
                CreatedAt = s.CreatedAt,
                
            }).ToList();

            return blogPosts;
        }

        public async Task<(string, CreatedBlogResponse)> PublishBlog(BlogRequest request)
        {
            try
            {
                var blogPost = _context.Blogs.Where(p => p.Title.ToLower() == request.Title.ToLower()).FirstOrDefaultAsync();

                if (blogPost != null)
                {
                    return ("RECORD EXIST", null);
                }

                Blog blog = new Blog
                {
                    Title = request.Title,
                    Excerpt = request.Excerpt,
                    Content = request.Content,
                    CoverImagePath = request.CoverImagePath
                };

                _context.Blogs.Add(blog);

                if (await _context.SaveChangesAsync() > 0)
                {
                    CreatedBlogResponse response = new CreatedBlogResponse
                    {
                        Id = blog.Id,
                        Title = blog.Title,
                        Excerpt = blog.Excerpt,
                        Content = blog.Content,
                        CoverImagePath = blog.CoverImagePath,
                        Public = blog.Public,
                        Views = blog.Views,
                        Deleted = blog.Deleted,
                        UpdatedAt = blog.UpdatedAt,
                        CreatedAt = blog.CreatedAt
                    };

                    return ("CREATED", response);
                }

                return ("DB ERROR", null);

            }
            catch (Exception ex)
            {
                return ("SERVER ERROR", null);

            }

        }

        public async Task<(string, bool)> ToggleBlogPostVisibility(int blogId, Status status)
        {
            try
            {
                var blog = await _context.Blogs.Where(p => p.Id == blogId).FirstOrDefaultAsync();

                if (blog == null)
                {
                    return ("NOTFOUND", false);
                }

                if (blog.Deleted == true)
                {
                    return ("NOTFOUND", true);
                }

                blog.Public = status.Public;

                _context.Blogs.Update(blog);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return ("OK", true);
                }
                return ("DB ERROR", false);
            }
            catch (Exception ex)
            {
                return ("SERVER ERROR", false);
            }
        }

        public async Task<(string, bool)> UpdateBlogPost(int blogId, BlogRequest request)
        {
            try
            {
                var blog = await _context.Blogs.Where(p => p.Id == blogId).FirstOrDefaultAsync();

                if (blog == null)
                {
                    return ("NOTFOUND", false);
                }

                if (blog.Deleted == true)
                {
                    return ("NOTFOUND", true);
                }

                blog.Title = request.Title;
                blog.Excerpt = request.Excerpt;
                blog.Content = request.Content;
                blog.CoverImagePath = request.CoverImagePath;
                blog.UpdatedAt = DateTime.Now;

                _context.Blogs.Update(blog);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return ("OK", true);
                }
                return ("DB ERROR", false);
            }
            catch (Exception ex)
            {
                return ("SERVER ERROR", false);
            }


        }
    }
}
