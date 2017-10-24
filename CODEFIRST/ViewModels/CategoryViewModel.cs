using CODEFIRST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CODEFIRST.ViewModels
{
    public class CategoryViewModel

    {
        internal List<Category> category { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Product { get; set; }

    }
}