using Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Interfaces
{
    public interface IBlogApi
    {
        Task<int> GetBlogPostCountAsync();
        Task<List<BlogPost>?> GetBlogPostsAsync(int numberOfPosts, int startIndex);
        Task<List<Category>?> GetCategoriesAsync();
        Task<List<Tag>?> GetTagsAsync();
        Task<BlogPost?> GetBlogPostByIdAsync(string Id);
        Task<Category?> GetCategoryByIdAsync(string Id);
        Task<Tag?> GetTagByIdAsync(string Id);
        Task<BlogPost?> SaveBlogPostAsync(BlogPost post);
        Task<Category?> SaveCategoryAsync(Category category);
        Task<Tag?> SaveTagAsync(Tag tag);
        Task DeleteBlogPostAsync(string Id);
        Task DeleteCategoryAsync(string id);
        Task DeletTagAsync(string Id);
    }
}
