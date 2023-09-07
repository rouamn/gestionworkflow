using gestionworkflow.Commands;
using gestionworkflow.Exeptions;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAvanceQuery(request.Id);
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
            }

            existingUser.Name = request.Name;
            existingUser.Email = request.Email;
            existingUser.Role = request.Role;
            existingUser.Password = request.Password;

            await _userRepository.UpdateUserAsync(existingUser);
        }
    }
}
