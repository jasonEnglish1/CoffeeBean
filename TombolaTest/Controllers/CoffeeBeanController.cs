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
            var beans = await _context.CofeeBeans.ToListAsync();

            return Ok(beans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CoffeeBean>>> GetBean(int id)
        {
            var bean = await _context.CofeeBeans.FindAsync(id);

            if (bean is null)
                return NotFound("Coffee bean not found.");

            return Ok(bean);
        }

        [HttpPost]
        public async Task<ActionResult<List<CoffeeBean>>> AddBean(CoffeeBean bean)
        {
            _context.CofeeBeans.Add(bean);
            await _context.SaveChangesAsync();

            return Ok(await _context.CofeeBeans.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<CoffeeBean>>> UpdateBean(CoffeeBean updatedBean)
        {
            var dbBean = await _context.CofeeBeans.FindAsync(updatedBean.Id);
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

            return Ok(await _context.CofeeBeans.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<CoffeeBean>>> DeleteBean(int id)
        {
            var dbBean = await _context.CofeeBeans.FindAsync(id);
            if (dbBean is null)
                return NotFound("Coffee bean not found.");

            _context.CofeeBeans.Remove(dbBean);
            await _context.SaveChangesAsync();

            return Ok(await _context.CofeeBeans.ToListAsync());
        }
    }
}
