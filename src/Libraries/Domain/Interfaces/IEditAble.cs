using System;

namespace TechOnIt.Domain.Interfaces;

internal interface IEditAble
{
#nullable enable
    public string? ModifyBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}