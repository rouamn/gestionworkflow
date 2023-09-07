using gestionworkflow.Models;
using gestionworkflow.Queries;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAvanceQuery(request.Id);
        }
    }
}

