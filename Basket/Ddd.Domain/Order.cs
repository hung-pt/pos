using System.Text.Json.Serialization;
using Ddd.Domain.Common;

namespace Ddd.Domain;

public class Order : Entity {
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }

    /// <summary> When the customer needs the products. </summary>
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }

    /// <summary>
    /// Shipped, On Hold, Resolved, Cancelled, In Process,...
    /// </summary>
    public string? Status { get; set; }
    public string? Comments { get; set; }
    public int? CustomerNumber { get; set; }
    [JsonIgnore] public virtual Customer? Customer { get; set; }
    [JsonIgnore] public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}
