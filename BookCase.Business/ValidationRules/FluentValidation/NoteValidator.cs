using BookCase.Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Business.ValidationRules.FluentValidation
{
    public class NoteValidator:AbstractValidator<NoteDto>
    {
        public NoteValidator()
        {
            RuleFor(u => u.BookId).NotEmpty();
            RuleFor(u => u.Notes).NotEmpty();
            RuleFor(u => u.NoteType).NotEmpty();
        }
    }
}
