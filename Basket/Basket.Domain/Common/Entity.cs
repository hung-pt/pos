namespace Ddd.Domain.Common;

public abstract class Entity {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }

    protected Entity() {
        Id = Guid.NewGuid();
        ModifiedOn = CreatedOn = DateTime.UtcNow;
    }
}
