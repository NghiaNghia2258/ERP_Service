namespace ERP_Service.Domain.Const
{
	/// <summary>
	/// Lớp chứa các mã trạng thái tùy chỉnh dùng trong hệ thống.
	/// Các mã này giúp định nghĩa trạng thái phản hồi của hệ thống.
	/// </summary>
	public static class StatusCodeCustom
	{
		/// <summary>
		/// Trạng thái thành công.
		/// </summary>
		public static int SUCCESS = 200;

		/// <summary>
		/// Trạng thái lỗi hệ thống hoặc server.
		/// </summary>
		public static int ERROR = 500;

		/// <summary>
		/// Trạng thái không tìm thấy tài nguyên hoặc đối tượng.
		/// </summary>
		public static int NOTFOUND = 404;
	}
}
