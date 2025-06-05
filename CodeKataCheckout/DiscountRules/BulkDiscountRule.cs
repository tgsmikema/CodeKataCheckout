namespace CodeKataCheckout;

public class BulkDiscountRule : IPricingRule, IDiscountRule
{
    public string Sku { get; }
    private readonly decimal _unitPrice;
    private readonly int _bulkQty;
    private readonly decimal _bulkPrice;

    public BulkDiscountRule(string sku, decimal unitPrice, int bulkQty, decimal bulkPrice)
    {
        Sku = sku;
        _unitPrice = unitPrice;
        _bulkQty = bulkQty;
        _bulkPrice = bulkPrice;
    }
    
    public decimal CalculateDiscount(int quantity) 
    {
        int sets = quantity / _bulkQty;
        decimal discountPerSet = decimal.Abs(_bulkPrice - _bulkQty * _unitPrice);
        decimal totalDiscount = (sets * discountPerSet);
        return totalDiscount;
    }

    public decimal GetUnitPrice()
    {
        return _unitPrice;
    }
}