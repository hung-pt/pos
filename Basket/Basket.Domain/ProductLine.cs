using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ddd.Domain.Common;

namespace Ddd.Domain;

public class ProductLine : Entity {
    [Key] public string ProductLineCode { get; set; }
    public string? ProductLineName { get; set; }
    public string? TextDescription { get; set; }
    public string? HtmlDescription { get; set; }
    public byte[]? Image { get; set; }

    [JsonIgnore] public virtual List<Product>? Products { get; set; }
}
