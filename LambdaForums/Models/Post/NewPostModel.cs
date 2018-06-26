using LambdaForums.Models.Reply;
using System;
using System.Collections.Generic;

namespace LambdaForums.Models.Post {
  public class NewPostModel {
    public int ForumId { get; set; }
    public string ForumName { get; set; }
    public string ForumImageUrl { get; set; }
    public string AuthorName { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }
    /*
    public int Id { get; set; }
    public string AuthorId { get; set; }
    public string AuthorImageUrl { get; set; }
    public int AuthorRating { get; set; }
    public DateTime Created { get; set; }
    public IEnumerable<PostReplyModel> Replies { get; set; }
    */
  }
}
