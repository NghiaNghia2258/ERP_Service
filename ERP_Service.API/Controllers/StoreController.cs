using ERP_Service.Application.Mapper.Model.Stores;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Stores;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using ERP_Service.Shared.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERP_Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController(
     AppDbContext _dbContext,
     IAuthoziService _authoziService
    ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        UserLogin newUser = new UserLogin()
        {
            Username = model.Username,
            Password = model.Password,
            RoleGroupId = 3,
            Stores = new List<Store>() { new Store() }
        };
        _dbContext.UserLogins.Add(newUser);
        await _dbContext.SaveChangesAsync();

        return Ok(new ApiSuccessResult());
    }
    [HttpGet("store-info")]
    public async Task<IActionResult> GetStoreById()
    {
        PayloadToken token = _authoziService.PayloadToken;
        var store = await _dbContext.Stores
            .Include(s => s.UserLogin) 
            .FirstOrDefaultAsync(s => s.Id == token.StoreId);

        if (store == null)
        {
            return NotFound(new ApiErrorResult());
        }

        return Ok(new ApiSuccessResult<StoreDto>(new StoreDto
        {
            Id = store.Id.ToString(),
            Name = store.Name,
            Description = store.Description,
            Logo = store.Logo,
            CoverImage = store.CoverImage,
            Location = store.Location,
            ContactPhone = store.ContactPhone,
            ContactEmail = store.ContactEmail,
            Facebook = store.Facebook,
            Instagram = store.Instagram,
            Twitter = store.Twitter,
            Policies = JsonConvert.DeserializeObject<List<StorePolicyDto>>(store.Policies),
            UserName = store.UserLogin.Username,
            Password = store.UserLogin.Password
        }));
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateStoreDto model)
    {
        if (!Guid.TryParse(model.Id, out Guid storeId))
        {
            return BadRequest(new ApiErrorResult());
        }

        var store = await _dbContext.Stores
            .Include(s => s.UserLogin)
            .FirstOrDefaultAsync(s => s.Id == storeId);

        if (store == null)
        {
            return NotFound(new ApiErrorResult());
        }

        store.Name = model.Name;
        store.Description = model.Description;
        store.Logo = model.Logo;
        store.CoverImage = model.CoverImage;
        store.Location = model.Location;
        store.ContactPhone = model.ContactPhone;
        store.ContactEmail = model.ContactEmail;
        store.Facebook = model.Facebook;
        store.Instagram = model.Instagram;
        store.Twitter = model.Twitter;
        store.Policies = JsonHelper.ConvertToJsonString(model.Policies);
        store.UserLogin.Password = model.Password;

        await _dbContext.SaveChangesAsync();

        return Ok(new ApiSuccessResult());
    }
}
public class RegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}