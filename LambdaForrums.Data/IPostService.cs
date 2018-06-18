using LambdaForums.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForums.Data {
  public interface IPostService {
    Post GetById(int id);
    IEnumerable<Post> GetAll();
    IEnumerable<Post> GetFilteredPosts(string searchQuery);
    IEnumerable<Post> GetPostsByForums(int forumId);

    Task Add(Post forum);
    Task Delete(int id);
    Task EditPostContent(int forumId, string content);
  }
}
