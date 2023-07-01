using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country country = new()
            {
                Name = request.Name
            };

            await _context.Countries.AddAsync(country);

            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CountryUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country dbCountry = await _context.Countries.FindAsync(id);

            if (dbCountry == null) return NotFound();

            dbCountry.Name = request.Name;

            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            if (searchText == null) return BadRequest();

            List<Country> searchCountries = new();

            List<Country> dbCountries = await _context.Countries.ToListAsync();

            foreach (Country country in dbCountries)
            {
                if (country.Name.ToLower().Contains(searchText.ToLower()))
                {
                    searchCountries.Add(country);
                }
            }

            return Ok(searchCountries);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Country dbCountry = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);

            if (dbCountry == null) return NotFound();

            return Ok(dbCountry);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Country> dbCountries = await _context.Countries.ToListAsync();
            return Ok(dbCountries);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Country country = await _context.Countries.FindAsync(id);

            if (country == null) return NotFound();

            _context.Countries.Remove(country);

            await _context.SaveChangesAsync();  

            return Ok();
        }

    }
}
