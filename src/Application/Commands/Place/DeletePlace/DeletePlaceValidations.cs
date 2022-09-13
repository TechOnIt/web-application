namespace iot.Application.Commands.Place.DeletePlace;

public class DeletePlaceValidations : BaseFluentValidator<DeletePlaceCommand>
{
	public DeletePlaceValidations()
	{
		RuleFor(a => a.Id)
			.NotNull()
			.NotEmpty()
			.NotEqual(Guid.Empty)
			;
	}
}
