namespace FlooringProgram.Models
{
    public class Order 
    {
        // Auto-generate
        public int OrderNumber { get; set; }  
        public string OrderDate { get; set; }

        // Input
        public string CustomerName { get; set; }
        public string StateAbbreviation { get; set; }
        public string ProductType { get; set; }
        public decimal TotalArea { get; set; }

        // Lookup from ProductRepo
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }

        // Lookup from StateRepo
        public decimal TaxRate { get; set; }

        // Calculated
        public decimal MaterialCost => TotalArea*CostPerSquareFoot;
        public decimal LaborCost => TotalArea*LaborCostPerSquareFoot;
        public decimal Tax => (MaterialCost + LaborCost)*(TaxRate/100);
        public decimal TotalPrice => MaterialCost + LaborCost + Tax;
    }
}