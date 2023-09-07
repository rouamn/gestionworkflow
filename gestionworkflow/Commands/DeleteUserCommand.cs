using MediatR;

namespace gestionworkflow.Commands
{
    public  class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
    }
}
