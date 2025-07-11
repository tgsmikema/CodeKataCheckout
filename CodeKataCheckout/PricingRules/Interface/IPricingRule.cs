namespace CodeKataCheckout;

public interface IPricingRule
{
    decimal CalculatePrice(Dictionary<string, int> cart);
    PricingRule GetPricingRule();
    HashSet<string> GetSkus();
}