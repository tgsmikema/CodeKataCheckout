namespace CodeKataCheckout;

public class Checkout
{
    private readonly List<IPricingRule> _allPricingRules;
    private readonly Dictionary<string, int> _cart;
    private List<IPricingRule> _simpleRules;
    private List<IPricingRule> _specialRules;
    private HashSet<string> _simpleRuleSkus;

    public Checkout(List<IPricingRule> allPricingRules)
    {
        _allPricingRules = allPricingRules;
        _cart = new Dictionary<string, int>();
        
        _simpleRules = new List<IPricingRule>();
        _specialRules = new List<IPricingRule>();
        _simpleRuleSkus = new HashSet<string>();

        CategorisePricingRules();
    }

    public void Scan(string sku)
    {
        if (!_cart.TryGetValue(sku, out var quantity))
        {
            if (!_simpleRuleSkus.Contains(sku))
            {
                throw new ArgumentException($"Sku {sku} not found");
            }
            _cart[sku] = 1;
        }
        else
        {
            _cart[sku] = quantity + 1;
        }
    }

    public decimal Total()
    {
        decimal minimumTotal = CalculateBaseTotal();

        // process each special price rule type individually, eg. bulkPricingRule, MultiSkusPricingRule
        foreach (var ruleType in GetDistinctSpecialRuleTypes())
        {
            decimal currentTotal = 0;
            var remainingSkus = new HashSet<string>(_simpleRuleSkus);
            
            // Apply all special rules of this type
            foreach (var rule in GetRulesByType(ruleType))
            {
                currentTotal += rule.CalculatePrice(_cart);
                remainingSkus.ExceptWith(rule.GetSkus());
            }
            
            //Add simple pricing for remaining items without any special pricing rules
            currentTotal += CalculateSimpleTotalForSkus(remainingSkus);
            
            minimumTotal = Math.Min(minimumTotal, currentTotal);
        }

        return minimumTotal;
    }

    private void CategorisePricingRules()
    {
        foreach (var pricingRule in _allPricingRules)
        {
            if (pricingRule.GetPricingRule() == PricingRule.SimplePricingRule)
            {
                _simpleRules.Add(pricingRule);
                _simpleRuleSkus.UnionWith(pricingRule.GetSkus());
            }
            else
            {
                _specialRules.Add(pricingRule);
            }
        }
    }

    private decimal CalculateBaseTotal()
    {
        decimal total = 0;
        foreach (var pricingRule in _simpleRules)
        {
            total += pricingRule.CalculatePrice(_cart);
        }
        return total;
    }

    private decimal CalculateSimpleTotalForSkus(HashSet<string> skus)
    {
        decimal total = 0;
        foreach (var sku in skus)
        {
            foreach (var rule in _simpleRules)
            {
                if (rule.GetSkus().Contains(sku))
                {
                    total += rule.CalculatePrice(_cart);
                    break;
                }
            }
        }

        return total;
    }

    private IEnumerable<PricingRule> GetDistinctSpecialRuleTypes()
    {
        return _specialRules.Select(r => r.GetPricingRule()).Distinct();
    }

    private IEnumerable<IPricingRule> GetRulesByType(PricingRule ruleType)
    {
        return _specialRules.Where(r => r.GetPricingRule() == ruleType);
    }

    
}