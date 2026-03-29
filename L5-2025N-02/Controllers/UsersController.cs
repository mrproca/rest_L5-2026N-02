using L5_2025N_02.Controllers.Dtos;
using L5_2025N_02.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace L5_2025N_02.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/users")]
	public class UsersController(UserService userService, ILogger<UsersController> logger) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<UserResponse>> Create(
			[FromBody] CreateUserRequest request,
			CancellationToken ct)
		{
			logger.LogInformation($"Creating user {request.Name} with email {request.Email}");
			var result = await userService.CreateAsync(request, ct);
			return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<UserResponse>> GetById(Guid id, CancellationToken ct)
		{
			logger.LogInformation($"Getting user {id}");
			var result = await userService.GetByIdAsync(id, ct);
			if (result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetAll(CancellationToken ct)
		{
			logger.LogInformation($"Getting all users");
			var result = await userService.GetAllAsync(ct);
			return Ok(result);
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(
			Guid id,
			[FromBody] UpdateUserRequest request,
			CancellationToken ct)
		{
			logger.LogInformation($"Updating user {id}");
			var updated = await userService.UpdateAsync(id, request, ct);
			if (!updated)
				return NotFound();

			return NoContent();
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
		{
			logger.LogInformation($"Deleting user {id}");
			var deleted = await userService.DeleteAsync(id, ct);
			if (!deleted)
				return NotFound();

			return NoContent();
		}
	}
}
