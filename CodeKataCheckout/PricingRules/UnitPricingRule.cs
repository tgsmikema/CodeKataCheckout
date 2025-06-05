namespace CodeKataCheckout;

public class UnitPricingRule : IPricingRule
{
    public string Sku { get; }
    private readonly decimal _unitPrice;

    public UnitPricingRule(string sku, decimal unitPrice)
    {
        Sku = sku;
        _unitPrice = unitPrice;
    }
    
    public decimal GetUnitPrice()
    {
        return _unitPrice;
    }
}