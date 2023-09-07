using gestionworkflow.Commands;
using gestionworkflow.Models;
using gestionworkflow.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using gestionworkflow.Repositories;
using System.Security.Claims;

namespace gestionworkflow.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        public UserController(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
            _userRepository = userRepository;

        }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetUserListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var user = await _mediator.Send(query);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
       
        [HttpPost]
        
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            command.Status = "Pending"; // Set the status to "Pending" by default
            var user = await _mediator.Send(command);
            return Created($"/users/{user.Id}", user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        public async Task<ActionResult<User>> Login(LoginUserCommand command)
        {
            var user = await _mediator.Send(command);
            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }
            
            // Retrieve the user's registration status
            var registration = await _userRepository.GetAvanceQuery(user.Id);
            if (registration == null || registration.Status != "Approved")
            {
                return BadRequest("Your registration is pending approval or has been rejected. Please try again later.");
            }

            // Store the user's ID and registration status in the session
            HttpContext.Session.SetInt32("UserId", user.Id);
            // Include the registration status in the response object
            return Ok(new { user, registrationStatus = registration.Status });
        }

        [Route("pending-users")]
        public async Task<IActionResult> GetPendingUsersAsync()
        {
            var users = await _userRepository.GetPendingUsersAsync();
            var usersWithRegistrationStatus = await Task.WhenAll(users.Select(async u =>
            {
                var registration = await _userRepository.GetAvanceQuery(u.Id);
                return new { user = u, registrationStatus = registration?.Status };
            }));
            return Ok(usersWithRegistrationStatus);
        }
        [HttpPut]
        [Route("approve-user-registration/{userId}")]
        public async Task<IActionResult> ApproveUserRegistrationAsync(int userId)
        {
            await _mediator.Send(new UpdateUserStatusCommand { Id = userId, Status = "Approved" });

            // Send notification to user about the approval of their registration
            // ...

            return Ok();
        }

        [HttpPut]
        [Route("reject-user-registration/{userId}")]
        public async Task<IActionResult> RejectUserRegistrationAsync(int userId)
        {
            await _mediator.Send(new UpdateUserStatusCommand { Id = userId, Status = "Rejected" });

            // Send notification to user about the rejection of their registration
            // ...

            return Ok();
        }
      
    }

    }

