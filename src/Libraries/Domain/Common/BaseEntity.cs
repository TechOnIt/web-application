namespace TechOnIt.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}