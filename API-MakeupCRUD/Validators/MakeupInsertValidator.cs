using API_MakeupCRUD.DTOs;
using FluentValidation;

namespace API_MakeupCRUD.Validators
{
    public class MakeupInsertValidator: AbstractValidator<MakeupInsertDto>
    {
        public MakeupInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is obligatory");
            RuleFor(x => x.BrandID).NotNull().WithMessage( x => "Brand is obligatory");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "Error with the value sent");
            RuleFor(x => x.Volume).GreaterThan(0).WithMessage(x => "{PropertyName} must be greater than 0");
        }
    }
}
