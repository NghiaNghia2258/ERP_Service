using ERP_Service.Application.Queries.Products;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.PagingRequest;
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

		public ProductCategoryController(IAuthoziService authoziService, IMediator mediator)
		{
			_authoziService = authoziService;
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] OptionFilterProductCategory option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetAllProductCategoryQuery(option));
			return Ok(result);
		}
	}
}
