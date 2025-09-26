using FluentValidation;
using Domain.Enums;
using System.Data;
using Application.Common;
using Application.DTOs.UserDtos;

namespace Application.Validators
{
    public class UpdateUserDtoValidator:AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(user=>user.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(ConstantsClass.usernameRequiredMessage)
                .MaximumLength(ConstantsClass.maxLengthCharsGeneral).WithMessage(ConstantsClass.usernameMaxLengthMessage)
                .Matches(ConstantsClass.alphanumRule).WithMessage(ConstantsClass.alphanumMessage);

            RuleFor(user => user.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage(ConstantsClass.negativeIdMessage);

            RuleFor(user => user.Role)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .WithMessage(ConstantsClass.roleMessage);

            RuleFor(user => user.UserEmail)
                .Cascade(CascadeMode.Stop)
                .EmailAddress().WithMessage(ConstantsClass.emailMessage);

            RuleFor(user => user.UserPassword)
                .Cascade(CascadeMode.Stop)
                .Matches(ConstantsClass.alphanumRule).WithMessage(ConstantsClass.alphanumMessage);

        }
    }
}
