﻿using AutoMapper;
using ERP_Service.Application.Mapper.Model.Orders;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.PagingRequest;
using MediatR;

namespace ERP_Service.Application.Queries.Orders;

public class GetAllOrderQuery: IRequest<ApiResult>
{
	public GetAllOrderQuery(OptionFilterOrder option)
	{
		Option = option;
	}

	public OptionFilterOrder Option { get; set; }
}
public class GetAllOrderQueryHandle :QueryHandlerBase, IRequestHandler<GetAllOrderQuery, ApiResult>
{
	public GetAllOrderQueryHandle(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : base(unitOfWork, mapper, cacheService)
	{
	}

	public async Task<ApiResult> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<Order> orders = await _unitOfWork.Order.GetAll(request.Option);
		IEnumerable<GetAllOrderDto> orderDto = _mapper.Map<IEnumerable<GetAllOrderDto>>(orders);
		return new ApiSuccessResult<IEnumerable<GetAllOrderDto>>(orderDto);
	}
}
