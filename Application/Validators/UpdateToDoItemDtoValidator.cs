using FluentValidation;
using Domain.Enums;
using Application.Common;
using Application.DTOs.ToDoItemDtos;

namespace Application.Validators
{
    public class UpdateToDoItemDtoValidator:AbstractValidator<UpdateToDoItemDto>
    {
        public UpdateToDoItemDtoValidator()
        {
            RuleFor(item => item.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(ConstantsClass.titleRequiredMessage)
                .MaximumLength(100).WithMessage(ConstantsClass.titleMaxLengthMessage)
                .Matches(ConstantsClass.alphanumRule).WithMessage(ConstantsClass.alphanumMessage);

            RuleFor(item => item.Description)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(100).WithMessage(ConstantsClass.descriptionMaxLengthMessage)
                .Matches(ConstantsClass.alphanumRule).WithMessage(ConstantsClass.alphanumMessage);

            RuleFor(item => item.PriorityLevel)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .WithMessage(ConstantsClass.priorityLevelMessage);

            RuleFor(item => item.DueDate)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(DateTime.Now.AddDays(1))
                .WithMessage(ConstantsClass.dueDateInPastMessage);

            RuleFor(item => item.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage(ConstantsClass.negativeIdMessage);
                
        }
    }
}
