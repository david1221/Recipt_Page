﻿using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Interfaces
{
  public  interface IArticleAndBar

    {
       // IEnumerable<BarArticleP> BarArticleImage();
        IEnumerable<ArticleP> ArticleImage();
    }
}
