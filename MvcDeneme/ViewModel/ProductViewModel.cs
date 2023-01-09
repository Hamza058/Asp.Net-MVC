using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="İsim Alanı Boş Geçilemez")]
        public string UrunAdı { get; set; }
        public string Renk { get; set; }
        public int Fiyat { get; set; }
        public bool isActive { get; set; }
        public int Expire { get; set; }
        public DateTime? PublishDate { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }
        [ValidateNever]
        public string? ImagePath { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
