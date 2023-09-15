using System;

namespace TechOnIt.Domain.Interfaces;

public interface ICreateAble
{
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; protected set; }
}