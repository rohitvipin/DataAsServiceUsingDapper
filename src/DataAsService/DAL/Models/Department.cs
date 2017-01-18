namespace DataAsService.DAL.Models
{
    public class Department
    {
        public int UniqueID { get; set; }
        public string DEPARTMENT { get; set; }
        public string DEPTNAME { get; set; }
        public string LOCATION { get; set; }
        public string SCALE { get; set; }
        public bool NUTRIENT { get; set; }
        public bool DISCOUNT { get; set; }
        public bool QUANTITY { get; set; }
        public string Category { get; set; }
        public bool Classification1 { get; set; }
        public bool Classification2 { get; set; }
        public bool Classification3 { get; set; }
        public bool Classification4 { get; set; }
        public bool Classification5 { get; set; }
        public bool Classification6 { get; set; }
        public string XRef1 { get; set; }
        public string XRef2 { get; set; }
        public string XRef3 { get; set; }
        public string XRef4 { get; set; }
        public string MissingLotNumWarnType { get; set; }
        public string InvalidLotNumWarnType { get; set; }
        public string GLqtyUOM { get; set; }
        public int PriceDA { get; set; }
        public int QtyDA { get; set; }
        public int CostDA { get; set; }
        public bool LogPriceOverrides { get; set; }
        public bool RequirePriceOverrideReason { get; set; }
        public string QtySellWarningMethod { get; set; }
        public bool discountbooklines { get; set; }
        public bool DiscDwnPay { get; set; }
        public bool AutoUpdatePricingOnCostChange { get; set; }
        public bool DiscDwnBook { get; set; }
    }
}