using MediatR;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Application.Interfaces;
using BCrypt.Net;
using EmployeeManagement.Application.Dtos;

namespace EmployeeManagement.Application.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IEmployeeRepository employeeRepository, IJwtService jwtService)
        {
            _employeeRepository = employeeRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByUsernameAsync(request.Username);
            if (employee == null || !BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = _jwtService.GenerateToken(employee);

            // map employee entity to EmployeeDto
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Username = employee.Username,
                FullName = employee.FullName,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                Phone = employee.Phone,
                Email = employee.Email,
                Role = employee.Role,
                Department = employee.Department,
                Position = employee.Position,
                JoiningDate = employee.JoiningDate,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                CreatedAt = employee.CreatedAt

                // add other properties as needed
            };

            return new LoginResponse
            {
                Token = token,
                Employee = employeeDto
            };
        }
    }

}
