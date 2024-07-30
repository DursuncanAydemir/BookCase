using BookCase.Core.Entities.Concrete;
using BookCase.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Business.ValidationRules.FluentValidation;

public class LoginValidator:AbstractValidator<User>
{
    public LoginValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.LastName).NotEmpty();
        
    }
}
