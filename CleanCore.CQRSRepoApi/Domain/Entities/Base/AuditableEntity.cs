namespace Domain.Entities.Base
{
    /// <summary>
    /// Extends BaseEntity to include audit fields.
    /// </summary>
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public string CreatedBy { get; protected set; } = "system";

        public DateTime? ModifiedAt { get; protected set; }
        public string ModifiedBy { get; protected set; }

        public void SetModified(string user)
        {
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = user;
        }
    }
}