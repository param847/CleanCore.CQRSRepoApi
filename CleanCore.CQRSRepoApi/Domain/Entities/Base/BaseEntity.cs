namespace Domain.Entities.Base
{
    /// <summary>
    /// Base for all entities, providing a primary key.
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        // Optionally: override equality by Id
        public override bool Equals(object obj) =>
            obj is BaseEntity other && Id == other.Id;

        public override int GetHashCode() => Id.GetHashCode();
    }
}