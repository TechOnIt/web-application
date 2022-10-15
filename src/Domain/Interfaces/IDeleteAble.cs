using System;

namespace iot.Domain.Interfaces;

public interface IDeleteable
{
    public DateTime? DeletedOn { get; protected set; }
}