using System;

namespace iot.Domain.Interfaces;

internal interface IEditAble
{
    public string? ModifyBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}