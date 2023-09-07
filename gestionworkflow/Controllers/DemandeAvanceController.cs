using gestionworkflow.Commands;
using gestionworkflow.Models;
using gestionworkflow.Queries;
using gestionworkflow.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gestionworkflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeAvanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        public DemandeAvanceController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;

        }

        [HttpPost]

        public async Task<IActionResult> AddDemandeCongeAsync([FromBody] AdDemandeAvanceCommand command)
        {
            command.Statut = "Pending"; // Set the status to "Pending" by default
            var user = await _mediator.Send(command);
            return Created($"/demandeAvance/{user.UtilisateurId}", user);
        }



        [Route("pending-avance")]
        public async Task<IActionResult> GetPendingAvancesAsync()
        {
            var demandes = await _userRepository.GetPendingAvancesAsync();
            var demandesStatus = await Task.WhenAll(demandes.Select(async u =>
            {
                var registration = await _userRepository.GetDemandeAvanceByIdAsync(u.Id);
                return new { demande = u, registrationStatus = registration?.Statut };
            }));
            return Ok(demandesStatus);
        }
        [HttpPut]
        [Route("approve-avance/{userId}")]
        public async Task<IActionResult> ApproveDemandeAsync(int userId)
        {
            await _mediator.Send(new UpdateAvanceStatusCommand { Id = userId, Statut = "Approved" });

            // Send notification to user about the approval of their registration
            // ...

            return Ok();
        }

        [HttpPut]
        [Route("reject-avance/{userId}")]
        public async Task<IActionResult> RejectAvanceRegistrationAsync(int userId)
        {
            await _mediator.Send(new UpdateAvanceStatusCommand { Id = userId, Statut = "Rejected" });

            // Send notification to user about the rejection of their registration
            // ...

            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<DemandeAvance>>> GetAvanceForUser(int userId)
        {
            var user = await _userRepository.GetAvanceQuery(userId);

            if (user == null)
                return NotFound();

            var query = new GetAvanceQuery { UserId = userId };
            var Demande = await _mediator.Send(query);

            return Ok(Demande);
        }
    }
}

