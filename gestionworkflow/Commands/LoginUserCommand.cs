using gestionworkflow.Models;
using MediatR;

namespace gestionworkflow.Commands
{
    public class LoginUserCommand : IRequest<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
