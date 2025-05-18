using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBean.Data;
using CoffeeBean.Models;

namespace CoffeeBean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeBeanController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<CoffeeBeanDto>>> GetAllBeans()
        {
            var beans = await _context.CoffeeBeans.ToListAsync();

            if (beans is null)
                return NotFound("No coffee beans found.");

            return Ok(beans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CoffeeBeanDto>>> GetBean(string id)
        {
            var bean = await _context.CoffeeBeans.FindAsync(id);

            if (bean is null)
                return NotFound("Coffee bean not found.");

            return Ok(bean);
        }

        [HttpPost]
        public async Task<ActionResult<List<CoffeeBeanDto>>> AddBean(CoffeeBeanDto bean)
        {
            _context.CoffeeBeans.Add(bean);
            await _context.SaveChangesAsync();

            return Ok(await _context.CoffeeBeans.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<CoffeeBeanDto>>> UpdateBean(CoffeeBeanDto updatedBean)
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
        public async Task<ActionResult<List<CoffeeBeanDto>>> DeleteBean(string id)
        {
            var dbBean = await _context.CoffeeBeans.FindAsync(id);
            if (dbBean is null)
                return NotFound("Coffee bean not found.");

            _context.CoffeeBeans.Remove(dbBean);
            await _context.SaveChangesAsync();

            return Ok(await _context.CoffeeBeans.ToListAsync());
        }

        [HttpGet("GetSearchedBean")]
        public async Task<ActionResult<List<CoffeeBeanDto>>> SearchBean(
            string? id = null, int? index = null, string? cost = null, 
            string? colour = null, string? name = null, string? description = null, string? country = null)
        {
            var dbBeans = await _context.CoffeeBeans.Where(b => 
               (string.IsNullOrEmpty(id) || b.Id == id)
            && (string.IsNullOrEmpty(cost) || b.Cost == cost)
            && (!index.HasValue || b.Index == index)
            && (string.IsNullOrEmpty(colour) || b.Colour == colour)
            && (string.IsNullOrEmpty(name) || b.Name == name)
            && (string.IsNullOrEmpty(description) || b.Description.Contains(description))
            && (string.IsNullOrEmpty(country) || b.Country == country)).ToListAsync();

            return Ok(dbBeans);
        }

        [HttpPut("GetBeanOfTheDay")]
        public async Task<ActionResult<List<CoffeeBeanDto>>> GetBeanOfTheDay()
        {
            var bean = await _context.CoffeeBeans.Where(b => b.IsBOTD == true).ToListAsync();

            if (bean is null)
                return NotFound("Coffee bean not found.");

            return Ok(bean);
        }
    }
}
