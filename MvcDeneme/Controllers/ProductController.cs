using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MvcDeneme.Filters;
using MvcDeneme.Models;
using MvcDeneme.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Controllers
{
    [LogFilter]
    public class ProductController : Controller
    {
        private Context _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        public ProductController(Context context, IMapper mapper, IFileProvider fileProvider)
        {
            _dbContext = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }
        [CustomResultFilter("sa", "as")]
        public IActionResult Index()
        {
            List<ProductViewModel> products = _dbContext.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                UrunAdı = x.UrunAdı,
                Fiyat = x.Fiyat,
                CategoryName = x.Category.Name,
                Renk = x.Renk,
                Expire = x.Expire,
                isActive = x.isActive,
                PublishDate = x.PublishDate,
                ImagePath = x.ImagePath
            }).ToList();

            return View(products);
        }
        //[Route("[controller]/[action]/{delete}")] 
        /*
         * yaparak startup.cs içinde yaptıklarımıza gerek kalmaz
         * Controller yazmamıza gerek yok. Action yerinde istedğimizi yazabiliriz
         * [Route("sil/siliyoruz/{delete}")] veya [Route("siliyoruz/{delete}")] yaparsak linke tıkladığımızda
         *         localhost/sil/siliyoruz/1 ----     localhost/Product/siliyoruz/1  böyle yazar. 
         * [Route("sil/siliyoruz/{delete}"), Name="asd"] yaprak View de asp-rout="asd" ile methodu çağırabilriz.
         * View kısmında controller ismi ile method ismi yazılır. Route içinde tanımladığımız değerler yazılmaz.
         * Genelde tercih edilen yöntem.
        */
        //[HttpGet("{delete}"] tanımlaması ile startup da yaptığımızı da yapabiliriz.
        [Route("[controller]/[action]/{delete}")]
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Delete(int delete)
        {
            var value = _dbContext.Products.Find(delete);
            _dbContext.Products.Remove(value);
            _dbContext.SaveChanges();
            TempData["status"] = "Ürün Silinmiştir";//TempData ile başka view lere veri taşıyabiliriz.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            //ViewBag.Expire = new List<int>() { 1, 2, 3 };
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorList>(){
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Siyah",Value="Siyah"},
                new(){Data="Sarı",Value="Sarı"},
            }, "Value", "Data");

            var categories = _dbContext.Category.ToList();
            ViewBag.category = new SelectList(categories, "Id", "Name");

            return View();
        }
        //Yöntem 1. En çok kullanılan
        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.Image != null && product.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(product.Image.FileName);

                        var path = Path.Combine(images.PhysicalPath, randomImageName);
                        using var stream = new FileStream(path, FileMode.Create);

                        product.Image.CopyTo(stream);
                        product.ImagePath = randomImageName;
                    }
                    
                    _dbContext.Products.Add(_mapper.Map<Product>(product));
                    _dbContext.SaveChanges();
                    TempData["status"] = "Ürün Eklenmiştir";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

                    ViewBag.ColorSelect = new SelectList(new List<ColorList>(){
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Siyah",Value="Siyah"},
                new(){Data="Sarı",Value="Sarı"},
            }, "Value", "Data");
                    result = View();
                }
            }
            else
            {
                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

                ViewBag.ColorSelect = new SelectList(new List<ColorList>(){
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Siyah",Value="Siyah"},
                new(){Data="Sarı",Value="Sarı"},
            }, "Value", "Data");
                result = View();
            }
            return result;
        }
        //Yöntem 2
        [HttpPost]
        public IActionResult AddProduct()//AddProduct(string UrunAdı,..) tarzı yaparak da verileri alabiliriz.
        {
            var name = HttpContext.Request.Form["UrunAdı"];
            var color = HttpContext.Request.Form["Renk"];
            var price = int.Parse(HttpContext.Request.Form["Fiyat"]);

            /*
             var name=UrunAdı;
             var color=Renk;
             var price=Fiyat;
            */

            Product newProduct = new Product() { UrunAdı = name, Renk = color, Fiyat = price };

            /*
            newProduct.UrunAdı = name;
            newProduct.Renk = color;
            newProduct.Fiyat = price;
            */

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Update(int id)
        {
            var value = _dbContext.Products.Find(id);

            var categories = _dbContext.Category.ToList();
            ViewBag.category = new SelectList(categories, "Id", "Name",value.CategoryId);

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorList>(){
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Siyah",Value="Siyah"},
                new(){Data="Sarı",Value="Sarı"},
            }, "Value", "Data", value.Renk);

            return View(_mapper.Map<ProductViewModel>(value));
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel product)
        {
            if (product.Image != null && product.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "images");

                var randomImageName = Guid.NewGuid() + Path.GetExtension(product.Image.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);
                using var stream = new FileStream(path, FileMode.Create);

                product.Image.CopyTo(stream);
                product.ImagePath = randomImageName;
            }
            
            var categories = _dbContext.Category.ToList();
            ViewBag.category = new SelectList(categories, "Id", "Name", product.CategoryId);

            _dbContext.Products.Update(_mapper.Map<Product>(product));
            _dbContext.SaveChanges();
            TempData["status"] = "Ürün Güncellenmiştir";
            return RedirectToAction("Index");
        }
    }
}
