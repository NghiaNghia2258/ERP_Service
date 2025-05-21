using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Models;
using ERP_Service.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController(
     AppDbContext _dbContext,
     IAuthoziService _authoziService
    ) : ControllerBase
{
    public async Task<IActionResult> Register(string username, string password)
    {
        UserLogin newUser = new UserLogin()
        {
            Username = username,
            Password = password,
            RoleGroupId = 1
        };
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
