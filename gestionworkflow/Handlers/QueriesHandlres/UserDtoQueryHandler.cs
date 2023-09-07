using gestionworkflow.Context;
using gestionworkflow.Models;
using gestionworkflow.Queries;
using gestionworkflow.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace gestionworkflow.Handlers.QueriesHandlres
{
    public class UserDtoQueryHandler : IRequestHandler<UserDtoQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public UserDtoQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UserDtoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null || user.Password != request.Password)
            {
                return null;
            }

            return user;
        }
    }

   
}
