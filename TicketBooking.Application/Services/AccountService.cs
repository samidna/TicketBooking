using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.DTOs.User;
using TicketBooking.Application.Interfaces;
using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Application.Services;
public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AccountService(UserManager<AppUser> userManager, ITokenService tokenService, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }

    public async Task RegisterAsync(UserRegisterDto dto)
    {
        var user = new AppUser
        {
            Email = dto.Email,
            UserName = dto.Username,
            Name = dto.Name,
            Surname = dto.Surname
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(x => x.Description));
            throw new Exception(errors);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = "User"
            });
        }

        await _userManager.AddToRoleAsync(user, "User");
    }

    public async Task<AuthResponseDto> LoginAsync(UserLoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            throw new Exception("Invalid username or password.");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.UserName,
            Expiration = DateTime.Now.AddMinutes(60)
        };
    }

    public async Task<PagedResponse<UserGetDto>> GetUsersPagedAsync(int page, int pageSize)
    {
        var query = _userManager.Users;

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UserGetDto
            {
                Name = u.Name,
                Surname = u.Surname,
                Phone = u.PhoneNumber,
                Username = u.UserName,
                Email = u.Email
            }).ToListAsync();

        return new PagedResponse<UserGetDto>(items, totalCount, page, pageSize);
    }
}
