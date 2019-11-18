using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
     public class ArticleP
    {
        [Key ]
        public int ArticleId { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Լրացրեք դաշտը")]
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }
        public byte[] ImgGeneral { get; set; }

        public string Star { get; set; }
        public int? CategoryId { set; get; }
        public virtual category Category { get; set; }

    }
    
   

}
