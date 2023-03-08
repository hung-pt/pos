using FluentValidation;

namespace Ddd.Domain.Validators;

public class OrderValidator : AbstractValidator<Order> {
    public OrderValidator() {
        RuleFor(e => e.OrderDetails).NotEmpty();
        RuleFor(e => e.OrderDate).GreaterThanOrEqualTo(DateTime.Now);
        RuleFor(e => e.RequiredDate).GreaterThanOrEqualTo(DateTime.Now).When(e => e.RequiredDate != null);
        RuleFor(e => e.ShippedDate).GreaterThanOrEqualTo(DateTime.Now).When(e => e.ShippedDate != null);
        RuleFor(e => e.CustomerNumber).NotNull();
    }
}
