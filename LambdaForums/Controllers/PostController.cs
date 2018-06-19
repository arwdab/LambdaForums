using LambdaForums.Data;
using LambdaForums.Data.Models;
using LambdaForums.Models.Post;
using LambdaForums.Models.Reply;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaForums.Controllers {
  public class PostController : Controller {
    private readonly IPostService postService;

    public PostController(IPostService postService) {
      this.postService = postService ?? throw new ArgumentNullException(nameof(postService));
    }

    public IActionResult Index(int postId) {
      var post = postService.GetById(postId);
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