using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TombolaTest.Data;
using TombolaTest.Models;

namespace TombolaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeBeanController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<CoffeeBean>>> GetAllBeans()
        {
            var beans = await _context.CoffeeBeans.ToListAsync();

            if (beans is null)
                return NotFound("No coffee beans found.");

            return Ok(beans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CoffeeBean>>> GetBean(string id)
        {
            var bean = await _context.CoffeeBeans.FindAsync(id);

            if (bean is null)
                return NotFound("Coffee bean not found.");

            return Ok(bean);
        }

        [HttpPost]
        public async Task<ActionResult<List<CoffeeBean>>> AddBean(CoffeeBean bean)
        {
            _context.CoffeeBeans.Add(bean);
            await _context.SaveChangesAsync();

            return Ok(await _context.CoffeeBeans.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<CoffeeBean>>> UpdateBean(CoffeeBean updatedBean)
        {
            var dbBean = await _context.CoffeeBeans.FindAsync(updatedBean.Id);
            if (dbBean is null)
                return NotFound("Coffee bean not found.");

            dbBean.Id = updatedBean.Id;
            dbBean.Index = updatedBean.Index;
            dbBean.IsBOTD = updatedBean.IsBOTD;
            dbBean.Cost = updatedBean.Cost;
            dbBean.Image = updatedBean.Image;
            dbBean.Colour = updatedBean.Colour;
            dbBean.Name = updatedBean.Name;
            dbBean.Description = updatedBean.Description;
            dbBean.Country = updatedBean.Country;

            await _context.SaveChangesAsync();

            return Ok(await _context.CoffeeBeans.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<CoffeeBean>>> DeleteBean(string id)
        {
            var dbBean = await _context.CoffeeBeans.FindAsync(id);
            if (dbBean is null)
                return NotFound("Coffee bean not found.");

            _context.CoffeeBeans.Remove(dbBean);
            await _context.SaveChangesAsync();

            return Ok(await _context.CoffeeBeans.ToListAsync());
        }

        [HttpGet("GetSearchedBean")]
        public async Task<ActionResult<List<CoffeeBean>>> SearchBean(string? id, int? index, string? cost, string? image, string? colour, string? name, string? description, string? country)
        {
            var dbBeans = await _context.CoffeeBeans.Where(b => 
               (string.IsNullOrEmpty(id) || b.Id == id)
            && (string.IsNullOrEmpty(cost) || b.Cost == cost)
            && (!index.HasValue || b.Index == index)
            && (string.IsNullOrEmpty(image) || b.Image == image)
            && (string.IsNullOrEmpty(colour) || b.Colour == colour)
            && (string.IsNullOrEmpty(name) || b.Name == name)
            && (string.IsNullOrEmpty(description) || b.Description == description)
            && (string.IsNullOrEmpty(country) || b.Country == country)).ToListAsync();

            return Ok(dbBeans);
        }

        [HttpPut("GetBeanOfTheDay")]
        public async Task<ActionResult<List<CoffeeBean>>> GetBeanOfTheDay()
        {
            var bean = await _context.CoffeeBeans.Where(b => b.IsBOTD == true).ToListAsync();

            if (bean is null)
                return NotFound("Coffee bean not found.");

            return Ok(bean);
        }
    }
}
