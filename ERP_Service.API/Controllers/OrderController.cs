using ERP_Service.Application.Comands.Orders;
using ERP_Service.Application.Queries.Orders;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Const;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Service.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IAuthoziService _authoziService;
		private readonly IMediator _mediator;

		public OrderController(IAuthoziService authoziService, IMediator mediator)
		{
			_authoziService = authoziService;
			_mediator = mediator;
		}
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetByIdOrderQuery(id));
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> Create()
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.CREATE_CUSTOMER);

			var result = await _mediator.Send(new CreateOrderCommand());
			return Ok(result);
		}
	}
}
