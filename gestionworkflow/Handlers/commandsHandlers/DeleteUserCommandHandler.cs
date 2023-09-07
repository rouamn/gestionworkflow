using gestionworkflow.Commands;
using gestionworkflow.Exeptions;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commands
{

        

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IUserRepository _userRepository;

            public DeleteUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAvanceQuery(request.Id);
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
            }
            await _userRepository.DeleteUserAsync(request.Id);
        }


    }
    }

