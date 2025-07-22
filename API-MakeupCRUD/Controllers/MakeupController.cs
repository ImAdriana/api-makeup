using API_MakeupCRUD.Context;
using API_MakeupCRUD.DTOs;
using API_MakeupCRUD.Models;
using API_MakeupCRUD.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_MakeupCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeupController : ControllerBase
    {
        private IMakeupService _makeupService;
        private IValidator<MakeupInsertDto> _makeupInsertValidator;
        private IValidator<MakeupUpdateDto> _makeupUpdateValidator;

        public MakeupController(IValidator<MakeupInsertDto> makeupInsertValidator, IValidator<MakeupUpdateDto> makeupUpdateValidator, IMakeupService makeupService)
        {
            _makeupInsertValidator = makeupInsertValidator;
            _makeupUpdateValidator = makeupUpdateValidator;
            _makeupService = makeupService;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeupDto>> Get() =>
            await _makeupService.Get();


        [HttpGet("{id}")]
        public async Task<ActionResult<MakeupDto>> GetById(int id)
        {
            var makeupDto = await _makeupService.GetById(id);
            return makeupDto == null ? NotFound() : Ok(makeupDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MakeupDto>> Update(int id, MakeupUpdateDto makeupUpdatedDto)
        {
            var validationResult = await _makeupUpdateValidator.ValidateAsync(makeupUpdatedDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var makeupDto = await _makeupService.Update(id, makeupUpdatedDto);
            return makeupDto == null ? NotFound() : Ok(makeupDto);
        }

        [HttpPost]
        public async Task<ActionResult<MakeupDto>> Add(MakeupInsertDto makeupInsertDto)
        {
            var validationResult = await _makeupInsertValidator.ValidateAsync(makeupInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var makeupDto = await _makeupService.Add(makeupInsertDto);

            return CreatedAtAction(nameof(GetById), new {id = makeupDto.Id}, makeupDto);
        }

        

        [HttpDelete("{id}")]
        public async Task<ActionResult<MakeupDto>> Delete(int id)
        {
            var makeupDto = await _makeupService.Delete(id);
            return makeupDto == null ? NotFound() : Ok(makeupDto);
        }
         

    }
}
