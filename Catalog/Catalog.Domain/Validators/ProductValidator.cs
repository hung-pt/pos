using Catalog.Domain;
using FluentValidation;

namespace Catalog.Domain.Validators;

public class ProductValidator : AbstractValidator<Product> {
    public ProductValidator() {
        RuleFor(product => product.ProductCode).NotEmpty();
        RuleFor(product => product.QuantityInStock).GreaterThanOrEqualTo(0);
        RuleFor(product => product.BuyPrice).GreaterThan(0);
        RuleFor(product => product.MSRP).GreaterThan(0);
    }
}
