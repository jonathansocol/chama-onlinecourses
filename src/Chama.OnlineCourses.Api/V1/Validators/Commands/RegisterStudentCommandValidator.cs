﻿using Chama.OnlineCourses.Api.V1.Commands;
using FluentValidation;

namespace Chama.OnlineCourses.Api.V1.Validators.Commands
{
    public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
    {
        public RegisterStudentCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Student.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Student.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Student.Age)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
