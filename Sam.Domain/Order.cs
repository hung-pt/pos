using System.Text.Json.Serialization;

namespace Sam.Domain;

public class Order : Entity {
    public int OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public string? Status { get; set; }
    public string? Comments { get; set; }
    public int? CustomerNumber { get; set; }
    [JsonIgnore] public virtual Customer? Customer { get; set; }
    [JsonIgnore] public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

    public Order() { }
    public Order(int orderNumber, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, string? status, string? comments, int customerNumber) {
        OrderNumber = orderNumber;
        OrderDate = orderDate;
        RequiredDate = requiredDate;
        ShippedDate = shippedDate;
        Status = status;
        Comments = comments;
        CustomerNumber = customerNumber;
    }
}
