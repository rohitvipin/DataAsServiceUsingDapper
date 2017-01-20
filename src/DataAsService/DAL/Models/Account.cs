namespace DataAsService.DAL.Models
{
    /// <summary>
    /// Account details
    /// </summary>
    public class Account
    {
        public string AccountId { get; set; }
        public double CurrentBalance { get; set; }
        public string PlCategory { get; set; }
        public double EndBalance { get; set; }
        public double ForecastBalance { get; set; }
        public string MonthId { get; set; }
        public char MonthFlag { get; set; }
    }
}