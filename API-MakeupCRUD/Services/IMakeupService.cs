using API_MakeupCRUD.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_MakeupCRUD.Services
{
    public interface IMakeupService
    {
        Task<IEnumerable<MakeupDto>> Get();
        Task<MakeupDto> GetById(int id);
        Task<MakeupDto> Update(int id, MakeupUpdateDto makeupUpdatedDto);
        Task<MakeupDto> Add(MakeupInsertDto makeupInsertDto);
        Task<MakeupDto> Delete(int id);
    }
}
