using FluentValidation.Results;

namespace Ddd.Domain.Common;

public abstract class Entity {
    //public int Id { get; protected set; }
    //public string CreatedBy { get; set; }
    //public DateTime CreatedOn { get; set; }
    //public string ModifiedBy { get; set; }
    //public DateTime ModifiedOn { get; set; }

    public abstract ValidationResult Validate();
}
