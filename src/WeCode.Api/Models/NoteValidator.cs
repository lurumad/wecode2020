using FluentValidation;

namespace WeCode.Api.Models
{
    public class NoteValidator : AbstractValidator<Note>
    {
        public NoteValidator()
        {
            RuleFor(note => note.Content)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(250);
        }
    }
}
