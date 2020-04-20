
using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ViewModels
{
    public class AddBarArticleViewModel
    {
        public BarArticleP barArtcileP { get; set; }
       public List<BarCategory> barCategories { get; set; }
        public AddBarArticleViewModel()
        {
            barCategories = new List<BarCategory>();
            barArtcileP = new BarArticleP();
        }

    }
}
