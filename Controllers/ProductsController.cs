using Microsoft.AspNetCore.Mvc;
using WebAPIexercitii.Modells;

namespace WebAPIexercitii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        public static readonly List<Product> _products =
        [
            new() {
                Id = Guid.NewGuid(),
                Name = "Laptop HP PROBOOK",
                Description = "i7 10th gen, 16GB RAM, 512GB SSD",
                Rating = [4, 5, 2, 5, 5],
                CreatedOn = DateTime.Now,
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Laptop Lenovo Thinkpad E14",
                Description = "i5 11th gen, 16GB RAM, 512GB SSD",
                Rating = [4, 5, 5, 4, 5],
                CreatedOn = DateTime.Now,
            }
        ];


        [HttpGet]
        public Product[] GetAllProducts()
        {
            return [.. _products];
        }

        [HttpGet("get-by-keyword/{keyword}")]
        public List<Product> GetProductsByKeyword(string keyword)
        {
            List<Product> FilteredProducts = [];
            foreach (var product in _products)
            {
                if (product.Name.Contains(keyword))
                {
                    FilteredProducts.Add(product);
                }
                else if (product.Description.Contains(keyword))
                {
                    FilteredProducts.Add(product);
                }
            }
            return FilteredProducts;
        }

        [HttpGet("get-oldest-product")]
        public Product GetOldestProduct()
        {
            Product OldestOne = _products[0];
            foreach (var product in _products)
            {
                int result = DateTime.Compare(product.CreatedOn, OldestOne.CreatedOn);
                if (result < 0)
                {
                    OldestOne = product;
                }
            }
            return OldestOne;
        }
        
        [HttpGet("get-newest-product")]
        public Product GetNewestProduct()
        {
            Product NewestOne = _products[0];
            foreach (var product in _products)
            {
                int result = DateTime.Compare(product.CreatedOn, NewestOne.CreatedOn);
                if (result > 0)
                {
                    NewestOne = product;
                }
            }
            return NewestOne;
        }

        [HttpGet("get-best-ratings")]
        public List<Product> GetBestRatings()
        {
            List<Product> BestProducts = _products;
            for(int i = 0 ; i < BestProducts.Count-1; i++)
            {
                for(int j = 0 ; j < BestProducts.Count-1; j++)
                {
                    if (BestProducts[j].GetAverageRating() < BestProducts[j+1].GetAverageRating())
                    {
                        var TempProduct = BestProducts[j];
                        BestProducts[j] = BestProducts[j+1];
                        BestProducts[j+1] = TempProduct;
                    }
                }
            }
            return BestProducts;
        }
        
        [HttpGet("get-worst-ratings")]
        public List<Product> GetWorstRatings()
        {
            List<Product> WorstProducts = _products;
            for(int i = 0 ; i < WorstProducts.Count-1; i++)
            {
                for(int j = 0 ; j < WorstProducts.Count-1; j++)
                {
                    if (WorstProducts[j].GetAverageRating() > WorstProducts[j+1].GetAverageRating())
                    {
                        var TempProduct = WorstProducts[j];
                        WorstProducts[j] = WorstProducts[j+1];
                        WorstProducts[j+1] = TempProduct;
                    }
                }
            }
            return WorstProducts;
        }


        [HttpPut("create-product")]
        public IActionResult CreateProduct(
            [FromBody] string name,
            string description
            )
        {
            Product NewProduct = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Rating = [],
                CreatedOn = DateTime.Now
            };
            _products.Add(NewProduct);
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteProduct ( Guid id )
        {
            foreach ( var product in _products )
            {
                if( product.Id == id )
                {
                    _products.Remove(product);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
