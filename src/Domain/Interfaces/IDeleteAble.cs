using System;

namespace TechOnIt.Domain.Interfaces;

public interface IDeleteable
{
    public DateTime? DeletedOn { get; protected set; }
}