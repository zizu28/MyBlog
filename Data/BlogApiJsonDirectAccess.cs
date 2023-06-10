using Data.Models.Interfaces;
using Data.Models.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Data
{
    public class BlogApiJsonDirectAccess : IBlogApi
    {
        BlogApiJsonDirectAccessSetting _option;
        private List<BlogPost>? _blogPosts;
        private List<Category>? _categories;
        private List<Tag>? _tags;

        private void Load<T>(ref List<T>? list, string folder)
        {
            if(list == null)
            {
                list = new();
                var fullPath = $@"{_option.DataPath}/{folder}";
                foreach(var file in Directory.GetFiles(fullPath))
                {
                    var json = File.ReadAllText(file);
                    var bp = JsonSerializer.Deserialize<T>(json);
                    if(bp != null)
                    {
                        list.Add(bp);
                    }
                }
            }
        }

        private Task LoadBlogPostsAsync()
        {
            Load<BlogPost>(ref _blogPosts, _option.BlogPostsFolder);
            return Task.CompletedTask;
        }

        private Task LoadTagsAsync()
        {
            Load<Tag>(ref _tags, _option.TagsFolder);
            return Task.CompletedTask;
        }

        private Task LoadCategoriesAsync()
        {
            Load<Category>(ref  _categories, _option.CategoriesFolder);
            return Task.CompletedTask;
        }

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
