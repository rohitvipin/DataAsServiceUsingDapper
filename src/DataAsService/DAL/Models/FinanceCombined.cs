namespace DataAsService.DAL.Models
{
    public class FinanceCombined
    {
        public string AccBalId { get; set; }
        public double CurrentBalance { get; set; }
        public double BeginingBalance { get; set; }
        public string PlCategory { get; set; }
        public double AcctBalMonthValue { get; set; }
        public double AcctbudgfcMonthValue { get; set; }
        public string MonthId { get; set; }
        public string MonthName { get; set; }
    }
}