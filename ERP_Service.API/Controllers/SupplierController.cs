using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Const;
using ERP_Service.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {

        private readonly IAuthoziService _authoziService;
        private readonly AppDbContext _dbContext;

        public SupplierController(IAuthoziService authoziService, AppDbContext dbContext)
        {
            _authoziService = authoziService;
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);
            var token = _authoziService.PayloadToken;

            var res = _dbContext.Suppliers.Where(x => x.StoreId == token.StoreId).Select(x => new
            {
                x.Id,
                x.Name
            }).ToList();
            return Ok(new ApiSuccessResult<object>(res));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierDto model)
        {
            _dbContext.Suppliers.Add(new Domain.Models.InboundReceipts.Supplier()
            {
                Name = model.Name,
                StoreId = _authoziService.PayloadToken.StoreId
            });
            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<bool>(true));
        }
    }
    public class CreateSupplierDto
    {
        public string Name { get; set; }
    }
}
