using Application.Common;
using Application.DTOs.Auth;
using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() {
            RuleFor(user => user.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(ConstantsClass.usernameRequiredMessage)
                .MaximumLength(ConstantsClass.maxLengthCharsGeneral).WithMessage(ConstantsClass.usernameMaxLengthMessage)
                .Matches(ConstantsClass.alphanumRule).WithMessage(ConstantsClass.alphanumMessage);

            RuleFor(user => user.Role)
                .Cascade(CascadeMode.Stop)
                .IsEnumName(typeof(Roles))
                .WithMessage(ConstantsClass.roleMessage);

            RuleFor(user => user.Email)
                .Cascade(CascadeMode.Stop)
                .EmailAddress().WithMessage(ConstantsClass.emailMessage);

            RuleFor(user => user.Password)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(ConstantsClass.minLengthCharsPassword).WithMessage(ConstantsClass.shortPasswordMessage)
                .MaximumLength(ConstantsClass.maxLengthCharsPassword).WithMessage(ConstantsClass.longPasswordMessage);
                
        }
    }
}
