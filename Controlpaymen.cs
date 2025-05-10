[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;
    private readonly AppDbContext _context;

    public PaymentsController(PaymentService paymentService, AppDbContext context)
    {
        _paymentService = paymentService;
        _context = context;
    }

    [HttpPost("create-checkout-session/{orderId}")]
    public async Task<IActionResult> CreateCheckoutSession(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            return NotFound("Order not found");

        var sessionUrl = await _paymentService.CreateCheckoutSessionAsync(order);
        return Ok(new { url = sessionUrl });
    }
}
