using gestionworkflow.Models;
using gestionworkflow.Queries;
using gestionworkflow.Repositories;
using MediatR;

namespace gestionworkflow.Handlers
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users;
        }
    }
}

