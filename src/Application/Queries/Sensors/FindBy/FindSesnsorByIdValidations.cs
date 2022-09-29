﻿namespace iot.Application.Queries.Sensors.FindBy;

public class FindSesnsorByIdValidations : BaseFluentValidator<FindSesnsorByIdQuery>
{
	public FindSesnsorByIdValidations()
	{
		RuleFor(a => a.Id)
			.NotNull()
			.NotEmpty()
			.NotEqual(Guid.Empty)
			;
	}
}