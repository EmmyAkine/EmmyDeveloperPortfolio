using EmmyDeveloperPortfolio.Models;

namespace EmmyDeveloperPortfolio.Services {
    public class TaxServices {

        private readonly TaxBreakdown[] _taxBands;

        public TaxServices() {
            _taxBands = new TaxBreakdown[] {
                new TaxBreakdown{BandLabel = "₦0 – ₦800,000", Rate = 0m, MaxTaxableAmount = 800000m}, //First 800k
                new TaxBreakdown{BandLabel = "₦800,001 – ₦3,000,000", Rate = 0.15m, MaxTaxableAmount = 2200000m}, //800k to 3m
                new TaxBreakdown{BandLabel = "₦3,000,001 – ₦12,000,000", Rate = 0.18m, MaxTaxableAmount = 9000000m}, //3m to 12m
                new TaxBreakdown{BandLabel = "₦12,000,001 – ₦25,000,000", Rate = 0.21m, MaxTaxableAmount = 13000000m}, //12m to 25m
                new TaxBreakdown{BandLabel = "₦25,000,001 – ₦50,000,000", Rate = 0.23m, MaxTaxableAmount = 25000000m}, // 25m to 50m
                new TaxBreakdown{BandLabel = "Above ₦50,000,000", Rate = 0.25m, MaxTaxableAmount = 0}, //Above 50m
            };
        }

        public TaxResultModel Calculate(decimal income, decimal pension, decimal rent) {
            //Calculate 20% of annual rent
            var rentdeduction = Math.Min(500000m, rent * 0.20m);

            var taxableIncome = Math.Max(income - pension - rentdeduction, 0m);

            var progressive = ProgressiveBandsTotalTax(taxableIncome, _taxBands);
            var totalAnnualTax = progressive.totalTax;
            var breakdown = progressive.breakdowns;
            var netIncome = income - pension - totalAnnualTax;

            var taxResultModel = new TaxResultModel {
                Income = income,
                Pension = pension,
                Rent = rent,

                Tax = totalAnnualTax,
                NetIncome = netIncome,
                EffectiveTaxRate = (totalAnnualTax / income) * 100m,
                PensionRelief = pension,
                RentRelief = rentdeduction,
                TaxableIncome = taxableIncome,
                MonthlyNet = netIncome / 12m,
                Breakdowns = breakdown,
            };

            return taxResultModel;
        }

        public static (decimal totalTax, List<TaxBreakdown> breakdowns) ProgressiveBandsTotalTax(decimal taxableIncome, TaxBreakdown[] taxBands) {
            var availBalance = taxableIncome;
            List<TaxBreakdown> breakdowns = new List<TaxBreakdown>();
            if (availBalance <= 0) {
                return (0, breakdowns);
            }

            var totalTax = 0m;
            for (int i = 0; i < taxBands.Length; i++) {
                var taxableAmountForBand = Math.Min(availBalance, taxBands[i].MaxTaxableAmount);
                if ( i == taxBands.Length - 1) {
                    taxableAmountForBand = availBalance; //For above 50m
                }
                var taxForBand = taxableAmountForBand * taxBands[i].Rate;
                totalTax += taxForBand;
                availBalance -= taxableAmountForBand;

                var bandResult = new TaxBreakdown {
                    BandLabel = taxBands[i].BandLabel,
                    MaxTaxableAmount = taxBands[i].MaxTaxableAmount,
                    AmountTaxedInBand = taxableAmountForBand,
                    Rate = taxBands[i].Rate,
                    TaxFromBand = taxForBand    
                };
                breakdowns.Add(bandResult);

                if (availBalance <= 0) {
                    break;
                }
            }

            return (totalTax, breakdowns);
        }
    }

}


//Step 1 — Reliefs (subtracted BEFORE tax bands apply)
//   Pension Relief = 8% of gross income (fully deductible)
//   Rent Relief    = 20% of annual rent, capped at ₦500,000
//                    (₦0 if no rent paid)

//Step 2 — Taxable Income
//   TaxableIncome = GrossIncome - PensionRelief - RentRelief
//   (floor at 0 if negative)

//Step 3 — Progressive Bands (apply to TaxableIncome)
//   First   ₦800,000                  →  0%
//   Next    ₦800,001 – ₦3,000,000     →  15%
//   Next    ₦3,000,001 – ₦12,000,000  →  18%
//   Next    ₦12,000,001 – ₦25,000,000 →  21%
//   Next    ₦25,000,001 – ₦50,000,000 →  23%
//   Above   ₦50,000,000               →  25%

//Step 4 — Same as before
//   NetIncome = GrossIncome - Tax
//   EffectiveTaxRate = (Tax / GrossIncome) * 100
//   MonthlyNet = NetIncome / 12
