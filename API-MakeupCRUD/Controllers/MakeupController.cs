using API_MakeupCRUD.Context;
using API_MakeupCRUD.DTOs;
using API_MakeupCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_MakeupCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeupController : ControllerBase
    {
        private AppDbContext _context;

        public MakeupController(AppDbContext context)
        {
            _context = context;
        }


        // 
        [HttpGet]
        public async Task<IEnumerable<MakeupDto>> Get() =>
            await _context.MakeupProducts.Select(m => new MakeupDto
            {
                Id = m.Id,
                Name = m.Name,
                BrandID = m.BrandID,
                Volume = m.Volume,
                Price = m.Price,
                Type = m.Type,
            }).ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<MakeupDto>> GetById(int id)
        {
            var makeup = await _context.MakeupProducts.FindAsync(id);
            if (makeup == null)
            {
                return NotFound();
            }

            var makeupDto = new MakeupDto
            {
                Id = makeup.Id,
                Name = makeup.Name,
                BrandID = makeup.BrandID,
                Volume = makeup.Volume,
                Price = makeup.Price,
                Type = makeup.Type,
            };
            return Ok(makeupDto);

        }

        [HttpPost]
        public async Task<ActionResult<MakeupDto>> Add(MakeupInsertDto makeupInsertDto)
        {
            var makeup = new MakeupProduct()
            {
                Name = makeupInsertDto.Name,
                BrandID = makeupInsertDto.BrandID,
                Volume = makeupInsertDto.Volume,
                Price = makeupInsertDto.Price,
                Type = makeupInsertDto.Type,
            };

            await _context.MakeupProducts.AddAsync(makeup);
            await _context.SaveChangesAsync(); // La base de datos representa los cambios

            var makeupDto = new MakeupDto
            {
                Id = makeup.Id,
                Name = makeup.Name,
                BrandID = makeup.BrandID,
                Volume = makeup.Volume,
                Price = makeup.Price,
                Type = makeup.Type,
            };

            return CreatedAtAction(nameof(GetById), new {id = makeup.Id}, makeupDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MakeupDto>> Update(int id, MakeupUpdateDto makeupUpdatedDto)
        {
            var makeup = await _context.MakeupProducts.FindAsync(id);

            if (makeup == null)
            {
                return NotFound();
            }

            makeup.Name = makeupUpdatedDto.Name;
            makeup.Volume = makeupUpdatedDto.Volume;
            makeup.Price = makeupUpdatedDto.Price;
            makeup.Type = makeupUpdatedDto.Type;
            makeup.Id = makeupUpdatedDto.Id;

            await _context.SaveChangesAsync();

            var makeupDto = new MakeupDto
            {
                Id = makeup.Id,
                Name = makeup.Name,
                BrandID = makeup.BrandID,
                Volume = makeup.Volume,
                Price = makeup.Price,
                Type= makeup.Type,
            };

            return Ok(makeupDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var makeup = await _context.MakeupProducts.FindAsync(id);
            if(makeup == null)
            {
                return NotFound();
            }
            _context.MakeupProducts.Remove(makeup);
            await _context.SaveChangesAsync();

            return Ok();
        }
         

    }
}
