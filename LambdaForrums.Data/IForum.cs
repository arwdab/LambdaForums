using LambdaForums.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForums.Data {
  public interface IForum {
    Forum GetById(int id);
    IEnumerable<Forum> GetAll();
    IEnumerable<IdentityUser> GetAllActiveUsers();

    Task Create(Forum forum);
    Task Delete(int forumId);
    Task UpdateForumTitle(int forumId, string title);
    Task UpdateForumDescription(int forumId, string description);
  }
}
