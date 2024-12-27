namespace ERP_Service.Domain.Const
{
    public static class TimeConst
    {
        public static DateTime ThreeMonthsAgo { get => DateTime.Now.AddMonths(-3); }
        public static DateTime Now { get => DateTime.Now; }
        public static DateTime UtcNow { get => DateTime.UtcNow; }
    }
}
