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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoriesController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models=await _categoryRepo.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Expression<Func<Category,bool>> predicate= x=>x.Id==id;
            var model =await _categoryRepo.GetById(predicate);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryRepo.CreateAsync(category);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            Expression<Func<Category, bool>> predicate = x => x.Id == category.Id;
            await _categoryRepo.UpdateAsync(predicate, category);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Expression<Func<Category, bool>> predicate = x => x.Id == id;
            await _categoryRepo.DeleteAsync(predicate);
            return Ok();
        }

    }
}
