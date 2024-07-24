using FluentValidation;
using OnlinePosting.Application.Implementation.Commands.CreatePost;

namespace OnlinePosting.Application.Shared.Validation
{
    public class ValidationConfig{}

    public sealed class CreatePostEntityCommandValidator : AbstractValidator<CreatePostEntityCommand>
    {
        public CreatePostEntityCommandValidator()
        {
            RuleFor(v => v.PostRequestDto.Title)
                .NotNull()
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50)
                .WithErrorCode("ONP001")
                .WithMessage("Title must not exceed 50 characters");

            RuleFor(v => v.PostRequestDto.Description)
                .NotNull()
                .NotEmpty().WithMessage("Description is required");
        }
    }
}
