﻿using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Common.Models.ViewModels.Structures;

public class StructureCardViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public StructureType Type { get; set; }
    public string ApiKey { get; set; }
    public bool IsActive { get; set; }
}