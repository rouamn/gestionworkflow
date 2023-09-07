using MediatR;

namespace gestionworkflow.Queries
{
    public class GetUserListQuery:IRequest<IEnumerable<Models.User>>
    {
    }
}
