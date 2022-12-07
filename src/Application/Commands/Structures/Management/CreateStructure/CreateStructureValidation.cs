using TechOnIt.Application.Commands.Users.Management.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Structures.Management.CreateStructure;

public class CreateStructureValidation : BaseFluentValidator<CreateStructureCommand>
{
    public CreateStructureValidation()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100)
            ;

        RuleFor(a => a.Type)
            .NotEmpty()
            .NotNull()
            ;
    }
}
