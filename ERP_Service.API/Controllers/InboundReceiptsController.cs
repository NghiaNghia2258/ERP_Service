using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.InboundReceipts;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Create(CreateInboundReceiptDto model)
    {
        PayloadToken token = _authoziService.PayloadToken;
        var inboundReceipt = new InboundReceipt()
        {
            Note = model.Note,
            StockInDate = model.StockInDate,
            CreatedName = token.Username,
            CreatedBy = token.Username,
            StoreId = token.StoreId,
            SupplierId = model.SupplierId,
            InboundReceiptItems = model.Items.Select(x => new InboundReceiptItem
            {
                ProductVariantId = x.Id,
                Name = x.Name,
                Image = x.Image,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList()
        };
        foreach (var item in model.Items)
        {
            var updateVariant = _context.ProductVariants.FirstOrDefault(x => x.Id == item.Id);
            if(updateVariant is not null)
            {
                updateVariant.Inventory += item.Quantity;
            }
        }
        await _context.InboundReceipts.AddAsync(inboundReceipt);
        await _context.SaveChangesAsync();
        return Ok(new ApiSuccessResult<bool>(true));
    }
    [HttpPut]
    public async Task<IActionResult> Update(CreateInboundReceiptDto inboundReceipt)
    {
        var inbound = await _context.InboundReceipts.FirstOrDefaultAsync(x => x.Id == inboundReceipt.Id);
        if (inbound == null) return NotFound();
        inbound.Note = inboundReceipt.Note;
        inbound.SupplierId = inboundReceipt.SupplierId;
        inbound.StockInDate = inboundReceipt.StockInDate;
        _context.InboundReceiptItems.Where(x => x.InboundReceiptId == inbound.Id).ExecuteDelete();

        inbound.InboundReceiptItems = inboundReceipt.Items.Select(x => new InboundReceiptItem
        {
            ProductVariantId = x.Id,
            Name = x.Name,
            Image = x.Image,
            Quantity = x.Quantity,
            UnitPrice = x.UnitPrice
        }).ToList();

        await _context.SaveChangesAsync();
        return Ok("");
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] InboundOptionFilter option)
    {
        var data = _context.InboundReceipts
            .Include(x => x.Supplier)
            .Include(ir => ir.InboundReceiptItems)
            .Where(ir => ir.StoreId == _authoziService.PayloadToken.StoreId
            && option.KeyWord == null || ir.Id.ToString().Contains(option.KeyWord)
            )
            .OrderByDescending(ir => ir.CreatedAt);

        int begin = option.PageSize * (option.PageIndex - 1);

        var result = await data.Select(ir => new 
        {
            ReceiptId = ir.Id,
            CreatedAt = ir.StockInDate,
            CreatedBy = ir.CreatedBy ?? "Unknown",
            SupplierName = ir.Supplier == null ? "N/A" : ir.Supplier.Name,
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
public class CreateInboundReceiptDto
{
    public Guid? Id { get; set; }
    public DateTime StockInDate { get; set; }
    public string Note { get; set; }
    public int SupplierId { get; set; }
    public List<CreateInboundReceiptItemDto> Items { get; set; }

}
public class CreateInboundReceiptItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}
