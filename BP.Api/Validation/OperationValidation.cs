using BP.Api.Requests;
using FluentValidation;

namespace BP.Api.Validation
{
    public class OperationValidation : AbstractValidator<OperationDto>
    {
        public OperationValidation()
        {
            RuleFor(operation => operation.Reason)
                .Length(4, 256).WithMessage("Описание должно быть от 4 до 256 символов")
                .NotEmpty().WithMessage("Не должно быть пустым");

            RuleFor(operation => operation.Sum)
                .GreaterThan(0).WithMessage("Не может быть 0, ты что-то купил или продал за 0 рублей?")
                .LessThan(10000).WithMessage("У тебя денег то столько нету, куда ты там купил");
        }
    }
}
