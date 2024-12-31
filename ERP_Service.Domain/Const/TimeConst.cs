namespace ERP_Service.Domain.Const
{
	/// <summary>
	/// Lớp chứa các hằng số thời gian được sử dụng trong hệ thống.
	/// Cung cấp các giá trị thời gian tiện dụng.
	/// </summary>
	public static class TimeConst
	{
		/// <summary>
		/// Lấy thời gian cách đây 3 tháng so với thời điểm hiện tại.
		/// </summary>
		public static DateTime ThreeMonthsAgo { get => DateTime.Now.AddMonths(-3); }

		/// <summary>
		/// Lấy thời gian hiện tại (local time).
		/// </summary>
		public static DateTime Now { get => DateTime.Now; }

		/// <summary>
		/// Lấy thời gian hiện tại theo múi giờ UTC.
		/// </summary>
		public static DateTime UtcNow { get => DateTime.UtcNow; }
	}
}
