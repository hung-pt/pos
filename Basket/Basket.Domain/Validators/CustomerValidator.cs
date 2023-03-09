using FluentValidation;

namespace Ddd.Domain.Validators {
    public class CustomerValidator : AbstractValidator<Customer> {
        public CustomerValidator() {
        }
    }
}
