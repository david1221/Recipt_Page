using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    

    public class BarCategory
    {
        [ScaffoldColumn(false), Key]
        public int BarCategoryId { get; set; }

        public string Name { get; set; }

        public List<BarArticleP> BarArticles { get; set; }

        public BarCategory()
        {
            BarArticles = new List<BarArticleP>();
        }



    }
}
