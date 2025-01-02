using FluentValidation;

namespace GamerProfileService.Models.Gamer
{
    public class CreateGamerModelValidator : AbstractValidator<CreateGamerModel>
    {
        public CreateGamerModelValidator()
        {
            RuleFor( x => x.Name ).NotEmpty().WithMessage( "Please specify a first name." );
            RuleFor( x => x.Name ).Length( 2, 50 ).WithMessage( "First name should contain from 2 to 50 characters." );

            RuleFor( x => x.Nickname ).NotEmpty().WithMessage( "Please specify a nickname." );
            RuleFor( x => x.Nickname ).Length( 3, 50 ).WithMessage( "Nickname should contain from 3 to 50 characters." );
        }
    }
}
