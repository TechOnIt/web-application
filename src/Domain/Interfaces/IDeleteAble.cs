using System;

namespace iot.Domain.Interfaces;

public interface IDeleteable
{
    public bool IsDeleted { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }

}