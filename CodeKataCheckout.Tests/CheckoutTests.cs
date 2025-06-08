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

    [Fact]
    public void Total_should_be_correct_when_multiple_items_scanned_with_Buy2AGet1BFree_pricing_rule()
    {
        var pricingRules = new List<IPricingRule>
        {
            new SimplePricingRule("A", 50),
            new SimplePricingRule("B", 30),
            new SimplePricingRule("C", 20),
            new BulkPricingRule("A", 50,3, 130),
            new Buy2AGet1BFreePricingRule("A", 2, 50, "B", 30, 100)
        };
        var checkout = new Checkout(pricingRules);
        
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("A");
        
        //because if 3 x A is 130 from BulkPricing, + 1 x B is 30, total would be 160
        // but if Buy2AGet1BFreePricingRule is used, 2 x A + 1 x B = 100 + 1 x A = 50 = 150 (cheaper)
        Assert.Equal(150, checkout.Total());
    }

    [Fact]
    public void Should_throw_exception_when_sku_scanned_not_in_simple_pricing_rule()
    {
        var pricingRules = new List<IPricingRule>
        {
            new SimplePricingRule("A", 50)
        };
        var checkout = new Checkout(pricingRules);
        
        var ex = Assert.Throws<ArgumentException>(() => checkout.Scan("B"));
        
        Assert.Equal("Sku B not found", ex.Message);
        
    }
}
