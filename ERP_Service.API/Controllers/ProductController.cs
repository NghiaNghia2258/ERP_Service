using AutoMapper;
using Azure.Core;
using ERP_Service.Application.Comands.Products;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Application.Queries.Products;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IAuthoziService _authoziService;
		private readonly IMediator _mediator;
		private readonly AppDbContext _dbContext;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;


        public ProductController(IAuthoziService authoziService, IMediator mediator, AppDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_authoziService = authoziService;
			_mediator = mediator;
			_dbContext = dbContext;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
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
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] GetByIdProductDto model)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.UPDATE_CUSTOMER);

			var result = await _mediator.Send(new UpdateProductCommand(model));
			return Ok(result);
		}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.DELETE_CUSTOMER);

			var result = await _mediator.Send(new DeleteProductCommand(id));
			return Ok(result);
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] OptionFilterProduct option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

            ApiResult res = new ApiSuccessResult();

            var products = await _unitOfWork.Product.GetAll(option);

            if (products == null)
            {
                res = new ApiNotFoundResult("product not found.");
                return BadRequest(res);
            }

            var productsDto = _mapper.Map<IEnumerable<GetAllProductDto>>(products);

            res = new ApiSuccessResult<IEnumerable<GetAllProductDto>>(productsDto)
            {
                TotalRecordsCount = TotalRecords.PRODUCT,
                FetchedRecordsCount = productsDto.LongCount()
            };

            return Ok(res);
		}
		[HttpGet("get-variants/{productId:int}")]
		public async Task<IActionResult> GetVariantsByProductId(int productId)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetProductVariantByProductIdQuery(productId));
			return Ok(result);
		}
        [HttpGet("get-statistics/{productId:int}")]
        public async Task<IActionResult> GetProductStatistics(int productId)
        {
			var statistics = await _dbContext.Products
				.Include(x => x.ProductRates)
				.Where(x => x.Id.Equals(productId))
				.Select(x => new
				{
                    productId = x.Id,
                    rateCount = x.ProductRates.Count,
					sellCount = x.SellCount,
                    ratingOneStar = x.ProductRates.Count(x => x.Rating.Equals(1)),
                    ratingTwoStar = x.ProductRates.Count(x => x.Rating.Equals(2)),
                    ratingThreeStar = x.ProductRates.Count(x => x.Rating.Equals(3)),
                    ratingFourStar = x.ProductRates.Count(x => x.Rating.Equals(4)),
                    ratingFiveStar = x.ProductRates.Count(x => x.Rating.Equals(5)),
                })
				.FirstOrDefaultAsync();
            
            return Ok(new ApiSuccessResult<object>(statistics));
        }
    }
}
