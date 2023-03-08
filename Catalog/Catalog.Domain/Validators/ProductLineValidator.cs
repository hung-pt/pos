using FluentValidation;

namespace Catalog.Domain.Validators;

public class ProductLineValidator : AbstractValidator<ProductLine> {
    public ProductLineValidator() {
        RuleFor(e => e.ProductLineCode).NotEmpty();
    }
}
