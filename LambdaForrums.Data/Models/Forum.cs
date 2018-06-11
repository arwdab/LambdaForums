﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaForrums.Data.Models {
  public class Forum {
    public int Id { get; set; }
    public string Title { get; set; }
    public string gDescription { get; set; }
    public DateTime Created { get; set; }
    public string ImageUrl { get; set; }

    public virtual IEnumerable<Post> Posts { get; set; }
  }
}
