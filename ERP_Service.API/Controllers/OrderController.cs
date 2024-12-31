using ERP_Service.Application.Comands.Orders;
using ERP_Service.Application.Mapper.Model.Orders;
using ERP_Service.Application.Queries.Orders;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.PagingRequest;
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

			var result = await _mediator.Send(new CreateOrderUsePOSCommand());
			return Ok(result);
		}
		[HttpPut]
		public async Task<IActionResult> Update(UpdateOrderDto orderDto)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.UPDATE_CUSTOMER);

			var result = await _mediator.Send(new UpdateOrderCommand(orderDto));
			return Ok(result);
		}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.DELETE_CUSTOMER);

			var result = await _mediator.Send(new DeleteOrderCommand(id));
			return Ok(result);
		}
		[HttpGet("payment/{orderId}")]
		public async Task<IActionResult> Payment(Guid orderId)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new PaymentCommand(orderId));
			return Ok(result);
		}
		[HttpDelete("remove-from-cart/{orderItemId}")]
		public async Task<IActionResult> RemoveFromCart(int orderItemId)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new RemoveFromCartCommand(orderItemId));
			return Ok(result);
		}
		[HttpGet("get-order-not-completed")]
		public async Task<IActionResult> GetOrderNotCompleted([FromQuery] OptionFilterOrder option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetOrderNotCompletedQuery(option));
			return Ok(result);
		}
		[HttpGet("get-all")]
		public async Task<IActionResult> GetAll([FromQuery] OptionFilterOrder option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetAllOrderQuery(option));
			return Ok(result);
		}
	}
}
