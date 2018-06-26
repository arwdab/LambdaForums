using LambdaForums.Data;
using LambdaForums.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaForums.Service {
  public class PostService : IPostService {
    private readonly ApplicationDbContext context;

    public PostService(ApplicationDbContext context) {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Post post) {
      context.Add(post);
      await context.SaveChangesAsync();
    }

    public Task Delete(int id) {
      throw new NotImplementedException();
    }

    public Task EditPostContent(int forumId, string content) {
      throw new NotImplementedException();
    }

    public IEnumerable<Post> GetAll() {
      throw new NotImplementedException();
    }

    public Post GetById(int id) {
      return context
                .Posts
                .Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies)?
                  .ThenInclude(reply => reply.User)
                .Include(post => post.Forum)
                .First();
    }

    public IEnumerable<Post> GetFilteredPosts(string searchQuery) {
      throw new NotImplementedException();
    }

    public IEnumerable<Post> GetPostsByForums(int forumId) {
      return context
                .Forums
                .Where(forum => forum.Id == forumId)
                .FirstOrDefault()
                ?.Posts;
    }
  }
}