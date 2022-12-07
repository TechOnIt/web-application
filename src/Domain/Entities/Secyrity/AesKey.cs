using System;

namespace TechOnIt.Domain.Entities.Secyrity;

public class AesKey
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Key { get; set; }
}