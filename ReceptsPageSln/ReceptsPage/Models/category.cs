using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    public class category
    {
        [ScaffoldColumn(false)]
        public int CategoryId { get; set; }

        public string Amanorya { get; set; }
        public string Cnndyan { get; set; }
        public string SurbZatik { get; set; }
        public string Amenorya { get; set; }
        public string TaqUteest { get; set; }
        public string AragSnund { get; set; }
        public string Azgayin { get; set; }
        public string Vracakan { get; set; }
        public string Xmorexen { get; set; }
        public string Tonakan { get; set; }

        private ICollection<ArticleP> articles;

        public virtual ICollection<ArticleP> GetArticles()
        {
            return articles;
        }

        public virtual void SetArticles(ICollection<ArticleP> value)
        {
            articles = value;
        }
    }
}
