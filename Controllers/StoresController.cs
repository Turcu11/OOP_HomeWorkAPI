using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using WebAPIexercitii.Modells;

namespace WebAPIexercitii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        public static readonly List<Store> _stores =
        [
            new() {
                Id = Guid.NewGuid(),
                Name = "name",
                Country = "RO",
                City = "Oradea",
                MonthlyIncome = 15000,
                OwnerName = "John Doe",
                ActiveSince = DateTime.Now,
            },new() {
                Id = Guid.NewGuid(),
                Name = "SC SRL",
                Country = "RO",
                City = "Oradea",
                MonthlyIncome = 650,
                OwnerName = "Batman",
                ActiveSince = DateTime.Now,
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "second store",
                Country = "UK",
                City = "London",
                MonthlyIncome = 18900,
                OwnerName = "UK king",
                ActiveSince = DateTime.Now,
            }
        ];

        [HttpGet]
        public Store[] GetAllStores()
        {
            return [.. _stores];
        }

        [HttpGet("oldest")]
        public Store GetOldestStore()
        {
            Store TheOldestSoFar = _stores[0];
            foreach (var store in _stores)
            {
                int result = DateTime.Compare(store.ActiveSince, TheOldestSoFar.ActiveSince);
                if (result > 0)
                {
                    TheOldestSoFar = store;
                    return TheOldestSoFar;
                }
            }
            return TheOldestSoFar;
        }

        [HttpGet("sort-monthly-income")]
        public List<Store> SortByMonthlyIncome()
        {
            //this is higher income first
            List<Store> sortedStores = [.. _stores.OrderBy(x => x.MonthlyIncome).Reverse()];
            return sortedStores;
        }

        [HttpGet("get-by-country/{country}")]
        public List<Store> GetByCountry(string country)
        {
            List<Store> foundByCountry = [];
            foreach (var store in _stores)
            {
                if (store.Country.Contains(country))
                {
                    foundByCountry.Add(store);
                }
            }
            return foundByCountry;
        }

        [HttpGet("get-by-city/{city}")]
        public List<Store> GetByCity(string city)
        {
            List<Store> foundByCity = [];
            foreach (var store in _stores)
            {
                if (store.City.Equals(city))
                {
                    foundByCity.Add(store);
                }
            }
            return foundByCity;
        }

        [HttpGet("get-by-keyword/{keyword}")]
        public List<Store> GetByKeyword(string keyword)
        {
            List<Store> foundByKeyword = [];
            foreach (var store in _stores)
            {
                if (store.Name.Contains(keyword))
                {
                    foundByKeyword.Add(store);
                }
            }
            return foundByKeyword;
        }


        [HttpDelete("id")]
        public IActionResult DeleteStore(Guid id)
        {
            foreach (var existingStores in _stores)
            {
                if (existingStores.Id == id)
                {
                    _stores.Remove(existingStores);
                    return Ok();
                }
            }
            return NotFound("Store not found");
        }

        [HttpPut("transfer-ownership/{storeId}")]
        public IActionResult TransferStore(Guid storeId, [FromBody] string name)
        {
            foreach (var existingStores in _stores)
            {
                if (existingStores.Id == storeId)
                {
                    existingStores.OwnerName = name;
                    return Ok();
                }
            }
            return NotFound("Store not found");
        }

        [HttpPut("create-store")]
        public IActionResult CreateStore(
            [FromBody] string Name,
            string Country,
            string City,
            int MonthlyIncome,
            string OwnerName,
            DateTime ActiveSince
            )
        {
            Store NewStore = new();
            NewStore.Id = Guid.NewGuid();
            NewStore.Name = Name;
            NewStore.Country = Country;
            NewStore.City = City;
            NewStore.MonthlyIncome = MonthlyIncome;
            NewStore.OwnerName = OwnerName;
            NewStore.ActiveSince = ActiveSince;
            _stores.Add(NewStore);
            return Ok();
        }
    }
}