using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using NET_MongoDB.Models;
using NET_MongoDB.Services;
using System.Linq.Expressions;

namespace NET_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        public ProductsController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models=await _productRepo.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Expression<Func<Product,bool>> predicate= x=>x.Id==id;
            var model =await _productRepo.GetById(predicate);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productRepo.CreateAsync(product);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            Expression<Func<Product, bool>> predicate = x => x.Id == product.Id;
            await _productRepo.UpdateAsync(predicate, product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Expression<Func<Product, bool>> predicate = x => x.Id == id;
            await _productRepo.DeleteAsync(predicate);
            return Ok();
        }

    }
}
