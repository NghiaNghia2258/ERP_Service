using ERP_Service.Application.Mapper.Model.Inbounds;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.InboundReceipts;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERP_Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InboundReceiptsController(
     AppDbContext _context,
     IAuthoziService _authoziService
    ) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        var receipt = await _context.InboundReceipts
            .Include(r => r.InboundReceiptItems)
                .ThenInclude(i => i.ProductVariant)
                .ThenInclude(pv => pv.Product)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (receipt == null)
            return NotFound();

        var result = new
        {
            stockInDate = receipt.StockInDate.ToString("yyyy-MM-dd"),
            supplierId = receipt.SupplierId,
            note = receipt.Note,
            items = receipt.InboundReceiptItems.Select(i => new
            {
                id = i.ProductVariantId,
                name = i.ProductVariant?.Product?.Name,
                image = i.ProductVariant?.ImageUrl,
                quantity = i.Quantity,
                unitPrice = i.UnitPrice
            }).ToList()
        };

        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create(InboundReceipt inboundReceipt)
    {
        await _context.InboundReceipts.AddAsync(inboundReceipt);
        await _context.SaveChangesAsync();
        return Ok("");
    }
    [HttpPut]
    public async Task<IActionResult> Update(InboundUpdateDto inboundReceipt)
    {
        var inbound = await _context.InboundReceipts.FirstOrDefaultAsync(x => x.Id == inboundReceipt.Id);
        if (inbound == null) return NotFound();
        inbound.Note = inboundReceipt.Note;
        inbound.SupplierId = inboundReceipt.SupplierId;
        inbound.StockInDate = inboundReceipt.StockInDate;
        for (int i = 0; i < inbound.InboundReceiptItems.Count; i++)
        {
            var inboundItem = inbound.InboundReceiptItems[i];
            var inboundItemUpdate = inboundReceipt.Items.FirstOrDefault(x => x.Id == inboundItem.ProductVariantId);
            inboundItem.UnitPrice = inboundItem.UnitPrice;
            inboundItem.Quantity = inboundItem.Quantity;
        }

        await _context.SaveChangesAsync();
        return Ok("");
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] InboundOptionFilter option)
    {
        var data = _context.InboundReceipts
            .Include(ir => ir.InboundReceiptItems)
            .Where(ir => ir.StoreId == _authoziService.PayloadToken.StoreId)
            .OrderByDescending(ir => ir.CreatedAt);

        int begin = option.PageSize * (option.PageIndex - 1);

        var result = await data.Select(ir => new 
        {
            ReceiptId = ir.Id,
            CreatedAt = ir.CreatedAt,
            CreatedBy = ir.CreatedBy ?? "Unknown",
            SupplierName = ir.SupplierId ?? "N/A",
            TotalQuantity = ir.InboundReceiptItems.Sum(i => i.Quantity),
            TotalValue = ir.InboundReceiptItems.Sum(i => i.Quantity * i.UnitPrice)
        }).Skip(begin).Take(option.PageSize).ToListAsync();

        var res = new ApiSuccessResult<IEnumerable<object>>(result)
        {
            TotalRecordsCount = data.Count(),
        };
        return Ok(res);
    }
    [HttpGet("getInboundSelectableProducts")]
    public IActionResult getInboundSelectableProducts(InboundOptionFilter option)
    {
        int begin = option.PageSize * (option.PageIndex - 1);

        var query = _context.ProductVariants
            .Include(p => p.Product)
            .Select(x => new
            {
                Id = x.Id,
                Name = $"{x.Product.Name}-{x.PropertyValue1}-{x.PropertyValue2}",
                Inventory = x.Inventory,
                Image = x.ImageUrl
            });

        var result = query.Skip(begin).Take(option.PageSize).ToList();

        var res = new ApiSuccessResult<IEnumerable<object>>(result)
        {
            TotalRecordsCount = query.Count(),
        };
        return Ok(res);
    }
}
