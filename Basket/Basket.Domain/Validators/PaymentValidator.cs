using FluentValidation;

namespace Ddd.Domain.Validators {
    public class PaymentValidator : AbstractValidator<Payment> {
        public PaymentValidator() {
        }
    }
}
