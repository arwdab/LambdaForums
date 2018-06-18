using LambdaForums.Data;
using LambdaForums.Data.Models;
using LambdaForums.Models.Forum;
using LambdaForums.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LambdaForums.Controllers {
  public class ForumController : Controller {
    private readonly IForumService forumService;
    private readonly IPostService postService;
    public ForumController(IForumService forumService) {
      this.forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
    }

    public IActionResult Index() {
      var forums = forumService
                    .GetAll()
                    .Select(f => new ForumListingModel() {
                                        Id = f.Id,
                                        Name = f.Title,
                                        Description = f.Description });

      var model = new ForumIndexModel() { ForumList = forums };

      return View(model);
    }

    public IActionResult Topic(int id) {
      var forum = forumService.GetById(id);
      var posts = forum.Posts;

      var postListings = posts.Select(post => new PostListingModel() {
        Id = post.Id,
        AuthorId = post.User.Id,
        AuthorName = post.User.UserName,
        AuthorRating = post.User.Rating,
        Title = post.Title,
        DatePosted = post.Created.ToLongDateString(),
        RepliesCount = post.Replies.Count(),
        Forum = BuildForumListing(post)
      });

      var model = new ForumTopicModel() {
        Forum = BuildForumListing(forum),
        Posts = postListings,
      };

      return View(model);
    }

    private ForumListingModel BuildForumListing(Forum forum) {
      return new ForumListingModel() {
        Id = forum.Id,
        Name = forum.Title,
        Description = forum.Description,
        ImageUrl = forum.ImageUrl
      };
    }

    private ForumListingModel BuildForumListing(Post post) {
      return BuildForumListing(post.Forum);
    }
  }
}