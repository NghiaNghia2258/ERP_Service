namespace ERP_Service.Domain.Const
{
	/// <summary>
	/// Lớp chứa các biến hằng dùng để lưu trữ tổng số bản ghi của từng loại dữ liệu.
	/// </summary>
	public static class TotalRecords
	{
		/// <summary>
		/// Tổng số bản ghi của khách hàng (Customer).
		/// Được sử dụng để lưu số lượng khách hàng trong các truy vấn.
		/// </summary>
		public static int CUSTOMER = 0;

		/// <summary>
		/// Tổng số bản ghi của sản phẩm (Product).
		/// Được sử dụng để lưu số lượng sản phẩm trong các truy vấn.
		/// </summary>
		public static int PRODUCT = 0;
	}
}
