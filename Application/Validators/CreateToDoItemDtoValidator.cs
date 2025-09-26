using Application.Common;
using Application.DTOs.ToDoItemDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateToDoItemDtoValidator:AbstractValidator<CreateToDoItemDto>
    {
        public CreateToDoItemDtoValidator() {
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

        }
    }
}
