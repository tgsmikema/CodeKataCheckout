namespace CodeKataCheckout.Data;

public static class PricingRulesData
{
    public static IEnumerable<IPricingRule> GetData()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricingRule("A", 50),
            new UnitPricingRule("B", 30),
            new UnitPricingRule("C", 20),
            new UnitPricingRule("D", 15),
            new BulkDiscountRule("A", 50, 3, 130),
            new BulkDiscountRule("B", 30, 2, 45),
            new BulkDiscountRule("A", 50, 6, 250),
            new BulkDiscountRule("B", 30, 4, 80),
            new BulkDiscountRule("C", 20, 3, 50),
        };
        
        return pricingRules;
    }
}