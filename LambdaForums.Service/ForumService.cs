using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaForums.Data;
using LambdaForums.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LambdaForums.Service {
  public class ForumService : IForum {
    private readonly ApplicationDbContext context;

    public ForumService(ApplicationDbContext context) {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task Create(Forum forum) {
      throw new System.NotImplementedException();
    }

    public Task Delete(int forumId) {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Forum> GetAll() {
      return context.Forums.Include(forum => forum.Posts);
    }

    public IEnumerable<IdentityUser> GetAllActiveUsers() {
      throw new System.NotImplementedException();
    }

    public Forum GetById(int id) {
      return context
              .Forums
              .Where(forum => forum.Id == id)
              .Include(forum => forum.Posts).ThenInclude(post => post.User)
              .Include(forum => forum.Posts).ThenInclude(post => post.Replies).ThenInclude(reply => reply.User)
              .FirstOrDefault();
    }

    public Task UpdateForumDescription(int forumId, string description) {
      throw new System.NotImplementedException();
    }

    public Task UpdateForumTitle(int forumId, string title) {
      throw new System.NotImplementedException();
    }
  }
}
