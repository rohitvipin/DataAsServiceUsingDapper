namespace DataAsService.DAL.Models
{
    public class SalesCombined
    {
        public string Category { get; set; }
        public double GrowShare { get; set; }
        public string DepartmentId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double CostShare { get; set; }
        public string Grower { get; set; }
        public string LocationName { get; set; }
        public double Profit { get; set; }
        public string SalesPerson { get; set; }
    }
}