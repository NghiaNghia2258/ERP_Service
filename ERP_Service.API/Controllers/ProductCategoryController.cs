using ERP_Service.Application.Queries.Products;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Service.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductCategoryController : ControllerBase
	{

		private readonly IAuthoziService _authoziService;
		private readonly IMediator _mediator;
        private readonly AppDbContext _dbContext;

        public ProductCategoryController(IAuthoziService authoziService, IMediator mediator, AppDbContext dbContext)
        {
            _authoziService = authoziService;
            _mediator = mediator;
            _dbContext = dbContext;
        }
        [HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] OptionFilterProductCategory option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);
			var token = _authoziService.PayloadToken;

			var result = await _mediator.Send(new GetAllProductCategoryQuery(option));
			var res = _dbContext.ProductCategories.Where(x => x.StoreId == token.StoreId).Select(x => new
			{
				x.Id,
				x.Name
			}).ToList();
			return Ok(new ApiSuccessResult<object>(res));
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateCategoryDto model)
		{
			_dbContext.ProductCategories.Add(new Domain.Models.Products.ProductCategory()
			{
				Name = model.Name,
				StoreId = _authoziService.PayloadToken.StoreId
			});
			await _dbContext.SaveChangesAsync();
			return Ok(new ApiSuccessResult<bool>(true));
		}
	}
	public class CreateCategoryDto
	{
		public string Name { get; set; }
	}
}
