using System.Security.Principal;
namespace CodeKataCheckout.Tests;

public class CheckoutTests
{
    [Fact]
    public void Total_should_be_zero_when_no_items_scanned()
    {
        var checkout = new Checkout(new List<IPricingRule>());
        
        Assert.Equal(0, checkout.Total());
        
    }

    [Fact]

    public void Total_should_be_simple_total_price_when_items_scanned_with_only_simple_pricing_rule()
    {
        var pricingRules = new List<IPricingRule>
        {
            new SimplePricingRule("A", 50),
            new SimplePricingRule("B", 30)
        };
        var checkout = new Checkout(pricingRules);

        checkout.Scan("A");
        checkout.Scan("B");
        
        Assert.Equal(80, checkout.Total());
    }

    [Fact]
    public void Total_should_be_correct_when_items_scanned_with_bulk_pricing_rule()
    {
        var pricingRules = new List<IPricingRule>
        {
            new SimplePricingRule("A", 50),
            new SimplePricingRule("B", 30),
            new BulkPricingRule("A", 50,3, 130)
        };
        var checkout = new Checkout(pricingRules);

        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("A");
        
        Assert.Equal(160, checkout.Total());
    }
    
    [Fact]
    public void Total_should_be_correct_when_multiple_items_scanned_with_bulk_pricing_rule()
    {
        var pricingRules = new List<IPricingRule>
        {
            new SimplePricingRule("A", 50),
            new SimplePricingRule("B", 30),
            new BulkPricingRule("A", 50,3, 130),
            new BulkPricingRule("B", 30,3, 80)
        };
        var checkout = new Checkout(pricingRules);

        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("B");
        
        Assert.Equal(210, checkout.Total());
    }
}
