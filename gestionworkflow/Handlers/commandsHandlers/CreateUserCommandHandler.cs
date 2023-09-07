using gestionworkflow.Commands;
using gestionworkflow.Models;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
                Password = request.Password,
                Status = "Pending" // Set the status to "Pending" by default
            };

            await _userRepository.CreateUserAsync(user);

            return user;
        }
    }

}
