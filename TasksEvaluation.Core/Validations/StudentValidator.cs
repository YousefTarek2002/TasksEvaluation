using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;

namespace TasksEvaluation.Core.Validations
{

    public class StudentValidator : AbstractValidator<StudentDto>
    {

        public StudentValidator() 
        {
            RuleFor(s => s.FullName)
                .NotEmpty()
                .WithMessage("FullName is required !!!");
            RuleFor(s => s.MobileNumber)
                .NotEmpty()
                .WithMessage("Phone Number is required !!!")
                .MatchPhoneNumberRule()
                .WithMessage("Please provide valid phone number");
            RuleFor(s => s.Email)
                .NotEmpty()
                .WithMessage("Email is required !!!")
                .EmailAddress()
                .WithMessage("Please Enter Valid Email Address !!!");
            RuleFor(s => s.ProfilePic ?? "No picture")
                .EndsWithCustomExtensions();
                

          

            
        }
    }
}
