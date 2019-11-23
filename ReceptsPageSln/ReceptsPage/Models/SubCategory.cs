using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    

    public class SubCategory
    {
        [ScaffoldColumn(false)]
        public int SubCategoryId { get; set; }

        public string Name { get; set; }

        public List<ArticleP> Articles { get; set; }
        public int CategoryId { set; get; }
        public virtual Category Category { get; set; }



    }
}
