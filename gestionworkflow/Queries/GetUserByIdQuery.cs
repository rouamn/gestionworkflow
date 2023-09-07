using gestionworkflow.Models;
using MediatR;

namespace gestionworkflow.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
    }

}
