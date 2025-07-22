using API_MakeupCRUD.Context;
using API_MakeupCRUD.DTOs;
using API_MakeupCRUD.Models;
using API_MakeupCRUD.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_MakeupCRUD.Services
{
    public class MakeupService : IMakeupService
    {
        
        private IRepository<MakeupProduct> _makeupRepository;
        private IMapper _mapper;
        public MakeupService(IRepository<MakeupProduct> makeupProduct, IMapper mapper)
        { 
            _makeupRepository = makeupProduct; 
            _mapper = mapper;
        }
        public async Task<IEnumerable<MakeupDto>> Get() {
            var makeup = await _makeupRepository.Get();
            return makeup.Select(m => new MakeupDto()
            {
                Id = m.Id,
                Name = m.Name,
                BrandID = m.BrandID,
                Volume = m.Volume,
                Price = m.Price,
                Type = m.Type,
            });

        }

        public async Task<MakeupDto> GetById(int id)
        {
            var makeup = await _makeupRepository.GetById(id);
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

            await _makeupRepository.Add(makeup);
            await _makeupRepository.Save(); // La base de datos representa los cambios

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
            var makeup = await _makeupRepository.GetById(id);

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
                _makeupRepository.Delete(makeup);
                await _makeupRepository.Save();

                return makeupDto;
            }
            return null;
        }

        

        public async Task<MakeupDto> Update(int id, MakeupUpdateDto makeupUpdatedDto)
        {
            var makeup = await _makeupRepository.GetById(id);

            if (makeup != null)
            {
                makeup.Name = makeupUpdatedDto.Name;
                makeup.Volume = makeupUpdatedDto.Volume;
                makeup.Price = makeupUpdatedDto.Price;
                makeup.Type = makeupUpdatedDto.Type;
                makeup.Id = makeupUpdatedDto.Id;

                _makeupRepository.Update(makeup);
                await _makeupRepository.Save();

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
