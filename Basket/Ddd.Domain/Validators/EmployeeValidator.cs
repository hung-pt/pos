using FluentValidation;

namespace Ddd.Domain.Validators {
    public class EmployeeValidator : AbstractValidator<Employee> {
        public EmployeeValidator() {
        }
    }
}
