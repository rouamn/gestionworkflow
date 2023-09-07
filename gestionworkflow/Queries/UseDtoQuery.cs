using gestionworkflow.Models;
using MediatR;

namespace gestionworkflow.Queries
{
public class UserDtoQuery : IRequest<User>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
}
