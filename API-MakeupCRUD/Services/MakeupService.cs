using API_MakeupCRUD.Context;
using API_MakeupCRUD.DTOs;
using API_MakeupCRUD.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_MakeupCRUD.Services
{
    public class MakeupService : IMakeupService
    {
        private AppDbContext _context;

        public MakeupService(AppDbContext context)
        {
            _context = context;
            
        }
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
        
           
        

        public async Task<MakeupDto> GetById(int id)
        {
            var makeup = await _context.MakeupProducts.FindAsync(id);
            if (makeup != null)
            {
                var makeupDto = new MakeupDto
                {
                    Id = makeup.Id,
                    Name = makeup.Name,
                    BrandID = makeup.BrandID,
                    Volume = makeup.Volume,
                    Price = makeup.Price,
                    Type = makeup.Type,
                };
                return makeupDto;
            }
            return null;


        }
        public async Task<MakeupDto> Add(MakeupInsertDto makeupInsertDto)
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
            return makeupDto;
        }

        public async Task<MakeupDto> Delete(int id)
        {
            var makeup = await _context.MakeupProducts.FindAsync(id);

            if (makeup != null)
            {
                var makeupDto = new MakeupDto
                {
                    Id = makeup.Id,
                    Name = makeup.Name,
                    BrandID = makeup.BrandID,
                    Volume = makeup.Volume,
                    Price = makeup.Price,
                    Type = makeup.Type,
                };
                _context.Remove(makeup);
                await _context.SaveChangesAsync();

                return makeupDto;
            }
            return null;
        }

        

        public async Task<MakeupDto> Update(int id, MakeupUpdateDto makeupUpdatedDto)
        {
            var makeup = await _context.MakeupProducts.FindAsync(id);

            if (makeup != null)
            {
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
                    Type = makeup.Type,
                };
                return makeupDto;
            }
            return null;

            
        }
    }
}
