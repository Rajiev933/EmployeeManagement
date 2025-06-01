using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public EmployeeDto Employee { get; set; } = new EmployeeDto();
    }
}
