﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public List<SubCategory> SubCategory { get; set; }



    }


}
