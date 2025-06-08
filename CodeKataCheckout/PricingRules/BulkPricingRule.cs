namespace CodeKataCheckout;

public class BulkPricingRule : IPricingRule
{
    private readonly string _sku;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
    private readonly decimal _specialPrice;
    
    public BulkPricingRule(string sku, decimal unitPrice, int quantity, decimal specialPrice)
    {
        _sku = sku;
        _unitPrice = unitPrice;
        _quantity = quantity;
        _specialPrice = specialPrice;
    }
    public decimal CalculatePrice(Dictionary<string, int> cart)
    {
        if (!cart.TryGetValue(_sku, out var count))
        {
            return 0;
        }
        int sets = count / _quantity;
        int remainder = count % _quantity;
        
        decimal total = sets * _specialPrice + remainder * _unitPrice;
        
        return total;
    }

    public PricingRule GetPricingRule()
    {
        return PricingRule.BulkPricingRule;
    }

    public HashSet<string> GetSkus()
    {
        return [_sku];
    }
}