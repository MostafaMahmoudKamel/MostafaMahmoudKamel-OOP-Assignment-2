namespace SRP.src.SrpLab.CheckoutBasket;

/// <summary>
/// Online checkout basket: totals,
/// coupon linguistics, 
/// gift-wrap copy,
/// and a faux payment auth code.
/// </summary>
public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;

    //composition
    private readonly DiscountService _discountService ;
    private readonly PaymentService _paymentService;

    private readonly GiftMessageService _giftMessageService ;
    //constructor Dependency injection 
    //used to insuare all service parameter not null
    public CheckoutBasket(
        DiscountService discountService,
        PaymentService paymentService,
        GiftMessageService giftMessageService)
    {
        _discountService = discountService?? throw new ArgumentNullException(nameof(discountService));
        _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        _giftMessageService = giftMessageService ?? throw new ArgumentNullException(nameof(giftMessageService));
    }

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;

    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);


    public decimal GrandTotal()
    {
        var total = SubTotal() - _discountService.DiscountAmount(_couponRaw,SubTotal());
        if (_giftWrap) total += 4.99m; // packaging fee policy ≠ cart math
        return Math.Max(0m, total);
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentService.AuthorizePaymentStub(cardLast4, GrandTotal(), _lines.Count);
    }
    public string GiftMessageCard(string fromName)
    {
        return _giftMessageService.GiftMessageCard(fromName, _lines, GrandTotal());
    }

}
