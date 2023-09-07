using gestionworkflow.Commands;
using gestionworkflow.Models;
using gestionworkflow.Queries;
using gestionworkflow.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gestionworkflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeCongeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        public DemandeCongeController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;

        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> AddDemandeCongeAsync([FromBody] AddDemandeCongeCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetAvanceQuery(int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            command.UtilisateurId = user.Id; // Set the UtilisateurId property of the command to the user ID
            command.Statut = "Pending"; // Set the status to "Pending" by default

            var demandeConge = await _mediator.Send(command);

            // Add the user's ID to the demand congé object
            demandeConge.UtilisateurId = user.Id;

            // Save the demand congé with the user's ID to the database
            await _userRepository.AddDemandeCongeAsync(demandeConge);

            return Created($"/demandeConge/{demandeConge.Id}", demandeConge);
        }
        [HttpGet]
        public async Task<ActionResult<List<DemandeConge>>> GetLeaveRequestsForUser(int userId)
        {
            var user = await _userRepository.GetAvanceQuery(userId);

            if (user == null)
                return NotFound();

            var query = new GetDemandeCongeQuery { UserId = userId };
            var leaveRequests = await _mediator.Send(query);

            return Ok(leaveRequests);
        }
        [Route("pending-demande")]
        public async Task<IActionResult> GetPendingDemandesAsync()
        {
            var demandes = await _userRepository.GetPendingDemandesAsync();
            var demandesStatus = await Task.WhenAll(demandes.Select(async u =>
            {
                var registration = await _userRepository.GetDemandeCongeByIdAsync(u.Id);
                return new { demande = u, registrationStatus = registration?.Statut };
            }));
            return Ok(demandesStatus);
        }
        [HttpPut]
        [Route("approve-demande/{userId}")]
        public async Task<IActionResult> ApproveDemandeAsync(int userId)
        {
            await _mediator.Send(new UpdateDemandeStatusCommand { Id = userId, Statut = "Approved" });

            // Send notification to user about the approval of their registration
            // ...

            return Ok();
        }

        [HttpPut]
        [Route("reject-demande/{userId}")]
        public async Task<IActionResult> RejectDemandeRegistrationAsync(int userId)
        {
            await _mediator.Send(new UpdateDemandeStatusCommand { Id = userId, Statut = "Rejected" });

            // Send notification to user about the rejection of their registration
            // ...

            return Ok();
        }

    }
}
