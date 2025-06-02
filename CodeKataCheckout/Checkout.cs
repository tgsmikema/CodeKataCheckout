namespace CodeKataCheckout;

public class Checkout
{
    private readonly IEnumerable<PricingRule> _pricingRules;
    public Checkout(IEnumerable<PricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
    }

    public int Total()
    {
        return 0;
    }
}