namespace iot.Application.Commands.Structures.Management.DeleteStructure;

public class DeleteStructureValidations : BaseFluentValidator<DeleteStructureCommand>
{
	public DeleteStructureValidations()
	{
		RuleFor(a => a.StructureId)
			.NotNull()
			.NotEmpty()
			;
	}
}
