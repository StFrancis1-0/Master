using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StFrancis.ViewModel.BlogDto;

namespace StFrancis.Interfaces
{
    public interface IBlogManager
    {
        Task<(string, CreatedBlogResponse)> PublishBlog(BlogRequest request);
        Task<List<CreatedBlogResponse>> GetBlogPosts();
        Task<(string, CreatedBlogResponse)> GetBlogPostByTitle(string title);
        Task<(string, CreatedBlogResponse)> GetBlogPostById(int blogId);
        Task<(string, bool)> UpdateBlogPost(int blogId, BlogRequest request);
        Task<(string, bool)> DeleteBlogPost(int blogId);
        Task<(string, bool)> ToggleBlogPostVisibility(int blogId, Status status);
    }
}
