using MediatR;
using EmployeeManagement.Application.Dtos;  // your DTO namespace

namespace EmployeeManagement.Application.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponse>  // <-- specify response DTO here
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
