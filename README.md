After interview, understand that I did not prepare my solution well, I'm still quite interested in how to implement this code kata that it is easily extendable.
I have redesigned the code from scratch, still following TDD approach, able to produce this new attempt.

The solution is working in this way:
* Assumption is made that only 1 type of special pricing rule can be applied besides the simple pricing rule, where we choose the pricing rule that cost minimum.
* All pricing rules extends the IPricingRule interface, with common method of CalculatePrice(Dictionary<string, int> cart)
* Checkout Class is constructed in a way that handles all general pricing rules, no modification is needed to Checkout class when new type of pricing rule is added.
* When new type of pricing rule is created, we only need to create the new PricingRule class that extend the IPricingRule interface, add internal logic, and add name to the PricingRule Enum. No other modification is needed in any other classes.

I believe this is a great improvement to the first attempt, where made code more modular and reduce dependency, and easily extendable.
