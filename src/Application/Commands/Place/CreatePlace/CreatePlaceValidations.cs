namespace iot.Application.Commands.Place.CreatePlace;

public class CreatePlaceValidations : BaseFluentValidator<CreatePlaceCommand>
{
	public CreatePlaceValidations()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(40)
			;

		RuleFor(a => a.StuctureId)
			.NotNull()
			.NotEmpty()
			.NotEqual(Guid.Empty)
			;

		RuleFor(x => x.Description)
			.NotEmpty()
			.MinimumLength(10)
			.MaximumLength(150)
			;
	}
}
