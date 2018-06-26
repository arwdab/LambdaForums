using LambdaForums.Data;
using LambdaForums.Data.Models;
using LambdaForums.Models.Post;
using LambdaForums.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaForums.Controllers {
  public class PostController : Controller {
    private readonly IPostService postService;
    private readonly IForumService forumService;
    private readonly UserManager<ApplicationUser> userManager;

    public PostController(IPostService postService, IForumService forumService, UserManager<ApplicationUser> userManager) {
      this.postService = postService ?? throw new ArgumentNullException(nameof(postService));
      this.forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
      this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public IActionResult Index(int id) {
      var post = postService.GetById(id);
      var replies = BuildPostReplies(post.Replies);

      var model = new PostIndexModel() { 
        Id = post.Id,
        Title = post.Title,
        PostContent = post.Content,
        Created = post.Created,
        AuthorId = post.User.Id,
        AuthorName = post.User.UserName,
        AuthorRating = post.User.Rating,
        AuthorImageUrl = post.User.ProfileImageUrl,
        Replies = replies
      };

      return View(model);
    }

    public IActionResult Create(int id) {
      // id is a forumId
      var forum = forumService.GetById(id);
      var model = new NewPostModel() {
        ForumId = forum.Id,
        ForumName = forum.Title,
        ForumImageUrl = forum.ImageUrl,
        AuthorName = User.Identity.Name
      };

      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(NewPostModel model) {
      var userId = userManager.GetUserId(User);
      var user = await userManager.FindByIdAsync(userId);
      var post = BuildPostModel(model, user);

      await postService.Add(post);
      // TODO: Implement User.Rating

      return RedirectToAction("Index", "Post", post.Id);
    }

    private Post BuildPostModel(NewPostModel model, ApplicationUser user) {
      return new Post() {
        Title = model.Title,
        Content = model.Content,
        Created = DateTime.Now,
        User = user
      };
    }

    private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies) {
      return replies.Select(reply =>  new PostReplyModel() {
        Id = reply.Id,
        ReplyContent = reply.Content,
        Created = reply.Created,
        PostId = reply.Post.Id,
        AuthorId = reply.User.Id,
        AuthorName = reply.User.UserName,
        AuthorImageUrl = reply.User.ProfileImageUrl,
        AuthorRating = reply.User.Rating
      });
    }
  }
}