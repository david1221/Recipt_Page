using ReceptsPage.ModelIdentity;
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

        [Required(ErrorMessage = "Լրացրեք դաշտը") , MaxLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Լրացրեք դաշտը")] 
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }
        public byte[] ImgGeneral { get; set; }

       public bool IfFavorite { get; set; }
        public int? SubCategoryId { set; get; }
        public virtual SubCategory SubCategory { get; set; }
        public AppUser AppUser { get; set; }
        public bool AdminConfirm { get; set; }
      
    }
    
   

}
