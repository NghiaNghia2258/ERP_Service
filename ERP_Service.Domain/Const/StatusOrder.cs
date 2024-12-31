namespace ERP_Service.Domain.Const;

/// <summary>
/// Class chứa các trạng thái của đơn hàng trong hệ thống.
/// Các trạng thái này được biểu diễn dưới dạng hằng số.
/// </summary>
public class StatusOrder
{
	/// <summary>
	/// Đơn hàng đang chờ xử lý.
	/// </summary>
	public const int Pending = 1;

	/// <summary>
	/// Đơn hàng đang được xử lý.
	/// </summary>
	public const int Processing = 2;

	/// <summary>
	/// Đơn hàng đã hoàn thành.
	/// </summary>
	public const int Completed = 3;

	/// <summary>
	/// Đơn hàng đã bị hủy.
	/// </summary>
	public const int Cancelled = 4;

	/// <summary>
	/// Đơn hàng đã được hoàn tiền.
	/// </summary>
	public const int Refunded = 5;

	/// <summary>
	/// Đơn hàng thất bại (ví dụ: thanh toán không thành công).
	/// </summary>
	public const int Failed = 6;
}
