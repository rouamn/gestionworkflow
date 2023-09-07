using gestionworkflow.Models;
using MediatR;

namespace gestionworkflow.Commands
{
   
        public class CreateUserCommand : IRequest<User>
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public string Password { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
    }

    }
