using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ddd.Domain.Common;
using FluentValidation;

namespace Ddd.Domain;

public class ProductLine : Entity {
    [Key] public string ProductLineCode { get; }
    public string? ProductLineName { get; set; }
    public string? TextDescription { get; set; }
    public string? HtmlDescription { get; set; }
    public byte[]? Image { get; set; }

    [JsonIgnore] public virtual List<Product>? Products { get; set; }

    public ProductLine(string productLineCode) : base() {
        ProductLineCode = productLineCode;
    }

    public override FluentValidation.Results.ValidationResult Validate() {
        return Vtors.ProductLineValidator.Validate(this);
    }
}



internal class ProductLineValidator : AbstractValidator<ProductLine> {
    internal ProductLineValidator() {
        RuleFor(e => e.ProductLineCode).NotEmpty();
    }
}

internal static partial class Vtors {
    //private static Dictionary<Entity, AbstractValidator<Entity>> Validators;
    internal static ProductLineValidator ProductLineValidator { get; } = new();

    internal static FluentValidation.Results.ValidationResult Validate<TEntity>() {
        // todo
        throw new NotImplementedException();
    }
}
