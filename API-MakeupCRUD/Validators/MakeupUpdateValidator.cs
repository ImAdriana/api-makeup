using FluentValidation;
using API_MakeupCRUD.DTOs;

namespace API_MakeupCRUD.Validators
{
    public class MakeupUpdateValidator: AbstractValidator<MakeupUpdateDto>
    {
        public MakeupUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is obligatory");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is obligatory");
            RuleFor(x => x.BrandID).NotNull().WithMessage(x => "Brand is obligatory");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "Error with the value sent");
            RuleFor(x => x.Volume).GreaterThan(0).WithMessage(x => "{PropertyName} must be greater than 0");
        }
    }
}
