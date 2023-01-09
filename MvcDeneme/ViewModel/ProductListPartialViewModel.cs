using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.ViewModel
{
    public class ProductListPartialViewModel
    {
        public List<ProductParitalViewModel> Products { get; set; }
    }
    public class ProductParitalViewModel
    {
        public int Id { get; set; }
        public string UrunAdı { get; set; }
        public int Fiyat { get; set; }
    }
}
