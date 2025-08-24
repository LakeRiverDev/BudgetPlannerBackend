using BP.Api.Requests;
using BP.Infrastructure.Interfaces;
using FluentValidation;

namespace BP.Api.Validation
{
    public class RegistrationValidation : AbstractValidator<RegistrationDto>
    {
        private readonly IUserRepository userRepository;

        public RegistrationValidation(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(user => user.Email)
                .EmailAddress().WithMessage("Введенные данные не подходят под почту")
                .Length(4, 32).WithMessage("Почта должна содержать от 4 до 32 символов")
                .NotEmpty().WithMessage("Не должно быть пустым")
                .Must(UniqueEmail).WithMessage("Эта почта уже занята");

            RuleFor(user => user.Password)
                .Length(4, 32).WithMessage("Пароль должен содержать от 4 до 32 символов")
                .NotEmpty().WithMessage("Не должно быть пустым")
                .Matches("[0-9]").WithMessage("Пароль должен содержать цифры");

            RuleFor(user => user.Login)
                .Length(4, 32).WithMessage("Логин должен содержать от 4 до 32 символов")
                .NotEmpty().WithMessage("Не должно быть пустым")
                .Must(UniqueLogin).WithMessage("Этот логин уже занят");

            RuleFor(user => user.Name)
                .Length(4, 32).WithMessage("Имя должно содержать от 4 до 32 символов")
                .NotEmpty().WithMessage("Не должно быть пустым");
        }

        private bool UniqueLogin(string login)
        {
            return userRepository.UniqueLogin(login);
        }

        private bool UniqueEmail(string email)
        {
            return userRepository.UniqueEmail(email);
        }
    }
}
