namespace ERP_Service.Application.Mapper.Model.Stores;

public class StoreDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Logo { get; set; } = default!;
    public string CoverImage { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string ContactPhone { get; set; } = default!;
    public string ContactEmail { get; set; } = default!;
    public List<StorePolicyDto> Policies { get; set; } = new();
    public string? Facebook { get; set; }
    public string? Instagram { get; set; }
    public string? Twitter { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
public class StorePolicyDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
}