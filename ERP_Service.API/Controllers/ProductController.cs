using ERP_Service.Application.Comands.Customers;
using ERP_Service.Application.Comands.Products;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Application.Queries.Customers;
using ERP_Service.Application.Queries.Products;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Const;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Service.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IAuthoziService _authoziService;
		private readonly IMediator _mediator;

		public ProductController(IAuthoziService authoziService, IMediator mediator)
		{
			_authoziService = authoziService;
			_mediator = mediator;
		}
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetByIdProductQuery(id));
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateProductDto model)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.CREATE_CUSTOMER);

			var result = await _mediator.Send(new CreateProductCommand(model));
			return Ok(result);
		}
	}
}
