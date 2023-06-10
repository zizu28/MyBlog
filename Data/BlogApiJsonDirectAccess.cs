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

        // Load data from filesystem and cache them
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

        // Generic helper function to save 
        private async Task SaveAsync<T>(List<T>? list, string folder, string filename, T item)
        {
            var filepath = $@"{_option.DataPath}\{folder}\{filename}";
            await File.WriteAllTextAsync(filepath, JsonSerializer.Serialize<T>(item));
            if(list == null)
            {
                list = new();
            }
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        // Generic helper function to delete
        private void DeleteAsync<T>(List<T> list, string folder, string Id) 
        {
            var filepath = $@"{_option.DataPath}\{folder}\{Id}.json";
            try
            {
                File.Delete(filepath);
            }
            catch 
            {

            }
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
        public Task DeleteBlogPostAsync(string Id)
        {
            DeleteAsync<BlogPost>(_blogPosts, _option.BlogPostsFolder, Id);
            if(_blogPosts != null)
            {
                var item = _blogPosts.FirstOrDefault(b => b.Id == Id);
                if(item != null)
                {
                    _blogPosts.Remove(item);
                }
            }
            return Task.CompletedTask;
        }

        public Task DeleteCategoryAsync(string id)
        {
            DeleteAsync<Category>(_categories, _option.CategoriesFolder, id);
            if(_categories != null)
            {
                var item = _categories.FirstOrDefault(c => c.Id == id);
                if( item != null)
                {
                    _categories.Remove(item);
                }
            }
            return Task.CompletedTask;
        }

        public Task DeletTagAsync(string Id)
        {
            DeleteAsync<Tag>(_tags, _option.TagsFolder, Id);
            if(_tags != null)
            {
                var item = _tags.FirstOrDefault(t => t.Id == Id);
                if (item != null)
                {
                    _tags.Remove(item);
                }
            }
            return Task.CompletedTask;
        }

        public async Task<BlogPost?> GetBlogPostByIdAsync(string Id)
        {
            await LoadBlogPostsAsync();
            if(_blogPosts == null)
            {
                throw new Exception("Blog posts not found");
            }
            return _blogPosts.FirstOrDefault(b => b.Id == Id);
        }

        public async Task<int> GetBlogPostCountAsync()
        {
            await LoadBlogPostsAsync();
            if(_blogPosts == null)
            {
                return 0;
            }
            return _blogPosts.Count();
        }

        public async Task<List<BlogPost>?> GetBlogPostsAsync(int numberOfPosts, int startIndex)
        {
            await LoadBlogPostsAsync();
            return _blogPosts ?? new();
        }

        public async Task<List<Category>?> GetCategoriesAsync()
        {
            await LoadCategoriesAsync();
            return _categories ?? new();
        }

        public async Task<Category?> GetCategoryByIdAsync(string Id)
        {
            await LoadCategoriesAsync();
            if(_categories == null)
            {
                throw new Exception("Categories not found");
            }
            return _categories.FirstOrDefault(c => c.Id == Id);
        }

        public async Task<Tag?> GetTagByIdAsync(string Id)
        {
            await LoadTagsAsync();
            if(_tags == null)
            {
                throw new Exception("Tags not found");
            }
            return _tags.FirstOrDefault(t => t.Id == Id);
        }

        public async Task<List<Tag>?> GetTagsAsync()
        {
            await LoadTagsAsync();
            return _tags ?? new();
        }

        public async Task<BlogPost?> SaveBlogPostAsync(BlogPost post)
        {
            if (post.Id == null)
            {
                post.Id = Guid.NewGuid().ToString();
            }
            await SaveAsync<BlogPost>(_blogPosts, _option.BlogPostsFolder, $"{post.Id}.json", post);
            return post;
        }

        public async Task<Category?> SaveCategoryAsync(Category category)
        {
            if(category == null)
            {
                category.Id = Guid.NewGuid().ToString();
            }
            await SaveAsync<Category>(_categories, _option.CategoriesFolder, $"{category.Id}.json", category);
            return category;
        }

        public async Task<Tag?> SaveTagAsync(Tag tag)
        {
            if(tag == null)
            {
                tag.Id = Guid.NewGuid().ToString();
            }
            await SaveAsync<Tag>(_tags, _option.TagsFolder, $"{tag.Id}.json", tag);
            return tag;
        }

        // Method to clear cache
        public Task InvalidateCacheAsync()
        {
            _blogPosts = null;
            _categories = null;
            _tags = null;
            return Task.CompletedTask;
        }
    }
}
