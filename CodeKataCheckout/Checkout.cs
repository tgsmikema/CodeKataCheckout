namespace CodeKataCheckout;

public class Checkout
{
    private readonly List<IPricingRule> _pricingRules;
    private readonly Dictionary<string, int> _cart;
    private List<IPricingRule> _simplePricingRules;
    private List<IPricingRule> _specialPricingRules;
    public Checkout(List<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
        _cart = new Dictionary<string, int>();
        _simplePricingRules = new List<IPricingRule>();
    }

    public void Scan(string sku)
    {
        if (!_cart.TryGetValue(sku, out var quantity))
        {
            _cart[sku] = 1;
        }
        else
        {
            _cart[sku] = quantity + 1;
        }
    }

    public decimal Total()
    {
        decimal minValue = int.MaxValue;
        decimal total = 0;
        
        var specialPricingRulesTypes = new List<PricingRule>();
        
        foreach (var pricingRule in _pricingRules)
        {
            var specialPricingRuleType = pricingRule.GetPricingRule();
            if (specialPricingRuleType != PricingRule.SimplePricingRule)
            {
                specialPricingRulesTypes.Add(specialPricingRuleType);
            }
        }
        
        HashSet<string> allSkus = new HashSet<string>();

        foreach (var simplePricingRule in _pricingRules)
        {
            if (simplePricingRule.GetPricingRule() == PricingRule.SimplePricingRule)
            {
                allSkus.UnionWith(simplePricingRule.GetSkus());
                _simplePricingRules.Add(simplePricingRule);
            }
        }
        

        foreach (var simplePricingRule in _simplePricingRules)
        {
            total += simplePricingRule.CalculatePrice(_cart);
        }
        
        minValue = Math.Min(minValue, total);

        // loop thru each special rule types
        foreach (var specialPricingRuleType in specialPricingRulesTypes)
        {
            _specialPricingRules = new List<IPricingRule>();

            foreach (var pricingRule in _pricingRules)
            {
                total = 0;
                
                if (pricingRule.GetPricingRule() == specialPricingRuleType)
                {
                    _specialPricingRules.Add(pricingRule);
                    allSkus.ExceptWith(pricingRule.GetSkus());
                }

                foreach (var specialPricingRule in _specialPricingRules)
                {
                    total += specialPricingRule.CalculatePrice(_cart);
                }

                foreach (var skuWithoutDiscount in allSkus)
                {
                    foreach (var simplePricingRule in _simplePricingRules)
                    {
                        if (simplePricingRule.GetSkus().Contains(skuWithoutDiscount))
                        {
                            total += simplePricingRule.CalculatePrice(_cart);
                        }
                    }
                }
                
                minValue = Math.Min(minValue, total);
            }
            
        }

        return minValue;

    }
}