using Core.DTO;
using FluentValidation;

namespace Core.Validations
{
    public class CreateLocationDtoValidator : AbstractValidator<CreateLocationDto>
    {
        public CreateLocationDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100); 
            RuleFor(dto => dto.OpeningTime)
            .NotEmpty().WithMessage("Opening time is required.")
            .Must(BeValidTimeFormat).WithMessage("Invalid opening time format. Please use 'hh:mm'.");
            RuleFor(dto => dto.ClosingTime)
            .NotEmpty().WithMessage("Closing time is required.")
            .Must(BeValidTimeFormat).WithMessage("Invalid closing time format. Please use 'hh:mm'.");
        }

        private bool BeValidTimeFormat(string time)
        {
            return TimeSpan.TryParse(time, out _);
        }
    }
}
