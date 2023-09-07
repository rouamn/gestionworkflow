using MediatR;

namespace gestionworkflow.Commands
{
    public class UpdateUserStatusCommand : IRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
