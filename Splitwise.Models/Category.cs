using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Subcategory> Subcategories { get; set; }
    }

    public class CategoriesWrapper
    {
        public List<Category> Categories { get; set; }
    }
}
