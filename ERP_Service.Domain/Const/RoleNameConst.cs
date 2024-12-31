namespace ERP_Service.Domain.Const
{
	/// <summary>
	/// Lớp chứa các hằng số tên quyền (RoleNameConst) để xác định các hành động liên quan đến quyền hạn trong hệ thống.
	/// </summary>
	public static class RoleNameConst
	{
		/// <summary>
		/// Quyền tạo khách hàng (Create Customer).
		/// Dùng để kiểm tra và cấp quyền tạo mới khách hàng trong hệ thống.
		/// </summary>
		public readonly static string CREATE_CUSTOMER = "CREATE_CUSTOMER";

		/// <summary>
		/// Quyền xóa khách hàng (Delete Customer).
		/// Dùng để kiểm tra và cấp quyền xóa thông tin khách hàng.
		/// </summary>
		public readonly static string DELETE_CUSTOMER = "DELETE_CUSTOMER";

		/// <summary>
		/// Quyền cập nhật thông tin khách hàng (Update Customer).
		/// Dùng để kiểm tra và cấp quyền chỉnh sửa thông tin khách hàng.
		/// </summary>
		public readonly static string UPDATE_CUSTOMER = "UPDATE_CUSTOMER";

		/// <summary>
		/// Quyền xem thông tin khách hàng (Select Customer).
		/// Dùng để kiểm tra và cấp quyền truy vấn hoặc xem thông tin khách hàng.
		/// </summary>
		public readonly static string SELECT_CUSTOMER = "SELECT_CUSTOMER";
	}
}
