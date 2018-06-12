using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaForums.Data;
using LambdaForums.Models.Forum;
using Microsoft.AspNetCore.Mvc;

namespace LambdaForums.Controllers {
  public class ForumController : Controller {
    private readonly IForum forum;
    public ForumController(IForum forum) {
      this.forum = forum ?? throw new ArgumentNullException(nameof(forum));
    }

    public IActionResult Index() {
      var forums = forum
                    .GetAll()
                    .Select(f => new ForumListingModel() {
                                        Id = f.Id,
                                        Name = f.Title,
                                        Description = f.Description });

      var model = new ForumIndexModel() { ForumList = forums };

      return View(model);
    }

    public IActionResult Topic(int id) {
      var f = forum.GetById(id);

      var model = new ForumIndexModel() { ForumList = forums };

      return View(model);
    }
  }
}