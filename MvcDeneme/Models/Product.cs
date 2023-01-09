using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string UrunAdı { get; set; }
        public string Renk { get; set; }
        public int Fiyat { get; set; }
        public bool isActive { get; set; }
        public int Expire { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? ImagePath { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
