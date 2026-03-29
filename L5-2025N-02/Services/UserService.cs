using L5_2025N_02.Controllers.Dtos;
using L5_2025N_02.Database;
using L5_2025N_02.Exceptions;
using L5_2025N_02.Model;
using Microsoft.EntityFrameworkCore;

namespace L5_2025N_02.Services;

public class UserService(AppDbContext dbContext)
{
	private readonly AppDbContext _db = dbContext;

	public async Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
	{
		var emailExists = await _db.Users.AnyAsync(e => e.Email == request.Email, cancellationToken);
		if (emailExists)
			throw new BadRequestException("Użytkownik o takim adresie email juz istnieje!");

		var user = new User
		{
			Id = Guid.NewGuid(),
			Name = request.Name,
			Email = request.Email
		};
		
		_db.Users.Add(user);
		await _db.SaveChangesAsync(cancellationToken);
		return Map(user);
	}

	public async Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var user = await _db.Users.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
		return user is null ? null : Map(user);
	}

	public async Task<IReadOnlyList<UserResponse>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _db.Users.OrderBy(e => e.Name).Select(e => Map(e)).ToListAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
	{
		var user = await _db.Users.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
		if (user is null) 
			return false;
		
		var emailExists = await _db.Users.AnyAsync(e => e.Email == request.Email, cancellationToken);
		if (emailExists)
			throw new BadRequestException("Email juz jest zajęty!");
		
		
		user.Name = request.Name;
		user.Email = request.Email;
		
		await _db.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var user = await _db.Users.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
		if (user is null)
			return false;
		_db.Users.Remove(user);
		await _db.SaveChangesAsync(cancellationToken);
		return true;
	}
	
	private static UserResponse Map(User user) => new()
	{
		Id = user.Id,
		Name = user.Name,
		Email = user.Email,
		CreatedOn = user.CreatedOn
	};
}