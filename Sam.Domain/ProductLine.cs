using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sam.Domain;

public class ProductLine : Entity {
    [Key] public string? ProductLineCode { get; set; }
    public string? TextDescription { get; set; }
    public string? HtmlDescription { get; set; }
    public byte[]? Image { get; set; }

    [JsonIgnore] public virtual List<Product>? Products { get; set; }

    public ProductLine() { }
    public ProductLine(string? productLineCode, string? textDescription, string? htmlDescription, byte[]? image) {
        ProductLineCode = productLineCode;
        TextDescription = textDescription;
        HtmlDescription = htmlDescription;
        Image = image;
    }
}
