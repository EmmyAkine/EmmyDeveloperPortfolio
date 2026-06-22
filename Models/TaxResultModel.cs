namespace EmmyDeveloperPortfolio.Models {
    public class TaxResultModel {
        //Inputs
        public decimal Income { get; set; }
        public decimal Pension { get; set; }
        public decimal Rent { get; set; }


        //OutPuts
        public decimal Tax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal EffectiveTaxRate { get; set; }
        public decimal PensionRelief { get; set; }
        public decimal RentRelief { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal MonthlyNet { get; set; }

        public List<TaxBreakdown> Breakdowns { get; set; } = new List<TaxBreakdown>();
    }
}

public class TaxBreakdown {
    public string BandLabel { get; set; } = "";
    public decimal Rate { get; set; }
    public decimal TaxFromBand { get; set; }
    public decimal MaxTaxableAmount { get; set; }

}
