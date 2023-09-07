using gestionworkflow.Commands;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.commandsHandlers
{
    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserStatusCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAvanceQuery(request.Id);

            if (user != null)
            {
                user.Status = request.Status;
                await _userRepository.UpdateUserAsync(user);
            }
        }

    }
}
