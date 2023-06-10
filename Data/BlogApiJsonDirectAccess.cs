using Data.Models.Interfaces;
using Data.Models.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Data
{
    public class BlogApiJsonDirectAccess : IBlogApi
    {
        BlogApiJsonDirectAccessSetting _option;

        public BlogApiJsonDirectAccess(IOptions<BlogApiJsonDirectAccessSetting> option)
        {
            _option = option.Value;

            if (!Directory.Exists(_option.DataPath))
            {
                Directory.CreateDirectory(_option.DataPath);
            }

            if (!Directory.Exists($@"{_option.DataPath}\{_option.BlogPostsFolder}"))
            {
                Directory.CreateDirectory($@"{_option.DataPath}\{_option.BlogPostsFolder}");
            }

            if (!Directory.Exists($@"{_option.DataPath}\{_option.CategoriesFolder}"))
            {
                Directory.CreateDirectory($@"{_option.DataPath}\{_option.CategoriesFolder}");
            }

            if (!Directory.Exists($@"{_option.DataPath}\{_option.TagsFolder}"))
            {
                Directory.CreateDirectory($@"{_option.DataPath}\{_option.TagsFolder}");
            }
        }
        public Task<BlogPost?> DeleteBlogPostAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> DeleteCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> DeletTagAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetBlogPostByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetBlogPostCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPost>?> GetBlogPostsAsync(int numberOfPosts, int startIndex)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>?> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetCategoryByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> GetTagByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tag>?> GetTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> SaveBlogPostAsync(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> SaveCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> SaveTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
